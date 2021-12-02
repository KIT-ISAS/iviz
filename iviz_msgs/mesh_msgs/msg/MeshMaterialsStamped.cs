/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class MeshMaterialsStamped : IDeserializable<MeshMaterialsStamped>, IMessage
    {
        // Mesh Attribute Message
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "uuid")] public string Uuid;
        [DataMember (Name = "mesh_materials")] public MeshMsgs.MeshMaterials MeshMaterials;
    
        /// Constructor for empty message.
        public MeshMaterialsStamped()
        {
            Uuid = string.Empty;
            MeshMaterials = new MeshMsgs.MeshMaterials();
        }
        
        /// Explicit constructor.
        public MeshMaterialsStamped(in StdMsgs.Header Header, string Uuid, MeshMsgs.MeshMaterials MeshMaterials)
        {
            this.Header = Header;
            this.Uuid = Uuid;
            this.MeshMaterials = MeshMaterials;
        }
        
        /// Constructor with buffer.
        internal MeshMaterialsStamped(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Uuid = b.DeserializeString();
            MeshMaterials = new MeshMsgs.MeshMaterials(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new MeshMaterialsStamped(ref b);
        
        MeshMaterialsStamped IDeserializable<MeshMaterialsStamped>.RosDeserialize(ref Buffer b) => new MeshMaterialsStamped(ref b);
    
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
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "mesh_msgs/MeshMaterialsStamped";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "80683ad6336327fea277cb1ed5f58927";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UTWvbQBC9768Y8CFJwSm0N0MPqUPSHAKlMb2UYkbSWFpY76r74Vj/vm+lSnZSDD3U" +
                "MTKaXc17++ZrZ/QooaGbGL0uUpS8DFyLCrFab0Md3n8RrsRT07+w7bWtKSVdqS2Qg0/meOQoXrMJNOyP" +
                "S6U+/eefeny6X9ArgWpGT5Ftxb6CgMgVR6aNg3BdN+LnRnZiAOJtKxX1X2PXSrgGcNXoQHhqseLZmI5S" +
                "gFN0VLrtNlldIhiKGoEd44HUlpha9lGXybCHv/OVttl943krmR1PkF9JbCn0cLuAjw1SpqghqAND6YVD" +
                "TurDLamkbfz4IQPUbPXs5lhKjfRPh1NsOGaxsm89agUxHBY4490Q3DW4kRzBKVWgy35vjWW4IhwCCdK6" +
                "sqFLKP/axcZZEArtGLUqjGTiEhkA60UGXVwdMWfZC7Js3Ug/MB7O+BdaO/HmmOYNamZy9CHVSCAcW+92" +
                "uoJr0fUkpdFiIxldePadyqjhSDW7yzmGE1B9RfDmEFypUYCKnnVsxobtq7FG056pG0/Mgjo5Xy8Bd1zK" +
                "0qQA2I+fiLi3wokJg8dhuoaGOYCOBu8l+rv4KPuV7Je5RwMQu35nnf99355tVE/GqmajMYWxwde1tpUu" +
                "BdlzbdTOshnLaLgQ87Y1HEcSaYrJ99pkf7gel844/+3+8w3mGpYqnDPUcFj/8T/79bccBaiNcZyV+smq" +
                "J6uYLH6b9L1qt78HYYXbdxKVJmun1G/gQWQKlAYAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
