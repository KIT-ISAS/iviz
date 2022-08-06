/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [DataContract]
    public sealed class QuaternionStamped : IDeserializable<QuaternionStamped>, IMessage
    {
        // This represents an orientation with reference coordinate frame and timestamp.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "quaternion")] public Quaternion Quaternion;
    
        public QuaternionStamped()
        {
        }
        
        public QuaternionStamped(in StdMsgs.Header Header, in Quaternion Quaternion)
        {
            this.Header = Header;
            this.Quaternion = Quaternion;
        }
        
        public QuaternionStamped(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Quaternion);
        }
        
        public QuaternionStamped(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Quaternion);
        }
        
        public QuaternionStamped RosDeserialize(ref ReadBuffer b) => new QuaternionStamped(ref b);
        
        public QuaternionStamped RosDeserialize(ref ReadBuffer2 b) => new QuaternionStamped(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(in Quaternion);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(in Quaternion);
        }
        
        public void RosValidate()
        {
        }
    
        public int RosMessageLength => 32 + Header.RosMessageLength;
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            c = Header.AddRos2MessageLength(c);
            c = WriteBuffer2.Align8(c);
            c += 32; /* Quaternion */
            return c;
        }
    
        public const string MessageType = "geometry_msgs/QuaternionStamped";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "e57f1e547e0e1fd13504588ffc8334e2";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE7VTTYvcMAy9+1cI5rC7hZnSD3oY6K3041Bou3srZdDESmJw7KykzGz66ys7THahFHpo" +
                "g8GKLT09PckbuOuDANPIJJRUABNkDmaihpzgHLS365aYUkPQ5Mw+JFSClnEgc/egYSBRHMadcx8JPTH0" +
                "dXNfJ/PkVIDuV9O5t//4c59vP+xB1B8G6eT5wsFt4FaNHrKHgRQ9KkKbjVvoeuJtpBNFqLzJQ73VeSTZ" +
                "WWAVxVZHiRhjnGESc9JsAgzDlEJTFFjrvsRbZEiAMCJraKaI/JtgBd2W0P1UBf30bm8+SaiZNBih2RAa" +
                "JpSQOrsEN4Wkr16WANjA929ZXvxwm7tz3to5dSb1ygK0Ry2s6aF0sxBG2VuyZ0uVO0tiKpGl8wLX9exg" +
                "v3IDls240JibHq6thC+z9tYz7QlOyAGPkQpwY1IY6lUJurp5gpwqdMKUL/AL4mOOv4FNK26padtb82KR" +
                "QabOlDTHkfMpeHM9zhWkiWVQIYYjI8+uRC0p3eZ9nU4tfaytsR1FchOsE75OtRPlgl7bcgj+f41lR9nG" +
                "j+dlNh9fxGXM/vz2TLGWyUoa0bQMT99QGeTBnlsbM+qb1/CwWvNq/Vyts3O/AIiCGdbpAwAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
