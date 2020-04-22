
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
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "std_msgs/Header";
    
        public IMessage Create() => new Header();
    
        public int GetLength()
        {
            int size = 16;
            size += frame_id.Length;
            return size;
        }
    
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
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "2176decaecbce78abc3b96ef049fabed";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAACo2RT2vDMAzF74F9B0EObQftYbvlPAa7DdZ7UW01Fjh2Jivt8u0nJ4xut4EhOH7v9/Sn" +
                "hQ/F5FE8DKToUREuWSBwH0j2ka4UoSgOI3lYXnUeqRyaFo6BC9jpKZFgjDNMxUSaweVhmBI7VALlgf74" +
                "zckJEEYUZTdFFNNn8Zyq/CI4UKXbKfQ5UXIEby+daVIhNylbQbMRnBAWTr09QjNx0uenamja4y3v7Uo9" +
                "yT0cNKDWYulrFCq1TiydZTyuzR2M3ZnfUnyB7fLvZNeyAwtxBDRmF2Brlb/PGnIyIMEVhfEcqYKdTcCo" +
                "m2ra7H6Ra9kdJEz5B78S7xn/wVbKyq097YPtLNbuy9TbAE04Sr6yN+l5XiAuMiWFyGdBmZvqWiOb9rXO" +
                "2ETmWjZiXywlO7YFeLixhqaoVPqyjRP75qH5BhP+q5MpAgAA";
                
    }
}
