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

    public RclSubscriber Subscribe(string topic, string type)
    {
        return new RclSubscriber(contextHandle, nodeHandle, topic, type);
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
            throw new Exception("Sizes do not match!");
        }

        var nodeNames = new NodeName[numNodeNames];
        for (int i = 0; i < numNodeNames; i++)
        {
            string ns = Rcl.ToString(Rcl.GetArrayValue(nodeNamespacesHandle, i));
            string name = Rcl.ToString(Rcl.GetArrayValue(nodeNamesHandle, i));
            nodeNames[i] = new NodeName(ns, name);
        }

        return nodeNames;
    }

    public TopicNameType[] GetTopicNamesAndTypes()
    {
        Check(Rcl.GetTopicNamesAndTypes(contextHandle, nodeHandle,
            out var topicNamesHandle, out var topicTypesHandle, out int numTopicTypes));

        var topics = new TopicNameType[numTopicTypes];
        for (int i = 0; i < numTopicTypes; i++)
        {
            string topic = Rcl.ToString(Rcl.GetArrayValue(topicNamesHandle, i));
            string type = Rcl.ToString(Rcl.GetArrayValue(topicTypesHandle, i)).Replace("/msg", "");
            topics[i] = new TopicNameType(topic, type);
        }

        return topics;
    }

    public EndpointInfo[] GetSubscriberInfo(string topic)
    {
        Check(Rcl.GetSubscribersInfoByTopic(contextHandle, nodeHandle, topic,
            out var nodeNamesHandle, out var nodeNamespacesHandle,
            out var topicTypesHandle, out var gidHandle, out int numNodes));

        var guids = numNodes != 0
            ? MemoryMarshal.CreateSpan(ref Rcl.GetArrayValue<Guid>(gidHandle, 0), numNodes)
            : Array.Empty<Guid>();
        
        var nodes = new EndpointInfo[numNodes];
        for (int i = 0; i < numNodes; i++)
        {
            string name = Rcl.ToString(Rcl.GetArrayValue(nodeNamesHandle, i));
            string ns = Rcl.ToString(Rcl.GetArrayValue(nodeNamespacesHandle, i));
            string type = Rcl.ToString(Rcl.GetArrayValue(topicTypesHandle, i));

            nodes[i] = new EndpointInfo(guids[i], new NodeName(ns, name), type);
        }

        return nodes;
    }

    public EndpointInfo[] GetPublisherInfo(string topic)
    {
        Check(Rcl.GetPublishersInfoByTopic(contextHandle, nodeHandle, topic,
            out var nodeNamesHandle, out var nodeNamespacesHandle,
            out var topicTypesHandle, out var gidHandle, out int numNodes));

        var guids = numNodes != 0
            ? MemoryMarshal.CreateSpan(ref Rcl.GetArrayValue<Guid>(gidHandle, 0), numNodes)
            : Array.Empty<Guid>();

        var nodes = new EndpointInfo[numNodes];
        for (int i = 0; i < numNodes; i++)
        {
            string name = Rcl.ToString(Rcl.GetArrayValue(nodeNamesHandle, i));
            string ns = Rcl.ToString(Rcl.GetArrayValue(nodeNamespacesHandle, i));
            string type = Rcl.ToString(Rcl.GetArrayValue(topicTypesHandle, i));

            nodes[i] = new EndpointInfo(guids[i], new NodeName(ns, name), type);
        }

        return nodes;
    }

    public static bool IsTypeSupported(string message) => Rcl.IsTypeSupported(message);

    public void Dispose()
    {
        if (disposed) return;
        disposed = true;
        GC.SuppressFinalize(this);
        if (nodeHandle != IntPtr.Zero)
        {
            Rcl.DestroyNode(nodeHandle);
        }

        if (contextHandle != IntPtr.Zero)
        {
            Rcl.Shutdown(contextHandle);
            Rcl.DestroyContext(contextHandle);
        }
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