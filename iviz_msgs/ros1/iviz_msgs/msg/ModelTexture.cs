/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class ModelTexture : IDeserializable<ModelTexture>, IMessage
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
        [DataMember (Name = "path")] public string Path;
        [DataMember (Name = "index")] public int Index;
        [DataMember (Name = "type")] public byte Type;
        [DataMember (Name = "mapping")] public byte Mapping;
        [DataMember (Name = "uv_index")] public int UvIndex;
        [DataMember (Name = "blend_factor")] public float BlendFactor;
        [DataMember (Name = "operation")] public byte Operation;
        [DataMember (Name = "wrap_mode_u")] public byte WrapModeU;
        [DataMember (Name = "wrap_mode_v")] public byte WrapModeV;
    
        public ModelTexture()
        {
            Path = "";
        }
        
        public ModelTexture(ref ReadBuffer b)
        {
            b.DeserializeString(out Path);
            b.Deserialize(out Index);
            b.Deserialize(out Type);
            b.Deserialize(out Mapping);
            b.Deserialize(out UvIndex);
            b.Deserialize(out BlendFactor);
            b.Deserialize(out Operation);
            b.Deserialize(out WrapModeU);
            b.Deserialize(out WrapModeV);
        }
        
        public ModelTexture(ref ReadBuffer2 b)
        {
            b.Align4();
            b.DeserializeString(out Path);
            b.Align4();
            b.Deserialize(out Index);
            b.Deserialize(out Type);
            b.Deserialize(out Mapping);
            b.Align4();
            b.Deserialize(out UvIndex);
            b.Deserialize(out BlendFactor);
            b.Deserialize(out Operation);
            b.Deserialize(out WrapModeU);
            b.Deserialize(out WrapModeV);
        }
        
        public ModelTexture RosDeserialize(ref ReadBuffer b) => new ModelTexture(ref b);
        
        public ModelTexture RosDeserialize(ref ReadBuffer2 b) => new ModelTexture(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
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
        
        public void RosSerialize(ref WriteBuffer2 b)
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
        
        public void RosValidate()
        {
            if (Path is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 21 + WriteBuffer.GetStringSize(Path);
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = WriteBuffer2.Align4(c);
            c = WriteBuffer2.AddLength(c, Path);
            c = WriteBuffer2.Align4(c);
            c += 4; // Index
            c += 1; // Type
            c += 1; // Mapping
            c = WriteBuffer2.Align4(c);
            c += 4; // UvIndex
            c += 4; // BlendFactor
            c += 1; // Operation
            c += 1; // WrapModeU
            c += 1; // WrapModeV
            return c;
        }
    
        public const string MessageType = "iviz_msgs/ModelTexture";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "0c05ed3d1750fc865d4edeecbd425ef0";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
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
