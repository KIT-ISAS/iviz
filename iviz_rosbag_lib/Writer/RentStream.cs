using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.Tools;

namespace Iviz.Rosbag.Writer;

internal struct RentStream : IDisposable
{
    readonly Rent<byte> bytes;
    int p;

    public RentStream(int size) => (bytes, p) = (new Rent<byte>(size), 0);

    public void Dispose()
    {
        bytes.Dispose();
    }

    readonly void ThrowIfOutOfRange(int off)
    {
        int remaining = bytes.Length - off;
        if (off > remaining)
        {
            BuiltIns.ThrowBufferOverflow(off, remaining);
        }
    }        
        
    void WriteValue<T>(T t)
    {
        ThrowIfOutOfRange(Unsafe.SizeOf<T>());
        ref byte dstPtr = ref bytes.Array[p];
        Unsafe.WriteUnaligned(ref dstPtr, t);
        p += Unsafe.SizeOf<T>();
    }

    public void Write(int value)
    {
        WriteValue(value);
    }

    public void Write(in time value)
    {
        WriteValue(value);
    }

    public void Write(OpCode value)
    {
        WriteValue((byte)value);
    }

    public void Write(long value)
    {
        WriteValue(value);
    }

    public void Write(string value)
    {
        byte[] array = bytes.Array;
        foreach (char t in value)
        {
            array[p++] = (byte)t;
        }
    }

    public void WriteUtf8(string value)
    {
        byte[] array = bytes.Array;
        int length = BuiltIns.UTF8.GetByteCount(value);
        BuiltIns.UTF8.GetBytes(value, 0, value.Length, array, p);
        p += length;
    }

    public void Write(char value)
    {
        WriteValue((byte)value);
    }

    public readonly Stream WriteTo(Stream stream)
    {
        stream.Write(bytes);
        return stream;
    }

    public readonly ValueTask WriteToAsync(Stream stream)
    {
        return stream.WriteAsync(bytes);
    }
}