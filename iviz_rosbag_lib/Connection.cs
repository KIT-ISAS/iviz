using System.IO;

namespace Iviz.Rosbag
{
    public sealed class Connection
    {
        internal int ConnectionId { get; }
        public string? Topic { get; }
        public string? MessageType { get; }
        public string? Md5Sum { get; }
        public string? MessageDefinition { get; }
        public string? CallerId { get; }

        readonly HeaderEntryEnumerable headerEntries;
        
        internal Connection(Stream reader, long dataStart, long dataEnd, int connectionId, string? topic)
        {
            ConnectionId = connectionId;
            Topic = topic;

            headerEntries = new HeaderEntryEnumerable(new HeaderEntry(reader, dataStart, dataEnd));

            foreach (var entry in headerEntries)
            {
                if (MessageType == null && entry.NameEquals("type"))
                {
                    MessageType = entry.ValueAsString;
                }
                else if (Md5Sum == null && entry.NameEquals("md5sum"))
                {
                    Md5Sum = entry.ValueAsString;
                }
                else if (MessageDefinition == null && entry.NameEquals("message_definition"))
                {
                    MessageDefinition = entry.ValueAsString;
                }
                else if (Topic == null && entry.NameEquals("topic"))
                {
                    Topic = entry.ValueAsString;
                }
                else if (CallerId == null && entry.NameEquals("topic"))
                {
                    CallerId = entry.ValueAsString;
                }
            }
        }

        public bool TryGetHeaderEntry(string name, out string value)
        {
            foreach (var entry in headerEntries)
            {
                if (entry.NameEquals(name))
                {
                    value = entry.ValueAsString;
                    return true;
                }
            }

            value = "";
            return false;
        } 
    }
}