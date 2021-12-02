/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
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
    
        /// Constructor for empty message.
        public MultiDOFJointState()
        {
            JointNames = System.Array.Empty<string>();
            Transforms = System.Array.Empty<GeometryMsgs.Transform>();
            Twist = System.Array.Empty<GeometryMsgs.Twist>();
            Wrench = System.Array.Empty<GeometryMsgs.Wrench>();
        }
        
        /// Explicit constructor.
        public MultiDOFJointState(in StdMsgs.Header Header, string[] JointNames, GeometryMsgs.Transform[] Transforms, GeometryMsgs.Twist[] Twist, GeometryMsgs.Wrench[] Wrench)
        {
            this.Header = Header;
            this.JointNames = JointNames;
            this.Transforms = Transforms;
            this.Twist = Twist;
            this.Wrench = Wrench;
        }
        
        /// Constructor with buffer.
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
        
        public ISerializable RosDeserialize(ref Buffer b) => new MultiDOFJointState(ref b);
        
        MultiDOFJointState IDeserializable<MultiDOFJointState>.RosDeserialize(ref Buffer b) => new MultiDOFJointState(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeArray(JointNames);
            b.SerializeStructArray(Transforms);
            b.SerializeStructArray(Twist);
            b.SerializeArray(Wrench);
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
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "sensor_msgs/MultiDOFJointState";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "690f272f0640d2631c305eeb8301e59d";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1WTW/cNhC961cQ8CF2sVaBuOjBQA8FXLcuUDRN3OZQFMasNCuxpkiFpFZRfn3ekJJ2" +
                "/VE0h9aGYetj5nHmzZsZnai33HsObCNF7axyOxVwyWrnvPrbaRuDGnVsVTeYqHvDqubGMwex3OGidt1G" +
                "FSewN8aN2jYqtgwMP1Rx8CxmPwvMO0EtixOY3kSlg6IQho5rmFNUlM9S2uIyTCFypyrnEVnvbB1UdHge" +
                "PdmAuLrs0zBio743GiiAJePm0++15Q75VKpqSdtSXSMZ/kgd4t8AqDdkaU5PnVKQY39/e3V9JnFdXP16" +
                "rU4/btSEXxrPBNlKmC5wflmRVVsGoDAXcDgQjqLLROKf10DJvgj34XugZosFrXJ2zz4CbUvVvST8IAbY" +
                "/0BVu9AU1GD1h4HNpHSN4umdkLDFHTix1DHsb8FEy1SzV6HnSkxAJB5G3bFCRGOrgShPMmoqPMrNKJtn" +
                "0F9zXarvjXliA3RQ5iyrDgxQg4NoD2AnmSyuckTSAsJJZ+bq37aIfnFD1kEHxAyZ0EFj5D1NYZNOECGy" +
                "pN6Tj2L3KJhSzbk2jowwgyg6uufsNNsjdwMZuV64J1Oq9y1bxWVTqskNq9JTFtYBcPRs4Q6RukrjlDp3" +
                "AbAgd7ikshlOWQN/MZe4FXd9nCQs4Ah7ORthLB7nHlo3mHpmbuEp6E+QKFIWgSWchTLJDFbOouYjTkGa" +
                "qwbWMI/IERWsQedmqiLAcgXLovgpayNLpCjQsujeP//K7nfiHoqGXcfRT3ddaMLXt4uEYbXK+YnRiIKK" +
                "gfx/9O59ogkvM19F8d1//FP88u7HS6RY5/Nyikga08fW5GuQH6mmSElWrW5a9ueG92yEl65HndPbOPXC" +
                "0YH6hi17MuB6kI4H+ZXrOhSgSqxD3Q/8c4NQ0qyuBoNpUzl0hbZptnqQK+hSDkYJbcXq5uoytQNXQ9QI" +
                "CL1sK88UZKbeXKliQFkuXotDcXI7unPccoMCrofnOYNgjyfTJc74KidXAhvkoD1lpJ6mZ3e4DWeQnYTA" +
                "vYOITxH5mwnzTvTKak9e0xZNCeAKDAD1lTi9OjtClrAvITnrFviMeDjjS2Dtiis5nbeomZHsw9CAQBj2" +
                "3u0x79KkS6rG7IfUjd568lORBls6sji5Fo5zw6WK5I1z3Myz4nM17nT9f6nxH3poEZdfFvA8nNclt+U4" +
                "MqZUHN0T8aRpIusXo50qaKn4A93t/EX2N3nH/DbAwVtZR97lBf8ySc7BPJMiqX169yh+tX4WpPnWMaGs" +
                "aLLVE461lgmGHEoZ955BEkaljqp24MM6GW5p8MMeq1o+GfBxgOF4zIk8hsupjP4NNiD4TVYihNS2qdHx" +
                "5eB1o+vHSz19hszJbVTcvYaQMOBTzPkwlBAgC9tnpbrZpXUxSkJp1eT5kjblElfqg+jcRobLDPGQ0Ddp" +
                "pC+bQ1t8IFGNqu+Mo/jtN+rjejWtV59epNQHjT1XbXwkeGnRTN+Dmsvdh4NAheR/TWi5Gl+oV9MKm9Na" +
                "hmqAgg3mSJwe5bP17h5yQqFEYgFTyTLGlnwAkm3SDpB1gLWy9Opscrif7V4mu7yMn6kaSpHLc0hug6ZC" +
                "8Gl2SoKy374sxQR2uMVf7Lui+AxH1VvNeQwAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
