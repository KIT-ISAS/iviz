/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.Roscpp
{
    [DataContract]
    public sealed class Logger : IDeserializableRos1<Logger>, IDeserializableRos2<Logger>, IMessageRos1, IMessageRos2
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
        
        /// Constructor with buffer.
        public Logger(ref ReadBuffer2 b)
        {
            b.DeserializeString(out Name);
            b.DeserializeString(out Level);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new Logger(ref b);
        
        public Logger RosDeserialize(ref ReadBuffer b) => new Logger(ref b);
        
        public Logger RosDeserialize(ref ReadBuffer2 b) => new Logger(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Name);
            b.Serialize(Level);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Name);
            b.Serialize(Level);
        }
        
        public void RosValidate()
        {
            if (Name is null) BuiltIns.ThrowNullReference();
            if (Level is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => 8 + WriteBuffer.GetStringSize(Name) + WriteBuffer.GetStringSize(Level);
        
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            WriteBuffer2.AddLength(ref c, Name);
            WriteBuffer2.AddLength(ref c, Level);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "roscpp/Logger";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "a6069a2ff40db7bd32143dd66e1f408e";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAEysuKcrMS1fIS8xN5SqGsHNSy1JzuLi4AGqsOFEbAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
