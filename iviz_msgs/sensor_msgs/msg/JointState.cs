/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [Preserve, DataContract (Name = "sensor_msgs/JointState")]
    public sealed class JointState : IDeserializable<JointState>, IMessage
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
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "name")] public string[] Name;
        [DataMember (Name = "position")] public double[] Position;
        [DataMember (Name = "velocity")] public double[] Velocity;
        [DataMember (Name = "effort")] public double[] Effort;
    
        /// <summary> Constructor for empty message. </summary>
        public JointState()
        {
            Name = System.Array.Empty<string>();
            Position = System.Array.Empty<double>();
            Velocity = System.Array.Empty<double>();
            Effort = System.Array.Empty<double>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public JointState(in StdMsgs.Header Header, string[] Name, double[] Position, double[] Velocity, double[] Effort)
        {
            this.Header = Header;
            this.Name = Name;
            this.Position = Position;
            this.Velocity = Velocity;
            this.Effort = Effort;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal JointState(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Name = b.DeserializeStringArray();
            Position = b.DeserializeStructArray<double>();
            Velocity = b.DeserializeStructArray<double>();
            Effort = b.DeserializeStructArray<double>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new JointState(ref b);
        }
        
        JointState IDeserializable<JointState>.RosDeserialize(ref Buffer b)
        {
            return new JointState(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeArray(Name, 0);
            b.SerializeStructArray(Position, 0);
            b.SerializeStructArray(Velocity, 0);
            b.SerializeStructArray(Effort, 0);
        }
        
        public void RosValidate()
        {
            if (Name is null) throw new System.NullReferenceException(nameof(Name));
            for (int i = 0; i < Name.Length; i++)
            {
                if (Name[i] is null) throw new System.NullReferenceException($"{nameof(Name)}[{i}]");
            }
            if (Position is null) throw new System.NullReferenceException(nameof(Position));
            if (Velocity is null) throw new System.NullReferenceException(nameof(Velocity));
            if (Effort is null) throw new System.NullReferenceException(nameof(Effort));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 16;
                size += Header.RosMessageLength;
                size += BuiltIns.GetArraySize(Name);
                size += 8 * Position.Length;
                size += 8 * Velocity.Length;
                size += 8 * Effort.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "sensor_msgs/JointState";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "3066dcd76a6cfaef579bd0f34173e9fd";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
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
                
        public override string ToString() => Extensions.ToString(this);
    }
}
