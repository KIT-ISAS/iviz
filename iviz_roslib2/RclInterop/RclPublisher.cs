using System.Runtime.CompilerServices;
using Iviz.Msgs;
using Iviz.Tools;

namespace Iviz.Roslib2.RclInterop;

internal sealed class RclPublisher : IDisposable
{
    readonly IntPtr contextHandle;
    readonly IntPtr nodeHandle;
    readonly IntPtr publisherHandle;
    //readonly SerializedMessage messageBuffer;
    bool disposed;

    public string Topic { get; }
    public string TopicType { get; }
    public QosProfile Profile { get; }

    public RclPublisher(IntPtr contextHandle, IntPtr nodeHandle, string topic, string topicType, QosProfile profile)
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

        int ret = Rcl.Impl.CreatePublisherHandle(out publisherHandle, nodeHandle, topic, topicType, in profile.Profile);
        switch ((RclRet)ret)
        {
            case RclRet.Ok:
                //messageBuffer = new SerializedMessage();
                break;
            case RclRet.InvalidMsgType:
                throw new RosUnsupportedMessageException(topicType);
            default:
                throw new RosRclException($"Advertisement for topic '{topic}' [{topicType}] failed!", ret);
        }
    }

    void Check(int result) => Rcl.Check(contextHandle, result);

    /*
    public int PublishSerialized<T>(in T message) where T : IMessage
    {
        const int headerSize = 4;

        int messageLength = message.Ros2MessageLength;
        int serializedLength = messageLength + headerSize;

        var span = messageBuffer.Resize(serializedLength);

        const int header = 0x00000100; // CDR_LE { 0x00, 0x01}, serialization options { 0x00, 0x00 } 
        Unsafe.WriteUnaligned(ref span[0], header);

        WriteBuffer2.Serialize(message, span[headerSize..]);

        Check(Rcl.Impl.PublishSerializedMessage(publisherHandle, messageBuffer.Handle));

        return serializedLength;
    }
    */
    
    public void Publish(IntPtr messageContextHandler)
    {
        Check(Rcl.Impl.Publish(publisherHandle, messageContextHandler));
    }

    public int GetNumSubscribers()
    {
        if (Rcl.Impl.GetSubscriptionCount(publisherHandle, out int count) == (int)RclRet.Ok)
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
        Rcl.Impl.DestroyPublisherHandle(publisherHandle, nodeHandle);
        //messageBuffer.Dispose();
    }

    public override string ToString() => $"[{nameof(RclPublisher)} {Topic} [{TopicType}] ]";

    ~RclPublisher() => Logger.LogErrorFormat("{0} has not been disposed!", this);
}
