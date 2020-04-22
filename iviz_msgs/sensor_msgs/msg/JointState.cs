
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
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "sensor_msgs/JointState";
    
        public IMessage Create() => new JointState();
    
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
    
        /// <summary> Constructor for empty message. </summary>
        public JointState()
        {
            header = new std_msgs.Header();
            name = System.Array.Empty<0>();
            position = System.Array.Empty<0>();
            velocity = System.Array.Empty<0>();
            effort = System.Array.Empty<0>();
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
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "3066dcd76a6cfaef579bd0f34173e9fd";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAACq1Uy2ocMRC8D/gfGvZgr7HHkIQcFnII5HmICdiQQwhGK/XOKNFIE0njzeTrUy3ty+SS" +
                "Q8weLE13dXdVqRd039tE+CkaOCXVMeVeZeqDM4mMyopyIMNJR7uWb0wpq8wUNkhJnOWfHOLPiUkHn2Nw" +
                "jg19D9bn1FKzaBYocZLESvf1M11EfgxukvtIY7RpUNnqpXRjeGM9cNbzCgB0WQqPIdlsgy8lcd6jKCMA" +
                "w/LqGPrILmib579Db1IJvklLUt7QMYU3mxBznV74GEdn0YH1pwC3g2TfLtsy2NvjLMiYvAULbiZr2Ge7" +
                "kew1TjmRVwPviOhZGY6URtYSkgp6tgMT6m57C8RjvcJaoi1Hpsg6RMOmpdfO/RUDdHQaPB9k7NUjgAOt" +
                "j6lSoiiIdkrNOkaxwD4NIiab0HMReJhctqNDczGqOV2VCuCpyjgqEPaE4dKMyF5m7YJywgy6GNQPrkm7" +
                "eMwuDgujKKpcS1969sRt19IcprhzUJ3CBwDu9FEpQVlUMbS1uZA1XEkKaeXJcZn6KGfpm3gY87xzo7BX" +
                "p6nansye+jA5s2Nuz1OyvxlzRyGy4uwpk8kQFTw036IKxjx44NDmCTnigkPTIDpClwywqmDbNM2Hao7q" +
                "kaZJOVrfff1W/bNxQeWXL3DcP4STq73hT64qA81Z8+o//501n+7er9C2eRhSl25q12eY5C7jTalowGhW" +
                "ZXmIV3rb9RyvHaNJGXYYIV5dLfMogx/57NhzVA4ETglBYFSHYQCrulAJyz7Jr65XxYhWT05FxMPq1kv4" +
                "JoI2QReOGbp4zfTxzap4nPWULRrCA/U6skpgGh+pmSDV82eS0Czut+EaR+4gyqH4YUXwrzHCOfKukqyp" +
                "yzpcC2ywgzfnYfCLcveAIzYOimi4aAx4Bxfo/POce+yzsrFUtGqNlwZgDQaAei5J58sTZGl7BTf4sIev" +
                "iMca/wIrKBVXZrruoZmT6dPUgUAEjjE8YomV9VWsik0I/zq7jirOTdlWpWSzeCcc11dUFJHN+fSF7lxc" +
                "1XiwBob8AyOv5l52BgAA";
                
    }
}
