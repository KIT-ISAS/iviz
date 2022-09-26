using System;
using System.Runtime.CompilerServices;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.IvizMsgs;
using Iviz.Msgs.StdMsgs;
using Iviz.Tools;

namespace Iviz.Msgs;

/// <summary>
/// Contains utilities to serialize ROS 2/CDR messages into a byte array. 
/// </summary>
public unsafe partial struct WriteBuffer2
{
    readonly byte* ptr;
    int offset;
    int remaining;

    public WriteBuffer2(byte* ptr, int length)
    {
        this.ptr = ptr;
        offset = 0;
        remaining = length;
    }

    public WriteBuffer2(IntPtr ptr, int length) : this((byte*)ptr, length)
    {
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
            BuiltIns.ThrowBufferOverflow(off, remaining);
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
    void Align2() => Advance((-offset) & 1);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void Align4() => Advance((-offset) & 3);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void Align8() => Advance((-offset) & 7);


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

    #region scalars

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Serialize(bool val)
    {
        const int size = sizeof(bool);
        ThrowIfOutOfRange(size);
        *(bool*)(ptr + offset) = val;
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Serialize(byte val)
    {
        const int size = sizeof(byte);
        ThrowIfOutOfRange(size);
        *(ptr + offset) = val;
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Serialize(sbyte val)
    {
        const int size = sizeof(sbyte);
        ThrowIfOutOfRange(size);
        *(sbyte*)(ptr + offset) = val;
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Serialize(short val)
    {
        Align2();
        const int size = sizeof(short);
        ThrowIfOutOfRange(size);
        *(short*)(ptr + offset) = val;
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Serialize(ushort val)
    {
        Align2();
        const int size = sizeof(ushort);
        ThrowIfOutOfRange(size);
        *(ushort*)(ptr + offset) = val;
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Serialize(in time val)
    {
        Align4();
        const int size = 2 * sizeof(uint);
        ThrowIfOutOfRange(size);
        *(time*)(ptr + offset) = val;
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Serialize(in duration val)
    {
        Align4();
        const int size = 2 * sizeof(int);
        ThrowIfOutOfRange(size);
        *(duration*)(ptr + offset) = val;
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Serialize(int val)
    {
        Align4();
        const int size = sizeof(int);
        ThrowIfOutOfRange(size);
        *(int*)(ptr + offset) = val;
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Serialize(uint val)
    {
        Align4();
        const int size = sizeof(int);
        ThrowIfOutOfRange(size);
        *(uint*)(ptr + offset) = val;
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Serialize(float val)
    {
        Align4();
        const int size = sizeof(float);
        ThrowIfOutOfRange(size);
        *(float*)(ptr + offset) = val;
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Serialize(double val)
    {
        Align8();
        const int size = sizeof(double);
        ThrowIfOutOfRange(size);
        *(double*)(ptr + offset) = val;
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Serialize(long val)
    {
        Align8();
        const int size = sizeof(long);
        ThrowIfOutOfRange(size);
        *(long*)(ptr + offset) = val;
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Serialize(ulong val)
    {
        Align8();
        const int size = sizeof(ulong);
        ThrowIfOutOfRange(size);
        *(ulong*)(ptr + offset) = val;
        Advance(size);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Serialize<T>(in T val) where T : unmanaged
    {
        ThrowIfOutOfRange(sizeof(T));
        *(T*)(ptr + offset) = val;
        Advance(sizeof(T));
    }

    #endregion

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
            ptr[offset] = 0;
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
                BuiltIns.WriteStringSimple(valPtr, ptr + offset, length);
                ptr[offset + length] = 0;
                Advance(lengthPlus1);
            }
            else
            {
                int byteCount = BuiltIns.UTF8.GetByteCount(val);
                int byteCountPlus1 = byteCount + 1;
                ThrowIfOutOfRange(4 + byteCountPlus1);
                WriteInt(byteCountPlus1);
                BuiltIns.UTF8.GetBytes(valPtr, val.Length, ptr + offset, remaining);
                ptr[offset + byteCount] = 0;
                Advance(byteCountPlus1);
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

    #region arrays

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SerializeStructArray(bool[] val)
    {
        int length = val.Length;
        WriteInt(length);
        if (length == 0) return;
        SerializeStructArrayCore(val);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SerializeStructArray(bool[] val, int count)
    {
        ThrowIfWrongSize(val, count);
        SerializeStructArrayCore(val);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SerializeStructArray(byte[] val)
    {
        int length = val.Length;
        WriteInt(length);
        if (length == 0) return;
        SerializeStructArrayCore(val);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SerializeStructArray(byte[] val, int count)
    {
        ThrowIfWrongSize(val, count);
        SerializeStructArrayCore(val);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SerializeStructArray(sbyte[] val)
    {
        int length = val.Length;
        WriteInt(length);
        if (length == 0) return;
        SerializeStructArrayCore(val);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SerializeStructArray(sbyte[] val, int count)
    {
        ThrowIfWrongSize(val, count);
        SerializeStructArrayCore(val);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SerializeStructArray(short[] val)
    {
        int length = val.Length;
        WriteInt(length);
        if (length == 0) return;
        SerializeStructArrayCore(val);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SerializeStructArray(short[] val, int count)
    {
        ThrowIfWrongSize(val, count);
        Align2();
        SerializeStructArrayCore(val);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SerializeStructArray(ushort[] val)
    {
        int length = val.Length;
        WriteInt(length);
        if (length == 0) return;
        SerializeStructArrayCore(val);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SerializeStructArray(ushort[] val, int count)
    {
        ThrowIfWrongSize(val, count);
        Align2();
        SerializeStructArrayCore(val);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SerializeStructArray(int[] val)
    {
        int length = val.Length;
        WriteInt(length);
        if (length == 0) return;
        SerializeStructArrayCore(val);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SerializeStructArray(int[] val, int count)
    {
        ThrowIfWrongSize(val, count);
        Align4();
        SerializeStructArrayCore(val);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SerializeStructArray(uint[] val)
    {
        int length = val.Length;
        WriteInt(length);
        if (length == 0) return;
        SerializeStructArrayCore(val);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SerializeStructArray(uint[] val, int count)
    {
        ThrowIfWrongSize(val, count);
        Align4();
        SerializeStructArrayCore(val);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SerializeStructArray(float[] val)
    {
        int length = val.Length;
        WriteInt(length);
        if (length == 0) return;
        SerializeStructArrayCore(val);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SerializeStructArray(float[] val, int count)
    {
        ThrowIfWrongSize(val, count);
        Align4();
        SerializeStructArrayCore(val);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SerializeStructArray(double[] val)
    {
        int length = val.Length;
        WriteInt(length);
        if (length == 0) return;
        Align8();
        SerializeStructArrayCore(val);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SerializeStructArray(double[] val, int count)
    {
        ThrowIfWrongSize(val, count);
        Align8();
        SerializeStructArrayCore(val);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SerializeStructArray(long[] val)
    {
        int length = val.Length;
        WriteInt(length);
        if (length == 0) return;
        Align8();
        SerializeStructArrayCore(val);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SerializeStructArray(long[] val, int count)
    {
        ThrowIfWrongSize(val, count);
        Align8();
        SerializeStructArrayCore(val);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SerializeStructArray(ulong[] val)
    {
        int length = val.Length;
        WriteInt(length);
        if (length == 0) return;
        Align8();
        SerializeStructArrayCore(val);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SerializeStructArray(ulong[] val, int count)
    {
        ThrowIfWrongSize(val, count);
        Align8();
        SerializeStructArrayCore(val);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SerializeStructArray(Vector3[] val)
    {
        int length = val.Length;
        WriteInt(length);
        if (length == 0) return;
        Align8();
        SerializeStructArrayCore(val);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SerializeStructArray(Vector3[] val, int count)
    {
        ThrowIfWrongSize(val, count);
        Align8();
        SerializeStructArrayCore(val);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SerializeStructArray(Point[] val)
    {
        int length = val.Length;
        WriteInt(length);
        if (length == 0) return;
        Align8();
        SerializeStructArrayCore(val);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SerializeStructArray(time[] val)
    {
        int length = val.Length;
        WriteInt(length);
        if (length == 0) return;
        SerializeStructArrayCore(val);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SerializeStructArray(duration[] val)
    {
        int length = val.Length;
        WriteInt(length);
        if (length == 0) return;
        SerializeStructArrayCore(val);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SerializeStructArray(Transform[] val)
    {
        int length = val.Length;
        WriteInt(length);
        if (length == 0) return;
        Align8();
        SerializeStructArrayCore(val);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SerializeStructArray(Pose[] val)
    {
        int length = val.Length;
        WriteInt(length);
        if (length == 0) return;
        Align8();
        SerializeStructArrayCore(val);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SerializeStructArray(Quaternion[] val)
    {
        int length = val.Length;
        WriteInt(length);
        if (length == 0) return;
        Align8();
        SerializeStructArrayCore(val);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SerializeStructArray(Point32[] val)
    {
        int length = val.Length;
        WriteInt(length);
        if (length == 0) return;
        SerializeStructArrayCore(val);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SerializeStructArray(Color32[] val)
    {
        int length = val.Length;
        WriteInt(length);
        if (length == 0) return;
        SerializeStructArrayCore(val);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SerializeStructArray(Triangle[] val)
    {
        int length = val.Length;
        WriteInt(length);
        if (length == 0) return;
        SerializeStructArrayCore(val);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SerializeStructArray(ColorRGBA[] val)
    {
        int length = val.Length;
        WriteInt(length);
        if (length == 0) return;
        SerializeStructArrayCore(val);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SerializeStructArray<T>(T[] val) where T : unmanaged
    {
        int length = val.Length;
        WriteInt(length);
        if (length == 0) return;
        SerializeStructArrayCore(val);
    }

    #endregion

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void SerializeStructArrayCore<T>(T[] val) where T : unmanaged
    {
        int size = val.Length * sizeof(T);
        ThrowIfOutOfRange(size);

        fixed (T* valPtr = val)
        {
            Unsafe.CopyBlock(ptr + offset, valPtr, (uint)size);
        }

        Advance(size);
    }

    public void SerializeStructArray(SharedRent val)
    {
        const int sizeOfT = 1;
        int size = val.Length * sizeOfT;
        ThrowIfOutOfRange(4 + size);

        WriteInt(val.Length);
        fixed (byte* valPtr = val.Array)
        {
            Unsafe.CopyBlock(ptr + offset, valPtr, (uint)size);
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

public unsafe partial struct WriteBuffer2
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Serialize(in Vector3 val)
    {
        Align8();
        const int size = Vector3.Ros2FixedMessageLength;
        ThrowIfOutOfRange(size);
        *(Vector3*)(ptr + offset) = val;
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Serialize(in Point val)
    {
        Align8();
        const int size = Point.Ros2FixedMessageLength;
        ThrowIfOutOfRange(size);
        *(Point*)(ptr + offset) = val;
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Serialize(in Quaternion val)
    {
        Align8();
        const int size = Quaternion.Ros2FixedMessageLength;
        ThrowIfOutOfRange(size);
        *(Quaternion*)(ptr + offset) = val;
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Serialize(in Transform val)
    {
        Align8();
        const int size = Transform.Ros2FixedMessageLength;
        ThrowIfOutOfRange(size);
        *(Transform*)(ptr + offset) = val;
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Serialize(in Pose val)
    {
        Align8();
        const int size = Transform.Ros2FixedMessageLength;
        ThrowIfOutOfRange(size);
        *(Pose*)(ptr + offset) = val;
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Serialize(in ColorRGBA val)
    {
        Align4();
        const int size = ColorRGBA.Ros2FixedMessageLength;
        ThrowIfOutOfRange(size);
        *(ColorRGBA*)(ptr + offset) = val;
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Serialize(in Point32 val)
    {
        Align4();
        const int size = Point32.Ros2FixedMessageLength;
        ThrowIfOutOfRange(size);
        *(Point32*)(ptr + offset) = val;
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Serialize(in Color32 val)
    {
        const int size = Color32.Ros2FixedMessageLength;
        ThrowIfOutOfRange(size);
        *(Color32*)(ptr + offset) = val;
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Serialize(in Triangle val)
    {
        Align4();
        const int size = Triangle.Ros2FixedMessageLength;
        ThrowIfOutOfRange(size);
        *(Triangle*)(ptr + offset) = val;
        Advance(size);
    }
}