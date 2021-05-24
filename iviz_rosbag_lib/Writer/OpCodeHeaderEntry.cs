using System.IO;
using System.Threading.Tasks;
using Iviz.Msgs;

namespace Iviz.Rosbag.Writer
{
    internal readonly struct OpCodeHeaderEntry
    {
        public const int Length = 2 + 1 + 1;
        readonly OpCode value;

        public OpCodeHeaderEntry(OpCode value) => this.value = value;

        public Stream Write(Stream stream)
        {
            using var bytes = new Rent<byte>(4 + Length);
            byte[] array = bytes.Array;
            array[0] = Length;
            array[1] = 0;
            array[2] = 0;
            array[3] = 0;
            array[4] = (byte) 'o';
            array[5] = (byte) 'p';
            array[6] = (byte) '=';
            array[7] = (byte) value;
            stream.Write(array, 0, 4 + Length);
            return stream;
        }
        
        public async Task WriteAsync(Stream stream)
        {
            using var bytes = new Rent<byte>(4 + Length);
            byte[] array = bytes.Array;
            array[0] = Length;
            array[1] = 0;
            array[2] = 0;
            array[3] = 0;
            array[4] = (byte) 'o';
            array[5] = (byte) 'p';
            array[6] = (byte) '=';
            array[7] = (byte) value;
            await stream.WriteAsync(array, 0, 4 + Length);
        }        
    }
}