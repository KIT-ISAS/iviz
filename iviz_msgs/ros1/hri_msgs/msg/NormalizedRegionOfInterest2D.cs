/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.HriMsgs
{
    [DataContract]
    public sealed class NormalizedRegionOfInterest2D : IDeserializable<NormalizedRegionOfInterest2D>, IMessage
    {
        // This contains the top-leftmost and bottom-rightmost coordinates of a region of interest (typically in an image)
        // the coordinates are always normalized and must belong to [0.,1.].
        // The (xmin, ymin) tuple stores the top-leftmost coordinates of the ROI, while (xmax, ymax) represents the
        // bottom-rightmost coordinates.
        // c is a confidence level (between 0. and 1.) associated to that ROI
        /// <summary> Header timestamp should be acquisition time of the original image </summary>
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "xmin")] public float Xmin;
        [DataMember (Name = "ymin")] public float Ymin;
        [DataMember (Name = "xmax")] public float Xmax;
        [DataMember (Name = "ymax")] public float Ymax;
        [DataMember (Name = "c")] public float C;
    
        public NormalizedRegionOfInterest2D()
        {
        }
        
        public NormalizedRegionOfInterest2D(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Xmin);
            b.Deserialize(out Ymin);
            b.Deserialize(out Xmax);
            b.Deserialize(out Ymax);
            b.Deserialize(out C);
        }
        
        public NormalizedRegionOfInterest2D(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Xmin);
            b.Deserialize(out Ymin);
            b.Deserialize(out Xmax);
            b.Deserialize(out Ymax);
            b.Deserialize(out C);
        }
        
        public NormalizedRegionOfInterest2D RosDeserialize(ref ReadBuffer b) => new NormalizedRegionOfInterest2D(ref b);
        
        public NormalizedRegionOfInterest2D RosDeserialize(ref ReadBuffer2 b) => new NormalizedRegionOfInterest2D(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Xmin);
            b.Serialize(Ymin);
            b.Serialize(Xmax);
            b.Serialize(Ymax);
            b.Serialize(C);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Xmin);
            b.Serialize(Ymin);
            b.Serialize(Xmax);
            b.Serialize(Ymax);
            b.Serialize(C);
        }
        
        public void RosValidate()
        {
        }
    
        public int RosMessageLength => 20 + Header.RosMessageLength;
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = Header.AddRos2MessageLength(c);
            c = WriteBuffer2.Align4(c);
            c += 4; // Xmin
            c += 4; // Ymin
            c += 4; // Xmax
            c += 4; // Ymax
            c += 4; // C
            return c;
        }
    
        public const string MessageType = "hri_msgs/NormalizedRegionOfInterest2D";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "33eb96af02d4b1dd1457132b5c2149c2";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE61TTW/bMAy961cQyKHJ0GTtdiuwW7Gth2FD21tRFIzM2AJkyZXoJt6v35O8ttkGDDvM" +
                "MJzog4/ke48Luu1cJhuDsguZtBPSOKy97LSPWYlDQ9uoGvt1cm03b9oYU+MCq2SKO2JK0roYyn8XVJLg" +
                "zlKnwVn2fsIeYMj13MrKLGqOYwROQuz3PGUKMfXs3XdpauJ+BNBWfAwtqqK7s83p+eZ+A4xbYCwPvQun" +
                "NOG7Ih0HL5Q1IvmfXfxWcDm//np1SvvO+QrEhwLEhxVaGQAhQSsMUv2t+1KKJRDIhcKdayRYIS9P4mm5" +
                "Fd2LBDrb1GbONyvinKN1iGxKP9qxljrMZ+FGEnXzz4J+rtX1YJL7gXIXRw8hQJR9HF12Wugu58/tRNSH" +
                "mvxMs9n5yPr+HRWKXhbT8aL0fHRytLDGfPjPj/ly8+kC6jQPfW7z27k/cHejYIYTlBblhpVpF8EDqJa0" +
                "nmmsBICvegpPzaRX1+JtJUiqJhvzTKqNfT8GOE/llcDneEQWM9LASZ0dPacjNWmXuJeCjjfL41jFvLq8" +
                "KNpmsaM6FFTsbJNwdjDl1SWZEZYHawiAcnfXMZ/fm8XtPq7LKLS/yFgFR9VyKB4rBXO+QLI3c5cbJAFL" +
                "gnRNpmXde8AyrwjZUIsM0Xa0RAvfJu2KA6D8EyfHW9i4zDGoAOpJCTpZHSGHCh04xGf4GfE1x7/Ahhfc" +
                "0tO6g3i+0JDHFkzi4pDiE4YATp3mMfcOk0TebROnyVTD1pRm8bGQjUuIqtKUIXqdjr3TzmRNBb3K8uAa" +
                "Y34ADwllva4EAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
