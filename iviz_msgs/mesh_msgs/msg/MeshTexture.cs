/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract]
    public sealed class MeshTexture : IDeserializable<MeshTexture>, IMessage
    {
        // Mesh Attribute Message
        [DataMember (Name = "uuid")] public string Uuid;
        [DataMember (Name = "texture_index")] public uint TextureIndex;
        [DataMember (Name = "image")] public SensorMsgs.Image Image;
    
        /// Constructor for empty message.
        public MeshTexture()
        {
            Uuid = "";
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
            b.DeserializeString(out Uuid);
            b.Deserialize(out TextureIndex);
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
            if (Uuid is null) BuiltIns.ThrowNullReference();
            if (Image is null) BuiltIns.ThrowNullReference();
            Image.RosValidate();
        }
    
        public int RosMessageLength => 8 + BuiltIns.GetStringSize(Uuid) + Image.RosMessageLength;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "mesh_msgs/MeshTexture";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public string RosMd5Sum => "831d05ad895f7916c0c27143f387dfa0";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE7VVy24bNxTd8ysuoEXsxJKDdhMYKJogr2oRoGiyCwKBM7yaYcshx3zoka/vIamRrNhO" +
                "s2gGsuQhec99nHsPZ/SBQ0+vYvS6SZHza5Adi4AF21FKWomkbfz1F4q8i8nzSlvFOxHYBudXQ+jC9XKA" +
                "Cen8LcRv//MjPnx8f0P33IkZfep1oKEGTK2zUWobSFpKtnXD6LHD6hDWjC6eX9HzS4KJjBTdODe8jjDz" +
                "lj259XROiD9YKiz19efwzOiwHDU8RjmMFHqXjKKGSba3SQcdtbNl/wRHDz1HrLWXA+qp7kC5MepWmrqV" +
                "cVr8evkYkPO60zafqwb3gVq2sSb4faRnu8l4dOAbFaLYMwG/jwQX+eW7OT3bnwMot7U/Zvj13BB/Lrsf" +
                "jbSlBP+JsKxnjuXs2YMUq85XD0ivSxmWdu0eg5taSobgWi0jmmirY3+KIzfb2ug2PoaQTzbcy412Pndc" +
                "wsistWUlpmHquRT2ZFKB6/IVANCkOlyRTUNT6fNuGybrrVaI5551WX7QuHUmDTaIMjVMhju0xkaaxIHW" +
                "iJExMSoPvETlQNpaGzSTb68L8GraDot2HEUp+N4l2sraKJgHq6RX+iuKRpa3dJAPQA8S6fwNYmHmXZin" +
                "wD68NDrEsAgu+ZZxqOOF5Vgow6CrPMM8SG1o9G50oQRWcKdAFmJSqGPkUyneTgtIe9Q7NoHmc2p7aS0b" +
                "cCstNq8wOZjA8l9A2A8TmZmU/zDK4d1QSM1xZ+DqPORSaduapPj6rkJ9W7W+8v4CrKwa3SFFjRQrcwHA" +
                "+FIySjru/T4xHSKPZwG9S8bkXgCHtkMTIIJmH7m2xovPXyrQHYPPf0EBvkCjYgLnoMPrXTlUM88BXBQv" +
                "T0uLXf40BY+qFqeKH9rh46FtQEqUJezcij0mgD3UeQO6itJi/spu3I8cFpPw44NiYZSN2VPKSo9OhO4P" +
                "yUL4cJUdlXqyhyWqJWmUHtqYjPQ4jz7QNh8vOpHR8Ql8m8Ae0/LNTR72wG2KGgHtM92eZWnJ5Rs60sS3" +
                "YvZp6+Z45e7smjhMI/FuupFkuIGPpzW5BbDz9QYvqjCBtRVewyXoySHw6NqeLhD5n/vYuyqoG+m1bExh" +
                "DypvgPokGz25vINsC7SV1k3wFfHk40dg7RE35zTHHCmTsw+pk0XcMKMbrXC02ReQ1mh0HEal8dLvRbkQ" +
                "i0sxe1cuqVO/58v4XGSnsZ5UW4h/AUvlkUajCAAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public void Dispose()
        {
        }
    }
}
