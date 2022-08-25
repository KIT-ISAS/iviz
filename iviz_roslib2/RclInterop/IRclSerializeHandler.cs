using System.Runtime.CompilerServices;
using Iviz.Msgs;

namespace Iviz.Roslib2.RclInterop;

internal interface IRclSerializeHandler
{
    void SerializeInto(IntPtr ptr, int length);
    int GetSerializedSize();
}

internal sealed class RclSerializeHandler<TMessage> : IRclSerializeHandler where TMessage : IMessage 
{
    public TMessage? message;
    public int messageLength;

    public int GetSerializedSize() => message!.Ros2MessageLength;

    public unsafe void SerializeInto(IntPtr ptr, int length)
    {
        const int header = 0x00000100; // CDR_LE { 0x00, 0x01}, serialization options { 0x00, 0x00 } 
        const int headerSize = 4;

        if (length < headerSize) BuiltIns.ThrowBufferOverflow(); 
        Unsafe.WriteUnaligned(ref Rcl.GetReference(ptr), header);

        int size = length - headerSize;
        
        var b = new WriteBuffer2((byte*)ptr + headerSize, size);
        message!.RosSerialize(ref b);

        
        messageLength = size;
    }
}