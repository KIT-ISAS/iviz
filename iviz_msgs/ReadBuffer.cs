using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Iviz.Tools;

namespace Iviz.Msgs
{
    /// <summary>
    /// Contains utilities to (de)serialize ROS messages from a byte array. 
    /// </summary>
    public ref struct ReadBuffer
    {
        /// <summary>
        /// Current position.
        /// </summary>
        readonly ReadOnlySpan<byte> ptr;

        int offset;
        int remaining;

        ReadBuffer(ReadOnlySpan<byte> ptr)
        {
            this.ptr = ptr;
            offset = 0;
            remaining = ptr.Length;
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
            if (off <= remaining)
            {
                return;
            }

            if (ptr == default)
            {
                throw new BufferException("Buffer has not been initialized!");
            }

            throw new BufferException($"Requested {off} bytes, but only {remaining} remain!");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        int ReadInt()
        {
            Deserialize(out int i);
            return i;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string DeserializeString()
        {
            int count = ReadInt();
            if (count == 0)
            {
                return "";
            }

            string val = BuiltIns.UTF8.GetString(ptr.Slice(offset, count));
            Advance(count);
            return val;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string SkipString()
        {
            int count = ReadInt();
            Advance(count);
            return "";
        }

        public string[] DeserializeStringArray()
        {
            int count = ReadInt();
            return count == 0 ? Array.Empty<string>() : DeserializeStringArray(count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining), SkipLocalsInit]
        public string[] DeserializeStringArray(int count)
        {
            ThrowIfOutOfRange(4 * count);
            string[] val = new string[count];
            for (int i = 0; i < val.Length; i++)
            {
                val[i] = DeserializeString();
            }

            return val;
        }

        public string[] SkipStringArray()
        {
            int count = ReadInt();
            for (int i = 0; i < count; i++)
            {
                int innerCount = ReadInt();
                Advance(innerCount);
            }

            return Array.Empty<string>();
        }

        /*
        public void DeserializeStringList(List<string> list)
        {
            int count = ReadInt();
            DeserializeStringArray(list, count);
        }
        */

        public void DeserializeStringArray(List<string> list, int count)
        {
            list.Clear();
            if (list.Capacity < count)
            {
                list.Capacity = count;
            }

            for (int i = 0; i < list.Count; i++)
            {
                list[i] = DeserializeString();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Deserialize<T>(out T t) where T : unmanaged
        {
            ThrowIfOutOfRange(Unsafe.SizeOf<T>());
            t = Unsafe.As<byte, T>(ref CurrentOffset());
            Advance(Unsafe.SizeOf<T>());
        }

        #region Deserializers

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Deserialize(out byte t)
        {
            ThrowIfOutOfRange(sizeof(byte));
            t = CurrentOffset();
            Advance(sizeof(byte));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Deserialize(out sbyte t)
        {
            ThrowIfOutOfRange(sizeof(sbyte));
            t = (sbyte)CurrentOffset();
            Advance(sizeof(sbyte));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Deserialize(out short t)
        {
            ThrowIfOutOfRange(sizeof(short));
            t = Unsafe.As<byte, short>(ref CurrentOffset());
            Advance(sizeof(short));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Deserialize(out ushort t)
        {
            ThrowIfOutOfRange(sizeof(ushort));
            t = Unsafe.As<byte, ushort>(ref CurrentOffset());
            Advance(sizeof(ushort));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Deserialize(out int t)
        {
            ThrowIfOutOfRange(sizeof(int));
            t = Unsafe.As<byte, int>(ref CurrentOffset());
            Advance(sizeof(int));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Deserialize(out uint t)
        {
            ThrowIfOutOfRange(sizeof(uint));
            t = Unsafe.As<byte, uint>(ref CurrentOffset());
            Advance(sizeof(uint));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Deserialize(out long t)
        {
            ThrowIfOutOfRange(sizeof(long));
            t = Unsafe.As<byte, long>(ref CurrentOffset());
            Advance(sizeof(long));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Deserialize(out ulong t)
        {
            ThrowIfOutOfRange(sizeof(ulong));
            t = Unsafe.As<byte, ulong>(ref CurrentOffset());
            Advance(sizeof(ulong));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Deserialize(out float t)
        {
            ThrowIfOutOfRange(sizeof(float));
            t = Unsafe.As<byte, float>(ref CurrentOffset());
            Advance(sizeof(float));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Deserialize(out double t)
        {
            ThrowIfOutOfRange(sizeof(double));
            t = Unsafe.As<byte, double>(ref CurrentOffset());
            Advance(sizeof(double));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Deserialize(out bool t)
        {
            ThrowIfOutOfRange(sizeof(bool));
            t = Unsafe.As<byte, bool>(ref CurrentOffset());
            Advance(sizeof(bool));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Deserialize(out duration t)
        {
            ThrowIfOutOfRange(Unsafe.SizeOf<duration>());
            t = Unsafe.As<byte, duration>(ref CurrentOffset());
            Advance(Unsafe.SizeOf<duration>());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Deserialize(out time t)
        {
            ThrowIfOutOfRange(Unsafe.SizeOf<time>());
            t = Unsafe.As<byte, time>(ref CurrentOffset());
            Advance(Unsafe.SizeOf<time>());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Deserialize(out GeometryMsgs.Quaternion t)
        {
            ThrowIfOutOfRange(Unsafe.SizeOf<GeometryMsgs.Quaternion>());
            t = Unsafe.As<byte, GeometryMsgs.Quaternion>(ref CurrentOffset());
            Advance(Unsafe.SizeOf<GeometryMsgs.Quaternion>());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Deserialize(out GeometryMsgs.Vector3 t)
        {
            ThrowIfOutOfRange(Unsafe.SizeOf<GeometryMsgs.Vector3>());
            t = Unsafe.As<byte, GeometryMsgs.Vector3>(ref CurrentOffset());
            Advance(Unsafe.SizeOf<GeometryMsgs.Vector3>());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Deserialize(out GeometryMsgs.Point t)
        {
            ThrowIfOutOfRange(Unsafe.SizeOf<GeometryMsgs.Point>());
            t = Unsafe.As<byte, GeometryMsgs.Point>(ref CurrentOffset());
            Advance(Unsafe.SizeOf<GeometryMsgs.Point>());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Deserialize(out GeometryMsgs.Pose t)
        {
            ThrowIfOutOfRange(Unsafe.SizeOf<GeometryMsgs.Pose>());
            t = Unsafe.As<byte, GeometryMsgs.Pose>(ref CurrentOffset());
            Advance(Unsafe.SizeOf<GeometryMsgs.Pose>());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Deserialize(out GeometryMsgs.Transform t)
        {
            ThrowIfOutOfRange(Unsafe.SizeOf<GeometryMsgs.Transform>());
            t = Unsafe.As<byte, GeometryMsgs.Transform>(ref CurrentOffset());
            Advance(Unsafe.SizeOf<GeometryMsgs.Transform>());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Deserialize(out StdMsgs.ColorRGBA t)
        {
            ThrowIfOutOfRange(Unsafe.SizeOf<StdMsgs.ColorRGBA>());
            t = Unsafe.As<byte, StdMsgs.ColorRGBA>(ref CurrentOffset());
            Advance(Unsafe.SizeOf<StdMsgs.ColorRGBA>());
        }

        #endregion

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        unsafe ref byte CurrentOffset() =>
            // we use unsafe here because this is kind of a hot path and
            // unity's il2cpp code for MemoryMarshal is really slow 
            ref *(byte*)ptr[offset];

        public T[] DeserializeStructArray<T>() where T : unmanaged
        {
            int count = ReadInt();
            return count == 0 ? Array.Empty<T>() : DeserializeStructArray<T>(count);
        }

#if NETSTANDARD2_1
        [SkipLocalsInit]
#endif
        public T[] DeserializeStructArray<T>(int count) where T : unmanaged
        {
            int sizeOfT = Unsafe.SizeOf<T>();
            int size = count * sizeOfT;
            var src = ptr.Slice(offset, size);

#if NET5_0_OR_GREATER
            T[] val = GC.AllocateUninitializedArray<T>(count);
#else
            T[] val = new T[count];
#endif

            src.CopyTo(MemoryMarshal.AsBytes(val.AsSpan()));

            Advance(size);
            return val;
        }

        public SharedRent<T> DeserializeStructRent<T>() where T : unmanaged
        {
            int count = ReadInt();
            if (count == 0)
            {
                return SharedRent<T>.Empty;
            }

            int sizeOfT = Unsafe.SizeOf<T>();
            int size = count * sizeOfT;
            var src = ptr.Slice(offset, size);

            var rent = new SharedRent<T>(count);
            src.CopyTo(MemoryMarshal.AsBytes(rent.AsSpan()));

            Advance(size);
            return rent;
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

        /*
        public void DeserializeStructList<T>(List<T> list) where T : unmanaged
        {
            int count = ReadInt();
            DeserializeStructList(list, count);
        }

        public void DeserializeStructList<T>(List<T> list, int count) where T : unmanaged
        {
            ThrowIfOutOfRange(count * Unsafe.SizeOf<T>());
            list.Clear();
            if (list.Capacity < count)
            {
                list.Capacity = count;
            }

            for (int i = 0; i < count; i++)
            {
                Deserialize(out T t);
                list.Add(t);
            }
        }
        */

        public T[] DeserializeArray<T>() where T : IMessage, new()
        {
            int count = ReadInt();
            if (count == 0)
            {
                return Array.Empty<T>();
            }

            if (count <= 1024 * 1024 * 1024)
            {
                return new T[count];
                // entry deserializations happen outside
            }

            throw new BufferException("Implausible message requested more than 1TB elements.");
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
        public static ISerializable Deserialize(ISerializable generator, ReadOnlySpan<byte> buffer)
        {
            var b = new ReadBuffer(buffer);
            return generator.RosDeserializeBase(ref b);
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
        public static T Deserialize<T>(IDeserializable<T> generator, ReadOnlySpan<byte> buffer)
            where T : ISerializable
        {
            var b = new ReadBuffer(buffer);
            return generator.RosDeserialize(ref b);
        }

        public static T Deserialize<T>(in T generator, ReadOnlySpan<byte> buffer)
            where T : ISerializable, IDeserializable<T>
        {
            var b = new ReadBuffer(buffer);
            return generator.RosDeserialize(ref b);
        }
    }
}