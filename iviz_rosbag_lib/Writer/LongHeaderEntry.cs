using System.IO;
using System.Threading.Tasks;

namespace Iviz.Rosbag.Writer
{
    internal readonly struct LongHeaderEntry
    {
        readonly string name;
        readonly long value;

        public LongHeaderEntry(string name, long value) => (this.name, this.value) = (name, value);
        public int Length => 8 + 1 + name.Length;

        public Stream Write(Stream stream)
        {
            using var rent = new RentStream(Length + 4);
            rent.Write(Length);
            rent.Write(name);
            rent.Write('=');
            rent.Write(value);
            return rent.WriteTo(stream);
        }

        public async ValueTask WriteAsync(Stream stream)
        {
            using var rent = new RentStream(Length + 4);
            rent.Write(Length);
            rent.Write(name);
            rent.Write('=');
            rent.Write(value);
            await rent.WriteToAsync(stream);
        }
    }
}