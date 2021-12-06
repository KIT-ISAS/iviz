/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class XRGazeState : IDeserializable<XRGazeState>, IMessage
    {
        [DataMember (Name = "is_valid")] public bool IsValid;
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "transform")] public GeometryMsgs.Transform Transform;
    
        /// Constructor for empty message.
        public XRGazeState()
        {
        }
        
        /// Explicit constructor.
        public XRGazeState(bool IsValid, in StdMsgs.Header Header, in GeometryMsgs.Transform Transform)
        {
            this.IsValid = IsValid;
            this.Header = Header;
            this.Transform = Transform;
        }
        
        /// Constructor with buffer.
        internal XRGazeState(ref ReadBuffer b)
        {
            IsValid = b.Deserialize<bool>();
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Transform);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new XRGazeState(ref b);
        
        public XRGazeState RosDeserialize(ref ReadBuffer b) => new XRGazeState(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(IsValid);
            Header.RosSerialize(ref b);
            b.Serialize(in Transform);
        }
        
        public void RosValidate()
        {
        }
    
        public int RosMessageLength => 57 + Header.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "iviz_msgs/XRGazeState";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "b062fdd884f10dff560e6f7d4400606b";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71UwW7UMBC9+ytG6qEt2i5SizhU4oaAHpCKWnFdzcaziYVjp7azafh6np3dbBeK4ABd" +
                "rRQnmfdm5r2ZrL23ZOJqy9Zo9UlYS6CmXFQtvpUUxlUb6/j6PrCLGx9aSvuTUurdP/6pz3cfrykmPSWd" +
                "ClIndJfYaQ6aUBFrTkwogBpTNxIurGzFAsRtJ5rK2zR2EpcA3jcmokGqxUlga0fqI4KSp8q3be9MxUko" +
                "mVaO8EAaR0wdh2Sq3nJAvA/auBy+CdxKZsc/ykMvrhK6eX+NGBel6pNBQSMYqiAcjavxklRvXLq6zAB1" +
                "cj/4C9xKDbnn5JQaTrlYeeyCxFwnx2vkeDU1twQ3xBFk0ZHOyrMVbuM5IQlKkM5XDZ2h8tsxNd6BUGjL" +
                "wfDaSiauoABYTzPo9PwJsyvUjp3f00+Mhxx/Q+tm3tzTRQPPbO4+9jUERGAX/NZohK7HQlJZIy6RNevA" +
                "YVQZNaVUJx+yxggCqjiCK8foKwMDNA0mNSqmkNmLGyuM73+axt/swX64gmSz0EYsLc3bQWtJgwjUGvwv" +
                "wxPzeG2CoN2OK8yS+ipV8uFqwltOxjv1pQcgOBwp+DQ9e5Emd8U80yLTtrz7qf68CTdldr3D5LfCsBVL" +
                "NiMB1CYAih6WYJUgEEkWZBJpDz2cT+Bo+RsoBYOU0dx1IOOnmuTHgJzJsl4uaGigb4nKg1DWtiy6qSiY" +
                "2uiDGzOYadfcgtLmEoNk7VTzlAwWgmSv9vmSbjY0+p6G3BAOYfd98bB3rqvsQfJ+kT8uO4pjQW89th2y" +
                "xMg1VsbFhC8bXN9Yz+ntG3qcT+N8+v4iVh9m7Dm3HfmQV3SS78jzfPdwGNAs8h8b2p8GpX4A38dFlnoG" +
                "AAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
