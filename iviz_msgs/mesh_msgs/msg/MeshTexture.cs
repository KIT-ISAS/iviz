/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class MeshTexture : IDeserializable<MeshTexture>, IMessage
    {
        // Mesh Attribute Message
        [DataMember (Name = "uuid")] public string Uuid;
        [DataMember (Name = "texture_index")] public uint TextureIndex;
        [DataMember (Name = "image")] public SensorMsgs.Image Image;
    
        /// Constructor for empty message.
        public MeshTexture()
        {
            Uuid = string.Empty;
            Image = new SensorMsgs.Image();
        }
        
        /// Explicit constructor.
        public MeshTexture(string Uuid, uint TextureIndex, SensorMsgs.Image Image)
        {
            this.Uuid = Uuid;
            this.TextureIndex = TextureIndex;
            this.Image = Image;
        }
        
        /// Constructor with buffer.
        public MeshTexture(ref ReadBuffer b)
        {
            Uuid = b.DeserializeString();
            TextureIndex = b.Deserialize<uint>();
            Image = new SensorMsgs.Image(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new MeshTexture(ref b);
        
        public MeshTexture RosDeserialize(ref ReadBuffer b) => new MeshTexture(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
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
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "mesh_msgs/MeshTexture";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "831d05ad895f7916c0c27143f387dfa0";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE7VVTW8bNxA9l79iAB1iJ5YctJfCQNAWTdPqEKBocgsCgVqOdtlyyTU/JG1+fR5JrT5q" +
                "O8mhXciSl+S8+XgzjzN6y6GjX2L0ep0i59cgWxYBC7allLQSSdv4w/cUeR+T55W2ivcisA3Or/rQhttl" +
                "DxPS+VuIV//xI96++/2OHrgTM3rf6UB9DZgaZ6PUNpC0lGzj+sFjh9UhrBldvbyhl9cEExkpumFueBNh" +
                "5i17cpvpnBB/sFRY6urP4ZnRYTlqeIyyHyh0LhlFaybZ3CcddNTOlv0THD32HLE2XvaopzqDckPUjTR1" +
                "K+M0+PXyKSDndattPlcNHgI1bGNN8MtIL/aT8eDANypEsWMCfhcJLvLLF3N6MV4CKLez32b46dIQfy67" +
                "H4y0pQRfRVjWM8dyduxBilWXqwekX0sZlnbjnoKbWkqG4BotI5pop2N3iiM328boJj6FkE+uuZNb7Xzu" +
                "uISR2WjLSkzD1HEp7MmkAtflGwCgSXW4IZv6daXPu12YrHdaIZ4H1mX5UePGmdTbIMrUMBlu0RpbaRIH" +
                "2iBGxsSoPPASlQNpG23QTL65LcCraTssmmEQpeCjS7STtVEwD1ZJr/QnFI0s7+ggH4DuJdL5G8TCzLsw" +
                "T4F9+NnoEMMiuOQbxqGWF5ZjoQyDrvIMcy+1ocG7wYUSWMGdAlmISaGOkU+l+G1aQNqD3rMJNJ9T00lr" +
                "2YBbabF5g8nBBJb/AsJ+nMjMpPyHUQ7v+kJqjjsDV+chl0rbxiTFt+cK9e+qdZX3H8HKaq1bpKiRYmUu" +
                "ABhfSkZJx72fJqZD5OEioDfJmNwL4NC2aAJEsB4jBzErHj58rEhnFrKJCWyDCK/3ZbfmnF1fFfznpbmu" +
                "xTnCd5P5h7+gIB//N12PqpasSiKa5N2hmUBVlCWX3KAd5oI9NHsLEov+YirLbhwHDovpOsAHJcSAGzNS" +
                "yvqP/sRt0CcLOcQFd9TvyR6WqKGkQXooZjLS4zy6Q9t8vKhHRscn8H0Cp0zL13dZAgI3KWoENOYm8CxL" +
                "oy5f05E8vhez9zs3xyu3F5fHYUaJ99M9JcMdfDyvyS2AnS89eFGFJayt8BquQV0OgQfXdHSFyP8cY+eq" +
                "zG6l13JtCrPQfgPUZ9no2fUZsi3QVlo3wVfEk49vgbVH3JzTHNOlTM4+pFYWycPkbrXC0fVYQBqj0UcY" +
                "oLWXfhTlmiwuxexNubpOU5Cv6EvpnYZ90nIhPgNN7mC8uQgAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
