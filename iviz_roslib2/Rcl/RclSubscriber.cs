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

    public string Topic { get; }
    public string TopicType { get; }

    public int NumPublishers
    {
        get
        {
            if (Rcl.GetPublisherCount(subscriptionHandle, out int count) == Rcl.Ok)
            {
                return count;
            }

            Logger.LogErrorFormat("{0}: {1} failed!", this, nameof(Rcl.GetPublisherCount));
            return 0;
        }
    }

    public RclSubscriber(IntPtr contextHandle, IntPtr nodeHandle, string topic, string topicType)
    {
        if (contextHandle == IntPtr.Zero) BuiltIns.ThrowArgumentNull(nameof(nodeHandle));
        if (nodeHandle == IntPtr.Zero) BuiltIns.ThrowArgumentNull(nameof(nodeHandle));
        if (topic == null) BuiltIns.ThrowArgumentNull(nameof(topic));
        if (topicType == null) BuiltIns.ThrowArgumentNull(nameof(topicType));

        this.contextHandle = contextHandle;
        this.nodeHandle = nodeHandle;
        Topic = topic;
        TopicType = topicType;

        int ret = Rcl.CreateSubscriptionHandle(out subscriptionHandle, nodeHandle, topic, topicType);
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
    
    public void AddHandle(out IntPtr handle)
    {
        if (disposed)
        {
            throw new ObjectDisposedException(ToString());
        }

        handle = subscriptionHandle;
    }

    public bool TryTakeMessage(out ReadOnlySpan<byte> span, out Guid guid)
    {
        if (disposed) throw new ObjectDisposedException(ToString());

        int ret = Rcl.TakeSerializedMessage(subscriptionHandle, messageBuffer, out IntPtr ptr, out int length,
            out guid);

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