using System.IO;
using System.Runtime.Serialization;

namespace Iviz.Rosbag.Reader;

/// <summary>
/// Information about a ROS connection.
/// </summary>
[DataContract]
public sealed class Connection
{
    internal int ConnectionId { get; }

    /// <summary>
    /// The ROS topic.
    /// </summary>
    [DataMember]
    public string? Topic { get; }

    /// <summary>
    /// The ROS message type.
    /// </summary>
    [DataMember]
    public string? MessageType { get; }

    /// <summary>
    /// The MD5 checksum of the ROS message type.
    /// </summary>
    public string? Md5Sum { get; }

    /// <summary>
    /// The text definition of the ROS message type.
    /// </summary>
    public string? MessageDefinition { get; }

    /// <summary>
    /// ROS caller id of the sender.
    /// </summary>
    [DataMember]
    public string? CallerId { get; }

    internal Connection(Stream reader, long dataStart, long dataEnd, int connectionId, string? topic)
    {
        ConnectionId = connectionId;
        Topic = topic;

        var headerEntries = new HeaderEntryEnumerable(new RecordHeaderEntry(reader, dataStart), dataEnd);

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
            else if (CallerId == null && entry.NameEquals("caller_id"))
            {
                CallerId = entry.ValueAsString;
            }
        }
    }
}