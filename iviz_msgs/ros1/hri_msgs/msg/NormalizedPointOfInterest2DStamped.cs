/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.HriMsgs
{
    [DataContract]
    public sealed class NormalizedPointOfInterest2DStamped : IHasSerializer<NormalizedPointOfInterest2DStamped>, IMessage
    {
        // This contains the position of a point of interest (typically in an image)
        // the coordinates are always normalized and must belong to [0.,1.].
        // c is a confidence level (between 0. and 1.) associated to that POI
        /// <summary> Header timestamp should be acquisition time of the original image </summary>
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "x")] public float X;
        [DataMember (Name = "y")] public float Y;
        [DataMember (Name = "c")] public float C;
    
        public NormalizedPointOfInterest2DStamped()
        {
        }
        
        public NormalizedPointOfInterest2DStamped(in StdMsgs.Header Header, float X, float Y, float C)
        {
            this.Header = Header;
            this.X = X;
            this.Y = Y;
            this.C = C;
        }
        
        public NormalizedPointOfInterest2DStamped(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            b.Deserialize(out X);
            b.Deserialize(out Y);
            b.Deserialize(out C);
        }
        
        public NormalizedPointOfInterest2DStamped(ref ReadBuffer2 b)
        {
            Header = new StdMsgs.Header(ref b);
            b.Align4();
            b.Deserialize(out X);
            b.Deserialize(out Y);
            b.Deserialize(out C);
        }
        
        public NormalizedPointOfInterest2DStamped RosDeserialize(ref ReadBuffer b) => new NormalizedPointOfInterest2DStamped(ref b);
        
        public NormalizedPointOfInterest2DStamped RosDeserialize(ref ReadBuffer2 b) => new NormalizedPointOfInterest2DStamped(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(X);
            b.Serialize(Y);
            b.Serialize(C);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(X);
            b.Serialize(Y);
            b.Serialize(C);
        }
        
        public void RosValidate()
        {
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 12;
                size += Header.RosMessageLength;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Header.AddRos2MessageLength(size);
            size = WriteBuffer2.Align4(size);
            size += 4; // X
            size += 4; // Y
            size += 4; // C
            return size;
        }
    
        public const string MessageType = "hri_msgs/NormalizedPointOfInterest2DStamped";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "f00d620d5791659f1cba63fdcb50f444";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE61TPW/bMBDd+SsO0BC7qNW43Qx0C9p6KBo02YLAOFNniQBFKiRlR/31faQSO9k6VBAg" +
                "UtK9u/fBiu47E0l7l9i4SKkTGnw0yXhH/kCMnXEpL/GQIDHRIk2D0WzthHfEjkzPrSxVVaq196ExjpNE" +
                "4iDE9sRTJOdDz9b8kQYVDfUjgPZivWspeXq4rj+u68caGJowD+eJDqYRp4WsHMXSYi/pJOLoui4I63pJ" +
                "HKPXBq2aDJI6TnT7a6t+CDcSqJsfFb3sk+kxPvcDxc6PtkF/Yv00mhe6+Xsmmln4YFqQsDM3dbCe05fP" +
                "9HxeTeeVVurrf77Uz7vvG4qp2fWxjZ/m+aHNXQJzDpBPEjecmA4ePE3bSVjNMhWC0KN8hVESs6jFZNyt" +
                "OAnFuTHOomnf96ODnUkuAr3WozI7TAOHZPRoObyxlw6Be8nouKM8jcWs7c0mexdFj8lgoJwRHYSjgdPb" +
                "G1IjcgTVUABnHn77uH5U1f3Jr3K+2nc2FUMxtTwPCF4emOMGzT7MLGs0gUqCdk2kRXm3wzYuCd0wiwxe" +
                "d7QAhdspddlhOHvkYHhvJQPnEAP1KhddLd8guwLt2PlX+Bnx0uNfYN0ZN3NadTDPZhni2EJJ/DgEf0TI" +
                "kcRpPjvWCA6bNfvAYVIlkKWlqr5lsfETqoo1+ZBc0n8yqVMxhYxebNmZRqm/wcvI490DAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<NormalizedPointOfInterest2DStamped> CreateSerializer() => new Serializer();
        public Deserializer<NormalizedPointOfInterest2DStamped> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<NormalizedPointOfInterest2DStamped>
        {
            public override void RosSerialize(NormalizedPointOfInterest2DStamped msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(NormalizedPointOfInterest2DStamped msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(NormalizedPointOfInterest2DStamped msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(NormalizedPointOfInterest2DStamped msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(NormalizedPointOfInterest2DStamped msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<NormalizedPointOfInterest2DStamped>
        {
            public override void RosDeserialize(ref ReadBuffer b, out NormalizedPointOfInterest2DStamped msg) => msg = new NormalizedPointOfInterest2DStamped(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out NormalizedPointOfInterest2DStamped msg) => msg = new NormalizedPointOfInterest2DStamped(ref b);
        }
    }
}
