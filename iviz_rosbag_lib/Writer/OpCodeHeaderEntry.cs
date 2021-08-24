using System.IO;
using System.Threading.Tasks;

namespace Iviz.Rosbag.Writer
{
    internal readonly struct OpCodeHeaderEntry
    {
        public const int Length = 2 + 1 + 1;
        readonly OpCode value;

        public OpCodeHeaderEntry(OpCode value) => this.value = value;

        public Stream Write(Stream stream)
        {
            using var rent = new RentStream(Length + 4);
            rent.Write(Length);
            rent.Write("op=");
            rent.Write(value);
            return rent.WriteTo(stream);
        }        
        
        public async Task WriteAsync(Stream stream)
        {
            using var rent = new RentStream(Length + 4);
            rent.Write(Length);
            rent.Write("op=");
            rent.Write(value);
            await rent.WriteToAsync(stream);
        }
    }
}