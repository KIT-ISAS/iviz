﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Iviz.Msgs
{
    /// <summary>
    /// Contains utilities to (de)serialize ROS messages from a byte array. 
    /// </summary>
    public ref struct Buffer
    {
        /// <summary>
        /// Current position.
        /// </summary>
        Span<byte> ptr;

        Buffer(Span<byte> ptr)
        {
            this.ptr = ptr;
        }

        void Advance(uint value)
        {
            ptr = ptr[(int)value..];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static uint SizeOf<T>() where T : unmanaged => (uint)Unsafe.SizeOf<T>();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly void ThrowIfOutOfRange(uint off)
        {
            if (off <= ptr.Length)
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
        static void ThrowIfWrongSize(Array array, uint size)
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
        static void ThrowIfWrongSize(IList array, uint size)
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
        uint ReadUInt() => Deserialize<uint>();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string DeserializeString()
        {
            uint count = ReadUInt();
            if (count == 0)
            {
                return string.Empty;
            }

            ThrowIfOutOfRange(count);
            string val = BuiltIns.UTF8.GetString(ptr[..(int)count]);
            Advance(count);
            return val;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string[] DeserializeStringArray()
        {
            uint count = ReadUInt();
            return count == 0 ? Array.Empty<string>() : DeserializeStringArray(count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string[] DeserializeStringArray(uint count)
        {
            ThrowIfOutOfRange(4 * count);
            string[] val = new string[count];
            for (int i = 0; i < val.Length; i++)
            {
                val[i] = DeserializeString();
            }

            return val;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string[] SkipStringArray()
        {
            uint count = ReadUInt();
            ThrowIfOutOfRange(4 * count);

            for (uint i = 0; i < count; i++)
            {
                uint innerCount = ReadUInt();
                ThrowIfOutOfRange(innerCount);
                Advance(innerCount);
            }

            return Array.Empty<string>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DeserializeStringList(List<string> list)
        {
            uint count = ReadUInt();
            DeserializeStringArray(list, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DeserializeStringArray(List<string> list, uint count)
        {
            list.Clear();
            if (list.Capacity < count) list.Capacity = (int)count;
            for (int i = 0; i < list.Count; i++)
            {
                list[i] = DeserializeString();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Deserialize<T>() where T : unmanaged
        {
            T val = MemoryMarshal.Read<T>(ptr);
            Advance(SizeOf<T>());
            return val;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Deserialize<T>(out T t) where T : unmanaged
        {
            t = MemoryMarshal.Read<T>(ptr);
            Advance(SizeOf<T>());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T[] DeserializeStructArray<T>() where T : unmanaged
        {
            uint count = ReadUInt();
            return count == 0 ? Array.Empty<T>() : DeserializeStructArray<T>(count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T[] DeserializeStructArray<T>(uint count) where T : unmanaged
        {
            uint sizeOfT = SizeOf<T>();
            ThrowIfOutOfRange(count * sizeOfT);

            T[] val = new T[count];
            ptr.CopyTo(MemoryMarshal.AsBytes(val.AsSpan()));

            Advance(count * sizeOfT);
            return val;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DeserializeStructList<T>(List<T> list) where T : unmanaged
        {
            uint count = ReadUInt();
            DeserializeStructList(list, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DeserializeStructList<T>(List<T> list, uint count) where T : unmanaged
        {
            ThrowIfOutOfRange(count * SizeOf<T>());
            list.Clear();
            if (list.Capacity < count)
            {
                list.Capacity = (int)count;
            }

            for (int i = 0; i < count; i++)
            {
                list.Add(Deserialize<T>());
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T[] DeserializeArray<T>() where T : IMessage, new()
        {
            uint count = ReadUInt();
            if (count > 1024 * 1024 * 1024)
            {
                throw new BufferException("Implausible message requested more than 1TB elements.");
            }

            return count == 0 ? Array.Empty<T>() : new T[count];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Serialize<T>(T val) where T : unmanaged
        {
            MemoryMarshal.Write(ptr, ref val);
            Advance(SizeOf<T>());
        }

        public void Serialize<T>(ref T val) where T : unmanaged
        {
            MemoryMarshal.Write(ptr, ref val);
            Advance(SizeOf<T>());
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

            uint count = (uint)BuiltIns.UTF8.GetByteCount(val);
            ThrowIfOutOfRange(4 + count);
            WriteInt((int)count);
            if (count == 0)
            {
                return;
            }

            BuiltIns.UTF8.GetBytes(val.AsSpan(), ptr);
            Advance(count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SerializeArray(string[] val)
        {
            WriteInt(val.Length);
            foreach (string str in val)
            {
                Serialize(str);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SerializeArray(string[] val, uint count)
        {
            ThrowIfWrongSize(val, count);
            foreach (string str in val)
            {
                Serialize(str);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SerializeArray(List<string> val, uint count = 0)
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SerializeStructArray<T>(T[] val) where T : unmanaged
        {
            uint sizeOfT = SizeOf<T>();
            uint size = (uint)val.Length * sizeOfT;
            ThrowIfOutOfRange(4 + size);
            WriteInt(val.Length);

            MemoryMarshal.AsBytes(val.AsSpan()).CopyTo(ptr);
            Advance(size);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SerializeStructArray<T>(T[] val, uint count) where T : unmanaged
        {
            ThrowIfWrongSize(val, count);

            uint sizeOfT = SizeOf<T>();
            uint size = count * sizeOfT;

            ThrowIfOutOfRange(size);
            MemoryMarshal.AsBytes(val.AsSpan()).CopyTo(ptr);
            Advance(size);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SerializeStructList<T>(List<T> val, uint count = 0) where T : unmanaged
        {
            uint sizeOfT = SizeOf<T>();
            if (count == 0)
            {
                ThrowIfOutOfRange(4 + (uint)val.Count * sizeOfT);
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SerializeArray<T>(T[] val) where T : IMessage
        {
            WriteInt(val.Length);
            foreach (T t in val)
            {
                t.RosSerialize(ref this);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SerializeArray<T>(T[] val, uint count) where T : IMessage
        {
            ThrowIfWrongSize(val, count);
            foreach (T t in val)
            {
                t.RosSerialize(ref this);
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
        /// <param name="size">
        /// Optional. The expected size of the message inside of the array. Must be less or equal the array size.
        /// </param>
        /// <param name="offset">Optional. Offset within the array.</param>
        /// <typeparam name="T">Message type.</typeparam>
        /// <returns>The deserialized message.</returns>
        public static T Deserialize<T>(T generator, byte[] buffer, int size = -1, int offset = 0)
            where T : ISerializable
        {
            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }

            if (generator == null)
            {
                throw new ArgumentNullException(nameof(generator));
            }

            if (offset < 0 || buffer.Length < offset)
            {
                throw new ArgumentOutOfRangeException(nameof(offset));
            }

            if (size == -1)
            {
                size = buffer.Length - offset;
            }
            else
            {
                if (size < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(size));
                }

                if (size > buffer.Length - offset)
                {
                    throw new ArgumentOutOfRangeException(nameof(offset));
                }
            }

            /*
            fixed (byte* bPtr = buffer)
            {
                var b = new Buffer(bPtr + offset, bPtr + offset + size);
                return (T)generator.RosDeserialize(ref b);
            }
            */
            var b = new Buffer(buffer.AsSpan(offset, size));
            return (T)generator.RosDeserialize(ref b);
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
        /// <param name="size">
        /// Optional. The expected size of the message inside of the array. Must be less or equal the array size.
        /// </param>
        /// <param name="offset">Optional. Offset within the array.</param>
        /// <typeparam name="T">Message type.</typeparam>
        /// <returns>The deserialized message.</returns>
        public static T Deserialize<T>(IDeserializable<T> generator, byte[] buffer, int size = -1,
            int offset = 0)
            where T : ISerializable
        {
            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }

            if (offset < 0 || buffer.Length < offset)
            {
                throw new ArgumentOutOfRangeException(nameof(offset));
            }

            if (generator == null)
            {
                throw new ArgumentNullException(nameof(generator));
            }

            if (size == -1)
            {
                size = buffer.Length - offset;
            }
            else
            {
                if (size < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(size));
                }

                if (size > buffer.Length - offset)
                {
                    throw new ArgumentOutOfRangeException(nameof(offset));
                }
            }

            /*
            fixed (byte* bPtr = buffer)
            {
                var b = new Buffer(bPtr + offset, bPtr + offset + size);
                return generator.RosDeserialize(ref b);
            }
            */
            var b = new Buffer(buffer.AsSpan(offset, size));
            return generator.RosDeserialize(ref b);
        }

        /// <summary>
        /// Serializes the given message into the buffer array.
        /// </summary>
        /// <param name="message">The ROS message.</param>
        /// <param name="buffer">The destination byte array.</param>
        /// <param name="offset">Optional offset at which to start writing</param>
        /// <returns>The number of bytes written.</returns>
        internal static uint Serialize<T>(in T message, byte[] buffer, int offset = 0) where T : ISerializable
        {
            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }

            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            var b = new Buffer(buffer.AsSpan(offset, buffer.Length - offset));
            int oldLength = b.ptr.Length;

            message.RosSerialize(ref b);
            int newLength = b.ptr.Length;

            return (uint)(oldLength - newLength);

            /*
            fixed (byte* bPtr = buffer)
            {
                var b = new Buffer(bPtr + offset, bPtr + buffer.Length);
                message.RosSerialize(ref b);
                return (uint)(b.ptr - bPtr - offset);
            }
        */
        }
    }
}

/*

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Iviz.Msgs
{
    /// <summary>
    /// Contains utilities to (de)serialize ROS messages from a byte array. 
    /// </summary>
    public struct Buffer
    {
        /// <summary>
        /// Current position.
        /// </summary>
        unsafe byte* ptr;

        /// <summary>
        /// Maximal position.
        /// </summary>
        readonly unsafe byte* end;

        unsafe Buffer(byte* ptr, byte* end)
        {
            this.ptr = ptr;
            this.end = end;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static unsafe void MemoryCopy(void* dst, void* src, uint size)
        {
            System.Buffer.MemoryCopy(src, dst, size, size);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly unsafe void ThrowIfOutOfRange(uint off)
        {
            if (ptr + off <= end)
            {
                return;
            }

            if (ptr == default && end == default)
            {
                throw new BufferException("Buffer has not been initialized!");
            }

            throw new BufferException($"Requested {off} bytes, but only {end - ptr} remain!");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static void ThrowIfWrongSize(Array array, uint size)
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
        static void ThrowIfWrongSize(IList array, uint size)
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
        uint ReadUInt() => Deserialize<uint>();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe string DeserializeString()
        {
            uint count = ReadUInt();
            if (count == 0)
            {
                return string.Empty;
            }

            ThrowIfOutOfRange(count);
            string val = BuiltIns.UTF8.GetString(ptr, (int)count);
            ptr += count;
            return val;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string[] DeserializeStringArray()
        {
            uint count = ReadUInt();
            return count == 0 ? Array.Empty<string>() : DeserializeStringArray(count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string[] DeserializeStringArray(uint count)
        {
            ThrowIfOutOfRange(4 * count);
            string[] val = new string[count];
            for (int i = 0; i < val.Length; i++)
            {
                val[i] = DeserializeString();
            }

            return val;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe string[] SkipStringArray()
        {
            uint count = ReadUInt();
            ThrowIfOutOfRange(4 * count);

            for (uint i = 0; i < count; i++)
            {
                uint innerCount = ReadUInt();
                ThrowIfOutOfRange(innerCount);

                ptr += innerCount;
            }

            return Array.Empty<string>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DeserializeStringList(List<string> list)
        {
            uint count = ReadUInt();
            DeserializeStringArray(list, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DeserializeStringArray(List<string> list, uint count)
        {
            list.Clear();
            if (list.Capacity < count) list.Capacity = (int)count;
            for (int i = 0; i < list.Count; i++)
            {
                list[i] = DeserializeString();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe T Deserialize<T>() where T : unmanaged
        {
            ThrowIfOutOfRange((uint)sizeof(T));
            T val = *(T*)ptr;
            ptr += sizeof(T);
            return val;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void Deserialize<T>(out T t) where T : unmanaged
        {
            ThrowIfOutOfRange((uint)sizeof(T));
            t = *(T*)ptr;
            ptr += sizeof(T);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T[] DeserializeStructArray<T>() where T : unmanaged
        {
            uint count = ReadUInt();
            return count == 0 ? Array.Empty<T>() : DeserializeStructArray<T>(count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe T[] DeserializeStructArray<T>(uint count) where T : unmanaged
        {
            ThrowIfOutOfRange(count * (uint)sizeof(T));
            T[] val = new T[count];
            fixed (T* bPtr = val)
            {
                uint size = count * (uint)sizeof(T);
                MemoryCopy(bPtr, ptr, size);
                ptr += size;
            }

            return val;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DeserializeStructList<T>(List<T> list) where T : unmanaged
        {
            uint count = ReadUInt();
            DeserializeStructList(list, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void DeserializeStructList<T>(List<T> list, uint count) where T : unmanaged
        {
            ThrowIfOutOfRange(count * (uint)sizeof(T));
            list.Clear();
            if (list.Capacity < count)
            {
                list.Capacity = (int)count;
            }

            T* tPtr = (T*)ptr;
            for (int i = 0; i < count; i++)
            {
                list.Add(*tPtr++);
            }

            uint size = count * (uint)sizeof(T);
            ptr += size;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T[] DeserializeArray<T>() where T : IMessage, new()
        {
            uint count = ReadUInt();
            if (count > 1024 * 1024 * 1024)
            {
                throw new BufferException("Implausible message requested more than 1TB elements.");
            }

            return count == 0 ? Array.Empty<T>() : new T[count];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void Serialize<T>(in T val) where T : unmanaged
        {
            ThrowIfOutOfRange((uint)sizeof(T));
            *(T*)ptr = val;
            ptr += sizeof(T);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void WriteInt(int value) => Serialize(value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void Serialize(string val)
        {
            int count = BuiltIns.UTF8.GetByteCount(val);
            ThrowIfOutOfRange(4 + (uint)count);
            WriteInt(count);
            if (count == 0)
            {
                return;
            }

            fixed (char* bPtr = val)
            {
                BuiltIns.UTF8.GetBytes(bPtr, val.Length, ptr, count);
                ptr += count;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SerializeArray(string[] val)
        {
            WriteInt(val.Length);
            foreach (string str in val)
            {
                Serialize(str);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SerializeArray(string[] val, uint count)
        {
            ThrowIfWrongSize(val, count);
            foreach (string str in val)
            {
                Serialize(str);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SerializeArray(List<string> val, uint count = 0)
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void SerializeStructArray<T>(T[] val) where T : unmanaged
        {
            ThrowIfOutOfRange((uint)(4 + val.Length * sizeof(T)));
            WriteInt(val.Length);

            fixed (T* bPtr = val)
            {
                uint size = (uint)(val.Length * sizeof(T));
                MemoryCopy(ptr, bPtr, size);
                ptr += size;
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void SerializeStructArray<T>(T[] val, uint count) where T : unmanaged
        {
            ThrowIfWrongSize(val, count);
            ThrowIfOutOfRange(count * (uint)sizeof(T));

            fixed (T* bPtr = val)
            {
                uint size = (uint)(val.Length * sizeof(T));
                MemoryCopy(ptr, bPtr, size);
                ptr += size;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void SerializeStructList<T>(List<T> val, uint count = 0) where T : unmanaged
        {
            if (count == 0)
            {
                ThrowIfOutOfRange((uint)(4 + val.Count * sizeof(T)));
                WriteInt(val.Count);
            }
            else
            {
                ThrowIfWrongSize(val, count);
                ThrowIfOutOfRange(count * (uint)sizeof(T));
            }

            T* tPtr = (T*)ptr;
            foreach (T v in val)
            {
                *tPtr++ = v;
            }

            uint size = (uint)(val.Count * sizeof(T));
            ptr += size;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SerializeArray<T>(T[] val) where T : IMessage
        {
            WriteInt(val.Length);
            foreach (T t in val)
            {
                t.RosSerialize(ref this);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SerializeArray<T>(T[] val, uint count) where T : IMessage
        {
            ThrowIfWrongSize(val, count);
            foreach (T t in val)
            {
                t.RosSerialize(ref this);
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
        /// <param name="size">
        /// Optional. The expected size of the message inside of the array. Must be less or equal the array size.
        /// </param>
        /// <param name="offset">Optional. Offset within the array.</param>
        /// <typeparam name="T">Message type.</typeparam>
        /// <returns>The deserialized message.</returns>
        public static unsafe T Deserialize<T>(T generator, byte[] buffer, int size = -1, int offset = 0)
            where T : ISerializable
        {
            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }

            if (generator == null)
            {
                throw new ArgumentNullException(nameof(generator));
            }

            if (offset < 0 || buffer.Length < offset)
            {
                throw new ArgumentOutOfRangeException(nameof(offset));
            }

            if (size == -1)
            {
                size = buffer.Length - offset;
            }
            else
            {
                if (size < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(size));
                }

                if (size > buffer.Length - offset)
                {
                    throw new ArgumentOutOfRangeException(nameof(offset));
                }
            }

            fixed (byte* bPtr = buffer)
            {
                var b = new Buffer(bPtr + offset, bPtr + offset + size);
                return (T)generator.RosDeserialize(ref b);
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
        /// <param name="size">
        /// Optional. The expected size of the message inside of the array. Must be less or equal the array size.
        /// </param>
        /// <param name="offset">Optional. Offset within the array.</param>
        /// <typeparam name="T">Message type.</typeparam>
        /// <returns>The deserialized message.</returns>
        public static unsafe T Deserialize<T>(IDeserializable<T> generator, byte[] buffer, int size = -1,
            int offset = 0)
            where T : ISerializable
        {
            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }

            if (offset < 0 || buffer.Length < offset)
            {
                throw new ArgumentOutOfRangeException(nameof(offset));
            }

            if (generator == null)
            {
                throw new ArgumentNullException(nameof(generator));
            }

            if (size == -1)
            {
                size = buffer.Length - offset;
            }
            else
            {
                if (size < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(size));
                }

                if (size > buffer.Length - offset)
                {
                    throw new ArgumentOutOfRangeException(nameof(offset));
                }
            }

            fixed (byte* bPtr = buffer)
            {
                var b = new Buffer(bPtr + offset, bPtr + offset + size);
                return generator.RosDeserialize(ref b);
            }
        }

        /// <summary>
        /// Serializes the given message into the buffer array.
        /// </summary>
        /// <param name="message">The ROS message.</param>
        /// <param name="buffer">The destination byte array.</param>
        /// <param name="offset">Optional offset at which to start writing</param>
        /// <returns>The number of bytes written.</returns>
        internal static unsafe uint Serialize<T>(in T message, byte[] buffer, int offset = 0) where T : ISerializable
        {
            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }

            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            fixed (byte* bPtr = buffer)
            {
                var b = new Buffer(bPtr + offset, bPtr + buffer.Length);
                message.RosSerialize(ref b);
                return (uint)(b.ptr - bPtr - offset);
            }
        }
    }
}
*/