/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.TrajectoryMsgs
{
    [DataContract]
    public sealed class JointTrajectory : IDeserializable<JointTrajectory>, IMessage
    {
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "joint_names")] public string[] JointNames;
        [DataMember (Name = "points")] public JointTrajectoryPoint[] Points;
    
        public JointTrajectory()
        {
            JointNames = System.Array.Empty<string>();
            Points = System.Array.Empty<JointTrajectoryPoint>();
        }
        
        public JointTrajectory(in StdMsgs.Header Header, string[] JointNames, JointTrajectoryPoint[] Points)
        {
            this.Header = Header;
            this.JointNames = JointNames;
            this.Points = Points;
        }
        
        public JointTrajectory(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.DeserializeStringArray(out JointNames);
            b.DeserializeArray(out Points);
            for (int i = 0; i < Points.Length; i++)
            {
                Points[i] = new JointTrajectoryPoint(ref b);
            }
        }
        
        public JointTrajectory(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Align4();
            b.DeserializeStringArray(out JointNames);
            b.Align4();
            b.DeserializeArray(out Points);
            for (int i = 0; i < Points.Length; i++)
            {
                Points[i] = new JointTrajectoryPoint(ref b);
            }
        }
        
        public JointTrajectory RosDeserialize(ref ReadBuffer b) => new JointTrajectory(ref b);
        
        public JointTrajectory RosDeserialize(ref ReadBuffer2 b) => new JointTrajectory(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeArray(JointNames);
            b.Serialize(Points.Length);
            foreach (var t in Points)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.SerializeArray(JointNames);
            b.Serialize(Points.Length);
            foreach (var t in Points)
            {
                t.RosSerialize(ref b);
            }
        }
        
        public void RosValidate()
        {
            if (JointNames is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < JointNames.Length; i++)
            {
                if (JointNames[i] is null) BuiltIns.ThrowNullReference(nameof(JointNames), i);
            }
            if (Points is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Points.Length; i++)
            {
                if (Points[i] is null) BuiltIns.ThrowNullReference(nameof(Points), i);
                Points[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 8;
                size += Header.RosMessageLength;
                size += WriteBuffer.GetArraySize(JointNames);
                size += WriteBuffer.GetArraySize(Points);
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = Header.AddRos2MessageLength(c);
            c = WriteBuffer2.AddLength(c, JointNames);
            c = WriteBuffer2.Align4(c);
            c += 4; // Points.Length
            foreach (var t in Points)
            {
                c = t.AddRos2MessageLength(c);
            }
            return c;
        }
    
        public const string MessageType = "trajectory_msgs/JointTrajectory";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "65b4f94a94d1ed67169da35a02f33d3f";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE7VUS4vbMBC+61cM5LC7pUnpgx4CPRT62BYKSze3EMJEGsdaZMmV5Oz63/eTvE2ypYce" +
                "WmOQNY9vXt/4WthIpLYeKuVo/X69obtgfd567iSpr+V7FflOdA5xvClXmPTlTEq9+8eP+nb7eUkpm22X" +
                "9unF9ZTZjG4ze8PRUCeZDWemJiBxu28lzp0cxMGJu14MVW0ee0kLOK5amwjvXrxEdm6kIcEoB9Kh6wZv" +
                "NWehbFHruT88rSemnmO2enAcYR+isb6YNxG9Keh4k/wYxGuhLx+WsPFJ9JAtEhqBoKNwQlOhJDWgZa9f" +
                "FQea0fp7SC83ara6D3PIZY85HLOg3HIuWctDHyWVhDktEezZVOUCQdAlQTiT6LLKtrimK0I05CJ90C1d" +
                "ooSbMbfBA1DowNHyzkkB1mgFUC+K08XVGbKv0J59+AU/IZ5i/A2sP+KWmuYthudKG9KwRydh2MdwsAam" +
                "u7GCaGfFZ3J2FzmOqnhNIdXsU2k2jOBVR4OTUwraYhKG7m1uH5k7jWVrzf+iZT6uwcTOP+0GSv7I6P3J" +
                "dtoVSr1o21jBUJEypt2HZLMFYdbPCXRBQRlaXFhrcSBrVW7AEQpPraUB+fOmrkBp3lks8HqH8T8UEoop" +
                "FH3v3DG2wbDcgBQ4SuF3cU6lu2A2MuJUJXX9qa5/MfqtygVKV6pxgfPbN/VP8JjYmexUzpnwSVln8qka" +
                "ZYZJVRmzbWLotiAAFOon0pFogqYEAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
