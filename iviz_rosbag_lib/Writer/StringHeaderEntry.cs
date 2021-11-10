using System.IO;
using System.Threading.Tasks;
using Iviz.Msgs;

namespace Iviz.Rosbag.Writer
{
    internal readonly struct StringHeaderEntry
    {
        readonly string name;
        readonly string value;

        public StringHeaderEntry(string name, string value) => (this.name, this.value) = (name, value);
        public int Length => 1 + name.Length + BuiltIns.UTF8.GetByteCount(value);

        public Stream Write(Stream stream)
        {
            using var rent = new RentStream(Length + 4);
            rent.Write(Length);
            rent.Write(name);
            rent.Write('=');
            rent.WriteUtf8(value);
            return rent.WriteTo(stream);
        }        
        
        public async ValueTask WriteAsync(Stream stream)
        {
            using var rent = new RentStream(Length + 4);
            rent.Write(Length);
            rent.Write(name);
            rent.Write('=');
            rent.WriteUtf8(value);
            await rent.WriteToAsync(stream);
        }
    }
}