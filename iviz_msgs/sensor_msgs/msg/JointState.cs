namespace Iviz.Msgs.sensor_msgs
{
    public sealed class JointState : IMessage
    {
        // This is a message that holds data to describe the state of a set of torque controlled joints. 
        //
        // The state of each joint (revolute or prismatic) is defined by:
        //  * the position of the joint (rad or m),
        //  * the velocity of the joint (rad/s or m/s) and 
        //  * the effort that is applied in the joint (Nm or N).
        //
        // Each joint is uniquely identified by its name
        // The header specifies the time at which the joint states were recorded. All the joint states
        // in one message have to be recorded at the same time.
        //
        // This message consists of a multiple arrays, one for each part of the joint state. 
        // The goal is to make each of the fields optional. When e.g. your joints have no
        // effort associated with them, you can leave the effort array empty. 
        //
        // All arrays in this message should have the same size, or be empty.
        // This is the only way to uniquely associate the joint name with the correct
        // states.
        
        
        public std_msgs.Header header;
        
        public string[] name;
        public double[] position;
        public double[] velocity;
        public double[] effort;
    
        /// <summary> Constructor for empty message. </summary>
        public JointState()
        {
            header = new std_msgs.Header();
            name = System.Array.Empty<string>();
            position = System.Array.Empty<double>();
            velocity = System.Array.Empty<double>();
            effort = System.Array.Empty<double>();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            header.Deserialize(ref ptr, end);
            BuiltIns.Deserialize(out name, ref ptr, end, 0);
            BuiltIns.Deserialize(out position, ref ptr, end, 0);
            BuiltIns.Deserialize(out velocity, ref ptr, end, 0);
            BuiltIns.Deserialize(out effort, ref ptr, end, 0);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            header.Serialize(ref ptr, end);
            BuiltIns.Serialize(name, ref ptr, end, 0);
            BuiltIns.Serialize(position, ref ptr, end, 0);
            BuiltIns.Serialize(velocity, ref ptr, end, 0);
            BuiltIns.Serialize(effort, ref ptr, end, 0);
        }
    
        public int GetLength()
        {
            int size = 20;
            size += header.GetLength();
            for (int i = 0; i < name.Length; i++)
            {
                size += name[i].Length;
            }
            size += 8 * position.Length;
            size += 8 * velocity.Length;
            size += 8 * effort.Length;
            return size;
        }
    
        public IMessage Create() => new JointState();
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "sensor_msgs/JointState";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "3066dcd76a6cfaef579bd0f34173e9fd";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAAE61UTW/UMBC951eMtId20TaVAHGoxAGJzwMVUitxQKiajWcTg2MH29kl/Hqe7exmq144" +
                "UO2hdmbezLz3PCu673Qg/Jh6CYFbodhxpM4ZFUhxZIqOlITG6236JhQiRyG3Q0qQmP6Jzv8ahRpno3fG" +
                "iKIfTtsYaqpW1QolzpKEm658pksve2fGdO9p8Dr0HHWzTt0o2WkLnO10AwB6lgsPLuionc0lcT6isEoA" +
                "/XqzhO7FuEbH6WnodcjB12FNbBUtKbLbOR/L9ImPYTAaHWh7DnDbp+zbdZ0He7fMgozRarBgJtJKbNQ7" +
                "nfsnHQNZ7mUmohNW4ikM0qSQkNGj7oVQ99BpIC71MmuBDuKFvDTOK1E1vTHmSQzQ0amzcpKx470k7bZL" +
                "aiqRFUQ7uWY964Puj2kQMeiAnrPA/WiiHgya856nsMkVwFORcWAfHzOcm0my51lbxyYxgy56/iklaY7H" +
                "7MlhbkiKsqnpayeWpG5rmtzoZweVKawD4KwPhwBlUUXRQcdMVr9JKdSwJSN56kXO3DdJP8RpdmNir0xT" +
                "tD2bPXRuNGpm7shT0H9kk1QHkQXnSJku4jkLzQ+ogjFPHji1eUZOcsGpaRDtoUsEWFGwrqrqYzFH8UhV" +
                "hei1bb99L/7ZGcfx1Uscjw/h7Opo+LOrwkBVvf7Pf9Xnuw83aFo99KEN16VnjHEX8aDYK9AZOW+OZJRO" +
                "t534KyPoME3aD1Cu7JVpSFMvZLZixbMBe2NAEOhsXN+D0ibzCL8+yi+W5+xC3YyGPeLhc21T+M5z9ncK" +
                "CwJRbCP06e1NNrg0Y9T7/FZt44UDaMZHqkbo9OJ5SqhW9wd3haO0UORU/LQf5PfgYZv0qELaUc/KcDWw" +
                "QQ4enIW7L/PdA45YNyiCFmRweASX6PzLFDtXtsueveYtnhmAG84b9CIlXazPkG2GtmzdEb4gLjX+Bdae" +
                "cNNMVx00M2n6MLYgEIGDd3utyu7KPsUahHmN3nr2U5VXVS5Zrd77vEiSfFmRtDYfP8/ZwkWNB62q6i8m" +
                "nvAbcgYAAA==";
                
    }
}
