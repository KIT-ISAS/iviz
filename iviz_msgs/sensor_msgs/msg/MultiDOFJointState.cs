/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [Preserve, DataContract (Name = "sensor_msgs/MultiDOFJointState")]
    public sealed class MultiDOFJointState : IDeserializable<MultiDOFJointState>, IMessage
    {
        // Representation of state for joints with multiple degrees of freedom, 
        // following the structure of JointState.
        //
        // It is assumed that a joint in a system corresponds to a transform that gets applied 
        // along the kinematic chain. For example, a planar joint (as in URDF) is 3DOF (x, y, yaw)
        // and those 3DOF can be expressed as a transformation matrix, and that transformation
        // matrix can be converted back to (x, y, yaw)
        //
        // Each joint is uniquely identified by its name
        // The header specifies the time at which the joint states were recorded. All the joint states
        // in one message have to be recorded at the same time.
        //
        // This message consists of a multiple arrays, one for each part of the joint state. 
        // The goal is to make each of the fields optional. When e.g. your joints have no
        // wrench associated with them, you can leave the wrench array empty. 
        //
        // All arrays in this message should have the same size, or be empty.
        // This is the only way to uniquely associate the joint name with the correct
        // states.
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "joint_names")] public string[] JointNames { get; set; }
        [DataMember (Name = "transforms")] public GeometryMsgs.Transform[] Transforms { get; set; }
        [DataMember (Name = "twist")] public GeometryMsgs.Twist[] Twist { get; set; }
        [DataMember (Name = "wrench")] public GeometryMsgs.Wrench[] Wrench { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public MultiDOFJointState()
        {
            Header = new StdMsgs.Header();
            JointNames = System.Array.Empty<string>();
            Transforms = System.Array.Empty<GeometryMsgs.Transform>();
            Twist = System.Array.Empty<GeometryMsgs.Twist>();
            Wrench = System.Array.Empty<GeometryMsgs.Wrench>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public MultiDOFJointState(StdMsgs.Header Header, string[] JointNames, GeometryMsgs.Transform[] Transforms, GeometryMsgs.Twist[] Twist, GeometryMsgs.Wrench[] Wrench)
        {
            this.Header = Header;
            this.JointNames = JointNames;
            this.Transforms = Transforms;
            this.Twist = Twist;
            this.Wrench = Wrench;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public MultiDOFJointState(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            JointNames = b.DeserializeStringArray();
            Transforms = b.DeserializeStructArray<GeometryMsgs.Transform>();
            Twist = b.DeserializeStructArray<GeometryMsgs.Twist>();
            Wrench = b.DeserializeArray<GeometryMsgs.Wrench>();
            for (int i = 0; i < Wrench.Length; i++)
            {
                Wrench[i] = new GeometryMsgs.Wrench(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new MultiDOFJointState(ref b);
        }
        
        MultiDOFJointState IDeserializable<MultiDOFJointState>.RosDeserialize(ref Buffer b)
        {
            return new MultiDOFJointState(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeArray(JointNames, 0);
            b.SerializeStructArray(Transforms, 0);
            b.SerializeStructArray(Twist, 0);
            b.SerializeArray(Wrench, 0);
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
            if (Transforms is null) throw new System.NullReferenceException(nameof(Transforms));
            if (Twist is null) throw new System.NullReferenceException(nameof(Twist));
            if (Wrench is null) throw new System.NullReferenceException(nameof(Wrench));
            for (int i = 0; i < Wrench.Length; i++)
            {
                if (Wrench[i] is null) throw new System.NullReferenceException($"{nameof(Wrench)}[{i}]");
                Wrench[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 16;
                size += Header.RosMessageLength;
                size += 4 * JointNames.Length;
                foreach (string s in JointNames)
                {
                    size += BuiltIns.UTF8.GetByteCount(s);
                }
                size += 56 * Transforms.Length;
                size += 48 * Twist.Length;
                size += 48 * Wrench.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "sensor_msgs/MultiDOFJointState";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "690f272f0640d2631c305eeb8301e59d";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1WTW/kNgy9B8h/ELCHTYrJFNgUPQTooUCaNgWKbnfT7qEoAo7NsdXIkleSx5n99X2k" +
                "7JnJR4Ec2gRB4g/yiY98JP3GfOA+cmKfKdvgTVibhEs26xDN38H6nMxoc2u6wWXbOzY1N5E5ieUaF3Xo" +
                "Fub46A0cnAuj9Y3JLQMkDlUeIovdz4LzUWCXsBTj62xsMpTS0HENB8qGynHGelymbcrcmSpEBNcHXyeT" +
                "A57nSD4htK74NIzwqO+dBYrgkgtTAHfWcwdOlalasn5prkCI76kDhwWQekeeJormhJKc+/uHy6tTCez8" +
                "8tcrc3K/MFv80niq0F4CDYnL24q8WTEQJX0JxwPiIL6STfyLFjDFFwE/fC+wxWSGq4LfcMyAW1F1J5wf" +
                "RiEeP1DVzrlKZvD288Bua2yNItq1ZGKFOyTGU8ficIN0tEw1R5N6rsQG6cTDbDs2iGpsLSDlSYFVBaDu" +
                "jPJFRhFqrpfme+ee2Ag8Ehc8mw5poAYn0QbIQdjMvnKGigIB6aGzDG5aMJgdwT3ZhLihGNrrjWKkbVro" +
                "GSJKFvo9xSx2j+JZmplvE8hJehBIR3dcvCYH8HcQVOilBuSW5lPL3vCyWZptGHayVyI+COIY2cMfeg2V" +
                "xTl16QmAQfzw0fI5VuY4YDaXyA13fd5qYIIkSSyMJG/5kH9qw+DqKX9ztpL9ArmCtmhNkXZ5E3YwCx7F" +
                "H3EQqO7EsIv0IEMih13cpbWqLGillAA+PvqpyKSoRR6gj9HSf/5VMG4FA0VvOHSc4/a2S036+mYWNcx2" +
                "An9qNaK4YiH/H7/8pBnD25I6Ofm7//jn+OiXjz9egGxdjixUhT8mk68p1qhEppoyqc5a27Qczxxv2EmK" +
                "uh5l17d522u69nVo2HMkh8QPMgpQiSp0HapRaQmg+AcAU9eQythWg8MgqgJaxXodvRFZVnytDqOkvmJz" +
                "fXmhPcLVkC2CQpP7KjIlmbnXlzAeUKLzd+IBx5sxnOGeG9RzF0GZQoj4cG5dyDFfFY5LwCNJaFyZuSf6" +
                "7Ba36RRSlCi4D5D2CcJ/v8U4FA2z2VC0tEKzArlCHgD7Vpzenh5CS+gXkKEPM36B3B/yEly/BxZaZy2K" +
                "5yQFaWiQR1j2MWwwDXUOqtSxHqB/Z1eR4vb4SMeeHgqQK0l2aUStTVlLh20+N0Gpy62t/0d1/ktj7bQW" +
                "5209DfDdOlxxHhlTLI/hiZR01MiuxvinSpR1fPQHej/E84Lgpm302wCf6GVzxVA+CF6N6xTQc0zJbPTl" +
                "Ixpm/yGhM7BjQpXReztXeNZWphyILGUtREayME9tNnVAXnzQAagbAg7Y7fKVge8JTNDD1Mhj+JzIjlhg" +
                "XSLRaiWqKO2sEwAfG9E2tn78GaCfLhPBhcnrd9AV9oBGXU5DMQVlzvrp0lyvdbGMwkm3Uhk9ulfnyLQ1" +
                "cggLGTszxsO0vtfRP68Y6/FZRbUqYO0C5W+/Mff7S/TGfPnllcq+l9yzlcenRZTmLXl8UH+5+7wXrGT7" +
                "Zbzmy/H1+risvInfPHoTZO0waPL2EbFVDHdQGAonqksYXJ4x2eQjknyj20IWh6yguY1NsTl4MFm+Gsey" +
                "wZ8rIipTqrWnuEC3gYKOWKGpC/GFTBXu4B5/sSCF5z/TTkt9zQwAAA==";
                
    }
}
