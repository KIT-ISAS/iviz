/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.DynamicReconfigure
{
    [Preserve, DataContract (Name = "dynamic_reconfigure/Group")]
    public sealed class Group : IDeserializable<Group>, IMessage
    {
        [DataMember (Name = "name")] public string Name;
        [DataMember (Name = "type")] public string Type;
        [DataMember (Name = "parameters")] public ParamDescription[] Parameters;
        [DataMember (Name = "parent")] public int Parent;
        [DataMember (Name = "id")] public int Id;
    
        /// <summary> Constructor for empty message. </summary>
        public Group()
        {
            Name = string.Empty;
            Type = string.Empty;
            Parameters = System.Array.Empty<ParamDescription>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Group(string Name, string Type, ParamDescription[] Parameters, int Parent, int Id)
        {
            this.Name = Name;
            this.Type = Type;
            this.Parameters = Parameters;
            this.Parent = Parent;
            this.Id = Id;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Group(ref Buffer b)
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
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Group(ref b);
        }
        
        Group IDeserializable<Group>.RosDeserialize(ref Buffer b)
        {
            return new Group(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Name);
            b.Serialize(Type);
            b.SerializeArray(Parameters, 0);
            b.Serialize(Parent);
            b.Serialize(Id);
        }
        
        public void Dispose()
        {
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
                size += BuiltIns.UTF8.GetByteCount(Name);
                size += BuiltIns.UTF8.GetByteCount(Type);
                foreach (var i in Parameters)
                {
                    size += i.RosMessageLength;
                }
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "dynamic_reconfigure/Group";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "9e8cd9e9423c94823db3614dd8b1cf7a";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAEysuKcrMS1fIS8xN5SqGsEsqC1K5AhKLEnNdUouTizILSjLz86JjFQpAQqklqUXFXJl5" +
                "JcZGIIHUvBIFKC8zhYvLlsqAyzfY3UohpRLovMzk+KLU5Py8tMz00qJUfXT3wRyP4ZFSiOtyUstSc2AS" +
                "KZj6UlMyS+KBvsvIB3oDAFF6cKsVAQAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
