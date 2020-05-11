using System;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace Iviz.Msgs
{
    public unsafe static class BuiltIns
    {
        public static UTF8Encoding UTF8 { get; } = new UTF8Encoding(false);

        public static CultureInfo Culture { get; } = CultureInfo.InvariantCulture;

        public static string InvalidArrayLengthStr { get; } = "Invalid array length.";

        /*
        static void Memcpy(void* dst, void* src, uint size)
        {
            System.Buffer.MemoryCopy(src, dst, size, size);
        }

        internal static void AssertInRange(byte* ptr, uint off, byte* end)
        {
            if (ptr + off > end)
                throw new ArgumentOutOfRangeException(nameof(ptr));
        }

        internal static void AssertSize<T>(T[] array, uint size)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (array.Length != size)
                throw new ArgumentException("Invalid array size. Expected " + size + ", but got " + array.Length + ".", nameof(array));
        }

        #region Deserializers

        internal static void Deserialize(out string val, Buffer b)
        {
            AssertInRange(b.ptr, 4, b.end);
            uint count = *(uint*)b.ptr; b.ptr += 4;
            if (count == 0) { val = string.Empty; return; }
            AssertInRange(b.ptr, count, b.end);
            val = UTF8.GetString(b.ptr, (int)count);
            b.ptr += count;
        }

        internal static string DeserializeString(Buffer b)
        {
            Deserialize(out string str, b);
            return str;
        }

        internal static void Deserialize(out string[] val, Buffer b, uint count)
        {
            if (count == 0)
            {
                AssertInRange(b.ptr, 4, b.end);
                count = *(uint*)b.ptr; b.ptr += 4;
                if (count == 0) { val = Array.Empty<string>(); return; }
            }
            val = new string[count];
            for (int i = 0; i < val.Length; i++)
            {
                Deserialize(out val[i], b);
            }
        }

        internal static string[] DeserializeStringArray(Buffer b, uint count)
        {
            Deserialize(out string[] str, b, count);
            return str;
        }

        internal static void Deserialize(out bool val, Buffer b)
        {
            val = (*b.ptr != 0);
            b.ptr++;
        }

        internal static void Deserialize(out time val, Buffer b)
        {
            AssertInRange(b.ptr, 8, b.end);
            uint secs = *(uint*)b.ptr; b.ptr += 4;
            uint nsecs = *(uint*)b.ptr; b.ptr += 4;
            val = new time(secs, nsecs);
        }

        internal static void Deserialize(out time[] val, Buffer b, uint count)
        {
            DeserializeStructArray(out val, b, count);
        }

        internal static void Deserialize(out duration val, Buffer b)
        {
            AssertInRange(b.ptr, 8, b.end);
            int secs = *(int*)b.ptr; b.ptr += 4;
            int nsecs = *(int*)b.ptr; b.ptr += 4;
            val = new duration(secs, nsecs);
        }

        internal static void Deserialize(out duration[] val, Buffer b, uint count)
        {
            DeserializeStructArray(out val, b, count);
        }

        internal static void Deserialize(out bool[] val, Buffer b, uint count)
        {
            if (count == 0)
            {
                AssertInRange(b.ptr, 4, b.end);
                count = *(uint*)b.ptr; b.ptr += 4;
                if (count == 0) { val = Array.Empty<bool>(); return; }
            }
            AssertInRange(b.ptr, count * 1, b.end);
            val = new bool[count];
            fixed (bool* b_ptr = val)
            {
                uint size = count * 1;
                Memcpy(b_ptr, b.ptr, size);
                b.ptr += size;
            }
        }

        internal static void Deserialize(out char val, Buffer b)
        {
            AssertInRange(b.ptr, 1, b.end);
            val = *(char*)b.ptr;
            b.ptr += 1;
        }

        internal static void Deserialize(out char[] val, Buffer b, uint count)
        {
            DeserializeStructArray(out val, b, count);
        }


        internal static void Deserialize(out byte val, Buffer b)
        {
            AssertInRange(b.ptr, 1, b.end);
            val = *b.ptr;
            b.ptr += 1;
        }

        internal static void Deserialize(out byte[] val, Buffer b, uint count)
        {
            DeserializeStructArray(out val, b, count);
        }

        internal static void Deserialize(out sbyte val, Buffer b)
        {
            AssertInRange(b.ptr, 1, b.end);
            val = *(sbyte*)b.ptr;
            b.ptr += 1;
        }

        internal static void Deserialize(out sbyte[] val, Buffer b, uint count)
        {
            DeserializeStructArray(out val, b, count);
        }

        internal static void Deserialize(out short val, Buffer b)
        {
            AssertInRange(b.ptr, 2, b.end);
            val = *(short*)b.ptr;
            b.ptr += 2;
        }

        internal static void Deserialize(out short[] val, Buffer b, uint count)
        {
            DeserializeStructArray(out val, b, count);
        }

        internal static void Deserialize(out ushort val, Buffer b)
        {
            AssertInRange(b.ptr, 2, b.end);
            val = *(ushort*)b.ptr;
            b.ptr += 2;
        }

        internal static void Deserialize(out ushort[] val, Buffer b, uint count)
        {
            DeserializeStructArray(out val, b, count);
        }

        internal static void Deserialize(out int val, Buffer b)
        {
            AssertInRange(b.ptr, 4, b.end);
            val = *(int*)b.ptr;
            b.ptr += 4;
        }

        internal static void Deserialize(out int[] val, Buffer b, uint count)
        {
            DeserializeStructArray(out val, b, count);
        }

        internal static void Deserialize(out uint val, Buffer b)
        {
            AssertInRange(b.ptr, 4, b.end);
            val = *(uint*)b.ptr;
            b.ptr += 4;
        }

        internal static void Deserialize(out uint[] val, Buffer b, uint count)
        {
            DeserializeStructArray(out val, b, count);
        }

        internal static void Deserialize(out long val, Buffer b)
        {
            AssertInRange(b.ptr, 8, b.end);
            val = *(long*)b.ptr;
            b.ptr += 8;
        }

        internal static void Deserialize(out long[] val, Buffer b, uint count)
        {
            DeserializeStructArray(out val, b, count);
        }

        internal static void Deserialize(out ulong val, Buffer b)
        {
            AssertInRange(b.ptr, 8, b.end);
            val = *(ulong*)b.ptr;
            b.ptr += 8;
        }

        internal static void Deserialize(out ulong[] val, Buffer b, uint count)
        {
            DeserializeStructArray(out val, b, count);
        }

        internal static void Deserialize(out float val, Buffer b)
        {
            AssertInRange(b.ptr, 4, b.end);
            val = *(float*)b.ptr;
            b.ptr += 4;
        }

        internal static void Deserialize(out float[] val, Buffer b, uint count)
        {
            DeserializeStructArray(out val, b, count);
        }

        internal static void Deserialize(out double val, Buffer b)
        {
            AssertInRange(b.ptr, 8, b.end);
            val = *(double*)b.ptr;
            b.ptr += 8;
        }

        internal static void Deserialize(out double[] val, Buffer b, uint count)
        {
            DeserializeStructArray(out val, b, count);
        }

        internal static void DeserializeStruct<T>(out T val, Buffer b) where T : unmanaged
        {
            AssertInRange(b.ptr, (uint)sizeof(T), b.end);
            val = *(T*)b.ptr;
            b.ptr += sizeof(T);
        }

        internal static T DeserializeStruct<T>(Buffer b) where T : unmanaged
        {
            AssertInRange(b.ptr, (uint)sizeof(T), b.end);
            T val = *(T*)b.ptr;
            b.ptr += sizeof(T);
            return val;
        }

        internal static void DeserializeStructArray<T>(out T[] val, Buffer b, uint count) where T : unmanaged
        {
            if (count == 0)
            {
                AssertInRange(b.ptr, 4, b.end);
                count = *(uint*)b.ptr; b.ptr += 4;
                if (count == 0) { val = Array.Empty<T>(); return; }
            }
            AssertInRange(b.ptr, count * (uint)sizeof(T), b.end);
            val = new T[count];
            fixed (T* b_ptr = val)
            {
                uint size = count * (uint)sizeof(T);
                Memcpy(b_ptr, b.ptr, size);
                b.ptr += size;
            }
        }

        internal static T[] DeserializeStructArray<T>(Buffer b, uint count) where T : unmanaged
        {
            DeserializeStructArray(out T[] val, b, count);
            return val;
        }

        internal static void DeserializeArray<T>(out T[] val, Buffer b, uint count) where T : IMessage, new()
        {
            if (count == 0)
            {
                AssertInRange(b.ptr, 4, b.end);
                count = *(uint*)b.ptr; b.ptr += 4;
                if (count == 0) { val = Array.Empty<T>(); return; }
            }
            val = new T[count];
            for (int i = 0; i < val.Length; i++)
            {
                val[i] = new T();
                val[i].Deserialize(b);
            }
        }

        internal static T[] DeserializeArray<T>(Buffer b, uint count) where T : IMessage, new()
        {
            DeserializeArray(out T[] val, b, count);
            return val;
        }

        #endregion



        #region Serializers

        internal static void Serialize(in time val, Buffer b)
        {
            SerializeStruct(val, b);
        }

        internal static void Serialize(time[] val, Buffer b, uint count)
        {
            SerializeStructArray(val, b, count);
        }

        internal static void Serialize(in duration val, Buffer b)
        {
            SerializeStruct(val, b);
        }

        internal static void Serialize(duration[] val, Buffer b, uint count)
        {
            SerializeStructArray(val, b, count);
        }

        internal static void Serialize(bool val, Buffer b)
        {
            AssertInRange(b.ptr, 1, b.end);
            *b.ptr = val ? (byte)1 : (byte)0;
            b.ptr++;
        }

        internal static void Serialize(string val, Buffer b)
        {
            if (val is null)
            {
                throw new ArgumentNullException(nameof(val));
            }

            uint count = (uint)UTF8.GetByteCount(val);
            AssertInRange(b.ptr, 4 + count, b.end);
            *(uint*)b.ptr = count; b.ptr += 4;
            if (count == 0) return;
            fixed (char* b_ptr = val)
            {
                UTF8.GetBytes(b_ptr, val.Length, b.ptr, (int)count);
                b.ptr += count;
            }
        }

        internal static void Serialize(string[] val, Buffer b, uint count)
        {
            if (val is null)
            {
                throw new ArgumentNullException(nameof(val));
            }

            if (count == 0)
            {
                AssertInRange(b.ptr, 4, b.end);
                *(int*)b.ptr = val.Length; b.ptr += 4;
            }
            else
            {
                AssertSize(val, count);
            }
            for (int i = 0; i < val.Length; i++)
            {
                Serialize(val[i], b);
            }
        }

        internal static void Serialize(bool[] val, Buffer b, uint count)
        {
            if (val is null)
            {
                throw new ArgumentNullException(nameof(val));
            }

            if (count == 0)
            {
                AssertInRange(b.ptr, (uint)(4 + val.Length * 1), b.end);
                *(int*)b.ptr = val.Length; b.ptr += 4;
            }
            else
            {
                AssertSize(val, count);
                AssertInRange(b.ptr, count * 1, b.end);
            }
            fixed (bool* b_ptr = val)
            {
                uint size = (uint)(val.Length * 1);
                Memcpy(b.ptr, b_ptr, size);
                b.ptr += size;
            }
        }

        internal static void Serialize(byte val, Buffer b)
        {
            AssertInRange(b.ptr, 1, b.end);
            *b.ptr = val;
            b.ptr += 1;
        }

        internal static void Serialize(byte[] val, Buffer b, uint count)
        {
            SerializeStructArray(val, b, count);
        }

        internal static void Serialize(char val, Buffer b)
        {
            AssertInRange(b.ptr, 1, b.end);
            *(char*)b.ptr = val;
            b.ptr += 1;
        }

        internal static void Serialize(char[] val, Buffer b, uint count)
        {
            SerializeStructArray(val, b, count);
        }

        internal static void Serialize(sbyte val, Buffer b)
        {
            AssertInRange(b.ptr, 1, b.end);
            *(sbyte*)b.ptr = val;
            b.ptr += 1;
        }

        internal static void Serialize(sbyte[] val, Buffer b, uint count)
        {
            SerializeStructArray(val, b, count);
        }

        internal static void Serialize(short val, Buffer b)
        {
            AssertInRange(b.ptr, 2, b.end);
            *(short*)b.ptr = val;
            b.ptr += 2;
        }

        internal static void Serialize(short[] val, Buffer b, uint count)
        {
            SerializeStructArray(val, b, count);
        }

        internal static void Serialize(ushort val, Buffer b)
        {
            AssertInRange(b.ptr, 2, b.end);
            *(ushort*)b.ptr = val;
            b.ptr += 2;
        }

        internal static void Serialize(ushort[] val, Buffer b, uint count)
        {
            SerializeStructArray(val, b, count);
        }

        internal static void Serialize(int val, Buffer b)
        {
            AssertInRange(b.ptr, 4, b.end);
            *(int*)b.ptr = val;
            b.ptr += 4;
        }

        internal static void Serialize(int[] val, Buffer b, uint count)
        {
            SerializeStructArray(val, b, count);
        }

        internal static void Serialize(uint val, Buffer b)
        {
            AssertInRange(b.ptr, 4, b.end);
            *(uint*)b.ptr = val;
            b.ptr += 4;
        }

        internal static void Serialize(uint[] val, Buffer b, uint count)
        {
            SerializeStructArray(val, b, count);
        }

        internal static void Serialize(long val, Buffer b)
        {
            AssertInRange(b.ptr, 8, b.end);
            *(long*)b.ptr = val;
            b.ptr += 8;
        }

        internal static void Serialize(long[] val, Buffer b, uint count)
        {
            SerializeStructArray(val, b, count);
        }

        internal static void Serialize(ulong val, Buffer b)
        {
            AssertInRange(b.ptr, 8, b.end);
            *(ulong*)b.ptr = val;
            b.ptr += 8;
        }

        internal static void Serialize(ulong[] val, Buffer b, uint count)
        {
            SerializeStructArray(val, b, count);
        }

        internal static void Serialize(float val, Buffer b)
        {
            AssertInRange(b.ptr, 4, b.end);
            *(float*)b.ptr = val;
            b.ptr += 4;
        }

        internal static void Serialize(float[] val, Buffer b, uint count)
        {
            SerializeStructArray(val, b, count);
        }

        internal static void Serialize(double val, Buffer b)
        {
            AssertInRange(b.ptr, 8, b.end);
            *(double*)b.ptr = val;
            b.ptr += 8;
        }

        internal static void Serialize(double[] val, Buffer b, uint count)
        {
            SerializeStructArray(val, b, count);
        }

        internal static void SerializeStruct<T>(in T val, Buffer b) where T : unmanaged
        {
            AssertInRange(b.ptr, (uint)sizeof(T), b.end);
            *(T*)b.ptr = val;
            b.ptr += sizeof(T);
        }

        internal static void SerializeStructArray<T>(T[] val, Buffer b, uint count) where T : unmanaged
        {
            if (val is null)
            {
                throw new ArgumentNullException(nameof(val));
            }

            if (count == 0)
            {
                AssertInRange(b.ptr, (uint)(4 + val.Length * sizeof(T)), b.end);
                *(int*)b.ptr = val.Length; b.ptr += 4;
            }
            else
            {
                AssertSize(val, count);
                AssertInRange(b.ptr, count * (uint)sizeof(T), b.end);
            }
            fixed (T* b_ptr = val)
            {
                uint size = (uint)(val.Length * sizeof(T));
                Memcpy(b.ptr, b_ptr, size);
                b.ptr += size;
            }
        }


        internal static void SerializeArray<T>(T[] val, Buffer b, uint count) where T : IMessage
        {
            if (val is null)
            {
                throw new ArgumentNullException(nameof(val));
            }

            if (count == 0)
            {
                AssertInRange(b.ptr, 4, b.end);
                *(int*)b.ptr = val.Length; b.ptr += 4;
            }
            else
            {
                AssertSize(val, count);
            }
            for (int i = 0; i < val.Length; i++)
            {
                val[i].Serialize(b);
            }
        }

        #endregion

        public static T Deserialize<T>(ISerializable<T> generator, byte[] buffer, int size)
        {
            if (generator is null)
            {
                throw new ArgumentNullException(nameof(generator));
            }

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
                return generator.Deserialize(b);
                //return (uint)(b.ptr - b_ptr);
            }
        }
        
        public static uint Serialize<T>(ISerializable<T> message, byte[] buffer)
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
        */

        public static string GetClassStringConstant(Type type, string name)
        {
            string constant = (string)type?.GetField(name)?.GetRawConstantValue();
            if (constant == null)
            {
                throw new ArgumentException("Failed to resolve constant '" + name + "' in class " + type.FullName, nameof(name));
            }
            return constant;
        }

        public static string GetMessageType(Type type)
        {
            return GetClassStringConstant(type, "RosMessageType");
        }

        public static string GetServiceType(Type type)
        {
            return GetClassStringConstant(type, "RosServiceType");
        }

        public static string GetMd5Sum(Type type)
        {
            return GetClassStringConstant(type, "RosMd5Sum");
        }

        public static string GetDependenciesBase64(Type type)
        {
            return GetClassStringConstant(type, "RosDepb.endenciesBase64");
        }

        public static IMessage CreateGenerator(Type type)
        {
            return (IMessage)Activator.CreateInstance(type);
        }

        public static string DecompressDependency(Type type)
        {
            string DependenciesBase64 = GetDependenciesBase64(type);
            byte[] inputBytes = Convert.FromBase64String(DependenciesBase64);

            StringBuilder str = new StringBuilder();
            byte[] outputBytes = new byte[32];

            using (var inputStream = new MemoryStream(inputBytes))
            using (var gZipStream = new GZipStream(inputStream, CompressionMode.Decompress))
            {
                int read;
                do
                {
                    read = gZipStream.Read(outputBytes, 0, outputBytes.Length);
                    str.Append(UTF8.GetString(outputBytes, 0, read));
                }
                while (read != 0);
                return str.ToString();
            }
        }
    }

}