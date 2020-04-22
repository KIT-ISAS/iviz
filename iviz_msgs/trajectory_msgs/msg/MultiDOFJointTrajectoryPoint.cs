
namespace Iviz.Msgs.trajectory_msgs
{
    public sealed class MultiDOFJointTrajectoryPoint : IMessage
    {
        // Each multi-dof joint can specify a transform (up to 6 DOF)
        public geometry_msgs.Transform[] transforms;
        
        // There can be a velocity specified for the origin of the joint 
        public geometry_msgs.Twist[] velocities;
        
        // There can be an acceleration specified for the origin of the joint 
        public geometry_msgs.Twist[] accelerations;
        
        public duration time_from_start;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "trajectory_msgs/MultiDOFJointTrajectoryPoint";
    
        public IMessage Create() => new MultiDOFJointTrajectoryPoint();
    
        public int GetLength()
        {
            int size = 20;
            size += 56 * transforms.Length;
            size += 48 * velocities.Length;
            size += 48 * accelerations.Length;
            return size;
        }
    
        /// <summary> Constructor for empty message. </summary>
        public MultiDOFJointTrajectoryPoint()
        {
            transforms = new geometry_msgs.Transform[0];
            velocities = new geometry_msgs.Twist[0];
            accelerations = new geometry_msgs.Twist[0];
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.DeserializeStructArray(out transforms, ref ptr, end, 0);
            BuiltIns.DeserializeArray(out velocities, ref ptr, end, 0);
            BuiltIns.DeserializeArray(out accelerations, ref ptr, end, 0);
            BuiltIns.Deserialize(out time_from_start, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.SerializeStructArray(transforms, ref ptr, end, 0);
            BuiltIns.SerializeArray(velocities, ref ptr, end, 0);
            BuiltIns.SerializeArray(accelerations, ref ptr, end, 0);
            BuiltIns.Serialize(time_from_start, ref ptr, end);
        }
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "3ebe08d1abd5b65862d50e09430db776";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAACr1UTWvcQAy9D+x/EOSSwNaFtORQyK0f5FDS0tBLKcusLXunGY9cjVzH/fXV2F47Gzb0" +
                "0LILBtkrPek9vZkzeGfzHdStF/eioBJ+kAsCuQ0QG8xd2YMFYRtiSVzDeduAEFzB29v3F6ZCqlG439Sx" +
                "ii/v9lnfvi8V0ZgzuNsh44C5RYX7hZ5yJ/3UwWEBmgqyQyB2lQugc6S3cZanbToXRVtMKA6PtAhg8xw9" +
                "shVH4d/6PEbSVkU7gYqrcVMy1ZsolsWszPV//q3Mxy8f3sAzKq8G1i4CY8MYMUgcyCzL2qJ0iDppR5AT" +
                "ceGCFYSSbY0RlH7JiCqOzTEz5ivmQvxqrPcDR/O51QIOiS6TjN9OxHMa5xjL5KD05xMKoKk3AppLwfdQ" +
                "o9WlqlnnSi0sHGup0shGx6hOuAYnUJBKEkgUo7b3CokhqpYEtmn8fAZGWdJnLTnHrMrW0O1U4iHLhUoT" +
                "FaHCgOxySCYrloXMxRYmdmuQ8hI65/0489hMt6gge8EvMrgpoacWukRIA4bCik5Eyev7uezWp3lpDW0a" +
                "fIA4VPTT4HJdfbQVqnZR0Ba6+NKTlavX8DBH/Rz9PtG2F6MdXbieVHYajgoerD29/VxsmnT+Gyezj7qT" +
                "Hdp0l8zM8CExi2q4+SY8pLRluldT6bqS0SJ4F9CyylDoU7Ve40avnJjNh3ZKWd6nPCX4B5ntXPjiBQAA";
                
    }
}
