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
        
        public uint x_offset; // Leftmost pixel of the ROI
        // (0 if the ROI includes the left edge of the image)
        public uint y_offset; // Topmost pixel of the ROI
        // (0 if the ROI includes the top edge of the image)
        public uint height; // Height of ROI
        public uint width; // Width of ROI
        
        // True if a distinct rectified ROI should be calculated from the "raw"
        // ROI in this message. Typically this should be False if the full image
        // is captured (ROI not used), and True if a subwindow is captured (ROI
        // used).
        public bool do_rectify;
    
        public unsafe void Deserialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Deserialize(out x_offset, ref ptr, end);
            BuiltIns.Deserialize(out y_offset, ref ptr, end);
            BuiltIns.Deserialize(out height, ref ptr, end);
            BuiltIns.Deserialize(out width, ref ptr, end);
            BuiltIns.Deserialize(out do_rectify, ref ptr, end);
        }
    
        public unsafe void Serialize(ref byte* ptr, byte* end)
        {
            BuiltIns.Serialize(x_offset, ref ptr, end);
            BuiltIns.Serialize(y_offset, ref ptr, end);
            BuiltIns.Serialize(height, ref ptr, end);
            BuiltIns.Serialize(width, ref ptr, end);
            BuiltIns.Serialize(do_rectify, ref ptr, end);
        }
    
        [IgnoreDataMember]
        public int RosMessageLength => 17;
    
        public IMessage Create() => new RegionOfInterest();
    
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
