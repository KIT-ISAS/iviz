
namespace Iviz.Msgs.sensor_msgs
{
    public sealed class MultiDOFJointState : IMessage
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
        
        public std_msgs.Header header;
        
        public string[] joint_names;
        public geometry_msgs.Transform[] transforms;
        public geometry_msgs.Twist[] twist;
        public geometry_msgs.Wrench[] wrench;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "sensor_msgs/MultiDOFJointState";
    
        public IMessage Create() => new MultiDOFJointState();
    
        public int GetLength()
        {
            int size = 20;
            size += header.GetLength();
            for (int i = 0; i < joint_names.Length; i++)
            {
                size += joint_names[i].Length;
            }
            size += 56 * transforms.Length;
            size += 48 * twist.Length;
            size += 48 * wrench.Length;
            return size;
        }
    
        /// <summary> Constructor for empty message. </summary>
        public MultiDOFJointState()
        {
            header = new std_msgs.Header();
            joint_names = System.Array.Empty<0>();
            transforms = System.Array.Empty<geometry_msgs.Transform>();
            twist = System.Array.Empty<geometry_msgs.Twist>();
            wrench = System.Array.Empty<geometry_msgs.Wrench>();
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            header.Deserialize(ref ptr, end);
            BuiltIns.Deserialize(out joint_names, ref ptr, end, 0);
            BuiltIns.DeserializeStructArray(out transforms, ref ptr, end, 0);
            BuiltIns.DeserializeArray(out twist, ref ptr, end, 0);
            BuiltIns.DeserializeArray(out wrench, ref ptr, end, 0);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            header.Serialize(ref ptr, end);
            BuiltIns.Serialize(joint_names, ref ptr, end, 0);
            BuiltIns.SerializeStructArray(transforms, ref ptr, end, 0);
            BuiltIns.SerializeArray(twist, ref ptr, end, 0);
            BuiltIns.SerializeArray(wrench, ref ptr, end, 0);
        }
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "690f272f0640d2631c305eeb8301e59d";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
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
                
    }
}
