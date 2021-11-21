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
        [DataMember (Name = "path")] public string Path;
        [DataMember (Name = "index")] public int Index;
        [DataMember (Name = "type")] public byte Type;
        [DataMember (Name = "mapping")] public byte Mapping;
        [DataMember (Name = "uv_index")] public int UvIndex;
        [DataMember (Name = "blend_factor")] public float BlendFactor;
        [DataMember (Name = "operation")] public byte Operation;
        [DataMember (Name = "wrap_mode_u")] public byte WrapModeU;
        [DataMember (Name = "wrap_mode_v")] public byte WrapModeV;
    
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
        internal Texture(ref Buffer b)
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
        
        public void RosValidate()
        {
            if (Path is null) throw new System.NullReferenceException(nameof(Path));
        }
    
        public int RosMessageLength => 21 + BuiltIns.GetStringSize(Path);
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "iviz_msgs/Texture";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "0c05ed3d1750fc865d4edeecbd425ef0";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACmWQzU6DQBSF9yS+A4+g1WpduJjCUCbOX2agldUELVWSFghC1bd3hoK9jRsC3znce+7p" +
                "y6pb+EkmseGCY//Jv/b6MwtJFKXa4RuItcRBSpGyfAY5YkuCeWLxLcSYEa3J2o25gzzGZBU79xxSLhRD" +
                "VFt8f7EzJpxwrJ3wAAUhUUCSzOLFZXQtKQowOwV6hBp1exmS7q6LexWOKA4SIriTLm5O+TMXm4HPvFGw" +
                "IyThKxMpwUy6Bu1NipYxVrC/SQgySniIYYWTtBQvoMGJ2mM4bHDi51zzKZaQhqU0IZJmIJKlKAxBFAt0" +
                "ukwUChKQwtKQrEmIQQbnZEIk8ThhyuA4WXEcjvwvwUYhadwD7B9YQBEbaoeQEaUEbGKgIQ4QHUJ4n11b" +
                "Vu9+k3cfnjXczvyy2hbfo7v7aYrx9ZA3jXWOpv5oTr7dvs4deN0X1dbs8reubsc/6qZo866sq/H7q80b" +
                "c6i3hen/kaN35f0CLKqARjEDAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
