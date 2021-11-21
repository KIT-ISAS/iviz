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
    
        /// <summary> Constructor for empty message. </summary>
        public ObjectHypothesisWithPose()
        {
            Id = string.Empty;
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
        internal ObjectHypothesisWithPose(ref Buffer b)
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
    
        public int RosMessageLength => 356 + BuiltIns.GetStringSize(Id);
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "vision_msgs/ObjectHypothesisWithPose";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "33f27ec2eb451c31670b819827352622";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1VTW/UMBC9R+p/GKkXkHYXEGgPRRwKldAeQEARX1WFJslk45LYqe20m/56nhMn2dIK" +
                "Lqh72HWcmTfz3ht7D+lYk0kvJPNUdo3xpTjlyJfsKTPas9KOGuOUV0aT0oWxNYf1KkkO6XMp1Gp12Qpt" +
                "TsgUyJMRLavYuRV9NrQVT5znPQRX+yDEqWk9gAiZKLs5WZBrs5LYkfKOyrZmvbTCOaeVDJCkuZYFVcp5" +
                "0WIdudK0VU6N2IBL3MNVxvxqG9Qiplo8ADxT+ErZyYpORehKObTws3Zb9+RLv96gsxWeCUBUGyuUI1VV" +
                "q8R5q/SWVD6ybqxJOVWV8h0hGFIVKhedAZYryBG1QD60kDyKsqLXXYi9Eh3oL2bmQ1akUikJnQcAy3or" +
                "dPZ0+ex8lRSVYb9+QS5Db2Mn65Pgz1TxjpewIBToYyJ+Kn3hXAql0RvEDpkjijO1UKF2eGOlENuzaozS" +
                "nuDYXGO0KpIQ+IxMCJVRBn5ix5bgsc6DfKnZBbHC3hxRB0+HyB4pKoX1e+NlmMReor4//GrjyXmuG8lf" +
                "UmExDbdGKmMNghO5tOtRGwYPj1FwjrfiAvxx5UwAizVyVfRk4+QKClrJVe9eEbt2Q7GmRXgYqJo7Kvkq" +
                "yjkBzInRHNSL4nTDvH3A5lflyzfmiq3iQWEnyUHy6j9/DpJ3p2+P6F/lD/ppgrpW0L0DC4eTM0iuoTLO" +
                "i2sYbV4jDWc+ExvuBt/hHghoQ/sA+WSulzVfQLAJafAlDsN6t8YJmFjDNKt2cZSNVVM4DIPYmBFHLeB5" +
                "mMgl7/Z7jDfRIW2Ab3Oxi2GS51zYfoT3j3YL6hZ0syBr/N7VQ98oIN7Z/n7/9o9++/F4EM+er8/3yDyk" +
                "e8Gv43skvuvYAh3WYTuP74ernHW+r/eKYGM44mNA8rFlKKh73Dnu4TiimWkop3+ieE8NFEAnjGjo+hbj" +
                "ZLwnd9Oqm1Y3D8Vg1u/es3VL1T/OGJ4uZ/XD1YZT9ndS4+oa9H4DTO9qjNQHAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
