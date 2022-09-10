/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.HriMsgs
{
    [DataContract]
    public sealed class NormalizedRegionOfInterest2D : IHasSerializer<NormalizedRegionOfInterest2D>, IMessage
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
            b.Align4();
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
    
        public int RosMessageLength
        {
            get
            {
                int size = 20;
                size += Header.RosMessageLength;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Header.AddRos2MessageLength(size);
            size = WriteBuffer2.Align4(size);
            size += 4; // Xmin
            size += 4; // Ymin
            size += 4; // Xmax
            size += 4; // Ymax
            size += 4; // C
            return size;
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
    
        public Serializer<NormalizedRegionOfInterest2D> CreateSerializer() => new Serializer();
        public Deserializer<NormalizedRegionOfInterest2D> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<NormalizedRegionOfInterest2D>
        {
            public override void RosSerialize(NormalizedRegionOfInterest2D msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(NormalizedRegionOfInterest2D msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(NormalizedRegionOfInterest2D msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(NormalizedRegionOfInterest2D msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(NormalizedRegionOfInterest2D msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<NormalizedRegionOfInterest2D>
        {
            public override void RosDeserialize(ref ReadBuffer b, out NormalizedRegionOfInterest2D msg) => msg = new NormalizedRegionOfInterest2D(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out NormalizedRegionOfInterest2D msg) => msg = new NormalizedRegionOfInterest2D(ref b);
        }
    }
}
