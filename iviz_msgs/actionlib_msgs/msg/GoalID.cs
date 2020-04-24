namespace Iviz.Msgs.actionlib_msgs
{
    public sealed class GoalID : IMessage
    {
        // The stamp should store the time at which this goal was requested.
        // It is used by an action server when it tries to preempt all
        // goals that were requested before a certain time
        public time stamp;
        
        // The id provides a way to associate feedback and
        // result message with specific goal requests. The id
        // specified must be unique.
        public string id;
        
    
        /// <summary> Constructor for empty message. </summary>
        public GoalID()
        {
            id = "";
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out stamp, ref ptr, end);
            BuiltIns.Deserialize(out id, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(stamp, ref ptr, end);
            BuiltIns.Serialize(id, ref ptr, end);
        }
    
        public int GetLength()
        {
            int size = 12;
            size += id.Length;
            return size;
        }
    
        public IMessage Create() => new GoalID();
    
        /// <summary> Full ROS name of this message. </summary>
        public const string _MessageType = "actionlib_msgs/GoalID";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string _Md5Sum = "302881f31927c1df708a2dbab0e80ee8";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string _DependenciesBase64 =
                "H4sIAAAAAAAAEz2PS5LDIAxE95yiq7L3PbKfC8ggG1VscJCIK7cf4XHNkqY/Tw/8ZIYa7Qc0174lf9TG" +
                "MJdNdgYZziwxuyKKtdKGkxSN353VOE3hgafB/7pywvwFFVA0qQXK7cPN81wgBmvCCqs4GvN+GGjbPD06" +
                "Xc5jiX36vxozL4OFELkZSbmIwoV1IQdPD35JXlk/kryeHO87Rki1RiFjLMxppvhysuSJxto3w86qtDJO" +
                "sQw9OMoi8e/Am0Cnu91Dt8Gh9q7mZOhF3DUF9bPKOlwh/AJcvpWYTwEAAA==";
                
    }
}
