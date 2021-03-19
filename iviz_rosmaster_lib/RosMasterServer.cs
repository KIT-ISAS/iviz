using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs.RosgraphMsgs;
using Iviz.Roslib;
using Iviz.XmlRpc;
using Nito.AsyncEx;
using HttpListenerContext = Iviz.XmlRpc.HttpListenerContext;
using Logger = Iviz.Msgs.Logger;

namespace Iviz.RosMaster
{
    public sealed class RosMasterServer : IDisposable
    {
        public const int DefaultPort = 11311;

        readonly HttpListener listener;

        readonly AsyncLock rosLock = new AsyncLock();

        readonly Dictionary<string, Func<XmlRpcValue[], XmlRpcArg[]>> methods;

        readonly Dictionary<string, Func<XmlRpcValue[], CancellationToken, Task>> lateCallbacks;

        readonly Dictionary<string, Dictionary<string, Uri>> publishersByTopic =
            new Dictionary<string, Dictionary<string, Uri>>();

        readonly Dictionary<string, Dictionary<string, Uri>> subscribersByTopic =
            new Dictionary<string, Dictionary<string, Uri>>();

        readonly Dictionary<string, (string Id, Uri Uri)> serviceProviders = new Dictionary<string, (string, Uri)>();

        readonly Dictionary<string, string> topicTypes = new Dictionary<string, string>();

        readonly Dictionary<string, XmlRpcArg> parameters = new Dictionary<string, XmlRpcArg>();

        readonly CancellationTokenSource runningTs = new CancellationTokenSource();

        public Uri MasterUri { get; }
        public string MasterCallerId { get; }

        static class StatusCode
        {
            public const int Error = -1;
            public const int Failure = 0;
            public const int Success = 1;
        }

        public RosMasterServer(Uri masterUri, string callerId = "/iviz_master")
        {
            MasterUri = masterUri;
            MasterCallerId = callerId;
            listener = new HttpListener(masterUri.Port);

            methods = new Dictionary<string, Func<XmlRpcValue[], XmlRpcArg[]>>
            {
                ["getPid"] = GetPid,
                ["getUri"] = GetUri,
                ["registerPublisher"] = RegisterPublisher,
                ["registerSubscriber"] = RegisterSubscriber,
                ["lookupNode"] = LookupNode,
                ["getTopicTypes"] = GetTopicTypes,
                ["getPublishedTopics"] = GetPublishedTopics,
                ["getSystemState"] = GetSystemState,
                ["deleteParam"] = DeleteParam,
                ["setParam"] = SetParam,
                ["getParam"] = GetParam,
                ["hasParam"] = HasParam,
                ["getParamNames"] = GetParamNames,
                ["subscribeParam"] = SubscribeParam,
                ["unsubscribeParam"] = UnsubscribeParam,
                ["searchParam"] = SearchParam,
                ["registerService"] = RegisterService,
                ["unregisterService"] = UnregisterService,
                ["lookupService"] = LookupService,
                ["unregisterPublisher"] = UnregisterPublisher,
                ["unregisterSubscriber"] = UnregisterSubscriber,
                ["system.multicall"] = SystemMulticall,
            };

            lateCallbacks = new Dictionary<string, Func<XmlRpcValue[], CancellationToken, Task>>
            {
                ["registerPublisher"] = RegisterPublisherLateCallback,
                ["unregisterPublisher"] = RegisterPublisherLateCallback
            };
        }

        bool disposed;

        public void Dispose()
        {
            if (disposed)
            {
                return;
            }

            runningTs.Cancel();
            listener.Dispose();
            runningTs.Dispose();

            disposed = true;
        }

        public override string ToString()
        {
            return $"[RosMaster Uri={MasterUri}]";
        }

        public void AddKey(string key, XmlRpcArg value)
        {
            Logger.Log($"** Adding key '{key}'");
            parameters[key] = value;
        }

        public async Task StartAsync()
        {
            Logger.Log($"** {this}: Starting at {MasterUri}");
            AddKey("/run_id", Guid.NewGuid().ToString());
            Task startTask = listener.StartAsync(StartContext, true);
            Task rosoutTask = ManageRosoutAggAsync().AwaitNoThrow(this);
            await (startTask, rosoutTask).WhenAll();
            Logger.Log($"** {this}: Leaving thread.");
        }

        async Task StartContext(HttpListenerContext context, CancellationToken token)
        {
            using var linkedTs = CancellationTokenSource.CreateLinkedTokenSource(token, runningTs.Token);

            try
            {
                await XmlRpcService.MethodResponseAsync(context, methods, lateCallbacks, linkedTs.Token);
            }
            catch (Exception e)
            {
                Logger.LogErrorFormat("{0}: Error in StartContext: {1}", this, e);
            }
        }

        async Task ManageRosoutAggAsync()
        {
            Uri ownUri = new Uri($"http://{MasterUri.Host}:0");
            RosClient client = await RosClient.CreateAsync(MasterUri, "/rosout", ownUri);
            RosChannelReader<Log> reader = new RosChannelReader<Log>();
            RosChannelWriter<Log> writer = new RosChannelWriter<Log>();

            try
            {
                await reader.StartAsync(client, "/rosout");
                await writer.StartAsync(client, "/rosout_agg");

                while (!runningTs.IsCancellationRequested)
                {
                    Log log = await reader.ReadAsync(runningTs.Token);
                    //Console.WriteLine(log.Msg);
                    await writer.WriteAsync(log);
                }
            }
            finally
            {
                await writer.DisposeAsync();
                await reader.DisposeAsync();
                await client.DisposeAsync();
            }
        }

        static readonly XmlRpcArg[] DefaultOkResponse = OkResponse(0);

        static XmlRpcArg[] OkResponse(XmlRpcArg arg)
        {
            return new XmlRpcArg[] {StatusCode.Success, "ok", arg};
        }

        static XmlRpcArg[] ErrorResponse(string msg)
        {
            return new XmlRpcArg[] {StatusCode.Error, msg, 0};
        }

        static XmlRpcArg[] GetPid(XmlRpcValue[] _)
        {
#if NET5_0
            int id = Environment.ProcessId;
#else
            int id = System.Diagnostics.Process.GetCurrentProcess().Id;
#endif
            return OkResponse(id);
        }

        XmlRpcArg[] GetUri(XmlRpcValue[] _)
        {
            return OkResponse(MasterUri);
        }

        XmlRpcArg[] RegisterPublisher(XmlRpcValue[] args)
        {
            using var myLock = rosLock.Lock();
            if (args.Length != 4 ||
                !args[0].TryGetString(out string callerId) ||
                !args[1].TryGetString(out string topic) ||
                !args[2].TryGetString(out string topicType) ||
                !args[3].TryGetString(out string callerApi))
            {
                return ErrorResponse("Failed to parse arguments");
            }

            if (!Uri.TryCreate(callerApi, UriKind.Absolute, out Uri callerUri))
            {
                return ErrorResponse("Caller api is not an uri");
            }

            if (!publishersByTopic.TryGetValue(topic, out var publishers))
            {
                publishers = new Dictionary<string, Uri>();
                publishersByTopic[topic] = publishers;
                topicTypes[topic] = topicType;

                Logger.Log($"++ Topic: {topic} [{topicType}]");
            }

            publishers[callerId] = callerUri;
            Logger.Log($"++ Publisher: {callerId}@{callerUri} -> {topic}");

            IEnumerable<Uri> currentSubscribers =
                subscribersByTopic.TryGetValue(topic, out var subscribers)
                    ? (IEnumerable<Uri>) subscribers.Values
                    : Array.Empty<Uri>();

            return OkResponse(new XmlRpcArg(currentSubscribers));
        }

        async Task RegisterPublisherLateCallback(XmlRpcValue[] args, CancellationToken token)
        {
            XmlRpcArg[] methodArgs;
            Dictionary<string, Uri> subscribers;

            {
                using var myLock = await rosLock.LockAsync(token);

                if (!args[1].TryGetString(out string topic) ||
                    !subscribersByTopic.TryGetValue(topic, out subscribers))
                {
                    return;
                }

                IEnumerable<Uri> publisherUris =
                    publishersByTopic.TryGetValue(topic, out var publishers)
                        ? (IEnumerable<Uri>) publishers.Values
                        : Array.Empty<Uri>();

                methodArgs = new XmlRpcArg[] {MasterCallerId, topic, new XmlRpcArg(publisherUris)};
            }

            token.ThrowIfCancellationRequested();
            foreach (var uri in subscribers.Values)
            {
                NotifySubscriber(uri, methodArgs, token);
            }
        }

        async void NotifySubscriber(Uri remoteUri, XmlRpcArg[] methodArgs, CancellationToken token)
        {
            try
            {
                await XmlRpcService.MethodCallAsync(remoteUri, MasterUri, "publisherUpdate", methodArgs, token);
            }
            catch (Exception e)
            {
                Logger.LogFormat("{0}: {1}", this, e);
            }
        }

        XmlRpcArg[] RegisterSubscriber(XmlRpcValue[] args)
        {
            using var myLock = rosLock.Lock();

            if (args.Length != 4 ||
                !args[0].TryGetString(out string callerId) ||
                !args[1].TryGetString(out string topic) ||
                !args[3].TryGetString(out string callerApi))
            {
                return ErrorResponse("Failed to parse arguments");
            }

            if (!Uri.TryCreate(callerApi, UriKind.Absolute, out Uri callerUri))
            {
                return ErrorResponse("Caller api is not an uri");
            }

            if (!subscribersByTopic.TryGetValue(topic, out var subscribers))
            {
                subscribers = new Dictionary<string, Uri>();
                subscribersByTopic[topic] = subscribers;
            }

            subscribers[callerId] = callerUri;

            IEnumerable<Uri> currentPublishers =
                publishersByTopic.TryGetValue(topic, out var publishers)
                    ? (IEnumerable<Uri>) publishers.Values
                    : Array.Empty<Uri>();

            return OkResponse(new XmlRpcArg(currentPublishers));
        }

        XmlRpcArg[] UnregisterSubscriber(XmlRpcValue[] args)
        {
            using var myLock = rosLock.Lock();

            if (args.Length != 3 ||
                !args[0].TryGetString(out string callerId) ||
                !args[1].TryGetString(out string topic) ||
                !args[2].TryGetString(out string callerApi))
            {
                return ErrorResponse("Failed to parse arguments");
            }

            if (!Uri.TryCreate(callerApi, UriKind.Absolute, out Uri callerUri))
            {
                return ErrorResponse("Caller api is not an uri");
            }

            if (!subscribersByTopic.TryGetValue(topic, out var subscribers) ||
                !(subscribers.TryGetValue(callerId, out Uri tmpUri) && tmpUri == callerUri) ||
                !subscribers.Remove(callerId))
            {
                return DefaultOkResponse;
            }

            if (subscribers.Count == 0)
            {
                subscribersByTopic.Remove(topic);
            }

            return OkResponse(1);
        }

        XmlRpcArg[] UnregisterPublisher(XmlRpcValue[] args)
        {
            using var myLock = rosLock.Lock();

            if (args.Length != 3 ||
                !args[0].TryGetString(out string callerId) ||
                !args[1].TryGetString(out string topic) ||
                !args[2].TryGetString(out string callerApi))
            {
                return ErrorResponse("Failed to parse arguments");
            }

            if (!Uri.TryCreate(callerApi, UriKind.Absolute, out Uri callerUri))
            {
                return ErrorResponse("Caller api is not an uri");
            }

            if (!publishersByTopic.TryGetValue(topic, out var publishers) ||
                !(publishers.TryGetValue(callerId, out Uri tmpUri) && tmpUri == callerUri) ||
                !publishers.Remove(callerId))
            {
                return DefaultOkResponse;
            }

            Logger.Log($"-- Publisher: {callerId}@{callerUri} -> {topic}");

            if (publishers.Count == 0)
            {
                publishersByTopic.Remove(topic);
                topicTypes.Remove(topic);

                Logger.Log($"-- Topic: {topic}");
            }

            return OkResponse(1);
        }

        XmlRpcArg[] RegisterService(XmlRpcValue[] args)
        {
            using var myLock = rosLock.Lock();

            if (args.Length != 4 ||
                !args[0].TryGetString(out string callerId) ||
                !args[1].TryGetString(out string service) ||
                !args[2].TryGetString(out string serviceApi))
            {
                return ErrorResponse("Failed to parse arguments");
            }

            if (!Uri.TryCreate(serviceApi, UriKind.Absolute, out Uri serviceUri))
            {
                return ErrorResponse("Service api is not an uri");
            }

            Logger.Log($"++ Service: {service}");

            serviceProviders[service] = (callerId, serviceUri);

            return DefaultOkResponse;
        }

        XmlRpcArg[] UnregisterService(XmlRpcValue[] args)
        {
            using var myLock = rosLock.Lock();

            if (args.Length != 3 ||
                !args[1].TryGetString(out string service) ||
                !args[2].TryGetString(out string serviceApi))
            {
                return ErrorResponse("Failed to parse arguments");
            }

            if (!Uri.TryCreate(serviceApi, UriKind.Absolute, out Uri serviceUri))
            {
                return ErrorResponse("Service api is not an uri");
            }

            if (!serviceProviders.TryGetValue(service, out var currentService) || serviceUri != currentService.Uri)
            {
                return DefaultOkResponse;
            }

            Logger.Log($"-- Service: {service}");
            serviceProviders.Remove(service);
            return OkResponse(1);
        }

        XmlRpcArg[] LookupNode(XmlRpcValue[] args)
        {
            using var myLock = rosLock.Lock();

            if (args.Length != 2 ||
                !args[1].TryGetString(out string node))
            {
                return ErrorResponse("Failed to parse arguments");
            }

            var publishersLookup = publishersByTopic.SelectMany(pair => pair.Value);
            var subscribersLookup = subscribersByTopic.SelectMany(pair => pair.Value);

            Uri uri = publishersLookup
                .Concat(subscribersLookup)
                .FirstOrDefault(tuple => tuple.Key == node)
                .Value;

            return uri is null
                ? ErrorResponse($"No node with id '{node}'")
                : OkResponse(uri);
        }

        XmlRpcArg[] LookupService(XmlRpcValue[] args)
        {
            using var myLock = rosLock.Lock();

            if (args.Length != 2 ||
                !args[1].TryGetString(out string service))
            {
                return ErrorResponse("Failed to parse arguments");
            }

            return serviceProviders.TryGetValue(service, out var provider)
                ? OkResponse(provider.Uri)
                : ErrorResponse($"No service with name '{service}'");
        }

        XmlRpcArg[] GetPublishedTopics(XmlRpcValue[] _)
        {
            using var myLock = rosLock.Lock();

            var topics = topicTypes.Select(pair => (pair.Key, pair.Value));

            return OkResponse(new XmlRpcArg(topics));
        }

        XmlRpcArg[] GetTopicTypes(XmlRpcValue[] _)
        {
            using var myLock = rosLock.Lock();

            var topics = topicTypes.Select(pair => (pair.Key, pair.Value));

            return OkResponse(new XmlRpcArg(topics));
        }

        XmlRpcArg[] GetSystemState(XmlRpcValue[] _)
        {
            using var myLock = rosLock.Lock();

            var publishers = publishersByTopic.Select(
                    pair => new XmlRpcArg[] {pair.Key, new XmlRpcArg(pair.Value.Select(tuple => tuple.Key))})
                .ToArray();
            var subscribers = subscribersByTopic.Select(
                    pair => new XmlRpcArg[] {pair.Key, new XmlRpcArg(pair.Value.Select(tuple => tuple.Key))})
                .ToArray();
            var providers = serviceProviders.Select(
                pair => new XmlRpcArg[] {pair.Key, new XmlRpcArg(new[] {pair.Value.Id})}).ToArray();

            return OkResponse(new[] {new XmlRpcArg(publishers), new XmlRpcArg(subscribers), new XmlRpcArg(providers)});
        }

        XmlRpcArg[] DeleteParam(XmlRpcValue[] args)
        {
            using var myLock = rosLock.Lock();

            if (args.Length != 2 ||
                !args[1].TryGetString(out string key))
            {
                return ErrorResponse("Failed to parse arguments");
            }

            //Console.WriteLine("**** Delete " + key);

            parameters.Remove(key);
            return DefaultOkResponse;
        }

        XmlRpcArg[] SetParam(XmlRpcValue[] args)
        {
            using var myLock = rosLock.Lock();


            if (args.Length != 3 ||
                !args[1].TryGetString(out string key))
            {
                return ErrorResponse("Failed to parse arguments");
            }

            if (key.Length == 0)
            {
                return ErrorResponse("Empty key");
            }

            if (key[0] != '/')
            {
                key = "/" + key;
            }

            if (args[2].TryGetArray(out XmlRpcValue[] argObj) &&
                argObj.Length != 0 &&
                argObj[0].TryGetStruct(out (string, XmlRpcValue)[] argEntries))
            {
                try
                {
                    AddDictionary(argEntries, key);
                }
                catch (ParseException e)
                {
                    return ErrorResponse(e.Message);
                }
            }
            else
            {
                XmlRpcArg arg = args[2].AsArg();
                if (!arg.IsValid)
                {
                    return ErrorResponse($"Parameter [{key}] could not be parsed'");
                }

                Console.WriteLine("++ Param " + key + " --> " + args[2]);
                parameters[key] = arg;
            }

            return DefaultOkResponse;
        }

        void AddDictionary((string, XmlRpcValue)[] entries, string root)
        {
            foreach ((string name, XmlRpcValue value) in entries)
            {
                string key = $"{root}/{name}";

                if (value.TryGetStruct(out (string, XmlRpcValue)[] subEntries))
                {
                    AddDictionary(subEntries, key);
                }
                else
                {
                    XmlRpcArg arg = value.AsArg();
                    if (!arg.IsValid)
                    {
                        throw new ParseException($"Parameter [{key}] could not be parsed'");
                    }

                    Console.WriteLine("++ Param " + key + " --> " + value);
                    parameters[key] = arg;
                }
            }
        }

        XmlRpcArg[] GetParam(XmlRpcValue[] args)
        {
            using var myLock = rosLock.Lock();

            if (args.Length != 2 ||
                !args[1].TryGetString(out string key))
            {
                return ErrorResponse("Failed to parse arguments");
            }

            if (key.Length == 0)
            {
                return ErrorResponse("Empty key");
            }

            if (key[0] != '/')
            {
                key = "/" + key;
            }

            if (parameters.TryGetValue(key, out XmlRpcArg arg))
            {
                return OkResponse(arg);
            }

            string keyAsNamespace = key;
            if (keyAsNamespace[keyAsNamespace.Length - 1] != '/')
            {
                keyAsNamespace += "/";
            }

            var candidates = parameters.Where(pair => pair.Key.StartsWith(keyAsNamespace)).ToArray();
            if (candidates.Length == 0)
            {
                //Console.WriteLine("**** " + key + " is missing");
                return ErrorResponse($"Parameter [{key}] is not set");
            }

            arg = new XmlRpcArg(candidates.Select(pair => (pair.Key, pair.Value)).ToArray());
            return OkResponse(arg);
        }

        XmlRpcArg[] GetParamNames(XmlRpcValue[] args)
        {
            using var myLock = rosLock.Lock();

            return OkResponse(new XmlRpcArg(parameters.Keys));
        }

        XmlRpcArg[] HasParam(XmlRpcValue[] args)
        {
            using var myLock = rosLock.Lock();

            if (args.Length != 2 ||
                !args[1].TryGetString(out string key))
            {
                return ErrorResponse("Failed to parse arguments");
            }

            bool success = parameters.ContainsKey(key);
            if (!success)
            {
                //Console.WriteLine("****© " + key + " is missing");
            }

            return OkResponse(success);
        }

        static XmlRpcArg[] SubscribeParam(XmlRpcValue[] _)
        {
            return ErrorResponse("Not implemented yet");
        }

        static XmlRpcArg[] UnsubscribeParam(XmlRpcValue[] _)
        {
            return ErrorResponse("Not implemented yet");
        }

        static XmlRpcArg[] SearchParam(XmlRpcValue[] _)
        {
            return ErrorResponse("Not implemented yet");
        }

        XmlRpcArg[] SystemMulticall(XmlRpcValue[] args)
        {
            if (args.Length != 1 ||
                !args[0].TryGetArray(out XmlRpcValue[] calls))
            {
                return ErrorResponse("Failed to parse arguments");
            }

            List<XmlRpcArg> responses = new List<XmlRpcArg>(calls.Length);
            foreach (var callObject in calls)
            {
                if (!callObject.TryGetStruct(out (string ElementName, XmlRpcValue Element)[] call))
                {
                    return ErrorResponse("Failed to parse arguments");
                }

                string methodName = null;
                XmlRpcValue[] arguments = null;
                foreach ((string elementName, XmlRpcValue element) in call)
                {
                    switch (elementName)
                    {
                        case "methodName":
                        {
                            if (!element.TryGetString(out string elementStr))
                            {
                                return ErrorResponse("Failed to parse methodname");
                            }

                            methodName = elementStr;
                            break;
                        }
                        case "params":
                        {
                            if (!element.TryGetArray(out XmlRpcValue[] elementObjs) ||
                                elementObjs.Length == 0)
                            {
                                return ErrorResponse("Failed to parse params");
                            }

                            arguments = elementObjs;
                            break;
                        }
                        default:
                            return ErrorResponse("Failed to parse struct array");
                    }
                }

                if (methodName == null || arguments == null)
                {
                    return ErrorResponse("methodname or params missing");
                }

                if (!methods.TryGetValue(methodName, out var method))
                {
                    return ErrorResponse("Method not found");
                }

                XmlRpcArg response = (XmlRpcArg) method(arguments);
                responses.Add(response);

                if (lateCallbacks != null &&
                    lateCallbacks.TryGetValue(methodName, out var lateCallback))
                {
                    lateCallback(args, default);
                }
            }

            return new[] {(XmlRpcArg) responses.ToArray()};
        }
    }
}