
namespace Iviz.Msgs.nav_msgs
{
    public sealed class Path : IMessage 
    {
        //An array of poses that represents a Path for a robot to follow
        public std_msgs.Header header;
        public geometry_msgs.PoseStamped[] poses;

        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "nav_msgs/Path";

        public IMessage Create() => new Path();

        public int GetLength()
        {
            int size = 4;
            size += header.GetLength();
            for (int i = 0; i < poses.Length; i++)
            {
                size += poses[i].GetLength();
            }
            return size;
        }

        /// <summary> Constructor for empty message. </summary>
        public Path()
        {
            header = new std_msgs.Header();
            poses = new geometry_msgs.PoseStamped[0];
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            header.Deserialize(ref ptr, end);
            BuiltIns.DeserializeArray(out poses, ref ptr, end, 0);
        }

        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            header.Serialize(ref ptr, end);
            BuiltIns.SerializeArray(poses, ref ptr, end, 0);
        }

        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "6227e2b7e9cce15051f669a5e197bbf7";

        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
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
