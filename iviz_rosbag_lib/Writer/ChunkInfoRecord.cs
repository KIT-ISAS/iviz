using Iviz.Msgs;

namespace Iviz.Rosbag.Writer
{
    internal readonly struct ChunkInfoRecord
    {
        public long ChunkPos { get; }
        public time StartTime { get; }
        public time EndTime { get; }
        public (int conn, int count)[] MessagesByConnection { get; }

        public ChunkInfoRecord(long chunkPos, time startTime, time endTime, (int, int)[] messagesByConnection) =>
            (this.ChunkPos, this.StartTime, this.EndTime, this.MessagesByConnection) =
            (chunkPos, startTime, endTime, messagesByConnection);
    }
}