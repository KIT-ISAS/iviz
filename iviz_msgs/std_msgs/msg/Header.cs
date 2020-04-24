namespace Iviz.Msgs.std_msgs
{
    public sealed class Header : IMessage
    {
        // Standard metadata for higher-level stamped data types.
        // This is generally used to communicate timestamped data 
        // in a particular coordinate frame.
        // 
        // sequence ID: consecutively increasing ID 
        public uint seq;
        //Two-integer timestamp that is expressed as:
        // * stamp.sec: seconds (stamp_secs) since epoch (in Python the variable is called 'secs')
        // * stamp.nsec: nanoseconds since stamp_secs (in Python the variable is called 'nsecs')
        // time-handling sugar is provided by the client library
        public time stamp;
        //Frame this data is associated with
        public string frame_id;
    
        /// <summary> Constructor for empty message. </summary>
        public Header()
        {
            frame_id = "";
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out seq, ref ptr, end);
            BuiltIns.Deserialize(out stamp, ref ptr, end);
            BuiltIns.Deserialize(out frame_id, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(seq, ref ptr, end);
            BuiltIns.Serialize(stamp, ref ptr, end);
            BuiltIns.Serialize(frame_id, ref ptr, end);
        }
    
        public int GetLength()
        {
            int size = 16;
            size += frame_id.Length;
            return size;
        }
    
        public IMessage Create() => new Header();
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "std_msgs/Header";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "2176decaecbce78abc3b96ef049fabed";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAAE42RT2vDMAzF7/4UghzaDtrDdut5DHYbrPei2moscOxMVtrl209O2brdBobg+L3f058O" +
                "3hVzQAkwkGJARTgXgch9JNkmulCCqjiMFGB51XmkunMdHCJXsNNTJsGUZpiqibSAL8MwZfaoBMoD/fGb" +
                "kzMgjCjKfkoopi8SODf5WXCgRrdT6WOi7Alen/emyZX8pGwFzUbwQlg59/YIbuKsT4/N4LrDtWztSj3J" +
                "PRw0orZi6XMUqq1OrHvLeLg1tzP23vyWEiqsl39Hu9YNWIiVQGPxEdZW+dussWQDElxQGE+JGtjbBIy6" +
                "aqbV5hc5L+iMuXzjb8R7xn+w+YfbetpG21lq3deptwGacJRy4WDS07xAfGLKColPgjK75rpFuu6lzdhE" +
                "5lo2Yl+stXi2BQS4skZXVRp92caRg3NfAmsPMygCAAA=";
                
    }
}
