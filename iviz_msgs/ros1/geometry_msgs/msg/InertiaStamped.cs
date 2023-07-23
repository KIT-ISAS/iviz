/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [DataContract]
    public sealed class InertiaStamped : IHasSerializer<InertiaStamped>, IMessage
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "inertia")] public Inertia Inertia;
    
        public InertiaStamped()
        {
            Inertia = new Inertia();
        }
        
        public InertiaStamped(in StdMsgs.Header Header, Inertia Inertia)
        {
            this.Header = Header;
            this.Inertia = Inertia;
        }
        
        public InertiaStamped(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Inertia = new Inertia(ref b);
        }
        
        public InertiaStamped(ref ReadBuffer2 b)
        {
            Header = new StdMsgs.Header(ref b);
            Inertia = new Inertia(ref b);
        }
        
        public InertiaStamped RosDeserialize(ref ReadBuffer b) => new InertiaStamped(ref b);
        
        public InertiaStamped RosDeserialize(ref ReadBuffer2 b) => new InertiaStamped(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            Inertia.RosSerialize(ref b);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            Inertia.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            Header.RosValidate();
            Inertia.RosValidate();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 80;
                size += Header.RosMessageLength;
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Header.AddRos2MessageLength(size);
            size = WriteBuffer2.Align8(size);
            size += 80; // Inertia
            return size;
        }
    
        public const string MessageType = "geometry_msgs/InertiaStamped";
    
        [IgnoreDataMember] public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "ddee48caeab5a966c5e8d166654a9ac7";
    
        [IgnoreDataMember] public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        [IgnoreDataMember]
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71UPW/bMBDd9SsO8JCksFU0KToEyNSijYcAQRN0CdzgLJ4lIhKpkpRtCfnxfaT8la1D" +
                "W0GCqNO9d993K6zEUZVe2dyIC5pJj+8su/nLV3b38O2afFDPjS/9+9vR7IQeAhvFTlEjgRUHppWFV7qs" +
                "xM1qWUsNEDetKEp/Q9+KzwF8rLQn3KXAZa7rnjoPpWCpsE3TGV1wEAq6kTd4ILUhppYRZ9HV7KBvndIm" +
                "qq8cNxLZcXv51YkphOZfrqFjvBRd0HCoB0PhhL02JX5S1mkTri4jgCb09N36D4ts8rixM8ilRJIPXlCo" +
                "OESvZds68dFh9tcw9m6MMocRZElgTnk6T7JnfPoLgjX4Iq0tKjpHCPd9qKwBodCaneZlLZG4QCrAehZB" +
                "ZxcnzCZRGzZ2Tz8yHm38Ca058MaYZhWKV8c0+K5EJqHYOrvWCqrLPpEUtRYTqNZLx67PImo0mU2+xmRD" +
                "CahUGrzZe1toVELRRocq88FF9lSWZ63+VVuWYtF+rh97czcLiPEO/tDTS7nIVrXl8OkjNRnEnxERqmpX" +
                "1CSFZpG9ZfghRbDuKnZi1N8P16MYj+YG4az5eYkeoXi9kt5u8aCttgO9Rn26SVJI+viM0r3ukCR6iNK9" +
                "W2A4Ofcn5+F47k/k/Yl8GP5PXndZ2c+ukzgCSCXKTuv0L47mygk6pOVC8jiF8zQu1mDqGmF0Egb8gARQ" +
                "aQeotiYHqzjB9pAp6UDKiidjAzgafgElki8RzW0LMmwSx8bXHLFRDMi55GU+pU0lZtSKvZdWRloyuiCn" +
                "S61GJAw1BzDTLrgphdUlereuR59HYxgEkDgbEuAip/mKetvRJgaEg9vtNktLOfiVRi9YO42LbUfxNqH3" +
                "FgsGafGeS0yp8QFbNc8OlT32xLHyQ/Yb0/+X//kFAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<InertiaStamped> CreateSerializer() => new Serializer();
        public Deserializer<InertiaStamped> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<InertiaStamped>
        {
            public override void RosSerialize(InertiaStamped msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(InertiaStamped msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(InertiaStamped msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(InertiaStamped msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(InertiaStamped msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<InertiaStamped>
        {
            public override void RosDeserialize(ref ReadBuffer b, out InertiaStamped msg) => msg = new InertiaStamped(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out InertiaStamped msg) => msg = new InertiaStamped(ref b);
        }
    }
}
