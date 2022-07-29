using Iviz.Msgs;
using Iviz.Tools;

namespace Iviz.Roslib2.Rcl;

internal sealed class RclPublisher : IDisposable
{
    readonly IntPtr contextHandle;
    readonly IntPtr nodeHandle;
    readonly IntPtr publisherHandle;
    readonly RclSerializedBuffer messageBuffer;
    bool disposed;

    public string Topic { get; }
    public string TopicType { get; }

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
        switch (ret)
        {
            case -1:
                throw new RosUnsupportedMessageException(topicType);
            case Rcl.Ok:
                messageBuffer = new RclSerializedBuffer();
                break;
            default:
                throw new RosRclException($"Advertisement for topic '{topic}' [{topicType}] failed!", ret);
        }
    }

    void Check(int result) => Rcl.Check(contextHandle, result);

    public int Publish<T>(in T message) where T : IMessage
    {
        const int headerSize = 4;

        const byte cdrLittleEndian0 = 0x00;
        const byte cdrLittleEndian1 = 0x01;
        const byte serializationOptions0 = 0x00;
        const byte serializationOptions1 = 0x00;

        int messageLength = message.Ros2MessageLength;
        int serializedLength = messageLength + headerSize;

        var span = messageBuffer.EnsureSize(serializedLength);
        
        span[0] = cdrLittleEndian0;
        span[1] = cdrLittleEndian1;
        span[2] = serializationOptions0;
        span[3] = serializationOptions1;

        WriteBuffer2.Serialize(message, span[headerSize..]);

        Rcl.PublishSerializedMessage(publisherHandle, messageBuffer.Handle);

        return serializedLength;
    }

    public int GetNumSubscribers()
    {
        if (Rcl.GetSubscriptionCount(publisherHandle, out int count) == Rcl.Ok)
        {
            return count;
        }

        Logger.LogErrorFormat("{0}: " + nameof(GetNumSubscribers) + " failed!", this);
        return 0;
    }

    public void Dispose()
    {
        if (disposed) return;
        disposed = true;
        GC.SuppressFinalize(this);
        Rcl.DestroyPublisherHandle(publisherHandle, nodeHandle);
        messageBuffer.Dispose();
    }

    public override string ToString() => $"[{nameof(RclPublisher)} {Topic} [{TopicType}] ]";

    ~RclPublisher() => Logger.LogErrorFormat("{0} has not been disposed!", this);
}