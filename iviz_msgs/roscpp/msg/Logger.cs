/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Roscpp
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class Logger : IDeserializable<Logger>, IMessage
    {
        [DataMember (Name = "name")] public string Name;
        [DataMember (Name = "level")] public string Level;
    
        /// Constructor for empty message.
        public Logger()
        {
            Name = "";
            Level = "";
        }
        
        /// Explicit constructor.
        public Logger(string Name, string Level)
        {
            this.Name = Name;
            this.Level = Level;
        }
        
        /// Constructor with buffer.
        public Logger(ref ReadBuffer b)
        {
            b.DeserializeString(out Name);
            b.DeserializeString(out Level);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new Logger(ref b);
        
        public Logger RosDeserialize(ref ReadBuffer b) => new Logger(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Name);
            b.Serialize(Level);
        }
        
        public void RosValidate()
        {
            if (Name is null) BuiltIns.ThrowNullReference();
            if (Level is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 8 + BuiltIns.GetStringSize(Name) + BuiltIns.GetStringSize(Level);
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "roscpp/Logger";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "a6069a2ff40db7bd32143dd66e1f408e";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAEysuKcrMS1fIS8xN5SqGsHNSy1JzuLi4AGqsOFEbAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
