using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Iviz.XmlRpc;
using HttpListenerContext = Iviz.XmlRpc.HttpListenerContext;
using Logger = Iviz.Msgs.Logger;

namespace Iviz.RosMaster
{
    public sealed class Server : IDisposable
    {
        public const int DefaultPort = 11311;

        readonly HttpListener listener;

        readonly Dictionary<string, Func<object[], Arg[]>> methods;

        readonly Dictionary<string, Func<object[], Task>> lateCallbacks;

        readonly Dictionary<string, HashSet<(string callerId, Uri callerUri)>> publishersByTopic =
            new Dictionary<string, HashSet<(string, Uri)>>();

        readonly Dictionary<string, HashSet<(string callerId, Uri callerUri)>> subscribersByTopic =
            new Dictionary<string, HashSet<(string, Uri)>>();

        readonly Dictionary<string, Uri> serviceProviders = new Dictionary<string, Uri>();

        readonly Dictionary<string, string> topicTypes = new Dictionary<string, string>();

        readonly Dictionary<string, Arg> parameters = new Dictionary<string, Arg>();

        public Uri MasterUri { get; }
        public string MasterCallerId { get; }
        
        static class StatusCode
        {
            public const int Error = -1;
            public const int Failure = 0;
            public const int Success = 1;
        }

        public Server(Uri masterUri, string callerId = "/iviz_master")
        {
            MasterUri = masterUri;
            MasterCallerId = callerId;
            listener = new HttpListener(masterUri);

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

            listener.Dispose();
            disposed = true;
        }

        public void AddKey(string key, Arg value)
        {
            Logger.Log("** Adding key '" + key + "'");
            parameters[key] = value;
        }

        public async Task Start()
        {
            Logger.Log( "** Starting at " + MasterUri);
            await listener.Start(StartContext);
            Logger.Log("** Leaving thread.");
        }

        async Task StartContext(HttpListenerContext context)
        {
            using (context)
            {
                try
                {
                    await Service.MethodResponseAsync(context, methods, lateCallbacks);
                }
                catch (Exception e)
                {
                    Logger.LogError(e);
                }
            }            
        }

        static Arg[] OkResponse(Arg arg)
        {
            return new[] {StatusCode.Success, "ok", arg};
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
                publishers = new HashSet<(string, Uri)>();
                publishersByTopic[topic] = publishers;
                topicTypes[topic] = topicType;

                Logger.Log($"++ Topic: {topic} [{topicType}]");
            }

            if (publishers.Add((callerId, callerUri)))
            {
                Logger.Log($"++ Publisher: {callerId}@{callerUri} -> {topic}");
            }

            IEnumerable<Uri> currentSubscribers =
                subscribersByTopic.TryGetValue(topic, out var subscribers)
                    ? subscribers.Select(tuple => tuple.callerUri)
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
                    ? publishers.Select(tuple => tuple.callerUri)
                    : Array.Empty<Uri>();
            Arg[] methodArgs = {MasterCallerId, topic, new Arg(publisherUris)};

            // fire and forget!
            // we don't await here because some subscribers may not be reachable anymore
            Task.Run(async() =>
            {
                Task[] tasks = subscribers.Select(tuple => NotifySubscriber(tuple.callerUri, methodArgs)).ToArray();
                await Task.WhenAll(tasks);
            });
        }

        async Task NotifySubscriber(Uri remoteUri, IEnumerable<Arg> methodArgs)
        {
            try
            {
                await Service.MethodCallAsync(remoteUri, MasterUri, "publisherUpdate", methodArgs);
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
                !(args[2] is string topicType) ||
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
                subscribers = new HashSet<(string, Uri)>();
                subscribersByTopic[topic] = subscribers;
            }

            subscribers.Add((callerId, callerUri));

            IEnumerable<Uri> currentPublishers =
                publishersByTopic.TryGetValue(topic, out var publishers)
                    ? publishers.Select(tuple => tuple.callerUri)
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
                !subscribers.Remove((callerId, callerUri)))
            {
                return OkResponse(0);
            }

            if (!subscribers.Any())
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
                !publishers.Remove((callerId, callerUri)))
            {
                return OkResponse(0);
            }
            
            Logger.Log($"-- Publisher: {callerId}@{callerUri} -> {topic}");
            
            if (!publishers.Any())
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
                !(args[2] is string serviceApi) ||
                !(args[3] is string callerApi))
            {
                return ErrorResponse("Failed to parse arguments");
            }

            if (!Uri.TryCreate(serviceApi, UriKind.Absolute, out Uri serviceUri))
            {
                return ErrorResponse("Service api is not an uri");
            }

            Logger.Log($"++ Service: {service}");

            serviceProviders[service] = serviceUri;

            return OkResponse(0);
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
                return OkResponse(0);
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
                .FirstOrDefault(tuple => tuple.callerId == node)
                .callerUri;
            
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
                pair => new Arg[] {pair.Key, new Arg(pair.Value.Select(tuple => tuple.callerUri))});
            var subscribers = subscribersByTopic.Select(
                pair => new Arg[] {pair.Key, new Arg(pair.Value.Select(tuple => tuple.callerUri))});
            var providers = serviceProviders.Select(
                pair => new Arg[] {pair.Key, new Arg(Yield(pair.Value))});

            return OkResponse(
                new[] {new Arg(publishers), new Arg(subscribers), new Arg(providers)}
            );
        }

        Arg[] DeleteParam(object[] args)
        {
            if (args.Length != 2 ||
                !(args[1] is string key))
            {
                return ErrorResponse("Failed to parse arguments");
            }

            parameters.Remove(key);
            return OkResponse(0);
        }

        Arg[] SetParam(object[] args)
        {
            Arg arg;

            if (args.Length != 3 ||
                !(args[1] is string key) ||
                (arg = Arg.Create(args[2])) is null)
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
            return OkResponse(0);
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

            arg = new Arg(candidates.Select(pair => (pair.Key, pair.Value)));
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
