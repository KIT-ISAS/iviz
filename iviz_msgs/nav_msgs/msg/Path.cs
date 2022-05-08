/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.NavMsgs
{
    [DataContract]
    public sealed class Path : IDeserializable<Path>, IMessage
    {
        //An array of poses that represents a Path for a robot to follow
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "poses")] public GeometryMsgs.PoseStamped[] Poses;
    
        /// Constructor for empty message.
        public Path()
        {
            Poses = System.Array.Empty<GeometryMsgs.PoseStamped>();
        }
        
        /// Explicit constructor.
        public Path(in StdMsgs.Header Header, GeometryMsgs.PoseStamped[] Poses)
        {
            this.Header = Header;
            this.Poses = Poses;
        }
        
        /// Constructor with buffer.
        public Path(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.DeserializeArray(out Poses);
            for (int i = 0; i < Poses.Length; i++)
            {
                Poses[i] = new GeometryMsgs.PoseStamped(ref b);
            }
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new Path(ref b);
        
        public Path RosDeserialize(ref ReadBuffer b) => new Path(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeArray(Poses);
        }
        
        public void RosValidate()
        {
            if (Poses is null) BuiltIns.ThrowNullReference();
            for (int i = 0; i < Poses.Length; i++)
            {
                if (Poses[i] is null) BuiltIns.ThrowNullReference(nameof(Poses), i);
                Poses[i].RosValidate();
            }
        }
    
        public int RosMessageLength => 4 + Header.RosMessageLength + BuiltIns.GetArraySize(Poses);
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "nav_msgs/Path";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "6227e2b7e9cce15051f669a5e197bbf7";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71UTYvbMBC961cM5LC7pUmhLT0Eelgo/TgUUnZvpSwTe2wLbMk7kpN1f32f5I2zWSjt" +
                "oY0xWLJm3rw3H1pcO2JVHslX1PsggWLDkVR6lSAuBmLacGyo8oql+q2PFD22bev35rNwKUpN/phafCdR" +
                "x7su1OHVBnA3kbteyu8/JnBj3v/jx3y9+bSmEMsp5sTHLAiBXclaEghxyZGzgMbWjeiylZ20cMrcKJ/G" +
                "sZewguNtYwPhrcWJctuONAQYQXLhu25wtuAoFG0nJ/7wtEgl9azRFkPLCnuvpXXJvFLuJKHjDXI/iCuE" +
                "vnxYw8YFKYZoQWgEQqHCwboah2QG6+Kb18nBLG73fomt1Mj2HHyqFcjKQypX4slhjRgvJnErYCM5gihl" +
                "oMv87w7bcEUIAgrS+6KhSzDfjLHxDoBCO1bL21YScIEMAPUiOV1cPUF2Gdqx8wf4CfEY429g3YybNC0b" +
                "1KxN6sNQI4Ew7NXvbAnT7ZhBitaiKam1W2UdTfKaQprFx5RjGMErVwRfDsEXFgUoaW9jY0LUhJ6rcWfL" +
                "/9WNvx0D6LymtM90MGSVaG6F561CSMSxzM+mLAOkcTof/0x8vhM4WtT08cJIbV+poAw9F/IyTUn6XT6e" +
                "22yb5Hi1B98VQQS6eTYw3wZoV5dxj3bnEggqh8lHL0e2LuRum/lDC0Y7Uz6Ra6rWc3z3lh7m1Tivfp6H" +
                "/jF1Bw1PL++TfJ6ST7v7Y95xP3Yr8wdFh9XemF9wmt//OwYAAA==";
                
    
        public override string ToString() => Extensions.ToString(this);
    }
}
