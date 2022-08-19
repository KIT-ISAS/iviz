namespace Iviz.Roslib2.RclInterop.Wrappers;

public interface IRclWrapper
{
    /// <summary>
    /// Sets the serialization and deserialization callbacks.
    /// </summary>
    void SetMessageCallbacks(
        CdrDeserializeCallback? cdrDeserializeCallback,
        CdrSerializeCallback? cdrSerializeCallback,
        CdrGetSerializedSizeCallback? cdrGetSerializedSizeCallback);

    /// <summary>
    /// Creates a variable on the C++ side that holds pointers and other stuff.
    /// Used to keep string and array pointers alive after returning to C#.
    /// </summary>
    IntPtr CreateContext();

    /// <summary>
    /// Destroys a context created by <see cref="CreateContext"/>.
    /// </summary>
    void DestroyContext(IntPtr contextHandle);

    /// <summary>
    /// Initializes the RCL library. Wrapper around rcl_init.
    /// </summary>
    int Init(IntPtr contextHandle, int domainId);

    /// <summary>
    /// Shuts down the RCL environment. Wrapper around rcl_shutdown.
    /// </summary>
    int Shutdown(IntPtr contextHandle);

    /// <summary>
    /// Initializes the logging system. Wrapper around rcutils_logging_initialize.
    /// </summary>
    int InitLogging();

    /// <summary>
    /// Sets the logging level. Wrapper around rcutils_logging_set_default_logger_level.
    /// </summary>
    void SetLoggingLevel(int level);

    /// <summary>
    /// Sets the logging callback.
    /// </summary>
    void SetLoggingHandler(LoggingHandler? handler);

    /// <summary>
    /// Creates an RCL node. Wrapper around rcl_node_init.
    /// </summary>
    int CreateNode(IntPtr contextHandle, out IntPtr nodeHandle, string name, string @namespace);

    /// <summary>
    /// Destroys an RCL node. Wrapper around rcl_node_fini.
    /// </summary>
    int DestroyNode(IntPtr nodeHandle);

    /// <summary>
    /// Returns the fully qualified name of the node. Wrapper around rcl_node_get_fully_qualified_name.
    /// </summary>
    IntPtr GetFullyQualifiedNodeName(IntPtr nodeHandle);

    /// <summary>
    /// Creates an RCL guard condition. Wrapper around rcl_guard_condition_init.
    /// </summary>
    IntPtr CreateGuard(IntPtr contextHandle);

    /// <summary>
    /// Destroys an RCL guard condition. Wrapper around rcl_guard_condition_fini.
    /// </summary>
    int DestroyGuard(IntPtr guardHandle);

    /// <summary>
    /// Triggers an RCL guard condition. Wrapper around rcl_trigger_guard_condition.
    /// </summary>
    int TriggerGuard(IntPtr guardHandle);

    /// <summary>
    /// Creates an RCL wait set. Wrapper around rcl_get_zero_initialized_wait_set.
    /// </summary>
    IntPtr CreateWaitSet();

    /// <summary>
    /// Initializes an RCL wait set. Wrapper around rcl_wait_set_init.
    /// </summary>
    int WaitSetInit(IntPtr contextHandle, IntPtr waitSetHandle, int numberOfSubscriptions,
        int numberOfGuardConditions, int numberOfTimers, int numberOfClients, int numberOfServices,
        int numberOfEvents);

    /// <summary>
    /// Waits for an RCL wait set to trigger. Wrapper around native_rcl_wait.
    /// </summary>
    int Wait(IntPtr waitSetHandle, int timeoutInMs,
        out IntPtr subscriptionHandles,
        out IntPtr guardHandles,
        out IntPtr clientHandles,
        out IntPtr serviceHandles);

    /// <summary>
    /// Clears an RCL wait set and sets new handles. Wrapper around rcl_wait_set_clear and rcl_wait_set_add_*.
    /// </summary>
    int WaitClearAndAdd(IntPtr waitSetHandle,
        in IntPtr subscriptionHandles, int numSubscriptionHandles,
        in IntPtr guardHandles, int numGuardHandles,
        in IntPtr clientHandles, int numClientHandles,
        in IntPtr serviceHandles, int numServiceHandles);

    /// <summary>
    /// Destroys an RCL wait set. Wrapper around rcl_wait_set_fini.
    /// </summary>
    int DestroyWaitSet(IntPtr waitSetHandle);

    /// <summary>
    /// Returns the last RCL error string. Wrapper around rcl_get_error_string.
    /// </summary>
    IntPtr GetErrorString(IntPtr contextHandle);

    int CreateSubscriptionHandle(out IntPtr subscriptionHandle, IntPtr nodeHandle, string topic, string type,
        in RmwQosProfile profile);

    int DestroySubscriptionHandle(IntPtr subscriptionHandle, IntPtr nodeHandle);
    int GetPublisherCount(IntPtr subscriptionHandle, out int count);

    int TakeSerializedMessage(IntPtr subscriptionHandle, IntPtr serializedMessage, out IntPtr ptr,
        out int length, out Guid gid, out byte moreRemaining);

    public int Take(IntPtr subscriptionHandle, IntPtr messageContextHandle, out Guid guid, out byte moreRemaining);

    int DestroySerializedMessage(IntPtr messageHandle);
    int CreateSerializedMessage(out IntPtr messageHandle);
    int EnsureSerializedMessageSize(IntPtr messageHandle, int size, out IntPtr ptr);

    int CreatePublisherHandle(out IntPtr publisherHandle, IntPtr nodeHandle, string topic, string type,
        in RmwQosProfile profile);

    int DestroyPublisherHandle(IntPtr publisherHandle, IntPtr nodeHandle);
    int GetSubscriptionCount(IntPtr publisherHandle, out int count);
    int Publish(IntPtr publisherHandle, IntPtr messageContextHandle);

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