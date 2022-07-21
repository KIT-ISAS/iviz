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
    readonly IntPtr waitSet;
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
            throw new Exception("Message type not implemented");
        }

        if (ret != Rcl.Ok)
        {
            throw new Exception("Subscription failed!");
        }

        Check(Rcl.CreateSerializedMessage(out messageBuffer));

        waitSet = Rcl.CreateWaitSet();
        Check(Rcl.WaitSetInit(contextHandle, waitSet, 1, 0, 0, 0, 0, 0));
    }

    void Check(int result) => Rcl.Check(contextHandle, result);

    public bool TryTakeMessage(int timeoutInMs, out ReadOnlySpan<byte> span, out Guid guid)
    {
        Check(Rcl.WaitSetClear(waitSet));
        Check(Rcl.WaitSetAddSubscription(waitSet, subscriptionHandle));

        if (Rcl.Wait(waitSet, timeoutInMs) != Rcl.Ok)
        {
            span = default;
            guid = default;
            return false;
        }

        if (Rcl.TakeSerializedMessage(subscriptionHandle, messageBuffer, out IntPtr ptr, out int length, out guid) !=
            Rcl.Ok)
        {
            Logger.LogErrorFormat("{0}: {1} failed!", this, nameof(Rcl.TakeSerializedMessage));
            span = default;
            return false;
        }

        unsafe
        {
            const int headerSize = 4;
            span = new ReadOnlySpan<byte>(ptr.ToPointer(), length)[headerSize..];
            return true;
        }
    }

    public void Dispose()
    {
        if (disposed) return;
        disposed = true;
        GC.SuppressFinalize(this);
        if (subscriptionHandle != IntPtr.Zero)
        {
            Rcl.DestroySubscriptionHandle(subscriptionHandle, nodeHandle);
        }

        if (messageBuffer != IntPtr.Zero)
        {
            Rcl.DestroySerializedMessage(messageBuffer);
        }

        if (waitSet != IntPtr.Zero)
        {
            Rcl.DestroyWaitSet(waitSet);
        }
    }

    public override string ToString()
    {
        return $"[{nameof(RclSubscriber)} {Topic} [{TopicType}] ]";
    }

    ~RclSubscriber() => Dispose();
}