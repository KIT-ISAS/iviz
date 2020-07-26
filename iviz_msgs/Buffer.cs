using System;
using System.Collections;
using System.Collections.Generic;

namespace Iviz.Msgs
{
    /// <summary>
    /// Wrapper around a byte array that contains a serialized ROS message. 
    /// </summary>
    public unsafe class Buffer
    {
        byte* ptr;
        readonly byte* end;

        Buffer(byte* ptr, byte* end)
        {
            this.ptr = ptr;
            this.end = end;
        }

        static void Memcpy(void* dst, void* src, uint size)
        {
            System.Buffer.MemoryCopy(src, dst, size, size);
        }

        void AssertInRange(uint off)
        {
            if (ptr + off > end)
            {
                throw new ArgumentOutOfRangeException(nameof(off));
            }
        }

        static void AssertSize<T>(IList<T> array, uint size)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (array.Count != size)
            {
                throw new ArgumentException($"Invalid array size. Expected {size}, but got {array.Count}.",
                    nameof(array));
            }
        }

        internal string DeserializeString()
        {
            AssertInRange(4);
            uint count = *(uint*) ptr;
            ptr += 4;
            if (count == 0)
            {
                return string.Empty;
            }

            AssertInRange(count);
            string val = BuiltIns.UTF8.GetString(ptr, (int) count);
            ptr += count;
            return val;
        }

        internal string[] DeserializeStringArray(uint count = 0)
        {
            if (count == 0)
            {
                AssertInRange(4);
                count = *(uint*) ptr;
                ptr += 4;
                if (count == 0)
                {
                    return Array.Empty<string>();
                }
            }

            string[] val = new string[count];
            for (int i = 0; i < val.Length; i++)
            {
                val[i] = DeserializeString();
            }

            return val;
        }

        internal T Deserialize<T>() where T : unmanaged
        {
            AssertInRange((uint) sizeof(T));
            T val = *(T*) ptr;
            ptr += sizeof(T);
            return val;
        }

        internal void Deserialize<T>(out T t) where T : unmanaged
        {
            AssertInRange((uint) sizeof(T));
            t = *(T*) ptr;
            ptr += sizeof(T);
        }

        internal T[] DeserializeStructArray<T>(uint count = 0) where T : unmanaged
        {
            if (count == 0)
            {
                AssertInRange(4);
                count = *(uint*) ptr;
                ptr += 4;
                if (count == 0)
                {
                    return Array.Empty<T>();
                }
            }

            AssertInRange(count * (uint) sizeof(T));
            T[] val = new T[count];
            fixed (T* bPtr = val)
            {
                uint size = count * (uint) sizeof(T);
                Memcpy(bPtr, ptr, size);
                ptr += size;
            }

            return val;
        }

        internal T[] DeserializeArray<T>(uint count = 0) where T : IMessage, new()
        {
            if (count == 0)
            {
                AssertInRange(4);
                count = *(uint*) ptr;
                ptr += 4;
                if (count == 0)
                {
                    return Array.Empty<T>();
                }
            }

            return new T[count];
        }

        internal void Serialize<T>(in T val) where T : unmanaged
        {
            AssertInRange((uint) sizeof(T));
            *(T*) ptr = val;
            ptr += sizeof(T);
        }

        internal void Serialize(string val)
        {
            uint count = (uint) BuiltIns.UTF8.GetByteCount(val);
            AssertInRange(4 + count);
            *(uint*) ptr = count;
            ptr += 4;
            if (count == 0) return;
            fixed (char* bPtr = val)
            {
                BuiltIns.UTF8.GetBytes(bPtr, val.Length, ptr, (int) count);
                ptr += count;
            }
        }

        internal void Serialize(ISerializable val)
        {
            val.RosSerialize(this);
        }

        internal void SerializeArray(IList<string> val, uint count)
        {
            if (count == 0)
            {
                AssertInRange(4);
                *(int*) ptr = val.Count;
                ptr += 4;
            }
            else
            {
                AssertSize(val, count);
            }

            foreach (string str in val)
            {
                Serialize(str);
            }
        }

        internal void SerializeStructArray<T>(T[] val, uint count) where T : unmanaged
        {
            if (count == 0)
            {
                AssertInRange((uint) (4 + val.Length * sizeof(T)));
                *(int*) ptr = val.Length;
                ptr += 4;
            }
            else
            {
                AssertSize(val, count);
                AssertInRange(count * (uint) sizeof(T));
            }

            fixed (T* bPtr = val)
            {
                uint size = (uint) (val.Length * sizeof(T));
                Memcpy(ptr, bPtr, size);
                ptr += size;
            }
        }


        internal void SerializeArray<T>(IList<T> val, uint count) where T : IMessage
        {
            if (count == 0)
            {
                AssertInRange(4);
                *(int*) ptr = val.Count;
                ptr += 4;
            }
            else
            {
                AssertSize(val, count);
            }

            foreach (T t in val)
            {
                t.RosSerialize(this);
            }
        }

        /// <summary>
        /// Deserializes a message of the given type from the buffer array.  
        /// </summary>
        /// <param name="generator">
        /// An arbitrary instance of the type T. Can be anything, for example "new T()".
        /// This is a (rather unclean) workaround to the fact that C# cannot invoke static functions from generics.
        /// So instead of using T.Deserialize(), we call the "static" method from the instance.
        /// </param>
        /// <param name="buffer">
        /// The byte array that contains the serialized message. 
        /// </param>
        /// <typeparam name="T">Message type.</typeparam>
        /// <returns>The deserialized message.</returns>
        public static T Deserialize<T>(T generator, ArraySegment<byte> buffer) where T : ISerializable
        {
            if (generator == null)
            {
                throw new ArgumentNullException(nameof(generator));
            }

            fixed (byte* bPtr = buffer.Array)
            {
                Buffer b = new Buffer(bPtr + buffer.Offset, bPtr + buffer.Offset + buffer.Count);
                return (T) generator.RosDeserialize(b);
            }
        }

        /// <summary>
        /// Deserializes a message of the given type from the buffer array.  
        /// </summary>
        /// <param name="generator">
        /// An arbitrary instance of the type T. Can be anything.
        /// This is a (rather unclean) workaround to the fact that C# cannot invoke static functions from generics.
        /// So instead of using T.Deserialize(), we need an instance to do this.
        /// </param>
        /// <param name="buffer">
        /// The source byte array. 
        /// </param>
        /// <param name="size">
        /// Optional. The expected size of the message inside of the array. Must be less or equal the array size.
        /// </param>
        /// <typeparam name="T">Message type.</typeparam>
        /// <returns>The deserialized message.</returns>
        public static T Deserialize<T>(T generator, byte[] buffer, int size = -1) where T : ISerializable
        {
            ArraySegment<byte> segment;
            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }

            if (size == -1)
            {
                segment = new ArraySegment<byte>(buffer);
            }
            else
            {
                if (buffer.Length < size)
                {
                    throw new ArgumentOutOfRangeException(nameof(buffer));
                }

                segment = new ArraySegment<byte>(buffer, 0, size);
            }

            return Deserialize(generator, segment);
        }

        /// <summary>
        /// Serializes the given message into the buffer array.
        /// </summary>
        /// <param name="message">The ROS message.</param>
        /// <param name="buffer">The destination byte array.</param>
        /// <returns>The number of bytes written.</returns>
        public static uint Serialize(ISerializable message, ArraySegment<byte> buffer)
        {
            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            fixed (byte* bPtr = buffer.Array)
            {
                Buffer b = new Buffer(bPtr + buffer.Offset, bPtr + buffer.Offset + buffer.Count);
                message.RosSerialize(b);
                return (uint) (b.ptr - bPtr);
            }
        }

        /// <summary>
        /// Serializes the given message into the buffer array.
        /// </summary>
        /// <param name="message">The ROS message.</param>
        /// <param name="buffer">The destination byte array.</param>
        /// <returns>The number of bytes written.</returns>
        public static uint Serialize(ISerializable message, byte[] buffer)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }

            return Serialize(message, new ArraySegment<byte>(buffer));
        }
    }
}