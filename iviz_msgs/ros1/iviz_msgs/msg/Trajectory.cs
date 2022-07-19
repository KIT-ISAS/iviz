/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class Trajectory : IDeserializableRos1<Trajectory>, IDeserializableRos2<Trajectory>, IMessageRos1, IMessageRos2
    {
        [DataMember (Name = "poses")] public GeometryMsgs.Pose[] Poses;
        [DataMember (Name = "timestamps")] public time[] Timestamps;
    
        /// Constructor for empty message.
        public Trajectory()
        {
            Poses = System.Array.Empty<GeometryMsgs.Pose>();
            Timestamps = System.Array.Empty<time>();
        }
        
        /// Explicit constructor.
        public Trajectory(GeometryMsgs.Pose[] Poses, time[] Timestamps)
        {
            this.Poses = Poses;
            this.Timestamps = Timestamps;
        }
        
        /// Constructor with buffer.
        public Trajectory(ref ReadBuffer b)
        {
            b.DeserializeStructArray(out Poses);
            b.DeserializeStructArray(out Timestamps);
        }
        
        /// Constructor with buffer.
        public Trajectory(ref ReadBuffer2 b)
        {
            b.DeserializeStructArray(out Poses);
            b.DeserializeStructArray(out Timestamps);
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new Trajectory(ref b);
        
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
        
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            WriteBuffer2.AddLength(ref c, Poses);
            WriteBuffer2.AddLength(ref c, Timestamps);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "iviz_msgs/Trajectory";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "106d0bcefed39b91bf6dcd161d3c7864";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE72RPwvCMBDF9/sUB65SF3EQHJycBEU3EQl6rQGT1NyJfz69l2Jbi4OLmCX3kkvyey8F" +
                "BUcS7zvHBQ8WgWmzxVInBrEuiTSxGFcyAEx+PGC+mo2x+ICAHk4xUhmJyYsRGzyGvAJD6zGPRMil2VMf" +
                "98Gl5cNr31a9xquOtj6bISyC9dI0wPJihKKv7m37/mVQUdTh+mhZ8fVt6xnlSC2/ejGqEnLHLuSnYGQ0" +
                "xFtT3Zvq8R/8NrraQ/NRrMG/59mFT+rc5p6H6DL44qiurgBPjHOw/qsCAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
