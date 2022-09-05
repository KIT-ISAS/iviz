using Iviz.Msgs;

namespace Iviz.Roslib2.RclInterop;

internal abstract class RclDeserializeHandler
{
    public abstract void DeserializeFrom(IntPtr ptr, int length);
}

internal sealed class RclDeserializeHandler<TMessage> : RclDeserializeHandler where TMessage : IMessage
{
    readonly Deserializer<TMessage> deserializer;
    readonly Ros2Subscriber subscriber;
    public TMessage? message;
    public int messageLength;
    public bool hasMessage;

    public RclDeserializeHandler(Ros2Subscriber subscriber, Deserializer<TMessage> deserializer)
    {
        this.subscriber = subscriber;
        this.deserializer = deserializer;
        Clear();
    }

    public override unsafe void DeserializeFrom(IntPtr ptr, int length)
    {
        const int headerSize = 4;
        int size = length - headerSize;
        messageLength = length;

        if (subscriber.IsPaused) return;

        var b = new ReadBuffer2((byte*)ptr + headerSize, size);
        deserializer.RosDeserialize(ref b, out message);
        hasMessage = true;
    }

    public void Clear()
    {
        message = default;
        messageLength = 0;
        hasMessage = false;
    }
}