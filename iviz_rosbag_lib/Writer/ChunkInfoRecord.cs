using Iviz.Msgs;

namespace Iviz.Rosbag.Writer;

internal sealed class ChunkInfoRecord
{
    public long ChunkPos { get; }
    public time StartTime { get; }
    public time EndTime { get; }
    public (int conn, int count)[] MessagesByConnection { get; }

    public ChunkInfoRecord(long chunkPos, time startTime, time endTime, (int, int)[] messagesByConnection) =>
        (ChunkPos, StartTime, EndTime, MessagesByConnection) =
        (chunkPos, startTime, endTime, messagesByConnection);
}