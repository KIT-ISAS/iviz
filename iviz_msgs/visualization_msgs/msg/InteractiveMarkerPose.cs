/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisualizationMsgs
{
    [Preserve, DataContract (Name = "visualization_msgs/InteractiveMarkerPose")]
    public sealed class InteractiveMarkerPose : IDeserializable<InteractiveMarkerPose>, IMessage
    {
        // Time/frame info.
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        // Initial pose. Also, defines the pivot point for rotations.
        [DataMember (Name = "pose")] public GeometryMsgs.Pose Pose { get; set; }
        // Identifying string. Must be globally unique in
        // the topic that this message is sent through.
        [DataMember (Name = "name")] public string Name { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public InteractiveMarkerPose()
        {
            Name = string.Empty;
        }
        
        /// <summary> Explicit constructor. </summary>
        public InteractiveMarkerPose(in StdMsgs.Header Header, in GeometryMsgs.Pose Pose, string Name)
        {
            this.Header = Header;
            this.Pose = Pose;
            this.Name = Name;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public InteractiveMarkerPose(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Pose = new GeometryMsgs.Pose(ref b);
            Name = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new InteractiveMarkerPose(ref b);
        }
        
        InteractiveMarkerPose IDeserializable<InteractiveMarkerPose>.RosDeserialize(ref Buffer b)
        {
            return new InteractiveMarkerPose(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Pose.RosSerialize(ref b);
            b.Serialize(Name);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Name is null) throw new System.NullReferenceException(nameof(Name));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 60;
                size += Header.RosMessageLength;
                size += BuiltIns.UTF8.GetByteCount(Name);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "visualization_msgs/InteractiveMarkerPose";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "a6e6833209a196a38d798dadb02c81f8";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
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
                
        public override string ToString() => Extensions.ToString(this);
    }
}
