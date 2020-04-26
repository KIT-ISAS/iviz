using System.Runtime.Serialization;

namespace Iviz.Msgs.sensor_msgs
{
    public sealed class JoyFeedbackArray : IMessage
    {
        // This message publishes values for multiple feedback at once. 
        public JoyFeedback[] array;
    
        /// <summary> Constructor for empty message. </summary>
        public JoyFeedbackArray()
        {
            array = System.Array.Empty<JoyFeedback>();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.DeserializeArray(out array, ref ptr, end, 0);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.SerializeArray(array, ref ptr, end, 0);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += 6 * array.Length;
                return size;
            }
        }
    
        public IMessage Create() => new JoyFeedbackArray();
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string RosMessageType = "sensor_msgs/JoyFeedbackArray";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string RosMd5Sum = "cde5730a895b1fc4dee6f91b754b213d";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE61Ry07DMBC8+ytG6rUKKQgJIXKpCKiISqiUA0WocpJNY+HYlR9t8/dsQ4PKnb3YnrVn" +
                "xrMjLBvl0ZL3ckPYxkIr35DHTurIS20d2qiD2mpCTVQVsvyCDLCmpATiyXYPJ/TjE9I52Ynsn0vMXx9v" +
                "4cl469at3/iLM1Uxwj2VWjqCrREaQui2/X5wK6Iy4QbL95d8/ZzfgytDeo4u3ubT55zRyTk6fVut8gWj" +
                "l+IEH5kFC/aR7ZXWaKyuIA1UBRPbglwfGMmy+bXRHwYvCb/OD7LlNMe92Vo5H6Cpwt5G5iqIubL0p+mp" +
                "tOZvZ3CoqqORmQmcigrd8PdBZ4za2RZpkiJYTBImVKbU0asdTw2zGhXtVMmUnmlkGaLUukOhjHTdGJXj" +
                "ew6+6ZWDIx54epcd7tLkGtKzWj1m8usjlE16xCSi1laGq0tWOrkSQnwDdkrRZGACAAA=";
                
    }
}
