namespace Iviz.Roslib2.Rcl.Wrappers;

internal interface IRclWrapper
{
    public bool SetDdsProfilePath(string path);
    public IntPtr CreateContext();
    public void DestroyContext(IntPtr context);
    public int Init(IntPtr context);
    public int Shutdown(IntPtr contextHandle);
    public int InitLogging();
    public void SetLoggingLevel(int level);
    public void SetLoggingHandler(LoggingHandler? handler);
    public int CreateNode(IntPtr contextHandle, out IntPtr nodeHandle, string name, string @namespace);
    public int DestroyNode(IntPtr nodeHandle);

    public IntPtr GetFullyQualifiedNodeName(IntPtr nodeHandle);

    public IntPtr CreateGuard(IntPtr contextHandle);
    public int DestroyGuard(IntPtr guardHandle);
    public int TriggerGuard(IntPtr guardHandle);

    public IntPtr CreateWaitSet();

    public int WaitSetInit(IntPtr contextHandle, IntPtr waitSetHandle, int numberOfSubscriptions,
        int numberOfGuardConditions, int numberOfTimers, int numberOfClients, int numberOfServices,
        int numberOfEvents);

    public int Wait(IntPtr waitSetHandle, int timeoutInMs,
        out IntPtr subscriptionHandles,
        out IntPtr guardHandles,
        out IntPtr clientHandles,
        out IntPtr serviceHandles);

    public int WaitClearAndAdd(IntPtr waitSetHandle,
        in IntPtr subscriptionHandles, int numSubscriptionHandles,
        in IntPtr guardHandles, int numGuardHandles,
        in IntPtr clientHandles, int numClientHandles,
        in IntPtr serviceHandles, int numServiceHandles);

    public int DestroyWaitSet(IntPtr waitSetHandle);
    IntPtr GetErrorString(IntPtr contextHandle);

    public int CreateSubscriptionHandle(out IntPtr subscriptionHandle, IntPtr nodeHandle, string topic, string type,
        in RmwQosProfile profile);

    public bool IsMessageTypeSupported(string type);
    public bool IsServiceTypeSupported(string type);
    public int DestroySubscriptionHandle(IntPtr subscriptionHandle, IntPtr nodeHandle);
    public int GetPublisherCount(IntPtr subscriptionHandle, out int count);

    public int TakeSerializedMessage(IntPtr subscriptionHandle, IntPtr serializedMessage, out IntPtr ptr,
        out int length, out Guid gid);

    public int DestroySerializedMessage(IntPtr messageHandle);
    public int CreateSerializedMessage(out IntPtr messageHandle);
    public int EnsureSerializedMessageSize(IntPtr messageHandle, int size, out IntPtr ptr);
    public int CreatePublisherHandle(out IntPtr publisherHandle, IntPtr nodeHandle, string topic, string type);
    public int DestroyPublisherHandle(IntPtr publisherHandle, IntPtr nodeHandle);
    public int GetSubscriptionCount(IntPtr publisherHandle, out int count);
    public int PublishSerializedMessage(IntPtr publisherHandle, IntPtr serializedMessageHandle);

    public int GetNodeNames(IntPtr contextHandle, IntPtr nodeHandle,
        out IntPtr nodeNamesHandle, out int numNodeNames,
        out IntPtr nodeNamespacesHandle, out int numNodeNamespaces);

    public int GetTopicNamesAndTypes(IntPtr contextHandle, IntPtr nodeHandle,
        out IntPtr topicNamesHandle, out IntPtr topicTypesHandle, out int numTopicTypes);

    public int GetServiceNamesAndTypesByNode(IntPtr contextHandle, IntPtr nodeHandle, string nodeName,
        string nodeNamespace, out IntPtr serviceNamesHandle, out IntPtr serviceTypesHandle, out int numNodeNamespaces);

    public int GetPublishersInfoByTopic(IntPtr contextHandle, IntPtr nodeHandle, string topic,
        out IntPtr nodeNamesHandle, out IntPtr nodeNamespacesHandle, out IntPtr topicTypesHandle,
        out IntPtr gidHandle, out IntPtr profilesHandle, out int numNodes);

    public int GetSubscribersInfoByTopic(IntPtr contextHandle, IntPtr nodeHandle, string topic,
        out IntPtr nodeNamesHandle, out IntPtr nodeNamespacesHandle, out IntPtr topicTypesHandle,
        out IntPtr gidHandle, out IntPtr profilesHandle, out int numNodes);

    public int CountPublishers(IntPtr nodeHandle, string topic, out int count);
    public int CountSubscribers(IntPtr nodeHandle, string topic, out int count);
    public IntPtr GetGraphGuardCondition(IntPtr nodeHandle);
    public int CreateClientHandle(out IntPtr serviceClientHandle, IntPtr nodeHandle, string service, string type);
    public int DestroyClientHandle(IntPtr clientHandle, IntPtr nodeHandle);
    public int SendRequest(IntPtr clientHandle, IntPtr serializedMessageHandle, out long sequenceId);

    public int TakeResponse(IntPtr clientHandle, IntPtr serializedMessageHandle, out RmwServiceInfo requestHeader,
        out IntPtr ptr, out int length);

    public int CreateServiceHandle(out IntPtr serviceHandle, IntPtr nodeHandle, string service, string type);
    public int DestroyServiceHandle(IntPtr serviceHandle, IntPtr nodeHandle);
    public int SendResponse(IntPtr serviceHandle, IntPtr serializedMessageHandle, in RmwRequestId requestHeaderHandle);

    public int TakeRequest(IntPtr serviceHandle, IntPtr serializedMessageHandle, out RmwServiceInfo requestHeaderHandle,
        out IntPtr ptr, out int length);
}