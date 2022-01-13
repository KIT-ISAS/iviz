/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class JoyFeedbackArray : IDeserializable<JoyFeedbackArray>, IMessage
    {
        // This message publishes values for multiple feedback at once. 
        [DataMember (Name = "array")] public JoyFeedback[] Array;
    
        /// Constructor for empty message.
        public JoyFeedbackArray()
        {
            Array = System.Array.Empty<JoyFeedback>();
        }
        
        /// Explicit constructor.
        public JoyFeedbackArray(JoyFeedback[] Array)
        {
            this.Array = Array;
        }
        
        /// Constructor with buffer.
        public JoyFeedbackArray(ref ReadBuffer b)
        {
            Array = b.DeserializeArray<JoyFeedback>();
            for (int i = 0; i < Array.Length; i++)
            {
                Array[i] = new JoyFeedback(ref b);
            }
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new JoyFeedbackArray(ref b);
        
        public JoyFeedbackArray RosDeserialize(ref ReadBuffer b) => new JoyFeedbackArray(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeArray(Array);
        }
        
        public void RosValidate()
        {
            if (Array is null) throw new System.NullReferenceException(nameof(Array));
            for (int i = 0; i < Array.Length; i++)
            {
                if (Array[i] is null) throw new System.NullReferenceException($"{nameof(Array)}[{i}]");
                Array[i].RosValidate();
            }
        }
    
        public int RosMessageLength => 4 + 6 * Array.Length;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "sensor_msgs/JoyFeedbackArray";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "cde5730a895b1fc4dee6f91b754b213d";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE61Ry07DMBC8+ytG6rUKKQgJIXKpCKiISqiUA0WocpJNY+HYlR9t8/dsQ4PKnb3YnrVn" +
                "xrMjLBvl0ZL3ckPYxkIr35DHTurIS20d2qiD2mpCTVQVsvyCDLCmpATiyXYPJ/TjE9I52Ynsn0vMXx9v" +
                "4cl469at3/iLM1Uxwj2VWjqCrREaQui2/X5wK6Iy4QbL95d8/ZzfgytDeo4u3ubT55zRyTk6fVut8gWj" +
                "l+IEH5kFC/aR7ZXWaKyuIA1UBRPbglwfGMmy+bXRHwYvCb/OD7LlNMe92Vo5H6Cpwt5G5iqIubL0p+mp" +
                "tOZvZ3CoqqORmQmcigrd8PdBZ4za2RZpkiJYTBImVKbU0asdTw2zGhXtVMmUnmlkGaLUukOhjHTdGJXj" +
                "ew6+6ZWDIx54epcd7tLkGtKzWj1m8usjlE16xCSi1laGq0tWOrkSQnwDdkrRZGACAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
