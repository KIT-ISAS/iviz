using Iviz.Msgs;
using Iviz.Tools;

namespace Iviz.Roslib2.RclInterop;

internal sealed class RclSubscriber : IDisposable, IHasHandle
{
    readonly IntPtr contextHandle;
    readonly IntPtr nodeHandle;
    readonly IntPtr subscriptionHandle;
    //readonly SerializedMessage messageBuffer;
    bool disposed;

    public IntPtr Handle => disposed
        ? throw new ObjectDisposedException(ToString())
        : subscriptionHandle;

    public string Topic { get; }
    public string TopicType { get; }
    public QosProfile Profile { get; }

    public RclSubscriber(IntPtr contextHandle, IntPtr nodeHandle, string topic, string topicType, QosProfile profile)
    {
        if (contextHandle == IntPtr.Zero) BuiltIns.ThrowArgumentNull(nameof(nodeHandle));
        if (nodeHandle == IntPtr.Zero) BuiltIns.ThrowArgumentNull(nameof(nodeHandle));
        if (topic == null) BuiltIns.ThrowArgumentNull(nameof(topic));
        if (topicType == null) BuiltIns.ThrowArgumentNull(nameof(topicType));

        this.contextHandle = contextHandle;
        this.nodeHandle = nodeHandle;
        Topic = topic;
        TopicType = topicType;
        Profile = profile;

        int ret = Rcl.Impl.CreateSubscriptionHandle(out subscriptionHandle, nodeHandle, topic, topicType,
            in profile.Profile);

        switch ((RclRet)ret)
        {
            case RclRet.Ok:
                break;
            case RclRet.InvalidMsgType:
                throw new RosUnsupportedMessageException(topicType);
            default:
                throw new RosRclException($"Subscription for topic '{topic}' [{topicType}] failed!", ret);
        }


        /*
        messageBuffer = (RclRet)ret switch
        {
            RclRet.Ok => new SerializedMessage(),
            RclRet.InvalidMsgType => throw new RosUnsupportedMessageException(topicType),
            _ => throw new RosRclException($"Subscription for topic '{topic}' [{topicType}] failed!", ret)
        };
        */
    }

    void Check(int result) => Rcl.Check(contextHandle, result);

    /*
    public bool TryTakeMessage(out Span<byte> span, out Guid guid, out bool moreRemaining)
    {
        int ret = Rcl.Impl.TakeSerializedMessage(Handle, messageBuffer.Handle, out IntPtr ptr, out int length, out guid,
            out byte moreRemainingByte);

        moreRemaining = moreRemainingByte != 0;

        switch ((RclRet)ret)
        {
            case RclRet.Ok:
                const int headerSize = 4;
                span = Rcl.CreateByteSpan(ptr + headerSize, length - headerSize);
                return true;
            case RclRet.SubscriptionTakeFailed:
                span = default;
                return false;
            default:
                Logger.LogErrorFormat("{0}: {1} failed!", this, nameof(Rcl.Impl.TakeSerializedMessage));
                span = default;
                return false;
        }
    }
    */

    public bool Take(IntPtr callbackContextHandle, out Guid guid)
    {
        int ret = Rcl.Impl.Take(subscriptionHandle, callbackContextHandle, out guid, out _);
        switch ((RclRet)ret)
        {
            case RclRet.Ok:
                return true;
            case RclRet.SubscriptionTakeFailed:
                return false;
            default:
                Logger.LogErrorFormat("{0}: + " + nameof(Rcl.Impl.TakeSerializedMessage) + " failed!", this);
                return false;
        }
    }

    public int GetNumPublishers()
    {
        if (Rcl.Impl.GetPublisherCount(Handle, out int count) == (int)RclRet.Ok)
        {
            return count;
        }

        Logger.LogErrorFormat("{0}: " + nameof(Rcl.Impl.GetPublisherCount) + " failed!", this);
        return 0;
    }

    public void Dispose()
    {
        if (disposed) return;
        disposed = true;
        GC.SuppressFinalize(this);
        //messageBuffer.Dispose();
        Rcl.Impl.DestroySubscriptionHandle(subscriptionHandle, nodeHandle);
    }

    public override string ToString() => $"[{nameof(RclSubscriber)} {Topic} [{TopicType}] ]";

    ~RclSubscriber() => Logger.LogErrorFormat("{0} has not been disposed!", this);
}