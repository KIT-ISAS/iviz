/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.VisionMsgs
{
    [DataContract]
    public sealed class ObjectHypothesisWithPose : IHasSerializer<ObjectHypothesisWithPose>, IMessage
    {
        // An object hypothesis that contains position information.
        // The unique numeric ID of object detected. To get additional information about
        //   this ID, such as its human-readable name, listeners should perform a lookup
        //   in a metadata database. See vision_msgs/VisionInfo.msg for more detail.
        [DataMember (Name = "id")] public long Id;
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
    
        public ObjectHypothesisWithPose()
        {
            Pose = new GeometryMsgs.PoseWithCovariance();
        }
        
        public ObjectHypothesisWithPose(long Id, double Score, GeometryMsgs.PoseWithCovariance Pose)
        {
            this.Id = Id;
            this.Score = Score;
            this.Pose = Pose;
        }
        
        public ObjectHypothesisWithPose(ref ReadBuffer b)
        {
            b.Deserialize(out Id);
            b.Deserialize(out Score);
            Pose = new GeometryMsgs.PoseWithCovariance(ref b);
        }
        
        public ObjectHypothesisWithPose(ref ReadBuffer2 b)
        {
            b.Align8();
            b.Deserialize(out Id);
            b.Deserialize(out Score);
            Pose = new GeometryMsgs.PoseWithCovariance(ref b);
        }
        
        public ObjectHypothesisWithPose RosDeserialize(ref ReadBuffer b) => new ObjectHypothesisWithPose(ref b);
        
        public ObjectHypothesisWithPose RosDeserialize(ref ReadBuffer2 b) => new ObjectHypothesisWithPose(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(Id);
            b.Serialize(Score);
            Pose.RosSerialize(ref b);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            b.Align8();
            b.Serialize(Id);
            b.Serialize(Score);
            Pose.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            BuiltIns.ThrowIfNull(Pose, nameof(Pose));
            Pose.RosValidate();
        }
    
        public const int RosFixedMessageLength = 360;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        public const int Ros2FixedMessageLength = 360;
        
        public int Ros2MessageLength => Ros2FixedMessageLength;
        
        public int AddRos2MessageLength(int c) => WriteBuffer2.Align8(c) + Ros2FixedMessageLength;
        
    
        public const string MessageType = "vision_msgs/ObjectHypothesisWithPose";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "fa1ab3bc7146f53921fa142d631d02db";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71VTW/TQBS8+1c8qReQEgMC5QDiUKiEcgABrfiqKvRsP8db7F13d53G/fXM2msnpRVc" +
                "UHNI1uvdeTPzPnJEx5pMdim5p6pvja/EKUe+Yk+50Z6VdtQap7wympQujW04rNMkOaKzSqjT6qoT0l0j" +
                "VuW0PiFTToiFePxIkdKZoY144qIYkLg+xCLOTOeBRwiM6OuTBbkur4gdKe+o6hrWSytccFYjFDeyoFo5" +
                "L1qsI1eZri6oFRsQiak25lfXDngK4NSIx1XPFL4ydpLSqQhtlUPwn43buCdfhvUanFI8E4CoMVaCAlZ1" +
                "mijtVy9IFZPq1pqMM1Ur3xPOwqpSFaJzoHINO+ABrJwNiIak9KYPZ7eig+7FXvJ4KyqplQTiAcCy3gid" +
                "P10+u0iTsjYcaLgc1CYmq5OQnzninVzC+xBgOBPxMxkCF1IqDW7shpsTijONUKl2eGOlFDuoag0cIKMP" +
                "Ykw5iiIECcZNH4oghz6xEyUkVxdKb7DYBbPC3v5Ew87FkwNSdArrD8bLWIl+VoBfbTw5z00rxSsqLYrh" +
                "Vi3lrCFwFpf1A2rL0OFRCc7xRlyAP66dCWAxRqHKQSzqvgYlQUArhRqyV0bWbgzWdn4oJXDvqeJttHMG" +
                "2F+MyUG8aE4/lttHbH5VvnprtmwVjw47SV7/50/y/vTdS/pX7KGS4KwVMHdQ4NA0o90aDqNVXMugeI1b" +
                "6PdcbJgLvscMCGAjdYB8NtfLhi9h1ow05iQWwmq3QvXPipEwq3axjI1V83EkC0b70Nsd4HmsxiXvDjnG" +
                "KXREa+DbQuxirOL9XaT8Jd4/2i2oX9DNgqzxB/OGvlFAvLP9/f7tH8P246kJz5+vLg7EJA+WulC59/h7" +
                "N10L0GvCdhHfjzOcdXFodkrIYejt6UDyqWPYpwfc/bmHEggqUznO/z9xOqlJK8dxdEvuPB1386qfVzcP" +
                "Q39v3X0tdcvPP1oLT1d738M0Q3P9XdG0uk6S30Iq3zLGBwAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<ObjectHypothesisWithPose> CreateSerializer() => new Serializer();
        public Deserializer<ObjectHypothesisWithPose> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<ObjectHypothesisWithPose>
        {
            public override void RosSerialize(ObjectHypothesisWithPose msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(ObjectHypothesisWithPose msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(ObjectHypothesisWithPose _) => RosFixedMessageLength;
            public override int Ros2MessageLength(ObjectHypothesisWithPose _) => Ros2FixedMessageLength;
        }
    
        sealed class Deserializer : Deserializer<ObjectHypothesisWithPose>
        {
            public override void RosDeserialize(ref ReadBuffer b, out ObjectHypothesisWithPose msg) => msg = new ObjectHypothesisWithPose(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out ObjectHypothesisWithPose msg) => msg = new ObjectHypothesisWithPose(ref b);
        }
    }
}
