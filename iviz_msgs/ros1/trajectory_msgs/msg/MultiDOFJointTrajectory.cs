/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.TrajectoryMsgs
{
    [DataContract]
    public sealed class MultiDOFJointTrajectory : IDeserializableRos1<MultiDOFJointTrajectory>, IDeserializableRos2<MultiDOFJointTrajectory>, IMessageRos1, IMessageRos2
    {
        // The header is used to specify the coordinate frame and the reference time for the trajectory durations
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // A representation of a multi-dof joint trajectory (each point is a transformation)
        // Each point along the trajectory will include an array of positions/velocities/accelerations
        // that has the same length as the array of joint names, and has the same order of joints as 
        // the joint names array.
        [DataMember (Name = "joint_names")] public string[] JointNames;
        [DataMember (Name = "points")] public MultiDOFJointTrajectoryPoint[] Points;
    
        /// Constructor for empty message.
        public MultiDOFJointTrajectory()
        {
            JointNames = System.Array.Empty<string>();
            Points = System.Array.Empty<MultiDOFJointTrajectoryPoint>();
        }
        
        /// Explicit constructor.
        public MultiDOFJointTrajectory(in StdMsgs.Header Header, string[] JointNames, MultiDOFJointTrajectoryPoint[] Points)
        {
            this.Header = Header;
            this.JointNames = JointNames;
            this.Points = Points;
        }
        
        /// Constructor with buffer.
        public MultiDOFJointTrajectory(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.DeserializeStringArray(out JointNames);
            b.DeserializeArray(out Points);
            for (int i = 0; i < Points.Length; i++)
            {
                Points[i] = new MultiDOFJointTrajectoryPoint(ref b);
            }
        }
        
        /// Constructor with buffer.
        public MultiDOFJointTrajectory(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.DeserializeStringArray(out JointNames);
            b.DeserializeArray(out Points);
            for (int i = 0; i < Points.Length; i++)
            {
                Points[i] = new MultiDOFJointTrajectoryPoint(ref b);
            }
        }
        
        ISerializableRos1 ISerializableRos1.RosDeserializeBase(ref ReadBuffer b) => new MultiDOFJointTrajectory(ref b);
        
        public MultiDOFJointTrajectory RosDeserialize(ref ReadBuffer b) => new MultiDOFJointTrajectory(ref b);
        
        public MultiDOFJointTrajectory RosDeserialize(ref ReadBuffer2 b) => new MultiDOFJointTrajectory(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeArray(JointNames);
            b.SerializeArray(Points);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.SerializeArray(JointNames);
            b.SerializeArray(Points);
        }
        
        public void RosValidate()
        {
            if (JointNames is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < JointNames.Length; i++)
            {
                if (JointNames[i] is null) BuiltIns.ThrowNullReference(nameof(JointNames), i);
            }
            if (Points is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Points.Length; i++)
            {
                if (Points[i] is null) BuiltIns.ThrowNullReference(nameof(Points), i);
                Points[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 8;
                size += Header.RosMessageLength;
                size += WriteBuffer.GetArraySize(JointNames);
                size += WriteBuffer.GetArraySize(Points);
                return size;
            }
        }
        
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            Header.AddRos2MessageLength(ref c);
            WriteBuffer2.AddLength(ref c, JointNames);
            WriteBuffer2.AddLength(ref c, Points);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "trajectory_msgs/MultiDOFJointTrajectory";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "ef145a45a5f47b77b7f5cdde4b16c942";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
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
                
        public override string ToString() => Extensions.ToString(this);
    }
}
