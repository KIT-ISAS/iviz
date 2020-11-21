/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.TrajectoryMsgs
{
    [DataContract (Name = "trajectory_msgs/MultiDOFJointTrajectoryPoint")]
    public sealed class MultiDOFJointTrajectoryPoint : IDeserializable<MultiDOFJointTrajectoryPoint>, IMessage
    {
        // Each multi-dof joint can specify a transform (up to 6 DOF)
        [DataMember (Name = "transforms")] public GeometryMsgs.Transform[] Transforms { get; set; }
        // There can be a velocity specified for the origin of the joint 
        [DataMember (Name = "velocities")] public GeometryMsgs.Twist[] Velocities { get; set; }
        // There can be an acceleration specified for the origin of the joint 
        [DataMember (Name = "accelerations")] public GeometryMsgs.Twist[] Accelerations { get; set; }
        [DataMember (Name = "time_from_start")] public duration TimeFromStart { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public MultiDOFJointTrajectoryPoint()
        {
            Transforms = System.Array.Empty<GeometryMsgs.Transform>();
            Velocities = System.Array.Empty<GeometryMsgs.Twist>();
            Accelerations = System.Array.Empty<GeometryMsgs.Twist>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public MultiDOFJointTrajectoryPoint(GeometryMsgs.Transform[] Transforms, GeometryMsgs.Twist[] Velocities, GeometryMsgs.Twist[] Accelerations, duration TimeFromStart)
        {
            this.Transforms = Transforms;
            this.Velocities = Velocities;
            this.Accelerations = Accelerations;
            this.TimeFromStart = TimeFromStart;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public MultiDOFJointTrajectoryPoint(ref Buffer b)
        {
            Transforms = b.DeserializeStructArray<GeometryMsgs.Transform>();
            Velocities = b.DeserializeStructArray<GeometryMsgs.Twist>();
            Accelerations = b.DeserializeStructArray<GeometryMsgs.Twist>();
            TimeFromStart = b.Deserialize<duration>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new MultiDOFJointTrajectoryPoint(ref b);
        }
        
        MultiDOFJointTrajectoryPoint IDeserializable<MultiDOFJointTrajectoryPoint>.RosDeserialize(ref Buffer b)
        {
            return new MultiDOFJointTrajectoryPoint(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeStructArray(Transforms, 0);
            b.SerializeStructArray(Velocities, 0);
            b.SerializeStructArray(Accelerations, 0);
            b.Serialize(TimeFromStart);
        }
        
        public void RosValidate()
        {
            if (Transforms is null) throw new System.NullReferenceException(nameof(Transforms));
            if (Velocities is null) throw new System.NullReferenceException(nameof(Velocities));
            if (Accelerations is null) throw new System.NullReferenceException(nameof(Accelerations));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 20;
                size += 56 * Transforms.Length;
                size += 48 * Velocities.Length;
                size += 48 * Accelerations.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "trajectory_msgs/MultiDOFJointTrajectoryPoint";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "3ebe08d1abd5b65862d50e09430db776";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UTYvUQBC9D+Q/FMxlF2YjqOxB8OYHexAVFy8iQ0+nkmm30xWrK2bjr7c6ySQ7OsKC" +
                "MgMDlaTqVb1Xr3sNr43dQ916cVcFlfCNXBCwJkBs0LqyBwPCJsSSuIaLtgEhuIZX799cZqsKqUbhflvH" +
                "Kj65PaR9+bqUxGyVrdZwu0fGAXaHivgDPVkn/dTEYQGaDLJHIHaVC6CjpKdxnD86dS6KdplgHJ7sEsBY" +
                "ix7ZiKPwj60eQg3dinbCFVfjtmSqt1EMS/r28j//stW7T29fwF/kHpm7CIwNY8QgcSC0rG2H0iHqqB2B" +
                "JeLCBSMIJZsaI6gEJSOqQMZinub/jFaIn40IfqCZrT62WsMhUWaS6eWZuE4DnWKazJQ+/kZDF7mGGwFN" +
                "puB7qNHodtW7c6lWFo61Vonko3VULNyAEyhIdQmky1xDbe4UFENURQlM0/j5TIzSpNdac4F5lW+g26vQ" +
                "Q5YLlSYmiAoDsrOQ/FYse5mrDUwENyDlU+ic9+PUYzddZkI5qH6Zw00JPbXQJU4aMBRGdCZKvj9MZnY+" +
                "TUwbaNPoI8axrB8Gy6sHoqlQBYyCphgcUHoycv0c7pewX8KfZ1r7YrmTm9ejy07DUcej/aen74thk9qP" +
                "41VOYXe+c5xumJkf3id+Ue0335HHxHZMd+owXVxyXQTvAhpWMQr9V63XuNFrKCrdwzGech68mDITx195" +
                "wQ3PAgYAAA==";
                
    }
}
