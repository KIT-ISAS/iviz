
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
            "H4sIAAAAAAAAE71UTY/TQAy9z6+wtJddqQQJ0B6QuPGhPSBArLggVE0nTjrsZBw8Dtnw6/EkabJdFXEA" +
            "tVIlT2s/+73nmQt4Y90emi6If1JSBd/JRwFnI6QWna8GsCBsY6qIG7jsWhCCa3j94e2VqZEaFB62TarT" +
            "09tD1tdva0Uy5gJu98g4Yu5Q4X5iIOdlmDt4LEFTQfYIxL72EXSOfJpmedym90m0xYzi8USLCNY5DMhW" +
            "PMV/6/MQSVuV3QwqvsFtxdRsk1gWY1795495//ndS/iDxiNln4CxZUwYJY1MVqd2KD2ijtkTOCIufbSC" +
            "ULFtMIFyrxhRlbEOC2O+oBPi51N9GAmaT50WcMxcmWT67Swk52FOUMy7k/97ND9o6o2A5lIMAzRo1U5d" +
            "06VSC0vPWqocimlXVCTcgBcoSfWIJIrR2DuFxJgwV9u2Dcv2h9l0yiWXWNTFBvq96jtm+VhroiLUGJG9" +
            "g7xe5erGUmxhJrcBqZ5B70OYZp6aqYUKclD7qoCbCgbqoM+ENGAordgMtMNlLrsLeV7aQJcHHyGOBf04" +
            "7rf6nmyNql0StKW6XgWycv0C7pdoWKJfZ7F63bFTbsd8TzWc5DvyPJ9+rAuaRf4roUPUn+mu5gfkQAvv" +
            "M62kq7a8fsd8dkx3mEmOK5Yg+IiWVYNSv3UXNG71mUnFclfnlPU85xnzG339a7jVBQAA";

    }
}
