using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs.RosgraphMsgs;
using Iviz.Roslib;
using Iviz.XmlRpc;
using Iviz.Tools;
using Nito.AsyncEx;
using HttpListenerContext = Iviz.XmlRpc.HttpListenerContext;
using Logger = Iviz.Tools.Logger;

namespace Iviz.RosMaster;

public sealed class RosMasterServer : IDisposable
{
    public const int DefaultPort = 11311;

    readonly AsyncLock rosLock = new();
    readonly Dictionary<string, Func<XmlRpcValue[], XmlRpcArg>> methods;
    readonly Dictionary<string, Func<XmlRpcValue[], CancellationToken, ValueTask>> lateCallbacks;
    readonly Dictionary<string, Dictionary<string, Uri>> publishersByTopic = new();
    readonly Dictionary<string, Dictionary<string, Uri>> subscribersByTopic = new();
    readonly Dictionary<string, (string Id, Uri Uri)> serviceProviders = new();
    readonly Dictionary<string, string> topicTypes = new();
    readonly Dictionary<string, XmlRpcArg> parameters = new();
    readonly Dictionary<string, HashSet<Uri>> parameterSubscribers = new();
    readonly CancellationTokenSource runningTs = new();

    readonly Task? backgroundTask;

    bool disposed;

    CancellationToken Token => runningTs.Token;
    public Uri MasterUri { get; }
    public string MasterCallerId { get; }

    static class StatusCode
    {
        public const int Error = -1;
        public const int Failure = 0;
        public const int Success = 1;
    }

    static readonly XmlRpcArg DefaultOkResponse = OkResponse(0);
    static readonly XmlRpcArg FailResponse = new(StatusCode.Failure, "Request failed", 0);

    static XmlRpcArg OkResponse(XmlRpcArg arg) => new(StatusCode.Success, "", arg);
    static XmlRpcArg ErrorResponse(string msg) => new(StatusCode.Error, msg, 0);

    public RosMasterServer(Uri masterUri, string callerId = "/iviz_master", bool startInBackground = false)
    {
        MasterUri = masterUri;
        MasterCallerId = callerId;

        methods = new Dictionary<string, Func<XmlRpcValue[], XmlRpcArg>>
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

        lateCallbacks = new Dictionary<string, Func<XmlRpcValue[], CancellationToken, ValueTask>>
        {
            ["registerPublisher"] = RegisterPublisherLateCallback,
            ["unregisterPublisher"] = RegisterPublisherLateCallback
        };

        backgroundTask = startInBackground ? StartAsync().AsTask() : null;
    }

    public void Dispose()
    {
        if (disposed)
        {
            return;
        }

        disposed = true;
        runningTs.Cancel();
        backgroundTask?.WaitNoThrow(2000, this);
    }

    public async ValueTask DisposeAsync()
    {
        if (disposed)
        {
            return;
        }

        disposed = true;
        runningTs.Cancel();
        if (backgroundTask != null)
        {
            await backgroundTask.AwaitNoThrow(2000, this, default);
        }
    }

    public override string ToString()
    {
        return $"[RosMaster Uri={MasterUri}]";
    }

    public void AddKey(string key, XmlRpcArg value)
    {
        parameters[key] = value;
    }

    public static async ValueTask<RosMasterServer> CreateAsync(Uri masterUri, string callerId = "/iviz_master")
    {
        var masterServer = new RosMasterServer(masterUri, callerId);
        await masterServer.StartAsync();
        return masterServer;
    }

    public async ValueTask StartAsync(TaskCompletionSource? initCompletedSignal = null)
    {
        if (backgroundTask != null)
        {
            throw new InvalidOperationException(
                "RosMasterServer has already been initialized as a background task");
        }

        var listener = new HttpListener(MasterUri.Port);

        Logger.LogDebugFormat("{0}: Starting!", this);
        AddKey("/run_id", Guid.NewGuid().ToString());

        var startTask = TaskUtils.Run(() => listener.StartAsync(StartContext, true).AwaitNoThrow(this), Token);

        await Task.Delay(100, Token);

        await TaskUtils.Run(() => ManageRosoutAggAsync(initCompletedSignal, Token).AwaitNoThrow(this), Token);

        await listener.DisposeAsync();
        await startTask;

        runningTs.Cancel();
        Logger.LogDebugFormat("{0}: Leaving thread.", this);
    }

    async ValueTask StartContext(HttpListenerContext context, CancellationToken token)
    {
        try
        {
            await XmlRpcService.MethodResponseAsync(context, methods, lateCallbacks, token);
        }
        catch (Exception e)
        {
            switch (e)
            {
                case OperationCanceledException:
                    break; // do nothing
                case SocketException or IOException:
                    Logger.LogDebugFormat("{0}: Error in StartContext: {1}", this, e);
                    break;
                default:
                    Logger.LogErrorFormat("{0}: Error in StartContext: {1}", this, e);
                    break;
            }
        }
    }

    async ValueTask ManageRosoutAggAsync(TaskCompletionSource? signal, CancellationToken token)
    {
        var ownUri = new Uri($"http://{MasterUri.Host}:0");

        Logger.LogDebug("** Starting Rosout routine...");

        try
        {
            await using var client = await RosClient.CreateAsync(MasterUri, "/rosout", ownUri, token: token);
            await using var reader = await client.CreateReaderAsync<Log>("/rosout", token);
            await using var writer = await client.CreateWriterAsync<Log>("/rosout_agg", true, token);

            Logger.LogDebug("** Rosout running!");
            signal?.TrySetResult();

            await foreach (var logMsg in reader.ReadAllAsync(token))
            {
                writer.Write(logMsg);
            }
        }
        catch (RosConnectionException e)
        {
            signal?.TrySetException(e);
            Logger.LogErrorFormat("{0}: Failed to start the rosout_agg node. " +
                                  "Our own uri is not reachable! {1}", this, e);
        }
    }

    static XmlRpcArg GetPid(XmlRpcValue[] _)
    {
        return OkResponse(ConnectionUtils.GetProcessId());
    }

    XmlRpcArg GetUri(XmlRpcValue[] _)
    {
        return OkResponse(MasterUri);
    }

    XmlRpcArg RegisterPublisher(XmlRpcValue[] args)
    {
        using var myLock = rosLock.Lock(Token);
        if (args.Length != 4 ||
            !args[0].TryGetString(out string callerId) ||
            !args[1].TryGetString(out string topic) ||
            !args[2].TryGetString(out string topicType) ||
            !args[3].TryGetString(out string callerApi))
        {
            return ErrorResponse("Failed to parse arguments");
        }

        if (!Uri.TryCreate(callerApi, UriKind.Absolute, out Uri? callerUri))
        {
            return ErrorResponse("Caller api is not an uri");
        }

        if (!publishersByTopic.TryGetValue(topic, out var publishers))
        {
            publishers = new Dictionary<string, Uri>();
            publishersByTopic[topic] = publishers;
            topicTypes[topic] = topicType;

            Logger.LogDebugFormat("++ Topic: {0} [{1}]", topic, topicType);
        }

        publishers[callerId] = callerUri;
        Logger.LogDebugFormat("++ Publisher: {0}@{1} -> {2} [{3}]", callerId, callerUri, topic, topicType);

        var currentSubscribers =
            subscribersByTopic.TryGetValue(topic, out var subscribers)
                ? subscribers.Values.ToArray()
                : Array.Empty<Uri>();

        return OkResponse(new XmlRpcArg(currentSubscribers));
    }

    ValueTask RegisterPublisherLateCallback(XmlRpcValue[] args, CancellationToken token)
    {
        XmlRpcArg[] methodArgs;
        Uri[] subscribersToNotify;

        using (rosLock.Lock(token))
        {
            if (!args[1].TryGetString(out string topic) ||
                !subscribersByTopic.TryGetValue(topic, out var subscribers))
            {
                return default;
            }

            var publisherUris =
                publishersByTopic.TryGetValue(topic, out var publishers)
                    ? publishers.Values.ToArray()
                    : Array.Empty<Uri>();

            if (subscribers.Count == 0 || token.IsCancellationRequested)
            {
                return default;
            }

            methodArgs = new XmlRpcArg[] { MasterCallerId, topic, new(publisherUris) };
            subscribersToNotify = subscribers.Values.ToArray();
        }

        foreach (var uri in subscribersToNotify)
        {
            NotifySubscriber(uri, methodArgs, token);
        }

        return default;
    }

    async void NotifySubscriber(Uri remoteUri, XmlRpcArg[] methodArgs, CancellationToken token)
    {
        try
        {
            await XmlRpcService.MethodCallAsync(remoteUri, MasterUri, "publisherUpdate", methodArgs, token);
        }
        catch (OperationCanceledException)
        {
        }
        catch (Exception e)
        {
            Logger.LogDebugFormat("{0}: {1}", this, e);
        }
    }

    XmlRpcArg RegisterSubscriber(XmlRpcValue[] args)
    {
        using var myLock = rosLock.Lock(Token);

        if (args.Length != 4 ||
            !args[0].TryGetString(out string callerId) ||
            !args[1].TryGetString(out string topic) ||
            !args[3].TryGetString(out string callerApi))
        {
            return ErrorResponse("Failed to parse arguments");
        }

        if (!Uri.TryCreate(callerApi, UriKind.Absolute, out Uri? callerUri))
        {
            return ErrorResponse("Caller api is not an uri");
        }

        if (!subscribersByTopic.TryGetValue(topic, out var subscribers))
        {
            subscribers = new Dictionary<string, Uri>();
            subscribersByTopic[topic] = subscribers;
        }

        subscribers[callerId] = callerUri;

        var currentPublishers =
            publishersByTopic.TryGetValue(topic, out var publishers)
                ? publishers.Values.ToArray()
                : Array.Empty<Uri>();

        return OkResponse(new XmlRpcArg(currentPublishers));
    }

    XmlRpcArg UnregisterSubscriber(XmlRpcValue[] args)
    {
        using var myLock = rosLock.Lock(Token);

        if (args.Length != 3 ||
            !args[0].TryGetString(out string callerId) ||
            !args[1].TryGetString(out string topic) ||
            !args[2].TryGetString(out string callerApi))
        {
            return ErrorResponse("Failed to parse arguments");
        }

        if (!Uri.TryCreate(callerApi, UriKind.Absolute, out Uri? callerUri))
        {
            return ErrorResponse("Caller api is not an uri");
        }

        if (!subscribersByTopic.TryGetValue(topic, out var subscribers) ||
            !(subscribers.TryGetValue(callerId, out Uri? existingUri) && existingUri == callerUri) ||
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

    XmlRpcArg UnregisterPublisher(XmlRpcValue[] args)
    {
        using var myLock = rosLock.Lock(Token);

        if (args.Length != 3 ||
            !args[0].TryGetString(out string callerId) ||
            !args[1].TryGetString(out string topic) ||
            !args[2].TryGetString(out string callerApi))
        {
            return ErrorResponse("Failed to parse arguments");
        }

        if (!Uri.TryCreate(callerApi, UriKind.Absolute, out Uri? callerUri))
        {
            return ErrorResponse("Caller api is not an uri");
        }

        if (!publishersByTopic.TryGetValue(topic, out var publishers) ||
            !(publishers.TryGetValue(callerId, out var tmpUri) && tmpUri == callerUri) ||
            !publishers.Remove(callerId))
        {
            return DefaultOkResponse;
        }

        Logger.LogDebugFormat("-- Publisher: {0}@{1} -> {2}", callerId, callerUri, topic);

        if (publishers.Count == 0)
        {
            publishersByTopic.Remove(topic);
            topicTypes.Remove(topic);

            Logger.LogDebugFormat("-- Topic: {0}", topic);
        }

        return OkResponse(1);
    }

    XmlRpcArg RegisterService(XmlRpcValue[] args)
    {
        using var myLock = rosLock.Lock(Token);

        if (args.Length != 4 ||
            !args[0].TryGetString(out string callerId) ||
            !args[1].TryGetString(out string service) ||
            !args[2].TryGetString(out string serviceApi))
        {
            return ErrorResponse("Failed to parse arguments");
        }

        if (!Uri.TryCreate(serviceApi, UriKind.Absolute, out Uri? serviceUri))
        {
            return ErrorResponse("Service api is not an uri");
        }

        Logger.LogDebugFormat("++ Service: {0}", service);

        serviceProviders[service] = (callerId, serviceUri);

        return DefaultOkResponse;
    }

    XmlRpcArg UnregisterService(XmlRpcValue[] args)
    {
        using var myLock = rosLock.Lock(Token);

        if (args.Length != 3 ||
            !args[1].TryGetString(out string service) ||
            !args[2].TryGetString(out string serviceApi))
        {
            return ErrorResponse("Failed to parse arguments");
        }

        if (!Uri.TryCreate(serviceApi, UriKind.Absolute, out Uri? serviceUri))
        {
            return ErrorResponse("Service api is not an uri");
        }

        if (!serviceProviders.TryGetValue(service, out var currentService) || serviceUri != currentService.Uri)
        {
            return DefaultOkResponse;
        }

        Logger.LogDebugFormat("-- Service: {0}", service);
        serviceProviders.Remove(service);
        return OkResponse(1);
    }

    XmlRpcArg LookupNode(XmlRpcValue[] args)
    {
        using var myLock = rosLock.Lock(Token);

        if (args.Length != 2 ||
            !args[1].TryGetString(out string node))
        {
            return ErrorResponse("Failed to parse arguments");
        }

        var publishersLookup = publishersByTopic.SelectMany(pair => pair.Value);
        var subscribersLookup = subscribersByTopic.SelectMany(pair => pair.Value);

        Uri? uri = publishersLookup
            .Concat(subscribersLookup)
            .FirstOrDefault(tuple => tuple.Key == node)
            .Value;

        // ReSharper disable once ConditionIsAlwaysTrueOrFalse
        return uri == null
            ? ErrorResponse($"No node with id '{node}'")
            : OkResponse(uri);
    }

    XmlRpcArg LookupService(XmlRpcValue[] args)
    {
        using var myLock = rosLock.Lock(Token);

        if (args.Length != 2 ||
            !args[1].TryGetString(out string service))
        {
            return ErrorResponse("Failed to parse arguments");
        }

        return serviceProviders.TryGetValue(service, out var provider)
            ? OkResponse(provider.Uri)
            : ErrorResponse($"No service with name '{service}'");
    }

    XmlRpcArg GetPublishedTopics(XmlRpcValue[] _)
    {
        using var myLock = rosLock.Lock(Token);

        var topics = topicTypes.Select(pair => (pair.Key, pair.Value));

        return OkResponse(new XmlRpcArg(topics));
    }

    XmlRpcArg GetTopicTypes(XmlRpcValue[] _)
    {
        using var myLock = rosLock.Lock(Token);

        var topics = topicTypes.Select(pair => (pair.Key, pair.Value));

        return OkResponse(new XmlRpcArg(topics));
    }

    XmlRpcArg GetSystemState(XmlRpcValue[] _)
    {
        using var myLock = rosLock.Lock(Token);

        var publishers = publishersByTopic.Select(
                pair => ((XmlRpcArg)pair.Key, new XmlRpcArg(pair.Value.Select(tuple => tuple.Key))))
            .ToArray();
        var subscribers = subscribersByTopic.Select(
                pair => ((XmlRpcArg)pair.Key, new XmlRpcArg(pair.Value.Select(tuple => tuple.Key))))
            .ToArray();
        var providers = serviceProviders.Select(
                pair => ((XmlRpcArg)pair.Key, new XmlRpcArg(new[] { pair.Value.Id })))
            .ToArray();

        return OkResponse(((XmlRpcArg)publishers, (XmlRpcArg)subscribers, (XmlRpcArg)providers));
    }

    XmlRpcArg DeleteParam(XmlRpcValue[] args)
    {
        using var myLock = rosLock.Lock(Token);

        if (args.Length != 2 ||
            !args[1].TryGetString(out string key))
        {
            return ErrorResponse("Failed to parse arguments");
        }

        parameters.Remove(key);
        return DefaultOkResponse;
    }

    XmlRpcArg SetParam(XmlRpcValue[] args)
    {
        Uri[] subscribersToNotify;
        XmlRpcArg[] methodArgs;

        using (rosLock.Lock(Token))
        {
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
                    return DefaultOkResponse;
                }
                catch (ParseException e)
                {
                    return ErrorResponse(e.Message);
                }
            }

            var arg = args[2].AsArg();
            if (!arg.IsValid)
            {
                return ErrorResponse($"Parameter [{key}] could not be parsed'");
            }

            parameters[key] = arg;

            if (!parameterSubscribers.TryGetValue(key, out var subscribers)
                || runningTs.IsCancellationRequested)
            {
                return DefaultOkResponse;
            }

            subscribersToNotify = subscribers.ToArray();
            methodArgs = new XmlRpcArg[] { MasterCallerId, key, arg };
        }

        Task.Run(() =>
        {
            foreach (var subscriberUri in subscribersToNotify)
            {
                NotifyParamSubscriber(subscriberUri, methodArgs);
            }
        }, default);

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
                var arg = value.AsArg();
                if (!arg.IsValid)
                {
                    throw new ParseException($"Parameter [{key}] could not be parsed'");
                }

                parameters[key] = arg;

                if (!parameterSubscribers.TryGetValue(key, out var subscribers))
                {
                    continue;
                }

                var methodArgs = new XmlRpcArg[] { MasterCallerId, key, arg };
                var subscribersToNotify = subscribers.ToArray();
                Task.Run(() =>
                {
                    foreach (var subscriberUri in subscribersToNotify)
                    {
                        NotifyParamSubscriber(subscriberUri, methodArgs);
                    }
                }, default);
            }
        }
    }

    async void NotifyParamSubscriber(Uri remoteUri, XmlRpcArg[] methodArgs)
    {
        try
        {
            await XmlRpcService.MethodCallAsync(remoteUri, MasterUri, "paramUpdate", methodArgs, runningTs.Token);
        }
        catch (Exception e)
        {
            Logger.LogFormat("{0}: {1}", this, e);
        }
    }

    XmlRpcArg GetParam(XmlRpcValue[] args)
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
        if (keyAsNamespace[^1] != '/')
        {
            keyAsNamespace += "/";
        }

        var candidates = parameters.Where(pair => pair.Key.StartsWith(keyAsNamespace)).ToArray();
        if (candidates.Length == 0)
        {
            return ErrorResponse($"Parameter [{key}] is not set");
        }

        arg = new XmlRpcArg(candidates.Select(pair => (pair.Key, pair.Value)).ToArray());
        return OkResponse(arg);
    }

    XmlRpcArg GetParamNames(XmlRpcValue[] args)
    {
        using var myLock = rosLock.Lock(Token);

        return OkResponse(new XmlRpcArg(parameters.Keys));
    }

    XmlRpcArg HasParam(XmlRpcValue[] args)
    {
        using var myLock = rosLock.Lock(Token);

        if (args.Length != 2 ||
            !args[1].TryGetString(out string key))
        {
            return ErrorResponse("Failed to parse arguments");
        }

        bool success = parameters.ContainsKey(key);

        return OkResponse(success);
    }

    XmlRpcArg SubscribeParam(XmlRpcValue[] args)
    {
        using var myLock = rosLock.Lock();

        if (args.Length != 3 ||
            !args[1].TryGetString(out string key) ||
            !args[2].TryGetString(out string callerApi))
        {
            return ErrorResponse("Failed to parse arguments");
        }

        if (!Uri.TryCreate(callerApi, UriKind.Absolute, out Uri? callerUri))
        {
            return ErrorResponse("Caller api is not an uri");
        }

        if (!parameterSubscribers.TryGetValue(key, out var subscribers))
        {
            subscribers = new HashSet<Uri>();
            parameterSubscribers[key] = subscribers;
        }

        subscribers.Add(callerUri);

        var response =
            parameters.TryGetValue(key, out XmlRpcArg arg)
                ? arg
                : Array.Empty<(string, XmlRpcArg)>();
        return OkResponse(response);
    }

    XmlRpcArg UnsubscribeParam(XmlRpcValue[] args)
    {
        using var myLock = rosLock.Lock(Token);

        if (args.Length != 3 ||
            !args[1].TryGetString(out string key) ||
            !args[2].TryGetString(out string callerApi))
        {
            return ErrorResponse("Failed to parse arguments");
        }

        if (!Uri.TryCreate(callerApi, UriKind.Absolute, out Uri? callerUri))
        {
            return ErrorResponse("Caller api is not an uri");
        }

        int numUnsubscribed =
            parameterSubscribers.TryGetValue(key, out var subscribers)
            && subscribers.Remove(callerUri)
                ? 1
                : 0;

        return OkResponse(numUnsubscribed);
    }

    XmlRpcArg SearchParam(XmlRpcValue[] args)
    {
        using var myLock = rosLock.Lock(Token);

        if (args.Length != 2 ||
            !args[0].TryGetString(out string callerId) ||
            !args[1].TryGetString(out string key))
        {
            return ErrorResponse("Failed to parse arguments");
        }

        string fullKey = callerId + "/" + key;
        if (parameters.ContainsKey(fullKey))
        {
            return OkResponse(fullKey);
        }

        if (callerId == "/")
        {
            return FailResponse;
        }

        string[] nodes = callerId.Split('/');
        for (int i = nodes.Length - 2; i >= 0; i--)
        {
            string subKey = string.Join("/", nodes, 0, i) + "/" + key;
            if (parameters.ContainsKey(subKey))
            {
                return OkResponse(subKey);
            }
        }

        return FailResponse;
    }

    XmlRpcArg SystemMulticall(XmlRpcValue[] args)
    {
        if (args.Length != 1 ||
            !args[0].TryGetArray(out XmlRpcValue[] calls))
        {
            return ErrorResponse("Failed to parse arguments");
        }

        var responses = new List<XmlRpcArg>(calls.Length);
        foreach (var callObject in calls)
        {
            if (!callObject.TryGetStruct(out (string ElementName, XmlRpcValue Element)[] call))
            {
                return ErrorResponse("Failed to parse arguments");
            }

            string? methodName = null;
            XmlRpcValue[]? arguments = null;
            foreach ((string elementName, XmlRpcValue element) in call)
            {
                switch (elementName)
                {
                    case "methodName":
                    {
                        if (!element.TryGetString(out string elementStr))
                        {
                            return ErrorResponse("Failed to parse methodname tag, not a string");
                        }

                        methodName = elementStr;
                        break;
                    }
                    case "params":
                    {
                        if (!element.TryGetArray(out XmlRpcValue[] elementObjs) ||
                            elementObjs.Length == 0)
                        {
                            return ErrorResponse("Failed to parse params tag, not an array");
                        }

                        arguments = elementObjs;
                        break;
                    }
                    default:
                        return ErrorResponse($"Unexpected element '{elementName}' in struct array");
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

            var response = method(arguments);
            responses.Add(response);

            if (lateCallbacks != null &&
                lateCallbacks.TryGetValue(methodName, out var lateCallback))
            {
                lateCallback(args, default);
            }
        }

        return new[] { (XmlRpcArg)responses.ToArray() };
    }
}