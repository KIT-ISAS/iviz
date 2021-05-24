using System.IO;

namespace Iviz.Rosbag.Reader
{
    public readonly struct Chunk
    {
        readonly Stream reader;
        readonly long dataStart;
        readonly long dataEnd;
        readonly bool isCompressed;

        public RecordEnumerable Records => new(new Record(reader, dataStart, dataEnd));

        internal Chunk(Stream reader, long dataStart, long dataEnd, bool isCompressed)
        {
            this.reader = reader;
            this.dataStart = dataStart;
            this.dataEnd = dataEnd;
            this.isCompressed = isCompressed;
        }
    }
}