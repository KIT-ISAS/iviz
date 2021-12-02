/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class MeshVertexColorsStamped : IDeserializable<MeshVertexColorsStamped>, IMessage
    {
        // Mesh Attribute Message
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "uuid")] public string Uuid;
        [DataMember (Name = "mesh_vertex_colors")] public MeshMsgs.MeshVertexColors MeshVertexColors;
    
        /// Constructor for empty message.
        public MeshVertexColorsStamped()
        {
            Uuid = string.Empty;
            MeshVertexColors = new MeshMsgs.MeshVertexColors();
        }
        
        /// Explicit constructor.
        public MeshVertexColorsStamped(in StdMsgs.Header Header, string Uuid, MeshMsgs.MeshVertexColors MeshVertexColors)
        {
            this.Header = Header;
            this.Uuid = Uuid;
            this.MeshVertexColors = MeshVertexColors;
        }
        
        /// Constructor with buffer.
        internal MeshVertexColorsStamped(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Uuid = b.DeserializeString();
            MeshVertexColors = new MeshMsgs.MeshVertexColors(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new MeshVertexColorsStamped(ref b);
        
        MeshVertexColorsStamped IDeserializable<MeshVertexColorsStamped>.RosDeserialize(ref Buffer b) => new MeshVertexColorsStamped(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Uuid);
            MeshVertexColors.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Uuid is null) throw new System.NullReferenceException(nameof(Uuid));
            if (MeshVertexColors is null) throw new System.NullReferenceException(nameof(MeshVertexColors));
            MeshVertexColors.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += Header.RosMessageLength;
                size += BuiltIns.GetStringSize(Uuid);
                size += MeshVertexColors.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "mesh_msgs/MeshVertexColorsStamped";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "9e3527729bbf26fabb162c58762b6739";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1Ty2ocMRC86ysa9mA7sA4kt4UcnBg/DgZjm1xMWHqk3hmBRprosfb8fUpaPDGBkByc" +
                "DILRo6q6ulta0Y2kgc5yjrYrWeoycS8qZbMdU5/eXwkbiTS0H7aj9T2VYo0awTxgqsZXiVmevwQXYqJ2" +
                "tG87W922lPr0xp+6ub/c0C8+1YruM3vD0cBEZsOZaRfg3/aDxLWTvTiQeJzEUDvN8yTpFMSHwSbC6MVL" +
                "ZOdmKgmgHEiHcSzeakaBskVyr/lgWk9ME8dsdXEcgQ/RWF/hu8ijVHWMJN+LeC10fb4BxifRJVsYmqGg" +
                "o3Cqtb0+J1Wszx8/VIJaPTyFNZbSowtLcMoD52pWnqeIlsEMpw1ivDskdwptFEcQxSQ6bntbLNMJIQgs" +
                "yBT0QMdwfjvnIXgICu05Wu6cVGGNCkD1qJKOTl4pV9sb8uzDi/xB8WeMv5H1i27NaT2gZ65mn0qPAgI4" +
                "xbC3BtBubiLaWfGZnO0ix1lV1iGkWl3UGgMEVusI/pxS0BYNMPRk8/Byb1s3tri7/+g2jr99Ekj0Ty+t" +
                "Ae8uP589fqP/+3aWyGrnAtebF5dZv8y6ZcZK/QBa6Oz/OQQAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
