using Iviz.Msgs;

namespace Iviz.Roslib2.RclInterop;

internal interface IRclDeserializeHandler
{
    void DeserializeFrom(IntPtr ptr, int length);
}

internal sealed class RclDeserializeHandler<TMessage> : IRclDeserializeHandler where TMessage : IMessage
{
    readonly IDeserializable<TMessage> generator;
    public TMessage? message;
    public int messageLength;
    public bool paused;

    public RclDeserializeHandler(IDeserializable<TMessage> generator)
    {
        this.generator = generator;
        Reset();
    }

    public unsafe void DeserializeFrom(IntPtr ptr, int length)
    {
        const int headerSize = 4;
        int size = length - headerSize;
        messageLength = length;

        if (paused) return;
        
        var b = new ReadBuffer2((byte*)ptr + headerSize, size);
        message = generator.RosDeserialize(ref b);
    }

    public void Reset()
    {
        message = default;
        messageLength = -1;
        paused = false;
    }
}