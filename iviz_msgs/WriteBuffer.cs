using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Tools;

namespace Iviz.Msgs;

/// <summary>
/// Contains utilities to serialize ROS messages into a byte array. 
/// </summary>
public unsafe ref struct WriteBuffer
{
    readonly byte* ptr;
    int offset;
    int remaining;

    WriteBuffer(byte* ptr, int length)
    {
        this.ptr = ptr;
        offset = 0;
        remaining = length;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void Advance(int value)
    {
        offset += value;
        remaining -= value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    readonly void ThrowIfOutOfRange(int off)
    {
        if ((uint)off > (uint)remaining)
        {
            BuiltIns.ThrowBufferOverflow(off);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static void ThrowIfWrongSize(Array array, int size)
    {
        if (array is null)
        {
            BuiltIns.ThrowArgumentNull(nameof(array));
        }

        if (array.Length != size)
        {
            BuiltIns.ThrowInvalidSizeForFixedArray(array.Length, size);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Serialize<T>(in T val) where T : unmanaged
    {
        ThrowIfOutOfRange(sizeof(T));
        *(T*)(ptr + offset) = val;
        Advance(sizeof(T));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void WriteInt(int value) => Serialize(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Serialize(string val)
    {
        int length = val.Length;
        if (length == 0)
        {
            WriteInt(0);
            return;
        }

        fixed (char* valPtr = val)
        {
            if (BuiltIns.CanWriteStringSimple(valPtr, length))
            {
                WriteInt(length);
                ThrowIfOutOfRange(length);
                BuiltIns.WriteStringSimple(valPtr, ptr + offset, length);
                Advance(length);
            }
            else
            {
                int byteCount = BuiltIns.UTF8.GetByteCount(val);
                WriteInt(byteCount);
                ThrowIfOutOfRange(byteCount);
                BuiltIns.UTF8.GetBytes(valPtr, val.Length, ptr + offset, remaining);
                Advance(byteCount);
            }
        }
    }

    public void SerializeArray(string[] val)
    {
        WriteInt(val.Length);
        foreach (string str in val)
        {
            Serialize(str);
        }
    }

    public void SerializeArray(string[] val, int count)
    {
        ThrowIfWrongSize(val, count);
        foreach (string str in val)
        {
            Serialize(str);
        }
    }
    
    public void SerializeStructArray(byte[] val)
    {
        const int sizeOfT = sizeof(byte);
        int size = val.Length * sizeOfT;
        ThrowIfOutOfRange(4 + size);

        WriteInt(val.Length);
        fixed (byte* valPtr = val)
        {
            Unsafe.CopyBlock(ptr + offset, valPtr, (uint)size);
        }

        Advance(size);
    }
    
    public void SerializeStructArray(Point32[] val)
    {
        const int sizeOfT = Point32.RosFixedMessageLength;
        int size = val.Length * sizeOfT;
        ThrowIfOutOfRange(4 + size);

        WriteInt(val.Length);
        fixed (Point32* valPtr = val)
        {
            Unsafe.CopyBlock(ptr + offset, valPtr, (uint)size);
        }

        Advance(size);
    }

    public void SerializeStructArray<T>(T[] val) where T : unmanaged
    {
        int sizeOfT = Unsafe.SizeOf<T>();
        int size = val.Length * sizeOfT;
        ThrowIfOutOfRange(4 + size);

        WriteInt(val.Length);
        fixed (T* valPtr = val)
        {
            Unsafe.CopyBlock(ptr + offset, valPtr, (uint)size);
        }

        Advance(size);
    }

    public void SerializeStructArray(SharedRent val)
    {
        const int sizeOfT = sizeof(byte);
        int size = val.Length * sizeOfT;
        ThrowIfOutOfRange(4 + size);

        WriteInt(val.Length);
        fixed (byte* valPtr = val.Array)
        {
            Unsafe.CopyBlock(ptr + offset, valPtr, (uint)size);
        }

        Advance(size);
    }

    public void SerializeStructArray<T>(T[] val, int count) where T : unmanaged
    {
        ThrowIfWrongSize(val, count);
        int size = val.Length * sizeof(T);
        ThrowIfOutOfRange(size);

        fixed (T* valPtr = val)
        {
            Unsafe.CopyBlock(ptr + offset, valPtr, (uint)size);
        }

        Advance(size);
    }
    
    public void SerializeArray(TransformStamped[] val)
    {
        WriteInt(val.Length);
        for (int i = 0; i < val.Length; i++)
        {
            val[i].RosSerialize(ref this);
        }
    }

    public void SerializeArray(IMessage[] val)
    {
        WriteInt(val.Length);
        for (int i = 0; i < val.Length; i++)
        {
            val[i].RosSerialize(ref this);
        }
    }

    public void SerializeArray(IMessage[] val, int count)
    {
        ThrowIfWrongSize(val, count);
        for (int i = 0; i < count; i++)
        {
            val[i].RosSerialize(ref this);
        }
    }
    
    public void SerializeArrayGeneric<T>(T[] val) where T : IMessage
    {
        WriteInt(val.Length);
        for (int i = 0; i < val.Length; i++)
        {
            val[i].RosSerialize(ref this);
        }
    }
    
    public void SerializeArrayGeneric<T>(T[] val, int count) where T : IMessage
    {
        ThrowIfWrongSize(val, count);
        for (int i = 0; i < count; i++)
        {
            val[i].RosSerialize(ref this);
        }
    }
        
    public static int GetArraySize(TransformStamped[] array)
    {
        int size = 0;
        for (int i = 0; i < array.Length; i++)
        {
            size += array[i].RosMessageLength;
        }

        return size;
    }

    /// Returns the size in bytes of a message array when serialized in ROS
    public static int GetArraySize(IMessage[] array)
    {
        int size = 0;
        for (int i = 0; i < array.Length; i++)
        {
            size += array[i].RosMessageLength;
        }

        return size;
    }

    /// Returns the size in bytes of a string array when serialized in ROS
    public static int GetArraySize(string[]? array)
    {
        if (array == null)
        {
            return 0;
        }

        int size = 4 * array.Length;
        for (int i = 0; i < array.Length; i++)
        {
            size += BuiltIns.UTF8.GetByteCount(array[i]);
        }

        return size;
    }

    /// Returns the size in bytes of a string when deserialized in ROS
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetStringSize(string? s)
    {
        return s is not {Length: not 0} ? 0 : BuiltIns.GetByteCount(s);
    }        

    /// <summary>
    /// Serializes the given message into the buffer array.
    /// </summary>
    /// <param name="message">The ROS message.</param>
    /// <param name="buffer">The destination byte array.</param>
    /// <returns>The number of bytes written.</returns>
    public static uint Serialize<T>(in T message, Span<byte> buffer) where T : ISerializableRos1
    {
        fixed (byte* bufferPtr = buffer)
        {
            var b = new WriteBuffer(bufferPtr, buffer.Length);
            message.RosSerialize(ref b);

            int oldLength = buffer.Length;
            int newLength = b.offset;

            return (uint)(oldLength - newLength);
        }
    }
}