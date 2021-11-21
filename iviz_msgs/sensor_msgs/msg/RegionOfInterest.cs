/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [Preserve, DataContract (Name = "sensor_msgs/RegionOfInterest")]
    public sealed class RegionOfInterest : IDeserializable<RegionOfInterest>, IMessage
    {
        // This message is used to specify a region of interest within an image.
        //
        // When used to specify the ROI setting of the camera when the image was
        // taken, the height and width fields should either match the height and
        // width fields for the associated image; or height = width = 0
        // indicates that the full resolution image was captured.
        [DataMember (Name = "x_offset")] public uint XOffset; // Leftmost pixel of the ROI
        // (0 if the ROI includes the left edge of the image)
        [DataMember (Name = "y_offset")] public uint YOffset; // Topmost pixel of the ROI
        // (0 if the ROI includes the top edge of the image)
        [DataMember (Name = "height")] public uint Height; // Height of ROI
        [DataMember (Name = "width")] public uint Width; // Width of ROI
        // True if a distinct rectified ROI should be calculated from the "raw"
        // ROI in this message. Typically this should be False if the full image
        // is captured (ROI not used), and True if a subwindow is captured (ROI
        // used).
        [DataMember (Name = "do_rectify")] public bool DoRectify;
    
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
        internal RegionOfInterest(ref Buffer b)
        {
            XOffset = b.Deserialize<uint>();
            YOffset = b.Deserialize<uint>();
            Height = b.Deserialize<uint>();
            Width = b.Deserialize<uint>();
            DoRectify = b.Deserialize<bool>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new RegionOfInterest(ref b);
        }
        
        RegionOfInterest IDeserializable<RegionOfInterest>.RosDeserialize(ref Buffer b)
        {
            return new RegionOfInterest(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(XOffset);
            b.Serialize(YOffset);
            b.Serialize(Height);
            b.Serialize(Width);
            b.Serialize(DoRectify);
        }
        
        public void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        [Preserve] public const int RosFixedMessageLength = 17;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "sensor_msgs/RegionOfInterest";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "bdb633039d588fcccb441a4d43ccfe09";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACq1STWsbMRC9L+Q/PLKXBIIJzbHkWhoIFIohxyBLI0tUKy3SiI3/fUfajd0m0FN9krXz" +
                "PvTmjdg7XzBRKepIkGMtZMAJZSbt7QkKmY4+RSQLH5kyFcbi2fkIFeEnwe2GcRjx4ih+grMj/PzxhELM" +
                "Ph4bS7vSaqKssDRI+99psKgiPKx+Ubzr14780bHoGJE07GA9BVNQXKrBgMQGZUyKtfswLzx/IWzKfUKV" +
                "krRXLC675lfIhw32uEEecS9wH43XMlgEp7iDbQ1B4igpVG6RnF3Le2aumcxuGKqk9PAFb6/JWnk1MOKZ" +
                "LE9Jcpv9G4X3DCSWAR9/I27u4c8D4kKHaroJQhAekBHNjaIbuH2XPP0huU/z/1DkNP9DcMutk3xfzzLX" +
                "VLaBNc914KWft++S7z5X4bNSMOOLdEOzRKvZy8LMWpl1yYfWlqBr6EuzOU3dyXVWy7XQrJbl6lLjHfan" +
                "WXYXQuuf3F+YvqlQuup5nf1Fbd2XJeKmkcbEvc23d71/F7ulHhYpR1o+YYSmI3bDIaUAk17XF52Gq+E3" +
                "lrDiWGoDAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
