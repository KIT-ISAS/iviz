/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.VisionMsgs
{
    [DataContract]
    public sealed class ObjectHypothesis : IHasSerializer<ObjectHypothesis>, IMessage
    {
        // An object hypothesis that contains no position information.
        // The unique numeric ID of object detected. To get additional information about
        //   this ID, such as its human-readable name, listeners should perform a lookup
        //   in a metadata database. See vision_msgs/VisionInfo.msg for more detail.
        [DataMember (Name = "id")] public long Id;
        // The probability or confidence value of the detected object. By convention,
        //   this value should lie in the range [0-1].
        [DataMember (Name = "score")] public double Score;
    
        public ObjectHypothesis()
        {
        }
        
        public ObjectHypothesis(long Id, double Score)
        {
            this.Id = Id;
            this.Score = Score;
        }
        
        public ObjectHypothesis(ref ReadBuffer b)
        {
            b.Deserialize(out Id);
            b.Deserialize(out Score);
        }
        
        public ObjectHypothesis(ref ReadBuffer2 b)
        {
            b.Align8();
            b.Deserialize(out Id);
            b.Deserialize(out Score);
        }
        
        public ObjectHypothesis RosDeserialize(ref ReadBuffer b) => new ObjectHypothesis(ref b);
        
        public ObjectHypothesis RosDeserialize(ref ReadBuffer2 b) => new ObjectHypothesis(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Id);
            b.Serialize(Score);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align8();
            b.Serialize(Id);
            b.Serialize(Score);
        }
        
        public void RosValidate()
        {
        }
    
        public const int RosFixedMessageLength = 16;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        public const int Ros2FixedMessageLength = 16;
        
        public int Ros2MessageLength => Ros2FixedMessageLength;
        
        public int AddRos2MessageLength(int c) => WriteBuffer2.Align8(c) + Ros2FixedMessageLength;
        
    
        public const string MessageType = "vision_msgs/ObjectHypothesis";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "abf73443e563396425a38201e9dacc73";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE02QzU4DMQyE7/sUI/XaLiAh7qBeeqbighDybrxdQxIv+anUt8cpLXBJbMn+PDMrPEbo" +
                "8MFjwXxatMycJaPMVDBqLCQxIyoWzVJEIyROmgK1uu+6FfYzo0b5qoxYAycZsdtCpyvUcbGPXY+94sAF" +
                "5NyZRP4/CzRoLcaD3TYBu+0auY4zKENKxlwDxU1icjR4O0WB1/CSC0dOGXnW6h0WTo0Iglf9rMuZJwZH" +
                "4GKrhdCegTL3eGbGUbIdfw/5kG9ezvXONPXWw0AImrg5IPF9J7E83EPc1fWSdKBBvJQTbNbSmsRxHI1K" +
                "3uKwDCzN3wAugfR4OrXZI8fme/1n+Wfr4sQLN+ENkCgeGK+3m7u3vpu8UpORR5PWfQPNueDevgEAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<ObjectHypothesis> CreateSerializer() => new Serializer();
        public Deserializer<ObjectHypothesis> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<ObjectHypothesis>
        {
            public override void RosSerialize(ObjectHypothesis msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(ObjectHypothesis msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(ObjectHypothesis _) => RosFixedMessageLength;
            public override int Ros2MessageLength(ObjectHypothesis _) => Ros2FixedMessageLength;
        }
    
        sealed class Deserializer : Deserializer<ObjectHypothesis>
        {
            public override void RosDeserialize(ref ReadBuffer b, out ObjectHypothesis msg) => msg = new ObjectHypothesis(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out ObjectHypothesis msg) => msg = new ObjectHypothesis(ref b);
        }
    }
}
