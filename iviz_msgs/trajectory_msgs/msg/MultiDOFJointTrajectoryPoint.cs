/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.TrajectoryMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class MultiDOFJointTrajectoryPoint : IDeserializable<MultiDOFJointTrajectoryPoint>, IMessage
    {
        // Each multi-dof joint can specify a transform (up to 6 DOF)
        [DataMember (Name = "transforms")] public GeometryMsgs.Transform[] Transforms;
        // There can be a velocity specified for the origin of the joint 
        [DataMember (Name = "velocities")] public GeometryMsgs.Twist[] Velocities;
        // There can be an acceleration specified for the origin of the joint 
        [DataMember (Name = "accelerations")] public GeometryMsgs.Twist[] Accelerations;
        [DataMember (Name = "time_from_start")] public duration TimeFromStart;
    
        /// Constructor for empty message.
        public MultiDOFJointTrajectoryPoint()
        {
            Transforms = System.Array.Empty<GeometryMsgs.Transform>();
            Velocities = System.Array.Empty<GeometryMsgs.Twist>();
            Accelerations = System.Array.Empty<GeometryMsgs.Twist>();
        }
        
        /// Explicit constructor.
        public MultiDOFJointTrajectoryPoint(GeometryMsgs.Transform[] Transforms, GeometryMsgs.Twist[] Velocities, GeometryMsgs.Twist[] Accelerations, duration TimeFromStart)
        {
            this.Transforms = Transforms;
            this.Velocities = Velocities;
            this.Accelerations = Accelerations;
            this.TimeFromStart = TimeFromStart;
        }
        
        /// Constructor with buffer.
        internal MultiDOFJointTrajectoryPoint(ref Buffer b)
        {
            Transforms = b.DeserializeStructArray<GeometryMsgs.Transform>();
            Velocities = b.DeserializeStructArray<GeometryMsgs.Twist>();
            Accelerations = b.DeserializeStructArray<GeometryMsgs.Twist>();
            TimeFromStart = b.Deserialize<duration>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new MultiDOFJointTrajectoryPoint(ref b);
        
        MultiDOFJointTrajectoryPoint IDeserializable<MultiDOFJointTrajectoryPoint>.RosDeserialize(ref Buffer b) => new MultiDOFJointTrajectoryPoint(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.SerializeStructArray(Transforms);
            b.SerializeStructArray(Velocities);
            b.SerializeStructArray(Accelerations);
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
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "trajectory_msgs/MultiDOFJointTrajectoryPoint";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "3ebe08d1abd5b65862d50e09430db776";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UTYvUQBC9968o2MsujBFU9iB484M9iIqLF5Ghp1PJtNvpitUVs/HXW51kkp1lxIMy" +
                "A4HqTNWreq9e+gLeWLeHpgvin5RUwXfyUcDZCKlF56sBLAjbmCriBi67FoTgGl5/eHtlaqQGhYdtk+r0" +
                "9PaQ9fXbWpGMuYDbPTKOmDtUuJ8YyHkZ5g4eS9BUkD0Csa99BJ0jn6ZZHrfpfRJtMaN4PNEignUOA7IV" +
                "T/Hf+jxE0lZlN4OKb3BbMTXbJJbFmFf/+Wfef373Ev6g8UjZJ2BsGRNGSSOTdVM7lB5Rx+wJHBGXPlpB" +
                "qNg2mEC5V4yoyliHhTFf0Anx86k+jATNp04LOGauTDK9OwvJeZgTFLN38n+P5gdNvRHQXIphgAatrlNt" +
                "ulRqYelZS5VDMXlFRcINeIGSVI9IohiNvVNIjEmFJLBtGxb3T5rk11pyiUVdbKDfq75jlo+1JipCjRHZ" +
                "O8j2KtdtLMUWZnIbkOoZ9D6Eaeapma5QQQ5qXxVwU8FAHfSZkAYMpRWdiLLLD3PZXcjz0ga6PPgIcSzo" +
                "x9Hfuvdka1TtkqAtdetVICvXL+B+iYYl+nWWVa8eO7Vt/UDZazjJd7TzfPqxGjSL/FdC1Rz1Z/pW8wVy" +
                "oIX3mVZSqy233zGfHdOd2kkXlS2WIPiIllWDUp+6Cxq3es2kYvlW55T1POcZ8xt9/Wu41QUAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
