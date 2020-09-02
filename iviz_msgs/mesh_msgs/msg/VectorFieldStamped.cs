/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract (Name = "mesh_msgs/VectorFieldStamped")]
    public sealed class VectorFieldStamped : IMessage
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "vector_field")] public MeshMsgs.VectorField VectorField { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public VectorFieldStamped()
        {
            Header = new StdMsgs.Header();
            VectorField = new MeshMsgs.VectorField();
        }
        
        /// <summary> Explicit constructor. </summary>
        public VectorFieldStamped(StdMsgs.Header Header, MeshMsgs.VectorField VectorField)
        {
            this.Header = Header;
            this.VectorField = VectorField;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal VectorFieldStamped(Buffer b)
        {
            Header = new StdMsgs.Header(b);
            VectorField = new MeshMsgs.VectorField(b);
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new VectorFieldStamped(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            Header.RosSerialize(b);
            VectorField.RosSerialize(b);
        }
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
            if (VectorField is null) throw new System.NullReferenceException(nameof(VectorField));
            VectorField.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += Header.RosMessageLength;
                size += VectorField.RosMessageLength;
                return size;
            }
        }
    
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
                
    }
}
