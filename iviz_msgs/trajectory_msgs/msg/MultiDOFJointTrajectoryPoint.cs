using System.Runtime.Serialization;

namespace Iviz.Msgs.TrajectoryMsgs
{
    [DataContract (Name = "trajectory_msgs/MultiDOFJointTrajectoryPoint")]
    public sealed class MultiDOFJointTrajectoryPoint : IMessage
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
        internal MultiDOFJointTrajectoryPoint(Buffer b)
        {
            Transforms = b.DeserializeArray<GeometryMsgs.Transform>();
            for (int i = 0; i < this.Transforms.Length; i++)
            {
                Transforms[i] = new GeometryMsgs.Transform(b);
            }
            Velocities = b.DeserializeArray<GeometryMsgs.Twist>();
            for (int i = 0; i < this.Velocities.Length; i++)
            {
                Velocities[i] = new GeometryMsgs.Twist(b);
            }
            Accelerations = b.DeserializeArray<GeometryMsgs.Twist>();
            for (int i = 0; i < this.Accelerations.Length; i++)
            {
                Accelerations[i] = new GeometryMsgs.Twist(b);
            }
            TimeFromStart = b.Deserialize<duration>();
        }
        
        ISerializable ISerializable.Deserialize(Buffer b)
        {
            return new MultiDOFJointTrajectoryPoint(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        void ISerializable.Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.SerializeArray(Transforms, 0);
            b.SerializeArray(Velocities, 0);
            b.SerializeArray(Accelerations, 0);
            b.Serialize(this.TimeFromStart);
        }
        
        public void Validate()
        {
            if (Transforms is null) throw new System.NullReferenceException();
            for (int i = 0; i < Transforms.Length; i++)
            {
                if (Transforms[i] is null) throw new System.NullReferenceException();
                Transforms[i].Validate();
            }
            if (Velocities is null) throw new System.NullReferenceException();
            for (int i = 0; i < Velocities.Length; i++)
            {
                if (Velocities[i] is null) throw new System.NullReferenceException();
                Velocities[i].Validate();
            }
            if (Accelerations is null) throw new System.NullReferenceException();
            for (int i = 0; i < Accelerations.Length; i++)
            {
                if (Accelerations[i] is null) throw new System.NullReferenceException();
                Accelerations[i].Validate();
            }
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
