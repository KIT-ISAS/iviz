/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [DataContract]
    public sealed class JoyFeedbackArray : IDeserializable<JoyFeedbackArray>, IMessage
    {
        // This message publishes values for multiple feedback at once. 
        [DataMember (Name = "array")] public JoyFeedback[] Array;
    
        public JoyFeedbackArray()
        {
            Array = System.Array.Empty<JoyFeedback>();
        }
        
        public JoyFeedbackArray(JoyFeedback[] Array)
        {
            this.Array = Array;
        }
        
        public JoyFeedbackArray(ref ReadBuffer b)
        {
            {
                int n = b.DeserializeArrayLength();
                Array = n == 0
                    ? System.Array.Empty<JoyFeedback>()
                    : new JoyFeedback[n];
                for (int i = 0; i < n; i++)
                {
                    Array[i] = new JoyFeedback(ref b);
                }
            }
        }
        
        public JoyFeedbackArray(ref ReadBuffer2 b)
        {
            b.Align4();
            {
                int n = b.DeserializeArrayLength();
                Array = n == 0
                    ? System.Array.Empty<JoyFeedback>()
                    : new JoyFeedback[n];
                for (int i = 0; i < n; i++)
                {
                    Array[i] = new JoyFeedback(ref b);
                }
            }
        }
        
        public JoyFeedbackArray RosDeserialize(ref ReadBuffer b) => new JoyFeedbackArray(ref b);
        
        public JoyFeedbackArray RosDeserialize(ref ReadBuffer2 b) => new JoyFeedbackArray(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Array.Length);
            foreach (var t in Array)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(Array.Length);
            foreach (var t in Array)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosValidate()
        {
            if (Array is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Array.Length; i++)
            {
                if (Array[i] is null) BuiltIns.ThrowNullReference(nameof(Array), i);
                Array[i].RosValidate();
            }
        }
    
        public int RosMessageLength => 4 + 6 * Array.Length;
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = WriteBuffer2.Align4(c);
            c += 4; // Array length
            c += 8 * Array.Length;
            return c;
        }
    
        public const string MessageType = "sensor_msgs/JoyFeedbackArray";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "cde5730a895b1fc4dee6f91b754b213d";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE61Ry07DMBC8+ytG6rUKKQgJIXKpCKiISqiUA0WocpJNY+HYlR9t8/dsQ4PKnb3YnrVn" +
                "xrMjLBvl0ZL3ckPYxkIr35DHTurIS20d2qiD2mpCTVQVsvyCDLCmpATiyXYPJ/TjE9I52Ynsn0vMXx9v" +
                "4cl469at3/iLM1Uxwj2VWjqCrREaQui2/X5wK6Iy4QbL95d8/ZzfgytDeo4u3ubT55zRyTk6fVut8gWj" +
                "l+IEH5kFC/aR7ZXWaKyuIA1UBRPbglwfGMmy+bXRHwYvCb/OD7LlNMe92Vo5H6Cpwt5G5iqIubL0p+mp" +
                "tOZvZ3CoqqORmQmcigrd8PdBZ4za2RZpkiJYTBImVKbU0asdTw2zGhXtVMmUnmlkGaLUukOhjHTdGJXj" +
                "ew6+6ZWDIx54epcd7tLkGtKzWj1m8usjlE16xCSi1laGq0tWOrkSQnwDdkrRZGACAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
