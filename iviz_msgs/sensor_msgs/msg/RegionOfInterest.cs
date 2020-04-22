
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
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "sensor_msgs/RegionOfInterest";
    
        public IMessage Create() => new RegionOfInterest();
    
        public int GetLength() => 17;
    
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
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "bdb633039d588fcccb441a4d43ccfe09";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public const string DependenciesBase64 =
                "H4sIAAAAAAAACq1STWsbMRC9L+Q/PLKXBIIJzbHkWhoIFIohxyBLI0tUKy3SiI3/fUfajd0m0FN9krXz" +
                "PvTmjdg7XzBRKepIkGMtZMAJZSbt7QkKmY4+RSQLH5kyFcbi2fkIFeEnwe2GcRjx4ih+grMj/PzxhELM" +
                "Ph4bS7vSaqKssDRI+99psKgiPKx+Ubzr14780bHoGJE07GA9BVNQXKrBgMQGZUyKtfswLzx/IWzKfUKV" +
                "krRXLC675lfIhw32uEEecS9wH43XMlgEp7iDbQ1B4igpVG6RnF3Le2aumcxuGKqk9PAFb6/JWnk1MOKZ" +
                "LE9Jcpv9G4X3DCSWAR9/I27u4c8D4kKHaroJQhAekBHNjaIbuH2XPP0huU/z/1DkNP9DcMutk3xfzzLX" +
                "VLaBNc914KWft++S7z5X4bNSMOOLdEOzRKvZy8LMWpl1yYfWlqBr6EuzOU3dyXVWy7XQrJbl6lLjHfan" +
                "WXYXQuuf3F+YvqlQuup5nf1Fbd2XJeKmkcbEvc23d71/F7ulHhYpR1o+YYSmI3bDIaUAk17XF52Gq+E3" +
                "lrDiWGoDAAA=";
                
    }
}
