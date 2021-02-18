/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.VisionMsgs
{
    [Preserve, DataContract (Name = "vision_msgs/Detection3D")]
    public sealed class Detection3D : IDeserializable<Detection3D>, IMessage
    {
        // Defines a 3D detection result.
        //
        // This extends a basic 3D classification by including position information,
        //   allowing a classification result for a specific position in an image to
        //   to be located in the larger image.
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        // Class probabilities. Does not have to include hypotheses for all possible
        //   object ids, the scores for any ids not listed are assumed to be 0.
        [DataMember (Name = "results")] public ObjectHypothesisWithPose[] Results { get; set; }
        // 3D bounding box surrounding the object.
        [DataMember (Name = "bbox")] public BoundingBox3D Bbox { get; set; }
        // The 3D data that generated these results (i.e. region proposal cropped out of
        //   the image). This information is not required for all detectors, so it may
        //   be empty.
        [DataMember (Name = "source_cloud")] public SensorMsgs.PointCloud2 SourceCloud { get; set; }
        // If this message was tracking result, this field set true.
        [DataMember (Name = "is_tracking")] public bool IsTracking { get; set; }
        // ID used for consistency across multiple detection messages. This value will
        //   likely differ from the id field set in each individual ObjectHypothesis.
        // If you set this field, be sure to also set is_tracking to True.
        [DataMember (Name = "tracking_id")] public string TrackingId { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Detection3D()
        {
            Results = System.Array.Empty<ObjectHypothesisWithPose>();
            Bbox = new BoundingBox3D();
            SourceCloud = new SensorMsgs.PointCloud2();
            TrackingId = string.Empty;
        }
        
        /// <summary> Explicit constructor. </summary>
        public Detection3D(in StdMsgs.Header Header, ObjectHypothesisWithPose[] Results, BoundingBox3D Bbox, SensorMsgs.PointCloud2 SourceCloud, bool IsTracking, string TrackingId)
        {
            this.Header = Header;
            this.Results = Results;
            this.Bbox = Bbox;
            this.SourceCloud = SourceCloud;
            this.IsTracking = IsTracking;
            this.TrackingId = TrackingId;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Detection3D(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Results = b.DeserializeArray<ObjectHypothesisWithPose>();
            for (int i = 0; i < Results.Length; i++)
            {
                Results[i] = new ObjectHypothesisWithPose(ref b);
            }
            Bbox = new BoundingBox3D(ref b);
            SourceCloud = new SensorMsgs.PointCloud2(ref b);
            IsTracking = b.Deserialize<bool>();
            TrackingId = b.DeserializeString();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Detection3D(ref b);
        }
        
        Detection3D IDeserializable<Detection3D>.RosDeserialize(ref Buffer b)
        {
            return new Detection3D(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeArray(Results, 0);
            Bbox.RosSerialize(ref b);
            SourceCloud.RosSerialize(ref b);
            b.Serialize(IsTracking);
            b.Serialize(TrackingId);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Results is null) throw new System.NullReferenceException(nameof(Results));
            for (int i = 0; i < Results.Length; i++)
            {
                if (Results[i] is null) throw new System.NullReferenceException($"{nameof(Results)}[{i}]");
                Results[i].RosValidate();
            }
            if (Bbox is null) throw new System.NullReferenceException(nameof(Bbox));
            Bbox.RosValidate();
            if (SourceCloud is null) throw new System.NullReferenceException(nameof(SourceCloud));
            SourceCloud.RosValidate();
            if (TrackingId is null) throw new System.NullReferenceException(nameof(TrackingId));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 89;
                size += Header.RosMessageLength;
                foreach (var i in Results)
                {
                    size += i.RosMessageLength;
                }
                size += SourceCloud.RosMessageLength;
                size += BuiltIns.UTF8.GetByteCount(TrackingId);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "vision_msgs/Detection3D";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "d570bbfcd5dea29f64da78e043da65ae";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71ZbW/cuBH+vsD9B+L8IfZhvT3bVzdIYRRJFmkMXJO0ca8vQWBwJWqXZ0lUSMr25tf3" +
                "mRlS0jqbXj80NnyxRJHzPs/M8A7U0lS2NUFpdbZUpYmmiNa1ypvQ13Hx3ewAv+pqY4My99G0Je1c6WAL" +
                "2l/UOgRb2ULzodVW2bao+9K2a9W5YHnVtpXzDe+YEzGldF27O9qjH1IQtgoH8C10pqBPU1JK499Gr42K" +
                "TohFp1ZG1Q4UTEk74gav2q+Nl51Q4rvZa6NLLGz4Dy0cqJfEWnXerfTK1mBgwkItHWzRuqg2+pZ4JIWM" +
                "2mw7B8oBn1m6uiaxgl3VRuRwq19hO2XLMGcRQuF83txuaZ3p1jaQnNobBfZ9g2fR4EfI+ZZpvE6sbPiH" +
                "jZt3LpgPH5NlgogO069c37KdV+5ehd77/E68RRYQfJFWX7h7OoO9QuAKm8jfOmoc0FGtTWs8W5CVzOzU" +
                "oV2YBd7WZH7YCjrrWhV46LDX9VG5KvkBJNneRwuJl4nflRXlvfnUW4+D2YQScM7DZgG2jqrRWyEHi5im" +
                "i1soEUwbnL9uwjr87p2zbXxZu748xYneF+a6oDdR67KCGODVmBAoRu50UNHr4oYMIyrNZUdlTV2qYCK+" +
                "9xQiK+dqiHmdtyeCS9WHJG/h2kDOa4ut0rAAgqcBQdvVZpI4iXVIRrjVdQ85bF2LWrW9MfVWlbaqEI6V" +
                "d40YrpxIhBg2utjgb2lvbdnD4A8DY5G03bpelBh0mpPlEA8cvLqGVZnkqBitX4nOIXpeSF+uLcw4u/g/" +
                "/8z+8v7Pz1SIpThQEhHSv4+6LbUvYbGoORDJyBu73hh/XJtbU+OQbijMJEy3HayawQi/ErI1rMkuglqF" +
                "a5q+JSyB9haemJ7HSYIP1WkfbdEDIbDfeWQHba+8bgxRx29AlMLLBt5/xl43RR/tLfkNaOAN0A9WQ2jM" +
                "egTj2SkdmB1c3bljvBrCnYG5JBeDZ4f4Izl1eAYeP4hyC9CGcQy4AB8Oee0ar+FIgQlEMJ1DJBxC8nfb" +
                "uHGCbrfaWw3gIcIFLACqT+jQk6MJ5ZZJt7p1mbxQHHn8L2TbgS7pdLyBz2rSPvRrGNAyfiJGsRXgT0SK" +
                "2pqWYG7ltd/O6JSwnB28IhtLqLJH8BcQ6ArLuHMHrMsRyd5AOH6raLy1AbkqAfk1zIXKz9uM6pvhs3gU" +
                "Bo3atmFvlVuMGItoRCxRrLhqgsxS9gARDlEclS5LJoJEn4KmBsbHDK7gfLkESvYIB6CaBTZv+ka3x4jH" +
                "kr3GNOHwxsylyiA94PSN64ErnfFEWOkEQ87d9J0kxJB/9A9KO/D+vTE7NvqFny8h2wLvnKgNqhvBnrb1" +
                "iCS2HFUfC+tWCXpWiBOKQMHEZBBBTqolUrPUiy3tvUUQjQ1DHKE06YMoy7Xe6xZQ/+HH45OPkKSqnY7n" +
                "P0n5HaU5X5KrzAM3jG5NcM17EotVKuwld0iUuHw0kwkOwVzZe3zxBmDOqnVUnlTKJ2GSfTZWybXBURis" +
                "UAW0BFwkoXZKOkzG6TTsaMi7snPacHAleOMI8Sgw46BFKrkJA/8oObUTXwV6qZUZ9FulyguApAweChmt" +
                "PqdC0g5cpHzRLg46QzBgSst+rJLkQdh1aBE4ulDZualKRh0ojCeTj4hjMtE2F/1gKC1fOoYoMXQw36xY" +
                "/Rb3XIS8IVCHEtQUi9Fb2BnJEzpdGIY0QEBhPIEFepnZjIjx1hmI/M3dHTf6VxhsoCSeSQFxfn+OXBh0" +
                "htu8vU8B7bwdtsNjMHakdEcphCwclsf6fiqjQBO1DaDvUYOlTZ2chd+pNB3ez9V2rj7PlXdxgkTqn4oo" +
                "frH8r/3L/+blo5yQH87OP06UeTzXUfTuse+X7ppT/0DLZfouwI6KNzX2Qs24Bx02zP7ao375lumO+x5L" +
                "QYiSw3EoSgmmbNZVJ1zaUXdAyvvhaTs8fX4c8UfT7UupHXs+SC28fRrtTpiG5PrvGuWnu8foKnYGLw7B" +
                "hzOb9BECwdlZVGUQbpxJ9MyJRKU+lYHDc7V8++qIIHSJxqolfqkowHZEleZKS1MIZfScqaFuUQWap/GK" +
                "+DV2nYa9NESjS0TQeKBBcQPQH/uMHX/Nc8mt+ogJY7EzS34tYfZWN9FnH84PnzLpYD+bfTTmUz33D8BP" +
                "wkCNCxvIf1FbfuH584zZPFJBSSz3VpFb/rYb7AsaSi55jHAthpDGaCQz5p3hJA6WmKsLAShYDR0J2h92" +
                "eZluNECj0TcgibCR4bDrQEzT+NeGWnwVeQg/NIv1Yq7uNqaVXXJXAwo8c6Fx8XaNkZVPjv0E0VRJORSX" +
                "6pTnXpFZmME1IJILxdEij7B3pBAefBr1+EIky8XNbXRuzsVNSOxBwWHoBwJG9MS/iQffxtVfuazIzs5C" +
                "blzNV2mFw5xV5Dx5c1zmtNa1gHYgP1gkI1IXRBLGf21kyGnb0krNCRuJXkRBN7Hg2AAVKQd5CAtwmPS3" +
                "Wq0wDntgRO1Wc0aeWm8JhEoTCm9Xw5yXROGoTcn5Pd8/hO+BQV5Tr5PmAObFlzRjI7iiBmatWyRdqU5L" +
                "TKJ0c3RMtyNH1PqelNSI9C03KqZEpLwbyYTJWQiN43w6ZMqYPMq+YFFJTLQ3HvlhOjRj4p0w2AmR4o3j" +
                "ZpvGW1cdV7Vdb6JIT7Mrdfp8SITXxafeCtAJunKT/uAegea9Q+qDz8ps7HC02L2DJAanSwjg+4LANFtx" +
                "Yq6F3GeZZDx4ajDIHFRITiyCzglLcmdLaGil/NemXeNtD9F8aSEE8hsfJpmWydFCpsDA35o6ZFWtzwGR" +
                "KkGKF7YNBc1CuqNXFAofPsqVVJjJ7Rp+bLheWYBIiSZQAdTC5Dpg+PCnLJTIfQ0ndTh7oH4elJp0NKtt" +
                "NGE44d1d3v/wBD7t7n8KAZlz/kGZhi9y5vG3udQfyHiYSf8gljuaDXeGJUPqQIRu15Rly9MtL/5r6R4Z" +
                "8yswU6Lhm12zPUQfdsR+8JHpm7zdZfxBB5J0R2L7bXIyjk+vXTMVQR2Jp6fq8s3VU1L/Qp2klb+npQt1" +
                "Ou45OeeVs8keWrpQP417yI9Y+f1kDy1dqPO08urnt89p6UL9YboCVL9QT2f5MoJuQrJL3mhJZY7HHCyu" +
                "quhulDe8lWe+kMXI7KPMAGQKSdHEiIOCriLp0DI/m7YnkBFUCKjb6NxuTeZToDOJSZDXCMKG/n+AqU3D" +
                "6JlbKpZs9h88w2xRkRkAAA==";
                
    }
}
