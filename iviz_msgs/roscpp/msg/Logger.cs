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
            Name = string.Empty;
            Level = string.Empty;
        }
        
        /// Explicit constructor.
        public Logger(string Name, string Level)
        {
            this.Name = Name;
            this.Level = Level;
        }
        
        /// Constructor with buffer.
        internal Logger(ref Buffer b)
        {
            Name = b.DeserializeString();
            Level = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new Logger(ref b);
        
        Logger IDeserializable<Logger>.RosDeserialize(ref Buffer b) => new Logger(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Name);
            b.Serialize(Level);
        }
        
        public void RosValidate()
        {
            if (Name is null) throw new System.NullReferenceException(nameof(Name));
            if (Level is null) throw new System.NullReferenceException(nameof(Level));
        }
    
        public int RosMessageLength => 8 + BuiltIns.GetStringSize(Name) + BuiltIns.GetStringSize(Level);
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "roscpp/Logger";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "a6069a2ff40db7bd32143dd66e1f408e";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAEysuKcrMS1fIS8xN5SqGsHNSy1JzuLi4AGqsOFEbAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
