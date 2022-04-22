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
            Id = "";
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
        public ObjectHypothesisWithPose(ref ReadBuffer b)
        {
            Id = b.DeserializeString();
            b.Deserialize(out Score);
            Pose = new GeometryMsgs.PoseWithCovariance(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new ObjectHypothesisWithPose(ref b);
        
        public ObjectHypothesisWithPose RosDeserialize(ref ReadBuffer b) => new ObjectHypothesisWithPose(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Id);
            b.Serialize(Score);
            Pose.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (Id is null) BuiltIns.ThrowNullReference();
            if (Pose is null) BuiltIns.ThrowNullReference();
            Pose.RosValidate();
        }
    
        public int RosMessageLength => 356 + BuiltIns.GetStringSize(Id);
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "vision_msgs/ObjectHypothesisWithPose";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "33f27ec2eb451c31670b819827352622";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71VS2/UMBC+51eMtBeQsgEE2kMRh0IltAcQ0IpXVaFJPNm4JHZqO9ukv56x4yTbh+CC" +
                "uoddx5n5Zr5vHruCYwU6v6TCQTW02lVkpQVXoYNCK4dSWWi1lU5qBVKV2jToz1mSrOCsIuiUvOoItieg" +
                "S/ajCa2o0doMzjTsyAEKESCwPgQBzHXnGAjYk8NuT1KwXVEBWpDOQtU1qNaGUGBe0wgJChtKoZbWkSJj" +
                "wVa6qwW0ZDwuYICrtf7dtRwLEBpyDOAQ/FeOljI4JYK9tJzCr8bu7LOv4bzlzDJ+BgaCRhsCwa6yzhLr" +
                "jFQ7kGJi3RqdYy5r6QZgY5aqlIJUwbBYsxxRC/ZnLUhEUTJ4O3jbPSlPP12Yj16RSi3JZ+4BDKodwfnz" +
                "9YuLLClrjW7zCmzBuU2ZbE58feiO+kstuQQ+QLCJ+DmFwIJKqTg3tMFzQrG6IShlz28MlWQCq1ZL5UCr" +
                "gxhTqSIJ4jqzJwtVQMH8yEwpcY2V8PLluvdi+bvFovE1HS0DUlSKzx+1o7ET3cyAf5V2YB02LYnXUBru" +
                "hlstVaBigjO5fAioLTIPx61gLe7Ievjj2moPFmMIWQaysXOJAxoSMlSvjFnbMVjbudBLnPsAFe6jnDPA" +
                "4hiLw/GiOMPYb5/48pt01Tu9RyNxVNhyTd/850/y4fT9EfwreGglltYQp26ZguWxGfVWLDEPi22Rc7xm" +
                "Lx74goxfDG7gJeDBYu4r+KKv1w1esloz0liU2AmbfsPtP1PmihnZxz7WRs7mXC1W2vnp7hgex3ZcY3+Y" +
                "Y1xDK9gyvhFk0rGNF1+u+RG/f9KnMKRwk4LR7mDvwHfwiPeufzx8/TNcP52m8Pzl5uKAzOOVzrfuA/re" +
                "L1fK6TX+WsT34xJHJQ7FzoBr6Id7Mkg+d8jyqYC72D0WQU5lasf5DyiuJzlxxbiPbtGd12M/n4b5dPM4" +
                "6S/SPTRSt/S8M1r8dLXo7tcZD9ffGU2n6yT5A36Ic3/HBwAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
