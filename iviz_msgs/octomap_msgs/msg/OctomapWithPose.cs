/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.OctomapMsgs
{
    [DataContract (Name = "octomap_msgs/OctomapWithPose")]
    public sealed class OctomapWithPose : IDeserializable<OctomapWithPose>, IMessage
    {
        // A 3D map in binary format, as Octree
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        // The pose of the octree with respect to the header frame 
        [DataMember (Name = "origin")] public GeometryMsgs.Pose Origin { get; set; }
        // The actual octree msg
        [DataMember (Name = "octomap")] public OctomapMsgs.Octomap Octomap { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public OctomapWithPose()
        {
            Header = new StdMsgs.Header();
            Octomap = new OctomapMsgs.Octomap();
        }
        
        /// <summary> Explicit constructor. </summary>
        public OctomapWithPose(StdMsgs.Header Header, in GeometryMsgs.Pose Origin, OctomapMsgs.Octomap Octomap)
        {
            this.Header = Header;
            this.Origin = Origin;
            this.Octomap = Octomap;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public OctomapWithPose(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Origin = new GeometryMsgs.Pose(ref b);
            Octomap = new OctomapMsgs.Octomap(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new OctomapWithPose(ref b);
        }
        
        OctomapWithPose IDeserializable<OctomapWithPose>.RosDeserialize(ref Buffer b)
        {
            return new OctomapWithPose(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Origin.RosSerialize(ref b);
            Octomap.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
            if (Octomap is null) throw new System.NullReferenceException(nameof(Octomap));
            Octomap.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 56;
                size += Header.RosMessageLength;
                size += Octomap.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "octomap_msgs/OctomapWithPose";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "20b380aca6a508a657e95526cddaf618";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
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
                
    }
}
