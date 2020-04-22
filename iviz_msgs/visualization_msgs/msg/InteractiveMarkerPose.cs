
namespace Iviz.Msgs.visualization_msgs
{
    public sealed class InteractiveMarkerPose : IMessage
    {
        // Time/frame info.
        public std_msgs.Header header;
        
        // Initial pose. Also, defines the pivot point for rotations.
        public geometry_msgs.Pose pose;
        
        // Identifying string. Must be globally unique in
        // the topic that this message is sent through.
        public string name;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "visualization_msgs/InteractiveMarkerPose";
    
        public IMessage Create() => new InteractiveMarkerPose();
    
        public int GetLength()
        {
            int size = 60;
            size += header.GetLength();
            size += name.Length;
            return size;
        }
    
        /// <summary> Constructor for empty message. </summary>
        public InteractiveMarkerPose()
        {
            header = new std_msgs.Header();
            name = "";
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            header.Deserialize(ref ptr, end);
            pose.Deserialize(ref ptr, end);
            BuiltIns.Deserialize(out name, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            header.Serialize(ref ptr, end);
            pose.Serialize(ref ptr, end);
            BuiltIns.Serialize(name, ref ptr, end);
        }
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "a6e6833209a196a38d798dadb02c81f8";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAACr1UwWrcMBC9C/YfBvaQpCQOtKWHhR4CoW0OgZTkHrTW2B6QJUeSN3G/vk9yvNulh/bQ" +
                "ZjEryZp5M2/ejNf0ID1fNkH3TOIaX6lvrA0H6sqi1JpunCTRlgYfuaIrG/05GW7EcaTUMQ2y8wm34hI1" +
                "PlDwSSfxLlaqZd9zCtNjH9t4eQeAglJQDbskzSSupZgClopux5hoy9Rav9XWTjQ6eRpzYnDIoZIfpMZO" +
                "J/xJpJ5j1C0MIkXA4WXwY9tVakYkB1pqpT7/499K3d5/3SBtMxObS7ZCkvdJO6ODQWZJG510qUgnbcfh" +
                "wvKOLbx0P7ChcpumgVGnNT1kOnhadhxm7hFGyVPt+x51qHUCf4h15A9PcaRp0CFJPVodYO+DEZfNi6wZ" +
                "HU9kVNLVTDfXG9i4yPWYBAlNQKgD65gLdnNNaoSQH95nB7V+ePYXOHKLjtgHnwVAsvwyBCiAZHTcIMa7" +
                "mVwFbFSHEcVEOi3vHnGMZ4QgSIEHX3d0iszvptR5V7Td6SB6a4uYNSoA1JPsdHL2C3JOewNZnV/gZ8RD" +
                "jL+BdXvczOmig2a29OHYooAwHILfiYHpdiogtZXcXla2QYdJZa85pFp/KaNTurEoglXH6GuBAIaeJXVL" +
                "MxY1HsX8v4b8fdxyT15R4KwTGJS5JN+UKcyd0wQGk0HXfJ4bLb82r/dSbFEa8kEW34rUXRn0xUB9H0E0" +
                "uIJ7sHs7jkhmtcwPOiJpca+fpYUC6GBAStZHjFVjvU6fPtLLfjftdz/eisGhfnsae7nQSkdVPc4/n54O" +
                "1ceHpq/UH0gtu2fQ+wnwUf+d/AUAAA==";
                
    }
}
