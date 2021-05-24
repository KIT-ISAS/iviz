using System.IO;
using System.Threading.Tasks;

namespace Iviz.Rosbag.Writer
{
    internal readonly struct IntHeaderEntry
    {
        readonly string name;
        readonly int value;

        public IntHeaderEntry(string name, int value) => (this.name, this.value) = (name, value);
        public int Length => 4 + 1 + name.Length;
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