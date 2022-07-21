using System;
using Iviz.Msgs;
using Iviz.Tools;

namespace Iviz.Roslib2.Rcl;

internal sealed class RclPublisher : IDisposable
{
    readonly IntPtr contextHandle;
    readonly IntPtr nodeHandle;
    readonly IntPtr publisherHandle;
    readonly IntPtr messageBuffer;
    bool disposed;

    public string Topic { get; }
    public string TopicType { get; }
    
    public int NumSubscribers
    {
        get
        {
            if (Rcl.GetSubscriptionCount(publisherHandle, out int count) == Rcl.Ok)
            {
                return count;
            }
            
            Logger.LogErrorFormat("{0}: {1} failed!", this, nameof(Rcl.GetSubscriptionCount));
            return 0;
        }  
    } 


    public RclPublisher(IntPtr contextHandle, IntPtr nodeHandle, string topic, string topicType)
    {
        if (contextHandle == IntPtr.Zero) BuiltIns.ThrowArgumentNull(nameof(nodeHandle));
        if (nodeHandle == IntPtr.Zero) BuiltIns.ThrowArgumentNull(nameof(nodeHandle));
        if (topic == null) BuiltIns.ThrowArgumentNull(nameof(topic));
        if (topicType == null) BuiltIns.ThrowArgumentNull(nameof(topicType));

        this.contextHandle = contextHandle;
        this.nodeHandle = nodeHandle;
        Topic = topic;
        TopicType = topicType;

        int ret = Rcl.CreatePublisherHandle(out publisherHandle, nodeHandle, topic, topicType);
        if (ret == -1)
        {
            throw new Exception("Message type not implemented");
        }

        if (ret != Rcl.Ok)
        {
            throw new Exception("Subscription failed!");
        }

        Check(Rcl.CreateSerializedMessage(out messageBuffer));
    }

    void Check(int result) => Rcl.Check(contextHandle, result);

    public void Publish<T>(in T message) where T : IMessage
    {
        const int headerSize = 4;

        int messageLength = message.Ros2MessageLength;
        int serializedLength = messageLength + headerSize;

        Rcl.EnsureSerializedMessageSize(messageBuffer, serializedLength, out IntPtr ptr);

        unsafe
        {
            var span = new Span<byte>(ptr.ToPointer(), serializedLength)
            {
                [0] = 0,
                [1] = 1,
                [2] = 0,
                [3] = 0
            };
            
            WriteBuffer2.Serialize(message, span[headerSize..]);
        }

        Rcl.PublishSerializedMessage(publisherHandle, messageBuffer);
    }

    public void Dispose()
    {
        if (disposed) return;
        disposed = true;
        GC.SuppressFinalize(this);
        if (publisherHandle != IntPtr.Zero)
        {
            Rcl.DestroyPublisherHandle(publisherHandle, nodeHandle);
        }

        if (messageBuffer != IntPtr.Zero)
        {
            Rcl.DestroySerializedMessage(messageBuffer);
        }
    }

    public override string ToString()
    {
        return $"[{nameof(RclPublisher)} {Topic} [{TopicType}] ]";
    }

    ~RclPublisher() => Dispose();
}