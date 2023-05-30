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
        byte* next = cursor + sizeof(int);
        if (next > end) BuiltIns.ThrowBufferOverflow();
        int t = Unsafe.ReadUnaligned<int>(cursor);
        cursor = next;
        return t;
    }

    #region strings

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string DeserializeString()
    {
        int length = ReadInt();
        if (length == 0)
        {
            return EmptyString;
        }

        ThrowIfOutOfRange(length);
        if (length == 1)
        {
            Advance(1);
            return EmptyString; 
        }

        int countWithoutZero = length - 1;
        string val = BuiltIns.GetString(cursor, countWithoutZero);

        Advance(length);
        return val;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string SkipString()
    {
        int length = ReadInt();
        ThrowIfOutOfRange(length);
        Advance(length);
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
        byte* next = cursor + 1;
        if (next > end) BuiltIns.ThrowBufferOverflow();
        t = *(bool*)cursor;
        cursor = next;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out byte t)
    {
        byte* next = cursor + sizeof(byte);
        if (next > end) BuiltIns.ThrowBufferOverflow();
        t = *cursor;
        cursor = next;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out sbyte t)
    {
        byte* next = cursor + sizeof(sbyte);
        if (next > end) BuiltIns.ThrowBufferOverflow();
        t = *(sbyte*)cursor;
        cursor = next;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out short t)
    {
        byte* next = cursor + sizeof(short);
        if (next > end) BuiltIns.ThrowBufferOverflow();
        t = Unsafe.ReadUnaligned<short>(cursor);
        cursor = next;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out ushort t)
    {
        byte* next = cursor + sizeof(ushort);
        if (next > end) BuiltIns.ThrowBufferOverflow();
        t = Unsafe.ReadUnaligned<ushort>(cursor);
        cursor = next;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out int t)
    {
        byte* next = cursor + sizeof(int);
        if (next > end) BuiltIns.ThrowBufferOverflow();
        t = Unsafe.ReadUnaligned<int>(cursor);
        cursor = next;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out uint t)
    {
        byte* next = cursor + sizeof(uint);
        if (next > end) BuiltIns.ThrowBufferOverflow();
        t = Unsafe.ReadUnaligned<uint>(cursor);
        cursor = next;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out time t)
    {
        byte* next = cursor + 2 * sizeof(uint);
        if (next > end) BuiltIns.ThrowBufferOverflow();
        t = Unsafe.ReadUnaligned<time>(cursor);
        cursor = next;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out duration t)
    {
        byte* next = cursor + 2 * sizeof(int);
        if (next > end) BuiltIns.ThrowBufferOverflow();
        t = Unsafe.ReadUnaligned<duration>(cursor);
        cursor = next;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out float t)
    {
        byte* next = cursor + sizeof(float);
        if (next > end) BuiltIns.ThrowBufferOverflow();
        t = Unsafe.ReadUnaligned<float>(cursor);
        cursor = next;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out double t)
    {
        byte* next = cursor + sizeof(double);
        if (next > end) BuiltIns.ThrowBufferOverflow();
        t = Unsafe.ReadUnaligned<double>(cursor);
        cursor = next;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out long t)
    {
        byte* next = cursor + sizeof(long);
        if (next > end) BuiltIns.ThrowBufferOverflow();
        t = Unsafe.ReadUnaligned<long>(cursor);
        cursor = next;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out ulong t)
    {
        byte* next = cursor + sizeof(ulong);
        if (next > end) BuiltIns.ThrowBufferOverflow();
        t = Unsafe.ReadUnaligned<ulong>(cursor);
        cursor = next;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize<T>(out T t) where T : unmanaged
    {
        byte* next = cursor + sizeof(T);
        if (next > end) BuiltIns.ThrowBufferOverflow();
        t = Unsafe.ReadUnaligned<T>(cursor);
        cursor = next;
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
        const int size = Vector3.RosFixedMessageLength;
        byte* next = cursor + size;
        if (next > end) BuiltIns.ThrowBufferOverflow();
        t = Unsafe.ReadUnaligned<Vector3>(cursor);
        cursor = next;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out Point t)
    {
        const int size = Point.RosFixedMessageLength;
        byte* next = cursor + size;
        if (next > end) BuiltIns.ThrowBufferOverflow();
        t = Unsafe.ReadUnaligned<Point>(cursor);
        cursor = next;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out Quaternion t)
    {
        const int size = Quaternion.RosFixedMessageLength;
        byte* next = cursor + size;
        if (next > end) BuiltIns.ThrowBufferOverflow();
        t = Unsafe.ReadUnaligned<Quaternion>(cursor);
        cursor = next;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out Pose t)
    {
        const int size = Pose.RosFixedMessageLength;
        byte* next = cursor + size;
        if (next > end) BuiltIns.ThrowBufferOverflow();
        t = Unsafe.ReadUnaligned<Pose>(cursor);
        cursor = next;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out Transform t)
    {
        const int size = Transform.RosFixedMessageLength;
        byte* next = cursor + size;
        if (next > end) BuiltIns.ThrowBufferOverflow();
        t = Unsafe.ReadUnaligned<Transform>(cursor);
        cursor = next;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out ColorRGBA t)
    {
        const int size = ColorRGBA.RosFixedMessageLength;
        byte* next = cursor + size;
        if (next > end) BuiltIns.ThrowBufferOverflow();
        t = Unsafe.ReadUnaligned<ColorRGBA>(cursor);
        cursor = next;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deserialize(out Color32 t)
    {
        const int size = Color32.RosFixedMessageLength;
        byte* next = cursor + size;
        if (next > end) BuiltIns.ThrowBufferOverflow();
        t = Unsafe.ReadUnaligned<Color32>(cursor);
        cursor = next;
    }

    #endregion
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void DeserializeStructArray<T>(T[] array) where T : unmanaged
    {
        DeserializeStructArray(ref Unsafe.As<T, byte>(ref array[0]), array.Length * sizeof(T));
    }
}