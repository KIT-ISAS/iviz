using System.Runtime.InteropServices;

namespace Iviz.Roslib2.Rcl.Wrappers;

public sealed class RclInternalWrapper : IRclWrapper
{
    public bool SetDdsProfilePath(string path) =>
        Rcl.SetDdsProfilePath(path);

    public IntPtr CreateContext() =>
        Rcl.CreateContext();

    public void DestroyContext(IntPtr context) =>
        Rcl.DestroyContext(context);

    public int Init(IntPtr context) =>
        Rcl.Init(context);

    public int Shutdown(IntPtr contextHandle) =>
        Rcl.Shutdown(contextHandle);

    public int InitLogging() =>
        Rcl.InitLogging();

    public void SetLoggingLevel(int level) =>
        Rcl.SetLoggingLevel(level);

    public void SetLoggingHandler(LoggingHandler? handler) =>
        Rcl.SetLoggingHandler(handler);

    public int CreateNode(IntPtr contextHandle, out IntPtr nodeHandle, string name, string @namespace) =>
        Rcl.CreateNode(contextHandle, out nodeHandle, name, @namespace);

    public int DestroyNode(IntPtr nodeHandle) =>
        Rcl.DestroyNode(nodeHandle);

    public IntPtr GetFullyQualifiedNodeName(IntPtr nodeHandle) =>
        Rcl.GetFullyQualifiedNodeName(nodeHandle);

    public IntPtr CreateGuard(IntPtr contextHandle) =>
        Rcl.CreateGuard(contextHandle);

    public int DestroyGuard(IntPtr guardHandle) =>
        Rcl.DestroyGuard(guardHandle);

    public int TriggerGuard(IntPtr guardHandle) =>
        Rcl.TriggerGuard(guardHandle);

    public IntPtr CreateWaitSet() =>
        Rcl.CreateWaitSet();

    public int WaitSetInit(IntPtr contextHandle, IntPtr waitSetHandle, int numberOfSubscriptions,
        int numberOfGuardConditions, int numberOfTimers, int numberOfClients, int numberOfServices,
        int numberOfEvents) =>
        Rcl.WaitSetInit(contextHandle, waitSetHandle, numberOfSubscriptions,
            numberOfGuardConditions, numberOfTimers, numberOfClients, numberOfServices,
            numberOfEvents);

    public int Wait(IntPtr waitSetHandle, int timeoutInMs,
        out IntPtr subscriptionHandles,
        out IntPtr guardHandles,
        out IntPtr clientHandles,
        out IntPtr serviceHandles) =>
        Rcl.Wait(waitSetHandle, timeoutInMs,
            out subscriptionHandles,
            out guardHandles,
            out clientHandles,
            out serviceHandles);

    public int WaitClearAndAdd(IntPtr waitSetHandle,
        in IntPtr subscriptionHandles, int numSubscriptionHandles,
        in IntPtr guardHandles, int numGuardHandles,
        in IntPtr clientHandles, int numClientHandles,
        in IntPtr serviceHandles, int numServiceHandles) =>
        Rcl.WaitClearAndAdd(waitSetHandle,
            in subscriptionHandles, numSubscriptionHandles,
            in guardHandles, numGuardHandles,
            in clientHandles, numClientHandles,
            in serviceHandles, numServiceHandles);

    public int DestroyWaitSet(IntPtr waitSetHandle) =>
        Rcl.DestroyWaitSet(waitSetHandle);

    public IntPtr GetErrorString(IntPtr contextHandle) =>
        Rcl.GetErrorString(contextHandle);

    public int CreateSubscriptionHandle(out IntPtr subscriptionHandle, IntPtr nodeHandle, string topic, string type,
        in RmwQosProfile profile) =>
        Rcl.CreateSubscriptionHandle(out subscriptionHandle, nodeHandle, topic, type, in profile);

    public bool IsMessageTypeSupported(string type) =>
        Rcl.IsMessageTypeSupported(type);

    public bool IsServiceTypeSupported(string type) =>
        Rcl.IsServiceTypeSupported(type);

    public int DestroySubscriptionHandle(IntPtr subscriptionHandle, IntPtr nodeHandle) =>
        Rcl.DestroySubscriptionHandle(subscriptionHandle, nodeHandle);

    public int GetPublisherCount(IntPtr subscriptionHandle, out int count) =>
        Rcl.GetPublisherCount(subscriptionHandle, out count);

    public int TakeSerializedMessage(IntPtr subscriptionHandle, IntPtr serializedMessage, out IntPtr ptr,
        out int length, out Guid gid, out byte moreRemaining) =>
        Rcl.TakeSerializedMessage(subscriptionHandle, serializedMessage, out ptr, out length, out gid,
            out moreRemaining);

    public int DestroySerializedMessage(IntPtr messageHandle) =>
        Rcl.DestroySerializedMessage(messageHandle);

    public int CreateSerializedMessage(out IntPtr messageHandle) =>
        Rcl.CreateSerializedMessage(out messageHandle);

    public int EnsureSerializedMessageSize(IntPtr messageHandle, int size, out IntPtr ptr) =>
        Rcl.EnsureSerializedMessageSize(messageHandle, size, out ptr);

    public int CreatePublisherHandle(out IntPtr publisherHandle, IntPtr nodeHandle, string topic, string type,
        in RmwQosProfile profile) =>
        Rcl.CreatePublisherHandle(out publisherHandle, nodeHandle, topic, type, in profile);

    public int DestroyPublisherHandle(IntPtr publisherHandle, IntPtr nodeHandle) =>
        Rcl.DestroyPublisherHandle(publisherHandle, nodeHandle);

    public int GetSubscriptionCount(IntPtr publisherHandle, out int count) =>
        Rcl.GetSubscriptionCount(publisherHandle, out count);

    public int PublishSerializedMessage(IntPtr publisherHandle, IntPtr serializedMessageHandle) =>
        Rcl.PublishSerializedMessage(publisherHandle, serializedMessageHandle);

    public int GetNodeNames(IntPtr contextHandle, IntPtr nodeHandle,
        out IntPtr nodeNamesHandle, out int numNodeNames,
        out IntPtr nodeNamespacesHandle, out int numNodeNamespaces) =>
        Rcl.GetNodeNames(contextHandle, nodeHandle,
            out nodeNamesHandle, out numNodeNames,
            out nodeNamespacesHandle, out numNodeNamespaces);

    public int GetTopicNamesAndTypes(IntPtr contextHandle, IntPtr nodeHandle,
        out IntPtr topicNamesHandle, out IntPtr topicTypesHandle, out int numTopicTypes) =>
        Rcl.GetTopicNamesAndTypes(contextHandle, nodeHandle,
            out topicNamesHandle, out topicTypesHandle, out numTopicTypes);

    public int GetServiceNamesAndTypes(IntPtr contextHandle, IntPtr nodeHandle,
        out IntPtr topicNamesHandle, out IntPtr topicTypesHandle, out int numTopicTypes) =>
        Rcl.GetServiceNamesAndTypes(contextHandle, nodeHandle,
            out topicNamesHandle, out topicTypesHandle, out numTopicTypes);

    public int GetServiceNamesAndTypesByNode(IntPtr contextHandle, IntPtr nodeHandle, string nodeName,
        string nodeNamespace, out IntPtr serviceNamesHandle, out IntPtr serviceTypesHandle,
        out int numNodeNamespaces) =>
        Rcl.GetServiceNamesAndTypesByNode(contextHandle, nodeHandle, nodeName,
            nodeNamespace, out serviceNamesHandle, out serviceTypesHandle,
            out numNodeNamespaces);

    public int GetPublishersInfoByTopic(IntPtr contextHandle, IntPtr nodeHandle, string topic,
        out IntPtr nodeNamesHandle, out IntPtr nodeNamespacesHandle, out IntPtr topicTypesHandle,
        out IntPtr gidHandle, out IntPtr profilesHandle, out int numNodes) =>
        Rcl.GetPublishersInfoByTopic(contextHandle, nodeHandle, topic,
            out nodeNamesHandle, out nodeNamespacesHandle, out topicTypesHandle,
            out gidHandle, out profilesHandle, out numNodes);

    public int GetSubscribersInfoByTopic(IntPtr contextHandle, IntPtr nodeHandle, string topic,
        out IntPtr nodeNamesHandle, out IntPtr nodeNamespacesHandle, out IntPtr topicTypesHandle,
        out IntPtr gidHandle, out IntPtr profilesHandle, out int numNodes) =>
        Rcl.GetSubscribersInfoByTopic(contextHandle, nodeHandle, topic,
            out nodeNamesHandle, out nodeNamespacesHandle, out topicTypesHandle,
            out gidHandle, out profilesHandle, out numNodes);

    public int CountPublishers(IntPtr nodeHandle, string topic, out int count) =>
        Rcl.CountPublishers(nodeHandle, topic, out count);

    public int CountSubscribers(IntPtr nodeHandle, string topic, out int count) =>
        Rcl.CountSubscribers(nodeHandle, topic, out count);

    public IntPtr GetGraphGuardCondition(IntPtr nodeHandle) =>
        Rcl.GetGraphGuardCondition(nodeHandle);

    public int CreateClientHandle(out IntPtr serviceClientHandle, IntPtr nodeHandle, string service, string type,
        in RmwQosProfile profile) =>
        Rcl.CreateClientHandle(out serviceClientHandle, nodeHandle, service, type, in profile);

    public int DestroyClientHandle(IntPtr clientHandle, IntPtr nodeHandle) =>
        Rcl.DestroyClientHandle(clientHandle, nodeHandle);

    public int IsServiceServerAvailable(IntPtr clientHandle, IntPtr nodeHandle, out byte isAvailable) =>
        Rcl.IsServiceServerAvailable(clientHandle, nodeHandle, out isAvailable);

    public int SendRequest(IntPtr clientHandle, IntPtr serializedMessageHandle, out long sequenceId) =>
        Rcl.SendRequest(clientHandle, serializedMessageHandle, out sequenceId);

    public int TakeResponse(IntPtr clientHandle, IntPtr serializedMessageHandle, out RmwServiceInfo requestHeader,
        out IntPtr ptr, out int length) =>
        Rcl.TakeResponse(clientHandle, serializedMessageHandle, out requestHeader, out ptr, out length);

    public int CreateServiceHandle(out IntPtr serviceHandle, IntPtr nodeHandle, string service, string type,
        in RmwQosProfile profile) =>
        Rcl.CreateServiceHandle(out serviceHandle, nodeHandle, service, type, in profile);

    public int DestroyServiceHandle(IntPtr serviceHandle, IntPtr nodeHandle) =>
        Rcl.DestroyServiceHandle(serviceHandle, nodeHandle);

    public int SendResponse(IntPtr serviceHandle, IntPtr serializedMessageHandle,
        in RmwRequestId requestHeaderHandle) =>
        Rcl.SendResponse(serviceHandle, serializedMessageHandle, in requestHeaderHandle);

    public int TakeRequest(IntPtr serviceHandle, IntPtr serializedMessageHandle, out RmwServiceInfo requestHeaderHandle,
        out IntPtr ptr, out int length) =>
        Rcl.TakeRequest(serviceHandle, serializedMessageHandle, out requestHeaderHandle, out ptr, out length);


    static class Rcl
    {
        const string Library = "__Internal";

        [DllImport(Library, EntryPoint = "native_rcl_set_dds_profile_path")]
        public static extern bool SetDdsProfilePath([MarshalAs(UnmanagedType.LPUTF8Str)] string path);

        [DllImport(Library, EntryPoint = "native_rcl_create_context")]
        public static extern IntPtr CreateContext();

        [DllImport(Library, EntryPoint = "native_rcl_destroy_context")]
        public static extern void DestroyContext(IntPtr context);

        [DllImport(Library, EntryPoint = "native_rcl_init")]
        public static extern int Init(IntPtr context);

        [DllImport(Library, EntryPoint = "native_rcl_shutdown")]
        public static extern int Shutdown(IntPtr contextHandle);

        [DllImport(Library, EntryPoint = "native_rcl_init_logging")]
        public static extern int InitLogging();

        [DllImport(Library, EntryPoint = "native_rcl_set_logging_level")]
        public static extern void SetLoggingLevel(int level);

        [DllImport(Library, EntryPoint = "native_rcl_set_logging_handler")]
        public static extern void SetLoggingHandler([MarshalAs(UnmanagedType.FunctionPtr)] LoggingHandler? handler);

        [DllImport(Library, EntryPoint = "native_rcl_create_node_handle")]
        public static extern int CreateNode(IntPtr contextHandle, out IntPtr nodeHandle,
            [MarshalAs(UnmanagedType.LPUTF8Str)] string name, [MarshalAs(UnmanagedType.LPUTF8Str)] string @namespace);

        [DllImport(Library, EntryPoint = "native_rcl_destroy_node_handle")]
        public static extern int DestroyNode(IntPtr nodeHandle);


        [DllImport(Library, EntryPoint = "native_rcl_get_fully_qualified_node_name")]
        public static extern IntPtr GetFullyQualifiedNodeName(IntPtr nodeHandle);


        [DllImport(Library, EntryPoint = "native_rcl_create_guard")]
        public static extern IntPtr CreateGuard(IntPtr contextHandle);

        [DllImport(Library, EntryPoint = "native_rcl_destroy_guard")]
        public static extern int DestroyGuard(IntPtr guardHandle);

        [DllImport(Library, EntryPoint = "native_rcl_trigger_guard")]
        public static extern int TriggerGuard(IntPtr guardHandle);


        [DllImport(Library, EntryPoint = "native_rcl_create_wait_set")]
        public static extern IntPtr CreateWaitSet();

        [DllImport(Library, EntryPoint = "native_rcl_wait_set_init")]
        public static extern int WaitSetInit(IntPtr contextHandle, IntPtr waitSetHandle, int numberOfSubscriptions,
            int numberOfGuardConditions, int numberOfTimers, int numberOfClients, int numberOfServices,
            int numberOfEvents);

        [DllImport(Library, EntryPoint = "native_rcl_wait")]
        public static extern int Wait(IntPtr waitSetHandle, int timeoutInMs,
            out IntPtr subscriptionHandles,
            out IntPtr guardHandles,
            out IntPtr clientHandles,
            out IntPtr serviceHandles);

        [DllImport(Library, EntryPoint = "native_rcl_wait_clear_and_add")]
        public static extern int WaitClearAndAdd(IntPtr waitSetHandle,
            in IntPtr subscriptionHandles, int numSubscriptionHandles,
            in IntPtr guardHandles, int numGuardHandles,
            in IntPtr clientHandles, int numClientHandles,
            in IntPtr serviceHandles, int numServiceHandles);

        [DllImport(Library, EntryPoint = "native_rcl_destroy_wait_set")]
        public static extern int DestroyWaitSet(IntPtr waitSetHandle);

        [DllImport(Library, EntryPoint = "native_rcl_get_error_string")]
        public static extern IntPtr GetErrorString(IntPtr contextHandle);

        [DllImport(Library, EntryPoint = "native_rcl_create_subscription_handle")]
        public static extern int CreateSubscriptionHandle(out IntPtr subscriptionHandle, IntPtr nodeHandle,
            [MarshalAs(UnmanagedType.LPUTF8Str)] string topic,
            [MarshalAs(UnmanagedType.LPUTF8Str)] string type,
            in RmwQosProfile profile);

        [DllImport(Library, EntryPoint = "native_rcl_is_message_type_supported")]
        public static extern bool IsMessageTypeSupported([MarshalAs(UnmanagedType.LPUTF8Str)] string type);

        [DllImport(Library, EntryPoint = "native_rcl_is_service_type_supported")]
        public static extern bool IsServiceTypeSupported([MarshalAs(UnmanagedType.LPUTF8Str)] string type);

        [DllImport(Library, EntryPoint = "native_rcl_destroy_subscription_handle")]
        public static extern int DestroySubscriptionHandle(IntPtr subscriptionHandle, IntPtr nodeHandle);

        [DllImport(Library, EntryPoint = "native_rcl_subscription_get_publisher_count")]
        public static extern int GetPublisherCount(IntPtr subscriptionHandle, out int count);

        [DllImport(Library, EntryPoint = "native_rcl_take_serialized_message")]
        public static extern int TakeSerializedMessage(IntPtr subscriptionHandle, IntPtr serializedMessage,
            out IntPtr ptr, out int length, out Guid gid, out byte moreRemaining);

        [DllImport(Library, EntryPoint = "native_rcl_destroy_serialized_message")]
        public static extern int DestroySerializedMessage(IntPtr messageHandle);

        [DllImport(Library, EntryPoint = "native_rcl_create_serialized_message")]
        public static extern int CreateSerializedMessage(out IntPtr messageHandle);

        [DllImport(Library, EntryPoint = "native_rcl_ensure_serialized_message_size")]
        public static extern int EnsureSerializedMessageSize(IntPtr messageHandle, int size, out IntPtr ptr);

        [DllImport(Library, EntryPoint = "native_rcl_create_publisher_handle")]
        public static extern int CreatePublisherHandle(out IntPtr publisherHandle, IntPtr nodeHandle,
            [MarshalAs(UnmanagedType.LPUTF8Str)] string topic,
            [MarshalAs(UnmanagedType.LPUTF8Str)] string type,
            in RmwQosProfile profile);

        [DllImport(Library, EntryPoint = "native_rcl_destroy_publisher_handle")]
        public static extern int DestroyPublisherHandle(IntPtr publisherHandle, IntPtr nodeHandle);

        [DllImport(Library, EntryPoint = "native_rcl_publisher_get_subscription_count")]
        public static extern int GetSubscriptionCount(IntPtr publisherHandle, out int count);

        [DllImport(Library, EntryPoint = "native_rcl_publish_serialized_message")]
        public static extern int PublishSerializedMessage(IntPtr publisherHandle, IntPtr serializedMessageHandle);

        [DllImport(Library, EntryPoint = "native_rcl_get_node_names")]
        public static extern int GetNodeNames(IntPtr contextHandle, IntPtr nodeHandle,
            out IntPtr nodeNamesHandle, out int numNodeNames,
            out IntPtr nodeNamespacesHandle, out int numNodeNamespaces);


        [DllImport(Library, EntryPoint = "native_rcl_get_topic_names_and_types")]
        public static extern int GetTopicNamesAndTypes(IntPtr contextHandle, IntPtr nodeHandle,
            out IntPtr topicNamesHandle, out IntPtr topicTypesHandle, out int numTopicTypes);

        [DllImport(Library, EntryPoint = "native_rcl_get_service_names_and_types")]
        public static extern int GetServiceNamesAndTypes(IntPtr contextHandle, IntPtr nodeHandle,
            out IntPtr topicNamesHandle, out IntPtr topicTypesHandle, out int numTopicTypes);

        
        [DllImport(Library, EntryPoint = "native_rcl_get_service_names_and_types_by_node")]
        public static extern int GetServiceNamesAndTypesByNode(IntPtr contextHandle, IntPtr nodeHandle,
            [MarshalAs(UnmanagedType.LPUTF8Str)] string nodeName,
            [MarshalAs(UnmanagedType.LPUTF8Str)] string nodeNamespace,
            out IntPtr serviceNamesHandle, out IntPtr serviceTypesHandle, out int numNodeNamespaces);

        [DllImport(Library, EntryPoint = "native_rcl_get_publishers_info_by_topic")]
        public static extern int GetPublishersInfoByTopic(IntPtr contextHandle, IntPtr nodeHandle,
            [MarshalAs(UnmanagedType.LPUTF8Str)] string topic,
            out IntPtr nodeNamesHandle, out IntPtr nodeNamespacesHandle, out IntPtr topicTypesHandle,
            out IntPtr gidHandle, out IntPtr profilesHandle, out int numNodes);

        [DllImport(Library, EntryPoint = "native_rcl_get_subscribers_info_by_topic")]
        public static extern int GetSubscribersInfoByTopic(IntPtr contextHandle, IntPtr nodeHandle,
            [MarshalAs(UnmanagedType.LPUTF8Str)] string topic,
            out IntPtr nodeNamesHandle, out IntPtr nodeNamespacesHandle, out IntPtr topicTypesHandle,
            out IntPtr gidHandle, out IntPtr profilesHandle, out int numNodes);

        [DllImport(Library, EntryPoint = "native_rcl_count_publishers")]
        public static extern int CountPublishers(IntPtr nodeHandle,
            [MarshalAs(UnmanagedType.LPUTF8Str)] string topic, out int count);

        [DllImport(Library, EntryPoint = "native_rcl_count_subscribers")]
        public static extern int CountSubscribers(IntPtr nodeHandle,
            [MarshalAs(UnmanagedType.LPUTF8Str)] string topic, out int count);

        [DllImport(Library, EntryPoint = "native_rcl_get_graph_guard_condition")]
        public static extern IntPtr GetGraphGuardCondition(IntPtr nodeHandle);


        [DllImport(Library, EntryPoint = "native_rcl_create_client_handle")]
        public static extern int CreateClientHandle(out IntPtr serviceClientHandle, IntPtr nodeHandle,
            [MarshalAs(UnmanagedType.LPUTF8Str)] string service,
            [MarshalAs(UnmanagedType.LPUTF8Str)] string type,
            in RmwQosProfile profile);

        [DllImport(Library, EntryPoint = "native_rcl_destroy_client_handle")]
        public static extern int DestroyClientHandle(IntPtr clientHandle, IntPtr nodeHandle);

        [DllImport(Library, EntryPoint = "native_rcl_is_service_server_available")]
        public static extern int IsServiceServerAvailable(IntPtr clientHandle, IntPtr nodeHandle, out byte isAvailable);

        [DllImport(Library, EntryPoint = "native_rcl_send_request")]
        public static extern int SendRequest(IntPtr clientHandle, IntPtr serializedMessageHandle, out long sequenceId);

        [DllImport(Library, EntryPoint = "native_rcl_take_response")]
        public static extern int TakeResponse(IntPtr clientHandle, IntPtr serializedMessageHandle,
            out RmwServiceInfo requestHeader, out IntPtr ptr, out int length);

        [DllImport(Library, EntryPoint = "native_rcl_create_service_handle")]
        public static extern int CreateServiceHandle(out IntPtr serviceHandle, IntPtr nodeHandle,
            [MarshalAs(UnmanagedType.LPUTF8Str)] string service,
            [MarshalAs(UnmanagedType.LPUTF8Str)] string type,
            in RmwQosProfile profile);

        [DllImport(Library, EntryPoint = "native_rcl_destroy_service_handle")]
        public static extern int DestroyServiceHandle(IntPtr serviceHandle, IntPtr nodeHandle);

        [DllImport(Library, EntryPoint = "native_rcl_send_response")]
        public static extern int SendResponse(IntPtr serviceHandle, IntPtr serializedMessageHandle,
            in RmwRequestId requestHeaderHandle);

        [DllImport(Library, EntryPoint = "native_rcl_take_request")]
        public static extern int TakeRequest(IntPtr serviceHandle, IntPtr serializedMessageHandle,
            out RmwServiceInfo requestHeaderHandle, out IntPtr ptr, out int length);
    }
}