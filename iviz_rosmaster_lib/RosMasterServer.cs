using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs.RosgraphMsgs;
using Iviz.Roslib;
using Iviz.XmlRpc;
using HttpListenerContext = Iviz.XmlRpc.HttpListenerContext;
using Logger = Iviz.Msgs.Logger;

namespace Iviz.RosMaster
{
    public sealed class RosMasterServer : IDisposable
    {
        public const int DefaultPort = 11311;

        readonly HttpListener listener;

        readonly Dictionary<string, Func<object[], Arg[]>> methods;

        readonly Dictionary<string, Func<object[], Task>> lateCallbacks;

        readonly Dictionary<string, Dictionary<string, Uri>> publishersByTopic =
            new Dictionary<string, Dictionary<string, Uri>>();

        readonly Dictionary<string, Dictionary<string, Uri>> subscribersByTopic =
            new Dictionary<string, Dictionary<string, Uri>>();

        readonly Dictionary<string, Uri> serviceProviders = new Dictionary<string, Uri>();

        readonly Dictionary<string, string> topicTypes = new Dictionary<string, string>();

        readonly Dictionary<string, Arg> parameters = new Dictionary<string, Arg>();

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

            methods = new Dictionary<string, Func<object[], Arg[]>>
            {
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
            };

            lateCallbacks = new Dictionary<string, Func<object[], Task>>
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

            disposed = true;
        }

        public override string ToString()
        {
            return $"[RosMaster Uri={MasterUri}]";
        }

        public void AddKey(string key, Arg value)
        {
            Logger.Log($"** Adding key '{key}'");
            parameters[key] = value;
        }

        public async Task Start()
        {
            Logger.Log($"** {this}: Starting at {MasterUri}");
            Task startTask = listener.StartAsync(StartContext, false);
            await ManageRosoutAggAsync().AwaitNoThrow(this);
            await startTask.AwaitNoThrow(this);
            Logger.Log($"** {this}: Leaving thread.");
        }

        async Task StartContext(HttpListenerContext context, CancellationToken token)
        {
            CancellationTokenSource linkedTs = CancellationTokenSource.CreateLinkedTokenSource(token, runningTs.Token);

            try
            {
                await XmlRpcService.MethodResponseAsync(context, methods, lateCallbacks, linkedTs.Token);
            }
            catch (Exception e)
            {
                Logger.LogError(e);
            }
        }

        async Task ManageRosoutAggAsync()
        {
            Uri ownUri = new Uri($"http://{MasterUri.Host}:0");
            RosClient client = new RosClient(MasterUri, "/rosout", ownUri);
            RosChannelReader<Log> reader = new RosChannelReader<Log>();
            RosChannelWriter<Log> writer = new RosChannelWriter<Log>();

            try
            {
                await reader.StartAsync(client, "/rosout");
                await writer.StartAsync(client, "/rosout_agg");

                while (!runningTs.IsCancellationRequested)
                {
                    Log log = await reader.ReadAsync(runningTs.Token);
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

        static readonly Arg[] DefaultOkResponse = OkResponse(0);

        static Arg[] OkResponse(Arg arg)
        {
            return new Arg[] {StatusCode.Success, "ok", arg};
        }

        static Arg[] ErrorResponse(string msg)
        {
            return new Arg[] {StatusCode.Error, msg, 0};
        }

        Arg[] GetUri(object[] _)
        {
            return OkResponse(MasterUri);
        }

        Arg[] RegisterPublisher(object[] args)
        {
            if (args.Length != 4 ||
                !(args[0] is string callerId) ||
                !(args[1] is string topic) ||
                !(args[2] is string topicType) ||
                !(args[3] is string callerApi))
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

            return OkResponse(new Arg(currentSubscribers));
        }

        async Task RegisterPublisherLateCallback(object[] args)
        {
            if (!(args[1] is string topic) ||
                !subscribersByTopic.TryGetValue(topic, out var subscribers))
            {
                return;
            }

            IEnumerable<Uri> publisherUris =
                publishersByTopic.TryGetValue(topic, out var publishers)
                    ? (IEnumerable<Uri>) publishers.Values
                    : Array.Empty<Uri>();

            Arg[] methodArgs = {MasterCallerId, topic, new Arg(publisherUris)};

            foreach (var uri in subscribers.Values)
            {
                NotifySubscriber(uri, methodArgs);
            }

            await Task.CompletedTask;
        }

        async void NotifySubscriber(Uri remoteUri, IEnumerable<Arg> methodArgs)
        {
            try
            {
                // note! will block until timeout if node is not reachable
                await XmlRpcService.MethodCallAsync(remoteUri, MasterUri, "publisherUpdate", methodArgs);
            }
            catch (Exception e)
            {
                Logger.LogDebug(e);
            }
        }

        Arg[] RegisterSubscriber(object[] args)
        {
            if (args.Length != 4 ||
                !(args[0] is string callerId) ||
                !(args[1] is string topic) ||
                !(args[3] is string callerApi))
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

            return OkResponse(new Arg(currentPublishers));
        }

        Arg[] UnregisterSubscriber(object[] args)
        {
            if (args.Length != 3 ||
                !(args[0] is string callerId) ||
                !(args[1] is string topic) ||
                !(args[2] is string callerApi))
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

        Arg[] UnregisterPublisher(object[] args)
        {
            if (args.Length != 3 ||
                !(args[0] is string callerId) ||
                !(args[1] is string topic) ||
                !(args[2] is string callerApi))
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

        Arg[] RegisterService(object[] args)
        {
            if (args.Length != 4 ||
                !(args[1] is string service) ||
                !(args[2] is string serviceApi))
            {
                return ErrorResponse("Failed to parse arguments");
            }

            if (!Uri.TryCreate(serviceApi, UriKind.Absolute, out Uri serviceUri))
            {
                return ErrorResponse("Service api is not an uri");
            }

            Logger.Log($"++ Service: {service}");

            serviceProviders[service] = serviceUri;

            return DefaultOkResponse;
        }

        Arg[] UnregisterService(object[] args)
        {
            if (args.Length != 3 ||
                !(args[1] is string service) ||
                !(args[2] is string serviceApi))
            {
                return ErrorResponse("Failed to parse arguments");
            }

            if (!Uri.TryCreate(serviceApi, UriKind.Absolute, out Uri serviceUri))
            {
                return ErrorResponse("Service api is not an uri");
            }

            if (!serviceProviders.TryGetValue(service, out Uri currentServiceUri) || serviceUri != currentServiceUri)
            {
                return DefaultOkResponse;
            }

            serviceProviders.Remove(service);
            return OkResponse(1);
        }

        Arg[] LookupNode(object[] args)
        {
            if (args.Length != 2 ||
                !(args[1] is string node))
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

        Arg[] LookupService(object[] args)
        {
            if (args.Length != 2 ||
                !(args[1] is string service))
            {
                return ErrorResponse("Failed to parse arguments");
            }

            return serviceProviders.TryGetValue(service, out Uri providerUri)
                ? OkResponse(providerUri)
                : ErrorResponse($"No service with name '{service}'");
        }

        Arg[] GetPublishedTopics(object[] _)
        {
            var topics = topicTypes.Select(pair => (pair.Key, pair.Value));

            return OkResponse(new Arg(topics));
        }

        Arg[] GetTopicTypes(object[] _)
        {
            var topics = topicTypes.Select(pair => (pair.Key, pair.Value));

            return OkResponse(new Arg(topics));
        }

        Arg[] GetSystemState(object[] _)
        {
            var publishers = publishersByTopic.Select(
                pair => new Arg[] {pair.Key, new Arg(pair.Value.Select(tuple => tuple.Value))});
            var subscribers = subscribersByTopic.Select(
                pair => new Arg[] {pair.Key, new Arg(pair.Value.Select(tuple => tuple.Value))});
            var providers = serviceProviders.Select(
                pair => new Arg[] {pair.Key, new Arg(Yield(pair.Value))});

            return OkResponse(new[] {new Arg(publishers), new Arg(subscribers), new Arg(providers)});
        }

        Arg[] DeleteParam(object[] args)
        {
            if (args.Length != 2 ||
                !(args[1] is string key))
            {
                return ErrorResponse("Failed to parse arguments");
            }

            parameters.Remove(key);
            return DefaultOkResponse;
        }

        Arg[] SetParam(object[] args)
        {
            Arg arg;

            if (args.Length != 3 ||
                !(args[1] is string key) ||
                !(arg = Arg.Create(args[2])).IsValid)
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

            parameters[key] = arg;
            return DefaultOkResponse;
        }

        Arg[] GetParam(object[] args)
        {
            if (args.Length != 2 ||
                !(args[1] is string key))
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

            if (parameters.TryGetValue(key, out Arg arg))
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
                return ErrorResponse($"Parameter '{key}' is not set");
            }

            arg = new Arg(Enumerable.Select(candidates, pair => (pair.Key, pair.Value)));
            return OkResponse(arg);
        }

        Arg[] GetParamNames(object[] args)
        {
            return OkResponse(new Arg(parameters.Keys));
        }

        Arg[] HasParam(object[] args)
        {
            if (args.Length != 2 ||
                !(args[1] is string key))
            {
                return ErrorResponse("Failed to parse arguments");
            }

            return OkResponse(parameters.ContainsKey(key));
        }

        static Arg[] SubscribeParam(object[] _)
        {
            return ErrorResponse("Not implemented yet");
        }

        static Arg[] UnsubscribeParam(object[] _)
        {
            return ErrorResponse("Not implemented yet");
        }

        static Arg[] SearchParam(object[] _)
        {
            return ErrorResponse("Not implemented yet");
        }

        static IEnumerable<T> Yield<T>(T element)
        {
            yield return element;
        }
    }
}