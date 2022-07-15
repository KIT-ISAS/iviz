using System;
using System.Runtime.CompilerServices;
using Iviz.Msgs2.GeometryMsgs;
using Iviz.Msgs2.StdMsgs;
using Iviz.Msgs2.GeometryMsgs;
using Iviz.Tools;

namespace Iviz.Msgs;

/// <summary>
/// Contains utilities to (de)serialize ROS messages from a byte array. 
/// </summary>
public unsafe partial struct WriteBuffer2
{
    readonly byte* ptr;
    int offset;
    int remaining;

    WriteBuffer2(byte* ptr, int length)
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
        if (off < 0 || off > remaining)
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
    void Align2() => Advance(((int.MaxValue - 1) - offset) & 1);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void Align4() => Advance(((int.MaxValue - 3) - offset) & 3);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void Align8() => Advance(((int.MaxValue - 7) - offset) & 7);


    #region advance

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static int DoAlign2(int c) => (c + 1) & ~1;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static int DoAlign4(int c) => (c + 3) & ~3;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static int DoAlign8(int c) => (c + 7) & ~7;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Advance(ref int c, bool _) => AdvanceAlign1(ref c);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Advance(ref int c, sbyte _) => AdvanceAlign1(ref c);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Advance(ref int c, byte _) => AdvanceAlign1(ref c);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Advance(ref int c, short _) => AdvanceAlign2(ref c);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Advance(ref int c, ushort _) => AdvanceAlign2(ref c);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Advance(ref int c, int _) => AdvanceAlign4(ref c);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Advance(ref int c, uint _) => AdvanceAlign4(ref c);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Advance(ref int c, float _) => AdvanceAlign4(ref c);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Advance(ref int c, double _) => AdvanceAlign8(ref c);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Advance(ref int c, long _) => AdvanceAlign8(ref c);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Advance(ref int c, ulong _) => AdvanceAlign8(ref c);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Advance(ref int c, in time _) => AdvanceAlign4T(ref c, 2 * sizeof(int));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Advance(ref int c, in duration _) => AdvanceAlign4T(ref c, 2 * sizeof(int));


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Advance(ref int c, byte[] b) => AdvanceAlign1(ref c, b.Length);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Advance(ref int c, byte[] _, int l) => AdvanceAlign1(ref c, l);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Advance(ref int c, sbyte[] b) => AdvanceAlign1(ref c, b.Length);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Advance(ref int c, sbyte[] _, int l) => AdvanceAlign1(ref c, l);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Advance(ref int c, bool[] b) => AdvanceAlign1(ref c, b.Length);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Advance(ref int c, short[] b) => AdvanceAlign2(ref c, b.Length);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Advance(ref int c, short[] _, int l) => AdvanceAlign2(ref c, l);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Advance(ref int c, ushort[] b) => AdvanceAlign2(ref c, b.Length);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Advance(ref int c, ushort[] _, int l) => AdvanceAlign2(ref c, l);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Advance(ref int c, int[] b) => AdvanceAlign4(ref c, b.Length);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Advance(ref int c, int[] _, int l) => AdvanceAlign4(ref c, l);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Advance(ref int c, uint[] b) => AdvanceAlign4(ref c, b.Length);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Advance(ref int c, uint[] _, int l) => AdvanceAlign4(ref c, l);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Advance(ref int c, float[] b) => AdvanceAlign4(ref c, b.Length);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Advance(ref int c, float[] _, int l) => AdvanceAlign4(ref c, l);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Advance(ref int c, double[] b) => AdvanceAlign8(ref c, b.Length);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Advance(ref int c, double[] _, int l) => AdvanceAlign8(ref c, l);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Advance(ref int c, long[] b) => AdvanceAlign8(ref c, b.Length);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Advance(ref int c, long[] _, int l) => AdvanceAlign8(ref c, l);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Advance(ref int c, ulong[] b) => AdvanceAlign8(ref c, b.Length);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Advance(ref int c, ulong[] _, int l) => AdvanceAlign8(ref c, l);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static void AdvanceAlign1(ref int c) => c++;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static void AdvanceAlign1(ref int c, int length)
    {
        c = DoAlign4(c) + 4 + length;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static void AdvanceAlign2(ref int c) => c = DoAlign2(c) + 2;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static void AdvanceAlign2(ref int c, int length)
    {
        c = DoAlign4(c) + 4 + 2 * length;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static void AdvanceAlign4(ref int c) => c = DoAlign4(c) + 4;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static void AdvanceAlign4(ref int c, int length)
    {
        c = DoAlign4(c) + 4 + 4 * length;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static void AdvanceAlign4T(ref int c, int size)
    {
        c = DoAlign4(c) + size;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static void AdvanceAlign4Ts(ref int c, int length) => AdvanceAlign4(ref c, length);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static void AdvanceAlign8(ref int c) => c = DoAlign8(c) + 8;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static void AdvanceAlign8(ref int c, int length)
    {
        c = DoAlign4(c) + 4;
        c = DoAlign8(c) + 8 * length;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static void AdvanceAlign8T(ref int c, int length)
    {
        c = DoAlign8(c) + length;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static void AdvanceAlign8Ts(ref int c, int length)
    {
        c = DoAlign4(c) + 4;
        c = DoAlign8(c) + length;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Advance(ref int c, IMessageRos2[] array)
    {
        c = DoAlign4(c) + sizeof(int);
        foreach (var message in array)
        {
            message.GetRosMessageLength(ref c);
        }
    }

    public static void Advance(ref int c, string s)
    {
        c = DoAlign4(c) + sizeof(int);
        int length = BuiltIns.UTF8.GetByteCount(s);
        if (length != 0)
        {
            c += length + 1;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Advance(ref int c, string[] bs)
    {
        c = DoAlign4(c) + sizeof(int);
        foreach (string b in bs)
        {
            Advance(ref c, b);
        }
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
        const int size = 2 * sizeof(int);
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

    #endregion

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void WriteInt(int value) => Serialize(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Serialize(string val)
    {
        if (val.Length == 0)
        {
            WriteInt(0);
            return;
        }

        int count = BuiltIns.UTF8.GetByteCount(val);
        ThrowIfOutOfRange(4 + count + 1);
        WriteInt(count);
        fixed (char* valPtr = val)
        {
            BuiltIns.UTF8.GetBytes(valPtr, val.Length, ptr, remaining);
        }

        ptr[offset + count] = 0;
        Advance(count + 1);
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
        WriteInt(val.Length);
        if (val.Length == 0) return;
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
        WriteInt(val.Length);
        if (val.Length == 0) return;
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
        WriteInt(val.Length);
        if (val.Length == 0) return;
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
        WriteInt(val.Length);
        if (val.Length == 0) return;
        Align2();
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
        WriteInt(val.Length);
        if (val.Length == 0) return;
        Align2();
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
        WriteInt(val.Length);
        if (val.Length == 0) return;
        Align4();
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
        WriteInt(val.Length);
        if (val.Length == 0) return;
        Align4();
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
        WriteInt(val.Length);
        if (val.Length == 0) return;
        Align4();
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
        WriteInt(val.Length);
        if (val.Length == 0) return;
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
        WriteInt(val.Length);
        if (val.Length == 0) return;
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
        WriteInt(val.Length);
        if (val.Length == 0) return;
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
        WriteInt(val.Length);
        if (val.Length == 0) return;
        Align8();
        SerializeStructArrayCore(val);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SerializeStructArray(Point[] val)
    {
        WriteInt(val.Length);
        if (val.Length == 0) return;
        Align8();
        SerializeStructArrayCore(val);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SerializeStructArray(Transform[] val)
    {
        WriteInt(val.Length);
        if (val.Length == 0) return;
        Align8();
        SerializeStructArrayCore(val);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SerializeStructArray(Pose[] val)
    {
        WriteInt(val.Length);
        if (val.Length == 0) return;
        Align8();
        SerializeStructArrayCore(val);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SerializeStructArray(Quaternion[] val)
    {
        WriteInt(val.Length);
        if (val.Length == 0) return;
        Align8();
        SerializeStructArrayCore(val);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SerializeStructArray(Point32[] val)
    {
        WriteInt(val.Length);
        if (val.Length == 0) return;
        Align4();
        SerializeStructArrayCore(val);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SerializeStructArray(ColorRGBA[] val)
    {
        WriteInt(val.Length);
        if (val.Length == 0) return;
        Align4();
        SerializeStructArrayCore(val);
    }

    #endregion

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

    public void SerializeStructArray<T>(SharedRent<T> val) where T : unmanaged
    {
        int sizeOfT = Unsafe.SizeOf<T>();
        int size = val.Length * sizeOfT;
        ThrowIfOutOfRange(4 + size);

        WriteInt(val.Length);
        fixed (T* valPtr = val.Array)
        {
            Unsafe.CopyBlock(ptr + offset, valPtr, (uint)size);
        }

        Advance(size);
    }

    public void SerializeArray<T>(T[] val) where T : IMessage
    {
        WriteInt(val.Length);
        for (int i = 0; i < val.Length; i++)
        {
            val[i].RosSerialize(ref this);
        }
    }

    public void SerializeArray<T>(T[] val, int count) where T : IMessage
    {
        ThrowIfWrongSize(val, count);
        for (int i = 0; i < val.Length; i++)
        {
            val[i].RosSerialize(ref this);
        }
    }

    /// <summary>
    /// Serializes the given message into the buffer array.
    /// </summary>
    /// <param name="message">The ROS message.</param>
    /// <param name="buffer">The destination byte array.</param>
    /// <returns>The number of bytes written.</returns>
    internal static uint Serialize<T>(in T message, Span<byte> buffer) where T : ISerializable
    {
        fixed (byte* bufferPtr = buffer)
        {
            var b = new WriteBuffer2(bufferPtr, buffer.Length);
            message.RosSerialize(ref b);

            int oldLength = buffer.Length;
            int newLength = b.offset;

            return (uint)(oldLength - newLength);
        }
    }
}

public unsafe partial struct WriteBuffer2
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Advance(ref int c, in Vector3 _) => AdvanceAlign8T(ref c, Vector3.RosFixedMessageLength);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Advance(ref int c, in Point _) => AdvanceAlign8T(ref c, Point.RosFixedMessageLength);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Advance(ref int c, in Quaternion _) => AdvanceAlign8T(ref c, Quaternion.RosFixedMessageLength);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Advance(ref int c, in Pose _) => AdvanceAlign8T(ref c, Pose.RosFixedMessageLength);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Advance(ref int c, in Transform _) => AdvanceAlign8T(ref c, Transform.RosFixedMessageLength);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Advance(ref int c, in ColorRGBA _) => AdvanceAlign4T(ref c, ColorRGBA.RosFixedMessageLength);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Advance(ref int c, in Point32 _) => AdvanceAlign4T(ref c, Point32.RosFixedMessageLength);

    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Advance(ref int c, Vector3[] b) =>
        AdvanceAlign8Ts(ref c, Vector3.RosFixedMessageLength * b.Length);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Advance(ref int c, Point[] b) =>
        AdvanceAlign8Ts(ref c, Point.RosFixedMessageLength * b.Length);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Advance(ref int c, Quaternion[] b) =>
        AdvanceAlign8Ts(ref c, Quaternion.RosFixedMessageLength * b.Length);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Advance(ref int c, Pose[] b) => 
        AdvanceAlign8Ts(ref c, Pose.RosFixedMessageLength * b.Length);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Advance(ref int c, Transform[] b) =>
        AdvanceAlign8Ts(ref c, Transform.RosFixedMessageLength * b.Length);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Advance(ref int c, ColorRGBA[] b) =>
        AdvanceAlign4Ts(ref c, ColorRGBA.RosFixedMessageLength * b.Length);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Advance(ref int c, Point32[] b) => 
        AdvanceAlign4Ts(ref c, sizeof(Point32) * b.Length);

    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Advance(ref int c, TransformStamped[] array)
    {
        c = DoAlign4(c) + sizeof(int);
        foreach (var message in array)
        {
            message.GetRosMessageLength(ref c);
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Serialize(in Vector3 val)
    {
        Align8();
        const int size = Vector3.RosFixedMessageLength;
        ThrowIfOutOfRange(size);
        *(Vector3*)(ptr + offset) = val;
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Serialize(in Point val)
    {
        Align8();
        const int size = Point.RosFixedMessageLength;
        ThrowIfOutOfRange(size);
        *(Point*)(ptr + offset) = val;
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Serialize(in Quaternion val)
    {
        Align8();
        const int size = Quaternion.RosFixedMessageLength;
        ThrowIfOutOfRange(size);
        *(Quaternion*)(ptr + offset) = val;
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Serialize(in Transform val)
    {
        Align8();
        const int size = Transform.RosFixedMessageLength;
        ThrowIfOutOfRange(size);
        *(Transform*)(ptr + offset) = val;
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Serialize(in Pose val)
    {
        Align8();
        const int size = Transform.RosFixedMessageLength;
        ThrowIfOutOfRange(size);
        *(Pose*)(ptr + offset) = val;
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Serialize(in ColorRGBA val)
    {
        Align4();
        const int size = ColorRGBA.RosFixedMessageLength;
        ThrowIfOutOfRange(size);
        *(ColorRGBA*)(ptr + offset) = val;
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Serialize(in Point32 val)
    {
        Align4();
        const int size = Point32.RosFixedMessageLength;
        ThrowIfOutOfRange(size);
        *(Point32*)(ptr + offset) = val;
        Advance(size);
    }
}