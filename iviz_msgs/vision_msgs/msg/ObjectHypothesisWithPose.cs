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
                "H4sIAAAAAAAACr1VTW/TQBC9R8p/GCkXkJwAAuVQxKFQCeUAAlqVj6pCY+843mLvurvrNO6vZ8brj5RW" +
                "6gU1h2S9nn0z773ZyQKODdj0irIARVvbUJDXHkKBATJrAmrjobZeB20NaJNbV6GsV/PZfLaAs4KgMfq6" +
                "IdicgM35JA14WYner+DMwpYCoFIdCJaHMICpbYIgAR/lzJuTBHyTFYAedPBQNBWapSNUmJYUMcFgRQmU" +
                "2gcy5Dz4wjalgpqcAANGvNLaP03N2QChosAIAUG+UvS0glMi2GnPRfyu/Na/OO/WG65txc/ASFBZR6D4" +
                "qC6Zrw9Omy1oNVGvnU0x1aUOLXA8K5ZrRSZjZCxZk14QhmBBSPXKrOB9K7E7MqJBckA/Huv5lJqkekFw" +
                "aLYEFy+Xry65kry0GNZvwGdc4FTN+kSsGrPes5W9kBxdTJ8ipZhcUa4NF8iqy9EBxtuKINd7fuMoJ9dR" +
                "q602Adi7Kcng2cCE2HI+yoJlkDFLckNRbLdRImNq9yKZ7E0RlbgbIyNUL5g8fLaBYmN2SnUl8q+xAXzA" +
                "qib1FnLHnXGnvzI0THLkl7YRt0bmErgtvMct+S7BcemtwPVZlM47xn0jE6d0pHTnY95X7mO6uuFw6a4K" +
                "WyhwN4g6Ikwne48kYy9RG9vvC+9+16H4YHfoNEahfefuu//8mc8+nX48gsfyx7ZiiR1x/Z55eL5JUXfD" +
                "UvP98TVynTd8jKdARk7mRWi72SB4I4MFfLM3ywqvWLcRLRrU98V6v+YrMVJn95zeD41tnR7j2TkWnfvF" +
                "Q8MZMLbnEveHhQ4jagEbzuAUuST29XSYG+BIAp7tE2gTuE3A2XAwk+AHCOa97Z8Pb//qtp+Pd/Pi9fry" +
                "gNCT2tg18wM637cu4RIr2Vb9+zjn0ahDzVcgdsqlHyLms68NsoymQ54in5AmlzM26PhP1c+uyIIZSbtK" +
                "3XdIT/NzPy15LgzL2ydjMYn44F27I+0/d46fricLZN51t+5RZsPyRqL/AhNWxFb+BwAA";
                
    }
}
