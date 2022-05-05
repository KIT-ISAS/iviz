/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.DynamicReconfigure
{
    [DataContract]
    public sealed class IntParameter : IDeserializable<IntParameter>, IMessage
    {
        [DataMember (Name = "name")] public string Name;
        [DataMember (Name = "value")] public int Value;
    
        /// Constructor for empty message.
        public IntParameter()
        {
            Name = "";
        }
        
        /// Explicit constructor.
        public IntParameter(string Name, int Value)
        {
            this.Name = Name;
            this.Value = Value;
        }
        
        /// Constructor with buffer.
        public IntParameter(ref ReadBuffer b)
        {
            b.DeserializeString(out Name);
            b.Deserialize(out Value);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new IntParameter(ref b);
        
        public IntParameter RosDeserialize(ref ReadBuffer b) => new IntParameter(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Name);
            b.Serialize(Value);
        }
        
        public void RosValidate()
        {
            if (Name is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 8 + BuiltIns.GetStringSize(Name);
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "dynamic_reconfigure/IntParameter";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public string RosMd5Sum => "65fedc7a0cbfb8db035e46194a350bf1";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAEysuKcrMS1fIS8xN5crMKzE2UihLzClN5eICAPL2vOMZAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
