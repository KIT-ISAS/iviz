/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = "geometry_msgs/PoseArray")]
    public sealed class PoseArray : IDeserializable<PoseArray>, IMessage
    {
        // An array of poses with a header for global reference.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "poses")] public Pose[] Poses;
    
        /// <summary> Constructor for empty message. </summary>
        public PoseArray()
        {
            Poses = System.Array.Empty<Pose>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public PoseArray(in StdMsgs.Header Header, Pose[] Poses)
        {
            this.Header = Header;
            this.Poses = Poses;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal PoseArray(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Poses = b.DeserializeStructArray<Pose>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new PoseArray(ref b);
        }
        
        PoseArray IDeserializable<PoseArray>.RosDeserialize(ref Buffer b)
        {
            return new PoseArray(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.SerializeStructArray(Poses, 0);
        }
        
        public void RosValidate()
        {
            if (Poses is null) throw new System.NullReferenceException(nameof(Poses));
        }
    
        public int RosMessageLength => 4 + Header.RosMessageLength + 56 * Poses.Length;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "geometry_msgs/PoseArray";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "916c28c5764443f268b296bb671b9d97";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
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
                
        public override string ToString() => Extensions.ToString(this);
    }
}
