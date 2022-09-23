/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [DataContract]
    public sealed class PolygonStamped : IHasSerializer<PolygonStamped>, IMessage
    {
        // This represents a Polygon with reference coordinate frame and timestamp
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "polygon")] public Polygon Polygon;
    
        public PolygonStamped()
        {
            Polygon = new Polygon();
        }
        
        public PolygonStamped(in StdMsgs.Header Header, Polygon Polygon)
        {
            this.Header = Header;
            this.Polygon = Polygon;
        }
        
        public PolygonStamped(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Polygon = new Polygon(ref b);
        }
        
        public PolygonStamped(ref ReadBuffer2 b)
        {
            Header = new StdMsgs.Header(ref b);
            Polygon = new Polygon(ref b);
        }
        
        public PolygonStamped RosDeserialize(ref ReadBuffer b) => new PolygonStamped(ref b);
        
        public PolygonStamped RosDeserialize(ref ReadBuffer2 b) => new PolygonStamped(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            Polygon.RosSerialize(ref b);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            Polygon.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Polygon is null) BuiltIns.ThrowNullReference();
            Polygon.RosValidate();
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 0;
                size += Header.RosMessageLength;
                size += Polygon.RosMessageLength;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Header.AddRos2MessageLength(size);
            size = Polygon.AddRos2MessageLength(size);
            return size;
        }
    
        public const string MessageType = "geometry_msgs/PolygonStamped";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "c6be8f7dc3bee7fe9e8d296070f53340";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71UwW7UMBC95ytG2kNbRIuAWyUOCAT0gFTR3ipUeZ1JMsKxg8fZNnw9z5PdpRIXDsAq" +
                "UrK25828mfe8odtBlDJPmZVjUXJ0ncLSp0gPUgbsdJw5eiafUm4lusLUZTcyudhSkZG1uHFqPrFrOdNg" +
                "r+aAMa3vpnnzl3/N55uPl6SlvR+11xdr9mZDNwVludzSyMW1rjjqEqqSfuB8HnjHgaxebsl2yzKxXiDQ" +
                "+oCn58jZhbDQrDhUEoiP4xzFV+ZHvod4REpE0yaXi/g5uPxboyo6HuXvszXy6v0lzkRlPxdBQQsQfGan" +
                "EntsUjNLLK9f1QDa0N2XpC+/Npvbh3SOde7R5GMVVAZXatX8WAdYC3Z6iWTPVpYXSIIuMdK1Sqe2do+/" +
                "ekbIhlp4Sn6gU1C4XsqAgZWBaeeyuG3gCuzRCqCe1KCTsyfI0aCji+kAvyL+yvEnsPGIWzmdDxheqG3Q" +
                "uUcncXDKaSctjm4XA/FBIFMKss0uL02NWlM2mw+mylLnaKPB26kmL5hEa2putOSKbmO5l/ZfybLnBPnl" +
                "ZdXm3gvN5i3pxF66KiVBU1JXhXNwGwTKRrGTrMXcFRw+piTmS+yCzjyuotxWQ8bIHtzgNlPM3df94f/F" +
                "y7IevINyipOoxmFKKk854mT1SZcZ45qc51O7XaDyrYAcTkG/XhQhZ9UvV6ZqLMF73K6U4UeynGuvdjAC" +
                "0qiYpKIWXAEVaF/WBdHqu/0FZ0i4GqwqrAAQ2hpTqcGFc5pg+60EKYuFHiLhNHW9ibZllT6uxRT3jWme" +
                "KGB7ZVSrivBahPd7RIe0J7afX6EEfzzHEGsnTMsOjKxBVvO7kOa25m66kFy9AR6PX8vx60fzE6wRhV60" +
                "BQAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<PolygonStamped> CreateSerializer() => new Serializer();
        public Deserializer<PolygonStamped> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<PolygonStamped>
        {
            public override void RosSerialize(PolygonStamped msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(PolygonStamped msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(PolygonStamped msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(PolygonStamped msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(PolygonStamped msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<PolygonStamped>
        {
            public override void RosDeserialize(ref ReadBuffer b, out PolygonStamped msg) => msg = new PolygonStamped(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out PolygonStamped msg) => msg = new PolygonStamped(ref b);
        }
    }
}
