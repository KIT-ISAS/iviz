/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.TrajectoryMsgs
{
    [Preserve, DataContract (Name = "trajectory_msgs/JointTrajectory")]
    public sealed class JointTrajectory : IDeserializable<JointTrajectory>, IMessage
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "joint_names")] public string[] JointNames { get; set; }
        [DataMember (Name = "points")] public JointTrajectoryPoint[] Points { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public JointTrajectory()
        {
            Header = new StdMsgs.Header();
            JointNames = System.Array.Empty<string>();
            Points = System.Array.Empty<JointTrajectoryPoint>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public JointTrajectory(StdMsgs.Header Header, string[] JointNames, JointTrajectoryPoint[] Points)
        {
            this.Header = Header;
            this.JointNames = JointNames;
            this.Points = Points;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public JointTrajectory(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            JointNames = b.DeserializeStringArray();
            Points = b.DeserializeArray<JointTrajectoryPoint>();
            for (int i = 0; i < Points.Length; i++)
            {
                Points[i] = new JointTrajectoryPoint(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new JointTrajectory(ref b);
        }
        
        JointTrajectory IDeserializable<JointTrajectory>.RosDeserialize(ref Buffer b)
        {
            return new JointTrajectory(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeArray(JointNames, 0);
            b.SerializeArray(Points, 0);
        }
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
            if (JointNames is null) throw new System.NullReferenceException(nameof(JointNames));
            for (int i = 0; i < JointNames.Length; i++)
            {
                if (JointNames[i] is null) throw new System.NullReferenceException($"{nameof(JointNames)}[{i}]");
            }
            if (Points is null) throw new System.NullReferenceException(nameof(Points));
            for (int i = 0; i < Points.Length; i++)
            {
                if (Points[i] is null) throw new System.NullReferenceException($"{nameof(Points)}[{i}]");
                Points[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 8;
                size += Header.RosMessageLength;
                size += 4 * JointNames.Length;
                foreach (string s in JointNames)
                {
                    size += BuiltIns.UTF8.GetByteCount(s);
                }
                foreach (var i in Points)
                {
                    size += i.RosMessageLength;
                }
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "trajectory_msgs/JointTrajectory";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "65b4f94a94d1ed67169da35a02f33d3f";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACrVUTWvcMBC9G/wfBD0kKd0ttKWHhR4K/UgLhUD2FhYzK43XCrLkSvIm/vd9IydZp/TQ" +
                "Q7t4MdLMvHkz88aXTIaj6sqrrlKO1h9uduo2WJ8bTz2nuvouh22kW9Y5xOlKjvAZ5A1zXX34x7+6+nH9" +
                "daNSNk2fDun15QO9F+o6kzcUjeo5k6FMqg2gbw8dx5XjIztEUT+wUcWap4HTWiK3nU0Kz4E9R3JuUmOC" +
                "Vw5Kh74fvdWUWWWLipcAEmq9IjVQzFaPjiICQjTWi38b0aKCL//EP0f2mtW3Txt4+cR6zBakJmDoyJTQ" +
                "XRjhPKJ3b99IBAK3d2GFMx8wiicGKneUhTHfD5GTkKW0kTQv5xrXgEeTGIlMUuflrsExXSjkAQsegu7U" +
                "OehfTbkLHoisjhQt7R0LskYfAHsmQWcXS2ihvlGefHjEnyFPSf4G15+ApaxVh+E5aUEaD+gjPIcYjtbA" +
                "dz8VFO0s+6yc3UeKU11J2JwUIF+k2XBDXJkN3pRS0BaTMOrO5u5RwfNcGmv+ozrz00LMIv3TlkjlnwlD" +
                "ODnPa6PSwNq2ljFe8Mbch5BsttDMzSsFxaCqDCsOpDU7SLYYdzuBDM/ducUS5F1ZBWniIhnkvYcQ7kWI" +
                "bIpQPzrsyEN2g7G5ESQoYnDzIJM0GQIHJ0rlpnwMIAYIU5x+K3SN6qXNrQuU378rH4YHbsvLU03L22fF" +
                "LQ1zTXVlxtlYBNS0MfQN5CCWuvoFTovuK70EAAA=";
                
    }
}
