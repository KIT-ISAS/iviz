/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.NavMsgs
{
    [Preserve, DataContract (Name = "nav_msgs/Path")]
    public sealed class Path : IDeserializable<Path>, IMessage
    {
        //An array of poses that represents a Path for a robot to follow
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "poses")] public GeometryMsgs.PoseStamped[] Poses { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public Path()
        {
            Poses = System.Array.Empty<GeometryMsgs.PoseStamped>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Path(in StdMsgs.Header Header, GeometryMsgs.PoseStamped[] Poses)
        {
            this.Header = Header;
            this.Poses = Poses;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Path(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Poses = b.DeserializeArray<GeometryMsgs.PoseStamped>();
            for (int i = 0; i < Poses.Length; i++)
            {
                Poses[i] = new GeometryMsgs.PoseStamped(ref b);
            }
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Path(ref b);
        }
        
        Path IDeserializable<Path>.RosDeserialize(ref Buffer b)
        {
            return new Path(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeArray(Poses, 0);
        }
        
        public void RosValidate()
        {
            if (Poses is null) throw new System.NullReferenceException(nameof(Poses));
            for (int i = 0; i < Poses.Length; i++)
            {
                if (Poses[i] is null) throw new System.NullReferenceException($"{nameof(Poses)}[{i}]");
                Poses[i].RosValidate();
            }
        }
    
        public int RosMessageLength
        {
            get {
                int size = 4;
                size += Header.RosMessageLength;
                foreach (var i in Poses)
                {
                    size += i.RosMessageLength;
                }
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "nav_msgs/Path";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "6227e2b7e9cce15051f669a5e197bbf7";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAA71UTYvbMBC961cM5LC7pUmhLT0Eelgo/TgUUnZvpSwTe2wLbMk7kpN1f32f5I2zWSjt" +
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
