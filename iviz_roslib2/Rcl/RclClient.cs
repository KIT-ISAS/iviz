using System;
using System.Runtime.InteropServices;
using Iviz.Roslib;
using Iviz.Tools;

namespace Iviz.Roslib2.Rcl;

internal sealed class RclClient : IDisposable
{
    static bool loggingInitialized;
    static Rcl.LoggingHandler? loggingHandler;

    readonly IntPtr contextHandle;
    readonly IntPtr nodeHandle;
    bool disposed;

    public string Name { get; }
    public string Namespace { get; }
    public string FullName { get; }

    public static Rcl.LoggingHandler? LoggingHandler
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

    public RclClient(string name, string @namespace = "")
    {
        Name = name;
        Namespace = @namespace;

        contextHandle = Rcl.CreateContext();
        Check(Rcl.Init(contextHandle));

        if (!loggingInitialized)
        {
            Rcl.InitLogging();
            SetLoggingLevel(RclLogSeverity.Info);
            LoggingHandler = ConsoleLoggingHandler;
            loggingInitialized = true;
        }

        //Console.WriteLine(Rcl.SetDdsProfilePath("/Users/akzeac/profile.xml"));
        Check(Rcl.CreateNode(contextHandle, out nodeHandle, name, @namespace));

        FullName = Rcl.ToString(Rcl.GetFullyQualifiedNodeName(nodeHandle));
    }

    public RclWaitSet CreateWaitSet(int maxSubscriptions, int maxGuardConditions)
    {
        return new RclWaitSet(contextHandle, maxSubscriptions, maxGuardConditions);
    }

    public RclGuardCondition CreateGuardCondition()
    {
        return new RclGuardCondition(contextHandle);
    }

    public RclSubscriber Subscribe(string topic, string type, in QosProfile profile)
    {
        return new RclSubscriber(contextHandle, nodeHandle, topic, type, profile);
    }

    public RclPublisher Advertise(string topic, string type)
    {
        return new RclPublisher(contextHandle, nodeHandle, topic, type);
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

        var nodeNames = Rcl.CreateSpan<IntPtr>(nodeNamesHandle, numNodeNames);
        var nodeNamespaces = Rcl.CreateSpan<IntPtr>(nodeNamespacesHandle, numNodeNames);

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
        var topicNamesSpan = Rcl.CreateSpan<IntPtr>(topicNamesHandle, numTopics);
        var topicTypesSpan = Rcl.CreateSpan<IntPtr>(topicTypesHandle, numTopics);

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
        var serviceNamesSpan = Rcl.CreateSpan<IntPtr>(serviceNamesHandle, numServices);
        var serviceTypesSpan = Rcl.CreateSpan<IntPtr>(serviceTypesHandle, numServices);

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

        var nodeNames = Rcl.CreateSpan<IntPtr>(nodeNamesHandle, numNodes);
        var nodeNamespaces = Rcl.CreateSpan<IntPtr>(nodeNamespacesHandle, numNodes);
        var topicTypes = Rcl.CreateSpan<IntPtr>(topicTypesHandle, numNodes);
        var guids = Rcl.CreateSpan<Guid>(gidHandle, numNodes);
        var profiles = Rcl.CreateSpan<QosProfile>(profilesHandle, numNodes);

        var nodes = new EndpointInfo[numNodes];
        for (int i = 0; i < numNodes; i++)
        {
            nodes[i] = new EndpointInfo(
                Rcl.ToString(nodeNames[i]),
                Rcl.ToString(nodeNamespaces[i]),
                Rcl.ToString(topicTypes[i]),
                guids[i],
                profiles[i]);
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

        var nodeNames = Rcl.CreateSpan<IntPtr>(nodeNamesHandle, numNodes);
        var nodeNamespaces = Rcl.CreateSpan<IntPtr>(nodeNamespacesHandle, numNodes);
        var topicTypes = Rcl.CreateSpan<IntPtr>(topicTypesHandle, numNodes);
        var guids = Rcl.CreateSpan<Guid>(gidHandle, numNodes);
        var profiles = Rcl.CreateSpan<QosProfile>(profilesHandle, numNodes);

        var nodes = new EndpointInfo[numNodes];
        for (int i = 0; i < numNodes; i++)
        {
            nodes[i] = new EndpointInfo(
                Rcl.ToString(nodeNames[i]),
                Rcl.ToString(nodeNamespaces[i]),
                Rcl.ToString(topicTypes[i]),
                guids[i],
                profiles[i]);
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

    [MonoPInvokeCallback(typeof(Rcl.LoggingHandler))]
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