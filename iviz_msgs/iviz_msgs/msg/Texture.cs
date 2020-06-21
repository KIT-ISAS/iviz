using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract (Name = "iviz_msgs/Texture")]
    public sealed class Texture : IMessage
    {
        [DataMember (Name = "width")] public uint Width { get; set; }
        [DataMember (Name = "height")] public uint Height { get; set; }
        [DataMember (Name = "bpp")] public uint Bpp { get; set; }
        [DataMember (Name = "data")] public byte[] Data { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Texture()
        {
            Data = System.Array.Empty<byte>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Texture(uint Width, uint Height, uint Bpp, byte[] Data)
        {
            this.Width = Width;
            this.Height = Height;
            this.Bpp = Bpp;
            this.Data = Data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Texture(Buffer b)
        {
            Width = b.Deserialize<uint>();
            Height = b.Deserialize<uint>();
            Bpp = b.Deserialize<uint>();
            Data = b.DeserializeStructArray<byte>();
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new Texture(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(Width);
            b.Serialize(Height);
            b.Serialize(Bpp);
            b.SerializeStructArray(Data, 0);
        }
        
        public void RosValidate()
        {
            if (Data is null) throw new System.NullReferenceException();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 16;
                size += 1 * Data.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "iviz_msgs/Texture";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "cfa9bc58f341a68c382ac43a1da03b79";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAEyvNzCsxNlIoz0wpyeAqhXAyUjPTM0pgvKSCAjDTIjpWISWxJJGLCwDzVI2NNAAAAA==";
                
    }
}
