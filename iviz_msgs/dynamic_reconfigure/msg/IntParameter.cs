/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.DynamicReconfigure
{
    [Preserve, DataContract (Name = RosMessageType)]
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
            Name = b.DeserializeString();
            Value = b.Deserialize<int>();
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
            if (Name is null) throw new System.NullReferenceException(nameof(Name));
        }
    
        public int RosMessageLength => 8 + BuiltIns.GetStringSize(Name);
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "dynamic_reconfigure/IntParameter";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "65fedc7a0cbfb8db035e46194a350bf1";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAEysuKcrMS1fIS8xN5crMKzE2UihLzClN5eICAPL2vOMZAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
