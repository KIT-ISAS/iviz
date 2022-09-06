/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class Trajectory : IDeserializable<Trajectory>, IHasSerializer<Trajectory>, IMessage
    {
        [DataMember (Name = "poses")] public GeometryMsgs.Pose[] Poses;
        [DataMember (Name = "timestamps")] public time[] Timestamps;
    
        public Trajectory()
        {
            Poses = EmptyArray<GeometryMsgs.Pose>.Value;
            Timestamps = EmptyArray<time>.Value;
        }
        
        public Trajectory(GeometryMsgs.Pose[] Poses, time[] Timestamps)
        {
            this.Poses = Poses;
            this.Timestamps = Timestamps;
        }
        
        public Trajectory(ref ReadBuffer b)
        {
            unsafe
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<GeometryMsgs.Pose>.Value
                    : new GeometryMsgs.Pose[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), n * 56);
                }
                Poses = array;
            }
            unsafe
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<time>.Value
                    : new time[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), n * 8);
                }
                Timestamps = array;
            }
        }
        
        public Trajectory(ref ReadBuffer2 b)
        {
            unsafe
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<GeometryMsgs.Pose>.Value
                    : new GeometryMsgs.Pose[n];
                if (n != 0)
                {
                    b.Align8();
                    b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), n * 56);
                }
                Poses = array;
            }
            unsafe
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<time>.Value
                    : new time[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), n * 8);
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
        public const string Md5Sum = "106d0bcefed39b91bf6dcd161d3c7864";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE72RPwvCMBDF9/sUB65SF3EQHJycBEU3EQl6rQGT1NyJfz69l2Jbi4OLmCX3kkvyey8F" +
                "BUcS7zvHBQ8WgWmzxVInBrEuiTSxGFcyAEx+PGC+mo2x+ICAHk4xUhmJyYsRGzyGvAJD6zGPRMil2VMf" +
                "98Gl5cNr31a9xquOtj6bISyC9dI0wPJihKKv7m37/mVQUdTh+mhZ8fVt6xnlSC2/ejGqEnLHLuSnYGQ0" +
                "xFtT3Zvq8R/8NrraQ/NRrMG/59mFT+rc5p6H6DL44qiurgBPjHOw/qsCAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<Trajectory> CreateSerializer() => new Serializer();
        public Deserializer<Trajectory> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<Trajectory>
        {
            public override void RosSerialize(Trajectory msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(Trajectory msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(Trajectory msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(Trajectory msg) => msg.Ros2MessageLength;
            public override void RosValidate(Trajectory msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<Trajectory>
        {
            public override void RosDeserialize(ref ReadBuffer b, out Trajectory msg) => msg = new Trajectory(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out Trajectory msg) => msg = new Trajectory(ref b);
        }
    }
}
