
namespace Iviz.Msgs.geometry_msgs
{
    public sealed class PoseArray : IMessage
    {
        // An array of poses with a header for global reference.
        
        public std_msgs.Header header;
        
        public Pose[] poses;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "geometry_msgs/PoseArray";
    
        public IMessage Create() => new PoseArray();
    
        public int GetLength()
        {
            int size = 4;
            size += header.GetLength();
            size += 56 * poses.Length;
            return size;
        }
    
        /// <summary> Constructor for empty message. </summary>
        public PoseArray()
        {
            header = new std_msgs.Header();
            poses = new Pose[0];
        }
        
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            header.Deserialize(ref ptr, end);
            BuiltIns.DeserializeStructArray(out poses, ref ptr, end, 0);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            header.Serialize(ref ptr, end);
            BuiltIns.SerializeStructArray(poses, ref ptr, end, 0);
        }
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "916c28c5764443f268b296bb671b9d97";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAACr1UTYvbMBC9C/IfBnLY3dKk0JYeAj0slH4cClt2b6WEiTWxBbLkHcnZdX99n+yN01xK" +
                "D+0GQyTrzZt5b0Ze0nUgVuWB4p66mCTRg8sNMTXCVpT2Uan2cceeVPaiEipZG/N5Op1Axtwg8vuPicAs" +
                "zPt//FuYr7efNpSy3bapTq+m7AuzpNvMwbJaaiWz5cxjwY2rG9GVl4N4RHHbiaXxNA+dpDUC7xqXCE8t" +
                "QZS9H6hPAOVIVWzbPriKs1B2rZzFI9LBMepYs6t6zwp8VOtCge+VW7izLLAk930xi7582AATklR9diho" +
                "AEOlwsmFGodkehfym9clwCzvHuIKW6lh7pyccsO5FCuPnUoqdXLaIMeLSdwa3HBHkMUmuhzfbbFNV4Qk" +
                "KEG6WDV0icpvhtzEAEKhA6vjnZdCXMEBsF6UoIur35hL2RsKHOKRfmI85fgb2jDzFk2rBj3zRX3qaxgI" +
                "YKfx4Cygu2EkqbyTkMm7nbIOpkRNKc3yY/EYIESNHcE/pxQrhwbYcXhNylrYx25snf1/A1lLxNzpME1l" +
                "uQRlJq9xU0qfoICzgy1PV6tMzl4FSjqu5GUZtPLaPp27EQtrKKo7xq4JdwsDMQPMtx5CNYy8J9zzaUQx" +
                "i+P9wURkdiGNPZslQA4uyFj1mWKz95Hzu7f0OK+GefXzuRSc/JtlzO3CKJ25el5/2d2f3MeHpsWX8M+i" +
                "jqsHyPsFey10Rm0FAAA=";
                
    }
}
