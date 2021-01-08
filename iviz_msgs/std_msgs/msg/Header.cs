/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StdMsgs
{
    [Preserve, DataContract (Name = "std_msgs/Header")]
    public sealed class Header : IDeserializable<Header>, IMessage
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
        public Header(ref Buffer b)
        {
            Seq = b.Deserialize<uint>();
            Stamp = b.Deserialize<time>();
            FrameId = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Header(ref b);
        }
        
        Header IDeserializable<Header>.RosDeserialize(ref Buffer b)
        {
            return new Header(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
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
                "H4sIAAAAAAAACo2RQWvDMAyF74H8B0EPbQftYbvlPAa7DdZ7UW01Fjh2Jivt8u8npxvdbgMH4/i9T8/S" +
                "Ct4Vk0fxMJCiR0U4Z4HAfSDZRbpQhKI4jORhudV5pLJvmxUcAhew1VMiwRhnmIqpNIPLwzAldqgEygP9" +
                "AVQrJ0AYUZTdFFHMkMVzqvqz4EALv36FPiZKjuD1uTNVKuQmZQs1G8MJYeHU26WJJ0769FgdZjxc887O" +
                "1JPcE4AG1JqYPkehUsNi6WqZh9sb94bvjGCFfIHN8u9ox7IFq2MpaMwuwMbiv80acjIiwQWF8RSpkp31" +
                "wbDralpvf6Nr9A4SpvzDvyHvRf7DrZRvcH3WLtjwYm1BmXrroylHyRf2pj3NC8VFpqQQ+SQoc9tU262o" +
                "QV5qs01mvmU2tmMp2bFNwsOVNbRNUakFlrkc2bdN23wB2ue47DYCAAA=";
                
    }
}
