namespace Iviz.Rosbag
{
    public enum OpCode
    {
        Unknown = 0x00,
        BagHeader = 0x03,
        Chunk = 0x05,
        Connection = 0x07,
        MessageData = 0x02,
        IndexData = 0x04,
        ChunkInfo = 0x06,
    }
}