using System;
using System.Runtime.CompilerServices;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.IvizMsgs;
using Iviz.Msgs.StdMsgs;
using Iviz.Tools;

namespace Iviz.Msgs;

/// <summary>
/// Contains utilities to serialize ROS 2 messages into a byte array. 
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
    public static void AddLength(ref int c, bool _) => AdvanceAlign1(ref c);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddLength(ref int c, sbyte _) => AdvanceAlign1(ref c);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddLength(ref int c, byte _) => AdvanceAlign1(ref c);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddLength(ref int c, short _) => AdvanceAlign2(ref c);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddLength(ref int c, ushort _) => AdvanceAlign2(ref c);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddLength(ref int c, int _) => AdvanceAlign4(ref c);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddLength(ref int c, uint _) => AdvanceAlign4(ref c);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddLength(ref int c, float _) => AdvanceAlign4(ref c);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddLength(ref int c, double _) => AdvanceAlign8(ref c);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddLength(ref int c, long _) => AdvanceAlign8(ref c);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddLength(ref int c, ulong _) => AdvanceAlign8(ref c);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddLength(ref int c, in time _) => AdvanceAlign4Type(ref c, 2 * sizeof(int));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddLength(ref int c, in duration _) => AdvanceAlign4Type(ref c, 2 * sizeof(int));


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddLength(ref int c, byte[] b) => AdvanceAlign1Array(ref c, b.Length);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddLength(ref int c, byte[] _, int l) => AdvanceAlign1ArrayFixed(ref c, l);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddLength(ref int c, SharedRent<byte> b) => AdvanceAlign1Array(ref c, b.Length);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddLength(ref int c, sbyte[] b) => AdvanceAlign1Array(ref c, b.Length);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddLength(ref int c, sbyte[] _, int l) => AdvanceAlign1ArrayFixed(ref c, l);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddLength(ref int c, bool[] b) => AdvanceAlign1Array(ref c, b.Length);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddLength(ref int c, short[] b) => AdvanceAlign2Array(ref c, b.Length);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddLength(ref int c, short[] _, int l) => AdvanceAlign2ArrayFixed(ref c, l);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddLength(ref int c, ushort[] b) => AdvanceAlign2Array(ref c, b.Length);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddLength(ref int c, ushort[] _, int l) => AdvanceAlign2ArrayFixed(ref c, l);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddLength(ref int c, int[] b) => AdvanceAlign4Array(ref c, b.Length);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddLength(ref int c, int[] _, int l) => AdvanceAlign4ArrayFixed(ref c, l);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddLength(ref int c, uint[] b) => AdvanceAlign4Array(ref c, b.Length);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddLength(ref int c, uint[] _, int l) => AdvanceAlign4ArrayFixed(ref c, l);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddLength(ref int c, float[] b) => AdvanceAlign4Array(ref c, b.Length);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddLength(ref int c, float[] _, int l) => AdvanceAlign4ArrayFixed(ref c, l);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddLength(ref int c, double[] b) => AdvanceAlign8Array(ref c, b.Length);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddLength(ref int c, double[] _, int l) => AdvanceAlign8ArrayFixed(ref c, l);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddLength(ref int c, long[] b) => AdvanceAlign8Array(ref c, b.Length);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddLength(ref int c, long[] _, int l) => AdvanceAlign8ArrayFixed(ref c, l);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddLength(ref int c, ulong[] b) => AdvanceAlign8Array(ref c, b.Length);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddLength(ref int c, ulong[] _, int l) => AdvanceAlign8ArrayFixed(ref c, l);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static void AdvanceAlign1(ref int c) => c++;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static void AdvanceAlign1Array(ref int c, int length) => c = DoAlign4(c) + 4 + length;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static void AdvanceAlign1ArrayFixed(ref int c, int size) => c += size;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static void AdvanceAlign1Type(ref int c, int length) => c += length;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static void AdvanceAlign1TypeArray(ref int c, int length) => AdvanceAlign1Array(ref c, length);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static void AdvanceAlign2(ref int c) => c = DoAlign2(c) + 2;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static void AdvanceAlign2Array(ref int c, int length) => c = DoAlign4(c) + 4 + 2 * length;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static void AdvanceAlign2ArrayFixed(ref int c, int length) => c = DoAlign2(c) + 2 * length;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static void AdvanceAlign4(ref int c) => c = DoAlign4(c) + 4;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static void AdvanceAlign4Array(ref int c, int length) => c = DoAlign4(c) + 4 + 4 * length;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static void AdvanceAlign4ArrayFixed(ref int c, int length) => c = DoAlign4(c) + 4 * length;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static void AdvanceAlign4Type(ref int c, int size) => c = DoAlign4(c) + size;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static void AdvanceAlign4TypeArray(ref int c, int length) => AdvanceAlign4Array(ref c, length);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static void AdvanceAlign8(ref int c)
    {
        c = DoAlign8(c) + 8;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static void AdvanceAlign8Array(ref int c, int length)
    {
        c = DoAlign4(c) + 4;
        c = DoAlign8(c) + 8 * length;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static void AdvanceAlign8ArrayFixed(ref int c, int length)
    {
        c = DoAlign8(c) + 8 * length;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static void AdvanceAlign8Type(ref int c, int length)
    {
        c = DoAlign8(c) + length;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static void AdvanceAlign8TypeArray(ref int c, int length)
    {
        c = DoAlign4(c) + 4;
        c = DoAlign8(c) + length;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddLength(ref int c, ISerializable[] array)
    {
        c = DoAlign4(c) + sizeof(int);
        foreach (var message in array)
        {
            message.AddRos2MessageLength(ref c);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddLength(ref int c, string? s)
    {
        c = DoAlign4(c) + sizeof(int);
        if (string.IsNullOrEmpty(s))
        {
            c++;
            return;
        }

        c += BuiltIns.UTF8.GetByteCount(s) + 1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddLength(ref int c, string[] bs)
    {
        c = DoAlign4(c) + sizeof(int);
        foreach (string b in bs)
        {
            AddLength(ref c, b);
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

    #endregion

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void WriteInt(int value) => Serialize(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Serialize(string val)
    {
        if (val.Length == 0)
        {
            ThrowIfOutOfRange(5);
            WriteInt(1);
            ptr[offset] = 0;
            Advance(1);
            return;
        }

        int count = BuiltIns.UTF8.GetByteCount(val);
        ThrowIfOutOfRange(4 + count + 1);
        WriteInt(count + 1);
        fixed (char* valPtr = val)
        {
            BuiltIns.UTF8.GetBytes(valPtr, val.Length, ptr + offset, remaining);
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
    public void SerializeStructArray(Vector3[] val, int count)
    {
        ThrowIfWrongSize(val, count);
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
    public void SerializeStructArray(time[] val)
    {
        WriteInt(val.Length);
        if (val.Length == 0) return;
        Align4();
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
    public void SerializeStructArray(Color32[] val)
    {
        WriteInt(val.Length);
        if (val.Length == 0) return;
        SerializeStructArrayCore(val);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SerializeStructArray(Vector3f[] val)
    {
        WriteInt(val.Length);
        if (val.Length == 0) return;
        Align4();
        SerializeStructArrayCore(val);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SerializeStructArray(Triangle[] val)
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

    public void SerializeStructArray(SharedRent<byte> val)
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

    public void SerializeArray(IMessage[] msgs)
    {
        WriteInt(msgs.Length);
        foreach (var msg in msgs)
        {
            msg.RosSerialize(ref this);
        }
    }

    public void SerializeArray(IMessage[] msgs, int count)
    {
        ThrowIfWrongSize(msgs, count);
        foreach (var msg in msgs)
        {
            msg.RosSerialize(ref this);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetRosMessageLength(ISerializable msg)
    {
        int s = 0;
        msg.AddRos2MessageLength(ref s);
        return s;
    }

    /// <summary>
    /// Serializes the given message into the buffer array.
    /// </summary>
    /// <param name="message">The ROS message.</param>
    /// <param name="buffer">The destination byte array.</param>
    /// <returns>The number of bytes written.</returns>
    public static uint Serialize<T>(in T message, Span<byte> buffer) where T : ISerializable
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
    public static void AddLength(ref int c, in Vector3 _) => AdvanceAlign8Type(ref c, Vector3.Ros2FixedMessageLength);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddLength(ref int c, in Point _) => AdvanceAlign8Type(ref c, Point.Ros2FixedMessageLength);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddLength(ref int c, in Quaternion _) =>
        AdvanceAlign8Type(ref c, Quaternion.Ros2FixedMessageLength);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddLength(ref int c, in Pose _) => AdvanceAlign8Type(ref c, Pose.Ros2FixedMessageLength);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddLength(ref int c, in Transform _) =>
        AdvanceAlign8Type(ref c, Transform.Ros2FixedMessageLength);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddLength(ref int c, in ColorRGBA _) =>
        AdvanceAlign4Type(ref c, ColorRGBA.Ros2FixedMessageLength);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddLength(ref int c, in Point32 _) => AdvanceAlign4Type(ref c, Point32.Ros2FixedMessageLength);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddLength(ref int c, in Color32 _) => AdvanceAlign1Type(ref c, Color32.Ros2FixedMessageLength);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddLength(ref int c, in Vector3f _) => AdvanceAlign4Type(ref c, Vector3f.Ros2FixedMessageLength);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddLength(ref int c, in Vector2f _) => AdvanceAlign4Type(ref c, Vector2f.Ros2FixedMessageLength);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddLength(ref int c, in Triangle _) => AdvanceAlign4Type(ref c, Triangle.Ros2FixedMessageLength);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddLength(ref int c, Vector3[] b) =>
        AdvanceAlign8TypeArray(ref c, Vector3.Ros2FixedMessageLength * b.Length);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddLength(ref int c, Vector3[] _, int l) =>
        AdvanceAlign8TypeArray(ref c, Vector3.Ros2FixedMessageLength * l);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddLength(ref int c, Point[] b) =>
        AdvanceAlign8TypeArray(ref c, Point.Ros2FixedMessageLength * b.Length);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddLength(ref int c, Quaternion[] b) =>
        AdvanceAlign8TypeArray(ref c, Quaternion.Ros2FixedMessageLength * b.Length);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddLength(ref int c, Pose[] b) =>
        AdvanceAlign8TypeArray(ref c, Pose.Ros2FixedMessageLength * b.Length);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddLength(ref int c, Transform[] b) =>
        AdvanceAlign8TypeArray(ref c, Transform.Ros2FixedMessageLength * b.Length);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddLength(ref int c, ColorRGBA[] b) =>
        AdvanceAlign4TypeArray(ref c, ColorRGBA.Ros2FixedMessageLength * b.Length);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddLength(ref int c, Vector3f[] b) =>
        AdvanceAlign4TypeArray(ref c, Vector3f.Ros2FixedMessageLength * b.Length);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddLength(ref int c, Triangle[] b) =>
        AdvanceAlign4TypeArray(ref c, Vector3f.Ros2FixedMessageLength * b.Length);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddLength(ref int c, Point32[] b) =>
        AdvanceAlign4TypeArray(ref c, sizeof(Point32) * b.Length);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddLength(ref int c, time[] b) =>
        AdvanceAlign4TypeArray(ref c, 2 * sizeof(int) * b.Length);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddLength(ref int c, Color32[] b) =>
        AdvanceAlign1TypeArray(ref c, sizeof(Color32) * b.Length);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddLength(ref int c, TransformStamped[] b)
    {
        c = DoAlign4(c) + sizeof(int);
        foreach (var message in b)
        {
            message.AddRos2MessageLength(ref c);
        }
    }


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
    public void Serialize(in Vector2f val)
    {
        Align4();
        const int size = Vector2f.Ros2FixedMessageLength;
        ThrowIfOutOfRange(size);
        *(Vector2f*)(ptr + offset) = val;
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Serialize(in Vector3f val)
    {
        Align4();
        const int size = Vector3f.Ros2FixedMessageLength;
        ThrowIfOutOfRange(size);
        *(Vector3f*)(ptr + offset) = val;
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


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetRosMessageLength(in RclInterfaces.Log msg)
    {
        int s = 0;
        msg.AddRos2MessageLength(ref s);
        return s;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetRosMessageLength(in RosgraphMsgs.Log msg)
    {
        int s = 0;
        msg.AddRos2MessageLength(ref s);
        return s;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetRosMessageLength(in Header msg)
    {
        int s = 0;
        msg.AddRos2MessageLength(ref s);
        return s;
    }


    public void SerializeArray(TransformStamped[] val)
    {
        WriteInt(val.Length);
        for (int i = 0; i < val.Length; i++)
        {
            val[i].RosSerialize(ref this);
        }
    }
}