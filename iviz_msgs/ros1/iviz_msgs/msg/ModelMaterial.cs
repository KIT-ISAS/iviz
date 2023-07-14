/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class ModelMaterial : IHasSerializer<ModelMaterial>, IMessage
    {
        public const byte BLEND_DEFAULT = 0;
        public const byte BLEND_ADDITIVE = 1;
        [DataMember (Name = "name")] public string Name;
        [DataMember (Name = "ambient")] public Color32 Ambient;
        [DataMember (Name = "diffuse")] public Color32 Diffuse;
        [DataMember (Name = "emissive")] public Color32 Emissive;
        [DataMember (Name = "opacity")] public float Opacity;
        [DataMember (Name = "bump_scaling")] public float BumpScaling;
        [DataMember (Name = "shininess")] public float Shininess;
        [DataMember (Name = "shininess_strength")] public float ShininessStrength;
        [DataMember (Name = "reflectivity")] public float Reflectivity;
        [DataMember (Name = "blend_mode")] public byte BlendMode;
        [DataMember (Name = "textures")] public ModelTexture[] Textures;
    
        public ModelMaterial()
        {
            Name = "";
            Textures = EmptyArray<ModelTexture>.Value;
        }
        
        public ModelMaterial(ref ReadBuffer b)
        {
            Name = b.DeserializeString();
            b.Deserialize(out Ambient);
            b.Deserialize(out Diffuse);
            b.Deserialize(out Emissive);
            b.Deserialize(out Opacity);
            b.Deserialize(out BumpScaling);
            b.Deserialize(out Shininess);
            b.Deserialize(out ShininessStrength);
            b.Deserialize(out Reflectivity);
            b.Deserialize(out BlendMode);
            {
                int n = b.DeserializeArrayLength();
                ModelTexture[] array;
                if (n == 0) array = EmptyArray<ModelTexture>.Value;
                else
                {
                    array = new ModelTexture[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new ModelTexture(ref b);
                    }
                }
                Textures = array;
            }
        }
        
        public ModelMaterial(ref ReadBuffer2 b)
        {
            b.Align4();
            Name = b.DeserializeString();
            b.Deserialize(out Ambient);
            b.Deserialize(out Diffuse);
            b.Deserialize(out Emissive);
            b.Align4();
            b.Deserialize(out Opacity);
            b.Deserialize(out BumpScaling);
            b.Deserialize(out Shininess);
            b.Deserialize(out ShininessStrength);
            b.Deserialize(out Reflectivity);
            b.Deserialize(out BlendMode);
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                ModelTexture[] array;
                if (n == 0) array = EmptyArray<ModelTexture>.Value;
                else
                {
                    array = new ModelTexture[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new ModelTexture(ref b);
                    }
                }
                Textures = array;
            }
        }
        
        public ModelMaterial RosDeserialize(ref ReadBuffer b) => new ModelMaterial(ref b);
        
        public ModelMaterial RosDeserialize(ref ReadBuffer2 b) => new ModelMaterial(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Name);
            b.Serialize(in Ambient);
            b.Serialize(in Diffuse);
            b.Serialize(in Emissive);
            b.Serialize(Opacity);
            b.Serialize(BumpScaling);
            b.Serialize(Shininess);
            b.Serialize(ShininessStrength);
            b.Serialize(Reflectivity);
            b.Serialize(BlendMode);
            b.Serialize(Textures.Length);
            foreach (var t in Textures)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align4();
            b.Serialize(Name);
            b.Serialize(in Ambient);
            b.Serialize(in Diffuse);
            b.Serialize(in Emissive);
            b.Align4();
            b.Serialize(Opacity);
            b.Serialize(BumpScaling);
            b.Serialize(Shininess);
            b.Serialize(ShininessStrength);
            b.Serialize(Reflectivity);
            b.Serialize(BlendMode);
            b.Align4();
            b.Serialize(Textures.Length);
            foreach (var t in Textures)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Name, nameof(Name));
            BuiltIns.ThrowIfNull(Textures, nameof(Textures));
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 41;
                size += WriteBuffer.GetStringSize(Name);
                foreach (var msg in Textures) size += msg.RosMessageLength;
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Name);
            size += 4; // Ambient
            size += 4; // Diffuse
            size += 4; // Emissive
            size = WriteBuffer2.Align4(size);
            size += 4; // Opacity
            size += 4; // BumpScaling
            size += 4; // Shininess
            size += 4; // ShininessStrength
            size += 4; // Reflectivity
            size += 1; // BlendMode
            size = WriteBuffer2.Align4(size);
            size += 4; // Textures.Length
            foreach (var msg in Textures) size = msg.AddRos2MessageLength(size);
            return size;
        }
    
        public const string MessageType = "iviz_msgs/ModelMaterial";
    
        [IgnoreDataMember] public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "dea645939e7a51f77d59181b714cdac1";
    
        [IgnoreDataMember] public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        [IgnoreDataMember]
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE7WUXW/bIBSG7/kV/gfb0nbrJvWC2CRBMxj5I11UTYgkJEXylwzO2v364c8S9Xb1BTbP" +
                "ewQvx5zTqtLce8sQ0YAHaAWzMPUevM+gdTgMApziLbLCFwC0aVR59kpRSOBXedXcLDxR7JUszTw/qtOp" +
                "1W+6LJTW6iLBKa+EsaCqxUGZ13m+b4ua64PI7dIz1M+qVKXU+j3h1oUsz+Z5lhp5yuXBqEu37OB+n8vy" +
                "yIvqKAGxQ57KF9M28um3Z4YvDcDDf34ASdY/POviLy/0WX8aMzA6asb3eXI4vsXHG3EzMO6a7hjiNKLI" +
                "+eM9C/BqlSXD/3ZwwpCfhTC2fOFySJYY0e7e3LgYEZwkw7W5dfkG4fWmi7679hETGCYWf73ac4Mppijp" +
                "hG+uEDHo43Rn8f219YSF0EdkMPTd1cJuXwJZd66r88ZoFSI/xRHtpKszZ/QnjR57vgCjYJdgmK75Ko4I" +
                "z7ZO9iYlYRsUu/mbBH8XYhogN4WTtIx+ORmcqD0MdTM48Tdfd5OtiHFiyxezcOdYstTWr2PFgiRbpjH0" +
                "U8eFpQHe4gA5HrpIEkXpZlzh1uF4TVEw8tnBYwwZ7wZn/575ISTM8dBDguM4cjPR0wD5MOxNTJ2mFrbM" +
                "bYAtclUe5csYbV7r6SIXoq67xjEEtRc+xM3Npe8DJ3Ew1VSAVS0bYVRVjvM/jaj7TsHbd+QCwD/NTufl" +
                "JgUAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<ModelMaterial> CreateSerializer() => new Serializer();
        public Deserializer<ModelMaterial> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<ModelMaterial>
        {
            public override void RosSerialize(ModelMaterial msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(ModelMaterial msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(ModelMaterial msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(ModelMaterial msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(ModelMaterial msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<ModelMaterial>
        {
            public override void RosDeserialize(ref ReadBuffer b, out ModelMaterial msg) => msg = new ModelMaterial(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out ModelMaterial msg) => msg = new ModelMaterial(ref b);
        }
    }
}
