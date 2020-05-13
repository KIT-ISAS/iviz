using System.Runtime.Serialization;

namespace Iviz.Msgs.std_msgs
{
    [DataContract]
    public sealed class Header : IMessage
    {
        // Standard metadata for higher-level stamped data types.
        // This is generally used to communicate timestamped data 
        // in a particular coordinate frame.
        // 
        // sequence ID: consecutively increasing ID 
        [DataMember] public uint seq { get; set; }
        //Two-integer timestamp that is expressed as:
        // * stamp.sec: seconds (stamp_secs) since epoch (in Python the variable is called 'secs')
        // * stamp.nsec: nanoseconds since stamp_secs (in Python the variable is called 'nsecs')
        // time-handling sugar is provided by the client library
        [DataMember] public time stamp { get; set; }
        //Frame this data is associated with
        [DataMember] public string frame_id { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Header()
        {
            frame_id = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public Header(uint seq, time stamp, string frame_id)
        {
            this.seq = seq;
            this.stamp = stamp;
            this.frame_id = frame_id ?? throw new System.ArgumentNullException(nameof(frame_id));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Header(Buffer b)
        {
            this.seq = b.Deserialize<uint>();
            this.stamp = b.Deserialize<time>();
            this.frame_id = b.DeserializeString();
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new Header(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.seq);
            b.Serialize(this.stamp);
            b.Serialize(this.frame_id);
        }
        
        public void Validate()
        {
            if (frame_id is null) throw new System.NullReferenceException();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 16;
                size += BuiltIns.UTF8.GetByteCount(frame_id);
                return size;
            }
        }
    
        string IMessage.RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "std_msgs/Header";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "2176decaecbce78abc3b96ef049fabed";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE42RT2vDMAzF7/4UghzaDtrDdut5DHYbrPei2moscOxMVtrl209O2brdBobg+L3f058O" +
                "3hVzQAkwkGJARTgXgch9JNkmulCCqjiMFGB51XmkunMdHCJXsNNTJsGUZpiqibSAL8MwZfaoBMoD/fGb" +
                "kzMgjCjKfkoopi8SODf5WXCgRrdT6WOi7Alen/emyZX8pGwFzUbwQlg59/YIbuKsT4/N4LrDtWztSj3J" +
                "PRw0orZi6XMUqq1OrHvLeLg1tzP23vyWEiqsl39Hu9YNWIiVQGPxEdZW+dussWQDElxQGE+JGtjbBIy6" +
                "aqbV5hc5L+iMuXzjb8R7xn+w+YfbetpG21lq3deptwGacJRy4WDS07xAfGLKColPgjK75rpFuu6lzdhE" +
                "5lo2Yl+stXi2BQS4skZXVRp92caRg3NfAmsPMygCAAA=";
                
    }
}
