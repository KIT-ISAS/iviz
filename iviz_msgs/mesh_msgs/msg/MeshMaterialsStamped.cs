/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [Preserve, DataContract (Name = "mesh_msgs/MeshMaterialsStamped")]
    public sealed class MeshMaterialsStamped : IDeserializable<MeshMaterialsStamped>, IMessage
    {
        // Mesh Attribute Message
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "uuid")] public string Uuid;
        [DataMember (Name = "mesh_materials")] public MeshMsgs.MeshMaterials MeshMaterials;
    
        /// <summary> Constructor for empty message. </summary>
        public MeshMaterialsStamped()
        {
            Uuid = string.Empty;
            MeshMaterials = new MeshMsgs.MeshMaterials();
        }
        
        /// <summary> Explicit constructor. </summary>
        public MeshMaterialsStamped(in StdMsgs.Header Header, string Uuid, MeshMsgs.MeshMaterials MeshMaterials)
        {
            this.Header = Header;
            this.Uuid = Uuid;
            this.MeshMaterials = MeshMaterials;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal MeshMaterialsStamped(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Uuid = b.DeserializeString();
            MeshMaterials = new MeshMsgs.MeshMaterials(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new MeshMaterialsStamped(ref b);
        }
        
        MeshMaterialsStamped IDeserializable<MeshMaterialsStamped>.RosDeserialize(ref Buffer b)
        {
            return new MeshMaterialsStamped(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Uuid);
            MeshMaterials.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Uuid is null) throw new System.NullReferenceException(nameof(Uuid));
            if (MeshMaterials is null) throw new System.NullReferenceException(nameof(MeshMaterials));
            MeshMaterials.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += Header.RosMessageLength;
                size += BuiltIns.GetStringSize(Uuid);
                size += MeshMaterials.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "mesh_msgs/MeshMaterialsStamped";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "80683ad6336327fea277cb1ed5f58927";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UTWvbQBC9L/g/DPiQpOAU2puhh9QhaQ6G0pheSjEjaSwtrHfV/XCsf9+3ciw7IYYe" +
                "GhsZza7mvX3ztWOaS2joJkavixQlLwPXokKslutQh4/fhCvx1PQvbHtta0pJV2oN5M4nc8w5itdsAu32" +
                "90s1Ul/+82+k5o/3U3olcaTG9BjZVuwraIhccWRaOWjXdSN+YmQjBihet1JR/zV2rYRrABeNDoSnFiue" +
                "jekoBThFR6Vbr5PVJeKhqBHbMR5IbYmpZR91mQx7+DtfaZvdV57XktnxBPmTxJZCD7dT+NggZYoagjow" +
                "lF445Lw+3JJK2sbPnzJAjRdPboKl1KjAcDjFhmMWK9vWo1wQw2GKMz7sgrsGN7IjOKUKdNnvLbEMV4RD" +
                "IEFaVzZ0CeXfu9g4C0KhDaNchZFMXCIDYL3IoIurI+Yse0qWrdvT7xgPZ/wLrR14c0yTBjUzOfqQaiQQ" +
                "jq13G13Bteh6ktJosZGMLjz7TmXU7kg1vss5hhNQfUXw5hBcqVGAip50bPY921djib59t4Y8MRC5MU9M" +
                "2UvEHZcyMykA9+s3gu6tcGLO4HGYsV3PHEBH4/cS/VN8lO1CtrPcpgGITb+zzP++dd9xYE9Giww9W4dI" +
                "Vvi81LbSpQQ1dm3UzrLZF9NwIebclRztZxPJisn38mR7uCpnzjj/4/7rDQYcliqcM9RwWD77n+EqnO0l" +
                "jNTKOM5i/WDVg1UMFp8ria86742hWOAyHnSlwdpA4V8B3g8OpwYAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
