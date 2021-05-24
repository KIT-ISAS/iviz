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

        public Stream Write(Stream stream) =>
            stream.WriteValue(Length).WriteValue(name).WriteValue('=').WriteValue(value.Secs).WriteValue(value.Nsecs);
        
        public async Task WriteAsync(Stream stream)
        {
            await stream.WriteValueAsync(Length);
            await stream.WriteValueAsync(name);
            await stream.WriteValueAsync('=');
            await stream.WriteValueAsync(value.Secs);
            await stream.WriteValueAsync(value.Nsecs);
        }        
    }
}