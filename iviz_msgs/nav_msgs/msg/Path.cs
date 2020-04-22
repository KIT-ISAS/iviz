
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
            poses = System.Array.Empty<geometry_msgs.PoseStamped>();
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
                "H4sIAAAAAAAACr1UTYvbMBC9C/IfBnLY3dKk0JYeAj0slH4cClt2b6WEiT2xBbbkHcnJur++T/LG2SyU" +
                "9tDGGCzJM2/emw/Nrx2xKg/kt9T5IIFizZFUOpUgLgZiuuFY09Yrluo3PlL02DaN35vPwqUo1fljKvGt" +
                "RB3WbajCqxvA3UZuOym//xjBzcy8/8fPzHy9/bSiEMsx6shoZuaE2K5kLQmcuOTIWUNtq1p00chOGnhl" +
                "epT/xqGTsITjXW0D4a3EiXLTDNQHGEF14du2d7bgKBRtKyf+8LTIJnWs0RZ9wwp7r6V1yXyr3EpCxxvk" +
                "vhdXCH35sIKNC1L00YLQAIRChYN1FX6S6a2Lb14nBzO/2/sFtlIh4VPwsVwgKw+pYoknhxVivBjFLYGN" +
                "7AiilIEu89ka23BFCAIK0vmipkswvxli7R0AhXasljeNJOACGQDqRXK6uHqCnGivyLHzB/gR8Rjjb2Dd" +
                "hJs0LWrUrEnqQ18hgTDs1O9sCdPNkEGKxqIvqbEbZR1M8hpDmvnHlGMYwStXBF8OwRcWBShpb2NtQtSE" +
                "nquxtuX/a8jfzkJqzWtKB5kRRm0rmrvhebcQcnGs9LNZywBpqM4pYeQ+XQ4cLSr7eHOk5t+qoBgdF/Iy" +
                "zUo6Lh//22ybFHm1B98lQQd6ejIw33rIV5dxj3bn0wgys8MVgKaObF26EuUoAXIw45n1iWKzbTzHd2/p" +
                "YVoN0+rnuRQc8zfJeHqXn2T1lH/a3R+zj7uyXZo/iDqs9pD3CwKs5QNLBgAA";
                
    }
}
