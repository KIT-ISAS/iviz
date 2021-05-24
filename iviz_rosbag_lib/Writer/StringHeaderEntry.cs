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

        public Stream Write(Stream stream) =>
            stream.WriteValue(Length).WriteValue(name).WriteValue('=').WriteValue(value);

        public async Task WriteAsync(Stream stream)
        {
            await stream.WriteValueAsync(Length);
            await stream.WriteValueAsync(name);
            await stream.WriteValueAsync('=');
            await stream.WriteValueAsync(value);
        }
    }
}