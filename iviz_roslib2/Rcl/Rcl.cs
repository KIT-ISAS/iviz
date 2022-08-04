using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Iviz.Roslib2.Rcl.Wrappers;

namespace Iviz.Roslib2.Rcl;

public enum RclWrapperType
{
    Internal,
    Macos,
}

internal delegate void LoggingHandler(int severity, IntPtr name, long timestamp, IntPtr message);

internal static class Rcl
{
    static IRclWrapper? wrapper;

    static IRclWrapper Wrapper => wrapper ?? throw new NullReferenceException("Rcl wrapper has not been set!");

    public static void SetRclWrapperType(RclWrapperType wrapperType)
    {
        wrapper = wrapperType switch
        {
            RclWrapperType.Macos => new RclMacosWrapper(),
            RclWrapperType.Internal => new RclInternalWrapper(),
            _ => throw new IndexOutOfRangeException()
        };
    }

    public const int Ok = (int)RclRet.Ok;

    public static string ToString(IntPtr ptr)
    {
        return Marshal.PtrToStringUTF8(ptr) ?? "";
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe Span<T> CreateSpan<T>(IntPtr ptr, int size) where T : unmanaged
    {
        return new Span<T>(ptr.ToPointer(), size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe Span<IntPtr> CreateIntPtrSpan(IntPtr ptr, int size)
    {
        return new Span<IntPtr>(ptr.ToPointer(), size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe Span<byte> CreateByteSpan(IntPtr ptr, int size)
    {
        return new Span<byte>(ptr.ToPointer(), size);
    }

    public static void Check(IntPtr context, int result)
    {
        if (result == Ok)
        {
            return;
        }

        string msg = ToString(GetErrorString(context));
        throw new RosRclException(msg, result);
    }

    public static void Check(int result)
    {
        if (result == Ok)
        {
            return;
        }

        throw new RosRclException("Rcl operation failed", result);
    }

    public static bool SetDdsProfilePath(string path) =>
        Wrapper.SetDdsProfilePath(path);

    public static IntPtr CreateContext() =>
        Wrapper.CreateContext();

    public static void DestroyContext(IntPtr context) =>
        Wrapper.DestroyContext(context);

    public static int Init(IntPtr context) =>
        Wrapper.Init(context);

    public static int Shutdown(IntPtr contextHandle) =>
        Wrapper.Shutdown(contextHandle);

    public static int InitLogging() =>
        Wrapper.InitLogging();

    public static void SetLoggingLevel(int level) =>
        Wrapper.SetLoggingLevel(level);

    public static void SetLoggingHandler(LoggingHandler? handler) =>
        Wrapper.SetLoggingHandler(handler);

    public static int CreateNode(IntPtr contextHandle, out IntPtr nodeHandle, string name, string @namespace) =>
        Wrapper.CreateNode(contextHandle, out nodeHandle, name, @namespace);

    public static int DestroyNode(IntPtr nodeHandle) =>
        Wrapper.DestroyNode(nodeHandle);

    public static IntPtr GetFullyQualifiedNodeName(IntPtr nodeHandle) =>
        Wrapper.GetFullyQualifiedNodeName(nodeHandle);

    public static IntPtr CreateGuard(IntPtr contextHandle) =>
        Wrapper.CreateGuard(contextHandle);

    public static int DestroyGuard(IntPtr guardHandle) =>
        Wrapper.DestroyGuard(guardHandle);

    public static int TriggerGuard(IntPtr guardHandle) =>
        Wrapper.TriggerGuard(guardHandle);

    public static IntPtr CreateWaitSet() =>
        Wrapper.CreateWaitSet();

    public static int WaitSetInit(IntPtr contextHandle, IntPtr waitSetHandle, int numberOfSubscriptions,
        int numberOfGuardConditions, int numberOfTimers, int numberOfClients, int numberOfServices,
        int numberOfEvents) =>
        Wrapper.WaitSetInit(contextHandle, waitSetHandle, numberOfSubscriptions,
            numberOfGuardConditions, numberOfTimers, numberOfClients, numberOfServices,
            numberOfEvents);

    public static int Wait(IntPtr waitSetHandle, int timeoutInMs,
        out IntPtr subscriptionHandles,
        out IntPtr guardHandles,
        out IntPtr clientHandles,
        out IntPtr serviceHandles) =>
        Wrapper.Wait(waitSetHandle, timeoutInMs,
            out subscriptionHandles,
            out guardHandles,
            out clientHandles,
            out serviceHandles);

    public static int WaitClearAndAdd(IntPtr waitSetHandle,
        in IntPtr subscriptionHandles, int numSubscriptionHandles,
        in IntPtr guardHandles, int numGuardHandles,
        in IntPtr clientHandles, int numClientHandles,
        in IntPtr serviceHandles, int numServiceHandles) =>
        Wrapper.WaitClearAndAdd(waitSetHandle,
            in subscriptionHandles, numSubscriptionHandles,
            in guardHandles, numGuardHandles,
            in clientHandles, numClientHandles,
            in serviceHandles, numServiceHandles);

    public static int DestroyWaitSet(IntPtr waitSetHandle) =>
        Wrapper.DestroyWaitSet(waitSetHandle);

    static IntPtr GetErrorString(IntPtr contextHandle) =>
        Wrapper.GetErrorString(contextHandle);

    public static int CreateSubscriptionHandle(out IntPtr subscriptionHandle, IntPtr nodeHandle, string topic,
        string type, in RmwQosProfile profile) =>
        Wrapper.CreateSubscriptionHandle(out subscriptionHandle, nodeHandle, topic, type, in profile);

    public static bool IsMessageTypeSupported(string type) =>
        Wrapper.IsMessageTypeSupported(type);

    public static bool IsServiceTypeSupported(string type) =>
        Wrapper.IsServiceTypeSupported(type);

    public static int DestroySubscriptionHandle(IntPtr subscriptionHandle, IntPtr nodeHandle) =>
        Wrapper.DestroySubscriptionHandle(subscriptionHandle, nodeHandle);

    public static int GetPublisherCount(IntPtr subscriptionHandle, out int count) =>
        Wrapper.GetPublisherCount(subscriptionHandle, out count);

    public static int TakeSerializedMessage(IntPtr subscriptionHandle, IntPtr serializedMessage, out IntPtr ptr,
        out int length, out Guid gid, out byte moreRemaining) =>
        Wrapper.TakeSerializedMessage(subscriptionHandle, serializedMessage, out ptr, out length, out gid,
            out moreRemaining);

    public static int DestroySerializedMessage(IntPtr messageHandle) =>
        Wrapper.DestroySerializedMessage(messageHandle);

    public static int CreateSerializedMessage(out IntPtr messageHandle) =>
        Wrapper.CreateSerializedMessage(out messageHandle);

    public static int EnsureSerializedMessageSize(IntPtr messageHandle, int size, out IntPtr ptr) =>
        Wrapper.EnsureSerializedMessageSize(messageHandle, size, out ptr);

    public static int CreatePublisherHandle(out IntPtr publisherHandle, IntPtr nodeHandle, string topic, string type,
        in RmwQosProfile profile) =>
        Wrapper.CreatePublisherHandle(out publisherHandle, nodeHandle, topic, type, in profile);

    public static int DestroyPublisherHandle(IntPtr publisherHandle, IntPtr nodeHandle) =>
        Wrapper.DestroyPublisherHandle(publisherHandle, nodeHandle);

    public static int GetSubscriptionCount(IntPtr publisherHandle, out int count) =>
        Wrapper.GetSubscriptionCount(publisherHandle, out count);

    public static int PublishSerializedMessage(IntPtr publisherHandle, IntPtr serializedMessageHandle) =>
        Wrapper.PublishSerializedMessage(publisherHandle, serializedMessageHandle);

    public static int GetNodeNames(IntPtr contextHandle, IntPtr nodeHandle,
        out IntPtr nodeNamesHandle, out int numNodeNames,
        out IntPtr nodeNamespacesHandle, out int numNodeNamespaces) =>
        Wrapper.GetNodeNames(contextHandle, nodeHandle,
            out nodeNamesHandle, out numNodeNames,
            out nodeNamespacesHandle, out numNodeNamespaces);

    public static int GetTopicNamesAndTypes(IntPtr contextHandle, IntPtr nodeHandle,
        out IntPtr topicNamesHandle, out IntPtr topicTypesHandle, out int numTopicTypes) =>
        Wrapper.GetTopicNamesAndTypes(contextHandle, nodeHandle,
            out topicNamesHandle, out topicTypesHandle, out numTopicTypes);

    public static int GetServiceNamesAndTypes(IntPtr contextHandle, IntPtr nodeHandle,
        out IntPtr topicNamesHandle, out IntPtr topicTypesHandle, out int numTopicTypes) =>
        Wrapper.GetServiceNamesAndTypes(contextHandle, nodeHandle,
            out topicNamesHandle, out topicTypesHandle, out numTopicTypes);

    public static int GetServiceNamesAndTypesByNode(IntPtr contextHandle, IntPtr nodeHandle, string nodeName,
        string nodeNamespace, out IntPtr serviceNamesHandle, out IntPtr serviceTypesHandle,
        out int numNodeNamespaces) =>
        Wrapper.GetServiceNamesAndTypesByNode(contextHandle, nodeHandle, nodeName,
            nodeNamespace, out serviceNamesHandle, out serviceTypesHandle, out numNodeNamespaces);

    public static int GetPublishersInfoByTopic(IntPtr contextHandle, IntPtr nodeHandle, string topic,
        out IntPtr nodeNamesHandle, out IntPtr nodeNamespacesHandle, out IntPtr topicTypesHandle,
        out IntPtr gidHandle, out IntPtr profilesHandle, out int numNodes) =>
        Wrapper.GetPublishersInfoByTopic(contextHandle, nodeHandle, topic,
            out nodeNamesHandle, out nodeNamespacesHandle, out topicTypesHandle,
            out gidHandle, out profilesHandle, out numNodes);

    public static int GetSubscribersInfoByTopic(IntPtr contextHandle, IntPtr nodeHandle, string topic,
        out IntPtr nodeNamesHandle, out IntPtr nodeNamespacesHandle, out IntPtr topicTypesHandle,
        out IntPtr gidHandle, out IntPtr profilesHandle, out int numNodes) =>
        Wrapper.GetSubscribersInfoByTopic(contextHandle, nodeHandle, topic,
            out nodeNamesHandle, out nodeNamespacesHandle, out topicTypesHandle,
            out gidHandle, out profilesHandle, out numNodes);

    public static int CountPublishers(IntPtr nodeHandle, string topic, out int count) =>
        Wrapper.CountPublishers(nodeHandle, topic, out count);

    public static int CountSubscribers(IntPtr nodeHandle, string topic, out int count) =>
        Wrapper.CountSubscribers(nodeHandle, topic, out count);

    public static IntPtr GetGraphGuardCondition(IntPtr nodeHandle) =>
        Wrapper.GetGraphGuardCondition(nodeHandle);

    public static int CreateClientHandle(out IntPtr serviceClientHandle, IntPtr nodeHandle, string service,
        string type, in RmwQosProfile profile) =>
        Wrapper.CreateClientHandle(out serviceClientHandle, nodeHandle, service, type, in profile);

    public static int DestroyClientHandle(IntPtr clientHandle, IntPtr nodeHandle) =>
        Wrapper.DestroyClientHandle(clientHandle, nodeHandle);

    public static int IsServiceServerAvailable(IntPtr clientHandle, IntPtr nodeHandle, out byte isAvailable) =>
        Wrapper.IsServiceServerAvailable(clientHandle, nodeHandle, out isAvailable);

    public static int SendRequest(IntPtr clientHandle, IntPtr serializedMessageHandle, out long sequenceId) =>
        Wrapper.SendRequest(clientHandle, serializedMessageHandle, out sequenceId);

    public static int TakeResponse(IntPtr clientHandle, IntPtr serializedMessageHandle,
        out RmwServiceInfo requestHeader,
        out IntPtr ptr, out int length) =>
        Wrapper.TakeResponse(clientHandle, serializedMessageHandle, out requestHeader, out ptr, out length);

    public static int CreateServiceHandle(out IntPtr serviceHandle, IntPtr nodeHandle, string service, string type,
        in RmwQosProfile profile) =>
        Wrapper.CreateServiceHandle(out serviceHandle, nodeHandle, service, type, in profile);

    public static int DestroyServiceHandle(IntPtr serviceHandle, IntPtr nodeHandle) =>
        Wrapper.DestroyServiceHandle(serviceHandle, nodeHandle);

    public static int SendResponse(IntPtr serviceHandle, IntPtr serializedMessageHandle,
        in RmwRequestId requestHeaderHandle) =>
        Wrapper.SendResponse(serviceHandle, serializedMessageHandle, in requestHeaderHandle);

    public static int TakeRequest(IntPtr serviceHandle, IntPtr serializedMessageHandle,
        out RmwServiceInfo requestHeaderHandle,
        out IntPtr ptr, out int length) =>
        Wrapper.TakeRequest(serviceHandle, serializedMessageHandle, out requestHeaderHandle, out ptr, out length);
}