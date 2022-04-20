using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Iviz.Tools;

namespace Iviz.Msgs
{
    /// <summary>
    /// Contains utilities to (de)serialize ROS messages from a byte array. 
    /// </summary>
    public ref struct WriteBuffer
    {
        /// <summary>
        /// Current position.
        /// </summary>
        Span<byte> ptr;

        WriteBuffer(Span<byte> ptr)
        {
            this.ptr = ptr;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void Advance(int value)
        {
            ptr = ptr[value..];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly void ThrowIfOutOfRange(int off)
        {
            if (0 <= off && off <= ptr.Length)
            {
                return;
            }

            if (ptr == default)
            {
                throw new BufferException("Buffer has not been initialized!");
            }

            throw new BufferException($"Requested {off} bytes, but only {ptr.Length} remain!");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static void ThrowIfWrongSize(Array array, int size)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (array.Length != size)
            {
                throw new IndexOutOfRangeException(
                    $"Cannot write {array.Length} values into array of fixed size {size}.");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static void ThrowIfWrongSize(IList array, int size)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (array.Count != size)
            {
                throw new IndexOutOfRangeException(
                    $"Cannot write {array.Count} values into array of fixed size {size}.");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Serialize<T>(T val) where T : unmanaged
        {
            MemoryMarshal.Write(ptr, ref val);
            Advance(Unsafe.SizeOf<T>());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Serialize<T>(in T val) where T : unmanaged
        {
            ref T valRef = ref Unsafe.AsRef(in val);
            MemoryMarshal.Write(ptr, ref valRef); // valRef is not written to!
            Advance(Unsafe.SizeOf<T>());
        }

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
            ThrowIfOutOfRange(4 + count);
            WriteInt(count);
            BuiltIns.UTF8.GetBytes(val.AsSpan(), ptr);
            Advance(count);
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SerializeArray(List<string> val, int count = 0)
        {
            if (count == 0)
            {
                WriteInt(val.Count);
            }
            else
            {
                ThrowIfWrongSize(val, count);
            }

            foreach (string str in val)
            {
                Serialize(str);
            }
        }

        public void SerializeStructArray<T>(T[] val) where T : unmanaged
        {
            int sizeOfT = Unsafe.SizeOf<T>();
            int size = val.Length * sizeOfT;
            ThrowIfOutOfRange(4 + size);
            
            WriteInt(val.Length);
            MemoryMarshal.AsBytes(new ReadOnlySpan<T>(val)).CopyTo(ptr);
            
            Advance(size);
        }
        
        public void SerializeStructArray<T>(SharedRent<T> val) where T : unmanaged
        {
            int sizeOfT = Unsafe.SizeOf<T>();
            int size = val.Length * sizeOfT;
            ThrowIfOutOfRange(4 + size);
            
            WriteInt(val.Length);
            MemoryMarshal.AsBytes(val.AsSpan()).CopyTo(ptr);
            
            Advance(size);
        }

        public void SerializeStructArray<T>(T[] val, int count) where T : unmanaged
        {
            ThrowIfWrongSize(val, count);

            int sizeOfT = Unsafe.SizeOf<T>();
            int size = count * sizeOfT;

            ThrowIfOutOfRange(size);
            MemoryMarshal.AsBytes(new ReadOnlySpan<T>(val)).CopyTo(ptr);
            Advance(size);
        }

        public void SerializeStructList<T>(List<T> val, int count = 0) where T : unmanaged
        {
            int sizeOfT = Unsafe.SizeOf<T>();
            if (count == 0)
            {
                ThrowIfOutOfRange(4 + val.Count * sizeOfT);
                WriteInt(val.Count);
            }
            else
            {
                ThrowIfWrongSize(val, count);
                ThrowIfOutOfRange(count * sizeOfT);
            }

            foreach (T v in val)
            {
                Serialize(v);
            }
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
        /// <param name="dest">The destination byte array.</param>
        /// <returns>The number of bytes written.</returns>
        internal static uint Serialize<T>(in T message, Span<byte> dest) where T : ISerializable
        {
            var b = new WriteBuffer(dest);
            message.RosSerialize(ref b);

            int oldLength = dest.Length;
            int newLength = b.ptr.Length;
            
            return (uint)(oldLength - newLength);
        }        
    }
}
