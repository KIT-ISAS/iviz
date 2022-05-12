using System;
using System.IO;
using System.Threading.Tasks;
using Iviz.Tools;

namespace Iviz.Rosbag.Writer
{
    internal static class Utils
    {
        public static Stream WriteValue(this Stream stream, in IntHeaderEntry value) => value.Write(stream);
        public static Stream WriteValue(this Stream stream, in LongHeaderEntry value) => value.Write(stream);
        public static Stream WriteValue(this Stream stream, in OpCodeHeaderEntry value) => value.Write(stream);
        public static Stream WriteValue(this Stream stream, in StringHeaderEntry value) => value.Write(stream);
        public static Stream WriteValue(this Stream stream, in TimeHeaderEntry value) => value.Write(stream);


        public static ValueTask WriteValueAsync(this Stream stream, in IntHeaderEntry value) => value.WriteAsync(stream);
        public static ValueTask WriteValueAsync(this Stream stream, in LongHeaderEntry value) => value.WriteAsync(stream);
        public static ValueTask WriteValueAsync(this Stream stream, in OpCodeHeaderEntry value) => value.WriteAsync(stream);
        public static ValueTask WriteValueAsync(this Stream stream, in StringHeaderEntry value) => value.WriteAsync(stream);
        public static ValueTask WriteValueAsync(this Stream stream, in TimeHeaderEntry value) => value.WriteAsync(stream);


        public static Stream WriteValue(this Stream stream, int value)
        {
            Span<byte> bytes = stackalloc byte[4];
            bytes.WriteInt(value);
            stream.Write(bytes);
            return stream;
        }

        public static async ValueTask WriteValueAsync(this Stream stream, int value)
        {
            using var bytes = new Rent<byte>(4);
            bytes.AsSpan().WriteInt(value);
            await stream.WriteAsync(bytes);
        }

        public static Stream WriteValue(this Stream stream, uint value) => stream.WriteValue((int) value);

        public static ValueTask WriteValueAsync(this Stream stream, uint value) => stream.WriteValueAsync((int) value);

        public static Stream WriteValueAscii(this Stream stream, string value)
        {
            using var bytes = new Rent<byte>(value.Length);
            byte[] array = bytes.Array;
            for (int i = 0; i < value.Length; i++)
            {
                array[i] = (byte) value[i];
            }

            stream.Write(bytes);
            return stream;
        }

        public static async ValueTask WriteValueAsciiAsync(this Stream stream, string value)
        {
            using var bytes = new Rent<byte>(value.Length);
            byte[] array = bytes.Array;
            for (int i = 0; i < value.Length; i++)
            {
                array[i] = (byte) value[i];
            }

            await stream.WriteAsync(bytes);
        }

        public static Stream WriteValueUtf8(this Stream stream, string value)
        {
            using var bytes = value.AsRent();
            stream.Write(bytes);
            return stream;
        }

        public static async ValueTask WriteValueUtf8Async(this Stream stream, string value)
        {
            using var bytes = value.AsRent();
            await stream.WriteAsync(bytes);
        }
    }
}