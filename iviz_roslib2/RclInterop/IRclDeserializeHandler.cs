using Iviz.Msgs;

namespace Iviz.Roslib2.RclInterop;

internal interface IRclDeserializeHandler
{
    void DeserializeFrom(IntPtr ptr, int length);
}

internal sealed class RclDeserializeHandler<TMessage> : IRclDeserializeHandler where TMessage : IMessage
{
    readonly IDeserializable<TMessage> generator;
    readonly Ros2Subscriber subscriber;
    public TMessage? message;
    public int messageLength;
    public bool hasMessage;

    public RclDeserializeHandler(Ros2Subscriber subscriber, IDeserializable<TMessage> generator)
    {
        this.subscriber = subscriber;
        this.generator = generator;
        Reset();
    }

    public unsafe void DeserializeFrom(IntPtr ptr, int length)
    {
        const int headerSize = 4;
        int size = length - headerSize;
        messageLength = length;

        if (subscriber.IsPaused) return;

        hasMessage = true;
        var b = new ReadBuffer2((byte*)ptr + headerSize, size);
        message = generator.RosDeserialize(ref b);
    }

    public void Reset()
    {
        message = default;
        messageLength = 0;
        hasMessage = false;
    }
}