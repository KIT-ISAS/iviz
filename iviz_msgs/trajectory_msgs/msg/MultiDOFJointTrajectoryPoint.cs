using System.Runtime.Serialization;

namespace Iviz.Msgs.trajectory_msgs
{
    [DataContract]
    public sealed class MultiDOFJointTrajectoryPoint : IMessage
    {
        // Each multi-dof joint can specify a transform (up to 6 DOF)
        [DataMember] public geometry_msgs.Transform[] transforms { get; set; }
        
        // There can be a velocity specified for the origin of the joint 
        [DataMember] public geometry_msgs.Twist[] velocities { get; set; }
        
        // There can be an acceleration specified for the origin of the joint 
        [DataMember] public geometry_msgs.Twist[] accelerations { get; set; }
        
        [DataMember] public duration time_from_start { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public MultiDOFJointTrajectoryPoint()
        {
            transforms = System.Array.Empty<geometry_msgs.Transform>();
            velocities = System.Array.Empty<geometry_msgs.Twist>();
            accelerations = System.Array.Empty<geometry_msgs.Twist>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public MultiDOFJointTrajectoryPoint(geometry_msgs.Transform[] transforms, geometry_msgs.Twist[] velocities, geometry_msgs.Twist[] accelerations, duration time_from_start)
        {
            this.transforms = transforms ?? throw new System.ArgumentNullException(nameof(transforms));
            this.velocities = velocities ?? throw new System.ArgumentNullException(nameof(velocities));
            this.accelerations = accelerations ?? throw new System.ArgumentNullException(nameof(accelerations));
            this.time_from_start = time_from_start;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal MultiDOFJointTrajectoryPoint(Buffer b)
        {
            this.transforms = b.DeserializeStructArray<geometry_msgs.Transform>();
            this.velocities = b.DeserializeArray<geometry_msgs.Twist>();
            for (int i = 0; i < this.velocities.Length; i++)
            {
                this.velocities[i] = new geometry_msgs.Twist(b);
            }
            this.accelerations = b.DeserializeArray<geometry_msgs.Twist>();
            for (int i = 0; i < this.accelerations.Length; i++)
            {
                this.accelerations[i] = new geometry_msgs.Twist(b);
            }
            this.time_from_start = b.Deserialize<duration>();
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new MultiDOFJointTrajectoryPoint(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.SerializeStructArray(this.transforms, 0);
            b.SerializeArray(this.velocities, 0);
            b.SerializeArray(this.accelerations, 0);
            b.Serialize(this.time_from_start);
        }
        
        public void Validate()
        {
            if (transforms is null) throw new System.NullReferenceException();
            if (velocities is null) throw new System.NullReferenceException();
            for (int i = 0; i < velocities.Length; i++)
            {
                if (velocities[i] is null) throw new System.NullReferenceException();
                velocities[i].Validate();
            }
            if (accelerations is null) throw new System.NullReferenceException();
            for (int i = 0; i < accelerations.Length; i++)
            {
                if (accelerations[i] is null) throw new System.NullReferenceException();
                accelerations[i].Validate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 20;
                size += 56 * transforms.Length;
                size += 48 * velocities.Length;
                size += 48 * accelerations.Length;
                return size;
            }
        }
    
        string IMessage.RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "trajectory_msgs/MultiDOFJointTrajectoryPoint";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "3ebe08d1abd5b65862d50e09430db776";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
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
