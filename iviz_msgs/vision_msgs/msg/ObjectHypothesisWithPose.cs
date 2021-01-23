/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisionMsgs
{
    [Preserve, DataContract (Name = "vision_msgs/ObjectHypothesisWithPose")]
    public sealed class ObjectHypothesisWithPose : IDeserializable<ObjectHypothesisWithPose>, IMessage
    {
        // An object hypothesis that contains position information.
        // The unique ID of the object class. To get additional information about
        //   this ID, such as its human-readable class name, listeners should perform a
        //   lookup in a metadata database. See vision_msgs/VisionInfo.msg for more detail.
        [DataMember (Name = "id")] public string Id { get; set; }
        // The probability or confidence value of the detected object. By convention,
        //   this value should lie in the range [0-1].
        [DataMember (Name = "score")] public double Score { get; set; }
        // The 6D pose of the object hypothesis. This pose should be
        //   defined as the pose of some fixed reference point on the object, such a
        //   the geometric center of the bounding box or the center of mass of the
        //   object.
        // Note that this pose is not stamped; frame information can be defined by
        //   parent messages.
        // Also note that different classes predicted for the same input data may have
        //   different predicted 6D poses.
        [DataMember (Name = "pose")] public GeometryMsgs.PoseWithCovariance Pose { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public ObjectHypothesisWithPose()
        {
            Id = "";
            Pose = new GeometryMsgs.PoseWithCovariance();
        }
        
        /// <summary> Explicit constructor. </summary>
        public ObjectHypothesisWithPose(string Id, double Score, GeometryMsgs.PoseWithCovariance Pose)
        {
            this.Id = Id;
            this.Score = Score;
            this.Pose = Pose;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public ObjectHypothesisWithPose(ref Buffer b)
        {
            Id = b.DeserializeString();
            Score = b.Deserialize<double>();
            Pose = new GeometryMsgs.PoseWithCovariance(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new ObjectHypothesisWithPose(ref b);
        }
        
        ObjectHypothesisWithPose IDeserializable<ObjectHypothesisWithPose>.RosDeserialize(ref Buffer b)
        {
            return new ObjectHypothesisWithPose(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(Id);
            b.Serialize(Score);
            Pose.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Id is null) throw new System.NullReferenceException(nameof(Id));
            if (Pose is null) throw new System.NullReferenceException(nameof(Pose));
            Pose.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 356;
                size += BuiltIns.UTF8.GetByteCount(Id);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "vision_msgs/ObjectHypothesisWithPose";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "33f27ec2eb451c31670b819827352622";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71VS2/TQBC+W+p/GKkXkJwAAuVQxKFQCeUAAlrxqio09o7jLfauu7NO4/56Zr1+pA/B" +
                "BTWHZL2e+Wa+bx45hGMDNruk3EPZNdaXxJrBl+ght8ajNgyNZe21NaBNYV2N4bw8SA6SQzgrCVqjr1qC" +
                "9QnYQjxpxMsrZF7CmYUNeUClehCs9mEAM9v6gATiKpHXJylwm5eADNozlG2NZuEIFWYVRUwwWFMKlWZP" +
                "hhwDl7atFDTkAjBgxKus/d02Eg0QavKC4BHCV4ZMSzglgq1mSeJXzRt+9rU/ryW3pTyDIEFtHYESV10J" +
                "X/ZOmw1oNVNvnM0w05X2HYi9KFZoRSYXZKxEk0EQgRBBSA3KLOFtF2y3ZIIG6R796DbwqTSF7AOCQ7Mh" +
                "OH++eHEhmRSVRb96BZxLgnM2q5NQKrpThrmsUosQo7cZQmQUgysqtJEEkXvXEYZtTVDonbxxVJDrqTVW" +
                "Gw/W7AUZazYyISm5uIpgOeTCktyYlJTbqCBjZndBsnA3W9ShutEyQg2ChYeP1lNsTD+xkF9jPbDHuiH1" +
                "GgonnXGrv3I0QnLil3URt0Hh4qUtmHFD3Ac4rtgGuCGK0kXPeGhkkpCOlO7rWAyZcwzXtL5vLMm/gxK3" +
                "o6gTwuw51ChEHCTqYvt9kttv2pfv7Badxig0C1Ly5j9/kg+n74/gX9H7nhJ9HUnyLCRYxiiKbkRnGR5u" +
                "UJK8Fi9ZATm5sCx8t0ySANabJgLyxV4varwUwSakWJmhIVa7lczCxFnK5vRuaGjr9GQuFROxfRj3VuAx" +
                "tuUCd/s5xtUk3mvBd4pcGtt59pW6H8n7J7sUuhRuUnDW720i+A4B8d71j4evf/bXT8eBPH+5utgj83il" +
                "C937gL73y5VKenW4VsP7uNjRqH2xlyA1DEM+GiSfWxT5TI872z0WQUllbMfpT2lYU3rkisNeukV32pS7" +
                "6dRNp5vHSX+W7qGRuqXnndGSp6tZ97DTZLj+zmg8XSfJHyWDuVnbBwAA";
                
    }
}
