/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class XRGazeState : IDeserializable<XRGazeState>, IHasSerializer<XRGazeState>, IMessage
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "transform")] public GeometryMsgs.Transform Transform;
        [DataMember (Name = "is_valid")] public bool IsValid;
    
        public XRGazeState()
        {
        }
        
        public XRGazeState(in StdMsgs.Header Header, in GeometryMsgs.Transform Transform, bool IsValid)
        {
            this.Header = Header;
            this.Transform = Transform;
            this.IsValid = IsValid;
        }
        
        public XRGazeState(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Transform);
            b.Deserialize(out IsValid);
        }
        
        public XRGazeState(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Align8();
            b.Deserialize(out Transform);
            b.Deserialize(out IsValid);
        }
        
        public XRGazeState RosDeserialize(ref ReadBuffer b) => new XRGazeState(ref b);
        
        public XRGazeState RosDeserialize(ref ReadBuffer2 b) => new XRGazeState(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(in Transform);
            b.Serialize(IsValid);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(in Transform);
            b.Serialize(IsValid);
        }
        
        public void RosValidate()
        {
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 57;
                size += Header.RosMessageLength;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Header.AddRos2MessageLength(size);
            size = WriteBuffer2.Align8(size);
            size += 56; // Transform
            size += 1; // IsValid
            return size;
        }
    
        public const string MessageType = "iviz_msgs/XRGazeState";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "95f43b2003d0b3928f0570ef6543b6ff";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71UwW4TMRC9+ytGyqEtSoMoiEMlbgjaA1KhFReEosl6smvhtbe2N+ny9Tx7k00LRXCA" +
                "RpHW6533/ObNjC+EtQRqykPV4ltJYVi2sY7PbwK7uPahpbRfqZX3lkxcbtgarZR6849/6sP1+3OKSY8S" +
                "LkZdM7pO7DQHTdDHmhMT5FBj6kbCqZWNWIC47URT+ZqGTuICwJvGRAimWpwEtnagPiIoeap82/bOVJyE" +
                "kmnlAR5I44ip45BM1VsOiPdBG5fD14Fbyez4R7ntxVVCl2/PEeOiVH0yEDSAoQrC0bgaH0n1xqWXZxlA" +
                "M/ryyccXX9XsZutPsS81qjCpoNRwyqrlrgsSs2CO5zjs2ZjlAofAJcFxOtJx2VviNZ4QToMW6XzV0DFS" +
                "uBpS4x0IhTYcDK+sZOIKVoD1KIOOTu4xu0Lt2Pk9/ch4OONvaN3Em3M6bVA8m22IfQ0nEdgFvzEaoauh" +
                "kFTWiEtkzSpwGFRGjUeq2btsNoKAKqXBk2P0lUElNG1NalRMIbOXsixzX/6ftvzNeOy7LEguFtKIJaVp" +
                "aGglaSsCt7b+ly6Kuc/WQZBuxxWaSn2WKvnwcsRbTsY79bEHIDgsKfg07j1Jkjsxj6TItCnfftKfR+Ky" +
                "9K53GIFWGGXFtE1IALUJgCKHBVglCEySOZlE2sMP5xM4Wv4GSkEjZTR3Hcj4vid5G5BjWdSLOW0b+Fui" +
                "ciOU+S0TbyoKpjb6UI0JzLRLbk5pfYZGsnbUPB6GEoJk7/bJgi7XNPietjkhLMLuovEo76SrzEHyfp5v" +
                "mR3FQ0OvPKYdtsTINUbGxYQrDlVfW8/p9Su6m1bDtPr+JKU+9Nhj1XbkQx7R0b4HNc9vt4cGzSb/MaH9" +
                "aqvUD46fegyDBgAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<XRGazeState> CreateSerializer() => new Serializer();
        public Deserializer<XRGazeState> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<XRGazeState>
        {
            public override void RosSerialize(XRGazeState msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(XRGazeState msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(XRGazeState msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(XRGazeState msg) => msg.Ros2MessageLength;
            public override void RosValidate(XRGazeState msg) => msg.RosValidate();
        }
        sealed class Deserializer : Deserializer<XRGazeState>
        {
            public override void RosDeserialize(ref ReadBuffer b, out XRGazeState msg) => msg = new XRGazeState(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out XRGazeState msg) => msg = new XRGazeState(ref b);
        }
    }
}
