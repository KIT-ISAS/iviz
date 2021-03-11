/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.TrajectoryMsgs
{
    [Preserve, DataContract (Name = "trajectory_msgs/MultiDOFJointTrajectoryPoint")]
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
        
        public void Dispose()
        {
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
                "H4sIAAAAAAAACr1UTWvcQAy9D+x/EOSSwNaFtORQyK0f5FDS0tBLKcusLXunGY9cjVzH/fXV2F47Gzb0" +
                "0LILBtkrPek9vZkzeGfzHdStF/eioBJ+kAsCuQ0QG8xd2YMFYRtiSVzDeduAEFzB29v3F6ZCqlG439Sx" +
                "ii/v9lnfvi8V0ZgzuNsh44C5RYX7hZ5yJ/3UwWEBmgqyQyB2lQugc6S3cZanbToXRVtMKA6PtAhg8xw9" +
                "shVH4d/6PEbSVkU7gYqrcVMy1ZsolsWszPV//q3Mxy8f3sAzKq8G1i4CY8MYMUgcyCzL2qJ0iDppR5AT" +
                "ceGCFYSSbY0RlH7JiCqOzTEz5ivmQvxqrPcDR/O51QIOiS6TjN9OxHMa5xjL5KD05xMKoKk3AppLwfdQ" +
                "o9WlqlnnSi0sHGup0shGx6hOuAYnUJBKEkgUo7b3CokhqpYEtmn8fAZGWdJnLTnHrMrW0O1U4iHLhUoT" +
                "FaHCgOxySCYrloXMxRYmdmuQ8hI65/0489hMt6gge8EvMrgpoacWukRIA4bCik5Eyev7uezWp3lpDW0a" +
                "fIA4VPTT4HJdfbQVqnZR0Ba6+NKTlavX8DBH/Rz9PtG2F6MdXbieVHYajgoerD29/VxsmnT+Gyezj7qT" +
                "Hdp0l8zM8CExi2q4+SY8pLRluldT6bqS0SJ4F9CyylDoU7Ve40avnJjNh3ZKWd6nPCX4B5ntXPjiBQAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
