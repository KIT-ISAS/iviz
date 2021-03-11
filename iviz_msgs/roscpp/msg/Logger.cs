/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Roscpp
{
    [Preserve, DataContract (Name = "roscpp/Logger")]
    public sealed class Logger : IDeserializable<Logger>, IMessage
    {
        [DataMember (Name = "name")] public string Name { get; set; }
        [DataMember (Name = "level")] public string Level { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Logger()
        {
            Name = string.Empty;
            Level = string.Empty;
        }
        
        /// <summary> Explicit constructor. </summary>
        public Logger(string Name, string Level)
        {
            this.Name = Name;
            this.Level = Level;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Logger(ref Buffer b)
        {
            Name = b.DeserializeString();
            Level = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Logger(ref b);
        }
        
        Logger IDeserializable<Logger>.RosDeserialize(ref Buffer b)
        {
            return new Logger(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Name);
            b.Serialize(Level);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Name is null) throw new System.NullReferenceException(nameof(Name));
            if (Level is null) throw new System.NullReferenceException(nameof(Level));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 8;
                size += BuiltIns.UTF8.GetByteCount(Name);
                size += BuiltIns.UTF8.GetByteCount(Level);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "roscpp/Logger";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "a6069a2ff40db7bd32143dd66e1f408e";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACisuKcrMS1fIS8xN5SqGsHNSy1JzuLh4uQC+IKDQHAAAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
