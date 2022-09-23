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
public unsafe struct ReadBuffer2
{
    /// <summary>
    /// Current position.
    /// </summary>
    byte* cursor;

    readonly byte* end;

    public ReadBuffer2(byte* ptr, int length)
    {
        cursor = ptr;
        end = ptr + length;
    }

    public ReadBuffer2(IntPtr ptr, int length) : this((byte*)ptr, length)
    {
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Align2() => cursor = (byte*)(((nint)cursor + 1) & ~1);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Align4() => cursor = (byte*)(((nint)cursor + 3) & ~3);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Align8() => cursor = (byte*)(((nint)cursor + 7) & ~7);

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
    int ReadInt()
    {
        Deserialize(out int i);
        return i;
    }

    #region strings

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string DeserializeString()
    {
        int count = ReadInt();
        if (count == 0)
        {
            return EmptyString;
        }

        ThrowIfOutOfRange(count);
        if (count == 1)
        {
            Advance(1);
            return EmptyString; 
        }

        int countWithoutZero = count - 1;
        string val = BuiltIns.GetString(cursor, countWithoutZero);

        Advance(count);
        return val;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string SkipString()
    {
        int count = ReadInt();
        ThrowIfOutOfRange(count);
        Advance(count);
        return EmptyString;
    }

    public string[] DeserializeStringArray()
    {
        int count = ReadInt();
        return count == 0 
            ? EmptyStringArray 
            : DeserializeStringArray(count);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string[] DeserializeStringArray(int count)
    {
        ThrowIfOutOfRange(4 * count);
        string[] val = new string[count];
        for (int i = 0; i < count; i++)
        {
            Align4();
            val[i] = DeserializeString();
        }

        return val;
    }

    public string[] SkipStringArray()
    {
        int count = ReadInt();
        for (int i = 0; i < count; i++)
        {
            Align4();
            int innerCount = ReadInt();
            ThrowIfOutOfRange(innerCount);
            Advance(innerCount);
        }

        return EmptyStringArray;
    }

    #endregion

    #region scalars

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out bool t)
    {
        ThrowIfOutOfRange(1);
        t = *cursor != 0;
        Advance(1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out byte t)
    {
        ThrowIfOutOfRange(1);
        t = *cursor;
        Advance(1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out sbyte t)
    {
        ThrowIfOutOfRange(1);
        t = (sbyte)*cursor;
        Advance(1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out short t)
    {
        const int size = sizeof(short);
        ThrowIfOutOfRange(size);
        t = *(short*)cursor;
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out ushort t)
    {
        const int size = sizeof(ushort);
        ThrowIfOutOfRange(size);
        t = *(ushort*)cursor;
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out int t)
    {
        const int size = sizeof(int);
        ThrowIfOutOfRange(size);
        t = *(int*)cursor;
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out uint t)
    {
        const int size = sizeof(uint);
        ThrowIfOutOfRange(size);
        t = *(uint*)cursor;
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out time t)
    {
        const int size = 2 * sizeof(uint);
        ThrowIfOutOfRange(size);
        t = *(time*)cursor;
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out duration t)
    {
        const int size = 2 * sizeof(int);
        ThrowIfOutOfRange(size);
        t = *(duration*)cursor;
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out float t)
    {
        const int size = sizeof(float);
        ThrowIfOutOfRange(size);
        t = *(float*)cursor;
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out double t)
    {
        const int size = sizeof(double);
        ThrowIfOutOfRange(size);
        t = *(double*)cursor;
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out long t)
    {
        const int size = sizeof(long);
        ThrowIfOutOfRange(size);
        t = *(long*)cursor;
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out ulong t)
    {
        const int size = sizeof(ulong);
        ThrowIfOutOfRange(size);
        t = *(ulong*)cursor;
        Advance(size);
    }

    #endregion

    #region arrays

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void DeserializeStructArray(ref byte value, int count)
    {
        ThrowIfOutOfRange(count);
        Unsafe.CopyBlock(ref value, ref *cursor, (uint)count);
        Advance(count);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SharedRent DeserializeStructRent()
    {
        int count = ReadInt();
        if (count == 0)
        {
            return SharedRent.Empty;
        }

        ThrowIfOutOfRange(count);

        var array = new SharedRent(count);
        Unsafe.CopyBlock(ref array.Array[0], ref *cursor, (uint)count);

        Advance(count);
        return array;
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int DeserializeArrayLength()
    {
        int count = ReadInt();
        if ((uint)count > 1024 * 1024 * 1024)
        {
            BuiltIns.ThrowImplausibleBufferSize();
        }

        return count;
    }

    #endregion

    #region Empties

    static string EmptyString => "";

    static string[] EmptyStringArray
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => EmptyArray.StringArray;
    }

    #endregion

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
    
    public static T Deserialize<T>(Deserializer<T> generator, ReadOnlySpan<byte> buffer)
        where T : IMessage
    {
        fixed (byte* bufferPtr = buffer)
        {
            var b = new ReadBuffer2(bufferPtr, buffer.Length);
            generator.RosDeserialize(ref b, out var msg);
            return msg;
        }
    }    

    #region extras

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out Vector3 t)
    {
        const int size = Vector3.Ros2FixedMessageLength;
        ThrowIfOutOfRange(size);
        t = *(Vector3*)cursor;
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out Point t)
    {
        const int size = Point.Ros2FixedMessageLength;
        ThrowIfOutOfRange(size);
        t = *(Point*)cursor;
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out Quaternion t)
    {
        const int size = Quaternion.Ros2FixedMessageLength;
        ThrowIfOutOfRange(size);
        t = *(Quaternion*)cursor;
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out Pose t)
    {
        const int size = Pose.Ros2FixedMessageLength;
        ThrowIfOutOfRange(size);
        t = *(Pose*)cursor;
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out Transform t)
    {
        const int size = Transform.Ros2FixedMessageLength;
        ThrowIfOutOfRange(size);
        t = *(Transform*)cursor;
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out Point32 t)
    {
        const int size = Point32.Ros2FixedMessageLength;
        ThrowIfOutOfRange(size);
        t = *(Point32*)cursor;
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out Color32 t)
    {
        const int size = Color32.Ros2FixedMessageLength;
        ThrowIfOutOfRange(size);
        t = *(Color32*)cursor;
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out Triangle t)
    {
        const int size = Triangle.Ros2FixedMessageLength;
        ThrowIfOutOfRange(size);
        t = *(Triangle*)cursor;
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out ColorRGBA t)
    {
        const int size = ColorRGBA.Ros2FixedMessageLength;
        ThrowIfOutOfRange(size);
        t = *(ColorRGBA*)cursor;
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void DeserializeStructArray<T>(T[] array) where T : unmanaged
    {
        DeserializeStructArray(ref Unsafe.As<T, byte>(ref array[0]), array.Length * sizeof(T));
    }

    /*
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void DeserializeStructArray(bool[] array)
    {
        DeserializeStructArray(ref Unsafe.As<bool, byte>(ref array[0]), array.Length);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void DeserializeStructArray(byte[] array)
    {
        DeserializeStructArray(ref array[0], array.Length);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void DeserializeStructArray(sbyte[] array)
    {
        DeserializeStructArray(ref Unsafe.As<sbyte, byte>(ref array[0]),
            array.Length * sizeof(sbyte));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void DeserializeStructArray(short[] array)
    {
        DeserializeStructArray(ref Unsafe.As<short, byte>(ref array[0]),
            array.Length * sizeof(short));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void DeserializeStructArray(ushort[] array)
    {
        DeserializeStructArray(ref Unsafe.As<ushort, byte>(ref array[0]),
            array.Length * sizeof(ushort));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void DeserializeStructArray(int[] array)
    {
        DeserializeStructArray(ref Unsafe.As<int, byte>(ref array[0]),
            array.Length * sizeof(int));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void DeserializeStructArray(uint[] array)
    {
        DeserializeStructArray(ref Unsafe.As<uint, byte>(ref array[0]),
            array.Length * sizeof(uint));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void DeserializeStructArray(long[] array)
    {
        DeserializeStructArray(ref Unsafe.As<long, byte>(ref array[0]),
            array.Length * sizeof(long));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void DeserializeStructArray(ulong[] array)
    {
        DeserializeStructArray(ref Unsafe.As<ulong, byte>(ref array[0]),
            array.Length * sizeof(ulong));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void DeserializeStructArray(float[] array)
    {
        DeserializeStructArray(ref Unsafe.As<float, byte>(ref array[0]),
            array.Length * sizeof(float));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void DeserializeStructArray(double[] array)
    {
        DeserializeStructArray(ref Unsafe.As<double, byte>(ref array[0]),
            array.Length * sizeof(double));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void DeserializeStructArray(time[] array)
    {
        DeserializeStructArray(ref Unsafe.As<time, byte>(ref array[0]),
            array.Length * (2*sizeof(int)));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void DeserializeStructArray(duration[] array)
    {
        DeserializeStructArray(ref Unsafe.As<duration, byte>(ref array[0]),
            array.Length * (2*sizeof(int)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void DeserializeStructArray(Point[] array)
    {
        DeserializeStructArray(ref Unsafe.As<Point, byte>(ref array[0]),
            array.Length * Point.RosFixedMessageLength);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void DeserializeStructArray(Vector3[] array)
    {
        DeserializeStructArray(ref Unsafe.As<Vector3, byte>(ref array[0]),
            array.Length * Vector3.RosFixedMessageLength);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void DeserializeStructArray(Point32[] array)
    {
        DeserializeStructArray(ref Unsafe.As<Point32, byte>(ref array[0]),
            array.Length * Point32.RosFixedMessageLength);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void DeserializeStructArray(Quaternion[] array)
    {
        DeserializeStructArray(ref Unsafe.As<Quaternion, byte>(ref array[0]),
            array.Length * Quaternion.RosFixedMessageLength);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void DeserializeStructArray(ColorRGBA[] array)
    {
        DeserializeStructArray(ref Unsafe.As<ColorRGBA, byte>(ref array[0]),
            array.Length * ColorRGBA.RosFixedMessageLength);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void DeserializeStructArray(Color32[] array)
    {
        DeserializeStructArray(ref Unsafe.As<Color32, byte>(ref array[0]),
            array.Length * Color32.RosFixedMessageLength);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void DeserializeStructArray(Triangle[] array)
    {
        DeserializeStructArray(ref Unsafe.As<Triangle, byte>(ref array[0]),
            array.Length * Triangle.RosFixedMessageLength);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void DeserializeStructArray(Transform[] array)
    {
        DeserializeStructArray(ref Unsafe.As<Transform, byte>(ref array[0]),
            array.Length * Transform.RosFixedMessageLength);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void DeserializeStructArray(Pose[] array)
    {
        DeserializeStructArray(ref Unsafe.As<Pose, byte>(ref array[0]),
            array.Length * Pose.RosFixedMessageLength);
    }
    */
    
    #endregion
}