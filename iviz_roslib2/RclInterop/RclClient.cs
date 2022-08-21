using Iviz.Roslib;
using Iviz.Roslib2.RclInterop.Wrappers;
using Iviz.Tools;

namespace Iviz.Roslib2.RclInterop;

internal sealed class RclClient : IDisposable
{
    static bool loggingInitialized;

    readonly IntPtr contextHandle;
    readonly IntPtr nodeHandle;
    bool disposed;

    public int DomainId { get; }
    public string FullName { get; }

    public static void SetLoggingLevel(RclLogSeverity severity)
    {
        Rcl.Impl.SetLoggingLevel((int)severity);
    }

    public static void SetRclWrapper(IRclWrapper wrapper)
    {
        Rcl.SetRclWrapper(wrapper);
    }
    
    public RclClient(string name, string @namespace, int domainId)
    {
        contextHandle = Rcl.Impl.CreateContext();
        DomainId = domainId;
        Check(Rcl.Impl.Init(contextHandle, domainId));

        if (!loggingInitialized)
        {
            Rcl.Impl.InitLogging();
            SetLoggingLevel(RclLogSeverity.Info);
            Rcl.SetLoggingHandler();
            Rcl.SetMessageCallbacks(); 
            loggingInitialized = true;
        }

        Check(Rcl.Impl.CreateNode(contextHandle, out nodeHandle, name, @namespace));

        FullName = Rcl.ToString(Rcl.Impl.GetFullyQualifiedNodeName(nodeHandle));
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
        return Rcl.Impl.GetGraphGuardCondition(nodeHandle);
    }

    public RclSubscriber CreateSubscriber(string topic, string type, QosProfile profile)
    {
        return new RclSubscriber(contextHandle, nodeHandle, topic, type, profile);
    }

    public RclPublisher CreatePublisher(string topic, string type, QosProfile profile)
    {
        return new RclPublisher(contextHandle, nodeHandle, topic, type, profile);
    }

    public RclServiceClient CreateServerClient(string topic, string type, QosProfile profile)
    {
        return new RclServiceClient(contextHandle, nodeHandle, topic, type, profile);
    }

    public RclServiceServer CreateServiceServer(string topic, string type, QosProfile profile)
    {
        return new RclServiceServer(contextHandle, nodeHandle, topic, type, profile);
    }

    public NodeName[] GetNodeNames()
    {
        Check(Rcl.Impl.GetNodeNames(contextHandle, nodeHandle,
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
        Check(Rcl.Impl.GetTopicNamesAndTypes(contextHandle, nodeHandle,
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

    public TopicNameType[] GetServiceNamesAndTypes()
    {
        Check(Rcl.Impl.GetServiceNamesAndTypes(contextHandle, nodeHandle,
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
            string type = Rcl.ToString(topicTypesSpan[i]).Replace("/srv", "");
            topics[i] = new TopicNameType(topic, type);
        }

        return topics;
    }

    public TopicNameType[] GetServiceNamesAndTypesByNode(string nodeName, string nodeNamespace)
    {
        Check(Rcl.Impl.GetServiceNamesAndTypesByNode(contextHandle, nodeHandle,
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
            string type = Rcl.ToString(serviceTypesSpan[i]).Replace("/srv", "");
            services[i] = new TopicNameType(topic, type);
        }

        return services;
    }

    public EndpointInfo[] GetSubscriberInfo(string topic)
    {
        Check(Rcl.Impl.GetSubscribersInfoByTopic(contextHandle, nodeHandle, topic,
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
        Check(Rcl.Impl.CountPublishers(nodeHandle, topic, out int count));
        return count;
    }

    public int CountSubscribers(string topic)
    {
        Check(Rcl.Impl.CountSubscribers(nodeHandle, topic, out int count));
        return count;
    }

    public EndpointInfo[] GetPublisherInfo(string topic)
    {
        Check(Rcl.Impl.GetPublishersInfoByTopic(contextHandle, nodeHandle, topic,
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
        Rcl.Impl.DestroyNode(nodeHandle);
        Rcl.Impl.Shutdown(contextHandle);
        Rcl.Impl.DestroyContext(contextHandle);
    }


    void Check(int result) => Rcl.Check(contextHandle, result);

    ~RclClient() => Dispose();
}