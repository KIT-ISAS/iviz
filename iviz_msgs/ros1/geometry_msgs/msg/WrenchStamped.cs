/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [DataContract]
    public sealed class WrenchStamped : IDeserializableRos1<WrenchStamped>, IDeserializableRos2<WrenchStamped>, IMessageRos1, IMessageRos2
    {
        // A wrench with reference coordinate frame and timestamp
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "wrench")] public Wrench Wrench;
    
        /// Constructor for empty message.
        public WrenchStamped()
        {
            Wrench = new Wrench();
        }
        
        /// Explicit constructor.
        public WrenchStamped(in StdMsgs.Header Header, Wrench Wrench)
        {
            this.Header = Header;
            this.Wrench = Wrench;
        }
        
        /// Constructor with buffer.
        public WrenchStamped(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Wrench = new Wrench(ref b);
        }
        
        /// Constructor with buffer.
        public WrenchStamped(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Wrench = new Wrench(ref b);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new WrenchStamped(ref b);
        
        public WrenchStamped RosDeserialize(ref ReadBuffer b) => new WrenchStamped(ref b);
        
        public WrenchStamped RosDeserialize(ref ReadBuffer2 b) => new WrenchStamped(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            Wrench.RosSerialize(ref b);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            Wrench.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Wrench is null) BuiltIns.ThrowNullReference();
            Wrench.RosValidate();
        }
    
        public int RosMessageLength => 48 + Header.RosMessageLength;
        
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            Header.AddRos2MessageLength(ref c);
            Wrench.AddRos2MessageLength(ref c);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "geometry_msgs/WrenchStamped";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "d78d3cb249ce23087ade7e7d0c40cfa7";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71UwW7UMBC9+ytG2kNbtA1SizhU4oCEgB6QKrWCYzUbTxKLxA72ZLfh63l2tikVHDgA" +
                "q2gTJ35v5s288Ybe0iGKrzs6OO0oSiN5KVSHEK3zrEJN5EGIvSV1gyTlYTQfha1E6srNfDlSlJsxb/7y" +
                "z3y6/XBFSe39kNr0coltNnSrSIqjpUGULStTE5CTazuJ573spaeSrVgqX3UeJVUA3nUuEa5WvETu+5mm" +
                "hE0aIHsYJu/qrHtV+4gH0nliGjmqq6ee4y9lyuy4knybShmv311hj09ST+qQ0AyGOgon51t8JDM5r5cX" +
                "GWA2d4dwjqW0qOwanLRjzcnKwxgl5Tw5XSHGi0VcBW4URxDFJjot7+6xTGeEIEhBxoDenCLzm1m74EEo" +
                "tOfoeNdLJq5RAbCeZNDJ2U/MvlB79uGRfmF8ivEntH7lzZrOO/Ssz+rT1KKA2DjGsHcWW3dzIal7J16p" +
                "d7vIcTYZtYQ0m/fFiprbVzqCO6cUaocG2GJhkzRm9tKNe2f/lRtbCXBdnBdLLgPw6KwouVPQkLIhUTAU" +
                "qYkCFSPXskWz4KCSMbodsquwEzUR1COPGfu2mCv7DH79LLWGeEkL2dMS/3DZ/xF4DPobhUz78u25yCpP" +
                "wXXxbfBw/SCMlmLAViSA1kVAXfAVWHHuQB+q45RskEQ+KDgG/gpKgYkymscRZJjkyD71nLH5NSCnUrXV" +
                "lg6d+GVXNkEZ2TLkrqboWmcXJAINK5jpKG5L2lzARH2/5LwEgyNBEoMWwFlF1w3NYaJDFoSHeDxbAu1k" +
                "zavMgIawzQfLkeJ5QW8Ceo+ypMRtNkhSnGqVMU0fWF+/oof1aV6fvpsfHy6CgrAFAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
