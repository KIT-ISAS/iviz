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

    public void DeserializeFrom(IntPtr ptr, int length)
    {
        messageLength = length;

        if (paused)
        {
            return;
        }

        const int headerSize = 4;
        var buffer = Rcl.CreateReadOnlyByteSpan(ptr + headerSize, length - headerSize);
        message = ReadBuffer2.Deserialize(generator, buffer);
    }

    public void Reset()
    {
        message = default;
        messageLength = -1;
        paused = false;
    }
}