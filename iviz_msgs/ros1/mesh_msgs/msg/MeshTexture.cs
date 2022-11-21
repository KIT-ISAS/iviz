/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract]
    public sealed class MeshTexture : IHasSerializer<MeshTexture>, System.IDisposable, IMessage
    {
        // Mesh Attribute Message
        [DataMember (Name = "uuid")] public string Uuid;
        [DataMember (Name = "texture_index")] public uint TextureIndex;
        [DataMember (Name = "image")] public SensorMsgs.Image Image;
    
        public MeshTexture()
        {
            Uuid = "";
            Image = new SensorMsgs.Image();
        }
        
        public MeshTexture(string Uuid, uint TextureIndex, SensorMsgs.Image Image)
        {
            this.Uuid = Uuid;
            this.TextureIndex = TextureIndex;
            this.Image = Image;
        }
        
        public MeshTexture(ref ReadBuffer b)
        {
            Uuid = b.DeserializeString();
            b.Deserialize(out TextureIndex);
            Image = new SensorMsgs.Image(ref b);
        }
        
        public MeshTexture(ref ReadBuffer2 b)
        {
            b.Align4();
            Uuid = b.DeserializeString();
            b.Align4();
            b.Deserialize(out TextureIndex);
            Image = new SensorMsgs.Image(ref b);
        }
        
        public MeshTexture RosDeserialize(ref ReadBuffer b) => new MeshTexture(ref b);
        
        public MeshTexture RosDeserialize(ref ReadBuffer2 b) => new MeshTexture(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Uuid);
            b.Serialize(TextureIndex);
            Image.RosSerialize(ref b);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align4();
            b.Serialize(Uuid);
            b.Align4();
            b.Serialize(TextureIndex);
            Image.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Uuid is null) BuiltIns.ThrowNullReference(nameof(Uuid));
            if (Image is null) BuiltIns.ThrowNullReference(nameof(Image));
            Image.RosValidate();
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 8;
                size += WriteBuffer.GetStringSize(Uuid);
                size += Image.RosMessageLength;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Uuid);
            size = WriteBuffer2.Align4(size);
            size += 4; // TextureIndex
            size = Image.AddRos2MessageLength(size);
            return size;
        }
    
        public const string MessageType = "mesh_msgs/MeshTexture";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "831d05ad895f7916c0c27143f387dfa0";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE7VVTW8bNxC9768YQIfYiSWn7SUwULRF07Q+BCia3AJDoJajXbZccs0h9ZFf30dSK1m1" +
                "nebQLmTJS3LefLyZxxm9Z+nppxiDWaXI+VVUx41gwXWUktFNMi5+9y1F3sUUeGmc5l0j7MSH5SCdXN8O" +
                "MCGTv5vm+//4ad5/+PWGHrlrZvSxN0JDDZha76IyTkg5Sq71wxiww/oQ1owuXl/R60uCiYoU/Ti3vI4w" +
                "C44D+fV0rml+Y6Wx1NefwzOjw3I08BjVMJL0PllNKybV3icjJhrvyv4Jjp56jljroAbUUz+A8mM0rbJ1" +
                "K+O0+A3qOSAfTGdcPlcNHgO17GJN8MtIr3aT8ejBNypEsWcCfh8JLvLLF3N6tT8H0H7rvs7w87kh/nx2" +
                "P1rlSgn+FeG2njmWs+cAUpw+Xz0g/VzKcOvW/jm4qaWUiG+NimiirYn9KY7cbGtr2vgcQj654l5tjA+5" +
                "4xJGZm0c62Yapp5LYU8mFbguXwEATWrkilwaVpW+4LcyWW+NRjyPrMvyk8att2lw0pSpYbLcoTU2yiYW" +
                "WiNGxsToPPAKlQNpa2PRTKG9LsDLaVsW7Tg2peB7n2iraqNgHpxWQZvPKBo53tJBPgA9KKTzJ4iFWfAy" +
                "T8JBfrRGoizEp9AyDnW8cBwLZRh0nWeYB2UsjcGPXkpgBXcKZNFMCnWMfCrFL9MC0h7Njq3QfE5tr5xj" +
                "C26Vw+YVJgcTWP4ThP00kZlJ9RejHMEPhdQcdwauziWXyrjWJs3XDxXqn1XrK+9vwMpyZTqkaJBiZU4A" +
                "jC+toqLj3g8T0xJ5PAvoXbI29wI4dB2aABGs9pFra7z5dFeBHhh8+gMKcAeNigmcg45gduVQzTwHcFG8" +
                "vCwtdvm/KXjUtThV/NAOHw5tA1KiKmHnVuwxARygzhvQVZQW81d2435kWUzCjw+KhVG2dk8pKz06Ebo/" +
                "JAfhw1V2VOrJHpaolqJRBWhjsirgPPrAuHy86ERGx0f4PoE9ptu3N3nYhdsUDQLaZ7oDq9KSt2/pSBPf" +
                "l1J7+eaumX3c+jnWuTu7Lw5jSbybriYlN3D2sma5gJN8z8GdLpRgbYlXuQRPORYefdvTBVL4fR97X5V1" +
                "o4JRK1tohNxboL7IRi8uHyC7Au2U8xN8RTz5+BpYd8TNOc0xUNrmMkjqVFE5DOvGaBxd7QtIaw1aDzOz" +
                "Cirsm3IzFpfN7F25rU6Nn2/lc7Wd5nuS76b5G6U8ZHKsCAAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public void Dispose()
        {
            Image.Dispose();
        }
    
        public Serializer<MeshTexture> CreateSerializer() => new Serializer();
        public Deserializer<MeshTexture> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<MeshTexture>
        {
            public override void RosSerialize(MeshTexture msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(MeshTexture msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(MeshTexture msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(MeshTexture msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(MeshTexture msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<MeshTexture>
        {
            public override void RosDeserialize(ref ReadBuffer b, out MeshTexture msg) => msg = new MeshTexture(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out MeshTexture msg) => msg = new MeshTexture(ref b);
        }
    }
}
