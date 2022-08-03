using System;
using System.Runtime.CompilerServices;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.IvizMsgs;
using Iviz.Msgs.StdMsgs;
using Iviz.Tools;

namespace Iviz.Msgs;

/// <summary>
/// Contains utilities to deserialize ROS 2 messages from a byte array.
/// </summary>
public unsafe partial struct ReadBuffer2
{
    /// <summary>
    /// Current position.
    /// </summary>
    readonly byte* ptr;
    int offset;
    int remaining;

    ReadBuffer2(byte* ptr, int length)
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
    int ReadInt()
    {
        Deserialize(out int i);
        return i;
    }

    static string EmptyString => "";

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void DeserializeString(out string val)
    {
        int count = ReadInt();
        if (count == 0)
        {
            val = EmptyString;
            return;
        }
        
        ThrowIfOutOfRange(count);
        if (count == 1)
        {
            val = EmptyString;
            Advance(1);
            return;
        }

        int countWithoutZero = count - 1;
        byte* srcPtr = ptr + offset;
        val = count <= 64
            ? BuiltIns.GetStringSimple(srcPtr, countWithoutZero)
            : BuiltIns.UTF8.GetString(srcPtr, countWithoutZero);

        Advance(count);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SkipString(out string val)
    {
        int count = ReadInt();
        ThrowIfOutOfRange(count);
        Advance(count);
        val = EmptyString;
    }

    public void DeserializeStringArray(out string[] val)
    {
        int count = ReadInt();
        if (count == 0)
        {
            val = Array.Empty<string>();
        }
        else
        {
            DeserializeStringArray(count, out val);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining), SkipLocalsInit]
    public void DeserializeStringArray(int count, out string[] val)
    {
        ThrowIfOutOfRange(4 * count);
        val = new string[count];
        for (int i = 0; i < val.Length; i++)
        {
            DeserializeString(out val[i]);
        }
    }

    public void SkipStringArray(out string[] val)
    {
        int count = ReadInt();
        for (int i = 0; i < count; i++)
        {
            int innerCount = ReadInt();
            Advance(innerCount);
        }

        val = Array.Empty<string>();
    }

    #region scalars

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out bool t)
    {
        ThrowIfOutOfRange(1);
        t = ptr[offset] != 0;
        Advance(1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out byte t)
    {
        ThrowIfOutOfRange(1);
        t = ptr[offset];
        Advance(1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out sbyte t)
    {
        ThrowIfOutOfRange(1);
        t = (sbyte)ptr[offset];
        Advance(1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out short t)
    {
        Align2();
        const int size = sizeof(short);
        ThrowIfOutOfRange(size);
        t = *(short*)(ptr + offset);
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out ushort t)
    {
        Align2();
        const int size = sizeof(ushort);
        ThrowIfOutOfRange(size);
        t = *(ushort*)(ptr + offset);
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out int t)
    {
        Align4();
        const int size = sizeof(int);
        ThrowIfOutOfRange(size);
        t = *(int*)(ptr + offset);
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out uint t)
    {
        Align4();
        const int size = sizeof(uint);
        ThrowIfOutOfRange(size);
        t = *(uint*)(ptr + offset);
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out time t)
    {
        Align4();
        const int size = 2 * sizeof(uint);
        ThrowIfOutOfRange(size);
        t = *(time*)(ptr + offset);
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out duration t)
    {
        Align4();
        const int size = 2 * sizeof(int);
        ThrowIfOutOfRange(size);
        t = *(duration*)(ptr + offset);
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out float t)
    {
        Align4();
        const int size = sizeof(float);
        ThrowIfOutOfRange(size);
        t = *(float*)(ptr + offset);
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out double t)
    {
        Align8();
        const int size = sizeof(double);
        ThrowIfOutOfRange(size);
        t = *(double*)(ptr + offset);
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out long t)
    {
        Align8();
        const int size = sizeof(long);
        ThrowIfOutOfRange(size);
        t = *(long*)(ptr + offset);
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out ulong t)
    {
        Align8();
        const int size = sizeof(ulong);
        ThrowIfOutOfRange(size);
        t = *(ulong*)(ptr + offset);
        Advance(size);
    }

    #endregion

    #region arrays

    public void DeserializeStructArray(out bool[] val)
    {
        int count = ReadInt();
        if (count == 0) val = Array.Empty<bool>();
        else DeserializeStructArray(count, out val);
    }

    public void DeserializeStructArray(int count, out bool[] val)
    {
        DeserializeStructArrayCore(count, out val);
    }

    public void DeserializeStructArray(out byte[] val)
    {
        int count = ReadInt();
        if (count == 0) val = Array.Empty<byte>();
        else DeserializeStructArray(count, out val);
    }

    public void DeserializeStructArray(int count, out byte[] val)
    {
        DeserializeStructArrayCore(count, out val);
    }

    public void DeserializeStructArray(out sbyte[] val)
    {
        int count = ReadInt();
        if (count == 0) val = Array.Empty<sbyte>();
        else DeserializeStructArray(count, out val);
    }

    public void DeserializeStructArray(int count, out sbyte[] val)
    {
        DeserializeStructArrayCore(count, out val);
    }

    public void DeserializeStructArray(out short[] val)
    {
        int count = ReadInt();
        if (count == 0) val = Array.Empty<short>();
        else DeserializeStructArray(count, out val);
    }

    public void DeserializeStructArray(int count, out short[] val)
    {
        Align4();
        DeserializeStructArrayCore(count, out val);
    }

    public void DeserializeStructArray(out ushort[] val)
    {
        int count = ReadInt();
        if (count == 0) val = Array.Empty<ushort>();
        else DeserializeStructArray(count, out val);
    }

    public void DeserializeStructArray(int count, out ushort[] val)
    {
        Align4();
        DeserializeStructArrayCore(count, out val);
    }

    public void DeserializeStructArray(out int[] val)
    {
        int count = ReadInt();
        if (count == 0) val = Array.Empty<int>();
        else DeserializeStructArray(count, out val);
    }

    public void DeserializeStructArray(int count, out int[] val)
    {
        Align4();
        DeserializeStructArrayCore(count, out val);
    }

    public void DeserializeStructArray(out uint[] val)
    {
        int count = ReadInt();
        if (count == 0) val = Array.Empty<uint>();
        else DeserializeStructArray(count, out val);
    }

    public void DeserializeStructArray(int count, out uint[] val)
    {
        Align4();
        DeserializeStructArrayCore(count, out val);
    }

    public void DeserializeStructArray(out float[] val)
    {
        int count = ReadInt();
        if (count == 0) val = Array.Empty<float>();
        else DeserializeStructArray(count, out val);
    }

    public void DeserializeStructArray(int count, out float[] val)
    {
        Align4();
        DeserializeStructArrayCore(count, out val);
    }

    public void DeserializeStructArray(out double[] val)
    {
        int count = ReadInt();
        if (count == 0) val = Array.Empty<double>();
        else DeserializeStructArray(count, out val);
    }

    public void DeserializeStructArray(int count, out double[] val)
    {
        Align8();
        DeserializeStructArrayCore(count, out val);
    }

    public void DeserializeStructArray(out long[] val)
    {
        int count = ReadInt();
        if (count == 0) val = Array.Empty<long>();
        else DeserializeStructArray(count, out val);
    }

    public void DeserializeStructArray(int count, out long[] val)
    {
        Align8();
        DeserializeStructArrayCore(count, out val);
    }

    public void DeserializeStructArray(out ulong[] val)
    {
        int count = ReadInt();
        if (count == 0) val = Array.Empty<ulong>();
        else DeserializeStructArray(count, out val);
    }

    public void DeserializeStructArray(int count, out ulong[] val)
    {
        Align8();
        DeserializeStructArrayCore(count, out val);
    }

    public void DeserializeStructArray<T>(out T[] val) where T : unmanaged
    {
        int count = ReadInt();
        if (count == 0) val = Array.Empty<T>();
        else DeserializeStructArrayCore(count, out val);
    }

    void DeserializeStructArrayCore<T>(int count, out T[] val) where T : unmanaged
    {
        int size = count * sizeof(T);
        ThrowIfOutOfRange(size);

        val = new T[count];

        byte* srcPtr = ptr + offset;
        fixed (T* valPtr = val)
        {
            Unsafe.CopyBlock(valPtr, srcPtr, (uint)size);
        }

        Advance(size);
    }

    #endregion

    public void DeserializeStructRent(out SharedRent<byte> val)
    {
        int count = ReadInt();
        if (count == 0)
        {
            val = SharedRent<byte>.Empty;
            return;
        }

        ThrowIfOutOfRange(count);

        val = new SharedRent<byte>(count);

        byte* srcPtr = ptr + offset;
        fixed (byte* valPtr = val.Array)
        {
            Unsafe.CopyBlock(valPtr, srcPtr, (uint)count);
        }

        Advance(count);
    }

    public T[] SkipStructArray<T>() where T : unmanaged
    {
        int count = ReadInt();
        int sizeOfT = Unsafe.SizeOf<T>();
        int size = count * sizeOfT;
        ThrowIfOutOfRange(size);
        Advance(size);
        return Array.Empty<T>();
    }

    public void DeserializeArray<T>(out T[] val) where T : IMessageRos2, new()
    {
        int count = ReadInt();
        if (count == 0)
        {
            val = Array.Empty<T>();
            return;
        }

        if (count <= 1024 * 1024 * 1024)
        {
            val = new T[count];
            return; // entry deserializations happen outside
        }

        BuiltIns.ThrowImplausibleBufferSize();
        val = Array.Empty<T>(); // unreachable
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void Align2() => Advance(((int.MaxValue - 1) - offset) & 1);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void Align4() => Advance(((int.MaxValue - 3) - offset) & 3);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void Align8() => Advance(((int.MaxValue - 7) - offset) & 7);

    /// <summary>
    /// Deserializes a message of the given type from the buffer array.  
    /// </summary>
    /// <param name="generator">
    /// An arbitrary instance of the type T. Can be anything, such as new T().
    /// This is a (rather unclean) workaround to the fact that C# cannot invoke static functions from generics.
    /// So instead of using T.Deserialize(), we need an instance to do this.
    /// </param>
    /// <param name="buffer">
    /// The source byte array. 
    /// </param>
    /// <typeparam name="T">Message type.</typeparam>
    /// <returns>The deserialized message.</returns>
    public static T Deserialize<T>(IDeserializableRos2<T> generator, ReadOnlySpan<byte> buffer)
        where T : ISerializableRos2
    {
        fixed (byte* bufferPtr = buffer)
        {
            var b = new ReadBuffer2(bufferPtr, buffer.Length);
            return generator.RosDeserialize(ref b);
        }
    }
}

public unsafe partial struct ReadBuffer2
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out Vector3 t)
    {
        Align8();
        const int size = Vector3.Ros2FixedMessageLength;
        ThrowIfOutOfRange(size);
        t = *(Vector3*)(ptr + offset);
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out Point t)
    {
        Align8();
        const int size = Point.Ros2FixedMessageLength;
        ThrowIfOutOfRange(size);
        t = *(Point*)(ptr + offset);
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out Quaternion t)
    {
        Align8();
        const int size = Quaternion.Ros2FixedMessageLength;
        ThrowIfOutOfRange(size);
        t = *(Quaternion*)(ptr + offset);
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out Pose t)
    {
        Align8();
        const int size = Pose.Ros2FixedMessageLength;
        ThrowIfOutOfRange(size);
        t = *(Pose*)(ptr + offset);
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out Transform t)
    {
        Align8();
        const int size = Transform.Ros2FixedMessageLength;
        ThrowIfOutOfRange(size);
        t = *(Transform*)(ptr + offset);
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out Point32 t)
    {
        Align4();
        const int size = Point32.Ros2FixedMessageLength;
        ThrowIfOutOfRange(size);
        t = *(Point32*)(ptr + offset);
        Advance(size);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out Color32 t)
    {
        const int size = Color32.Ros2FixedMessageLength;
        ThrowIfOutOfRange(size);
        t = *(Color32*)(ptr + offset);
        Advance(size);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out Vector3f t)
    {
        Align4();
        const int size = Vector3f.Ros2FixedMessageLength;
        ThrowIfOutOfRange(size);
        t = *(Vector3f*)(ptr + offset);
        Advance(size);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out Vector2f t)
    {
        Align4();
        const int size = Vector2f.Ros2FixedMessageLength;
        ThrowIfOutOfRange(size);
        t = *(Vector2f*)(ptr + offset);
        Advance(size);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out Triangle t)
    {
        Align4();
        const int size = Triangle.Ros2FixedMessageLength;
        ThrowIfOutOfRange(size);
        t = *(Triangle*)(ptr + offset);
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out ColorRGBA t)
    {
        Align4();
        const int size = ColorRGBA.Ros2FixedMessageLength;
        ThrowIfOutOfRange(size);
        t = *(ColorRGBA*)(ptr + offset);
        Advance(size);
    }
    
    public void DeserializeStructArray(out Vector3[] val)
    {
        int count = ReadInt();
        if (count == 0) val = Array.Empty<Vector3>();
        else DeserializeStructArray(count, out val);
    }

    public void DeserializeStructArray(int count, out Vector3[] val)
    {
        Align8();
        DeserializeStructArrayCore(count, out val);
    }

    public void DeserializeStructArray(out Point[] val)
    {
        int count = ReadInt();
        if (count == 0) val = Array.Empty<Point>();
        else DeserializeStructArray(count, out val);
    }

    public void DeserializeStructArray(int count, out Point[] val)
    {
        Align8();
        DeserializeStructArrayCore(count, out val);
    }

    public void DeserializeStructArray(out Point32[] val)
    {
        int count = ReadInt();
        if (count == 0) val = Array.Empty<Point32>();
        else DeserializeStructArray(count, out val);
    }

    public void DeserializeStructArray(int count, out Point32[] val)
    {
        Align4();
        DeserializeStructArrayCore(count, out val);
    }

    public void DeserializeStructArray(out ColorRGBA[] val)
    {
        int count = ReadInt();
        if (count == 0) val = Array.Empty<ColorRGBA>();
        else DeserializeStructArray(count, out val);
    }

    public void DeserializeStructArray(int count, out ColorRGBA[] val)
    {
        Align4();
        DeserializeStructArrayCore(count, out val);
    }

    public void DeserializeStructArray(out Transform[] val)
    {
        int count = ReadInt();
        if (count == 0) val = Array.Empty<Transform>();
        else DeserializeStructArray(count, out val);
    }

    public void DeserializeStructArray(int count, out Transform[] val)
    {
        Align8();
        DeserializeStructArrayCore(count, out val);
    }

    public void DeserializeStructArray(out Pose[] val)
    {
        int count = ReadInt();
        if (count == 0) val = Array.Empty<Pose>();
        else DeserializeStructArray(count, out val);
    }

    public void DeserializeStructArray(int count, out Pose[] val)
    {
        Align8();
        DeserializeStructArrayCore(count, out val);
    }    
}