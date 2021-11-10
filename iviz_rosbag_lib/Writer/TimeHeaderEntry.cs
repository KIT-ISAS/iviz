using System.IO;
using System.Threading.Tasks;
using Iviz.Msgs;

namespace Iviz.Rosbag.Writer
{
    internal readonly struct TimeHeaderEntry
    {
        readonly string name;
        readonly time value;

        public TimeHeaderEntry(string name, time value) => (this.name, this.value) = (name, value);
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