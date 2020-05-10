using System.Runtime.Serialization;

namespace Iviz.Msgs.nav_msgs
{
    public sealed class Path : IMessage
    {
        //An array of poses that represents a Path for a robot to follow
        public std_msgs.Header header { get; set; }
        public geometry_msgs.PoseStamped[] poses { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Path()
        {
            header = new std_msgs.Header();
            poses = System.Array.Empty<geometry_msgs.PoseStamped>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Path(std_msgs.Header header, geometry_msgs.PoseStamped[] poses)
        {
            this.header = header ?? throw new System.ArgumentNullException(nameof(header));
            this.poses = poses ?? throw new System.ArgumentNullException(nameof(poses));
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Path(Buffer b)
        {
            this.header = new std_msgs.Header(b);
            this.poses = b.DeserializeArray<geometry_msgs.PoseStamped>(0);
        }
        
        public IMessage Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new Path(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            this.header.Serialize(b);
            b.SerializeArray(this.poses, 0);
        }
        
        public void Validate()
        {
            if (header is null) throw new System.NullReferenceException();
            if (poses is null) throw new System.NullReferenceException();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += header.RosMessageLength;
                for (int i = 0; i < poses.Length; i++)
                {
                    size += poses[i].RosMessageLength;
                }
                return size;
            }
        }
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve]
        public const string RosMessageType = "nav_msgs/Path";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve]
        public const string RosMd5Sum = "6227e2b7e9cce15051f669a5e197bbf7";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve]
        public const string RosDependenciesBase64 =
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
                
    }
}
