using System;
using System.Buffers;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

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
        ReadOnlySpan<byte> ptr;

        ReadBuffer(ReadOnlySpan<byte> ptr)
        {
            this.ptr = ptr;
        }

        void Advance(int value)
        {
            ptr = ptr[value..];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static int SizeOf<T>() where T : unmanaged => Unsafe.SizeOf<T>();

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
        int ReadInt() => Deserialize<int>();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string DeserializeString()
        {
            int count = ReadInt();
            if (count == 0)
            {
                return string.Empty;
            }

            ThrowIfOutOfRange(count);
            string val = BuiltIns.UTF8.GetString(ptr[..count]);
            Advance(count);
            return val;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string[] DeserializeStringArray()
        {
            int count = ReadInt();
            return count == 0 ? Array.Empty<string>() : DeserializeStringArray(count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string[] SkipStringArray()
        {
            int count = ReadInt();
            ThrowIfOutOfRange(4 * count);

            for (int i = 0; i < count; i++)
            {
                int innerCount = ReadInt();
                ThrowIfOutOfRange(innerCount);
                Advance(innerCount);
            }

            return Array.Empty<string>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DeserializeStringList(List<string> list)
        {
            int count = ReadInt();
            DeserializeStringArray(list, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
            int count = ReadInt();
            return count == 0 ? Array.Empty<T>() : DeserializeStructArray<T>(count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T[] DeserializeStructArray<T>(int count) where T : unmanaged
        {
            int sizeOfT = SizeOf<T>();
            int size = count * sizeOfT;
            ThrowIfOutOfRange(size);

            T[] val = new T[count];
            ptr[..size].CopyTo(MemoryMarshal.AsBytes(val.AsSpan()));

            Advance(size);
            return val;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Memory<T> DeserializeStructRent<T>() where T : unmanaged
        {
            int count = ReadInt();
            if (count == 0)
            {
                return Memory<T>.Empty;
            }

            int sizeOfT = SizeOf<T>();
            int size = count * sizeOfT;
            ThrowIfOutOfRange(size);

            var val = new Memory<T>(ArrayPool<T>.Shared.Rent(count))[..count];
            ptr[..size].CopyTo(MemoryMarshal.AsBytes(val.Span));

            Advance(size);
            return val;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DeserializeStructList<T>(List<T> list) where T : unmanaged
        {
            int count = ReadInt();
            DeserializeStructList(list, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DeserializeStructList<T>(List<T> list, int count) where T : unmanaged
        {
            ThrowIfOutOfRange(count * SizeOf<T>());
            list.Clear();
            if (list.Capacity < count)
            {
                list.Capacity = count;
            }

            for (int i = 0; i < count; i++)
            {
                list.Add(Deserialize<T>());
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T[] DeserializeArray<T>() where T : IMessage, new()
        {
            int count = ReadInt();
            if (count > 1024 * 1024 * 1024)
            {
                throw new BufferException("Implausible message requested more than 1TB elements.");
            }

            return count == 0 ? Array.Empty<T>() : new T[count];
            // per entry deserialization happens outside
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
            if (generator == null)
            {
                throw new ArgumentNullException(nameof(generator));
            }

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
            if (generator == null)
            {
                throw new ArgumentNullException(nameof(generator));
            }

            var b = new ReadBuffer(buffer);
            return generator.RosDeserialize(ref b);
        }
        
        public static T Deserialize<T>(T generator, ReadOnlySpan<byte> buffer)
            where T : ISerializable, IDeserializable<T>
        {
            if (generator == null)
            {
                throw new ArgumentNullException(nameof(generator));
            }

            var b = new ReadBuffer(buffer);
            return generator.RosDeserialize(ref b);
        }
    }
}
