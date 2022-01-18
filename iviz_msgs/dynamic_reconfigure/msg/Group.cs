/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.DynamicReconfigure
{
    [Preserve, DataContract (Name = RosMessageType)]
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
            Name = string.Empty;
            Type = string.Empty;
            Parameters = System.Array.Empty<ParamDescription>();
        }
        
        /// Explicit constructor.
        public Group(string Name, string Type, ParamDescription[] Parameters, int Parent, int Id)
        {
            this.Name = Name;
            this.Type = Type;
            this.Parameters = Parameters;
            this.Parent = Parent;
            this.Id = Id;
        }
        
        /// Constructor with buffer.
        public Group(ref ReadBuffer b)
        {
            Name = b.DeserializeString();
            Type = b.DeserializeString();
            Parameters = b.DeserializeArray<ParamDescription>();
            for (int i = 0; i < Parameters.Length; i++)
            {
                Parameters[i] = new ParamDescription(ref b);
            }
            Parent = b.Deserialize<int>();
            Id = b.Deserialize<int>();
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
            if (Name is null) throw new System.NullReferenceException(nameof(Name));
            if (Type is null) throw new System.NullReferenceException(nameof(Type));
            if (Parameters is null) throw new System.NullReferenceException(nameof(Parameters));
            for (int i = 0; i < Parameters.Length; i++)
            {
                if (Parameters[i] is null) throw new System.NullReferenceException($"{nameof(Parameters)}[{i}]");
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
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "dynamic_reconfigure/Group";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "9e8cd9e9423c94823db3614dd8b1cf7a";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAEysuKcrMS1fIS8xN5SqGsEsqC1K5AhKLEnNdUouTizILSjLz86JjFQpAQqklqUXFXJl5" +
                "JcZGIIHUvBIFKC8zhYvLlsqAyzfY3UohpRLovMzk+KLU5Py8tMz00qJUfXT3wRyP4ZFSiOtyUstSc2AS" +
                "KZj6UlMyS+KBvsvIB3oDAFF6cKsVAQAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
