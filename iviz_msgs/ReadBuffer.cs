using System;
using System.Runtime.CompilerServices;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.IvizMsgs;
using Iviz.Msgs.StdMsgs;
using Iviz.Tools;

namespace Iviz.Msgs;

/// <summary>
/// Contains utilities to deserialize ROS messages from a byte array. 
/// </summary>
public unsafe struct ReadBuffer
{
    byte* cursor;
    readonly byte* end;

    ReadBuffer(byte* ptr, int length)
    {
        cursor = ptr;
        end = ptr + length;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void Advance(int value)
    {
        cursor += value;
    }

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
        val = BuiltIns.GetString(cursor, count);

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
            val = EmptyStringArray;
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
        for (int i = 0; i < count; i++)
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
            ThrowIfOutOfRange(innerCount);
            Advance(innerCount);
        }

        val = EmptyStringArray;
    }

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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out Vector3 t)
    {
        const int size = Vector3.RosFixedMessageLength;
        ThrowIfOutOfRange(size);
        t = *(Vector3*)cursor;
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out Point t)
    {
        const int size = Point.RosFixedMessageLength;
        ThrowIfOutOfRange(size);
        t = *(Point*)cursor;
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out Quaternion t)
    {
        const int size = Quaternion.RosFixedMessageLength;
        ThrowIfOutOfRange(size);
        t = *(Quaternion*)cursor;
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out Pose t)
    {
        const int size = Pose.RosFixedMessageLength;
        ThrowIfOutOfRange(size);
        t = *(Pose*)cursor;
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out Transform t)
    {
        const int size = Transform.RosFixedMessageLength;
        ThrowIfOutOfRange(size);
        t = *(Transform*)cursor;
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out Point32 t)
    {
        const int size = Point32.RosFixedMessageLength;
        ThrowIfOutOfRange(size);
        t = *(Point32*)cursor;
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out ColorRGBA t)
    {
        const int size = ColorRGBA.RosFixedMessageLength;
        ThrowIfOutOfRange(size);
        t = *(ColorRGBA*)cursor;
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out Color32 t)
    {
        const int size = Color32.RosFixedMessageLength;
        ThrowIfOutOfRange(size);
        t = *(Color32*)cursor;
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out Triangle t)
    {
        const int size = Triangle.RosFixedMessageLength;
        ThrowIfOutOfRange(size);
        t = *(Triangle*)cursor;
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize<T>(out T t) where T : unmanaged
    {
        int size = sizeof(T);
        ThrowIfOutOfRange(size);
        t = *(T*)cursor;
        Advance(size);
    }

    #endregion

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void DeserializeStructArray(out byte[] val)
    {
        int count = ReadInt();
        if (count == 0)
        {
            val = Array.Empty<byte>();
            return;
        }

        ThrowIfOutOfRange(count);

        val = new byte[count];
        fixed (byte* valPtr = val)
        {
            Unsafe.CopyBlock(valPtr, cursor, (uint)count);
        }

        Advance(count);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void DeserializeStructArray(void* value, int count)
    {
        ThrowIfOutOfRange(count);
        Unsafe.CopyBlock(value, cursor, (uint)count);
        Advance(count);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void DeserializeStructArray(out Point32[] val)
    {
        int count = ReadInt();
        if (count == 0)
        {
            val = Array.Empty<Point32>();
            return;
        }

        int size = count * Point32.RosFixedMessageLength;
        ThrowIfOutOfRange(size);

        val = new Point32[count];
        fixed (Point32* valPtr = val)
        {
            Unsafe.CopyBlock(valPtr, cursor, (uint)size);
        }

        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void DeserializeStructArray<T>(out T[] val) where T : unmanaged
    {
        int count = ReadInt();
        if (count == 0)
        {
            val = Array.Empty<T>();
        }
        else
        {
            DeserializeStructArray(count, out val);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void DeserializeStructArray<T>(int count, out T[] val) where T : unmanaged
    {
        int sizeOfT = sizeof(T);
        int size = count * sizeOfT;
        ThrowIfOutOfRange(size);

        val = new T[count];
        fixed (T* valPtr = val)
        {
            Unsafe.CopyBlock(valPtr, cursor, (uint)size);
        }

        Advance(size);
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void DeserializeStructRent(out SharedRent val)
    {
        int count = ReadInt();
        if (count == 0)
        {
            val = SharedRent.Empty;
            return;
        }

        ThrowIfOutOfRange(count);

        val = new SharedRent(count);

        fixed (byte* valPtr = val.Array)
        {
            Unsafe.CopyBlock(valPtr, cursor, (uint)count);
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void DeserializeArray<T>(out T[] val) where T : IMessageRos1, new()
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

    #region Empties

    static string EmptyString => "";
    static string[] EmptyStringArray => Array.Empty<string>();

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
    public static T Deserialize<T>(IDeserializableRos1<T> generator, ReadOnlySpan<byte> buffer)
        where T : ISerializableRos1
    {
        fixed (byte* bufferPtr = buffer)
        {
            var b = new ReadBuffer(bufferPtr, buffer.Length);
            return generator.RosDeserialize(ref b);
        }
    }

    public static T Deserialize<T>(in T generator, ReadOnlySpan<byte> buffer)
        where T : ISerializableRos1, IDeserializableRos1<T>
    {
        fixed (byte* bufferPtr = buffer)
        {
            var b = new ReadBuffer(bufferPtr, buffer.Length);
            return generator.RosDeserialize(ref b);
        }
    }
}