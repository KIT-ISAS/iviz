/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [DataContract]
    public sealed class PointStamped : IDeserializable<PointStamped>, IHasSerializer<PointStamped>, IMessage
    {
        // This represents a Point with reference coordinate frame and timestamp
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "point")] public Point Point;
    
        public PointStamped()
        {
        }
        
        public PointStamped(in StdMsgs.Header Header, in Point Point)
        {
            this.Header = Header;
            this.Point = Point;
        }
        
        public PointStamped(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Point);
        }
        
        public PointStamped(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Align8();
            b.Deserialize(out Point);
        }
        
        public PointStamped RosDeserialize(ref ReadBuffer b) => new PointStamped(ref b);
        
        public PointStamped RosDeserialize(ref ReadBuffer2 b) => new PointStamped(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(in Point);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(in Point);
        }
        
        public void RosValidate()
        {
        }
    
        public int RosMessageLength => 24 + Header.RosMessageLength;
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = Header.AddRos2MessageLength(c);
            c = WriteBuffer2.Align8(c);
            c += 24; // Point
            return c;
        }
    
        public const string MessageType = "geometry_msgs/PointStamped";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "c63aecb41bfdfd6b7e1fac37c7cbe7bf";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE7VTTYvcMAy9+1cI5rC7hdnSD3oY6K3041BYunsrZdDYSiJw7NRSZjf99ZUdJi300kMb" +
                "QuzY1pPe0/MOHgYWKDQVEkoqgHCXOSk8sg623lGh5Al8ziVwQiXoCo4EmAIojySK4+Q+EgYqMLTBrQhT" +
                "/Tr39h8/7vP9hwOIhuMovTxfM7sd3KuVhCXASIoBFaHLVhH3A5V9pDNFaLVSgLary0Rya4FNAXt7SlQw" +
                "xgVmsUOajfQ4zol9Zb1xvcRbJCeTa8Ki7OeI5Q+RKrq9Qt/nJuKndwc7k4T8rGwFLYbgC6Fw6m0T3GyK" +
                "vXpZA2AHX79kefHN7R4e897WqTeBtypAB9RaNT3V1tWCUQ6W7NnK8taSmEpk6YLAdVs72q/cgGWzWmjK" +
                "foBro3C36JCTARKcsTCeIlVgb1IY6lUNurr5DTk16IQpX+BXxF85/gY2bbiV036w5sUqg8y9KWkHp5LP" +
                "HOzoaWkgPrIZFCKfCpbF1ag1pdu9b47U2sfWGhtRJHu2ToTmZCdaKnpry5HD/7JlT9nsV5bVm+0eXBxm" +
                "UilykkZmysLKJk/uqoXafTHNukJGakJProsZ9c1reNpmyzb74dxP2ruPWrkDAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<PointStamped> CreateSerializer() => new Serializer();
        public Deserializer<PointStamped> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<PointStamped>
        {
            public override void RosSerialize(PointStamped msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(PointStamped msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(PointStamped msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(PointStamped msg) => msg.Ros2MessageLength;
        }
        sealed class Deserializer : Deserializer<PointStamped>
        {
            public override void RosDeserialize(ref ReadBuffer b, out PointStamped msg) => msg = new PointStamped(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out PointStamped msg) => msg = new PointStamped(ref b);
        }
    }
}
