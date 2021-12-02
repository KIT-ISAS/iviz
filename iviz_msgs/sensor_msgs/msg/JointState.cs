/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class JointState : IDeserializable<JointState>, IMessage
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
    
        /// Constructor for empty message.
        public JointState()
        {
            Name = System.Array.Empty<string>();
            Position = System.Array.Empty<double>();
            Velocity = System.Array.Empty<double>();
            Effort = System.Array.Empty<double>();
        }
        
        /// Explicit constructor.
        public JointState(in StdMsgs.Header Header, string[] Name, double[] Position, double[] Velocity, double[] Effort)
        {
            this.Header = Header;
            this.Name = Name;
            this.Position = Position;
            this.Velocity = Velocity;
            this.Effort = Effort;
        }
        
        /// Constructor with buffer.
        internal JointState(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Name = b.DeserializeStringArray();
            Position = b.DeserializeStructArray<double>();
            Velocity = b.DeserializeStructArray<double>();
            Effort = b.DeserializeStructArray<double>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new JointState(ref b);
        
        JointState IDeserializable<JointState>.RosDeserialize(ref Buffer b) => new JointState(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeArray(Name);
            b.SerializeStructArray(Position);
            b.SerializeStructArray(Velocity);
            b.SerializeStructArray(Effort);
        }
        
        public void RosValidate()
        {
            if (Name is null) throw new System.NullReferenceException(nameof(Name));
            for (int i = 0; i < Name.Length; i++)
            {
                if (Name[i] is null) throw new System.NullReferenceException($"{nameof(Name)}[{i}]");
            }
            if (Position is null) throw new System.NullReferenceException(nameof(Position));
            if (Velocity is null) throw new System.NullReferenceException(nameof(Velocity));
            if (Effort is null) throw new System.NullReferenceException(nameof(Effort));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 16;
                size += Header.RosMessageLength;
                size += BuiltIns.GetArraySize(Name);
                size += 8 * Position.Length;
                size += 8 * Velocity.Length;
                size += 8 * Effort.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "sensor_msgs/JointState";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "3066dcd76a6cfaef579bd0f34173e9fd";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACq1Uy24UMRC8+yta2kOyKJlIgDisxAGJ54EIKZE4IBR5x70zBo892J5dhq+n2t5XlAsH" +
                "oj3Enu7q7qpyL+i+t4nw0zRwSrpjyr3O1AdnEhmdNeVAhlMb7Vq+MaWsM1PYICVxln9yiL8mpjb4HINz" +
                "bOhHsD6nhtRCLVDiLIl129fPdBl5G9wk95HGaNOgs22X0o3hjfXAWc8rANCzUngMyWYbfCmJ8wFFGwEY" +
                "llen0C270No8Pw29SSX4Ji1Je0OnFN5sQsx1euFjHJ1FB9afA9wOkn27bMpg706zIGPyFiy4maxhn+1G" +
                "stc45UReD7wnomdtOFIauZWQVNCzHZhQd9dbIJ7qFdYS7TgyRW5DNGwaeuPckxigo9Pg+Shjr7cADrQ+" +
                "pUqJoiDaKTXrGMUChzSImGxCz0XgYXLZjg7NxajndFUqgKcq46hB2COGSzMie5m1C9oJM+hi0D+5Ju3j" +
                "Mbs4LIyiqHYNfe3ZEzddQ3OY4t5BdQofALjXR6cEZVHF0M7mQtZwJSnUak+Oy9QnOUvfxMOY570bhb06" +
                "TdX2bPbUh8mZPXMHnpL9w5g7CpEV50CZTIao4KH5DlUw5tEDxzbPyBEXHJsG0RG6ZIBVBRul1MdqjuoR" +
                "pVKO1nffvlf/bFzQ+dVLHA8P4ezqYPizq8qAUq//85/6fPdhhabNw5C6dFN7xhh3GQ9KRwM6sy6bQ4zS" +
                "267neO0YHcqkwwjl6l6ZR5n6RGbHnqN2YG9KCAKdbRgGUNoWHuHXR/nV8rq40LaT0xHx8Ln1Er6J4EzQ" +
                "hWCGKL5l+vR2VQzO7ZQtGsLr9G1knUAzPpKaoNOL55KgFve7cI0jd1DkWPy4H/j3GGEbeVRJdtSzOlwD" +
                "bJCDB+fh7sty94Aj1g2KoAUeAx7BJTr/Mucey6ysKx2tXuOZAbgFA0C9kKSL5RmytL2CFXw4wFfEU41/" +
                "gRWUiiszXffQzMn0aepAIALHGLbYYGV3FZ9iDcK8zq6jjrMqq6qUVIv3wnF9QkURWZuPn+fewlWNB2uU" +
                "+gsmnvAbcgYAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
