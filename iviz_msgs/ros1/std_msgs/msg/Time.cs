/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract]
    public sealed class Time : IDeserializable<Time>, IHasSerializer<Time>, IMessage
    {
        [DataMember (Name = "data")] public time Data;
    
        public Time()
        {
        }
        
        public Time(time Data)
        {
            this.Data = Data;
        }
        
        public Time(ref ReadBuffer b)
        {
            b.Deserialize(out Data);
        }
        
        public Time(ref ReadBuffer2 b)
        {
            b.Align4();
            b.Deserialize(out Data);
        }
        
        public Time RosDeserialize(ref ReadBuffer b) => new Time(ref b);
        
        public Time RosDeserialize(ref ReadBuffer2 b) => new Time(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Data);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Data);
        }
        
        public void RosValidate()
        {
        }
    
        public const int RosFixedMessageLength = 8;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        public const int Ros2FixedMessageLength = 8;
        
        public int Ros2MessageLength => Ros2FixedMessageLength;
        
        public int AddRos2MessageLength(int c) => WriteBuffer2.Align4(c) + Ros2FixedMessageLength;
        
    
        public const string MessageType = "std_msgs/Time";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "cd7166c74c552c311fbcc2fe5a7bc289";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAEyvJzE1VSEksSeTiAgBuylFyCwAAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<Time> CreateSerializer() => new Serializer();
        public Deserializer<Time> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<Time>
        {
            public override void RosSerialize(Time msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(Time msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(Time _) => RosFixedMessageLength;
            public override int Ros2MessageLength(Time _) => Ros2FixedMessageLength;
        }
    
        sealed class Deserializer : Deserializer<Time>
        {
            public override void RosDeserialize(ref ReadBuffer b, out Time msg) => msg = new Time(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out Time msg) => msg = new Time(ref b);
        }
    }
}
