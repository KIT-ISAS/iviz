/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract]
    public sealed class MeshMaterialsStamped : IHasSerializer<MeshMaterialsStamped>, IMessage
    {
        // Mesh Attribute Message
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "uuid")] public string Uuid;
        [DataMember (Name = "mesh_materials")] public MeshMsgs.MeshMaterials MeshMaterials;
    
        public MeshMaterialsStamped()
        {
            Uuid = "";
            MeshMaterials = new MeshMsgs.MeshMaterials();
        }
        
        public MeshMaterialsStamped(in StdMsgs.Header Header, string Uuid, MeshMsgs.MeshMaterials MeshMaterials)
        {
            this.Header = Header;
            this.Uuid = Uuid;
            this.MeshMaterials = MeshMaterials;
        }
        
        public MeshMaterialsStamped(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Uuid = b.DeserializeString();
            MeshMaterials = new MeshMsgs.MeshMaterials(ref b);
        }
        
        public MeshMaterialsStamped(ref ReadBuffer2 b)
        {
            Header = new StdMsgs.Header(ref b);
            b.Align4();
            Uuid = b.DeserializeString();
            MeshMaterials = new MeshMsgs.MeshMaterials(ref b);
        }
        
        public MeshMaterialsStamped RosDeserialize(ref ReadBuffer b) => new MeshMaterialsStamped(ref b);
        
        public MeshMaterialsStamped RosDeserialize(ref ReadBuffer2 b) => new MeshMaterialsStamped(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Uuid);
            MeshMaterials.RosSerialize(ref b);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Align4();
            b.Serialize(Uuid);
            MeshMaterials.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            Header.RosValidate();
            MeshMaterials.RosValidate();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                size += Header.RosMessageLength;
                size += WriteBuffer.GetStringSize(Uuid);
                size += MeshMaterials.RosMessageLength;
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Header.AddRos2MessageLength(size);
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Uuid);
            size = MeshMaterials.AddRos2MessageLength(size);
            return size;
        }
    
        public const string MessageType = "mesh_msgs/MeshMaterialsStamped";
    
        [IgnoreDataMember] public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "80683ad6336327fea277cb1ed5f58927";
    
        [IgnoreDataMember] public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        [IgnoreDataMember]
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71UTWvbQBC9768Y0CFJISltb4YeUoekORhKYnoJwYyksbSw3lX3w7H+fd/KteykGHqo" +
                "Y2Q0u5r39s3XFjST0NJ1jF6XKUpeBm5EhVgvVqEJH78L1+KpHV7Y9to2lJKu1QrIrU/mmHEUr9kE2u7v" +
                "lkp9/c8/NXu8m9Abgaqgx8i2Zl9DQOSaI9PSQbhuWvGXRtZiAOJVJzUNX2PfSbgCcN7qQHgaseLZmJ5S" +
                "gFN0VLnVKlldIRiKGoEd4oHUlpg69lFXybCHv/O1ttl96XklmR1PkF9JbCV0fzOBjw1SpaghqAdD5YVD" +
                "Tur9DamkbfzyOQOooKcHFz49q2L+4i6xLw3qMKqg2HLMqmXTeRQNqjhMcNiHbZRXOARZEhxXBzof9hZY" +
                "hgvCadAinataOkcIP/rYOgtCoTWjaKWRTFwhFWA9y6CziwNmO1Bbtm5Hv2Xcn/EvtHbkzTFdtiieyWkI" +
                "qUEm4dh5t9Y1XMt+IKmMFhvJ6NKz71VGbY9UxW1ONpyAGkqDN4fgKo1K1PSiY7vr3KEsC3TvidryyFAg" +
                "yiOD9hpwy5VMTQqAPT0j4sEKR0YNHvsx23bOHnQwga/RP8VH2cxlM83NGoBYDzuL/B8a+GQzezRWVeyM" +
                "MYwlvi60rXUlyJ7ronaWza6Mhksx71vD3WwiTTH5QZts9vfk1BnnH+6+XWPAYanSOUMth8Uf/5Pfg6MA" +
                "tTSOs1I/Ws1olaPF75O+N+329yDMcQ2PotJorZX6DSQDhsadBgAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<MeshMaterialsStamped> CreateSerializer() => new Serializer();
        public Deserializer<MeshMaterialsStamped> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<MeshMaterialsStamped>
        {
            public override void RosSerialize(MeshMaterialsStamped msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(MeshMaterialsStamped msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(MeshMaterialsStamped msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(MeshMaterialsStamped msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(MeshMaterialsStamped msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<MeshMaterialsStamped>
        {
            public override void RosDeserialize(ref ReadBuffer b, out MeshMaterialsStamped msg) => msg = new MeshMaterialsStamped(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out MeshMaterialsStamped msg) => msg = new MeshMaterialsStamped(ref b);
        }
    }
}
