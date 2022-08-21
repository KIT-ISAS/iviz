/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class ModelMaterial : IDeserializable<ModelMaterial>, IMessage
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
            Textures = System.Array.Empty<ModelTexture>();
        }
        
        public ModelMaterial(ref ReadBuffer b)
        {
            b.DeserializeString(out Name);
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
                Textures = n == 0
                    ? System.Array.Empty<ModelTexture>()
                    : new ModelTexture[n];
                for (int i = 0; i < n; i++)
                {
                    Textures[i] = new ModelTexture(ref b);
                }
            }
        }
        
        public ModelMaterial(ref ReadBuffer2 b)
        {
            b.Align4();
            b.DeserializeString(out Name);
            b.Deserialize(out Ambient);
            b.Deserialize(out Diffuse);
            b.Deserialize(out Emissive);
            b.Deserialize(out Opacity);
            b.Deserialize(out BumpScaling);
            b.Deserialize(out Shininess);
            b.Deserialize(out ShininessStrength);
            b.Deserialize(out Reflectivity);
            b.Deserialize(out BlendMode);
            b.Align4();
            {
                int n = b.DeserializeArrayLength();
                Textures = n == 0
                    ? System.Array.Empty<ModelTexture>()
                    : new ModelTexture[n];
                for (int i = 0; i < n; i++)
                {
                    Textures[i] = new ModelTexture(ref b);
                }
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
        
        public void RosValidate()
        {
            if (Name is null) BuiltIns.ThrowNullReference();
            if (Textures is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Textures.Length; i++)
            {
                if (Textures[i] is null) BuiltIns.ThrowNullReference(nameof(Textures), i);
                Textures[i].RosValidate();
            }
        }
    
        public int RosMessageLength => 41 + WriteBuffer.GetStringSize(Name) + WriteBuffer.GetArraySize(Textures);
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = WriteBuffer2.Align4(c);
            c = WriteBuffer2.AddLength(c, Name);
            c += 4; // Ambient
            c += 4; // Diffuse
            c += 4; // Emissive
            c = WriteBuffer2.Align4(c);
            c += 4; // Opacity
            c += 4; // BumpScaling
            c += 4; // Shininess
            c += 4; // ShininessStrength
            c += 4; // Reflectivity
            c += 1; // BlendMode
            c = WriteBuffer2.Align4(c);
            c += 4; // Textures.Length
            foreach (var t in Textures)
            {
                c = t.AddRos2MessageLength(c);
            }
            return c;
        }
    
        public const string MessageType = "iviz_msgs/ModelMaterial";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "dea645939e7a51f77d59181b714cdac1";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
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
    }
}
