using System.Runtime.Serialization;

namespace Iviz.Msgs.trajectory_msgs
{
    public sealed class JointTrajectory : IMessage
    {
        public std_msgs.Header header { get; set; }
        public string[] joint_names { get; set; }
        public JointTrajectoryPoint[] points { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public JointTrajectory()
        {
            header = new std_msgs.Header();
            joint_names = System.Array.Empty<string>();
            points = System.Array.Empty<JointTrajectoryPoint>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public JointTrajectory(std_msgs.Header header, string[] joint_names, JointTrajectoryPoint[] points)
        {
            this.header = header ?? throw new System.ArgumentNullException(nameof(header));
            this.joint_names = joint_names ?? throw new System.ArgumentNullException(nameof(joint_names));
            this.points = points ?? throw new System.ArgumentNullException(nameof(points));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal JointTrajectory(Buffer b)
        {
            this.header = new std_msgs.Header(b);
            this.joint_names = BuiltIns.DeserializeStringArray(b, 0);
            this.points = BuiltIns.DeserializeArray<JointTrajectoryPoint>(b, 0);
        }
        
        public IMessage Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new JointTrajectory(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            this.header.Serialize(b);
            BuiltIns.Serialize(this.joint_names, b, 0);
            BuiltIns.SerializeArray(this.points, b, 0);
        }
        
        public void Validate()
        {
            if (header is null) throw new System.NullReferenceException();
            if (joint_names is null) throw new System.NullReferenceException();
            if (points is null) throw new System.NullReferenceException();
        }
    
        [IgnoreDataMember]
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
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve]
        public const string RosMessageType = "trajectory_msgs/JointTrajectory";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve]
        public const string RosMd5Sum = "65b4f94a94d1ed67169da35a02f33d3f";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve]
        public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE7VUTYvbMBC961cM5LC7pUmhLT0Eeij0Y1soLGxuSzATaRJrkSVXkrPrf98neZt4Sw89" +
                "tMYgS/PmzdeTr4WNRGrrolKO1h/utnQfrM+N506S+la+N5HvRecQx5uyBaQva1Lq/T9+1PfbL2tK2TRd" +
                "OqRX11NmC7rN7A1HQ51kNpyZ9gGJ20MrcenkKA5O3PViqFrz2EtawXHT2kR4D+IlsnMjDQmgHEiHrhu8" +
                "1ZyFskWtc394Wk9MPcds9eA4Ah+isb7A9xG9Kex4k/wYxGuhrx/XwPgkesgWCY1g0FE4oakwkhrQsjev" +
                "i4NabB7CEls5oP2n4JRbziVZeeyjpJInpzVivJiKW4EbzRFEMYku61mDbboiBEEK0gfd0iUyvxlzGzwI" +
                "hY4cLe+cFGKNDoD1ojhdXM2YfaX27MMv+onxHONvaP2Jt9S0bDEzV6pPwwENBLCP4WgNoLuxkmhnxWdy" +
                "dhc5jqp4TSHV4nPpMUDwqhPByikFbTEAQw82t0+CnabRWPO/1JhP6p9E+acrgZI/MXp/xk5XhFIv2u6t" +
                "YKhIGdPuQ7LZQid3LwkqQUEZVmxYa3HQaDVut2AMz9Gyh+bztiq/NG8WC3LeYfyPRXtiijI/OHeKbTAs" +
                "NyAFjlJkXZxT6S4EjYw41ZN666ne+gL6rcoVSldq7wLnd2/rD+ApsdnZuZzZ4bOyZudTNcoMk6kqptnH" +
                "0DUQAAzqJ1kk7fqdBAAA";
                
    }
}
