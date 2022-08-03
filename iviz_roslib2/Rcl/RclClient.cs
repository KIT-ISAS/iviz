using Iviz.Roslib;
using Iviz.Tools;

namespace Iviz.Roslib2.Rcl;

internal sealed class RclClient : IDisposable
{
    static bool loggingInitialized;
    static LoggingHandler? loggingHandler;

    readonly IntPtr contextHandle;
    readonly IntPtr nodeHandle;
    bool disposed;
    
    public string FullName { get; }

    public static LoggingHandler? LoggingHandler
    {
        set
        {
            loggingHandler = value;
            Rcl.SetLoggingHandler(loggingHandler);
        }
    }

    public static void SetLoggingLevel(RclLogSeverity severity)
    {
        Rcl.SetLoggingLevel((int)severity);
    }

    public static void SetRclWrapperType(RclWrapperType wrapperType)
    {
        Rcl.SetRclWrapperType(wrapperType);
    }

    public static bool SetDdsProfilePath(string path)
    {
        return Rcl.SetDdsProfilePath(path);
    }

    public RclClient(string name, string @namespace = "")
    {
        contextHandle = Rcl.CreateContext();
        Check(Rcl.Init(contextHandle));

        if (!loggingInitialized)
        {
            Rcl.InitLogging();
            SetLoggingLevel(RclLogSeverity.Info);
            LoggingHandler = ConsoleLoggingHandler;
            loggingInitialized = true;
        }

        Check(Rcl.CreateNode(contextHandle, out nodeHandle, name, @namespace));

        FullName = Rcl.ToString(Rcl.GetFullyQualifiedNodeName(nodeHandle));
    }

    public RclWaitSet CreateWaitSet(int maxSubscriptions, int maxGuardConditions, int maxClients, int maxServers)
    {
        return new RclWaitSet(contextHandle, maxSubscriptions, maxGuardConditions, maxClients, maxServers);
    }

    public RclGuardCondition CreateGuardCondition()
    {
        return new RclGuardCondition(contextHandle);
    }

    public IntPtr GetGraphGuardCondition()
    {
        return Rcl.GetGraphGuardCondition(nodeHandle);
    }

    public RclSubscriber CreateSubscriber(string topic, string type, QosProfile profile)
    {
        return new RclSubscriber(contextHandle, nodeHandle, topic, type, profile);
    }

    public RclPublisher CreatePublisher(string topic, string type, QosProfile profile)
    {
        return new RclPublisher(contextHandle, nodeHandle, topic, type, profile);
    }

    public RclServiceClient CreateServerClient(string topic, string type)
    {
        return new RclServiceClient(contextHandle, nodeHandle, topic, type);
    }
    
    public RclServiceServer CreateServiceServer(string topic, string type)
    {
        return new RclServiceServer(contextHandle, nodeHandle, topic, type);
    }
    
    public NodeName[] GetNodeNames()
    {
        Check(Rcl.GetNodeNames(contextHandle, nodeHandle,
            out var nodeNamesHandle, out int numNodeNames,
            out var nodeNamespacesHandle, out int numNodeNamespaces));

        if (numNodeNames != numNodeNamespaces)
        {
            throw new RosRclException($"Sizes in {nameof(GetNodeNames)} do not match!");
        }

        if (numNodeNames == 0)
        {
            return Array.Empty<NodeName>();
        }

        var nodeNames = Rcl.CreateIntPtrSpan(nodeNamesHandle, numNodeNames);
        var nodeNamespaces = Rcl.CreateIntPtrSpan(nodeNamespacesHandle, numNodeNames);

        var result = new NodeName[numNodeNames];
        for (int i = 0; i < numNodeNames; i++)
        {
            result[i] = new NodeName(
                Rcl.ToString(nodeNames[i]),
                Rcl.ToString(nodeNamespaces[i])
            );
        }

        return result;
    }

    public TopicNameType[] GetTopicNamesAndTypes()
    {
        Check(Rcl.GetTopicNamesAndTypes(contextHandle, nodeHandle,
            out var topicNamesHandle, out var topicTypesHandle, out int numTopics));

        if (numTopics == 0)
        {
            return Array.Empty<TopicNameType>();
        }

        var topics = new TopicNameType[numTopics];
        var topicNamesSpan = Rcl.CreateIntPtrSpan(topicNamesHandle, numTopics);
        var topicTypesSpan = Rcl.CreateIntPtrSpan(topicTypesHandle, numTopics);

        for (int i = 0; i < numTopics; i++)
        {
            string topic = Rcl.ToString(topicNamesSpan[i]);
            string type = Rcl.ToString(topicTypesSpan[i]).Replace("/msg", "");
            topics[i] = new TopicNameType(topic, type);
        }

        return topics;
    }

    public TopicNameType[] GetServiceNamesAndTypesByNode(string nodeName, string nodeNamespace)
    {
        Check(Rcl.GetServiceNamesAndTypesByNode(contextHandle, nodeHandle,
            nodeName, nodeNamespace, out var serviceNamesHandle, out var serviceTypesHandle, out int numServices));

        if (numServices == 0)
        {
            return Array.Empty<TopicNameType>();
        }

        var services = new TopicNameType[numServices];
        var serviceNamesSpan = Rcl.CreateIntPtrSpan(serviceNamesHandle, numServices);
        var serviceTypesSpan = Rcl.CreateIntPtrSpan(serviceTypesHandle, numServices);

        for (int i = 0; i < numServices; i++)
        {
            string topic = Rcl.ToString(serviceNamesSpan[i]);
            string type = Rcl.ToString(serviceTypesSpan[i]).Replace("/msg", "");
            services[i] = new TopicNameType(topic, type);
        }

        return services;
    }

    public EndpointInfo[] GetSubscriberInfo(string topic)
    {
        Check(Rcl.GetSubscribersInfoByTopic(contextHandle, nodeHandle, topic,
            out var nodeNamesHandle, out var nodeNamespacesHandle,
            out var topicTypesHandle, out var gidHandle, out var profilesHandle, out int numNodes));

        if (numNodes == 0)
        {
            return Array.Empty<EndpointInfo>();
        }

        var nodeNames = Rcl.CreateIntPtrSpan(nodeNamesHandle, numNodes);
        var nodeNamespaces = Rcl.CreateIntPtrSpan(nodeNamespacesHandle, numNodes);
        var topicTypes = Rcl.CreateIntPtrSpan(topicTypesHandle, numNodes);
        var guids = Rcl.CreateSpan<Guid>(gidHandle, numNodes);
        var profiles = Rcl.CreateSpan<RmwQosProfile>(profilesHandle, numNodes);

        var nodes = new EndpointInfo[numNodes];
        for (int i = 0; i < numNodes; i++)
        {
            nodes[i] = new EndpointInfo(
                Rcl.ToString(nodeNames[i]),
                Rcl.ToString(nodeNamespaces[i]),
                Rcl.ToString(topicTypes[i]),
                guids[i],
                new QosProfile(profiles[i]));
        }

        return nodes;
    }

    public int CountPublishers(string topic)
    {
        Check(Rcl.CountPublishers(nodeHandle, topic, out int count));
        return count;
    }

    public int CountSubscribers(string topic)
    {
        Check(Rcl.CountSubscribers(nodeHandle, topic, out int count));
        return count;
    }

    public EndpointInfo[] GetPublisherInfo(string topic)
    {
        Check(Rcl.GetPublishersInfoByTopic(contextHandle, nodeHandle, topic,
            out var nodeNamesHandle, out var nodeNamespacesHandle,
            out var topicTypesHandle, out var gidHandle, out var profilesHandle, out int numNodes));

        if (numNodes == 0)
        {
            return Array.Empty<EndpointInfo>();
        }

        var nodeNames = Rcl.CreateIntPtrSpan(nodeNamesHandle, numNodes);
        var nodeNamespaces = Rcl.CreateIntPtrSpan(nodeNamespacesHandle, numNodes);
        var topicTypes = Rcl.CreateIntPtrSpan(topicTypesHandle, numNodes);
        var guids = Rcl.CreateSpan<Guid>(gidHandle, numNodes);
        var profiles = Rcl.CreateSpan<RmwQosProfile>(profilesHandle, numNodes);

        var nodes = new EndpointInfo[numNodes];
        for (int i = 0; i < numNodes; i++)
        {
            nodes[i] = new EndpointInfo(
                Rcl.ToString(nodeNames[i]),
                Rcl.ToString(nodeNamespaces[i]),
                Rcl.ToString(topicTypes[i]),
                guids[i],
                new QosProfile(profiles[i]));
        }

        return nodes;
    }

    public void Dispose()
    {
        if (disposed) return;
        disposed = true;
        GC.SuppressFinalize(this);
        Rcl.DestroyNode(nodeHandle);
        Rcl.Shutdown(contextHandle);
        Rcl.DestroyContext(contextHandle);
    }

    [MonoPInvokeCallback(typeof(LoggingHandler))]
    static void ConsoleLoggingHandler(int severity, IntPtr name, long timestamp, IntPtr message)
    {
        switch (severity)
        {
            case <= (int)RclLogSeverity.Debug:
                if (Logger.LogDebugCallback != null)
                {
                    Logger.LogDebugFormat("[{0}] {1}", Rcl.ToString(name), Rcl.ToString(message));
                }

                break;
            case <= (int)RclLogSeverity.Warn:
                if (Logger.LogCallback != null)
                {
                    Logger.LogFormat("[{0}] {1}", Rcl.ToString(name), Rcl.ToString(message));
                }

                break;
            case <= (int)RclLogSeverity.Fatal:
                if (Logger.LogErrorCallback != null)
                {
                    Logger.LogErrorFormat("[{0}] {1}", Rcl.ToString(name), Rcl.ToString(message));
                }

                break;
        }
    }

    void Check(int result) => Rcl.Check(contextHandle, result);

    ~RclClient() => Dispose();
}