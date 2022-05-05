/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.DynamicReconfigure
{
    [DataContract]
    public sealed class ParamDescription : IDeserializable<ParamDescription>, IMessage
    {
        [DataMember (Name = "name")] public string Name;
        [DataMember (Name = "type")] public string Type;
        [DataMember (Name = "level")] public uint Level;
        [DataMember (Name = "description")] public string Description;
        [DataMember (Name = "edit_method")] public string EditMethod;
    
        /// Constructor for empty message.
        public ParamDescription()
        {
            Name = "";
            Type = "";
            Description = "";
            EditMethod = "";
        }
        
        /// Constructor with buffer.
        public ParamDescription(ref ReadBuffer b)
        {
            b.DeserializeString(out Name);
            b.DeserializeString(out Type);
            b.Deserialize(out Level);
            b.DeserializeString(out Description);
            b.DeserializeString(out EditMethod);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new ParamDescription(ref b);
        
        public ParamDescription RosDeserialize(ref ReadBuffer b) => new ParamDescription(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Name);
            b.Serialize(Type);
            b.Serialize(Level);
            b.Serialize(Description);
            b.Serialize(EditMethod);
        }
        
        public void RosValidate()
        {
            if (Name is null) BuiltIns.ThrowNullReference();
            if (Type is null) BuiltIns.ThrowNullReference();
            if (Description is null) BuiltIns.ThrowNullReference();
            if (EditMethod is null) BuiltIns.ThrowNullReference();
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
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "dynamic_reconfigure/ParamDescription";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public string RosMd5Sum => "7434fcb9348c13054e0c3b267c8cb34d";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAEysuKcrMS1fIS8xN5SqGsEsqC1K5SjPzSoyNFHJSy1JzYBIpqcXJRZkFJZn5eTCh1JTM" +
                "kvjc1JKM/BQuLgC4Qc3XTAAAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
