/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract]
    public sealed class Time : IDeserializableRos1<Time>, IDeserializableRos2<Time>, IMessageRos1, IMessageRos2
    {
        [DataMember (Name = "data")] public time Data;
    
        /// Constructor for empty message.
        public Time()
        {
        }
        
        /// Explicit constructor.
        public Time(time Data)
        {
            this.Data = Data;
        }
        
        /// Constructor with buffer.
        public Time(ref ReadBuffer b)
        {
            b.Deserialize(out Data);
        }
        
        /// Constructor with buffer.
        public Time(ref ReadBuffer2 b)
        {
            b.Deserialize(out Data);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new Time(ref b);
        
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
    
        /// <summary> Constant size of this message. </summary> 
        public const int RosFixedMessageLength = 8;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        /// <summary> Constant size of this message. </summary> 
        public const int Ros2FixedMessageLength = 8;
        
        public int Ros2MessageLength => Ros2FixedMessageLength;
        
        public void AddRos2MessageLength(ref int c)
        {
            WriteBuffer2.AddLength(ref c, Data);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "std_msgs/Time";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "cd7166c74c552c311fbcc2fe5a7bc289";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAEyvJzE1VSEksSeTiAgBuylFyCwAAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
