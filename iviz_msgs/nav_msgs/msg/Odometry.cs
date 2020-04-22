
namespace Iviz.Msgs.nav_msgs
{
    public sealed class Odometry : IMessage
    {
        // This represents an estimate of a position and velocity in free space.  
        // The pose in this message should be specified in the coordinate frame given by header.frame_id.
        // The twist in this message should be specified in the coordinate frame given by the child_frame_id
        public std_msgs.Header header;
        public string child_frame_id;
        public geometry_msgs.PoseWithCovariance pose;
        public geometry_msgs.TwistWithCovariance twist;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "nav_msgs/Odometry";
    
        public IMessage Create() => new Odometry();
    
        public int GetLength()
        {
            int size = 684;
            size += header.GetLength();
            size += child_frame_id.Length;
            return size;
        }
    
        /// <summary> Constructor for empty message. </summary>
        public Odometry()
        {
            header = new std_msgs.Header();
            child_frame_id = "";
            pose = new geometry_msgs.PoseWithCovariance();
            twist = new geometry_msgs.TwistWithCovariance();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            header.Deserialize(ref ptr, end);
            BuiltIns.Deserialize(out child_frame_id, ref ptr, end);
            pose.Deserialize(ref ptr, end);
            twist.Deserialize(ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            header.Serialize(ref ptr, end);
            BuiltIns.Serialize(child_frame_id, ref ptr, end);
            pose.Serialize(ref ptr, end);
            twist.Serialize(ref ptr, end);
        }
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "cd5e73d190d741a2f92e81eda573aca7";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
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
                
    }
}
