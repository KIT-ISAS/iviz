namespace Iviz.Roslib2.Rcl.Wrappers;

public interface IRclWrapper
{
    bool SetDdsProfilePath(string path);
    IntPtr CreateContext();
    void DestroyContext(IntPtr context);
    int Init(IntPtr context);
    int Shutdown(IntPtr contextHandle);
    int InitLogging();
    void SetLoggingLevel(int level);
    void SetLoggingHandler(LoggingHandler? handler);
    int CreateNode(IntPtr contextHandle, out IntPtr nodeHandle, string name, string @namespace);
    int DestroyNode(IntPtr nodeHandle);

    IntPtr GetFullyQualifiedNodeName(IntPtr nodeHandle);

    IntPtr CreateGuard(IntPtr contextHandle);
    int DestroyGuard(IntPtr guardHandle);
    int TriggerGuard(IntPtr guardHandle);

    IntPtr CreateWaitSet();

    int WaitSetInit(IntPtr contextHandle, IntPtr waitSetHandle, int numberOfSubscriptions,
        int numberOfGuardConditions, int numberOfTimers, int numberOfClients, int numberOfServices,
        int numberOfEvents);

    int Wait(IntPtr waitSetHandle, int timeoutInMs,
        out IntPtr subscriptionHandles,
        out IntPtr guardHandles,
        out IntPtr clientHandles,
        out IntPtr serviceHandles);

    int WaitClearAndAdd(IntPtr waitSetHandle,
        in IntPtr subscriptionHandles, int numSubscriptionHandles,
        in IntPtr guardHandles, int numGuardHandles,
        in IntPtr clientHandles, int numClientHandles,
        in IntPtr serviceHandles, int numServiceHandles);

    int DestroyWaitSet(IntPtr waitSetHandle);
    IntPtr GetErrorString(IntPtr contextHandle);

    int CreateSubscriptionHandle(out IntPtr subscriptionHandle, IntPtr nodeHandle, string topic, string type,
        in RmwQosProfile profile);

    bool IsMessageTypeSupported(string type);
    bool IsServiceTypeSupported(string type);
    int DestroySubscriptionHandle(IntPtr subscriptionHandle, IntPtr nodeHandle);
    int GetPublisherCount(IntPtr subscriptionHandle, out int count);

    int TakeSerializedMessage(IntPtr subscriptionHandle, IntPtr serializedMessage, out IntPtr ptr,
        out int length, out Guid gid, out byte moreRemaining);

    int DestroySerializedMessage(IntPtr messageHandle);
    int CreateSerializedMessage(out IntPtr messageHandle);
    int EnsureSerializedMessageSize(IntPtr messageHandle, int size, out IntPtr ptr);

    int CreatePublisherHandle(out IntPtr publisherHandle, IntPtr nodeHandle, string topic, string type,
        in RmwQosProfile profile);

    int DestroyPublisherHandle(IntPtr publisherHandle, IntPtr nodeHandle);
    int GetSubscriptionCount(IntPtr publisherHandle, out int count);
    int PublishSerializedMessage(IntPtr publisherHandle, IntPtr serializedMessageHandle);

    int GetNodeNames(IntPtr contextHandle, IntPtr nodeHandle,
        out IntPtr nodeNamesHandle, out int numNodeNames,
        out IntPtr nodeNamespacesHandle, out int numNodeNamespaces);

    int GetTopicNamesAndTypes(IntPtr contextHandle, IntPtr nodeHandle,
        out IntPtr topicNamesHandle, out IntPtr topicTypesHandle, out int numTopicTypes);

    int GetServiceNamesAndTypes(IntPtr contextHandle, IntPtr nodeHandle,
        out IntPtr topicNamesHandle, out IntPtr topicTypesHandle, out int numTopicTypes);

    int GetServiceNamesAndTypesByNode(IntPtr contextHandle, IntPtr nodeHandle, string nodeName,
        string nodeNamespace, out IntPtr serviceNamesHandle, out IntPtr serviceTypesHandle, out int numNodeNamespaces);

    int GetPublishersInfoByTopic(IntPtr contextHandle, IntPtr nodeHandle, string topic,
        out IntPtr nodeNamesHandle, out IntPtr nodeNamespacesHandle, out IntPtr topicTypesHandle,
        out IntPtr gidHandle, out IntPtr profilesHandle, out int numNodes);

    int GetSubscribersInfoByTopic(IntPtr contextHandle, IntPtr nodeHandle, string topic,
        out IntPtr nodeNamesHandle, out IntPtr nodeNamespacesHandle, out IntPtr topicTypesHandle,
        out IntPtr gidHandle, out IntPtr profilesHandle, out int numNodes);

    int CountPublishers(IntPtr nodeHandle, string topic, out int count);
    int CountSubscribers(IntPtr nodeHandle, string topic, out int count);
    IntPtr GetGraphGuardCondition(IntPtr nodeHandle);

    int CreateClientHandle(out IntPtr serviceClientHandle, IntPtr nodeHandle, string service, string type,
        in RmwQosProfile profile);

    int DestroyClientHandle(IntPtr clientHandle, IntPtr nodeHandle);
    
    int IsServiceServerAvailable(IntPtr clientHandle, IntPtr nodeHandle, out byte isAvailable);
    
    int SendRequest(IntPtr clientHandle, IntPtr serializedMessageHandle, out long sequenceId);

    int TakeResponse(IntPtr clientHandle, IntPtr serializedMessageHandle, out RmwServiceInfo requestHeader,
        out IntPtr ptr, out int length);

    int CreateServiceHandle(out IntPtr serviceHandle, IntPtr nodeHandle, string service, string type,
        in RmwQosProfile profile);

    int DestroyServiceHandle(IntPtr serviceHandle, IntPtr nodeHandle);
    int SendResponse(IntPtr serviceHandle, IntPtr serializedMessageHandle, in RmwRequestId requestHeaderHandle);

    int TakeRequest(IntPtr serviceHandle, IntPtr serializedMessageHandle, out RmwServiceInfo requestHeaderHandle,
        out IntPtr ptr, out int length);
}