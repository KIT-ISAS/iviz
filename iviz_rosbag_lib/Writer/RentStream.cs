using System;
using System.IO;
using System.Runtime.CompilerServices;
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

        void Write<T>(T value) where T : unmanaged
        {
            bytes.AsSpan()[p..].Write(value);
            p += Unsafe.SizeOf<T>();
        }

        public void Write(int value)
        {
            Write<int>(value);
        }

        public void Write(in time value)
        {
            Write<time>(value);
        }

        public void Write(OpCode value)
        {
            Write((byte)value);
        }

        public void Write(long value)
        {
            Write<long>(value);
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
            Write((byte)value);
        }

        public readonly Stream WriteTo(Stream stream)
        {
            stream.Write(bytes);
            return stream;
        }

        public readonly ValueTask WriteToAsync(Stream stream)
        {
            return stream.WriteAsync(bytes);
        }
    }
}