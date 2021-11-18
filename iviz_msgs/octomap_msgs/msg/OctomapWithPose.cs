/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.OctomapMsgs
{
    [Preserve, DataContract (Name = "octomap_msgs/OctomapWithPose")]
    public sealed class OctomapWithPose : IDeserializable<OctomapWithPose>, IMessage
    {
        // A 3D map in binary format, as Octree
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // The pose of the octree with respect to the header frame 
        [DataMember (Name = "origin")] public GeometryMsgs.Pose Origin;
        // The actual octree msg
        [DataMember (Name = "octomap")] public OctomapMsgs.Octomap Octomap;
    
        /// <summary> Constructor for empty message. </summary>
        public OctomapWithPose()
        {
            Octomap = new OctomapMsgs.Octomap();
        }
        
        /// <summary> Explicit constructor. </summary>
        public OctomapWithPose(in StdMsgs.Header Header, in GeometryMsgs.Pose Origin, OctomapMsgs.Octomap Octomap)
        {
            this.Header = Header;
            this.Origin = Origin;
            this.Octomap = Octomap;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal OctomapWithPose(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Origin);
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
            b.Serialize(Origin);
            Octomap.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Octomap is null) throw new System.NullReferenceException(nameof(Octomap));
            Octomap.RosValidate();
        }
    
        public int RosMessageLength => 56 + Header.RosMessageLength + Octomap.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "octomap_msgs/OctomapWithPose";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "20b380aca6a508a657e95526cddaf618";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1VXWsVMRB9D/Q/DPShrbS3oCJS8EEsfjyI9eNNpMzNzt0NZJM1ybauv94z2e7WoqCI" +
                "ermwyWbmZM6Zj92np/TgnHoeyAXausBpol1MPZdj4kxvbEki5qVwI4m6+jBmnz50QkPMQnFHBetY7eja" +
                "lY6S5EFsoRLr0exEu8S9kGkl9lLSdNnnNp9eVIjkWhcWVLZlZL8AwspgGRHg7PFm3ui5Ps2eefKXf3vm" +
                "9fsXZ5RLM984c99DeO8Lh4ZTQ2DADRdWpahzbSfpxMuVeHhxP0hD9bRMg+RN5eUy4d9KkMTeTzRmGEEg" +
                "G/t+DM5yESqulzv+8ERSmAZOxdnRc4J9TA2yBPMqqKLjn+XzKMEKvTo/g03IYsfiENAEBJuEswstDsmM" +
                "LpQH99XB7H+4jifYSov0rJcjZ1w0WPkyIJMaJ+cz3HFvJrcBNtQR3NJkOqzvLrHNR4RLEIIM0XZ0iMgv" +
                "ptLFUIvgipPjrRcFtlAAqAfqdHD0HbKGfUaBQ1zgZ8TbO34HNqy4yumkQ868ss9jCwFhOKR45RqYbqcK" +
                "Yr2TUMi7bUL1G/WarzT7z2vRFk1fzQienHO0DgloarWbXJKi12xcuubfFeSPnaM1+RTtpnkCAy4OsqAh" +
                "a2NCqJ12UB7YyrEWmr5ubs5dtYU02n2L74bMRURBrAbm7QiiKVTcW7v/xxHB7C39g4oo7EKuOVspgA4a" +
                "pEZ9h7HZ+cjl0UP6sq6mdfX1fzG41W+lsaYLpXRH1bvx6+7zrfo6kjfmF6SW1fW/o/ezWTyX4Z98RZ57" +
                "bnUKNhIiJhovrocxYHKpGKfR2nFw0hxBKtqNXr8MeMPBTss34nCzLaebWGjnvByZbYz+BkjveObRseRq" +
                "4ddun8tIO2F2XzoYvQvzd5Kjx+yE5jpretw7++Ve50sui1uIjeRVcqT0xk1BbmhkwXTy7uvamLPrsU5/" +
                "jeNKUsZJ3nQqAqY0Ri068jo5iDHbZoPKfvzxUx0/SOs3w3pbZLAHAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
