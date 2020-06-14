using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract (Name = "mesh_msgs/MeshVertexColorsStamped")]
    public sealed class MeshVertexColorsStamped : IMessage
    {
        // Mesh Attribute Message
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "uuid")] public string Uuid { get; set; }
        [DataMember (Name = "mesh_vertex_colors")] public MeshMsgs.MeshVertexColors MeshVertexColors { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public MeshVertexColorsStamped()
        {
            Uuid = "";
            MeshVertexColors = new MeshMsgs.MeshVertexColors();
        }
        
        /// <summary> Explicit constructor. </summary>
        public MeshVertexColorsStamped(in StdMsgs.Header Header, string Uuid, MeshMsgs.MeshVertexColors MeshVertexColors)
        {
            this.Header = Header;
            this.Uuid = Uuid;
            this.MeshVertexColors = MeshVertexColors;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal MeshVertexColorsStamped(Buffer b)
        {
            Header = new StdMsgs.Header(b);
            Uuid = b.DeserializeString();
            MeshVertexColors = new MeshMsgs.MeshVertexColors(b);
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new MeshVertexColorsStamped(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            Header.RosSerialize(b);
            b.Serialize(Uuid);
            MeshVertexColors.RosSerialize(b);
        }
        
        public void RosValidate()
        {
            Header.RosValidate();
            if (Uuid is null) throw new System.NullReferenceException();
            if (MeshVertexColors is null) throw new System.NullReferenceException();
            MeshVertexColors.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += Header.RosMessageLength;
                size += BuiltIns.UTF8.GetByteCount(Uuid);
                size += MeshVertexColors.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "mesh_msgs/MeshVertexColorsStamped";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "9e3527729bbf26fabb162c58762b6739";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71Ty2ocMRC86ysa9mA7sA4kt4UcnBg/DgZjm1xMWHqk3hmBRprosfb8fUpaPDGBkByc" +
                "DIKRWlXVT63oRtJAZzlH25Us9Zi4F5Wy2Y6pT++vhI1EGtoP5mh9T6VYo0YwD5iq8VVilucvwYWYqF3t" +
                "m2Wrm0mpT2/8qZv7yw39Eqda0X1mbzgaBJHZcGbaBcRv+0Hi2sleHEg8TmKo3eZ5knQK4sNgE2H14iWy" +
                "czOVBFAOpMM4Fm81o0DZIrnXfDCtJ6aJY7a6OI7Ah2isr/Bd5FGqOlaS70W8Fro+3wDjk+iSLQKaoaCj" +
                "cKq1vT4nVazPHz9Uglo9PIU1jtKjC4tzygPnGqw8TxEtQzCcNvDx7pDcKbRRHIEXk+i42bY4phOCE4Qg" +
                "U9ADHSPy2zkPwUNQaM/RcuekCmtUAKpHlXR08krZN2nPPrzIHxR/+vgbWb/o1pzWA3rmavap9CgggFMM" +
                "e2sA7eYmop0Vn8nZLnKcVWUdXKrVRa0xQGC1juDPKQVt0QBDTzYPL3PburHF7P6jafz9k0Cif3ppDXh3" +
                "+fns8Rv937ezeFY7F7hOXlx2/bLrlh0r9QNa6Oz/OQQAAA==";
                
    }
}
