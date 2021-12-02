/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisionMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class ObjectHypothesisWithPose : IDeserializable<ObjectHypothesisWithPose>, IMessage
    {
        // An object hypothesis that contains position information.
        // The unique ID of the object class. To get additional information about
        //   this ID, such as its human-readable class name, listeners should perform a
        //   lookup in a metadata database. See vision_msgs/VisionInfo.msg for more detail.
        [DataMember (Name = "id")] public string Id;
        // The probability or confidence value of the detected object. By convention,
        //   this value should lie in the range [0-1].
        [DataMember (Name = "score")] public double Score;
        // The 6D pose of the object hypothesis. This pose should be
        //   defined as the pose of some fixed reference point on the object, such a
        //   the geometric center of the bounding box or the center of mass of the
        //   object.
        // Note that this pose is not stamped; frame information can be defined by
        //   parent messages.
        // Also note that different classes predicted for the same input data may have
        //   different predicted 6D poses.
        [DataMember (Name = "pose")] public GeometryMsgs.PoseWithCovariance Pose;
    
        /// Constructor for empty message.
        public ObjectHypothesisWithPose()
        {
            Id = string.Empty;
            Pose = new GeometryMsgs.PoseWithCovariance();
        }
        
        /// Explicit constructor.
        public ObjectHypothesisWithPose(string Id, double Score, GeometryMsgs.PoseWithCovariance Pose)
        {
            this.Id = Id;
            this.Score = Score;
            this.Pose = Pose;
        }
        
        /// Constructor with buffer.
        internal ObjectHypothesisWithPose(ref Buffer b)
        {
            Id = b.DeserializeString();
            Score = b.Deserialize<double>();
            Pose = new GeometryMsgs.PoseWithCovariance(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new ObjectHypothesisWithPose(ref b);
        
        ObjectHypothesisWithPose IDeserializable<ObjectHypothesisWithPose>.RosDeserialize(ref Buffer b) => new ObjectHypothesisWithPose(ref b);
    
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
    
        public int RosMessageLength => 356 + BuiltIns.GetStringSize(Id);
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "vision_msgs/ObjectHypothesisWithPose";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "33f27ec2eb451c31670b819827352622";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1VTW/TQBC9+1eMlAtIqQGBcgBxKFRCOYCAVnxVFRrb43iLvevurtO4v5639tpJaQUX" +
                "1ByS9Xrmzbz3ZjcLOtZkskvJPVV9a3wlTjnyFXvKjfastKPWOOWV0aR0aWzDYZ0myYLOKqFOq6tOaH1C" +
                "pkSeTGh5zc6ldGZoI564KAYIrg9BiDPTeQARMlF2fbIk1+UVsSPlHVVdw/rIChec1TJCkuZGllQr50WL" +
                "deQq09UFtWIDLvEAVxvzq2tRi5ga8QDwTOErYycpnYrQVjm08LNxG/fky7Beo7MUzwQgaowVKpCq6jRx" +
                "3iq9IVVMrFtrMs5UrXxPCIZUpSpE54DlGnJELZAPLaSIoqT0pg+xW9GB/nLPfMyKVGolofMAYFlvhM6f" +
                "Hj27SJOyNuxXL8jl6G3qZHUS/Jkr3vESFoQCQ0zEz2QoXEipNHqD2CFzQnGmESrVDm+slGIHVq1R2hMc" +
                "29eYrIokBD4jE0LllIOf2KkleKyLIF9mdkGssLePaIKnY+SAFJXC+oPxMk7iINHQH3618eQ8N60Ur6i0" +
                "mIZbI5WzBsGZXNYPqC2Dh8coOMcbcQH+uHYmgMUahSoHsnFyBQWtFGpwr4xdu7FY2yE8DFTDPVW8jXLO" +
                "APvEaA7qRXH6cd4+YvOr8tVbs2WreFTYwdPX//mTvD9995L+VXwYJUhrBa07UHA4NqPeGhLjsLiW0eM1" +
                "snDgc7HhYvA9LoEAFntf0GdzfdTwJdSakUZT4iSsdiuM/0wZjlm1i3NsrJrD4RaUxoA46gDP4zge8e6w" +
                "x3gNLWgNfFuIXY5jvM+F5y/x/tFuSf2SbpZkjT+4d+gbBcQ729/v3/4xbD+eTuH589XFAZmHsy6M7j36" +
                "3rVrifaasF3E9+Mlzro4FDsleBgO9xSQfOoY8ukBdx/3UATRyjSO8x9QvJ7G/sElDGdo+RbdZLoed/Oq" +
                "n1c3D9P+Xrr7jtQtPf84Wni62userjMcrr8zmlbXSfIbfohzf8cHAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
