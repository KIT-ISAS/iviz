/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.RosgraphMsgs
{
    [DataContract]
    public sealed class Clock : IDeserializableCommon<Clock>, IMessageCommon
    {
        // roslib/Clock is used for publishing simulated time in ROS. 
        // This message simply communicates the current time.
        // For more information, see http://www.ros.org/wiki/Clock
        [DataMember (Name = "clock")] public time Clock_;
    
        public Clock()
        {
        }
        
        public Clock(time Clock_)
        {
            this.Clock_ = Clock_;
        }
        
        public Clock(ref ReadBuffer b)
        {
            b.Deserialize(out Clock_);
        }
        
        public Clock(ref ReadBuffer2 b)
        {
            b.Deserialize(out Clock_);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new Clock(ref b);
        
        public Clock RosDeserialize(ref ReadBuffer b) => new Clock(ref b);
        
        public Clock RosDeserialize(ref ReadBuffer2 b) => new Clock(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Clock_);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Clock_);
        }
        
        public void RosValidate()
        {
        }
    
        public const int RosFixedMessageLength = 8;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        public const int Ros2FixedMessageLength = 8;
        
        public int Ros2MessageLength => Ros2FixedMessageLength;
        
        public void AddRos2MessageLength(ref int c)
        {
            WriteBuffer2.AddLength(ref c, Clock_);
        }
    
        public const string MessageType = "rosgraph_msgs/Clock";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "a9c97c1d230cfc112e270351a944ee47";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAEyWOsQrDMBBD93yFIGux966FroW2P5C41/iI7Qs+G5O/r5NuAulJGpFFA8/2FsStYEVV" +
                "+uArGVudA6vntEA51jCVbhSOBE54Pl4Gw4i370gk1WmhI7aFHU5irIldBxTFE1zNmVI5YdOhe2+Pko+i" +
                "PhSnwpIuUCL4Urarta01038ZyYttvPL/3XCOu1MOP+0lWhy5AAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
