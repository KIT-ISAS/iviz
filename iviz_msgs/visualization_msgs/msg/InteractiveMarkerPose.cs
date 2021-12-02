/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisualizationMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class InteractiveMarkerPose : IDeserializable<InteractiveMarkerPose>, IMessage
    {
        // Time/frame info.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // Initial pose. Also, defines the pivot point for rotations.
        [DataMember (Name = "pose")] public GeometryMsgs.Pose Pose;
        // Identifying string. Must be globally unique in
        // the topic that this message is sent through.
        [DataMember (Name = "name")] public string Name;
    
        /// Constructor for empty message.
        public InteractiveMarkerPose()
        {
            Name = string.Empty;
        }
        
        /// Explicit constructor.
        public InteractiveMarkerPose(in StdMsgs.Header Header, in GeometryMsgs.Pose Pose, string Name)
        {
            this.Header = Header;
            this.Pose = Pose;
            this.Name = Name;
        }
        
        /// Constructor with buffer.
        internal InteractiveMarkerPose(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Pose);
            Name = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new InteractiveMarkerPose(ref b);
        
        InteractiveMarkerPose IDeserializable<InteractiveMarkerPose>.RosDeserialize(ref Buffer b) => new InteractiveMarkerPose(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(ref Pose);
            b.Serialize(Name);
        }
        
        public void RosValidate()
        {
            if (Name is null) throw new System.NullReferenceException(nameof(Name));
        }
    
        public int RosMessageLength => 60 + Header.RosMessageLength + BuiltIns.GetStringSize(Name);
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "visualization_msgs/InteractiveMarkerPose";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "a6e6833209a196a38d798dadb02c81f8";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UwWrcMBC96ysG9pCkJA60pYeFHgKhbQ6BLck9aK2xPSBLjiTvxv36Psnxbpce2kOb" +
                "xaxla+bpvTczXtGj9HzdBN0ziWt8pb6xNhyoKzelVnTnJIm2NPjIFd3Y6C/JcCOOI6WOaZCdT9gVl6jx" +
                "gYJPOol3sVIt+55TmJ762MbrDQAKSkE17JI0k7iWYgq4VXQ/xkRbptb6rbZ2otHJ85iJISEflfwgNVY6" +
                "4U8i9RyjbhEQKQIOL4Mf265SMyI5yFLq8z/+qfuHr2uQNrOs2TAwfEjaGR0MaCVtdNLFjk7ajsOV5R1b" +
                "JOl+YENlN00Dw6QVPWYtuFp2HGbhEUHJU+37HibUOkE8KnWSj0xxpGnQIUk9Wh0Q74MRl8NLTTM6rsiw" +
                "0dVMd7drxLjI9ZgEhCYg1IF1zG7d3ZIaUcUP73OCWj3u/RUeuUU7HA6f3QdZfhkC7AcZHdc4490srgI2" +
                "zGGcYiKdl3dPeIwXhENAgQdfd3QO5pspdd6Vwu50EL21pZI1HADqWU46u/gFOdNeo6bOL/Az4vGMv4F1" +
                "B9ys6apDzWxpwrGFgQgcgt+JQeh2KiC1ldxbVrZBh0nlrPlItfpS5qa0YqkI7jpGXwsKYGgvqVs6sVTj" +
                "Scz/6sbfJw0CbyhwLhLol4kk35T5y23TBIaMQdd8mbssvzav+1Ji4Qv5IEtuRWpTRnwJUN9HqAyu4B7j" +
                "3kogqCyTg15IWtzr12jhDy0YjUL5RK5qrNfp00d6Oaymw+rH29A/WrdoOBQKHXTi5yn5/PR89B3fl75S" +
                "f1C0rPZK/QT4TUEp7wUAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
