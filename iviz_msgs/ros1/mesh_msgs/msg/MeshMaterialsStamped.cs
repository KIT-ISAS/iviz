/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [DataContract]
    public sealed class MeshMaterialsStamped : IDeserializable<MeshMaterialsStamped>, IMessage
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
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.DeserializeString(out Uuid);
            MeshMaterials = new MeshMsgs.MeshMaterials(ref b);
        }
        
        public MeshMaterialsStamped(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.DeserializeString(out Uuid);
            MeshMaterials = new MeshMsgs.MeshMaterials(ref b);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new MeshMaterialsStamped(ref b);
        
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
            b.Serialize(Uuid);
            MeshMaterials.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Uuid is null) BuiltIns.ThrowNullReference();
            if (MeshMaterials is null) BuiltIns.ThrowNullReference();
            MeshMaterials.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += Header.RosMessageLength;
                size += WriteBuffer.GetStringSize(Uuid);
                size += MeshMaterials.RosMessageLength;
                return size;
            }
        }
        
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            Header.AddRos2MessageLength(ref c);
            WriteBuffer2.AddLength(ref c, Uuid);
            MeshMaterials.AddRos2MessageLength(ref c);
        }
    
        public const string MessageType = "mesh_msgs/MeshMaterialsStamped";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "80683ad6336327fea277cb1ed5f58927";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
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
    }
}
