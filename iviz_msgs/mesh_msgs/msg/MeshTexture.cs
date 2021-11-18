/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [Preserve, DataContract (Name = "mesh_msgs/MeshTexture")]
    public sealed class MeshTexture : IDeserializable<MeshTexture>, IMessage
    {
        // Mesh Attribute Message
        [DataMember (Name = "uuid")] public string Uuid;
        [DataMember (Name = "texture_index")] public uint TextureIndex;
        [DataMember (Name = "image")] public SensorMsgs.Image Image;
    
        /// <summary> Constructor for empty message. </summary>
        public MeshTexture()
        {
            Uuid = string.Empty;
            Image = new SensorMsgs.Image();
        }
        
        /// <summary> Explicit constructor. </summary>
        public MeshTexture(string Uuid, uint TextureIndex, SensorMsgs.Image Image)
        {
            this.Uuid = Uuid;
            this.TextureIndex = TextureIndex;
            this.Image = Image;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal MeshTexture(ref Buffer b)
        {
            Uuid = b.DeserializeString();
            TextureIndex = b.Deserialize<uint>();
            Image = new SensorMsgs.Image(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new MeshTexture(ref b);
        }
        
        MeshTexture IDeserializable<MeshTexture>.RosDeserialize(ref Buffer b)
        {
            return new MeshTexture(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Uuid);
            b.Serialize(TextureIndex);
            Image.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Uuid is null) throw new System.NullReferenceException(nameof(Uuid));
            if (Image is null) throw new System.NullReferenceException(nameof(Image));
            Image.RosValidate();
        }
    
        public int RosMessageLength => 8 + BuiltIns.GetStringSize(Uuid) + Image.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "mesh_msgs/MeshTexture";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "831d05ad895f7916c0c27143f387dfa0";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACrVVTW8bNxC9L+D/MIAOsRNLDtpLYKBoi6ZpdQhQILkVhcBdjnbZcsk1PyRtfn0fSVGy" +
                "6jjNoV3IkpfkvPl4b4YLes9+oB9DcKqNgdOrFz03HgumpxiVbKIy4dtvKPAhRMcbZSQfGs/GW7cZfe/v" +
                "1iNMSKXv5qr57j9+rpr3H365pycOr5oFfRyUp7HETJ01QSjjSRiKprPj5LDD8hjZgq5f39LrG4KJCBTs" +
                "tNS8DTBzhh3ZbT3XNL+ykFgays/xWdBxOSh4DGKcyA82akktk+geovIqKGvy/hmuml88J6ytEyNKKh9B" +
                "2SmoTuiylXA6/DrxHJB1qlcmnSsGT4E6NqEk+GWkV4dqPFlQjgpRGJiAPwSCi/TyxZxezZcA0u7N1xl+" +
                "ujTEH3xbmrQwuQT/irAuZ07lHNiBFCMvV49IP+UyrM3WPgdXJSW8t50SASLaqzCc40hi22rVhecQ0smW" +
                "B7FT1iXFRXTNVhmWTe2ngXNhzyYFuCzfAgAiVf6WTBzbQp+ze1+t90oinifWefmzxp3VcTS+yV3DpLmH" +
                "NHZCR/a0RYyMjpGp5wUqB9K2SkNMrrvLwJu67VfdNDW54LONtBdFKOgHI4WT6hOKRob3dJwggB4F0vkT" +
                "xMLMWb+Mnp3/QSsf/Mrb6DrGoZ5XhkOmDJ0uUw/zKJSmydnJ+hxYxq2BrJo6pE6R11L8XBeQ9qQOrD0t" +
                "l9QNwhjW4FYYbN6ic9CB+T+PsD9PZGJS/MUoh7NjJjXFnYCLc59KpUyno+S7xyPqn1UbCu9vwMqmVT1S" +
                "VEixMOcBjC8pgqDT3veVaR94ugjoXdQ6aQEcmh4iQATtHLhI483vfxSgRwaiCxFkgwenDnm3pJw8X2f4" +
                "l1lbN//j9A6y1KXMvTS7PxwlA0KCyCEnGQ5QPztM5h2oylMWvZd3wzyxX9Whjw8KhTbWeiYoCp1uofFx" +
                "jAZDDzfZaUpXe1iiUoIm4TAXoxYO56EBZdLxPCMSOj6eHyKYY1q/vU+N7rmLQSGgOVHtWGQ5rt/SiSJ+" +
                "aBYf93aJV+4vrohjJxIf6m0k/D18vCzJrYCd7jZ4kZkMrG3w6m/AkEEIPNluoGtE/tschnS7QIM74ZRo" +
                "0Z8AxoTXQH2RjF7cPEJOYd+TEcZW+IJ49vE1sAml4KaclughqVP2PvYoIA6iP3dK4mg7Z5BOK9w3aJPW" +
                "CTc3+TLMLpvFu3xBnbWeLuLLAVtbuk5sCPJvXotRtKMIAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
