using System.Runtime.Serialization;

namespace Iviz.Msgs.sensor_msgs
{
    [DataContract]
    public sealed class JoyFeedbackArray : IMessage
    {
        // This message publishes values for multiple feedback at once. 
        [DataMember] public JoyFeedback[] array { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public JoyFeedbackArray()
        {
            array = System.Array.Empty<JoyFeedback>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public JoyFeedbackArray(JoyFeedback[] array)
        {
            this.array = array ?? throw new System.ArgumentNullException(nameof(array));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal JoyFeedbackArray(Buffer b)
        {
            this.array = b.DeserializeArray<JoyFeedback>();
            for (int i = 0; i < this.array.Length; i++)
            {
                this.array[i] = new JoyFeedback(b);
            }
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new JoyFeedbackArray(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.SerializeArray(this.array, 0);
        }
        
        public void Validate()
        {
            if (array is null) throw new System.NullReferenceException();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += 6 * array.Length;
                return size;
            }
        }
    
        string IMessage.RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "sensor_msgs/JoyFeedbackArray";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "cde5730a895b1fc4dee6f91b754b213d";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE61Ry07DMBC8+ytG6rUKKQgJIXKpCKiISqiUA0WocpJNY+HYlR9t8/dsQ4PKnb3YnrVn" +
                "xrMjLBvl0ZL3ckPYxkIr35DHTurIS20d2qiD2mpCTVQVsvyCDLCmpATiyXYPJ/TjE9I52Ynsn0vMXx9v" +
                "4cl469at3/iLM1Uxwj2VWjqCrREaQui2/X5wK6Iy4QbL95d8/ZzfgytDeo4u3ubT55zRyTk6fVut8gWj" +
                "l+IEH5kFC/aR7ZXWaKyuIA1UBRPbglwfGMmy+bXRHwYvCb/OD7LlNMe92Vo5H6Cpwt5G5iqIubL0p+mp" +
                "tOZvZ3CoqqORmQmcigrd8PdBZ4za2RZpkiJYTBImVKbU0asdTw2zGhXtVMmUnmlkGaLUukOhjHTdGJXj" +
                "ew6+6ZWDIx54epcd7tLkGtKzWj1m8usjlE16xCSi1laGq0tWOrkSQnwDdkrRZGACAAA=";
                
    }
}
