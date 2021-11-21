/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.DynamicReconfigure
{
    [Preserve, DataContract (Name = "dynamic_reconfigure/ParamDescription")]
    public sealed class ParamDescription : IDeserializable<ParamDescription>, IMessage
    {
        [DataMember (Name = "name")] public string Name;
        [DataMember (Name = "type")] public string Type;
        [DataMember (Name = "level")] public uint Level;
        [DataMember (Name = "description")] public string Description;
        [DataMember (Name = "edit_method")] public string EditMethod;
    
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
        internal ParamDescription(ref Buffer b)
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
                size += BuiltIns.GetStringSize(Name);
                size += BuiltIns.GetStringSize(Type);
                size += BuiltIns.GetStringSize(Description);
                size += BuiltIns.GetStringSize(EditMethod);
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
                "H4sIAAAAAAAACisuKcrMS1fIS8xN5SqGsEsqC1K5SjPzSoyNFHJSy1JzYBIpqcXJRZkFJZn5eTCh1JTM" +
                "kvjc1JKM/BQuXi4Aq2b7uE0AAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
