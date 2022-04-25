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
            if (off > remaining)
            {
                ThrowBufferException(off, remaining);
            }
        }

        [DoesNotReturn]
        static void ThrowBufferException(int off, int remaining) =>
            throw new BufferException($"Requested {off} bytes, but only {remaining} remain!");

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        int ReadInt()
        {
            this.Deserialize(out int i);
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

#if NETSTANDARD2_1
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal ref byte GetRefAndAdvance(int size)
        {
            ThrowIfOutOfRange(size);
            ref byte result = ref ptr[offset].AsRef();
            Advance(size);
            return ref result;
        }
#else
        public void Deserialize<T>(out T t) where T : unmanaged
        {
            int size = Unsafe.SizeOf<T>();
            ThrowIfOutOfRange(size);
            t = Unsafe.ReadUnaligned<T>(ref Unsafe.AsRef(in ptr[offset]));
            Advance(size);
        }
#endif

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