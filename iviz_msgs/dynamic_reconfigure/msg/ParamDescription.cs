/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.DynamicReconfigure
{
    [Preserve, DataContract (Name = "dynamic_reconfigure/ParamDescription")]
    public sealed class ParamDescription : IDeserializable<ParamDescription>, IMessage
    {
        [DataMember (Name = "name")] public string Name { get; set; }
        [DataMember (Name = "type")] public string Type { get; set; }
        [DataMember (Name = "level")] public uint Level { get; set; }
        [DataMember (Name = "description")] public string Description { get; set; }
        [DataMember (Name = "edit_method")] public string EditMethod { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public ParamDescription()
        {
            Name = string.Empty;
            Type = string.Empty;
            Description = string.Empty;
            EditMethod = string.Empty;
        }
        
        /// <summary> Explicit constructor. </summary>
        public ParamDescription(string Name, string Type, uint Level, string Description, string EditMethod)
        {
            this.Name = Name;
            this.Type = Type;
            this.Level = Level;
            this.Description = Description;
            this.EditMethod = EditMethod;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public ParamDescription(ref Buffer b)
        {
            Name = b.DeserializeString();
            Type = b.DeserializeString();
            Level = b.Deserialize<uint>();
            Description = b.DeserializeString();
            EditMethod = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new ParamDescription(ref b);
        }
        
        ParamDescription IDeserializable<ParamDescription>.RosDeserialize(ref Buffer b)
        {
            return new ParamDescription(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Name);
            b.Serialize(Type);
            b.Serialize(Level);
            b.Serialize(Description);
            b.Serialize(EditMethod);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Name is null) throw new System.NullReferenceException(nameof(Name));
            if (Type is null) throw new System.NullReferenceException(nameof(Type));
            if (Description is null) throw new System.NullReferenceException(nameof(Description));
            if (EditMethod is null) throw new System.NullReferenceException(nameof(EditMethod));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 20;
                size += BuiltIns.UTF8.GetByteCount(Name);
                size += BuiltIns.UTF8.GetByteCount(Type);
                size += BuiltIns.UTF8.GetByteCount(Description);
                size += BuiltIns.UTF8.GetByteCount(EditMethod);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "dynamic_reconfigure/ParamDescription";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "7434fcb9348c13054e0c3b267c8cb34d";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAEysuKcrMS1fIS8xN5SqGsEsqC1K5SjPzSoyNFHJSy1JzYBIpqcXJRZkFJZn5eTCh1JTM" +
                "kvjc1JKM/BQuLgC4Qc3XTAAAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}