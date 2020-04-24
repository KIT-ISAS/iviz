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
    
        public int GetLength()
        {
            int size = 684;
            size += header.GetLength();
            size += child_frame_id.Length;
            return size;
        }
    
        public IMessage Create() => new Odometry();
    
        /// <summary> Full ROS name of this message. </summary>
        public const string _MessageType = "nav_msgs/Odometry";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string _Md5Sum = "cd5e73d190d741a2f92e81eda573aca7";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string _DependenciesBase64 =
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
                
    }
}
