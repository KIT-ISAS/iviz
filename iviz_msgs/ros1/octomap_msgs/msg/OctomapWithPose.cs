/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.OctomapMsgs
{
    [DataContract]
    public sealed class OctomapWithPose : IDeserializable<OctomapWithPose>, IMessageRos1
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
        public OctomapWithPose(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Origin);
            Octomap = new OctomapMsgs.Octomap(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new OctomapWithPose(ref b);
        
        public OctomapWithPose RosDeserialize(ref ReadBuffer b) => new OctomapWithPose(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(in Origin);
            Octomap.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Octomap is null) BuiltIns.ThrowNullReference();
            Octomap.RosValidate();
        }
    
        public int RosMessageLength => 56 + Header.RosMessageLength + Octomap.RosMessageLength;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "octomap_msgs/OctomapWithPose";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "20b380aca6a508a657e95526cddaf618";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71VW2sVMRB+z68YOA/2SHsKVkQKPohS9UFaL28iZU4yZzeQTbZJtu321/sle3ZrUVBE" +
                "PSycXGa+mfnmkhW9pJPX1HFP1tPWeo4j7ULsOB8SJzrXOYqot8JGIrX1T6kVfW6F+pCEwo4y1qHK0Y3N" +
                "LUVJvehMOdSrSYl2kTsh1UjoJMfxsktNOr6oENE21s+orPPAbgaElMIywMFJ43za0P5QqRd/+afef3pz" +
                "Simbyd4UOXz7lNkbjobgPhvOXGii1jatxCMn1+KgxF0vhuptHntJmxqUTYSvES+RnRtpSBACOzp03eCt" +
                "5iyUbScP9KGJjDD1HLPVg+MI+RANUgTxymZBx5fkahCvhd69PoWMT6KHbOHQCAQdhZP1DS5JDdbnkydF" +
                "Qa0+34QjbKVBbhbjSBjn4qzc9khj8ZPTKWw8noLbABvkCKyYRAf17BLbtCYYgQvSB93SATy/GHMbfK2A" +
                "a46Wt04KsAYDQH1UlB6tv0P2FdqzDzP8hHhv43dg/YJbYjpqkTNXok9DAwIh2MdwbQ1Et2MF0c6Kz+Ts" +
                "NqL0VdGaTKrVWa3YXNJXM4J/TiloiwSYWuoq5VjQazYurflX1fhj0yDAl2i0kiS4z9mCE7RibUmwtCu9" +
                "k3rWcliqrByb/b2tsuCl9N2suyF1EVANi4D6MCDK6Cvuvdz/ChCuzJ2DWshsfarZWvxHLGiN6vKDcNXO" +
                "Bc7PntLtshqX1d3/cf+eujmGJVGooAd8PnS+7K7ueS9jeKN+EdG8uvlXsf1s+tbq+5Nn48xxUyafER8w" +
                "xXhWPQge06owcRy0HnorZg2eaDe48hTghL0e50fhYLPNx5uQaWedrNU2BLcHKjZeOXQpWTM/TfsCKg0w" +
                "qc9dW/p1RR8lBTfUZJT50q1nvdSVmZLyrOaDkbTwHRe1ArIPIwkmkrN3Sz9Oqodl4hc/riUm3KRNW0jA" +
                "ZDa1EW+izfMTmhRq+vmXr3XkKPUN7UeuJKAHAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
