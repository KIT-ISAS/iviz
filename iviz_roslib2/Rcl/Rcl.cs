using System;
using System.Runtime.InteropServices;

namespace Iviz.Roslib2.Rcl;

internal static class Rcl
{
    //const string Library = "iviz_ros2_rcl.framework/iviz_ros2_rcl";
    const string Library = "iviz_ros2_rcl_macos";

    public const int Ok = (int)RclRet.Ok;

    public delegate void LoggingHandler(int severity, IntPtr name, long timestamp, IntPtr message);

    public static string ToString(IntPtr ptr)
    {
        return Marshal.PtrToStringUTF8(ptr) ?? "";
    }

    public static unsafe IntPtr GetArrayValue(IntPtr ptr, int index) => (IntPtr)((void**)ptr.ToPointer())[index];

    public static unsafe ref T GetArrayValue<T>(IntPtr ptr, int index) where T : unmanaged => ref ((T*)ptr.ToPointer())[index];

    public static void Check(IntPtr context, int result)
    {
        if (result == Ok)
        {
            return;
        }

        string msg = ToString(GetErrorString(context));
        throw new Exception(msg);
    }


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

    [DllImport(Library, EntryPoint = "native_rcl_ok")]
    public static extern bool IsOk(IntPtr contextHandle);

    [DllImport(Library, EntryPoint = "native_rcl_create_wait_set")]
    public static extern IntPtr CreateWaitSet();

    [DllImport(Library, EntryPoint = "native_rcl_wait_set_init")]
    public static extern int WaitSetInit(IntPtr contextHandle, IntPtr waitSetHandle, int numberOfSubscriptions,
        int numberOfGuardConditions, int numberOfTimers, int numberOfClients, int numberOfServices, int numberOfEvents);

    [DllImport(Library, EntryPoint = "native_rcl_wait_set_clear")]
    public static extern int WaitSetClear(IntPtr waitSetHandle);

    [DllImport(Library, EntryPoint = "native_rcl_wait_set_add_subscription")]
    public static extern int WaitSetAddSubscription(IntPtr waitSetHandle, IntPtr subscriptionHandle);

    [DllImport(Library, EntryPoint = "native_rcl_wait")]
    public static extern int Wait(IntPtr waitSetHandle, int timeoutInMs);

    [DllImport(Library, EntryPoint = "native_rcl_destroy_wait_set")]
    public static extern int DestroyWaitSet(IntPtr waitSetHandle);

    [DllImport(Library, EntryPoint = "native_rcl_get_error_string")]
    public static extern IntPtr GetErrorString(IntPtr contextHandle);

    [DllImport(Library, EntryPoint = "native_rcl_create_subscription_handle")]
    public static extern int CreateSubscriptionHandle(out IntPtr subscriptionHandle, IntPtr nodeHandle,
        [MarshalAs(UnmanagedType.LPUTF8Str)] string topic, [MarshalAs(UnmanagedType.LPUTF8Str)] string type);

    [DllImport(Library, EntryPoint = "native_rcl_is_type_supported")]
    public static extern bool IsTypeSupported([MarshalAs(UnmanagedType.LPUTF8Str)] string type);

    [DllImport(Library, EntryPoint = "native_rcl_destroy_subscription_handle")]
    public static extern int DestroySubscriptionHandle(IntPtr subscriptionHandle, IntPtr nodeHandle);

    [DllImport(Library, EntryPoint = "native_rcl_subscription_get_publisher_count")]
    public static extern int GetPublisherCount(IntPtr subscriptionHandle, out int count);

    [DllImport(Library, EntryPoint = "native_rcl_take_serialized_message")]
    public static extern int TakeSerializedMessage(IntPtr subscriptionHandle, IntPtr serializedMessage, out IntPtr ptr,
        out int length, out Guid gid);

    [DllImport(Library, EntryPoint = "native_rcl_destroy_serialized_message")]
    public static extern int DestroySerializedMessage(IntPtr messageHandle);

    [DllImport(Library, EntryPoint = "native_rcl_create_serialized_message")]
    public static extern int CreateSerializedMessage(out IntPtr messageHandle);

    [DllImport(Library, EntryPoint = "native_rcl_ensure_serialized_message_size")]
    public static extern int EnsureSerializedMessageSize(IntPtr messageHandle, int size, out IntPtr ptr);

    [DllImport(Library, EntryPoint = "native_rcl_create_publisher_handle")]
    public static extern int CreatePublisherHandle(out IntPtr publisherHandle, IntPtr nodeHandle,
        [MarshalAs(UnmanagedType.LPUTF8Str)] string topic,
        [MarshalAs(UnmanagedType.LPUTF8Str)] string type);

    [DllImport(Library, EntryPoint = "native_rcl_destroy_publisher_handle")]
    public static extern int DestroyPublisherHandle(IntPtr publisherHandle, IntPtr nodeHandle);

    [DllImport(Library, EntryPoint = "native_rcl_publisher_get_subscription_count")]
    public static extern int GetSubscriptionCount(IntPtr publisherHandle, out int count);

    [DllImport(Library, EntryPoint = "native_rcl_publish")]
    public static extern int Publish(IntPtr publisherHandle, IntPtr rawRosMessage);

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
        out IntPtr nodeNamesHandle, out int numNodeNames,
        out IntPtr nodeNamespacesHandle, out int numNodeNamespaces);

    [DllImport(Library, EntryPoint = "native_rcl_get_publishers_info_by_topic")]
    public static extern int GetPublishersInfoByTopic(IntPtr contextHandle, IntPtr nodeHandle,
        [MarshalAs(UnmanagedType.LPUTF8Str)] string topic,
        out IntPtr nodeNamesHandle, out IntPtr nodeNamespacesHandle, out IntPtr topicTypesHandle,
        out IntPtr gidHandle, out int numNodes);

    [DllImport(Library, EntryPoint = "native_rcl_get_subscribers_info_by_topic")]
    public static extern int GetSubscribersInfoByTopic(IntPtr contextHandle, IntPtr nodeHandle,
        [MarshalAs(UnmanagedType.LPUTF8Str)] string topic,
        out IntPtr nodeNamesHandle, out IntPtr nodeNamespacesHandle, out IntPtr topicTypesHandle,
        out IntPtr gidHandle, out int numNodes);
}