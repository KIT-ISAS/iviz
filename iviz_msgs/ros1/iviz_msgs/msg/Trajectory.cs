/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class Trajectory : IDeserializable<Trajectory>, IMessage
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
                Poses = n == 0
                    ? EmptyArray<GeometryMsgs.Pose>.Value
                    : new GeometryMsgs.Pose[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(Unsafe.AsPointer(ref Poses[0]), n * 56);
                }
            }
            unsafe
            {
                int n = b.DeserializeArrayLength();
                Timestamps = n == 0
                    ? EmptyArray<time>.Value
                    : new time[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(Unsafe.AsPointer(ref Timestamps[0]), n * 8);
                }
            }
        }
        
        public Trajectory(ref ReadBuffer2 b)
        {
            unsafe
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                Poses = n == 0
                    ? EmptyArray<GeometryMsgs.Pose>.Value
                    : new GeometryMsgs.Pose[n];
                if (n != 0)
                {
                    b.Align8();
                    b.DeserializeStructArray(Unsafe.AsPointer(ref Poses[0]), n * 56);
                }
            }
            unsafe
            {
                int n = b.DeserializeArrayLength();
                Timestamps = n == 0
                    ? EmptyArray<time>.Value
                    : new time[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(Unsafe.AsPointer(ref Timestamps[0]), n * 8);
                }
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
    
        public int RosMessageLength => 8 + 56 * Poses.Length + 8 * Timestamps.Length;
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = WriteBuffer2.Align4(c);
            c += 4; // Poses length
            c = WriteBuffer2.Align8(c);
            c += 56 * Poses.Length;
            c += 4; // Timestamps length
            c += 8 * Timestamps.Length;
            return c;
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
    }
}
