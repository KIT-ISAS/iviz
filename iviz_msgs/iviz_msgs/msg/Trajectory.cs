/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [Preserve, DataContract (Name = "iviz_msgs/Trajectory")]
    public sealed class Trajectory : IDeserializable<Trajectory>, IMessage
    {
        [DataMember (Name = "poses")] public GeometryMsgs.Pose[] Poses;
        [DataMember (Name = "timestamps")] public time[] Timestamps;
    
        /// <summary> Constructor for empty message. </summary>
        public Trajectory()
        {
            Poses = System.Array.Empty<GeometryMsgs.Pose>();
            Timestamps = System.Array.Empty<time>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Trajectory(GeometryMsgs.Pose[] Poses, time[] Timestamps)
        {
            this.Poses = Poses;
            this.Timestamps = Timestamps;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Trajectory(ref Buffer b)
        {
            Poses = b.DeserializeStructArray<GeometryMsgs.Pose>();
            Timestamps = b.DeserializeStructArray<time>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Trajectory(ref b);
        }
        
        Trajectory IDeserializable<Trajectory>.RosDeserialize(ref Buffer b)
        {
            return new Trajectory(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeStructArray(Poses, 0);
            b.SerializeStructArray(Timestamps, 0);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Poses is null) throw new System.NullReferenceException(nameof(Poses));
            if (Timestamps is null) throw new System.NullReferenceException(nameof(Timestamps));
        }
    
        public int RosMessageLength => 8 + 56 * Poses.Length + 8 * Timestamps.Length;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "iviz_msgs/Trajectory";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "106d0bcefed39b91bf6dcd161d3c7864";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE72RPwvCMBDF9/sUB65SF3EQHJycBEU3EQl6rQGT1NyJfz69l2Jbi4OLmCX3kkvyey8F" +
                "BUcS7zvHBQ8WgWmzxVInBrEuiTSxGFcyAEx+PGC+mo2x+ICAHk4xUhmJyYsRGzyGvAJD6zGPRMil2VMf" +
                "98Gl5cNr31a9xquOtj6bISyC9dI0wPJihKKv7m37/mVQUdTh+mhZ8fVt6xnlSC2/ejGqEnLHLuSnYGQ0" +
                "xFtT3Zvq8R/8NrraQ/NRrMG/59mFT+rc5p6H6DL44qiurgBPjHOw/qsCAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
