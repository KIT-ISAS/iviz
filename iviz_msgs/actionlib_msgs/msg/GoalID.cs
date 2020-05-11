using System.Runtime.Serialization;

namespace Iviz.Msgs.actionlib_msgs
{
    public sealed class GoalID : IMessage
    {
        // The stamp should store the time at which this goal was requested.
        // It is used by an action server when it tries to preempt all
        // goals that were requested before a certain time
        public time stamp { get; set; }
        
        // The id provides a way to associate feedback and
        // result message with specific goal requests. The id
        // specified must be unique.
        public string id { get; set; }
        
    
        /// <summary> Constructor for empty message. </summary>
        public GoalID()
        {
            id = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public GoalID(time stamp, string id)
        {
            this.stamp = stamp;
            this.id = id ?? throw new System.ArgumentNullException(nameof(id));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal GoalID(Buffer b)
        {
            this.stamp = b.Deserialize<time>();
            this.id = b.DeserializeString();
        }
        
        public IMessage Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new GoalID(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.stamp);
            b.Serialize(this.id);
        }
        
        public void Validate()
        {
            if (id is null) throw new System.NullReferenceException();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 12;
                size += BuiltIns.UTF8.GetByteCount(id);
                return size;
            }
        }
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve]
        public const string RosMessageType = "actionlib_msgs/GoalID";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve]
        public const string RosMd5Sum = "302881f31927c1df708a2dbab0e80ee8";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve]
        public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAEz2PS5LDIAxE95yiq7L3PbKfC8ggG1VscJCIK7cf4XHNkqY/Tw/8ZIYa7Qc0174lf9TG" +
                "MJdNdgYZziwxuyKKtdKGkxSN353VOE3hgafB/7pywvwFFVA0qQXK7cPN81wgBmvCCqs4GvN+GGjbPD06" +
                "Xc5jiX36vxozL4OFELkZSbmIwoV1IQdPD35JXlk/kryeHO87Rki1RiFjLMxppvhysuSJxto3w86qtDJO" +
                "sQw9OMoi8e/Am0Cnu91Dt8Gh9q7mZOhF3DUF9bPKOlwh/AJcvpWYTwEAAA==";
                
    }
}
