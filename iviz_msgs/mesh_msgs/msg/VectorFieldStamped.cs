/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class VectorFieldStamped : IDeserializable<VectorFieldStamped>, IMessage
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "vector_field")] public MeshMsgs.VectorField VectorField;
    
        /// Constructor for empty message.
        public VectorFieldStamped()
        {
            VectorField = new MeshMsgs.VectorField();
        }
        
        /// Explicit constructor.
        public VectorFieldStamped(in StdMsgs.Header Header, MeshMsgs.VectorField VectorField)
        {
            this.Header = Header;
            this.VectorField = VectorField;
        }
        
        /// Constructor with buffer.
        public VectorFieldStamped(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            VectorField = new MeshMsgs.VectorField(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new VectorFieldStamped(ref b);
        
        public VectorFieldStamped RosDeserialize(ref ReadBuffer b) => new VectorFieldStamped(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            VectorField.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (VectorField is null) throw new System.NullReferenceException(nameof(VectorField));
            VectorField.RosValidate();
        }
    
        public int RosMessageLength => 0 + Header.RosMessageLength + VectorField.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "mesh_msgs/VectorFieldStamped";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "3d9fc2de2c0939ad4bbe0890ccb68ce5";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71UTWvcMBC9+1cM5JCkbFxISg8LvZW0ORQCCb2UssxKY1tUllxJ3o376/sk77pJCbSH" +
                "pothZWvemzefMelNH9v4+qOwlkBd+at6id38/bOo5MO1EatpV86bJr9U1bt//Ks+3X1YU3wqqDqhu8RO" +
                "c9DUS2LNianxEGraTsKFlZ1YgLgfRFO5TdMgsQbwvjOR8LTiJLC1E40RRsmT8n0/OqM4CSWDYB/jgTSO" +
                "mAYOyajRcoC9D9q4bN4E7iWz44nyfRSnhG7er2HjoqgxGQiawKCCcDSuxSVVo3Hp6jIDqpP7vb/Aq7RI" +
                "9+KcUscpi5WHIUjMOjmu4ePVHFwNbiRH4EVHOivfNniN5wQnkCCDVx2dQfntlDrvQCi042B4ayUTK2QA" +
                "rKcZdHr+iNkVasfOH+lnxl8+/obWLbw5posONbM5+ji2SCAMh+B3RsN0OxUSZY24RNZsA4epyqjZZXVy" +
                "nXMMI6BKRfDPMXplUABNe5O6KqaQ2Us1NubFuvHZOaha8ejFMM03tx7V/PKVBh9NMuiC3+5n5BUs5vmJ" +
                "LyX2GVnHKUBdExsXS+aPQsk3uc2zXe74JggqMLCSqrGe09s39LCcpuX04//IP2TtGECQPBZoGLTCIY9P" +
                "Ndd5IG/KCHmHAeyFERZmfUECqE0AFKHXYJUgWCSyIpNIe4nkfM5Xz99AKejnjOZhABmWSmAXLZe04TMg" +
                "Z1K39Yr2nbjZKvdj2R5l3xhFwbRGz0g46hcw0yG4FaXmEv1s7ax5doYSgST4VADnNd00NPmR9jkgHMJh" +
                "zXnayqKrjGPyfpV33IHimX7IDR25xeS6mLBg6+oPtf4JFgGUXiAGAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
