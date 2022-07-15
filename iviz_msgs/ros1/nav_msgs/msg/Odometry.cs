/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.NavMsgs
{
    [DataContract]
    public sealed class Odometry : IDeserializable<Odometry>, IMessageRos1
    {
        // This represents an estimate of a position and velocity in free space.  
        // The pose in this message should be specified in the coordinate frame given by header.frame_id.
        // The twist in this message should be specified in the coordinate frame given by the child_frame_id
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "child_frame_id")] public string ChildFrameId;
        [DataMember (Name = "pose")] public GeometryMsgs.PoseWithCovariance Pose;
        [DataMember (Name = "twist")] public GeometryMsgs.TwistWithCovariance Twist;
    
        /// Constructor for empty message.
        public Odometry()
        {
            ChildFrameId = "";
            Pose = new GeometryMsgs.PoseWithCovariance();
            Twist = new GeometryMsgs.TwistWithCovariance();
        }
        
        /// Explicit constructor.
        public Odometry(in StdMsgs.Header Header, string ChildFrameId, GeometryMsgs.PoseWithCovariance Pose, GeometryMsgs.TwistWithCovariance Twist)
        {
            this.Header = Header;
            this.ChildFrameId = ChildFrameId;
            this.Pose = Pose;
            this.Twist = Twist;
        }
        
        /// Constructor with buffer.
        public Odometry(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.DeserializeString(out ChildFrameId);
            Pose = new GeometryMsgs.PoseWithCovariance(ref b);
            Twist = new GeometryMsgs.TwistWithCovariance(ref b);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new Odometry(ref b);
        
        public Odometry RosDeserialize(ref ReadBuffer b) => new Odometry(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(ChildFrameId);
            Pose.RosSerialize(ref b);
            Twist.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (ChildFrameId is null) BuiltIns.ThrowNullReference();
            if (Pose is null) BuiltIns.ThrowNullReference();
            Pose.RosValidate();
            if (Twist is null) BuiltIns.ThrowNullReference();
            Twist.RosValidate();
        }
    
        public int RosMessageLength => 684 + Header.RosMessageLength + BuiltIns.GetStringSize(ChildFrameId);
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "nav_msgs/Odometry";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "cd5e73d190d741a2f92e81eda573aca7";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE+1WTW/bRhC981cM4EPsQmKBuPDBQA9FgrY+FEgbo0lbFMaKOyS3IXeV3aUk5tfnzfJD" +
                "sqw0OaQ61RBgcjmfb97MzgXd1yaQ57XnwDYGUpY4RNOqyORKUrR2wUTjLL5o2nDjChN7MpZKz0xhrQrO" +
                "ibILWGIRZvkWxWrLIagKMrXrGk0rkebClIb1IMNUOOe1seKs9KplqsyGLa16qllp9nk6fTA6Hx3ErQnx" +
                "63hIX2vT6IfJSfZzcjr6zkL0xlbHMhW7lqPvH9pQhW9fIeE3JtYv3EZ5o2wxYHAkdS9RH4mlTLLs+6/8" +
                "l/3y+qdbClEPnoeMAN7riPopr4FZVFpFRaVDpqaq2S8bRmGhpNo1kEtfY7/mMKAOoPGr2LJXTdNTFyAU" +
                "HaBt286aQrAFY/iRPjRRAdBH+WiKrlH+SSnEOn6B33csiNy9vIWMDVx0EUVqhGWFZxWkCncvKeuMjdfP" +
                "RSG7uN+6JV65Qr1m56ipihIs74TQEqcKt/DxzZBcDtsAh+FFB7pMZw94DVcEJwiB166o6RKRv+pj7QYK" +
                "pZKtGhbDBRCA1Wei9OzqwLJNpq2ybjI/WNz7+BKzdrYrOS1r1KyR7ENXAUAIrr3bGA3RicCNQdtSY1Ze" +
                "+T4TrcFldvFjonvqk1QR/FchoH1RAE1bsHFi+Mzt/4iNn2uZiWWHY2geJfsxk2KmDgo+KtS+z7NMjA0d" +
                "ByO/ue2yVf+A17MllUYXBpmAdbO7AcHmDsSM82Y3DhbnzSwOzgKQyD4I1xFLaXasl2p3GGMSFQrfwb5H" +
                "ky2SjwNd5Vm4d7lbUL+gDwvybnSgVq6L9JbE4pPjP04f/5mOr7KycSrefPfX9c3fB8mcr3TI6IcT+D4t" +
                "10IGhBzr8fv+GjkAOyfUEMWcBbJfOxDU22R3L3euBBHKREe0sfAsDHWd4h8vRQn5UbpTYWg3P/Xz04fz" +
                "hL+H7lRLPcLzqLXw9n6PO66GFs317xlNT9vz5HbiDp2SnKZ9+MR+cmpwJHPTJfz/6DhP+b64YCvv3rFw" +
                "FGuGAXdxCTIuQJkdylZpm5DFAgvK71xE569pFNm/j3LnyW70evIi26RvxxuzVD+tKs5i0WlZYZ4g2VkT" +
                "itp4qKYZCZp5RldiqJpI2gE56wTOVr2DScbeINpqvYYxLG9e2dAMFEgI0iXnVb6gbQ1Uk5Tc+2lLS3ud" +
                "KcibyuhBU9p/VlY0JgeSls/RSk0zxDw4A3FhZCLcVU53JfWuo60khAc/rpNOtvMprrT2ROcWqUkGEycG" +
                "8bzfYwhHLLKfHUkfAfEV6ZrPDAAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
