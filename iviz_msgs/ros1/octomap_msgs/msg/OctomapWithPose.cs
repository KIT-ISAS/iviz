/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.OctomapMsgs
{
    [DataContract]
    public sealed class OctomapWithPose : IHasSerializer<OctomapWithPose>, IMessage
    {
        // A 3D map in binary format, as Octree
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // The pose of the octree with respect to the header frame 
        [DataMember (Name = "origin")] public GeometryMsgs.Pose Origin;
        // The actual octree msg
        [DataMember (Name = "octomap")] public OctomapMsgs.Octomap Octomap;
    
        public OctomapWithPose()
        {
            Octomap = new OctomapMsgs.Octomap();
        }
        
        public OctomapWithPose(in StdMsgs.Header Header, in GeometryMsgs.Pose Origin, OctomapMsgs.Octomap Octomap)
        {
            this.Header = Header;
            this.Origin = Origin;
            this.Octomap = Octomap;
        }
        
        public OctomapWithPose(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            b.Deserialize(out Origin);
            Octomap = new OctomapMsgs.Octomap(ref b);
        }
        
        public OctomapWithPose(ref ReadBuffer2 b)
        {
            Header = new StdMsgs.Header(ref b);
            b.Align8();
            b.Deserialize(out Origin);
            Octomap = new OctomapMsgs.Octomap(ref b);
        }
        
        public OctomapWithPose RosDeserialize(ref ReadBuffer b) => new OctomapWithPose(ref b);
        
        public OctomapWithPose RosDeserialize(ref ReadBuffer2 b) => new OctomapWithPose(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(in Origin);
            Octomap.RosSerialize(ref b);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Align8();
            b.Serialize(in Origin);
            Octomap.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Octomap, nameof(Octomap));
            Octomap.RosValidate();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 56;
                size += Header.RosMessageLength;
                size += Octomap.RosMessageLength;
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Header.AddRos2MessageLength(size);
            size = WriteBuffer2.Align8(size);
            size += 56; // Origin
            size = Octomap.AddRos2MessageLength(size);
            return size;
        }
    
        public const string MessageType = "octomap_msgs/OctomapWithPose";
    
        [IgnoreDataMember] public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "20b380aca6a508a657e95526cddaf618";
    
        [IgnoreDataMember] public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        [IgnoreDataMember]
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71VW2sVMRB+z68YOA/2SHuKF0QKPoji5UFabd+KlDnJnN1ANlmTbNv11/sle3ZrUVBE" +
                "XRY2m8x8M/PNJSt6SU9eU8c9WU9b6zmOtAux43xInOhU5yii3gkbidTWj1IrumiF+pCEwo4y1qHK0Y3N" +
                "LUVJvehMOdSjSYl2kTsh1UjoJMfxqktNOj6rENE21s+orPPAbgaElMIywMFJ43T6of2mUi/+8qM+nL89" +
                "oZTNZG+KHL6dZ/aGoyG4z4YzF5qotU0r8cjJtTgocdeLoXqax17SpgZlE+FtxEtk50YaEoTAjg5dN3ir" +
                "OQtl28k9fWgiI0w9x2z14DhCPkSDFEG8slnQ8Sb5MojXQu9fn0DGJ9FDtnBoBIKOwsn6BoekBuvzk8dF" +
                "gVZ0+SmkR5/V6uImHGFfGiRp8QKZ41y8ltse+SwOczqBsYdTlBsYAUsCcybRQd27wm9aE6zBF+mDbukA" +
                "IZyNuQ2+lsI1R8tbJwVYgwqgPihKD9bfIfsK7dmHGX5CvLPxO7B+wS0xHbVInis0pKEBkxDsY7i2BqLb" +
                "sYJoZ8VncnYb0QOqaE0m1epNLd1c8lhTgy+nFLRFJkyteZVyLOg1LVfW/Kuy/LF7EOBLdFxJEtznbMEJ" +
                "erL2JljalSZKPWs5LOVWts3+3FZZ8FIacNbdkDoLqIZFQH0cEGX0FfdO7n8FCFfmFkItZLY+1Wwt/iMW" +
                "9Eh1+V64aucC52dP6XZZjcvq6/9x/466OYYlUaige3zed778fbnjvczjjfpFRPPq5l/F9rMxXKvvT+6P" +
                "N46bMgKN+IBxxrPqQfAYW4WJ46D10Fsxa/BEu8GVOwE77PU43w4Hm20+3oRMO+tkrbYhuD1QsfHKoUvJ" +
                "mvmO2hdQaYBJfe7a0q8r+iQpuKEmo8yXbj3rpa7MlJRnNR+MpIXvuKgVkH0YSTCRnP269OOkelhGf/Hj" +
                "WmLCSdq0hQSMaFMb8SbaPN+lSaGmn19+riNHqW+zpoClqQcAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<OctomapWithPose> CreateSerializer() => new Serializer();
        public Deserializer<OctomapWithPose> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<OctomapWithPose>
        {
            public override void RosSerialize(OctomapWithPose msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(OctomapWithPose msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(OctomapWithPose msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(OctomapWithPose msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(OctomapWithPose msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<OctomapWithPose>
        {
            public override void RosDeserialize(ref ReadBuffer b, out OctomapWithPose msg) => msg = new OctomapWithPose(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out OctomapWithPose msg) => msg = new OctomapWithPose(ref b);
        }
    }
}
