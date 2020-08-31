using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [DataContract (Name = "std_msgs/Header")]
    public sealed class Header : IMessage
    {
        // Standard metadata for higher-level stamped data types.
        // This is generally used to communicate timestamped data 
        // in a particular coordinate frame.
        // 
        // sequence ID: consecutively increasing ID 
        [DataMember (Name = "seq")] public uint Seq { get; set; }
        //Two-integer timestamp that is expressed as:
        // * stamp.sec: seconds (stamp_secs) since epoch (in Python the variable is called 'secs')
        // * stamp.nsec: nanoseconds since stamp_secs (in Python the variable is called 'nsecs')
        // time-handling sugar is provided by the client library
        [DataMember (Name = "stamp")] public time Stamp { get; set; }
        //Frame this data is associated with
        [DataMember (Name = "frame_id")] public string FrameId { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Header()
        {
            FrameId = "";
        }
        
        /// <summary> Explicit constructor. </summary>
        public Header(uint Seq, time Stamp, string FrameId)
        {
            this.Seq = Seq;
            this.Stamp = Stamp;
            this.FrameId = FrameId;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Header(Buffer b)
        {
            Seq = b.Deserialize<uint>();
            Stamp = b.Deserialize<time>();
            FrameId = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new Header(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(Seq);
            b.Serialize(Stamp);
            b.Serialize(FrameId);
        }
        
        public void RosValidate()
        {
            if (FrameId is null) throw new System.NullReferenceException(nameof(FrameId));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 16;
                size += BuiltIns.UTF8.GetByteCount(FrameId);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
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
