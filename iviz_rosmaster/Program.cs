using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.Msgs.Rosapi;
using Iviz.Roslib;
using Iviz.XmlRpc;
using HttpListenerContext = Iviz.XmlRpc.HttpListenerContext;

namespace Iviz.RosMaster
{
    public class Server : IDisposable
    {
        static async Task Main(string[] args)
        {
            Uri masterUri = RosClient.TryGetCallerUri(DefaultPort);
            using Server server = new Server(masterUri);
            Logger.Log("** Welcome to iviz_rosmaster!");
            server.AddKey("/rosdistro", "noetic");
            server.AddKey("/rosversion", "1.15.8");
            await server.Start();
        }

        const int DefaultPort = 11311;

        readonly XmlRpc.HttpListener listener;

        readonly Dictionary<string, Func<object[], Arg[]>> methods;

        readonly Dictionary<string, Func<object[], Task>> lateCallbacks;

        public Uri MasterUri { get; }

        readonly Dictionary<string, HashSet<(string callerId, Uri callerUri)>> publishersByTopic =
            new Dictionary<string, HashSet<(string, Uri)>>();

        readonly Dictionary<string, HashSet<(string callerId, Uri callerUri)>> subscribersByTopic =
            new Dictionary<string, HashSet<(string, Uri)>>();

        readonly Dictionary<string, Uri> serviceProviders = new Dictionary<string, Uri>();

        readonly Dictionary<string, string> topicTypes = new Dictionary<string, string>();

        readonly Dictionary<string, Arg> parameters = new Dictionary<string, Arg>();

        static class StatusCode
        {
            public const int Error = -1;
            public const int Failure = 0;
            public const int Success = 1;
        }

        public Server(Uri masterUri)
        {
            MasterUri = masterUri;
            listener = new XmlRpc.HttpListener(masterUri);

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
            Logger.Log("** Starting at " + MasterUri);
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
        
        Arg[] GetUri(object[] _)
        {
            return new Arg[] {StatusCode.Success, "ok", MasterUri};
        }

        Arg[] RegisterPublisher(object[] args)
        {
            if (args.Length != 4 ||
                !(args[0] is string callerId) ||
                !(args[1] is string topic) ||
                !(args[2] is string topicType) ||
                !(args[3] is string callerApi))
            {
                return new Arg[] {StatusCode.Error, "Failed to parse arguments", 0};
            }

            if (!Uri.TryCreate(callerApi, UriKind.Absolute, out Uri callerUri))
            {
                return new Arg[] {StatusCode.Error, "Caller api is not an uri", 0};
            }

            if (!publishersByTopic.TryGetValue(topic, out var publishers))
            {
                publishers = new HashSet<(string, Uri)>();
                publishersByTopic[topic] = publishers;
                topicTypes[topic] = topicType;

                Logger.Log($"++ Topic: {topic} [{topicType}]");
            }

            publishers.Add((callerId, callerUri));
            Logger.Log($"++ Publisher: {callerId}@{callerUri} -> {topic}");

            IEnumerable<Uri> currentSubscribers =
                subscribersByTopic.TryGetValue(topic, out var subscribers)
                    ? subscribers.Select(tuple => tuple.callerUri)
                    : ArraySegment<Uri>.Empty;

            return new Arg[] {StatusCode.Success, "ok", new Arg(currentSubscribers)};
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
            Arg[] methodArgs = {new Arg(publisherUris)};

            IEnumerable<Task> tasks = subscribers.Select(tuple => NotifySubscriber(tuple.callerUri, methodArgs));

            await Task.WhenAll(tasks);
        }

        async Task NotifySubscriber(Uri remoteUri, Arg[] methodArgs)
        {
            try
            {
                await Service.MethodCallAsync(remoteUri, MasterUri, "publisherUpdate", methodArgs);
            }
            catch (Exception e)
            {
                Logger.LogError(e);
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
                return new Arg[] {StatusCode.Error, "Failed to parse arguments", 0};
            }

            if (!Uri.TryCreate(callerApi, UriKind.Absolute, out Uri callerUri))
            {
                return new Arg[] {StatusCode.Error, "Caller api is not an uri", 0};
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
                    : ArraySegment<Uri>.Empty;

            return new Arg[] {StatusCode.Success, "ok", new Arg(currentPublishers)};
        }

        Arg[] UnregisterSubscriber(object[] args)
        {
            if (args.Length != 3 ||
                !(args[0] is string callerId) ||
                !(args[1] is string topic) ||
                !(args[2] is string callerApi))
            {
                return new Arg[] {StatusCode.Error, "Failed to parse arguments", 0};
            }

            if (!Uri.TryCreate(callerApi, UriKind.Absolute, out Uri callerUri))
            {
                return new Arg[] {StatusCode.Error, "Caller api is not an uri", 0};
            }

            if (!subscribersByTopic.TryGetValue(topic, out var subscribers) || 
                !subscribers.Remove((callerId, callerUri)))
            {
                return new Arg[] {StatusCode.Success, "ok", 0};
            }

            if (!subscribers.Any())
            {
                subscribersByTopic.Remove(topic);
            }
            
            return new Arg[] {StatusCode.Success, "ok", 1};
        }
        
        Arg[] UnregisterPublisher(object[] args)
        {
            if (args.Length != 3 ||
                !(args[0] is string callerId) ||
                !(args[1] is string topic) ||
                !(args[2] is string callerApi))
            {
                return new Arg[] {StatusCode.Error, "Failed to parse arguments", 0};
            }

            if (!Uri.TryCreate(callerApi, UriKind.Absolute, out Uri callerUri))
            {
                return new Arg[] {StatusCode.Error, "Caller api is not an uri", 0};
            }

            if (!publishersByTopic.TryGetValue(topic, out var publishers) || 
                !publishers.Remove((callerId, callerUri)))
            {
                return new Arg[] {StatusCode.Success, "ok", 0};
            }

            if (!publishers.Any())
            {
                publishersByTopic.Remove(topic);
                topicTypes.Remove(topic);
            }
            
            return new Arg[] {StatusCode.Success, "ok", 1};
        }

        Arg[] RegisterService(object[] args)
        {
            if (args.Length != 4 ||
                !(args[1] is string service) ||
                !(args[2] is string serviceApi) ||
                !(args[3] is string callerApi))
            {
                return new Arg[] {StatusCode.Error, "Failed to parse arguments", 0};
            }

            if (!Uri.TryCreate(serviceApi, UriKind.Absolute, out Uri serviceUri))
            {
                return new Arg[] {StatusCode.Error, "Service api is not an uri", 0};
            }

            Logger.Log($"++ Service: {service}");

            serviceProviders[service] = serviceUri;

            return new Arg[] {StatusCode.Success, "ok", 0};
        }

        Arg[] UnregisterService(object[] args)
        {
            if (args.Length != 3 ||
                !(args[1] is string service) ||
                !(args[2] is string serviceApi))
            {
                return new Arg[] {StatusCode.Error, "Failed to parse arguments", 0};
            }

            if (!Uri.TryCreate(serviceApi, UriKind.Absolute, out Uri serviceUri))
            {
                return new Arg[] {StatusCode.Error, "Service api is not an uri", 0};
            }

            if (!serviceProviders.TryGetValue(service, out Uri currentServiceUri) || serviceUri != currentServiceUri)
            {
                return new Arg[] {StatusCode.Success, "ok", 0};
            }

            serviceProviders.Remove(service);
            return new Arg[] {StatusCode.Success, "ok", 1};
        }

        Arg[] LookupNode(object[] args)
        {
            if (args.Length != 2 ||
                !(args[1] is string node))
            {
                return new Arg[] {StatusCode.Error, "Failed to parse arguments", 0};
            }

            var publishersLookup = publishersByTopic.SelectMany(pair => pair.Value);
            var subscribersLookup = subscribersByTopic.SelectMany(pair => pair.Value);

            Uri uri = publishersLookup
                .Concat(subscribersLookup)
                .FirstOrDefault(tuple => tuple.callerId == node)
                .callerUri;
            
            return uri is null
                ? new Arg[] {StatusCode.Error, $"No node with id '{node}'", 0}
                : new Arg[] {StatusCode.Success, "ok", uri};
        }

        Arg[] LookupService(object[] args)
        {
            if (args.Length != 2 ||
                !(args[1] is string service))
            {
                return new Arg[] {StatusCode.Error, "Failed to parse arguments", 0};
            }

            return serviceProviders.TryGetValue(service, out Uri providerUri)
                ? new Arg[] {StatusCode.Success, "ok", providerUri}
                : new Arg[] {StatusCode.Error, $"No service with name '{service}'", 0};
        }

        Arg[] GetPublishedTopics(object[] _)
        {
            var topics = topicTypes.Select(pair => (pair.Key, pair.Value));

            return new Arg[] {StatusCode.Success, "ok", new Arg(topics)};
        }

        Arg[] GetTopicTypes(object[] _)
        {
            var topics = topicTypes.Select(pair => (pair.Key, pair.Value));

            return new Arg[] {StatusCode.Success, "ok", new Arg(topics)};
        }

        Arg[] GetSystemState(object[] _)
        {
            var publishers = publishersByTopic.Select(
                pair => new Arg[] {pair.Key, new Arg(pair.Value.Select(tuple => tuple.callerUri))});
            var subscribers = subscribersByTopic.Select(
                pair => new Arg[] {pair.Key, new Arg(pair.Value.Select(tuple => tuple.callerUri))});
            var providers = serviceProviders.Select(
                pair => new Arg[] {pair.Key, new Arg(Yield(pair.Value))});

            return new Arg[]
            {
                StatusCode.Success,
                "ok",
                new[] {new Arg(publishers), new Arg(subscribers), new Arg(providers)}
            };
        }

        Arg[] DeleteParam(object[] args)
        {
            if (args.Length != 2 ||
                !(args[1] is string key))
            {
                return new Arg[] {StatusCode.Error, "Failed to parse arguments", 0};
            }

            parameters.Remove(key);
            return new Arg[] {StatusCode.Success, "ok", 0};
        }

        Arg[] SetParam(object[] args)
        {
            Arg arg;

            if (args.Length != 3 ||
                !(args[1] is string key) ||
                (arg = Arg.Create(args[2])) is null)
            {
                return new Arg[] {StatusCode.Error, "Failed to parse arguments", 0};
            }

            if (key.Length == 0)
            {
                return new Arg[] {StatusCode.Error, "Empty key", 0};
            }

            if (key[0] != '/')
            {
                key = "/" + key;
            }

            parameters[key] = arg;
            return new Arg[] {StatusCode.Success, "ok", 0};
        }

        Arg[] GetParam(object[] args)
        {
            if (args.Length != 2 ||
                !(args[1] is string key))
            {
                return new Arg[] {StatusCode.Error, "Failed to parse arguments", 0};
            }

            if (key.Length == 0)
            {
                return new Arg[] {StatusCode.Error, "Empty key", 0};
            }

            if (key[0] != '/')
            {
                key = "/" + key;
            }

            if (parameters.TryGetValue(key, out Arg arg))
            {
                return new Arg[] {StatusCode.Success, "ok", arg};
            }

            string keyAsNamespace = key;
            if (keyAsNamespace[^1] != '/')
            {
                keyAsNamespace += "/";
            }

            var candidates = parameters.Where(pair => pair.Key.StartsWith(keyAsNamespace)).ToArray();
            if (candidates.Length == 0)
            {
                return new Arg[] {StatusCode.Error, $"Parameter '{key}' is not set", 0};
            }

            arg = new Arg(candidates.Select(pair => (pair.Key, pair.Value)));
            return new Arg[] {StatusCode.Success, "ok", arg};
        }

        Arg[] GetParamNames(object[] args)
        {
            return new Arg[] {StatusCode.Success, "ok", new Arg(parameters.Keys)};
        }

        Arg[] HasParam(object[] args)
        {
            if (args.Length != 2 ||
                !(args[1] is string key))
            {
                return new Arg[] {StatusCode.Error, "Failed to parse arguments", 0};
            }

            return new Arg[] {StatusCode.Success, "ok", parameters.ContainsKey(key)};
        }

        Arg[] SubscribeParam(object[] _)
        {
            return new Arg[] {StatusCode.Error, "Not implemented yet", 0};
        }

        Arg[] UnsubscribeParam(object[] _)
        {
            return new Arg[] {StatusCode.Error, "Not implemented yet", 0};
        }

        Arg[] SearchParam(object[] _)
        {
            return new Arg[] {StatusCode.Error, "Not implemented yet", 0};
        }
        
        static IEnumerable<T> Yield<T>(T element)
        {
            yield return element;
        }
    }
}