using System.Runtime.Serialization;

namespace Iviz.Msgs.RosgraphMsgs
{
    [DataContract (Name = "rosgraph_msgs/Clock")]
    public sealed class Clock : IMessage
    {
        // roslib/Clock is used for publishing simulated time in ROS. 
        // This message simply communicates the current time.
        // For more information, see http://www.ros.org/wiki/Clock
        [DataMember (Name = "clock")] public time Clock_ { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Clock()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public Clock(time Clock_)
        {
            this.Clock_ = Clock_;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Clock(Buffer b)
        {
            Clock_ = b.Deserialize<time>();
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new Clock(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(Clock_);
        }
        
        public void RosValidate()
        {
        }
    
        public int RosMessageLength => 8;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "rosgraph_msgs/Clock";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "a9c97c1d230cfc112e270351a944ee47";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAEyWOsQrDMBBD93yFIGux966FroW2P5C41/iI7Qs+G5O/r5NuAulJGpFFA8/2FsStYEVV" +
                "+uArGVudA6vntEA51jCVbhSOBE54Pl4Gw4i370gk1WmhI7aFHU5irIldBxTFE1zNmVI5YdOhe2+Pko+i" +
                "PhSnwpIuUCL4Urarta01038ZyYttvPL/3XCOu1MOP+0lWhy5AAAA";
                
    }
}
