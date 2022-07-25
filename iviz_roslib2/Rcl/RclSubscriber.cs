using System;
using Iviz.Msgs;
using Iviz.Tools;

namespace Iviz.Roslib2.Rcl;

internal sealed class RclSubscriber : IDisposable
{
    readonly IntPtr contextHandle;
    readonly IntPtr nodeHandle;
    readonly IntPtr subscriptionHandle;
    readonly IntPtr messageBuffer;
    bool disposed;

    internal IntPtr Handle => disposed
        ? throw new ObjectDisposedException(ToString())
        : subscriptionHandle;

    public string Topic { get; }
    public string TopicType { get; }
    
    public RclSubscriber(IntPtr contextHandle, IntPtr nodeHandle, string topic, string topicType, in QosProfile profile)
    {
        if (contextHandle == IntPtr.Zero) BuiltIns.ThrowArgumentNull(nameof(nodeHandle));
        if (nodeHandle == IntPtr.Zero) BuiltIns.ThrowArgumentNull(nameof(nodeHandle));
        if (topic == null) BuiltIns.ThrowArgumentNull(nameof(topic));
        if (topicType == null) BuiltIns.ThrowArgumentNull(nameof(topicType));

        this.contextHandle = contextHandle;
        this.nodeHandle = nodeHandle;
        Topic = topic;
        TopicType = topicType;

        int ret = Rcl.CreateSubscriptionHandle(out subscriptionHandle, nodeHandle, topic, topicType, profile);
        if (ret == -1)
        {
            throw new RosUnsupportedMessageException(topicType);
        }

        if (ret != Rcl.Ok)
        {
            throw new RosRclException($"Subscription for topic '{topic}' [{topicType}] failed!", ret);
        }

        Check(Rcl.CreateSerializedMessage(out messageBuffer));
    }

    void Check(int result) => Rcl.Check(contextHandle, result);

    public bool TryTakeMessage(out ReadOnlySpan<byte> span, out Guid guid)
    {
        int ret = Rcl.TakeSerializedMessage(Handle, messageBuffer, out IntPtr ptr, out int length, out guid);

        switch ((RclRet)ret)
        {
            case RclRet.Ok:
                const int headerSize = 4;
                span = Rcl.CreateSpan<byte>(ptr, length)[headerSize..];
                return true;
            case RclRet.SubscriptionTakeFailed:
                span = default;
                return false;
            default:
                Logger.LogErrorFormat("{0}: {1} failed!", this, nameof(Rcl.TakeSerializedMessage));
                span = default;
                return false;
        }
    }
    
    public int GetNumPublishers()
    {
        if (Rcl.GetPublisherCount(Handle, out int count) == Rcl.Ok)
        {
            return count;
        }

        Logger.LogErrorFormat("{0}: {1} failed!", this, nameof(Rcl.GetPublisherCount));
        return 0;
    }
    
    public void Dispose()
    {
        if (disposed) return;
        disposed = true;
        GC.SuppressFinalize(this);
        Rcl.DestroySerializedMessage(messageBuffer);
        Rcl.DestroySubscriptionHandle(subscriptionHandle, nodeHandle);
    }

    public override string ToString() => $"[{nameof(RclSubscriber)} {Topic} [{TopicType}] ]";

    ~RclSubscriber() => Logger.LogErrorFormat("{0} has not been disposed!", this);
}