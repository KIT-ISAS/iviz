using System.Runtime.Serialization;

namespace Iviz.Msgs.sensor_msgs
{
    public sealed class RegionOfInterest : IMessage
    {
        // This message is used to specify a region of interest within an image.
        //
        // When used to specify the ROI setting of the camera when the image was
        // taken, the height and width fields should either match the height and
        // width fields for the associated image; or height = width = 0
        // indicates that the full resolution image was captured.
        
        public uint x_offset { get; set; } // Leftmost pixel of the ROI
        // (0 if the ROI includes the left edge of the image)
        public uint y_offset { get; set; } // Topmost pixel of the ROI
        // (0 if the ROI includes the top edge of the image)
        public uint height { get; set; } // Height of ROI
        public uint width { get; set; } // Width of ROI
        
        // True if a distinct rectified ROI should be calculated from the "raw"
        // ROI in this message. Typically this should be False if the full image
        // is captured (ROI not used), and True if a subwindow is captured (ROI
        // used).
        public bool do_rectify { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public RegionOfInterest()
        {
        }
        
        /// <summary> Explicit constructor. </summary>
        public RegionOfInterest(uint x_offset, uint y_offset, uint height, uint width, bool do_rectify)
        {
            this.x_offset = x_offset;
            this.y_offset = y_offset;
            this.height = height;
            this.width = width;
            this.do_rectify = do_rectify;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal RegionOfInterest(Buffer b)
        {
            this.x_offset = b.Deserialize<uint>();
            this.y_offset = b.Deserialize<uint>();
            this.height = b.Deserialize<uint>();
            this.width = b.Deserialize<uint>();
            this.do_rectify = b.Deserialize<bool>();
        }
        
        public IMessage Deserialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            return new RegionOfInterest(b);
        }
    
        public void Serialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this.x_offset);
            b.Serialize(this.y_offset);
            b.Serialize(this.height);
            b.Serialize(this.width);
            b.Serialize(this.do_rectify);
        }
        
        public void Validate()
        {
        }
    
        [IgnoreDataMember]
        public int RosMessageLength => 17;
    
        [IgnoreDataMember]
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve]
        public const string RosMessageType = "sensor_msgs/RegionOfInterest";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve]
        public const string RosMd5Sum = "bdb633039d588fcccb441a4d43ccfe09";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve]
        public const string RosDependenciesBase64 =
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
