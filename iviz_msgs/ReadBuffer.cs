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
                BuiltIns.ThrowBufferOverflow(off, remaining);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        int ReadInt()
        {
            this.Deserialize(out int i);
            return i;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DeserializeString(out string val)
        {
            int count = ReadInt();
            if (count == 0)
            {
                val = "";
                return;
            }

            val = BuiltIns.UTF8.GetString(ptr.Slice(offset, count));
            Advance(count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SkipString(out string val)
        {
            int count = ReadInt();
            Advance(count);
            val = "";
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
            int sizeOfT = Unsafe.SizeOf<T>();
            int size = count * sizeOfT;
            var src = ptr.Slice(offset, size);

#if NET5_0_OR_GREATER
            val = GC.AllocateUninitializedArray<T>(count);
#else
            val = new T[count];
#endif
            src.CopyTo(MemoryMarshal.AsBytes(val.AsSpan()));
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

            const int sizeOfT = sizeof(byte);
            int size = count * sizeOfT;
            var src = ptr.Slice(offset, size);

            val = new SharedRent<byte>(count);
            src.CopyTo(MemoryMarshal.AsBytes(val.AsSpan()));

            Advance(size);
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

        public void DeserializeArray<T>(out T[] val) where T : IMessage, new()
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