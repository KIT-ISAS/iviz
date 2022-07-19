using System;
using System.Runtime.CompilerServices;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.IvizMsgs;
using Iviz.Msgs.StdMsgs;
using Iviz.Tools;

namespace Iviz.Msgs;

/// <summary>
/// Contains utilities to (de)serialize ROS messages from a byte array. 
/// </summary>
public unsafe struct ReadBuffer
{
    readonly byte* ptr;
    int offset;
    int remaining;

    ReadBuffer(byte* ptr, int length)
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
        this.Deserialize(out int i);
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
        byte* srcPtr = ptr + offset;
        val = count <= 64
            ? BuiltIns.GetStringSimple(srcPtr, count)
            : BuiltIns.UTF8.GetString(srcPtr, count);

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
        const int size = sizeof(short);
        ThrowIfOutOfRange(size);
        t = *(short*)(ptr + offset);
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out ushort t)
    {
        const int size = sizeof(ushort);
        ThrowIfOutOfRange(size);
        t = *(ushort*)(ptr + offset);
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out int t)
    {
        const int size = sizeof(int);
        ThrowIfOutOfRange(size);
        t = *(int*)(ptr + offset);
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out uint t)
    {
        const int size = sizeof(uint);
        ThrowIfOutOfRange(size);
        t = *(uint*)(ptr + offset);
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out time t)
    {
        const int size = 2 * sizeof(uint);
        ThrowIfOutOfRange(size);
        t = *(time*)(ptr + offset);
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out duration t)
    {
        const int size = 2 * sizeof(int);
        ThrowIfOutOfRange(size);
        t = *(duration*)(ptr + offset);
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out float t)
    {
        const int size = sizeof(float);
        ThrowIfOutOfRange(size);
        t = *(float*)(ptr + offset);
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out double t)
    {
        const int size = sizeof(double);
        ThrowIfOutOfRange(size);
        t = *(double*)(ptr + offset);
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out long t)
    {
        const int size = sizeof(long);
        ThrowIfOutOfRange(size);
        t = *(long*)(ptr + offset);
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out ulong t)
    {
        const int size = sizeof(ulong);
        ThrowIfOutOfRange(size);
        t = *(ulong*)(ptr + offset);
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out Vector3 t)
    {
        const int size = Vector3.RosFixedMessageLength;
        ThrowIfOutOfRange(size);
        t = *(Vector3*)(ptr + offset);
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out Point t)
    {
        const int size = Point.RosFixedMessageLength;
        ThrowIfOutOfRange(size);
        t = *(Point*)(ptr + offset);
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out Quaternion t)
    { 
        const int size = Quaternion.RosFixedMessageLength;
        ThrowIfOutOfRange(size);
        t = *(Quaternion*)(ptr + offset);
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out Pose t)
    {
        const int size = Pose.RosFixedMessageLength;
        ThrowIfOutOfRange(size);
        t = *(Pose*)(ptr + offset);
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out Transform t)
    {
        const int size = Transform.RosFixedMessageLength;
        ThrowIfOutOfRange(size);
        t = *(Transform*)(ptr + offset);
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out Point32 t)
    {
        const int size = Point32.RosFixedMessageLength;
        ThrowIfOutOfRange(size);
        t = *(Point32*)(ptr + offset);
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out ColorRGBA t)
    {
        const int size = ColorRGBA.RosFixedMessageLength;
        ThrowIfOutOfRange(size);
        t = *(ColorRGBA*)(ptr + offset);
        Advance(size);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out Color32 t)
    {
        const int size = Color32.RosFixedMessageLength;
        ThrowIfOutOfRange(size);
        t = *(Color32*)(ptr + offset);
        Advance(size);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out Vector3f t)
    {
        const int size = Vector3f.RosFixedMessageLength;
        ThrowIfOutOfRange(size);
        t = *(Vector3f*)(ptr + offset);
        Advance(size);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out Triangle t)
    {
        const int size = Triangle.RosFixedMessageLength;
        ThrowIfOutOfRange(size);
        t = *(Triangle*)(ptr + offset);
        Advance(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out Vector2f t)
    {
        const int size = Triangle.RosFixedMessageLength;
        ThrowIfOutOfRange(size);
        t = *(Vector2f*)(ptr + offset);
        Advance(size);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize<T>(out T t) where T : unmanaged
    {
        int size = sizeof(T);
        ThrowIfOutOfRange(size);
        t = *(T*)(ptr + offset);
        Advance(size);
    }
    
    #endregion

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

    public void DeserializeStructArray<T>(int count, out T[] val) where T : unmanaged
    {
        int sizeOfT = sizeof(T);
        int size = count * sizeOfT;
        ThrowIfOutOfRange(size);

#if NET5_0_OR_GREATER
        val = GC.AllocateUninitializedArray<T>(count);
#else
        val = new T[count];
#endif

        byte* srcPtr = ptr + offset;
        fixed (T* valPtr = val)
        {
            Unsafe.CopyBlock(valPtr, srcPtr, (uint)size);
        }

        Advance(size);
    }

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

        //src.CopyTo(MemoryMarshal.AsBytes(val.AsSpan()));
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
    /// <returns>The deserialized message.</returns>
    public static ISerializable Deserialize(ISerializableRos1 generator, ReadOnlySpan<byte> buffer)
    {
        fixed (byte* bufferPtr = buffer)
        {
            var b = new ReadBuffer(bufferPtr, buffer.Length);
            return generator.RosDeserializeBase(ref b);
        }
    }

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