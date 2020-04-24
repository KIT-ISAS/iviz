namespace Iviz.Msgs.trajectory_msgs
{
    public sealed class JointTrajectory : IMessage
    {
        public std_msgs.Header header;
        public string[] joint_names;
        public JointTrajectoryPoint[] points;
    
        /// <summary> Constructor for empty message. </summary>
        public JointTrajectory()
        {
            header = new std_msgs.Header();
            joint_names = System.Array.Empty<string>();
            points = System.Array.Empty<JointTrajectoryPoint>();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            header.Deserialize(ref ptr, end);
            BuiltIns.Deserialize(out joint_names, ref ptr, end, 0);
            BuiltIns.DeserializeArray(out points, ref ptr, end, 0);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            header.Serialize(ref ptr, end);
            BuiltIns.Serialize(joint_names, ref ptr, end, 0);
            BuiltIns.SerializeArray(points, ref ptr, end, 0);
        }
    
        public int GetLength()
        {
            int size = 12;
            size += header.GetLength();
            for (int i = 0; i < joint_names.Length; i++)
            {
                size += joint_names[i].Length;
            }
            for (int i = 0; i < points.Length; i++)
            {
                size += points[i].GetLength();
            }
            return size;
        }
    
        public IMessage Create() => new JointTrajectory();
    
        /// <summary> Full ROS name of this message. </summary>
        public const string _MessageType = "trajectory_msgs/JointTrajectory";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string _Md5Sum = "65b4f94a94d1ed67169da35a02f33d3f";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string _DependenciesBase64 =
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
