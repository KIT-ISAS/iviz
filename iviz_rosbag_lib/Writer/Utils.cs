using System.IO;
using System.Threading.Tasks;
using Iviz.Msgs;

namespace Iviz.Rosbag.Writer
{
    internal static class Utils
    {
        public static Stream WriteValue(this Stream stream, in IntHeaderEntry value) => value.Write(stream);
        public static Stream WriteValue(this Stream stream, in LongHeaderEntry value) => value.Write(stream);
        public static Stream WriteValue(this Stream stream, in OpCodeHeaderEntry value) => value.Write(stream);
        public static Stream WriteValue(this Stream stream, in StringHeaderEntry value) => value.Write(stream);
        public static Stream WriteValue(this Stream stream, in TimeHeaderEntry value) => value.Write(stream);


        public static Task WriteValueAsync(this Stream stream, in IntHeaderEntry value) => value.WriteAsync(stream);
        public static Task WriteValueAsync(this Stream stream, in LongHeaderEntry value) => value.WriteAsync(stream);
        public static Task WriteValueAsync(this Stream stream, in OpCodeHeaderEntry value) => value.WriteAsync(stream);
        public static Task WriteValueAsync(this Stream stream, in StringHeaderEntry value) => value.WriteAsync(stream);
        public static Task WriteValueAsync(this Stream stream, in TimeHeaderEntry value) => value.WriteAsync(stream);


        public static Stream WriteValue(this Stream stream, in Rent<byte> value)
        {
            stream.Write(value.Array, 0, value.Length);
            return stream;
        }

        public static Task WriteValueAsync(this Stream stream, Rent<byte> value)
        {
            return stream.WriteAsync(value.Array, 0, value.Length);
        }

        public static Stream WriteValue(this Stream stream, int value)
        {
            using var bytes = new Rent<byte>(4);
            byte[] array = bytes.Array;
            array[3] = (byte) (value >> 24);
            array[0] = (byte) value;
            array[1] = (byte) (value >> 8);
            array[2] = (byte) (value >> 16);
            stream.Write(array, 0, 4);
            return stream;
        }

        public static async Task WriteValueAsync(this Stream stream, int value)
        {
            using var bytes = new Rent<byte>(4);
            byte[] array = bytes.Array;
            array[3] = (byte) (value >> 24);
            array[0] = (byte) value;
            array[1] = (byte) (value >> 8);
            array[2] = (byte) (value >> 16);
            await stream.WriteAsync(array, 0, 4);
        }

        public static Stream WriteValue(this Stream stream, uint value) => stream.WriteValue((int) value);

        public static Task WriteValueAsync(this Stream stream, uint value) => stream.WriteValueAsync((int) value);

        public static Stream WriteValue(this Stream stream, long value)
        {
            using var bytes = new Rent<byte>(8);
            byte[] array = bytes.Array;
            array[7] = (byte) (value >> 56);
            array[0] = (byte) value;
            array[1] = (byte) (value >> 8);
            array[2] = (byte) (value >> 16);
            array[3] = (byte) (value >> 24);
            array[4] = (byte) (value >> 32);
            array[5] = (byte) (value >> 40);
            array[6] = (byte) (value >> 48);
            stream.Write(array, 0, 8);
            return stream;
        }

        public static async Task WriteValueAsync(this Stream stream, long value)
        {
            using var bytes = new Rent<byte>(8);
            byte[] array = bytes.Array;
            array[7] = (byte) (value >> 56);
            array[0] = (byte) value;
            array[1] = (byte) (value >> 8);
            array[2] = (byte) (value >> 16);
            array[3] = (byte) (value >> 24);
            array[4] = (byte) (value >> 32);
            array[5] = (byte) (value >> 40);
            array[6] = (byte) (value >> 48);
            await stream.WriteAsync(array, 0, 8);
        }

        public static Stream WriteValue(this Stream stream, char value)
        {
            stream.WriteByte((byte) value);
            return stream;
        }

        public static Stream WriteValue(this Stream stream, byte value)
        {
            stream.WriteByte(value);
            return stream;
        }

        public static Task WriteValueAsync(this Stream stream, char value)
        {
            stream.WriteByte((byte) value);
            return Task.CompletedTask;
        }

        public static Task WriteValueAsync(this Stream stream, byte value)
        {
            stream.WriteByte(value);
            return Task.CompletedTask;
        }

        public static Stream WriteValue(this Stream stream, string value)
        {
            using var bytes = new Rent<byte>(value.Length);
            byte[] array = bytes.Array;
            for (int i = 0; i < value.Length; i++)
            {
                array[i] = (byte) value[i];
            }

            stream.WriteValue(bytes);
            return stream;
        }

        public static async Task WriteValueAsync(this Stream stream, string value)
        {
            using var bytes = new Rent<byte>(value.Length);
            byte[] array = bytes.Array;
            for (int i = 0; i < value.Length; i++)
            {
                array[i] = (byte) value[i];
            }

            await stream.WriteValueAsync(bytes);
        }

        public static Stream WriteValueUtf8(this Stream stream, string value)
        {
            int length = BuiltIns.UTF8.GetByteCount(value);
            using var bytes = new Rent<byte>(length);
            BuiltIns.UTF8.GetBytes(value, 0, value.Length, bytes.Array, 0);
            stream.WriteValue(bytes);
            return stream;
        }

        public static async Task WriteValueUtf8Async(this Stream stream, string value)
        {
            int length = BuiltIns.UTF8.GetByteCount(value);
            using var bytes = new Rent<byte>(length);
            BuiltIns.UTF8.GetBytes(value, 0, value.Length, bytes.Array, 0);
            await stream.WriteValueAsync(bytes);
        }
    }
}