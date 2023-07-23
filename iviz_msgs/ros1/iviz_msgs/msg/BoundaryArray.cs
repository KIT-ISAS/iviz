/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class BoundaryArray : IHasSerializer<BoundaryArray>, IMessage
    {
        [DataMember (Name = "boundaries")] public Boundary[] Boundaries;
    
        public BoundaryArray()
        {
            Boundaries = EmptyArray<Boundary>.Value;
        }
        
        public BoundaryArray(Boundary[] Boundaries)
        {
            this.Boundaries = Boundaries;
        }
        
        public BoundaryArray(ref ReadBuffer b)
        {
            {
                int n = b.DeserializeArrayLength();
                Boundary[] array;
                if (n == 0) array = EmptyArray<Boundary>.Value;
                else
                {
                    array = new Boundary[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new Boundary(ref b);
                    }
                }
                Boundaries = array;
            }
        }
        
        public BoundaryArray(ref ReadBuffer2 b)
        {
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                Boundary[] array;
                if (n == 0) array = EmptyArray<Boundary>.Value;
                else
                {
                    array = new Boundary[n];
                    for (int i = 0; i < n; i++)
                    {
                        array[i] = new Boundary(ref b);
                    }
                }
                Boundaries = array;
            }
        }
        
        public BoundaryArray RosDeserialize(ref ReadBuffer b) => new BoundaryArray(ref b);
        
        public BoundaryArray RosDeserialize(ref ReadBuffer2 b) => new BoundaryArray(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Boundaries.Length);
            foreach (var t in Boundaries)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align4();
            b.Serialize(Boundaries.Length);
            foreach (var t in Boundaries)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Boundaries, nameof(Boundaries));
            foreach (var msg in Boundaries) msg.RosValidate();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 4;
                foreach (var msg in Boundaries) size += msg.RosMessageLength;
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size += 4; // Boundaries.Length
            foreach (var msg in Boundaries) size = msg.AddRos2MessageLength(size);
            return size;
        }
    
        public const string MessageType = "iviz_msgs/BoundaryArray";
    
        [IgnoreDataMember] public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "0d02a550d3d1e9e0e885f27019d35557";
    
        [IgnoreDataMember] public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        [IgnoreDataMember]
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71VbW/aSBD+7l8xUj40OSXcNa1OVaR+IEATSyRQQiNVVYUWe7BXZ++6u2so/fX37NoY" +
                "aCP1PlyCeBlmZ56dZ958rWuVCrP98pWWjSjZRtH7//kV3T3cXJFcyx+L0mb2z+v22qiWyr2j/mAeT+4X" +
                "/eGQ3tNfx8rZ6G7yOIL+9VP6/niMo8uoPZt/no4WD/HddDw6QAraQTwbjEeL2/jmdozP/ACx8fr4qT87" +
                "Pu9gr0e3/cd4MlvcT+4PgTv9YDIex8PR7AD0wGcef/jcmDwg8gY4si5tcnHLImVDefhpnUXipFawMVJl" +
                "JNNW7bYVt+KSc7GW2kQZ65Kd2TZgU22ZKnz9pH/kxGnzhmwiCt7fPdCFNrOb6z4lXnrqwHKiVbrYnYeA" +
                "ElGF+J6pT35KTXRCD074fkkJlEQqnKCVRspklrO5KHjNBZxEWXFK4dQnyvbgOM+lJbwzVmxEUWyptjBy" +
                "GoTLslYyEY7JyZKP/OEpFQmqhHEyqQthYK9NKpU3XxlRskfH2/K3mlXCFA+vYKOQrtpJBLQFQmJYWJ+w" +
                "eEihbm8uvQOd0JeZtq+/Rifzjb6AnjN0QBcFuVw4HzV/rwxbH7CwV7jsj4ZlD5dctYWxdBp0C/y1Z4Tb" +
                "EAtXOsnpFBSmW5drBUCmNWZbLAv2wOiCAqivvNOrswNkFaCVUHoH3yDu7/gvsKrD9ZwuchSv8GmwdYZM" +
                "wrAyei1TmC63ASQpJCtHhVwavxW8V3NldPLBJxtG8Aqlwa+wVicSlUhpI12+68pQlgWG5Zna8tdJA8E+" +
                "GfZFQvjCzwTpVZg/3z8rw6BRiYTPfbt5ddqey2CLvJA2cufbo2iq0Q2dQfSxBkujAu7e7qUIIpTdCKEX" +
                "nJDKhmp18YMLZiSEfEQ3WhVauL/f0vdO2nbSj5cJf5+6HYeuUOigo3weB+//fdvnHYum7EW/YbSTNi/D" +
                "rd3mTxGjdTg7ptTzmyoOK0UrbKaSBUqGJdh5wjGVhpOmDedYqwzi6FvpKNVsSWnfC6X4B5CM+fbeoqoA" +
                "hm1rhLJFk0qo4XLKvax3TpucVWPl5zOs1bCIZUJGZjJtPH2GO2dBLblzcqtLzHdRNDE3l6H9AGJ0U7iz" +
                "HsUr2uqaNp4QBNPuf40HZBdXWE9O63O//FuIJ3odabFWZL4BrMOT57dVf55S//oMbq7Es8N0UtZJy04S" +
                "UfQvvYVjwc8JAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<BoundaryArray> CreateSerializer() => new Serializer();
        public Deserializer<BoundaryArray> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<BoundaryArray>
        {
            public override void RosSerialize(BoundaryArray msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(BoundaryArray msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(BoundaryArray msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(BoundaryArray msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(BoundaryArray msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<BoundaryArray>
        {
            public override void RosDeserialize(ref ReadBuffer b, out BoundaryArray msg) => msg = new BoundaryArray(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out BoundaryArray msg) => msg = new BoundaryArray(ref b);
        }
    }
}
