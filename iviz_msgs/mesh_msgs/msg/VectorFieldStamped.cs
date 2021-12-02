/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.MeshMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class VectorFieldStamped : IDeserializable<VectorFieldStamped>, IMessage
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "vector_field")] public MeshMsgs.VectorField VectorField;
    
        /// Constructor for empty message.
        public VectorFieldStamped()
        {
            VectorField = new MeshMsgs.VectorField();
        }
        
        /// Explicit constructor.
        public VectorFieldStamped(in StdMsgs.Header Header, MeshMsgs.VectorField VectorField)
        {
            this.Header = Header;
            this.VectorField = VectorField;
        }
        
        /// Constructor with buffer.
        internal VectorFieldStamped(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            VectorField = new MeshMsgs.VectorField(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new VectorFieldStamped(ref b);
        
        VectorFieldStamped IDeserializable<VectorFieldStamped>.RosDeserialize(ref Buffer b) => new VectorFieldStamped(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            VectorField.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (VectorField is null) throw new System.NullReferenceException(nameof(VectorField));
            VectorField.RosValidate();
        }
    
        public int RosMessageLength => 0 + Header.RosMessageLength + VectorField.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "mesh_msgs/VectorFieldStamped";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "3d9fc2de2c0939ad4bbe0890ccb68ce5";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UTWvcMBC961cM5JDdsnEhKT0s9FbS5lAIJPRSyjJrjW1RWXIleTfur++TvLtNSqA9" +
                "NF0MK1vz3rz5jElv+tjG1x+FtQTqyp/qJXbz989SJx+ujVhNu3LeNPlFqXf/+Kc+3X1YU3wqSJ3RXWKn" +
                "OWjqJbHmxNR4CDVtJ+HCyk4sQNwPoqncpmmQWAF435lIeFpxEtjaicYIo+Sp9n0/OlNzEkoGwT7GA2kc" +
                "MQ0ckqlHywH2PmjjsnkTuJfMjifK91FcLXTzfg0bF6Uek4GgCQx1EI7GtbgkNRqXri4zQJ3d7/0FXqVF" +
                "uk/OKXWcslh5GILErJPjGj5ezcFV4EZyBF50pEX5tsFrXBKcQIIMvu5oAeW3U+q8A6HQjoPhrZVMXCMD" +
                "YD3PoPPlI+Yse02OnT/Sz4y/fPwNrTvx5pguOtTM5ujj2CKBMByC3xkN0+1USGprxCWyZhs4TCqjZpfq" +
                "7DrnGEZAlYrgn2P0tUEBNO1N6lRMIbOXamzMi3Xjs3OgWvHoxTDNN7ce1fzylQYfTTLogt/uZ+QVLOb5" +
                "iS8l9hlZxylAXRMbF0vmj0LJN7nNs13u+CYIKjBwLaqxntPbN/RwOk2n04//I/+QtWMAQfJYoGHQCoc8" +
                "PtVc5YG8KSPkHQawF0ZYmPUTEkBtAqAIvQKrBMEikRWZRNpLJOdzvnr+BkpBPxPQPAwgw1IJ7KLlkjZ8" +
                "BmQhVVutaN8JlkW2yv1YtkfZN6amYFqDdZORcNSfwEyH4FaUmkv0s7Wz5tkZSgSS4FMBLCu6aWjyI+1z" +
                "QDiEw5rztIXEg64yjsn7Vd5xB4pn+iE3dOQWk+tiwoKt1B9q/RMWAZReIAYAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
