using System.Runtime.Serialization;

namespace Iviz.Msgs.trajectory_msgs
{
    [DataContract]
    public sealed class MultiDOFJointTrajectory : IMessage
    {
        // The header is used to specify the coordinate frame and the reference time for the trajectory durations
        [DataMember] public std_msgs.Header header { get; set; }
        
        // A representation of a multi-dof joint trajectory (each point is a transformation)
        // Each point along the trajectory will include an array of positions/velocities/accelerations
        // that has the same length as the array of joint names, and has the same order of joints as 
        // the joint names array.
        
        [DataMember] public string[] joint_names { get; set; }
        [DataMember] public MultiDOFJointTrajectoryPoint[] points { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public MultiDOFJointTrajectory()
        {
            header = new std_msgs.Header();
            joint_names = System.Array.Empty<string>();
            points = System.Array.Empty<MultiDOFJointTrajectoryPoint>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public MultiDOFJointTrajectory(std_msgs.Header header, string[] joint_names, MultiDOFJointTrajectoryPoint[] points)
        {
            this.header = header ?? throw new System.ArgumentNullException(nameof(header));
            this.joint_names = joint_names ?? throw new System.ArgumentNullException(nameof(joint_names));
            this.points = points ?? throw new System.ArgumentNullException(nameof(points));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal MultiDOFJointTrajectory(Buffer b)
        {
            this.header = new std_msgs.Header(b);
            this.joint_names = b.DeserializeStringArray();
            this.points = b.DeserializeArray<MultiDOFJointTrajectoryPoint>();
            for (int i = 0; i < this.points.Length; i++)
            {
                this.points[i] = new MultiDOFJointTrajectoryPoint(b);
            }
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new MultiDOFJointTrajectory(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.header);
            b.SerializeArray(this.joint_names, 0);
            b.SerializeArray(this.points, 0);
        }
        
        public void Validate()
        {
            if (header is null) throw new System.NullReferenceException();
            header.Validate();
            if (joint_names is null) throw new System.NullReferenceException();
            for (int i = 0; i < joint_names.Length; i++)
            {
                if (joint_names[i] is null) throw new System.NullReferenceException();
            }
            if (points is null) throw new System.NullReferenceException();
            for (int i = 0; i < points.Length; i++)
            {
                if (points[i] is null) throw new System.NullReferenceException();
                points[i].Validate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 8;
                size += header.RosMessageLength;
                size += 4 * joint_names.Length;
                for (int i = 0; i < joint_names.Length; i++)
                {
                    size += BuiltIns.UTF8.GetByteCount(joint_names[i]);
                }
                for (int i = 0; i < points.Length; i++)
                {
                    size += points[i].RosMessageLength;
                }
                return size;
            }
        }
    
        string IMessage.RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "trajectory_msgs/MultiDOFJointTrajectory";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "ef145a45a5f47b77b7f5cdde4b16c942";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71WS4/bNhC+81cM4EPswusFkiKHBXookKbdAkFTxMilKAxaGklMJFIhqXXUX99vqJft" +
                "TR9AmzV84Gve33yjFe0rpop1zp5MoC5wTtFRaDkzRU8Rt5lzPjdWR6bC64ZJ2zxdeC7Ys82YosFx4Xw6" +
                "jl5/4Cw631PeeR2Ns0H9NJgYLCm1ou8h3noObGN6Qq4gTU1XR3OTY/3BGRvPda1ZZxW16RiearmzAUab" +
                "JL+Bzh+WF7p2trx252TqmozN6i6XKEh7r3sx3Lpgkp+3D1y7DGsOtzrLuOYpgBWU6UiVDklrkETUbMtY" +
                "0Xg0axtct3gRtilZF0JIJhIxPQsinZTzudygbKdUiN7Y8rffh8tDulRvJEuvfnn9s5zt5/jeyhZPUwaC" +
                "Ut/9zz/15t2PdxRifmhCGW6HksL3dxFBap9Tw1HnOuoEhcqUFfubmpFSCOmmBbTSbexbDjsI7isUEv+S" +
                "LfJc1/2Mv8w1TWdNJqATcF3IQ9KgeNRqH03W1do/wqhoxz/wpy4B9P7VHd7YwFkXDRzqBQaedUBycUmq" +
                "Q8pePBcBtdqf3A22XKJOs/Gh/HCWPwtsxU8d7mDjmyG4HXQjOQwreaB1OjtgGzYEI3CBWwd0ruH52z5W" +
                "ALyU/EF7o481i+IMGYDWZyL0bHOm2SbVVls3qR80Ljb+jVo765WYbirUrJboQ1fq1Putdw8mx9Pj2Pe1" +
                "QXNSbY5e+16lHk8m1ep14oEo5UsVkX4MAY2DAuRos1iNwB2qcTD510Lj0t0DKP+uNSaGuCaZDFQwEd4Z" +
                "rdC6awWLLwn6NqpkB3xPdvbTK/TbLBFUAjVIMek8ghNoJJR+tGCQn4konTelScS3dP+1mZMJ0tILLT02" +
                "gVY4Y6r/ZueS89TE3wkyh8K75gAE+Pi1qvkXOZ6oYp4YYWL2sVJHjidmuHlyj6ggCFkUngHeVmdgBvU+" +
                "YeLFIF+nANWvHQS8lVi9GybS0wQ5OvOFEAU7cnflv/DafWIiZ8FjDWuZk26RhGBuPEQRw27ACpLEWzKR" +
                "cod8WCet0OiPUMmgBZHWbVvP6K/HojsRWfOu3G3pVCG/6ZW0dSLhRNsmI4FXfjWOk04ag9tSLJ4P0zf5" +
                "PBhDCaFkyvZmR/cF9a6jkwSEhR+nhROUT34lVovObWVUjCouE5paHWkJQZcgQBsi5hSqXtROx5ff0ud5" +
                "1c+rP56k1AvGvlRtK306fw1d1Fx2nxaASpL/MaBpdXqiXhUCmcKaRmRY2O8ynqN3H1mCTBALmDGWMYTk" +
                "W0nbMk10Ge74SJh6dXyy7Md3Sv0JINHGMsMKAAA=";
                
    }
}
