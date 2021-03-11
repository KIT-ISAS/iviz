/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = "geometry_msgs/PolygonStamped")]
    public sealed class PolygonStamped : IDeserializable<PolygonStamped>, IMessage
    {
        // This represents a Polygon with reference coordinate frame and timestamp
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "polygon")] public Polygon Polygon { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public PolygonStamped()
        {
            Polygon = new Polygon();
        }
        
        /// <summary> Explicit constructor. </summary>
        public PolygonStamped(in StdMsgs.Header Header, Polygon Polygon)
        {
            this.Header = Header;
            this.Polygon = Polygon;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public PolygonStamped(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Polygon = new Polygon(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new PolygonStamped(ref b);
        }
        
        PolygonStamped IDeserializable<PolygonStamped>.RosDeserialize(ref Buffer b)
        {
            return new PolygonStamped(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            Polygon.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (Polygon is null) throw new System.NullReferenceException(nameof(Polygon));
            Polygon.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 0;
                size += Header.RosMessageLength;
                size += Polygon.RosMessageLength;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "geometry_msgs/PolygonStamped";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "c6be8f7dc3bee7fe9e8d296070f53340";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UTW/UMBC9R9r/MNIe2iJaJLitxAGBgB6QKrU3hCqvPZtYOHbwONuGX88bJ9lW4sIB" +
                "iCLlw54382be85buOi+UecgsHIuQoZsUpjZFevClw8qBM0fLZFPKzkdTmA7Z9EwmOiq+ZymmH5rPbBxn" +
                "6uqjWTGG+dlsmrd/+do0X24/7UiKu++llVdz/k2zpduCykx21HMxzhRDh4TCfNtxvgx85EC1ZHZUV8s0" +
                "sFw1Sytwtxw5mxAmGgWbSgL3vh+jt0r+RHmNR6SP6NtgcvF2DCb/1itFxy38Y6y9vP6ww54obMfiUdAE" +
                "BJvZiI8tFqkZfSxvXmtAs717SJf45BbtPSWn0pmixfKjjk7rNLJDjhczuStgozuMLE7ovP67x6dcEJKg" +
                "BB6S7egcld9MpcOoSsd0NNmbfWAFtugAUM806OziGbKWvaNoYlrhZ8SnHH8CG0+4yumyw8yCspexRQOx" +
                "ccjp6B227qcKYoOHQCn4fTZ5ajRqTtlsP1Y9Fh1fnQieRiRZjwG4quNGSlb0Oo177/6dIFtO0F2eZlUu" +
                "PoAs35EMbP1BReTRl3RQyaxWgzSVAOTis5RqrWDwMiRMHmywCkZjP8txr26MkS3owWpVK1+/LZv/H7Wa" +
                "d7MaBxUV46NUGkMS/5wmtqpJDpkxtMFYPq+nCyS+9+CHXVCx9YKQCzXLddU2fsF47GbWMCPOJkWq7TrC" +
                "DkgjvgorSsEBoEBLXVekjluLW5BwLtSq8AeAUFifYFE1V04DPL/3wZephq6R8JuYtkrXsfg2zsUU851p" +
                "HChgeWakVUU4LsL4LaJDWogtIyyU4JKXmKN2oiragFFtUK35fUij09zNISSj9n88vU2nt5+b5he6T1q8" +
                "tQUAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
