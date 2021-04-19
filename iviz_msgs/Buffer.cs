using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

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
        static void ThrowIfWrongSize<T>(T[] array, uint size)
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

        static void ThrowIfWrongSize<T>(UniqueRef<T> array, uint size)
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
        static void ThrowIfWrongSize<T>(List<T> array, uint size)
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
        public unsafe string DeserializeString()
        {
            ThrowIfOutOfRange(4);
            uint count = *(uint*) ptr;
            ptr += 4;
            
            if (count == 0)
            {
                return string.Empty;
            }

            ThrowIfOutOfRange(count);
            string val = BuiltIns.UTF8.GetString(ptr, (int) count);
            ptr += count;
            return val;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe StringRef DeserializeStringS()
        {
            ThrowIfOutOfRange(4);
            uint count = *(uint*) ptr;
            ptr += 4;

            if (count == 0)
            {
                return StringRef.Empty;
            }

            ThrowIfOutOfRange(count);
            StringRef val = new(count);
            fixed (byte* bPtr = val.Array)
            {
                MemoryCopy(bPtr, ptr, count);
                ptr += count;
            }

            return val;
        }        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe string[] DeserializeStringArray()
        {
            ThrowIfOutOfRange(4);
            uint count = *(uint*) ptr;
            ptr += 4;
            return count == 0 ? Array.Empty<string>() : DeserializeStringArray(count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string[] DeserializeStringArray(uint count)
        {
            string[] val = new string[count];
            for (int i = 0; i < val.Length; i++)
            {
                val[i] = DeserializeString();
            }

            return val;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe UniqueRef<StringRef> DeserializeStringArrayS()
        {
            ThrowIfOutOfRange(4);
            uint count = *(uint*) ptr;
            ptr += 4;
            return count == 0 ? UniqueRef<StringRef>.Empty : DeserializeStringArrayS(count);
        }        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UniqueRef<StringRef> DeserializeStringArrayS(uint count)
        {
            UniqueRef<StringRef> val = new(count);
            for (int i = 0; i < val.Length; i++)
            {
                val[i] = DeserializeStringS();
            }

            return val;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void DeserializeStringList(List<string> list)
        {
            ThrowIfOutOfRange(4);
            uint count = *(uint*) ptr;
            ptr += 4;
            DeserializeStringArray(list, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DeserializeStringArray(List<string> list, uint count)
        {
            list.Clear();
            if (list.Capacity < count) list.Capacity = (int) count;
            for (int i = 0; i < list.Count; i++)
            {
                list[i] = DeserializeString();
            }
        }        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe T Deserialize<T>() where T : unmanaged
        {
            ThrowIfOutOfRange((uint) sizeof(T));
            T val = *(T*) ptr;
            ptr += sizeof(T);
            return val;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void Deserialize<T>(out T t) where T : unmanaged
        {
            ThrowIfOutOfRange((uint) sizeof(T));
            t = *(T*) ptr;
            ptr += sizeof(T);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe T[] DeserializeStructArray<T>() where T : unmanaged
        {
            ThrowIfOutOfRange(4);
            uint count = *(uint*) ptr;
            ptr += 4;
            return count == 0 ? Array.Empty<T>() : DeserializeStructArray<T>(count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe T[] DeserializeStructArray<T>(uint count) where T : unmanaged
        {
            ThrowIfOutOfRange(count * (uint) sizeof(T));
            T[] val = new T[count];
            fixed (T* bPtr = val)
            {
                uint size = count * (uint) sizeof(T);
                MemoryCopy(bPtr, ptr, size);
                ptr += size;
            }

            return val;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe UniqueRef<T> DeserializeStructArrayS<T>() where T : unmanaged
        {
            ThrowIfOutOfRange(4);
            uint count = *(uint*) ptr;
            ptr += 4;
            return count == 0 ? UniqueRef<T>.Empty : DeserializeStructArrayS<T>(count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe UniqueRef<T> DeserializeStructArrayS<T>(uint count) where T : unmanaged
        {
            ThrowIfOutOfRange(count * (uint) sizeof(T));
            UniqueRef<T> val = new(count);
            fixed (T* bPtr = val.Array)
            {
                uint size = count * (uint) sizeof(T);
                MemoryCopy(bPtr, ptr, size);
                ptr += size;
            }

            return val;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void DeserializeStructList<T>(List<T> list) where T : unmanaged
        {
            ThrowIfOutOfRange(4);
            uint count = *(uint*) ptr;
            ptr += 4;
            DeserializeStructList(list, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void DeserializeStructList<T>(List<T> list, uint count) where T : unmanaged
        {
            ThrowIfOutOfRange(count * (uint) sizeof(T));
            list.Clear();
            if (list.Capacity < count) list.Capacity = (int) count;

            T* tPtr = (T*) ptr;
            for (int i = 0; i < count; i++)
            {
                list.Add(*tPtr++);
            }

            uint size = count * (uint) sizeof(T);
            ptr += size;
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe T[] DeserializeArray<T>() where T : IMessage, new()
        {
            ThrowIfOutOfRange(4);
            uint count = *(uint*) ptr;
            ptr += 4;
            return count == 0 ? Array.Empty<T>() : new T[count];
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe uint DeserializeList<T>(List<T> list) where T : IMessage, new()
        {
            ThrowIfOutOfRange(4);
            uint count = *(uint*) ptr;
            ptr += 4;
            if (list.Capacity < count) list.Capacity = (int) count;
            return count;
        }        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe UniqueRef<T> DeserializeArrayS<T>() where T : IMessage, new()
        {
            ThrowIfOutOfRange(4);
            uint count = *(uint*) ptr;
            ptr += 4;
            return count == 0 ? UniqueRef<T>.Empty : new UniqueRef<T>(count);
        }        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void Serialize<T>(in T val) where T : unmanaged
        {
            ThrowIfOutOfRange((uint) sizeof(T));
            *(T*) ptr = val;
            ptr += sizeof(T);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void Serialize(string val)
        {
            uint count = (uint) BuiltIns.UTF8.GetByteCount(val);
            ThrowIfOutOfRange(4 + count);
            *(uint*) ptr = count;
            ptr += 4;
            if (count == 0)
            {
                return;
            }

            fixed (char* bPtr = val)
            {
                BuiltIns.UTF8.GetBytes(bPtr, val.Length, ptr, (int) count);
                ptr += count;
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Serialize(StringRef val)
        {
            SerializeStructArray(val.Ref);
        }        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void SerializeArray(string[] val, uint count = 0)
        {
            if (count == 0)
            {
                ThrowIfOutOfRange(4);
                *(int*) ptr = val.Length;
                ptr += 4;
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
        public unsafe void SerializeArray(UniqueRef<StringRef> val, uint count = 0)
        {
            if (count == 0)
            {
                ThrowIfOutOfRange(4);
                *(int*) ptr = val.Length;
                ptr += 4;
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
        public unsafe void SerializeArray(List<string> val, uint count = 0)
        {
            if (count == 0)
            {
                ThrowIfOutOfRange(4);
                *(int*) ptr = val.Count;
                ptr += 4;
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
        public unsafe void SerializeStructArray<T>(T[] val, uint count = 0) where T : unmanaged
        {
            if (count == 0)
            {
                ThrowIfOutOfRange((uint) (4 + val.Length * sizeof(T)));
                *(int*) ptr = val.Length;
                ptr += 4;
            }
            else
            {
                ThrowIfWrongSize(val, count);
                ThrowIfOutOfRange(count * (uint) sizeof(T));
            }

            fixed (T* bPtr = val)
            {
                uint size = (uint) (val.Length * sizeof(T));
                MemoryCopy(ptr, bPtr, size);
                ptr += size;
            }
        }
        
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void SerializeStructArray<T>(UniqueRef<T> val, uint count = 0) where T : unmanaged
        {
            if (count == 0)
            {
                ThrowIfOutOfRange((uint) (4 + val.Length * sizeof(T)));
                *(int*) ptr = val.Length;
                ptr += 4;
            }
            else
            {
                ThrowIfWrongSize(val, count);
                ThrowIfOutOfRange(count * (uint) sizeof(T));
            }

            fixed (T* bPtr = val.Array)
            {
                uint size = (uint) (val.Length * sizeof(T));
                MemoryCopy(ptr, bPtr, size);
                ptr += size;
            }
        }    
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void SerializeStructList<T>(List<T> val, uint count = 0) where T : unmanaged
        {
            if (count == 0)
            {
                ThrowIfOutOfRange((uint) (4 + val.Count * sizeof(T)));
                *(int*) ptr = val.Count;
                ptr += 4;
            }
            else
            {
                ThrowIfWrongSize(val, count);
                ThrowIfOutOfRange(count * (uint) sizeof(T));
            }

            T* tPtr = (T*) ptr;
            foreach (T v in val)
            {
                *tPtr++ = v;
            }

            uint size = (uint) (val.Count * sizeof(T));
            ptr += size;
        }        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void SerializeArray<T>(T[] val, uint count = 0) where T : IMessage
        {
            if (count == 0)
            {
                ThrowIfOutOfRange(4);
                *(int*) ptr = val.Length;
                ptr += 4;
            }
            else
            {
                ThrowIfWrongSize(val, count);
            }

            foreach (T t in val)
            {
                t.RosSerialize(ref this);
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void SerializeArray<T>(UniqueRef<T> val, uint count = 0) where T : IMessage
        {
            if (count == 0)
            {
                ThrowIfOutOfRange(4);
                *(int*) ptr = val.Length;
                ptr += 4;
            }
            else
            {
                ThrowIfWrongSize(val, count);
            }

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
        public static unsafe T Deserialize<T>(T generator, byte[] buffer, int size = -1, int offset = 0) where T : ISerializable
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

            if (size < -1)
            {
                throw new ArgumentOutOfRangeException(nameof(size));
            }            

            int span = (size == -1) ? buffer.Length : size;
            if (offset + span > buffer.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(offset));
            }            

            
            fixed (byte* bPtr = buffer)
            {
                Buffer b = new Buffer(bPtr + offset, bPtr + offset + span);
                return (T) generator.RosDeserialize(ref b);
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
        public static unsafe T Deserialize<T>(IDeserializable<T> generator, byte[] buffer, int size = -1, int offset = 0)
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

            if (size < -1)
            {
                throw new ArgumentOutOfRangeException(nameof(size));
            }

            if (generator == null)
            {
                throw new ArgumentNullException(nameof(generator));
            }

            int span = (size == -1) ? buffer.Length : size;
            if (offset + span > buffer.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(offset));
            }            
            
            fixed (byte* bPtr = buffer)
            {
                Buffer b = new Buffer(bPtr + offset, bPtr + offset + span);
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
        public static unsafe uint Serialize<T>(in T message, byte[] buffer, int offset = 0) where T : ISerializable
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
                Buffer b = new Buffer(bPtr + offset, bPtr + buffer.Length);
                message.RosSerialize(ref b);
                return (uint) (b.ptr - bPtr - offset);
            }
        }
    }
}