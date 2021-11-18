/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.NavMsgs
{
    [Preserve, DataContract (Name = "nav_msgs/Odometry")]
    public sealed class Odometry : IDeserializable<Odometry>, IMessage
    {
        // This represents an estimate of a position and velocity in free space.  
        // The pose in this message should be specified in the coordinate frame given by header.frame_id.
        // The twist in this message should be specified in the coordinate frame given by the child_frame_id
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "child_frame_id")] public string ChildFrameId;
        [DataMember (Name = "pose")] public GeometryMsgs.PoseWithCovariance Pose;
        [DataMember (Name = "twist")] public GeometryMsgs.TwistWithCovariance Twist;
    
        /// <summary> Constructor for empty message. </summary>
        public Odometry()
        {
            ChildFrameId = string.Empty;
            Pose = new GeometryMsgs.PoseWithCovariance();
            Twist = new GeometryMsgs.TwistWithCovariance();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Odometry(in StdMsgs.Header Header, string ChildFrameId, GeometryMsgs.PoseWithCovariance Pose, GeometryMsgs.TwistWithCovariance Twist)
        {
            this.Header = Header;
            this.ChildFrameId = ChildFrameId;
            this.Pose = Pose;
            this.Twist = Twist;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Odometry(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            ChildFrameId = b.DeserializeString();
            Pose = new GeometryMsgs.PoseWithCovariance(ref b);
            Twist = new GeometryMsgs.TwistWithCovariance(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Odometry(ref b);
        }
        
        Odometry IDeserializable<Odometry>.RosDeserialize(ref Buffer b)
        {
            return new Odometry(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(ChildFrameId);
            Pose.RosSerialize(ref b);
            Twist.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (ChildFrameId is null) throw new System.NullReferenceException(nameof(ChildFrameId));
            if (Pose is null) throw new System.NullReferenceException(nameof(Pose));
            Pose.RosValidate();
            if (Twist is null) throw new System.NullReferenceException(nameof(Twist));
            Twist.RosValidate();
        }
    
        public int RosMessageLength => 684 + Header.RosMessageLength + BuiltIns.GetStringSize(ChildFrameId);
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "nav_msgs/Odometry";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "cd5e73d190d741a2f92e81eda573aca7";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACu1WS2/bRhC+E/B/GMCH2IWkAnHhg4EeigZtfSiQNkb6QmGMyBG5Dbmr7C4lMb++3ywf" +
                "kmW1ySHVqYYAk8t57Mz3zeOSHioTyMvaSxAbA7ElCdE0HIXcipjWLphonMWXgjZSu9zEjoyllRehsOZc" +
                "FkTZJSyJCot+i2q1kRC4hEzl2rqgpUpLblZGil5GKHfOF8aqs5XnRqg0G7G07KgSLsQv0umjKRaDg7g1" +
                "IX4eD+lrZericXSS/ZCcDr6zEL2x5bFMKa6R6LvHJpThy9cI+BcTq2/dhr1hm/c5OJJ60FsfiaVIsovs" +
                "68/8d5H9+Ob7Owqx6H33MV0gf28iIGRfIG2RC45MK4dgTVmJn9cCbKHFzRrJS19jt5bQJx65xq8UK57r" +
                "uqM2QCg6ZLdpWmtyTS9II0/0oQkQwCD20eRtzf4ZGmodvyDvW9Gk3L+6g4wNkrcROMGTsbkXDgrE/SvK" +
                "WmPjzUtVyC4ftm6OVykB2eQcsDIIEkh2ymm9J4c7+PiiD24B28iOwEsR6CqdPeI1XBOc4AqydnlFV7j5" +
                "6y5W4L3yJKG2rMHtQDkyAKsvVOnF9YFlvfYdWbZuNN9b3Pv4FLN2sqsxzStgVmv0oS2RQAiuvduYAqIj" +
                "h2uDyqXaLD37LlOt3mV2+V1ifCqVhAj+cwioYABQ0BaEHEk+0fs/I+TH6kYZ+qwZTQ1l32zStamFho8M" +
                "+LtFlqm1vu5g5Ge3nTf8F6g9WeLUwNDONF+3u1twbKpDdDpvdkN7cd5M4qAtchLFB6U77rIyOynmvDu8" +
                "YxJVFt/DvkehzZKPA132ovS72s2om9GHGXk3OOClayP9Smrx2fFvp49/T8fX2ap2HG+/+uPm9s+DYM6J" +
                "nuL1zYkUP0dspm1Cj4vh+36eHOR7QYAReE4C2U8taOptsruXO1+MuMxEStSzsi306I4hDANSb/0k4hEe" +
                "2k1P3fT04VwR7PN3sraeZPWoxvD2fp99jIkGVfbvQY1P23OFd2KoTnGOzT/8w8Zyqokke8NY/r+NnA3B" +
                "T8ds6d07rG6Ay5EBgzEWBSNR+wjbMu0XumpgZXkreXT+hgaR/fsgdy6KDn5Pj7ZN+ni8SesoSfuLs9h+" +
                "GmH0FsQ7aUKxMB6qqWViZnlBeaLHmkiFQ/Ksi7DR8DuYFCwTBG1er2EMG51nG+q+4FMS6UoW5WJG2wqJ" +
                "TVK6DKTVLS17JidvSoNdTzW1D0zKTEN0mHirlyiouu7v3DtDn4SRcXpdL+h+RZ1raasB4cEPO6bTrX28" +
                "V9qFonOzNHF7Eye68rT3oyFHrLcf600X2d9eeCHl6AwAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
