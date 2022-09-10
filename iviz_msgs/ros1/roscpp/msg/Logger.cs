/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.Roscpp
{
    [DataContract]
    public sealed class Logger : IHasSerializer<Logger>, IMessage
    {
        [DataMember (Name = "name")] public string Name;
        [DataMember (Name = "level")] public string Level;
    
        public Logger()
        {
            Name = "";
            Level = "";
        }
        
        public Logger(string Name, string Level)
        {
            this.Name = Name;
            this.Level = Level;
        }
        
        public Logger(ref ReadBuffer b)
        {
            b.DeserializeString(out Name);
            b.DeserializeString(out Level);
        }
        
        public Logger(ref ReadBuffer2 b)
        {
            b.Align4();
            b.DeserializeString(out Name);
            b.Align4();
            b.DeserializeString(out Level);
        }
        
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
    
        public int RosMessageLength
        {
            get
            {
                int size = 8;
                size += WriteBuffer.GetStringSize(Name);
                size += WriteBuffer.GetStringSize(Level);
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Name);
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Level);
            return size;
        }
    
        public const string MessageType = "roscpp/Logger";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "a6069a2ff40db7bd32143dd66e1f408e";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAEysuKcrMS1fIS8xN5SqGsHNSy1JzuLi4AGqsOFEbAAAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<Logger> CreateSerializer() => new Serializer();
        public Deserializer<Logger> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<Logger>
        {
            public override void RosSerialize(Logger msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(Logger msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(Logger msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(Logger msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(Logger msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<Logger>
        {
            public override void RosDeserialize(ref ReadBuffer b, out Logger msg) => msg = new Logger(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out Logger msg) => msg = new Logger(ref b);
        }
    }
}
