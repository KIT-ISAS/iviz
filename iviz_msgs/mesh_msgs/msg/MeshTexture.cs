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
        internal MeshTexture(ref Buffer b)
        {
            Uuid = b.DeserializeString();
            TextureIndex = b.Deserialize<uint>();
            Image = new SensorMsgs.Image(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new MeshTexture(ref b);
        
        MeshTexture IDeserializable<MeshTexture>.RosDeserialize(ref Buffer b) => new MeshTexture(ref b);
    
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
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "mesh_msgs/MeshTexture";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "831d05ad895f7916c0c27143f387dfa0";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACrVVTW8bNxC9768YQIfYiSUH7SUwELRF0rQ6BCiQ3IpC4C5Hu2y45Jofkja/vo+kKFlx" +
                "nObQLmTJS3LefLw3wwW9Zz/QLyE41cbA6dWLnhuPBdNTjEo2UZnw4w8U+BCi440ykg+NZ+Ot24y+97fr" +
                "ESak0nfTvP6Pn+b9h9/u6JG7ZkEfB+VpLAFTZ00QyngShqLp7Dg57LA8hrWgq5c39PKaYCICBTstNW8D" +
                "zJxhR3ZbzzXN7ywklobyc3wWdFwOCh6DGCfyg41aUsskuvuovArKmrx/hqvmF88Ja+vEiHrKB1B2CqoT" +
                "umwlnA6/TjwFZJ3qlUnnisFjoI5NKAl+G+nFoRpPFnyjQhQGJuAPgeAivXwzpxfzJYC0e/N9hp8vDfEH" +
                "35YmLUwuwb8irMuZUzkHdiDFyMvVI9KbXIa12dqn4KqkhPe2UyJARHsVhnMcSWxbrbrwFEI62fIgdsq6" +
                "pLiIltkqw7KpzTRwLuzZpACX5RsAQKTK35CJY1voc3bvq/VeScTzyDovf9W4szqOxje5a5g095DGTujI" +
                "nraIkdExMjW8QOVA2lZpiMl1txl4U7f9qpumJhd8tpH2oggF/WCkcFJ9RtHI8J6O4wPQo0A6f4NYmDnr" +
                "l9Gz8z9r5YNfeRtdxzjU88pwyJSh0WXqYR6F0jQ5O1mfA8u4NZBVUyfUKfJail/rAtKe1IG1p+WSukEY" +
                "wxrcCoPNG3QOOjD/5xH214lMTIpPjHI4O2ZSU9wJuDj3qVTKdDpKvn04ob6s2lB4fwVWNq3qkaJCioU5" +
                "D2B8SREEnfZ+qkz7wNNFQO+i1kkL4ND0EAEiaOfARRqv/vyrAD0wEF2IIBs8OHXIuyXl5Pkqwz/P2rr+" +
                "30Z3kKUqZepBBx+OegEbQeR4kwYHSJ8dxvIOPOURi8bLu2Ge2K/qxMcHVUIPaz0T5IQ2txD4OEaDiYc7" +
                "7DSiqz0sUSZBk3AYilELh/MQgDLpeB4QCR0fz/cRtDGt396lLvfcxaAQ0Jx4diyyFtdv6cQP3zeLj3u7" +
                "xCv3F/fDsQ2JD/UqEv4OPp6X5FbATvcavMjMBNY2ePXXoMcgBJ5sN9AVIv9jDkO6WiDAnXBKtGhOAGO8" +
                "a6A+S0bPrh8gp7DvyAhjK3xBPPv4HtiEUnBTTks0kNQpex97FBAH0Zw7JXG0nTNIpxUuG/RI64Sbm3wT" +
                "ZpfN4l2+nc5CT7fw5XSt/VzHddP8A+sDVU+cCAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
