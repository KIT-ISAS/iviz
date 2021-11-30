/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.TrajectoryMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class JointTrajectory : IDeserializable<JointTrajectory>, IMessage
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "joint_names")] public string[] JointNames;
        [DataMember (Name = "points")] public JointTrajectoryPoint[] Points;
    
        /// Constructor for empty message.
        public JointTrajectory()
        {
            JointNames = System.Array.Empty<string>();
            Points = System.Array.Empty<JointTrajectoryPoint>();
        }
        
        /// Explicit constructor.
        public JointTrajectory(in StdMsgs.Header Header, string[] JointNames, JointTrajectoryPoint[] Points)
        {
            this.Header = Header;
            this.JointNames = JointNames;
            this.Points = Points;
        }
        
        /// Constructor with buffer.
        internal JointTrajectory(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            JointNames = b.DeserializeStringArray();
            Points = b.DeserializeArray<JointTrajectoryPoint>();
            for (int i = 0; i < Points.Length; i++)
            {
                Points[i] = new JointTrajectoryPoint(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new JointTrajectory(ref b);
        
        JointTrajectory IDeserializable<JointTrajectory>.RosDeserialize(ref Buffer b) => new JointTrajectory(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeArray(JointNames);
            b.SerializeArray(Points);
        }
        
        public void RosValidate()
        {
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
                size += BuiltIns.GetArraySize(JointNames);
                size += BuiltIns.GetArraySize(Points);
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "trajectory_msgs/JointTrajectory";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "65b4f94a94d1ed67169da35a02f33d3f";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
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
                
        public override string ToString() => Extensions.ToString(this);
    }
}
