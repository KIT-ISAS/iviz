using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace Iviz.Msgs
{
    public unsafe static class BuiltIns
    {
        public static readonly double[] zc_double = new double[0];
        public static readonly float[] zc_float = new float[0];
        public static readonly ushort[] zc_ushort = new ushort[0];
        public static readonly short[] zc_short = new short[0];
        public static readonly uint[] zc_uint = new uint[0];
        public static readonly int[] zc_int = new int[0];
        public static readonly string[] zc_string = new string[0];
        public static readonly long[] zc_long = new long[0];
        public static readonly ulong[] zc_ulong = new ulong[0];
        public static readonly byte[] zc_byte = new byte[0];
        public static readonly sbyte[] zc_sbyte = new sbyte[0];

        static void Memcpy(void* dst, void* src, uint size)
        {
            Buffer.MemoryCopy(src, dst, size, size);
        }

        public static void AssertInRange(byte* ptr, uint off, byte* end)
        {
            if (ptr + off > end)
                throw new ArgumentOutOfRangeException();
        }

        public static void AssertSize<T>(T[] array, uint size)
        {
            if (array.Length != size)
                throw new ArgumentException("Invalid array size. Expected " + size + ", but got " + array.Length + ".");
        }

        #region Deserializers

        public static void Deserialize(out string obj, ref byte* ptr, byte* end)
        {
            AssertInRange(ptr, 4, end);
            uint count = *(uint*)ptr; ptr += 4;
            if (count == 0) { obj = string.Empty; return; }
            AssertInRange(ptr, count, end);
            obj = System.Text.Encoding.UTF8.GetString(ptr, (int)count);
            ptr += count;
        }

        public static void Deserialize(out string[] obj, ref byte* ptr, byte* end, uint count)
        {
            if (count == 0)
            {
                AssertInRange(ptr, 4, end);
                count = *(uint*)ptr; ptr += 4;
                if (count == 0) { obj = zc_string; return; }
            }
            obj = new string[count];
            for (int i = 0; i < obj.Length; i++)
            {
                Deserialize(out obj[i], ref ptr, end);
            }
        }

        public static void Deserialize(out bool obj, ref byte* ptr, byte* end)
        {
            obj = (*ptr != 0);
            ptr++;
        }

        public static void Deserialize(out time obj, ref byte* ptr, byte* end)
        {
            AssertInRange(ptr, 8, end);
            uint secs = *(uint*)ptr; ptr += 4;
            uint nsecs = *(uint*)ptr; ptr += 4;
            obj = new time(secs, nsecs);
        }

        public static void Deserialize(out duration obj, ref byte* ptr, byte* end)
        {
            AssertInRange(ptr, 8, end);
            int secs = *(int*)ptr; ptr += 4;
            int nsecs = *(int*)ptr; ptr += 4;
            obj = new duration(secs, nsecs);
        }

        public static void Deserialize(out bool[] obj, ref byte* ptr, byte* end, uint count)
        {
            if (count == 0)
            {
                AssertInRange(ptr, 4, end);
                count = *(uint*)ptr; ptr += 4;
            }
            AssertInRange(ptr, count * 1, end);
            obj = new bool[count];
            fixed (bool* obj_ptr = obj)
            {
                uint size = count * 1;
                Memcpy(obj_ptr, ptr, size);
                ptr += size;
            }
        }

        public static void Deserialize(out char obj, ref byte* ptr, byte* end)
        {
            AssertInRange(ptr, 1, end);
            obj = *(char*)ptr;
            ptr += 1;
        }

        public static void Deserialize(out byte obj, ref byte* ptr, byte* end)
        {
            AssertInRange(ptr, 1, end);
            obj = *ptr;
            ptr += 1;
        }

        public static void Deserialize(out byte[] obj, ref byte* ptr, byte* end, uint count)
        {
            /*
            if (count == 0)
            {
                AssertInRange(ptr, 4, end);
                count = *(uint*)ptr; ptr += 4;
                if (count == 0) { obj = zc_byte; return; }
            }
            AssertInRange(ptr, count * 1, end);
            obj = new byte[count];
            fixed (byte* obj_ptr = obj)
            {
                uint size = count * 1;
                Memcpy(obj_ptr, ptr, size);
                ptr += size;
            }
            */
            DeserializeStructArray(out obj, ref ptr, end, count);
        }

        public static void Deserialize(out sbyte obj, ref byte* ptr, byte* end)
        {
            AssertInRange(ptr, 1, end);
            obj = *(sbyte*)ptr;
            ptr += 1;
        }

        public static void Deserialize(out sbyte[] obj, ref byte* ptr, byte* end, uint count)
        {
            /*
            if (count == 0)
            {
                AssertInRange(ptr, 4, end);
                count = *(uint*)ptr; ptr += 4;
                if (count == 0) { obj = zc_sbyte; return; }
            }
            AssertInRange(ptr, count * 1, end);
            obj = new sbyte[count];
            fixed (sbyte* obj_ptr = obj)
            {
                uint size = count * 1;
                Memcpy(obj_ptr, ptr, size);
                ptr += size;
            }
            */
            DeserializeStructArray(out obj, ref ptr, end, count);
        }

        public static void Deserialize(out short obj, ref byte* ptr, byte* end)
        {
            AssertInRange(ptr, 2, end);
            obj = *(short*)ptr;
            ptr += 2;
        }

        public static void Deserialize(out short[] obj, ref byte* ptr, byte* end, uint count)
        {
            /*
            if (count == 0)
            {
                AssertInRange(ptr, 4, end);
                count = *(uint*)ptr; ptr += 4;
                if (count == 0) { obj = zc_short; return; }
            }
            AssertInRange(ptr, count * 2, end);
            obj = new short[count];
            fixed (short* obj_ptr = obj)
            {
                uint size = count * 2;
                Memcpy(obj_ptr, ptr, size);
                ptr += size;
            }
            */
            DeserializeStructArray(out obj, ref ptr, end, count);
        }

        public static void Deserialize(out ushort obj, ref byte* ptr, byte* end)
        {
            AssertInRange(ptr, 2, end);
            obj = *(ushort*)ptr;
            ptr += 2;
        }

        public static void Deserialize(out ushort[] obj, ref byte* ptr, byte* end, uint count)
        {
            /*
            if (count == 0)
            {
                AssertInRange(ptr, 4, end);
                count = *(uint*)ptr; ptr += 4;
                if (count == 0) { obj = zc_ushort; return; }
            }
            AssertInRange(ptr, count * 2, end);
            obj = new ushort[count];
            fixed (ushort* obj_ptr = obj)
            {
                uint size = count * 2;
                Memcpy(obj_ptr, ptr, size);
                ptr += size;
            }
            */
            DeserializeStructArray(out obj, ref ptr, end, count);
        }

        public static void Deserialize(out int obj, ref byte* ptr, byte* end)
        {
            AssertInRange(ptr, 4, end);
            obj = *(int*)ptr;
            ptr += 4;
        }

        public static void Deserialize(out int[] obj, ref byte* ptr, byte* end, uint count)
        {
            /*
            if (count == 0)
            {
                AssertInRange(ptr, 4, end);
                count = *(uint*)ptr; ptr += 4;
                if (count == 0) { obj = zc_int; return; }
            }
            AssertInRange(ptr, count * 4, end);
            obj = new int[count];
            fixed (int* obj_ptr = obj)
            {
                uint size = count * 4;
                Memcpy(obj_ptr, ptr, size);
                ptr += size;
            }
            */
            DeserializeStructArray(out obj, ref ptr, end, count);
        }

        public static void Deserialize(out uint obj, ref byte* ptr, byte* end)
        {
            AssertInRange(ptr, 4, end);
            obj = *(uint*)ptr;
            ptr += 4;
        }

        public static void Deserialize(out uint[] obj, ref byte* ptr, byte* end, uint count)
        {
            /*
            if (count == 0)
            {
                AssertInRange(ptr, 4, end);
                count = *(uint*)ptr; ptr += 4;
                if (count == 0) { obj = zc_uint; return; }
            }
            AssertInRange(ptr, count * 4, end);
            obj = new uint[count];
            fixed (uint* obj_ptr = obj)
            {
                uint size = count * 4;
                Memcpy(obj_ptr, ptr, size);
                ptr += size;
            }
            */
            DeserializeStructArray(out obj, ref ptr, end, count);
        }

        public static void Deserialize(out long obj, ref byte* ptr, byte* end)
        {
            AssertInRange(ptr, 8, end);
            obj = *(long*)ptr;
            ptr += 8;
        }

        public static void Deserialize(out long[] obj, ref byte* ptr, byte* end, uint count)
        {
            /*
            if (count == 0)
            {
                AssertInRange(ptr, 4, end);
                count = *(uint*)ptr; ptr += 4;
                if (count == 0) { obj = zc_long; return; }
            }
            AssertInRange(ptr, count * 8, end);
            obj = new long[count];
            fixed (long* obj_ptr = obj)
            {
                uint size = count * 8;
                Memcpy(obj_ptr, ptr, size);
                ptr += size;
            }
            */
            DeserializeStructArray(out obj, ref ptr, end, count);
        }

        public static void Deserialize(out ulong obj, ref byte* ptr, byte* end)
        {
            AssertInRange(ptr, 8, end);
            obj = *(ulong*)ptr;
            ptr += 8;
        }

        public static void Deserialize(out ulong[] obj, ref byte* ptr, byte* end, uint count)
        {
            /*
            if (count == 0)
            {
                AssertInRange(ptr, 4, end);
                count = *(uint*)ptr; ptr += 4;
                if (count == 0) { obj = zc_ulong; return; }
            }
            AssertInRange(ptr, count * 8, end);
            obj = new ulong[count];
            fixed (ulong* obj_ptr = obj)
            {
                uint size = count * 8;
                Memcpy(obj_ptr, ptr, size);
                ptr += size;
            }
            */
            DeserializeStructArray(out obj, ref ptr, end, count);
        }

        public static void Deserialize(out float obj, ref byte* ptr, byte* end)
        {
            AssertInRange(ptr, 4, end);
            obj = *(float*)ptr;
            ptr += 4;
        }

        public static void Deserialize(out float[] obj, ref byte* ptr, byte* end, uint count)
        {
            /*
            if (count == 0)
            {
                AssertInRange(ptr, 4, end);
                count = *(uint*)ptr; ptr += 4;
                if (count == 0) { obj = zc_float; return; }
            }
            AssertInRange(ptr, count * 4, end);
            obj = new float[count];
            fixed (float* obj_ptr = obj)
            {
                uint size = count * 4;
                Memcpy(obj_ptr, ptr, size);
                ptr += size;
            }
            */
            DeserializeStructArray(out obj, ref ptr, end, count);
        }

        public static void Deserialize(out double obj, ref byte* ptr, byte* end)
        {
            AssertInRange(ptr, 8, end);
            obj = *(double*)ptr;
            ptr += 8;
        }

        public static void Deserialize(out double[] obj, ref byte* ptr, byte* end, uint count)
        {
            /*
            if (count == 0)
            {
                AssertInRange(ptr, 4, end);
                count = *(uint*)ptr; ptr += 4;
                if (count == 0) { obj = zc_double; return; }
            }
            AssertInRange(ptr, count * 8, end);
            obj = new double[count];
            fixed (double* obj_ptr = obj)
            {
                uint size = count * 8;
                Memcpy(obj_ptr, ptr, size);
                ptr += size;
            }
            */
            DeserializeStructArray(out obj, ref ptr, end, count);
        }

        public static void DeserializeStruct<T>(out T obj, ref byte* ptr, byte* end) where T : unmanaged
        {
            AssertInRange(ptr, (uint)sizeof(T), end);
            obj = *(T*)ptr;
            ptr += sizeof(T);
        }

        public static void DeserializeStructArray<T>(out T[] obj, ref byte* ptr, byte* end, uint count) where T : unmanaged
        {
            if (count == 0)
            {
                AssertInRange(ptr, 4, end);
                count = *(uint*)ptr; ptr += 4;
            }
            AssertInRange(ptr, count * (uint)sizeof(T), end);
            obj = new T[count];
            fixed (T* obj_ptr = obj)
            {
                uint size = count * (uint)sizeof(T);
                Memcpy(obj_ptr, ptr, size);
                ptr += size;
            }
        }

        public static void DeserializeArray<T>(out T[] obj, ref byte* ptr, byte* end, uint count) where T : IMessage, new()
        {
            if (count == 0)
            {
                AssertInRange(ptr, 4, end);
                count = *(uint*)ptr; ptr += 4;
            }
            obj = new T[count];
            for (int i = 0; i < obj.Length; i++)
            {
                obj[i] = new T();
                obj[i].Deserialize(ref ptr, end);
            }
        }

        #endregion



        #region Serializers


        public static void Serialize(in time obj, ref byte* ptr, byte* end)
        {
            AssertInRange(ptr, 8, end);
            *(uint*)ptr = obj.secs; ptr += 4;
            *(uint*)ptr = obj.nsecs; ptr += 4;
        }

        public static void Serialize(in duration obj, ref byte* ptr, byte* end)
        {
            AssertInRange(ptr, 8, end);
            *(int*)ptr = obj.secs; ptr += 4;
            *(int*)ptr = obj.nsecs; ptr += 4;
        }

        public static void Serialize(bool obj, ref byte* ptr, byte* end)
        {
            AssertInRange(ptr, 1, end);
            *ptr = obj ? (byte)1 : (byte)0;
            ptr++;
        }

        public static void Serialize(string obj, ref byte* ptr, byte* end)
        {
            uint count = (uint)Encoding.UTF8.GetByteCount(obj);
            AssertInRange(ptr, 4 + count, end);
            *(uint*)ptr = count; ptr += 4;
            if (count == 0) return;
            fixed (char* obj_ptr = obj)
            {
                Encoding.UTF8.GetBytes(obj_ptr, obj.Length, ptr, (int)count);
                ptr += count;
            }
        }

        public static void Serialize(string[] obj, ref byte* ptr, byte* end, uint count)
        {
            if (count == 0)
            {
                AssertInRange(ptr, 4, end);
                *(int*)ptr = obj.Length; ptr += 4;
            }
            else
            {
                AssertSize(obj, count);
            }
            for (int i = 0; i < obj.Length; i++)
            {
                Serialize(obj[i], ref ptr, end);
            }
        }

        public static void Serialize(bool[] obj, ref byte* ptr, byte* end, uint count)
        {
            if (count == 0)
            {
                AssertInRange(ptr, (uint)(4 + obj.Length * 1), end);
                *(int*)ptr = obj.Length; ptr += 4;
            }
            else
            {
                AssertSize(obj, count);
                AssertInRange(ptr, count * 1, end);
            }
            fixed (bool* obj_ptr = obj)
            {
                uint size = (uint)(obj.Length * 1);
                Memcpy(ptr, obj_ptr, size);
                ptr += size;
            }
        }

        public static void Serialize(byte obj, ref byte* ptr, byte* end)
        {
            AssertInRange(ptr, 1, end);
            *(byte*)ptr = obj;
            ptr += 1;
        }

        public static void Serialize(byte[] obj, ref byte* ptr, byte* end, uint count)
        {
            /*
            if (count == 0)
            {
                AssertInRange(ptr, (uint)(4 + obj.Length * 1), end);
                *(int*)ptr = obj.Length; ptr += 4;
            }
            else
            {
                AssertSize(obj, count);
                AssertInRange(ptr, count * 1, end);
            }
            fixed (byte* obj_ptr = obj)
            {
                uint size = (uint)(obj.Length * 1);
                Memcpy(ptr, obj_ptr, size);
                ptr += size;
            }
            */
            SerializeStructArray(obj, ref ptr, end, count);
        }

        public static void Serialize(sbyte obj, ref byte* ptr, byte* end)
        {
            AssertInRange(ptr, 1, end);
            *(sbyte*)ptr = obj;
            ptr += 1;
        }

        public static void Serialize(sbyte[] obj, ref byte* ptr, byte* end, uint count)
        {
            /*
            if (count == 0)
            {
                AssertInRange(ptr, (uint)(4 + obj.Length * 1), end);
                *(int*)ptr = obj.Length; ptr += 4;
            }
            else
            {
                AssertSize(obj, count);
                AssertInRange(ptr, count * 1, end);
            }
            fixed (sbyte* obj_ptr = obj)
            {
                uint size = (uint)(obj.Length * 1);
                Memcpy(ptr, obj_ptr, size);
                ptr += size;
            }
            */
            SerializeStructArray(obj, ref ptr, end, count);
        }

        public static void Serialize(short obj, ref byte* ptr, byte* end)
        {
            AssertInRange(ptr, 2, end);
            *(short*)ptr = obj;
            ptr += 2;
        }

        public static void Serialize(short[] obj, ref byte* ptr, byte* end, uint count)
        {
            /*
            if (count == 0)
            {
                AssertInRange(ptr, (uint)(4 + obj.Length * 2), end);
                *(int*)ptr = obj.Length; ptr += 4;
            }
            else
            {
                AssertSize(obj, count);
                AssertInRange(ptr, count * 2, end);
            }
            fixed (short* obj_ptr = obj)
            {
                uint size = (uint)(obj.Length * 2);
                Memcpy(ptr, obj_ptr, size);
                ptr += size;
            }
            */
            SerializeStructArray(obj, ref ptr, end, count);
        }

        public static void Serialize(ushort obj, ref byte* ptr, byte* end)
        {
            AssertInRange(ptr, 2, end);
            *(ushort*)ptr = obj;
            ptr += 2;
        }

        public static void Serialize(ushort[] obj, ref byte* ptr, byte* end, uint count)
        {
            /*
            if (count == 0)
            {
                AssertInRange(ptr, (uint)(4 + obj.Length * 2), end);
                *(int*)ptr = obj.Length; ptr += 4;
            }
            else
            {
                AssertSize(obj, count);
                AssertInRange(ptr, count * 2, end);
            }
            fixed (ushort* obj_ptr = obj)
            {
                uint size = (uint)(obj.Length * 2);
                Memcpy(ptr, obj_ptr, size);
                ptr += size;
            }
            */
            SerializeStructArray(obj, ref ptr, end, count);
        }

        public static void Serialize(int obj, ref byte* ptr, byte* end)
        {
            AssertInRange(ptr, 4, end);
            *(int*)ptr = obj;
            ptr += 4;
        }

        public static void Serialize(int[] obj, ref byte* ptr, byte* end, uint count)
        {
            /*
            if (count == 0)
            {
                AssertInRange(ptr, (uint)(4 + obj.Length * 4), end);
                *(int*)ptr = obj.Length; ptr += 4;
            }
            else
            {
                AssertSize(obj, count);
                AssertInRange(ptr, count * 4, end);
            }
            fixed (int* obj_ptr = obj)
            {
                uint size = (uint)(obj.Length * 4);
                Memcpy(ptr, obj_ptr, size);
                ptr += size;
            }
            */
            SerializeStructArray(obj, ref ptr, end, count);
        }

        public static void Serialize(uint obj, ref byte* ptr, byte* end)
        {
            AssertInRange(ptr, 4, end);
            *(uint*)ptr = obj;
            ptr += 4;
        }

        public static void Serialize(uint[] obj, ref byte* ptr, byte* end, uint count)
        {
            /*
            if (count == 0)
            {
                AssertInRange(ptr, (uint)(4 + obj.Length * 4), end);
                *(int*)ptr = obj.Length; ptr += 4;
            }
            else
            {
                AssertSize(obj, count);
                AssertInRange(ptr, count * 4, end);
            }
            fixed (uint* obj_ptr = obj)
            {
                uint size = (uint)(obj.Length * 4);
                Memcpy(ptr, obj_ptr, size);
                ptr += size;
            }
            */
            SerializeStructArray(obj, ref ptr, end, count);
        }

        public static void Serialize(long obj, ref byte* ptr, byte* end)
        {
            AssertInRange(ptr, 8, end);
            *(long*)ptr = obj;
            ptr += 8;
        }

        public static void Serialize(long[] obj, ref byte* ptr, byte* end, uint count)
        {
            /*
            if (count == 0)
            {
                AssertInRange(ptr, (uint)(4 + obj.Length * 8), end);
                *(int*)ptr = obj.Length; ptr += 4;
            }
            else
            {
                AssertSize(obj, count);
                AssertInRange(ptr, count * 8, end);
            }
            fixed (long* obj_ptr = obj)
            {
                uint size = (uint)(obj.Length * 8);
                Memcpy(ptr, obj_ptr, size);
                ptr += size;
            }
            */
            SerializeStructArray(obj, ref ptr, end, count);
        }

        public static void Serialize(ulong obj, ref byte* ptr, byte* end)
        {
            AssertInRange(ptr, 8, end);
            *(ulong*)ptr = obj;
            ptr += 8;
        }

        public static void Serialize(ulong[] obj, ref byte* ptr, byte* end, uint count)
        {
            /*
            if (count == 0)
            {
                AssertInRange(ptr, (uint)(4 + obj.Length * 8), end);
                *(int*)ptr = obj.Length; ptr += 4;
            }
            else
            {
                AssertSize(obj, count);
                AssertInRange(ptr, count * 8, end);
            }
            fixed (ulong* obj_ptr = obj)
            {
                uint size = (uint)(obj.Length * 8);
                Memcpy(ptr, obj_ptr, size);
                ptr += size;
            }

            */
            SerializeStructArray(obj, ref ptr, end, count);
        }

        public static void Serialize(float obj, ref byte* ptr, byte* end)
        {
            AssertInRange(ptr, 4, end);
            *(float*)ptr = obj;
            ptr += 4;
        }

        public static void Serialize(float[] obj, ref byte* ptr, byte* end, uint count)
        {
            /*
            if (count == 0)
            {
                AssertInRange(ptr, (uint)(4 + obj.Length * 4), end);
                *(int*)ptr = obj.Length; ptr += 4;
            }
            else
            {
                AssertSize(obj, count);
                AssertInRange(ptr, count * 4, end);
            }
            fixed (float* obj_ptr = obj)
            {
                uint size = (uint)(obj.Length * 4);
                Memcpy(ptr, obj_ptr, size);
                ptr += size;
            }
            */
            SerializeStructArray(obj, ref ptr, end, count);
        }

        public static void Serialize(double obj, ref byte* ptr, byte* end)
        {
            AssertInRange(ptr, 8, end);
            *(double*)ptr = obj;
            ptr += 8;
        }

        public static void Serialize(double[] obj, ref byte* ptr, byte* end, uint count)
        {
            /*
            if (count == 0)
            {
                AssertInRange(ptr, (uint)(4 + obj.Length * 8), end);
                *(int*)ptr = obj.Length; ptr += 4;
            }
            else
            {
                AssertSize(obj, count);
                AssertInRange(ptr, count * 8, end);
            }
            fixed (double* obj_ptr = obj)
            {
                uint size = (uint)(obj.Length * 8);
                Memcpy(ptr, obj_ptr, size);
                ptr += size;
            }
            */
            SerializeStructArray(obj, ref ptr, end, count);
        }

        public static void SerializeStruct<T>(in T obj, ref byte* ptr, byte* end) where T : unmanaged
        {
            AssertInRange(ptr, (uint)sizeof(T), end);
            *(T*)ptr = obj;
            ptr += sizeof(T);
        }

        public static void SerializeStructArray<T>(T[] obj, ref byte* ptr, byte* end, uint count) where T : unmanaged
        {
            if (count == 0)
            {
                AssertInRange(ptr, (uint)(4 + obj.Length * sizeof(T)), end);
                *(int*)ptr = obj.Length; ptr += 4;
            }
            else
            {
                AssertSize(obj, count);
                AssertInRange(ptr, count * (uint)sizeof(T), end);
            }
            fixed (T* obj_ptr = obj)
            {
                uint size = (uint)(obj.Length * sizeof(T));
                Memcpy(ptr, obj_ptr, size);
                ptr += size;
            }
        }


        public static void SerializeArray<T>(T[] obj, ref byte* ptr, byte* end, uint count) where T : IMessage
        {
            if (count == 0)
            {
                AssertInRange(ptr, 4, end);
                *(int*)ptr = obj.Length; ptr += 4;
            }
            else
            {
                AssertSize(obj, count);
            }
            for (int i = 0; i < obj.Length; i++)
            {
                obj[i].Serialize(ref ptr, end);
            }
        }

        public static uint Deserialize(IMessage generator, byte[] buffer, int size, out IMessage message)
        {
            fixed (byte* buffer_ptr = buffer)
            {
                message = generator.Create();
                byte* buffer_ptr_off = buffer_ptr;
                message.Deserialize(ref buffer_ptr_off, buffer_ptr + size);
                return (uint)(buffer_ptr_off - buffer_ptr);
            }

        }
        public static uint Serialize(IMessage msg, byte[] buffer)
        {
            fixed (byte* buffer_ptr = buffer)
            {
                byte* buffer_ptr_off = buffer_ptr;
                msg.Serialize(ref buffer_ptr_off, buffer_ptr + buffer.Length);
                return (uint)(buffer_ptr_off - buffer_ptr);
            }

        }

        #endregion

        public static string GetClassStringConstant(Type type, string name)
        {
            return (string)type?.GetField(name).GetRawConstantValue();
        }

        public static string GetMessageType(Type type)
        {
            return GetClassStringConstant(type, "MessageType");
        }

        public static string GetMd5Sum(Type type)
        {
            return GetClassStringConstant(type, "Md5Sum");
        }

        public static string GetDependenciesBase64(Type type)
        {
            return GetClassStringConstant(type, "DependenciesBase64");
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
                    str.Append(Encoding.UTF8.GetString(outputBytes, 0, read));
                }
                while (read != 0);
                return str.ToString();
            }
        }
    }

}