/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.DynamicReconfigure
{
    [DataContract]
    public sealed class Group : IDeserializable<Group>, IMessage
    {
        [DataMember (Name = "name")] public string Name;
        [DataMember (Name = "type")] public string Type;
        [DataMember (Name = "parameters")] public ParamDescription[] Parameters;
        [DataMember (Name = "parent")] public int Parent;
        [DataMember (Name = "id")] public int Id;
    
        /// Constructor for empty message.
        public Group()
        {
            Name = "";
            Type = "";
            Parameters = System.Array.Empty<ParamDescription>();
        }
        
        /// Constructor with buffer.
        public Group(ref ReadBuffer b)
        {
            b.DeserializeString(out Name);
            b.DeserializeString(out Type);
            b.DeserializeArray(out Parameters);
            for (int i = 0; i < Parameters.Length; i++)
            {
                Parameters[i] = new ParamDescription(ref b);
            }
            b.Deserialize(out Parent);
            b.Deserialize(out Id);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new Group(ref b);
        
        public Group RosDeserialize(ref ReadBuffer b) => new Group(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Name);
            b.Serialize(Type);
            b.SerializeArray(Parameters);
            b.Serialize(Parent);
            b.Serialize(Id);
        }
        
        public void RosValidate()
        {
            if (Name is null) BuiltIns.ThrowNullReference();
            if (Type is null) BuiltIns.ThrowNullReference();
            if (Parameters is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Parameters.Length; i++)
            {
                if (Parameters[i] is null) BuiltIns.ThrowNullReference(nameof(Parameters), i);
                Parameters[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 20;
                size += BuiltIns.GetStringSize(Name);
                size += BuiltIns.GetStringSize(Type);
                size += BuiltIns.GetArraySize(Parameters);
                return size;
            }
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "dynamic_reconfigure/Group";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "9e8cd9e9423c94823db3614dd8b1cf7a";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAEysuKcrMS1fIS8xN5SqGsEsqC1K5AhKLEnNdUouTizILSjLz86JjFQpAQqklqUXFXJl5" +
                "JcZGIIHUvBIFKC8zhYvLlsqAyzfY3UohpRLovMzk+KLU5Py8tMz00qJUfXT3wRyP4ZFSiOtyUstSc2AS" +
                "KZj6UlMyS+KBvsvIB3oDAFF6cKsVAQAA";
                
    
        public override string ToString() => Extensions.ToString(this);
    }
}
