using System.Runtime.Serialization;

namespace Iviz.Msgs.rosgraph_msgs
{
    [DataContract]
    public sealed class Clock : IMessage
    {
        // roslib/Clock is used for publishing simulated time in ROS. 
        // This message simply communicates the current time.
        // For more information, see http://www.ros.org/wiki/Clock
        [DataMember] public time clock { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Clock()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public Clock(time clock)
        {
            this.clock = clock;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Clock(Buffer b)
        {
            this.clock = b.Deserialize<time>();
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new Clock(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.clock);
        }
        
        public void Validate()
        {
        }
    
        public int RosMessageLength => 8;
    
        string IMessage.RosType => RosMessageType;
    
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
