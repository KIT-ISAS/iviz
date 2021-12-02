/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.OctomapMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class OctomapWithPose : IDeserializable<OctomapWithPose>, IMessage
    {
        // A 3D map in binary format, as Octree
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // The pose of the octree with respect to the header frame 
        [DataMember (Name = "origin")] public GeometryMsgs.Pose Origin;
        // The actual octree msg
        [DataMember (Name = "octomap")] public OctomapMsgs.Octomap Octomap;
    
        /// Constructor for empty message.
        public OctomapWithPose()
        {
            Octomap = new OctomapMsgs.Octomap();
        }
        
        /// Explicit constructor.
        public OctomapWithPose(in StdMsgs.Header Header, in GeometryMsgs.Pose Origin, OctomapMsgs.Octomap Octomap)
        {
            this.Header = Header;
            this.Origin = Origin;
            this.Octomap = Octomap;
        }
        
        /// Constructor with buffer.
        internal OctomapWithPose(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Origin);
            Octomap = new OctomapMsgs.Octomap(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new OctomapWithPose(ref b);
        
        OctomapWithPose IDeserializable<OctomapWithPose>.RosDeserialize(ref Buffer b) => new OctomapWithPose(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(ref Origin);
            Octomap.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Octomap is null) throw new System.NullReferenceException(nameof(Octomap));
            Octomap.RosValidate();
        }
    
        public int RosMessageLength => 56 + Header.RosMessageLength + Octomap.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "octomap_msgs/OctomapWithPose";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "20b380aca6a508a657e95526cddaf618";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1VXWsVMRB9z68YuA+20t6CikjBB1GqPoj1402kzM3O3Q1kk22SbV1/vWey3a1FQRH1" +
                "cmGTzczJmTMfu6Fn9PAF9TyQC7RzgdNE+5h6LkfEmd7akkTMK+FGEnX1YcyGPnZCQ8xCcU8F61jt6NqV" +
                "jpLkQWyhEuvR7ET7xL2QaSX2UtJ00ec2n5xXiORaFxZUtmVkvwDCymAZQXD2eDtv9Fyfxjz9yz/z5sPL" +
                "U8qlme+bIwe3D4VDw6kh0OeGC6tM1Lm2k3Ts5Uo8nLgfpKF6WqZB8rYG5TLh30qQxN5PNGYYQR0b+34M" +
                "znIRKq6XO/7wREaYBk7F2dFzgn1MDVIE86qmouOf5XKUYIVevziFTchix+JAaAKCTcLZhRaHZEYXysMH" +
                "6mA2H6/jMbbSIjfr5UgYFyUrXwakUXlyPsUd9+fgtsCGOIJbmkwH9d0FtvmQcAkoyBBtRwdgfj6VLoZa" +
                "AVecHO+8KLCFAkC9p073Dr9DVtqnFDjEBX5GvL3jd2DDiqsxHXfImdfo89hCQBgOKV65Bqa7qYJY7yQU" +
                "8m6XUPpGveYrzeasVmzR9NWM4Mk5R+uQgKaWusklKXrNxoVr/lU1/tg0CPAZGk2TBPpcHDRBK9aWhEp7" +
                "7Z08sJUjrTJ93dycu2oLXbTvFt8tmfOIalgNzLsRUaZQcW/t/leAoLJ0DmqhsAu5Zmvlj1jQGpXynXDN" +
                "3kcujx/Rl3U1rauv/4f+rXRLDGuiUEF39LxLXneXt7rrGN6aX0S0rK7/VWw/m77mTz8bZ55bnXyNhIgp" +
                "xovrQQyYVqrESbR2HJw0h9CJ9qPXTwHecLDT8lE42O7KyTYW2jsvh2YXo78B0juee3QpuVrvtcPnAtIG" +
                "mN2XrtV+3dB7ydFjXkJwnS897p39cq8zJZfFLcRG8qo38nnjpiA3YWTBRPLu69qPs+uRTnzlcSUp4yRv" +
                "OxUBkxnjFY14nRzEmG2zQU0/+fS5jhxjvgHtR64koAcAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
