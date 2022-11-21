using System;
using System.Runtime.CompilerServices;
using Iviz.Tools;

namespace Iviz.Msgs;

/// <summary>
/// Contains utilities to serialize ROS messages into a byte array. 
/// </summary>
public unsafe struct WriteBuffer
{
    byte* cursor;
    readonly byte* end;

    public WriteBuffer(byte* ptr, int length)
    {
        cursor = ptr;
        end = ptr + length;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void Advance(int value) => cursor += value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    readonly void ThrowIfOutOfRange(int off)
    {
        if ((nuint)off > (nuint)end - (nuint)cursor)
        {
            BuiltIns.ThrowBufferOverflow();
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
    public void Serialize(int val)
    {
        ThrowIfOutOfRange(sizeof(int));
        *(int*)cursor = val;
        Advance(sizeof(int));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Serialize<T>(in T val) where T : unmanaged
    {
        ThrowIfOutOfRange(sizeof(T));
        *(T*)cursor = val;
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
                BuiltIns.WriteStringSimple(valPtr, cursor, length);
                Advance(length);
            }
            else
            {
                int byteCount = BuiltIns.UTF8.GetByteCount(val);
                WriteInt(byteCount);
                ThrowIfOutOfRange(byteCount);
                BuiltIns.UTF8.GetBytes(valPtr, val.Length, cursor, byteCount);
                Advance(byteCount);
            }
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SerializeArray(string[] val, int count)
    {
        ThrowIfWrongSize(val, count);
        SerializeArray(val);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SerializeArray(string[] val)
    {
        WriteInt(val.Length);
        foreach (string str in val)
        {
            Serialize(str);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SerializeStructArray<T>(T[] val, int count) where T : unmanaged
    {
        ThrowIfWrongSize(val, count);
        SerializeStructArray(val);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SerializeStructArray<T>(T[] val) where T : unmanaged
    {
        int size = val.Length * sizeof(T);
        ThrowIfOutOfRange(size);

        fixed (T* valPtr = val)
        {
            Unsafe.CopyBlock(cursor, valPtr, (uint)size);
        }

        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SerializeStructArray(SharedRent val)
    {
        int size = val.Length;
        ThrowIfOutOfRange(size);

        fixed (byte* valPtr = val.Array)
        {
            Unsafe.CopyBlock(cursor, valPtr, (uint)size);
        }

        Advance(size);
    }

    /// Returns the size in bytes of a string array when serialized in ROS
    public static int GetArraySize(string[]? array)
    {
        if (array == null)
        {
            return 0;
        }

        int size = 4 * array.Length;
        foreach (string t in array)
        {
            size += BuiltIns.UTF8.GetByteCount(t);
        }

        return size;
    }

    /// Returns the size in bytes of a string when deserialized in ROS
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetStringSize(string? s)
    {
        return s is not { Length: not 0 } ? 0 : BuiltIns.GetByteCount(s);
    }

    /// <summary>
    /// Serializes the given message into the buffer array.
    /// </summary>
    /// <param name="message">The ROS message.</param>
    /// <param name="buffer">The destination byte array.</param>
    /// <returns>The number of bytes written.</returns>
    public static void Serialize<T>(in T message, Span<byte> buffer) where T : ISerializableRos1
    {
        fixed (byte* bufferPtr = buffer)
        {
            var b = new WriteBuffer(bufferPtr, buffer.Length);
            message.RosSerialize(ref b);
        }
    }

    public static void Serialize(Serializer serializer, IMessage message, Span<byte> buffer)
    {
        fixed (byte* bufferPtr = buffer)
        {
            var b = new WriteBuffer2(bufferPtr, buffer.Length);
            serializer.RosSerialize(message, ref b);
        }
    }
}