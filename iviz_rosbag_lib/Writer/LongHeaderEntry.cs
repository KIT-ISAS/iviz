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
        public Stream Write(Stream stream) => stream.WriteValue(Length).WriteValue(name).WriteValue('=').WriteValue(value);

        public async Task WriteAsync(Stream stream)
        {
            await stream.WriteValueAsync(Length);
            await stream.WriteValueAsync(name);
            await stream.WriteValueAsync('=');
            await stream.WriteValueAsync(value);
        }
    }
}