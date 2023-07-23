/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [DataContract]
    public sealed class JoyFeedbackArray : IHasSerializer<JoyFeedbackArray>, IMessage
    {
        // This message publishes values for multiple feedback at once. 
        [DataMember (Name = "array")] public JoyFeedback[] Array;
    
        public JoyFeedbackArray()
        {
            Array = EmptyArray<JoyFeedback>.Value;
        }
        
        public JoyFeedbackArray(JoyFeedback[] Array)
        {
            this.Array = Array;
        }
        
        public JoyFeedbackArray(ref ReadBuffer b)
        {
            {
                int n = b.DeserializeArrayLength();
                JoyFeedback[] array;
                if (n == 0) array = EmptyArray<JoyFeedback>.Value;
                else
                {
                    array = new JoyFeedback[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new JoyFeedback(ref b);
                    }
                }
                Array = array;
            }
        }
        
        public JoyFeedbackArray(ref ReadBuffer2 b)
        {
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                JoyFeedback[] array;
                if (n == 0) array = EmptyArray<JoyFeedback>.Value;
                else
                {
                    array = new JoyFeedback[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new JoyFeedback(ref b);
                    }
                }
                Array = array;
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
            b.Align4();
            b.Serialize(Array.Length);
            foreach (var t in Array)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Array, nameof(Array));
            foreach (var msg in Array) msg.RosValidate();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                size += 6 * Array.Length;
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size += 4; // Array.Length
            size += 8 * Array.Length;
            return size;
        }
    
        public const string MessageType = "sensor_msgs/JoyFeedbackArray";
    
        [IgnoreDataMember] public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "cde5730a895b1fc4dee6f91b754b213d";
    
        [IgnoreDataMember] public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        [IgnoreDataMember]
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE61Ry07DMBC8+ytG6rUKKQgJIXKpCKiISqiUA0WocpJNY+HYlR9t8/dsQ4PKnb3YnrVn" +
                "xrMjLBvl0ZL3ckPYxkIr35DHTurIS20d2qiD2mpCTVQVsvyCDLCmpATiyXYPJ/TjE9I52Ynsn0vMXx9v" +
                "4cl469at3/iLM1Uxwj2VWjqCrREaQui2/X5wK6Iy4QbL95d8/ZzfgytDeo4u3ubT55zRyTk6fVut8gWj" +
                "l+IEH5kFC/aR7ZXWaKyuIA1UBRPbglwfGMmy+bXRHwYvCb/OD7LlNMe92Vo5H6Cpwt5G5iqIubL0p+mp" +
                "tOZvZ3CoqqORmQmcigrd8PdBZ4za2RZpkiJYTBImVKbU0asdTw2zGhXtVMmUnmlkGaLUukOhjHTdGJXj" +
                "ew6+6ZWDIx54epcd7tLkGtKzWj1m8usjlE16xCSi1laGq0tWOrkSQnwDdkrRZGACAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<JoyFeedbackArray> CreateSerializer() => new Serializer();
        public Deserializer<JoyFeedbackArray> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<JoyFeedbackArray>
        {
            public override void RosSerialize(JoyFeedbackArray msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(JoyFeedbackArray msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(JoyFeedbackArray msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(JoyFeedbackArray msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(JoyFeedbackArray msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<JoyFeedbackArray>
        {
            public override void RosDeserialize(ref ReadBuffer b, out JoyFeedbackArray msg) => msg = new JoyFeedbackArray(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out JoyFeedbackArray msg) => msg = new JoyFeedbackArray(ref b);
        }
    }
}
