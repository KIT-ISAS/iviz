/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [DataContract (Name = "sensor_msgs/RegionOfInterest")]
    public sealed class RegionOfInterest : IMessage
    {
        // This message is used to specify a region of interest within an image.
        //
        // When used to specify the ROI setting of the camera when the image was
        // taken, the height and width fields should either match the height and
        // width fields for the associated image; or height = width = 0
        // indicates that the full resolution image was captured.
        [DataMember (Name = "x_offset")] public uint XOffset { get; set; } // Leftmost pixel of the ROI
        // (0 if the ROI includes the left edge of the image)
        [DataMember (Name = "y_offset")] public uint YOffset { get; set; } // Topmost pixel of the ROI
        // (0 if the ROI includes the top edge of the image)
        [DataMember (Name = "height")] public uint Height { get; set; } // Height of ROI
        [DataMember (Name = "width")] public uint Width { get; set; } // Width of ROI
        // True if a distinct rectified ROI should be calculated from the "raw"
        // ROI in this message. Typically this should be False if the full image
        // is captured (ROI not used), and True if a subwindow is captured (ROI
        // used).
        [DataMember (Name = "do_rectify")] public bool DoRectify { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public RegionOfInterest()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public RegionOfInterest(uint XOffset, uint YOffset, uint Height, uint Width, bool DoRectify)
        {
            this.XOffset = XOffset;
            this.YOffset = YOffset;
            this.Height = Height;
            this.Width = Width;
            this.DoRectify = DoRectify;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal RegionOfInterest(Buffer b)
        {
            XOffset = b.Deserialize<uint>();
            YOffset = b.Deserialize<uint>();
            Height = b.Deserialize<uint>();
            Width = b.Deserialize<uint>();
            DoRectify = b.Deserialize<bool>();
        }
        
        public ISerializable RosDeserialize(Buffer b)
        {
            return new RegionOfInterest(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
    
        public void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(XOffset);
            b.Serialize(YOffset);
            b.Serialize(Height);
            b.Serialize(Width);
            b.Serialize(DoRectify);
        }
        
        public void RosValidate()
        {
        }
    
        public int RosMessageLength => 17;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "sensor_msgs/RegionOfInterest";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "bdb633039d588fcccb441a4d43ccfe09";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE61STWvcMBC9+1c84ksCYQntseRaWigUykKOQSuNViKyZKQRjv99R7I32ybQU32SpXkf" +
                "82ZGHJ0vmKgUdSbIsRYy4IQyk/Z2hUKms08RycJHpkyFsXh2PkJF+Elwh2EcRjw5ih/g7Ai/fn5HIWYf" +
                "z42lXWk1UVZYGqT9dxosqggPqxeK9/3akT87Fh0jkoYdrKdgCopLNRiQ2KCMSbF27+qF5y+ETblXqFKS" +
                "9orFZdf8AnnYYY875BEPAvfReC2FRXCKO9jWECSOkkLlFsmba+ln5prJHIahSkqfP+H1OVkrXQMjfpDl" +
                "KUlus3+lcMlAYhnw/htx+wD/ViAudKimmyAE4QEZ0dwpuoG7i+T6h+Qxzf9DkdP8D8E9t07ybTtLXVPZ" +
                "C7Y8t4Knft7fJd9jrtR0FYwvshuaJVrNXgZmtpXZhnxq2xJ0DX1oNqepO7nJarkRms2yXF3X+IDjOsvs" +
                "Qli3+yvTVxUKXbrt4+wdtXFfh4jbRhoT922+u+/7d7Vb6mmR5UjLB4zQdMRhOKUUYNLz1tE6DL8BsmLy" +
                "G2kDAAA=";
                
    }
}
