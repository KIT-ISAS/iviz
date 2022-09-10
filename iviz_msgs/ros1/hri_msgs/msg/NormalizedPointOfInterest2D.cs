/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.HriMsgs
{
    [DataContract]
    public sealed class NormalizedPointOfInterest2D : IHasSerializer<NormalizedPointOfInterest2D>, IMessage
    {
        // This contains the position of a point of interest (typically in an image)
        // the coordinates are always normalized and must belong to [0.,1.].
        // c is a confidence level (between 0. and 1.) associated to that POI
        [DataMember (Name = "x")] public float X;
        [DataMember (Name = "y")] public float Y;
        [DataMember (Name = "c")] public float C;
    
        public NormalizedPointOfInterest2D()
        {
        }
        
        public NormalizedPointOfInterest2D(float X, float Y, float C)
        {
            this.X = X;
            this.Y = Y;
            this.C = C;
        }
        
        public NormalizedPointOfInterest2D(ref ReadBuffer b)
        {
            b.Deserialize(out X);
            b.Deserialize(out Y);
            b.Deserialize(out C);
        }
        
        public NormalizedPointOfInterest2D(ref ReadBuffer2 b)
        {
            b.Align4();
            b.Deserialize(out X);
            b.Deserialize(out Y);
            b.Deserialize(out C);
        }
        
        public NormalizedPointOfInterest2D RosDeserialize(ref ReadBuffer b) => new NormalizedPointOfInterest2D(ref b);
        
        public NormalizedPointOfInterest2D RosDeserialize(ref ReadBuffer2 b) => new NormalizedPointOfInterest2D(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(X);
            b.Serialize(Y);
            b.Serialize(C);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(X);
            b.Serialize(Y);
            b.Serialize(C);
        }
        
        public void RosValidate()
        {
        }
    
        public const int RosFixedMessageLength = 12;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        public const int Ros2FixedMessageLength = 12;
        
        public int Ros2MessageLength => Ros2FixedMessageLength;
        
        public int AddRos2MessageLength(int c) => WriteBuffer2.Align4(c) + Ros2FixedMessageLength;
        
    
        public const string MessageType = "hri_msgs/NormalizedPointOfInterest2D";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "eb224da30b2d872f41cf40e039cdb0d6";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAEz2OOw7CQAxE+5xiJBqQ0CrAKaigoEMUZtchljY2yppPOD0bCio/W5rnWeDUS0E0dRIt" +
                "8J5xtyIuprAOVDdRn7EOHrk4lj7dJVLOU72BFDLQjVfN4peOZmMSJecCGhmUXzQVqI0DZflwqomE4VFF" +
                "V86mN7jh3Ib1JlxCdUTUPjQ36iSxRkbmJ2csr+wvZkUbfoZNWIFKsSj1VZol3pPjeNg3XTby3RbvP01/" +
                "ik3zBVDCj9P0AAAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<NormalizedPointOfInterest2D> CreateSerializer() => new Serializer();
        public Deserializer<NormalizedPointOfInterest2D> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<NormalizedPointOfInterest2D>
        {
            public override void RosSerialize(NormalizedPointOfInterest2D msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(NormalizedPointOfInterest2D msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(NormalizedPointOfInterest2D _) => RosFixedMessageLength;
            public override int Ros2MessageLength(NormalizedPointOfInterest2D _) => Ros2FixedMessageLength;
        }
    
        sealed class Deserializer : Deserializer<NormalizedPointOfInterest2D>
        {
            public override void RosDeserialize(ref ReadBuffer b, out NormalizedPointOfInterest2D msg) => msg = new NormalizedPointOfInterest2D(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out NormalizedPointOfInterest2D msg) => msg = new NormalizedPointOfInterest2D(ref b);
        }
    }
}
