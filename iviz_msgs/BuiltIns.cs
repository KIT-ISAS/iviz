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

        static void Memcpy(void* dst, void* src, uint size)
        {
            Buffer.MemoryCopy(src, dst, size, size);
        }

        internal static void AssertInRange(byte* arrayPtr, uint off, byte* end)
        {
            if (arrayPtr + off > end)
                throw new ArgumentOutOfRangeException(nameof(arrayPtr));
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

        internal static void Deserialize(out string val, ref byte* arrayPtr, byte* end)
        {
            AssertInRange(arrayPtr, 4, end);
            uint count = *(uint*)arrayPtr; arrayPtr += 4;
            if (count == 0) { val = string.Empty; return; }
            AssertInRange(arrayPtr, count, end);
            val = UTF8.GetString(arrayPtr, (int)count);
            arrayPtr += count;
        }

        internal static void Deserialize(out string[] val, ref byte* arrayPtr, byte* end, uint count)
        {
            if (count == 0)
            {
                AssertInRange(arrayPtr, 4, end);
                count = *(uint*)arrayPtr; arrayPtr += 4;
                if (count == 0) { val = Array.Empty<string>(); return; }
            }
            val = new string[count];
            for (int i = 0; i < val.Length; i++)
            {
                Deserialize(out val[i], ref arrayPtr, end);
            }
        }

        internal static void Deserialize(out bool val, ref byte* arrayPtr, byte* _)
        {
            val = (*arrayPtr != 0);
            arrayPtr++;
        }

        internal static void Deserialize(out time val, ref byte* arrayPtr, byte* end)
        {
            AssertInRange(arrayPtr, 8, end);
            uint secs = *(uint*)arrayPtr; arrayPtr += 4;
            uint nsecs = *(uint*)arrayPtr; arrayPtr += 4;
            val = new time(secs, nsecs);
        }

        internal static void Deserialize(out time[] val, ref byte* arrayPtr, byte* end, uint count)
        {
            DeserializeStructArray(out val, ref arrayPtr, end, count);
        }

        internal static void Deserialize(out duration val, ref byte* arrayPtr, byte* end)
        {
            AssertInRange(arrayPtr, 8, end);
            int secs = *(int*)arrayPtr; arrayPtr += 4;
            int nsecs = *(int*)arrayPtr; arrayPtr += 4;
            val = new duration(secs, nsecs);
        }

        internal static void Deserialize(out duration[] val, ref byte* arrayPtr, byte* end, uint count)
        {
            DeserializeStructArray(out val, ref arrayPtr, end, count);
        }

        internal static void Deserialize(out bool[] val, ref byte* arrayPtr, byte* end, uint count)
        {
            if (count == 0)
            {
                AssertInRange(arrayPtr, 4, end);
                count = *(uint*)arrayPtr; arrayPtr += 4;
                if (count == 0) { val = Array.Empty<bool>(); return; }
            }
            AssertInRange(arrayPtr, count * 1, end);
            val = new bool[count];
            fixed (bool* val_arrayPtr = val)
            {
                uint size = count * 1;
                Memcpy(val_arrayPtr, arrayPtr, size);
                arrayPtr += size;
            }
        }

        internal static void Deserialize(out char val, ref byte* arrayPtr, byte* end)
        {
            AssertInRange(arrayPtr, 1, end);
            val = *(char*)arrayPtr;
            arrayPtr += 1;
        }

        internal static void Deserialize(out char[] val, ref byte* arrayPtr, byte* end, uint count)
        {
            DeserializeStructArray(out val, ref arrayPtr, end, count);
        }


        internal static void Deserialize(out byte val, ref byte* arrayPtr, byte* end)
        {
            AssertInRange(arrayPtr, 1, end);
            val = *arrayPtr;
            arrayPtr += 1;
        }

        internal static void Deserialize(out byte[] val, ref byte* arrayPtr, byte* end, uint count)
        {
            DeserializeStructArray(out val, ref arrayPtr, end, count);
        }

        internal static void Deserialize(out sbyte val, ref byte* arrayPtr, byte* end)
        {
            AssertInRange(arrayPtr, 1, end);
            val = *(sbyte*)arrayPtr;
            arrayPtr += 1;
        }

        internal static void Deserialize(out sbyte[] val, ref byte* arrayPtr, byte* end, uint count)
        {
            DeserializeStructArray(out val, ref arrayPtr, end, count);
        }

        internal static void Deserialize(out short val, ref byte* arrayPtr, byte* end)
        {
            AssertInRange(arrayPtr, 2, end);
            val = *(short*)arrayPtr;
            arrayPtr += 2;
        }

        internal static void Deserialize(out short[] val, ref byte* arrayPtr, byte* end, uint count)
        {
            DeserializeStructArray(out val, ref arrayPtr, end, count);
        }

        internal static void Deserialize(out ushort val, ref byte* arrayPtr, byte* end)
        {
            AssertInRange(arrayPtr, 2, end);
            val = *(ushort*)arrayPtr;
            arrayPtr += 2;
        }

        internal static void Deserialize(out ushort[] val, ref byte* arrayPtr, byte* end, uint count)
        {
            DeserializeStructArray(out val, ref arrayPtr, end, count);
        }

        internal static void Deserialize(out int val, ref byte* arrayPtr, byte* end)
        {
            AssertInRange(arrayPtr, 4, end);
            val = *(int*)arrayPtr;
            arrayPtr += 4;
        }

        internal static void Deserialize(out int[] val, ref byte* arrayPtr, byte* end, uint count)
        {
            DeserializeStructArray(out val, ref arrayPtr, end, count);
        }

        internal static void Deserialize(out uint val, ref byte* arrayPtr, byte* end)
        {
            AssertInRange(arrayPtr, 4, end);
            val = *(uint*)arrayPtr;
            arrayPtr += 4;
        }

        internal static void Deserialize(out uint[] val, ref byte* arrayPtr, byte* end, uint count)
        {
            DeserializeStructArray(out val, ref arrayPtr, end, count);
        }

        internal static void Deserialize(out long val, ref byte* arrayPtr, byte* end)
        {
            AssertInRange(arrayPtr, 8, end);
            val = *(long*)arrayPtr;
            arrayPtr += 8;
        }

        internal static void Deserialize(out long[] val, ref byte* arrayPtr, byte* end, uint count)
        {
            DeserializeStructArray(out val, ref arrayPtr, end, count);
        }

        internal static void Deserialize(out ulong val, ref byte* arrayPtr, byte* end)
        {
            AssertInRange(arrayPtr, 8, end);
            val = *(ulong*)arrayPtr;
            arrayPtr += 8;
        }

        internal static void Deserialize(out ulong[] val, ref byte* arrayPtr, byte* end, uint count)
        {
            DeserializeStructArray(out val, ref arrayPtr, end, count);
        }

        internal static void Deserialize(out float val, ref byte* arrayPtr, byte* end)
        {
            AssertInRange(arrayPtr, 4, end);
            val = *(float*)arrayPtr;
            arrayPtr += 4;
        }

        internal static void Deserialize(out float[] val, ref byte* arrayPtr, byte* end, uint count)
        {
            DeserializeStructArray(out val, ref arrayPtr, end, count);
        }

        internal static void Deserialize(out double val, ref byte* arrayPtr, byte* end)
        {
            AssertInRange(arrayPtr, 8, end);
            val = *(double*)arrayPtr;
            arrayPtr += 8;
        }

        internal static void Deserialize(out double[] val, ref byte* arrayPtr, byte* end, uint count)
        {
            DeserializeStructArray(out val, ref arrayPtr, end, count);
        }

        internal static void DeserializeStruct<T>(out T val, ref byte* arrayPtr, byte* end) where T : unmanaged
        {
            AssertInRange(arrayPtr, (uint)sizeof(T), end);
            val = *(T*)arrayPtr;
            arrayPtr += sizeof(T);
        }

        internal static void DeserializeStructArray<T>(out T[] val, ref byte* arrayPtr, byte* end, uint count) where T : unmanaged
        {
            if (count == 0)
            {
                AssertInRange(arrayPtr, 4, end);
                count = *(uint*)arrayPtr; arrayPtr += 4;
                if (count == 0) { val = Array.Empty<T>(); return; }
            }
            AssertInRange(arrayPtr, count * (uint)sizeof(T), end);
            val = new T[count];
            fixed (T* val_arrayPtr = val)
            {
                uint size = count * (uint)sizeof(T);
                Memcpy(val_arrayPtr, arrayPtr, size);
                arrayPtr += size;
            }
        }

        internal static void DeserializeArray<T>(out T[] val, ref byte* arrayPtr, byte* end, uint count) where T : IMessage, new()
        {
            if (count == 0)
            {
                AssertInRange(arrayPtr, 4, end);
                count = *(uint*)arrayPtr; arrayPtr += 4;
                if (count == 0) { val = Array.Empty<T>(); return; }
            }
            val = new T[count];
            for (int i = 0; i < val.Length; i++)
            {
                val[i] = new T();
                val[i].Deserialize(ref arrayPtr, end);
            }
        }

        #endregion



        #region Serializers


        internal static void Serialize(in time val, ref byte* arrayPtr, byte* end)
        {
            SerializeStruct(val, ref arrayPtr, end);
        }

        internal static void Serialize(time[] val, ref byte* arrayPtr, byte* end, uint count)
        {
            SerializeStructArray(val, ref arrayPtr, end, count);
        }

        internal static void Serialize(in duration val, ref byte* arrayPtr, byte* end)
        {
            SerializeStruct(val, ref arrayPtr, end);
        }

        internal static void Serialize(duration[] val, ref byte* arrayPtr, byte* end, uint count)
        {
            SerializeStructArray(val, ref arrayPtr, end, count);
        }

        internal static void Serialize(bool val, ref byte* arrayPtr, byte* end)
        {
            AssertInRange(arrayPtr, 1, end);
            *arrayPtr = val ? (byte)1 : (byte)0;
            arrayPtr++;
        }

        internal static void Serialize(string val, ref byte* arrayPtr, byte* end)
        {
            if (val is null)
            {
                throw new ArgumentNullException(nameof(val));
            }

            uint count = (uint)UTF8.GetByteCount(val);
            AssertInRange(arrayPtr, 4 + count, end);
            *(uint*)arrayPtr = count; arrayPtr += 4;
            if (count == 0) return;
            fixed (char* val_arrayPtr = val)
            {
                UTF8.GetBytes(val_arrayPtr, val.Length, arrayPtr, (int)count);
                arrayPtr += count;
            }
        }

        internal static void Serialize(string[] val, ref byte* arrayPtr, byte* end, uint count)
        {
            if (val is null)
            {
                throw new ArgumentNullException(nameof(val));
            }

            if (count == 0)
            {
                AssertInRange(arrayPtr, 4, end);
                *(int*)arrayPtr = val.Length; arrayPtr += 4;
            }
            else
            {
                AssertSize(val, count);
            }
            for (int i = 0; i < val.Length; i++)
            {
                Serialize(val[i], ref arrayPtr, end);
            }
        }

        internal static void Serialize(bool[] val, ref byte* arrayPtr, byte* end, uint count)
        {
            if (val is null)
            {
                throw new ArgumentNullException(nameof(val));
            }

            if (count == 0)
            {
                AssertInRange(arrayPtr, (uint)(4 + val.Length * 1), end);
                *(int*)arrayPtr = val.Length; arrayPtr += 4;
            }
            else
            {
                AssertSize(val, count);
                AssertInRange(arrayPtr, count * 1, end);
            }
            fixed (bool* val_arrayPtr = val)
            {
                uint size = (uint)(val.Length * 1);
                Memcpy(arrayPtr, val_arrayPtr, size);
                arrayPtr += size;
            }
        }

        internal static void Serialize(byte val, ref byte* arrayPtr, byte* end)
        {
            AssertInRange(arrayPtr, 1, end);
            *arrayPtr = val;
            arrayPtr += 1;
        }

        internal static void Serialize(byte[] val, ref byte* arrayPtr, byte* end, uint count)
        {
            SerializeStructArray(val, ref arrayPtr, end, count);
        }

        internal static void Serialize(char val, ref byte* arrayPtr, byte* end)
        {
            AssertInRange(arrayPtr, 1, end);
            *(char*)arrayPtr = val;
            arrayPtr += 1;
        }

        internal static void Serialize(char[] val, ref byte* arrayPtr, byte* end, uint count)
        {
            SerializeStructArray(val, ref arrayPtr, end, count);
        }

        internal static void Serialize(sbyte val, ref byte* arrayPtr, byte* end)
        {
            AssertInRange(arrayPtr, 1, end);
            *(sbyte*)arrayPtr = val;
            arrayPtr += 1;
        }

        internal static void Serialize(sbyte[] val, ref byte* arrayPtr, byte* end, uint count)
        {
            SerializeStructArray(val, ref arrayPtr, end, count);
        }

        internal static void Serialize(short val, ref byte* arrayPtr, byte* end)
        {
            AssertInRange(arrayPtr, 2, end);
            *(short*)arrayPtr = val;
            arrayPtr += 2;
        }

        internal static void Serialize(short[] val, ref byte* arrayPtr, byte* end, uint count)
        {
            SerializeStructArray(val, ref arrayPtr, end, count);
        }

        internal static void Serialize(ushort val, ref byte* arrayPtr, byte* end)
        {
            AssertInRange(arrayPtr, 2, end);
            *(ushort*)arrayPtr = val;
            arrayPtr += 2;
        }

        internal static void Serialize(ushort[] val, ref byte* arrayPtr, byte* end, uint count)
        {
            SerializeStructArray(val, ref arrayPtr, end, count);
        }

        internal static void Serialize(int val, ref byte* arrayPtr, byte* end)
        {
            AssertInRange(arrayPtr, 4, end);
            *(int*)arrayPtr = val;
            arrayPtr += 4;
        }

        internal static void Serialize(int[] val, ref byte* arrayPtr, byte* end, uint count)
        {
            SerializeStructArray(val, ref arrayPtr, end, count);
        }

        internal static void Serialize(uint val, ref byte* arrayPtr, byte* end)
        {
            AssertInRange(arrayPtr, 4, end);
            *(uint*)arrayPtr = val;
            arrayPtr += 4;
        }

        internal static void Serialize(uint[] val, ref byte* arrayPtr, byte* end, uint count)
        {
            SerializeStructArray(val, ref arrayPtr, end, count);
        }

        internal static void Serialize(long val, ref byte* arrayPtr, byte* end)
        {
            AssertInRange(arrayPtr, 8, end);
            *(long*)arrayPtr = val;
            arrayPtr += 8;
        }

        internal static void Serialize(long[] val, ref byte* arrayPtr, byte* end, uint count)
        {
            SerializeStructArray(val, ref arrayPtr, end, count);
        }

        internal static void Serialize(ulong val, ref byte* arrayPtr, byte* end)
        {
            AssertInRange(arrayPtr, 8, end);
            *(ulong*)arrayPtr = val;
            arrayPtr += 8;
        }

        internal static void Serialize(ulong[] val, ref byte* arrayPtr, byte* end, uint count)
        {
            SerializeStructArray(val, ref arrayPtr, end, count);
        }

        internal static void Serialize(float val, ref byte* arrayPtr, byte* end)
        {
            AssertInRange(arrayPtr, 4, end);
            *(float*)arrayPtr = val;
            arrayPtr += 4;
        }

        internal static void Serialize(float[] val, ref byte* arrayPtr, byte* end, uint count)
        {
            SerializeStructArray(val, ref arrayPtr, end, count);
        }

        internal static void Serialize(double val, ref byte* arrayPtr, byte* end)
        {
            AssertInRange(arrayPtr, 8, end);
            *(double*)arrayPtr = val;
            arrayPtr += 8;
        }

        internal static void Serialize(double[] val, ref byte* arrayPtr, byte* end, uint count)
        {
            SerializeStructArray(val, ref arrayPtr, end, count);
        }

        internal static void SerializeStruct<T>(in T val, ref byte* arrayPtr, byte* end) where T : unmanaged
        {
            AssertInRange(arrayPtr, (uint)sizeof(T), end);
            *(T*)arrayPtr = val;
            arrayPtr += sizeof(T);
        }

        internal static void SerializeStructArray<T>(T[] val, ref byte* arrayPtr, byte* end, uint count) where T : unmanaged
        {
            if (val is null)
            {
                throw new ArgumentNullException(nameof(val));
            }

            if (count == 0)
            {
                AssertInRange(arrayPtr, (uint)(4 + val.Length * sizeof(T)), end);
                *(int*)arrayPtr = val.Length; arrayPtr += 4;
            }
            else
            {
                AssertSize(val, count);
                AssertInRange(arrayPtr, count * (uint)sizeof(T), end);
            }
            fixed (T* val_arrayPtr = val)
            {
                uint size = (uint)(val.Length * sizeof(T));
                Memcpy(arrayPtr, val_arrayPtr, size);
                arrayPtr += size;
            }
        }


        internal static void SerializeArray<T>(T[] val, ref byte* arrayPtr, byte* end, uint count) where T : IMessage
        {
            if (val is null)
            {
                throw new ArgumentNullException(nameof(val));
            }

            if (count == 0)
            {
                AssertInRange(arrayPtr, 4, end);
                *(int*)arrayPtr = val.Length; arrayPtr += 4;
            }
            else
            {
                AssertSize(val, count);
            }
            for (int i = 0; i < val.Length; i++)
            {
                val[i].Serialize(ref arrayPtr, end);
            }
        }

        #endregion


        public static uint Deserialize(ISerializable message, byte[] buffer, int size)
        {
            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            if (buffer is null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }
            if (size > buffer.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(buffer));
            }

            fixed (byte* buffer_arrayPtr = buffer)
            {
                byte* buffer_arrayPtr_off = buffer_arrayPtr;
                message.Deserialize(ref buffer_arrayPtr_off, buffer_arrayPtr + size);
                return (uint)(buffer_arrayPtr_off - buffer_arrayPtr);
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

            fixed (byte* buffer_arrayPtr = buffer)
            {
                byte* buffer_arrayPtr_off = buffer_arrayPtr;
                message.Serialize(ref buffer_arrayPtr_off, buffer_arrayPtr + buffer.Length);
                return (uint)(buffer_arrayPtr_off - buffer_arrayPtr);
            }
        }

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
            return GetClassStringConstant(type, "RosDependenciesBase64");
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