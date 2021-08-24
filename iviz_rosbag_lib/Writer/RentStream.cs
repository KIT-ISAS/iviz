using System;
using System.IO;
using System.Threading.Tasks;
using Iviz.Msgs;
using Iviz.Tools;

namespace Iviz.Rosbag.Writer
{
    internal struct RentStream : IDisposable
    {
        readonly Rent<byte> bytes;
        int p;

        public RentStream(int size) => (bytes, p) = (new Rent<byte>(size), 0);

        public void Dispose()
        {
            bytes.Dispose();
        }

        public void Write(int value)
        {
            byte[] array = bytes.Array;
            array[p++] = (byte) value;
            array[p++] = (byte) (value >> 8);
            array[p++] = (byte) (value >> 16);
            array[p++] = (byte) (value >> 24);
        }

        public void Write(uint value)
        {
            byte[] array = bytes.Array;
            array[p++] = (byte) value;
            array[p++] = (byte) (value >> 8);
            array[p++] = (byte) (value >> 16);
            array[p++] = (byte) (value >> 24);
        }

        public void Write(in time time)
        {
            Write(time.Secs);
            Write(time.Nsecs);
        }

        public void Write(OpCode value)
        {
            Write((byte) value);
        }

        public void Write(long value)
        {
            byte[] array = bytes.Array;
            array[p++] = (byte) value;
            array[p++] = (byte) (value >> 8);
            array[p++] = (byte) (value >> 16);
            array[p++] = (byte) (value >> 24);
            array[p++] = (byte) (value >> 32);
            array[p++] = (byte) (value >> 40);
            array[p++] = (byte) (value >> 48);
            array[p++] = (byte) (value >> 56);
        }

        public void Write(string value)
        {
            byte[] array = bytes.Array;
            foreach (char t in value)
            {
                array[p++] = (byte) t;
            }
        }

        public void WriteUtf8(string value)
        {
            byte[] array = bytes.Array;
            int length = BuiltIns.UTF8.GetByteCount(value);
            BuiltIns.UTF8.GetBytes(value, 0, value.Length, array, p);
            p += length;
        }

        public void Write(char value)
        {
            byte[] array = bytes.Array;
            array[p++] = (byte) value;
        }

        public void Write(byte value)
        {
            byte[] array = bytes.Array;
            array[p++] = value;
        }

        public readonly Stream WriteTo(Stream stream)
        {
            stream.Write(bytes.Array, 0, bytes.Length);
            return stream;
        }

        public readonly Task WriteToAsync(Stream stream)
        {
            return stream.WriteAsync(bytes.Array, 0, bytes.Length);
        }
    }
}