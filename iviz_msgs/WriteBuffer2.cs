using System;
using System.Runtime.CompilerServices;
using Iviz.Tools;

namespace Iviz.Msgs;

/// <summary>
/// Contains utilities to serialize ROS 2/CDR messages into a byte array. 
/// </summary>
public unsafe struct WriteBuffer2
{
    byte* cursor;
    readonly byte* end;

    public WriteBuffer2(byte* ptr, int length)
    {
        cursor = ptr;
        end = ptr + length;
    }

    public WriteBuffer2(IntPtr ptr, int length) : this((byte*)ptr, length)
    {
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
    public void Align2() => cursor = (byte*)(((nint)cursor + 1) & ~1);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Align4() => cursor = (byte*)(((nint)cursor + 3) & ~3);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Align8() => cursor = (byte*)(((nint)cursor + 7) & ~7);


    #region advance

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Align2(int c) => (c + 1) & ~1;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Align4(int c) => (c + 3) & ~3;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Align8(int c) => (c + 7) & ~7;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int AddLength(int c, IMessage[] array) // used by tests
    {
        int d = Align4(c) + sizeof(int);
        foreach (var message in array)
        {
            d = message.AddRos2MessageLength(d);
        }

        return d;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int AddLength(int c, string? s)
    {
        int d = c + (sizeof(int) + 1); // trailing '\0'
        return s is not { Length: not 0 }
            ? d
            : d + BuiltIns.GetByteCount(s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int AddLength(int c, string[] bs)
    {
        int d = c + sizeof(int);
        foreach (string b in bs)
        {
            d = Align4(d);
            d = AddLength(d, b);
        }

        return d;
    }

    #endregion

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
            ThrowIfOutOfRange(5);
            WriteInt(1);
            *cursor = 0;
            Advance(1);
            return;
        }

        fixed (char* valPtr = val)
        {
            if (BuiltIns.CanWriteStringSimple(valPtr, length))
            {
                int lengthPlus1 = length + 1;
                ThrowIfOutOfRange(4 + lengthPlus1);
                WriteInt(lengthPlus1);
                BuiltIns.WriteStringSimple(valPtr, cursor, length);
                cursor[length] = 0;
                Advance(lengthPlus1);
            }
            else
            {
                int byteCount = BuiltIns.UTF8.GetByteCount(val);
                int byteCountPlus1 = byteCount + 1;
                ThrowIfOutOfRange(4 + byteCountPlus1);
                WriteInt(byteCountPlus1);
                BuiltIns.UTF8.GetBytes(valPtr, val.Length, cursor, byteCount);
                cursor[byteCount] = 0;
                Advance(byteCountPlus1);
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

    /// <summary>
    /// Serializes the given message into the buffer array.
    /// </summary>
    /// <param name="message">The ROS message.</param>
    /// <param name="buffer">The destination byte array.</param>
    /// <returns>The number of bytes written.</returns>
    public static void Serialize<T>(in T message, Span<byte> buffer) where T : ISerializable
    {
        fixed (byte* bufferPtr = buffer)
        {
            var b = new WriteBuffer2(bufferPtr, buffer.Length);
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