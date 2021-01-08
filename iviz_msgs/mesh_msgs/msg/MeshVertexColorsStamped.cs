/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [Preserve, DataContract (Name = "mesh_msgs/MeshVertexColorsStamped")]
    public sealed class MeshVertexColorsStamped : IDeserializable<MeshVertexColorsStamped>, IMessage
    {
        // Mesh Attribute Message
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "uuid")] public string Uuid { get; set; }
        [DataMember (Name = "mesh_vertex_colors")] public MeshMsgs.MeshVertexColors MeshVertexColors { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public MeshVertexColorsStamped()
        {
            Header = new StdMsgs.Header();
            Uuid = "";
            MeshVertexColors = new MeshMsgs.MeshVertexColors();
        }
        
        /// <summary> Explicit constructor. </summary>
        public MeshVertexColorsStamped(StdMsgs.Header Header, string Uuid, MeshMsgs.MeshVertexColors MeshVertexColors)
        {
            this.Header = Header;
            this.Uuid = Uuid;
            this.MeshVertexColors = MeshVertexColors;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public MeshVertexColorsStamped(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Uuid = b.DeserializeString();
            MeshVertexColors = new MeshMsgs.MeshVertexColors(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new MeshVertexColorsStamped(ref b);
        }
        
        MeshVertexColorsStamped IDeserializable<MeshVertexColorsStamped>.RosDeserialize(ref Buffer b)
        {
            return new MeshVertexColorsStamped(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Uuid);
            MeshVertexColors.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
            if (Uuid is null) throw new System.NullReferenceException(nameof(Uuid));
            if (MeshVertexColors is null) throw new System.NullReferenceException(nameof(MeshVertexColors));
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
                "H4sIAAAAAAAACr1TTWvcMBC9G/wfBnJIUtgU2ttCD2lD0xwCpQm9lLKMpVlLIEuuNNrE/74zWrIbAqU9" +
                "tDU21sd7b97MSCdwS8XBJXP2Q2XSacGR+q6w3UxlLK8/EVrK4NpP17OPI9Tqbd9NQt6jVOYrZabHDymk" +
                "XKBt7drKxrSlvuu7d3/56bvbu+s1vDDbdydwxxgtZitGGC0ywjZJFn50lFeBdhSEhdNMFtouLzOVC2Xe" +
                "O19A3pEiZQxhgVoExQlMmqYavUEpFHvJ8LmAUn0EhBkze1MDZiGkbH1U/DbjRE1fv0I/KkVDcHO1FlQs" +
                "ZCp7MbWIhsmERYt8cyXg6iO/faMMId4/pJXMaZSOHBwAO2R1TI9zlv6JIyxrDfNqn+OFyEuRSALZAmdt" +
                "bSPTcg4SR1zQnIyDM7H/eWGXoigS7DB7HAKpspE6iOypkk7Pn0ur9TVEjOlJfy95DPInuvEorGmtnDQv" +
                "aAlKHaWOgpxz2nkr2GFpKiZ4igzBDxnz0ndK2wcVkY9abIEJr/VG/lhKMl46YeHBszsc5NaXjR7mf3Y6" +
                "p1/eEs33txewQb9cv7/89h3++4U6BO+7bUioJ1Gu19NwPA6H4xDV2E/49QwqWgQAAA==";
                
    }
}
