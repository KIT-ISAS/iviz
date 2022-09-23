/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [DataContract]
    public sealed class MultiDOFJointState : IHasSerializer<MultiDOFJointState>, IMessage
    {
        // Representation of state for joints with multiple degrees of freedom, 
        // following the structure of JointState.
        //
        // It is assumed that a joint in a system corresponds to a transform that gets applied 
        // along the kinematic chain. For example, a planar joint (as in URDF) is 3DOF (x, y, yaw)
        // and those 3DOF can be expressed as a transformation matrix, and that transformation
        // matrix can be converted back to (x, y, yaw)
        //
        // Each joint is uniquely identified by its name
        // The header specifies the time at which the joint states were recorded. All the joint states
        // in one message have to be recorded at the same time.
        //
        // This message consists of a multiple arrays, one for each part of the joint state. 
        // The goal is to make each of the fields optional. When e.g. your joints have no
        // wrench associated with them, you can leave the wrench array empty. 
        //
        // All arrays in this message should have the same size, or be empty.
        // This is the only way to uniquely associate the joint name with the correct
        // states.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "joint_names")] public string[] JointNames;
        [DataMember (Name = "transforms")] public GeometryMsgs.Transform[] Transforms;
        [DataMember (Name = "twist")] public GeometryMsgs.Twist[] Twist;
        [DataMember (Name = "wrench")] public GeometryMsgs.Wrench[] Wrench;
    
        public MultiDOFJointState()
        {
            JointNames = EmptyArray<string>.Value;
            Transforms = EmptyArray<GeometryMsgs.Transform>.Value;
            Twist = EmptyArray<GeometryMsgs.Twist>.Value;
            Wrench = EmptyArray<GeometryMsgs.Wrench>.Value;
        }
        
        public MultiDOFJointState(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            JointNames = b.DeserializeStringArray();
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<GeometryMsgs.Transform>.Value
                    : new GeometryMsgs.Transform[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(array);
                }
                Transforms = array;
            }
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<GeometryMsgs.Twist>.Value
                    : new GeometryMsgs.Twist[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(array);
                }
                Twist = array;
            }
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<GeometryMsgs.Wrench>.Value
                    : new GeometryMsgs.Wrench[n];
                for (int i = 0; i < n; i++)
                {
                    array[i] = new GeometryMsgs.Wrench(ref b);
                }
                Wrench = array;
            }
        }
        
        public MultiDOFJointState(ref ReadBuffer2 b)
        {
            Header = new StdMsgs.Header(ref b);
            b.Align4();
            JointNames = b.DeserializeStringArray();
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<GeometryMsgs.Transform>.Value
                    : new GeometryMsgs.Transform[n];
                if (n != 0)
                {
                    b.Align8();
                    b.DeserializeStructArray(array);
                }
                Transforms = array;
            }
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<GeometryMsgs.Twist>.Value
                    : new GeometryMsgs.Twist[n];
                if (n != 0)
                {
                    b.Align8();
                    b.DeserializeStructArray(array);
                }
                Twist = array;
            }
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<GeometryMsgs.Wrench>.Value
                    : new GeometryMsgs.Wrench[n];
                for (int i = 0; i < n; i++)
                {
                    array[i] = new GeometryMsgs.Wrench(ref b);
                }
                Wrench = array;
            }
        }
        
        public MultiDOFJointState RosDeserialize(ref ReadBuffer b) => new MultiDOFJointState(ref b);
        
        public MultiDOFJointState RosDeserialize(ref ReadBuffer2 b) => new MultiDOFJointState(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeArray(JointNames);
            b.SerializeStructArray(Transforms);
            b.SerializeStructArray(Twist);
            b.Serialize(Wrench.Length);
            foreach (var t in Wrench)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.SerializeArray(JointNames);
            b.SerializeStructArray(Transforms);
            b.SerializeStructArray(Twist);
            b.Serialize(Wrench.Length);
            foreach (var t in Wrench)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosValidate()
        {
            if (JointNames is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < JointNames.Length; i++)
            {
                if (JointNames[i] is null) BuiltIns.ThrowNullReference(nameof(JointNames), i);
            }
            if (Transforms is null) BuiltIns.ThrowNullReference();
            if (Twist is null) BuiltIns.ThrowNullReference();
            if (Wrench is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Wrench.Length; i++)
            {
                if (Wrench[i] is null) BuiltIns.ThrowNullReference(nameof(Wrench), i);
                Wrench[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 16;
                size += Header.RosMessageLength;
                size += WriteBuffer.GetArraySize(JointNames);
                size += 56 * Transforms.Length;
                size += 48 * Twist.Length;
                size += 48 * Wrench.Length;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Header.AddRos2MessageLength(size);
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, JointNames);
            size = WriteBuffer2.Align4(size);
            size += 4; // Transforms.Length
            size = WriteBuffer2.Align8(size);
            size += 56 * Transforms.Length;
            size += 4; // Twist.Length
            size = WriteBuffer2.Align8(size);
            size += 48 * Twist.Length;
            size += 4; // Wrench.Length
            size = WriteBuffer2.Align8(size);
            size += 48 * Wrench.Length;
            return size;
        }
    
        public const string MessageType = "sensor_msgs/MultiDOFJointState";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "690f272f0640d2631c305eeb8301e59d";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71WTW/cNhC961cQ2EPsYr1F46IHAz0UcN26QNE0cZtDEBhcaVZiTZEKSa2s/Pq+GUra" +
                "9UfRHFobhq2PmceZN29mtFJvqQsUySWdjHfK71TEJamdD+ovb1yKajCpUW1vk+ksqYrqQBTZcoeLyrdr" +
                "Vaxgb60fjKtVaggYoS9TH4jNfmGYd4y6KVYwvU7KRKVj7FuqYK6T0vksZRwu4xgTtar0AZF13lVRJY/n" +
                "KWgXEVebfWpCbLrrrAEKYLX10+l3xlGLfEpVNtq4jbpCMnSvW8S/BlBntdNTeupERz72j7eXV6cc1/nl" +
                "b1fq5H6tRvzq4ZSRHYfpI+WXpXZqSwBk5iIOB8JRdJlI/AsGKNkX4T58D9RsMaOV3u0pJKBtdXnHCT+I" +
                "AfY/6rKZaYqqd+ZTT3ZUpkLxzI5J2OIOnDjdEuxvwERDuqKgYkclm0ShJ5mWFCIaGgNEfpJRpfAoN6Fs" +
                "gUB/RdVG/WDtExuggzLvSLVgQNc4SO+Jg94eXPkI0QLCkTNz9W8aRD+7IetoYhI16YPGdAh6jGs5gYVI" +
                "nHqnQ2K7R8Fs1JRr7bVlZhBFq+8oO032yN1CRr5j7rXdqPcNOUWbeqNG3y9KlyycB+AQyMEdIvWl0VwW" +
                "6QJgQe5wkbJZkqyBP5tz3IraLo0cFnCYvZwNM5aOc4+N7201MTfzFM1nSBQps8AEZ6bM5OJ5h5oPOAVp" +
                "LhpYwjwih1WwBJ2bqUwAyxXcFMXPWRtZIkWBlkX3fviY3W/ZPRY1+ZZSGG/bWMevb2YJw2qR8xOjAQVl" +
                "A/7/6N17oQkvM19F8f1//FP8+u6nC6RY5fNyikga08dVOlQgP+lKJy2yakzdUDiztCfLvLQd6ixv09gx" +
                "Rwfqa3IUtAXXPXc8yC9926IApbAOdT/wzw2iRbOm7C2mTenRFcbJbA1auoHNIqGEriR1fXkh7UBln8xe" +
                "OtuVgXTkmXp9qYoeZTl/zQ5qpT689fGbj8XqZvBneE41KrlEkQcOoj4eURc47Kuc5QaHgCX0Kc/WE3l2" +
                "i9t4Cv1xLNR5qPkEKbwZMficSGivg9FbdCeAS1AB1Ffs9Or0CNkJtNPOz/AZ8XDGl8C6BZdzOmtQPMs0" +
                "xL4GkzDsgt+bKo88kTeWADRvzTboMBYy4eTIYnUVZP5wHaU0efUcd/Uk/VyWW1P9X7L8h2aaVRbmTTxN" +
                "6WXbbSkNhHGVBv9ERTJWeA9jxusSoir+RJv7cJ79bV42v/dwCI73UvB5079MklMwz6So1V7ePYpfLd8H" +
                "Muha0igrum3xhGNleJQhhw3P/UAgCTPTJFV58OF8kuWKDQD7KFuJvxLGeUdnTvgxXE54B6yxCsGvWLEQ" +
                "pH+l4/EJEUxtqsfbXb5HpuTWKu1eQ0iY9BJzPgwlBMjM9ulGXe9kbwyckOycPGhkZc5xSR8k79c8ZSaI" +
                "h4S+kdk+rxDj8KWkK1R9Z71O332r7percbn6/CKlPmjsuWrjayGY5SvzQc357tNBoEzyvyY0Xw0v1Kuy" +
                "y6a05qEaoWCLOZLGR/lsg78jTlIkFjGVHGFs8ZegdrUsA94L2C9zr04mh/vJ7mWyy1v5maqhFLk8h+TW" +
                "aCoEL7OTE+RF92UpCtjhFn+x+Irib7w5FE2CDAAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<MultiDOFJointState> CreateSerializer() => new Serializer();
        public Deserializer<MultiDOFJointState> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<MultiDOFJointState>
        {
            public override void RosSerialize(MultiDOFJointState msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(MultiDOFJointState msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(MultiDOFJointState msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(MultiDOFJointState msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(MultiDOFJointState msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<MultiDOFJointState>
        {
            public override void RosDeserialize(ref ReadBuffer b, out MultiDOFJointState msg) => msg = new MultiDOFJointState(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out MultiDOFJointState msg) => msg = new MultiDOFJointState(ref b);
        }
    }
}
