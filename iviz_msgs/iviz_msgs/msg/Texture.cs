/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [Preserve, DataContract (Name = "iviz_msgs/Texture")]
    public sealed class Texture : IDeserializable<Texture>, IMessage
    {
        public const byte TYPE_NONE = 0;
        public const byte TYPE_DIFFUSE = 1;
        public const byte TYPE_SPECULAR = 2;
        public const byte TYPE_AMBIENT = 3;
        public const byte TYPE_EMISSIVE = 4;
        public const byte TYPE_HEIGHT = 5;
        public const byte TYPE_NORMALS = 6;
        public const byte TYPE_SHININESS = 7;
        public const byte TYPE_OPACITY = 8;
        public const byte TYPE_DISPLACEMENT = 9;
        public const byte TYPE_LIGHTMAP = 10;
        public const byte TYPE_REFLECTION = 11;
        public const byte TYPE_UNKNOWN = 12;
        public const byte MAPPING_FROM_UV = 0;
        public const byte MAPPING_SPHERE = 1;
        public const byte MAPPING_CYLINDER = 2;
        public const byte MAPPING_BOX = 3;
        public const byte MAPPING_PLANE = 4;
        public const byte MAPPING_UNKNOWN = 5;
        public const byte OP_MULTIPLY = 0;
        public const byte OP_ADD = 1;
        public const byte OP_SUBTRACT = 2;
        public const byte OP_DIVIDE = 3;
        public const byte OP_SMOOTH_ADD = 4;
        public const byte OP_SIGNED_ADD = 5;
        public const byte WRAP_WRAP = 0;
        public const byte WRAP_CLAMP = 1;
        public const byte WRAP_MIRROR = 2;
        public const byte WRAP_DECAL = 3;
        [DataMember (Name = "path")] public string Path { get; set; }
        [DataMember (Name = "index")] public int Index { get; set; }
        [DataMember (Name = "type")] public byte Type { get; set; }
        [DataMember (Name = "mapping")] public byte Mapping { get; set; }
        [DataMember (Name = "uv_index")] public int UvIndex { get; set; }
        [DataMember (Name = "blend_factor")] public float BlendFactor { get; set; }
        [DataMember (Name = "operation")] public byte Operation { get; set; }
        [DataMember (Name = "wrap_mode_u")] public byte WrapModeU { get; set; }
        [DataMember (Name = "wrap_mode_v")] public byte WrapModeV { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Texture()
        {
            Path = string.Empty;
        }
        
        /// <summary> Explicit constructor. </summary>
        public Texture(string Path, int Index, byte Type, byte Mapping, int UvIndex, float BlendFactor, byte Operation, byte WrapModeU, byte WrapModeV)
        {
            this.Path = Path;
            this.Index = Index;
            this.Type = Type;
            this.Mapping = Mapping;
            this.UvIndex = UvIndex;
            this.BlendFactor = BlendFactor;
            this.Operation = Operation;
            this.WrapModeU = WrapModeU;
            this.WrapModeV = WrapModeV;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Texture(ref Buffer b)
        {
            Path = b.DeserializeString();
            Index = b.Deserialize<int>();
            Type = b.Deserialize<byte>();
            Mapping = b.Deserialize<byte>();
            UvIndex = b.Deserialize<int>();
            BlendFactor = b.Deserialize<float>();
            Operation = b.Deserialize<byte>();
            WrapModeU = b.Deserialize<byte>();
            WrapModeV = b.Deserialize<byte>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Texture(ref b);
        }
        
        Texture IDeserializable<Texture>.RosDeserialize(ref Buffer b)
        {
            return new Texture(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Path);
            b.Serialize(Index);
            b.Serialize(Type);
            b.Serialize(Mapping);
            b.Serialize(UvIndex);
            b.Serialize(BlendFactor);
            b.Serialize(Operation);
            b.Serialize(WrapModeU);
            b.Serialize(WrapModeV);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Path is null) throw new System.NullReferenceException(nameof(Path));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 21;
                size += BuiltIns.UTF8.GetByteCount(Path);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "iviz_msgs/Texture";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "0c05ed3d1750fc865d4edeecbd425ef0";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE2WQzVKDMBSF9zwFj6D90bpwkUIoGfM3CbSyyqClykwLDELVtzehoLfjhoHvHO499/Rl" +
                "1a38JJPYcMGx/+jfeP0fC0kUpdrhW4i1xEFKkbJ8Bjlia4J5YvEcYsyI1mTrxiwgjzHZxM69hJQLxRDV" +
                "Ft9d7YwJJxxrJ9xDQUgUkCSzeHUdXUuKAswugR6gRt1ehqS76+pehSOKg4QI7qSrm1P+xMVu4DNvFOwI" +
                "SfjGREowk25Be5OiZYwV7G8SgowSHmJY4SStxTNocKL2GA4bnPhfruUUS0jDUpoQSTMQyVIUhiCKBTpd" +
                "JwoFCUhhaUi2JMQgg3MyIZJ4nLAAnGw4Dkf+m2CnkDTuAfYPLKCISZBhgIwoJWATAw1xgOgQwvvo2rJ6" +
                "85u8e/esYT7zy2pffI3u7rspxtdT3jTWOZr6s7n4Dsc6d+DlWFR7c8hfu7od/6ibos27sq7G7882b8yp" +
                "3hem/0fOnvcDILAaeDADAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
