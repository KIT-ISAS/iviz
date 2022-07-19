/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract]
    public sealed class MeshVertexColorsStamped : IDeserializableRos1<MeshVertexColorsStamped>, IDeserializableRos2<MeshVertexColorsStamped>, IMessageRos1, IMessageRos2
    {
        // Mesh Attribute Message
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "uuid")] public string Uuid;
        [DataMember (Name = "mesh_vertex_colors")] public MeshMsgs.MeshVertexColors MeshVertexColors;
    
        /// Constructor for empty message.
        public MeshVertexColorsStamped()
        {
            Uuid = "";
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
        public MeshVertexColorsStamped(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.DeserializeString(out Uuid);
            MeshVertexColors = new MeshMsgs.MeshVertexColors(ref b);
        }
        
        /// Constructor with buffer.
        public MeshVertexColorsStamped(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.DeserializeString(out Uuid);
            MeshVertexColors = new MeshMsgs.MeshVertexColors(ref b);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new MeshVertexColorsStamped(ref b);
        
        public MeshVertexColorsStamped RosDeserialize(ref ReadBuffer b) => new MeshVertexColorsStamped(ref b);
        
        public MeshVertexColorsStamped RosDeserialize(ref ReadBuffer2 b) => new MeshVertexColorsStamped(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Uuid);
            MeshVertexColors.RosSerialize(ref b);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Uuid);
            MeshVertexColors.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Uuid is null) BuiltIns.ThrowNullReference();
            if (MeshVertexColors is null) BuiltIns.ThrowNullReference();
            MeshVertexColors.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += Header.RosMessageLength;
                size += WriteBuffer.GetStringSize(Uuid);
                size += MeshVertexColors.RosMessageLength;
                return size;
            }
        }
        
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            Header.AddRos2MessageLength(ref c);
            WriteBuffer2.AddLength(ref c, Uuid);
            MeshVertexColors.AddRos2MessageLength(ref c);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "mesh_msgs/MeshVertexColorsStamped";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "9e3527729bbf26fabb162c58762b6739";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71Ty2ocMRC86ysa9mA7sA4kt4UcnBg/DgZjm1xMWHqk3hmBRprosfb8fUpaPDGBkByc" +
                "DIKRWlXVT63oRtJAZzlH25Us9Zi4F5Wy2Y6pT++vhI1EGtoP5mh9T6VYo0YwD5iq8VVilucvwYWYqF3t" +
                "m2Wrm0mpT2/8qZv7yw39Eqda0X1mbzgaBJHZcGbaBcRv+0Hi2sleHEg8TmKo3eZ5knQK4sNgE2H14iWy" +
                "czOVBFAOpMM4Fm81o0DZIrnXfDCtJ6aJY7a6OI7Ah2isr/Bd5FGqOlaS70W8Fro+3wDjk+iSLQKaoaCj" +
                "cKq1vT4nVazPHz9Uglo9PIU1jtKjC4tzygPnGqw8TxEtQzCcNvDx7pDcKbRRHIEXk+i42bY4phOCE4Qg" +
                "U9ADHSPy2zkPwUNQaM/RcuekCmtUAKpHlXR08krZN2nPPrzIHxR/+vgbWb/o1pzWA3rmavap9CgggFMM" +
                "e2sA7eYmop0Vn8nZLnKcVWUdXKrVRa0xQGC1juDPKQVt0QBDTzYPL3PburHF7P6jafz9k0Cif3ppDXh3" +
                "+fns8Rv937ezeFY7F7hOXlx2/bLrlh0r9QNa6Oz/OQQAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
