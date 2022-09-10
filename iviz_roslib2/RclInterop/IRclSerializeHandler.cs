using System.Runtime.CompilerServices;
using Iviz.Msgs;

namespace Iviz.Roslib2.RclInterop;

internal abstract class RclSerializeHandler
{
    public abstract void SerializeInto(IntPtr ptr, int length);
    public abstract int GetSerializedSize();
}

internal sealed class RclSerializeHandler<TMessage> : RclSerializeHandler where TMessage : IMessage
{
    readonly Serializer<TMessage> serializer;
    public TMessage? message;
    public int messageLength;

    public RclSerializeHandler(Serializer<TMessage> serializer)
    {
        this.serializer = serializer;
    }
    
    public override int GetSerializedSize() => serializer.Ros2MessageLength(message!);

    public override void SerializeInto(IntPtr ptr, int length)
    {
        const int header = 0x00000100; // CDR_LE { 0x00, 0x01}, serialization options { 0x00, 0x00 } 
        const int headerSize = 4;

        if (length < headerSize) BuiltIns.ThrowBufferOverflow(); 
        Unsafe.WriteUnaligned(ref Rcl.GetReference(ptr), header);

        int size = length - headerSize;
        
        var b = new WriteBuffer2(ptr + headerSize, size);
        serializer.RosSerialize(message!, ref b);
        
        messageLength = size;
    }
}