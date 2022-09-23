/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class Trajectory : IHasSerializer<Trajectory>, IMessage
    {
        [DataMember (Name = "poses")] public GeometryMsgs.Pose[] Poses;
        [DataMember (Name = "timestamps")] public duration[] Timestamps;
    
        public Trajectory()
        {
            Poses = EmptyArray<GeometryMsgs.Pose>.Value;
            Timestamps = EmptyArray<duration>.Value;
        }
        
        public Trajectory(GeometryMsgs.Pose[] Poses, duration[] Timestamps)
        {
            this.Poses = Poses;
            this.Timestamps = Timestamps;
        }
        
        public Trajectory(ref ReadBuffer b)
        {
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<GeometryMsgs.Pose>.Value
                    : new GeometryMsgs.Pose[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(array);
                }
                Poses = array;
            }
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<duration>.Value
                    : new duration[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(array);
                }
                Timestamps = array;
            }
        }
        
        public Trajectory(ref ReadBuffer2 b)
        {
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<GeometryMsgs.Pose>.Value
                    : new GeometryMsgs.Pose[n];
                if (n != 0)
                {
                    b.Align8();
                    b.DeserializeStructArray(array);
                }
                Poses = array;
            }
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<duration>.Value
                    : new duration[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(array);
                }
                Timestamps = array;
            }
        }
        
        public Trajectory RosDeserialize(ref ReadBuffer b) => new Trajectory(ref b);
        
        public Trajectory RosDeserialize(ref ReadBuffer2 b) => new Trajectory(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.SerializeStructArray(Poses);
            b.SerializeStructArray(Timestamps);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.SerializeStructArray(Poses);
            b.SerializeStructArray(Timestamps);
        }
        
        public void RosValidate()
        {
            if (Poses is null) BuiltIns.ThrowNullReference();
            if (Timestamps is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 8;
                size += 56 * Poses.Length;
                size += 8 * Timestamps.Length;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = WriteBuffer2.Align4(size);
            size += 4; // Poses.Length
            size = WriteBuffer2.Align8(size);
            size += 56 * Poses.Length;
            size += 4; // Timestamps.Length
            size += 8 * Timestamps.Length;
            return size;
        }
    
        public const string MessageType = "iviz_msgs/Trajectory";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "f435db5957a1950f948a0591eb95bfa2";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE72RwQrCMAyG73mKgFfRi3gQPHjyJCh6E5Gi2Vaw7WwydD692XCb4sGLrJcmadJ+/9+U" +
                "giOJ5dFxyuN1YNofMNeN4VxEIzZ4LYh1xGJczgAw//OC1XY5w/QLBAa4wEh5JCYvNQqGpIZD6zGJRMi5" +
                "OdEQT8FV5fPr3Na9xmsebTM7QlgH66VtgE1hhKKv7+36+hKoKKpwl1lWfH3bekbJqONXLUazCvlDLiSX" +
                "YGQ6wXsblW306Ae/s67R0H4Uq/Hvfn7CV9m18z0J0Y3gh6ImugE8AXzpGQKvAgAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<Trajectory> CreateSerializer() => new Serializer();
        public Deserializer<Trajectory> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<Trajectory>
        {
            public override void RosSerialize(Trajectory msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(Trajectory msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(Trajectory msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(Trajectory msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(Trajectory msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<Trajectory>
        {
            public override void RosDeserialize(ref ReadBuffer b, out Trajectory msg) => msg = new Trajectory(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out Trajectory msg) => msg = new Trajectory(ref b);
        }
    }
}
