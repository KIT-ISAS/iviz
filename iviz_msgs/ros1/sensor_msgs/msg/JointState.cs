/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [DataContract]
    public sealed class JointState : IHasSerializer<JointState>, IMessage
    {
        // This is a message that holds data to describe the state of a set of torque controlled joints. 
        //
        // The state of each joint (revolute or prismatic) is defined by:
        //  * the position of the joint (rad or m),
        //  * the velocity of the joint (rad/s or m/s) and 
        //  * the effort that is applied in the joint (Nm or N).
        //
        // Each joint is uniquely identified by its name
        // The header specifies the time at which the joint states were recorded. All the joint states
        // in one message have to be recorded at the same time.
        //
        // This message consists of a multiple arrays, one for each part of the joint state. 
        // The goal is to make each of the fields optional. When e.g. your joints have no
        // effort associated with them, you can leave the effort array empty. 
        //
        // All arrays in this message should have the same size, or be empty.
        // This is the only way to uniquely associate the joint name with the correct
        // states.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "name")] public string[] Name;
        [DataMember (Name = "position")] public double[] Position;
        [DataMember (Name = "velocity")] public double[] Velocity;
        [DataMember (Name = "effort")] public double[] Effort;
    
        public JointState()
        {
            Name = EmptyArray<string>.Value;
            Position = EmptyArray<double>.Value;
            Velocity = EmptyArray<double>.Value;
            Effort = EmptyArray<double>.Value;
        }
        
        public JointState(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Name = b.DeserializeStringArray();
            {
                int n = b.DeserializeArrayLength();
                double[] array;
                if (n == 0) array = EmptyArray<double>.Value;
                else
                {
                     array = new double[n];
                    b.DeserializeStructArray(array);
                }
                Position = array;
            }
            {
                int n = b.DeserializeArrayLength();
                double[] array;
                if (n == 0) array = EmptyArray<double>.Value;
                else
                {
                     array = new double[n];
                    b.DeserializeStructArray(array);
                }
                Velocity = array;
            }
            {
                int n = b.DeserializeArrayLength();
                double[] array;
                if (n == 0) array = EmptyArray<double>.Value;
                else
                {
                     array = new double[n];
                    b.DeserializeStructArray(array);
                }
                Effort = array;
            }
        }
        
        public JointState(ref ReadBuffer2 b)
        {
            Header = new StdMsgs.Header(ref b);
            b.Align4();
            Name = b.DeserializeStringArray();
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                double[] array;
                if (n == 0) array = EmptyArray<double>.Value;
                else
                {
                     array = new double[n];
                    b.Align8();
                    b.DeserializeStructArray(array);
                }
                Position = array;
            }
            {
                int n = b.DeserializeArrayLength();
                double[] array;
                if (n == 0) array = EmptyArray<double>.Value;
                else
                {
                     array = new double[n];
                    b.Align8();
                    b.DeserializeStructArray(array);
                }
                Velocity = array;
            }
            {
                int n = b.DeserializeArrayLength();
                double[] array;
                if (n == 0) array = EmptyArray<double>.Value;
                else
                {
                     array = new double[n];
                    b.Align8();
                    b.DeserializeStructArray(array);
                }
                Effort = array;
            }
        }
        
        public JointState RosDeserialize(ref ReadBuffer b) => new JointState(ref b);
        
        public JointState RosDeserialize(ref ReadBuffer2 b) => new JointState(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Name.Length);
            b.SerializeArray(Name);
            b.Serialize(Position.Length);
            b.SerializeStructArray(Position);
            b.Serialize(Velocity.Length);
            b.SerializeStructArray(Velocity);
            b.Serialize(Effort.Length);
            b.SerializeStructArray(Effort);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Align4();
            b.Serialize(Name.Length);
            b.SerializeArray(Name);
            b.Align4();
            b.Serialize(Position.Length);
            b.Align8();
            b.SerializeStructArray(Position);
            b.Serialize(Velocity.Length);
            b.Align8();
            b.SerializeStructArray(Velocity);
            b.Serialize(Effort.Length);
            b.Align8();
            b.SerializeStructArray(Effort);
        }
        
        public void RosValidate()
        {
            if (Name is null) BuiltIns.ThrowNullReference(nameof(Name));
            for (int i = 0; i < Name.Length; i++)
            {
                if (Name[i] is null) BuiltIns.ThrowNullReference(nameof(Name), i);
            }
            if (Position is null) BuiltIns.ThrowNullReference(nameof(Position));
            if (Velocity is null) BuiltIns.ThrowNullReference(nameof(Velocity));
            if (Effort is null) BuiltIns.ThrowNullReference(nameof(Effort));
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 16;
                size += Header.RosMessageLength;
                size += WriteBuffer.GetArraySize(Name);
                size += 8 * Position.Length;
                size += 8 * Velocity.Length;
                size += 8 * Effort.Length;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Header.AddRos2MessageLength(size);
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Name);
            size = WriteBuffer2.Align4(size);
            size += 4; // Position.Length
            size = WriteBuffer2.Align8(size);
            size += 8 * Position.Length;
            size += 4; // Velocity.Length
            size = WriteBuffer2.Align8(size);
            size += 8 * Velocity.Length;
            size += 4; // Effort.Length
            size = WriteBuffer2.Align8(size);
            size += 8 * Effort.Length;
            return size;
        }
    
        public const string MessageType = "sensor_msgs/JointState";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "3066dcd76a6cfaef579bd0f34173e9fd";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE61Uy27bMBC86ysW8CFx4SjoAz0E6KFAn4cGRROghyAI1uJaYkuRKknZVb++Q9K2HOTS" +
                "Qw0dLGp3dndmuAu67XQgPEy9hMCtUOw4UueMCqQ4MkVHSkLj9Tp9EwqRo5DbICVITH+i879GocbZ6J0x" +
                "ouiH0zaGmqpFtUCJkyThpiuf6dzL1pkxnXsavA49R90sUzdKNtoCZz1dAYCe5cKDCzpqZ3NJvB9QWCWA" +
                "frmaQ7diXKPj9DT0MuTgy7AktormFNlsnI9l+sTHMBiNDrQ9BbjuU/b1ss6DvZ9nQcZoNVgwE2klNuqN" +
                "zv2TjoEs97InohNW4ikM0qSQkNGj7oVQd9dpIM71MmuBduKFvDTOK1E1vTXmSQzQ0amzcpSx460k7dZz" +
                "aiqRFUQ7uWa91wfdH9IgYtABPWeB+9FEPRg05z1PYZUrgKci48A+PmY4N5Nkz7O2jk1iBl30/FNK0j4e" +
                "syeHuSEpyqam751YkrqtaXKj3zuoTGEdAPf6cAhQFlUU7XTMZPWrlEINWzKSp57lzH2T9EOc9m5M7JVp" +
                "irYns4fOjUbtmTvwFPQfWSXVQWTBOVCmi3jOQvMdqmDMoweObZ6Qk1xwbBpEe+gSAVYUrKuq+lTMUTxS" +
                "VSF6bdu7++KfjXEcX7/C6+EinBwdDH9yVBioqjf/+Vd9ufl4habVQx/acFl6xhg3EReKvQKdkfPmSEbp" +
                "dNuJvzCCDtOk/QDlyl6ZhjT1TGYrVjwbsDcGBIHOxvU9KG0yj/Dro/xiec4u1M1o2CMePtc2hW88Z3+n" +
                "sCAQxTZCn99dZYNLM0a9zXfVNl44gGZ8pGqETi9fpARa0N03F57fV4vbnbvAubSQ5tjFcVHI78HDP+l2" +
                "hbSsnpUpaxQBS7h5FjY/z2cPeMXeQTX0IoPDbTjHCF+n2LmyZrbsNa9x3wDccF6lZynpbHmCbDO0ZesO" +
                "8AVxrvEvsPaIm2a66CCeSTSEsQWTCBy822pVllg2LPYhXGz02rOfqryzcslq8cHnjZJ0zNKk/fn4nu69" +
                "XGR50Kqq/gLoi6OzewYAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<JointState> CreateSerializer() => new Serializer();
        public Deserializer<JointState> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<JointState>
        {
            public override void RosSerialize(JointState msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(JointState msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(JointState msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(JointState msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(JointState msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<JointState>
        {
            public override void RosDeserialize(ref ReadBuffer b, out JointState msg) => msg = new JointState(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out JointState msg) => msg = new JointState(ref b);
        }
    }
}
