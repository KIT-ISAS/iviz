using System;

namespace Iviz.Msgs
{
    /// <summary>
    /// Class that contains information about an underlying buffer.
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
                throw new ArgumentOutOfRangeException(nameof(off));
        }

        static void AssertSize<T>(T[] array, uint size)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (array.Length != size)
                throw new ArgumentException("Invalid array size. Expected " + size + ", but got " + array.Length + ".", nameof(array));
        }

        internal string DeserializeString()
        {
            AssertInRange(4);
            uint count = *(uint*)ptr; ptr += 4;
            if (count == 0) { return string.Empty; }
            AssertInRange(count);
            string val = BuiltIns.UTF8.GetString(ptr, (int)count);
            ptr += count;
            return val;
        }

        internal string[] DeserializeStringArray(uint count = 0)
        {
            if (count == 0)
            {
                AssertInRange(4);
                count = *(uint*)ptr; ptr += 4;
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
            AssertInRange((uint)sizeof(T));
            T val = *(T*)ptr;
            ptr += sizeof(T);
            return val;
        }

        internal T[] DeserializeStructArray<T>(uint count = 0) where T : unmanaged
        {
            if (count == 0)
            {
                AssertInRange(4);
                count = *(uint*)ptr; ptr += 4;
                if (count == 0)
                {
                    return Array.Empty<T>();
                }
            }
            AssertInRange(count * (uint)sizeof(T));
            T[] val = new T[count];
            fixed (T* b_ptr = val)
            {
                uint size = count * (uint)sizeof(T);
                Memcpy(b_ptr, ptr, size);
                ptr += size;
            }
            return val;
        }

        internal T[] DeserializeArray<T>(uint count = 0) where T : IMessage, new()
        {
            if (count == 0)
            {
                AssertInRange(4);
                count = *(uint*)ptr; ptr += 4;
                if (count == 0)
                {
                    return Array.Empty<T>();
                }
            }
            return new T[count];
        }

        internal void Serialize<T>(in T val) where T : unmanaged
        {
            AssertInRange((uint)sizeof(T));
            *(T*)ptr = val;
            ptr += sizeof(T);
        }

        internal void Serialize(string val)
        {
            if (val is null)
            {
                throw new ArgumentNullException(nameof(val));
            }

            uint count = (uint)BuiltIns.UTF8.GetByteCount(val);
            AssertInRange(4 + count);
            *(uint*)ptr = count; ptr += 4;
            if (count == 0) return;
            fixed (char* b_ptr = val)
            {
                BuiltIns.UTF8.GetBytes(b_ptr, val.Length, ptr, (int)count);
                ptr += count;
            }
        }

        internal void Serialize(ISerializable val)
        {
            val.Serialize(this);
        }

        internal void SerializeArray(string[] val, uint count)
        {
            if (val is null)
            {
                throw new ArgumentNullException(nameof(val));
            }

            if (count == 0)
            {
                AssertInRange(4);
                *(int*)ptr = val.Length; ptr += 4;
            }
            else
            {
                AssertSize(val, count);
            }
            for (int i = 0; i < val.Length; i++)
            {
                Serialize(val[i]);
            }
        }

        internal void SerializeStructArray<T>(T[] val, uint count) where T : unmanaged
        {
            if (val is null)
            {
                throw new ArgumentNullException(nameof(val));
            }

            if (count == 0)
            {
                AssertInRange((uint)(4 + val.Length * sizeof(T)));
                *(int*)ptr = val.Length; ptr += 4;
            }
            else
            {
                AssertSize(val, count);
                AssertInRange(count * (uint)sizeof(T));
            }
            fixed (T* b_ptr = val)
            {
                uint size = (uint)(val.Length * sizeof(T));
                Memcpy(ptr, b_ptr, size);
                ptr += size;
            }
        }


        internal void SerializeArray<T>(T[] val, uint count) where T : IMessage
        {
            if (val is null)
            {
                throw new ArgumentNullException(nameof(val));
            }

            if (count == 0)
            {
                AssertInRange(4);
                *(int*)ptr = val.Length; ptr += 4;
            }
            else
            {
                AssertSize(val, count);
            }
            for (int i = 0; i < val.Length; i++)
            {
                val[i].Serialize(this);
            }
        }

        public static T Deserialize<T>(T generator, byte[] buffer, int size) where T : ISerializable
        {
            if (buffer is null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }
            if (size > buffer.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(buffer));
            }

            fixed (byte* b_ptr = buffer)
            {
                Buffer b = new Buffer(b_ptr, b_ptr + size);
                return (T)generator.Deserialize(b);
                //return (uint)(b.ptr - b_ptr);
            }
        }

        public static uint Serialize(ISerializable message, byte[] buffer)
        {
            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            if (buffer is null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }

            fixed (byte* b_ptr = buffer)
            {
                Buffer b = new Buffer(b_ptr, b_ptr + buffer.Length);
                message.Serialize(b);
                return (uint)(b.ptr - b_ptr);
            }
        }
    }
}