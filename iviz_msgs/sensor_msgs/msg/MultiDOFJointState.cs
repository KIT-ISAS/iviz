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
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "joint_names")] public string[] JointNames;
        [DataMember (Name = "transforms")] public GeometryMsgs.Transform[] Transforms;
        [DataMember (Name = "twist")] public GeometryMsgs.Twist[] Twist;
        [DataMember (Name = "wrench")] public GeometryMsgs.Wrench[] Wrench;
    
        /// <summary> Constructor for empty message. </summary>
        public MultiDOFJointState()
        {
            JointNames = System.Array.Empty<string>();
            Transforms = System.Array.Empty<GeometryMsgs.Transform>();
            Twist = System.Array.Empty<GeometryMsgs.Twist>();
            Wrench = System.Array.Empty<GeometryMsgs.Wrench>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public MultiDOFJointState(in StdMsgs.Header Header, string[] JointNames, GeometryMsgs.Transform[] Transforms, GeometryMsgs.Twist[] Twist, GeometryMsgs.Wrench[] Wrench)
        {
            this.Header = Header;
            this.JointNames = JointNames;
            this.Transforms = Transforms;
            this.Twist = Twist;
            this.Wrench = Wrench;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal MultiDOFJointState(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
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
                size += BuiltIns.GetArraySize(JointNames);
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
                "H4sIAAAAAAAACr1WTW/cNhC9C/B/IOBD7GC9BeKiBwM9FHDdukDRNHGTQ1EYs9KsxJoiFZJaWfn1eUNK" +
                "2vVH0RxaG4atj5nhvDdvZnSs3nHnObCNFLWzym1VwCWrrfPqb6dtDGrQsVFtb6LuDKuKa88cxHKLi8q1" +
                "K1Ucw94YN2hbq9gwYvi+jL1nMftFwryXqOviGKbXUemgKIS+5QrmFBXls5S2uAxjiNyq0nlk1jlbBRUd" +
                "nkdPNiCvNvvUjNyo64xGFIQl46bT77TlFnhKVTak7VpdAQzfU4v8VwjUGbI0wVMnFOTYP95dXp1KXueX" +
                "v12pk/uVGvFLw6lEtpKmC5xflmTVhhFQmAs4HBEOsstE4p/XiJJ9ke7D94iaLeZopbM79hHRNlTeCeAH" +
                "OcD+Ryqbmaageqs/9WxGpSsUT2+FhA3uwImllmF/AyYapoq9Ch2XYgIi8TDqlhUyGhqNiPIkR02FR7kZ" +
                "ZfMM+iuu1uoHY57YIDooc5ZVCwaoxkG0Q2AnSGZXOSJpAemkM3P1bxpkP7sBddABOUMmtNcYeU9jWKUT" +
                "RIgs0DvyUeweJbNWE9bakRFmkEVLd5ydJntgN5CR64R7Mmv1sWGreF2v1ej6RekJhXUIOHi2cIdIXalx" +
                "SpW7ALEgd7ikshlOqBF/Npe8FbddHCUtxBH2MhphLB5iD43rTTUxN/MU9GdIFJBFYCnOTJkgg5WzqPmA" +
                "UwBz0cCS5gE5ooIl6dxMZUSwXMF1UfyctZElUhRoWXTvn39l91txD0XNruXox9s21OGbm1nCsFrk/MRo" +
                "QEHFQP4/evcx0YSXma/iqPj+P/45Kn59/9MFQFb5xAzyCLgxgGxFvgL/kSqKlJTV6Lphf2Z4x0aoaTuU" +
                "Or2NYyc07dmv2bInA7p7aXrwX7q2RQ3KRDwE/sA/9wgl2eqyNxg4pUNjaJvGqwe/El0qwqiiLVldX16k" +
                "juCyjxoJoZ1t6ZmCjNXrS1X0qMz5G3Eojm8Gd4ZbrlHD5fA8apDs4XC6wBmvM7g1YoMddKhM1ZP07Ba3" +
                "4RTKkxS4c9DxCTJ/O2LkiWRZ7chr2qAvEbgEA4j6SpxenR5ElrQvoDrr5vA54v6Mrwlrl7iC6axBzYyg" +
                "D30NAmHYebfDyEvDLgkb4x9qN3rjyY9Fmm3pyOL4SjjOPZcqkpfOYT9Pos/VuNXV/yfIf2iko1lffl7D" +
                "04heVt2G48CYVXFwT/STZoosYQx4KiGn4gN63Pnz7G/ypvm9h4O3spS8y2v+pXBO6TyHktQuvXwEQS3f" +
                "B2nQtUwoLlpt8YRjpWWUAcZa5r5n8ISZqaOqHCixTqZc2gCwx86Wbwd8JWBKHtIij+FyIjtghVUIipOV" +
                "yCE1b2p3fEJ4Xevq8XZP3yMTupWK2zeQEyZ9yjkfhioiyEz46Vpdb9PeGARQ2jl5yqSVOeeVuiE6t5IR" +
                "M4V4yOjbNNvnFaItvpSoQuG3xlH87lt1v1yNy9XnF6r2XmjPFhwfDF56NTP4oOxy92kvU+H53zAtV8OL" +
                "Na0stAXZPGADdGwwU+L4CNLGuzuICuUSoQVMKMsYYfI9SLZO+0BWA1bM3LQqm+zvJ7uXApi383O1Q0Fy" +
                "kfb4Vugu5J9GqWCUdfd1KFOw/S3+Yv0B4xdAgtgNjAwAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
