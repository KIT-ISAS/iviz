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
        public RegionOfInterest(ref Buffer b)
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
                "H4sIAAAAAAAACq1SwWrcMBC9L+w/PLKXBEII7bHkWlooFMJCjkErjdYismSkEc7+fUZjZ7dNoKcaH2R5" +
                "3nszb94O+yFUjFSrORLk2Co5cEadyAZ/gkGhY8gJ2SMkpkKVMQceQoJJCKPg7rabnbx4Gih9IuCB8Pj7" +
                "Jyoxh3TsPP3KmpGKwdwh/VuJMJvaidi8ULrV+4HCcWCRcqLqeIAPFF1FHXKLDiSdUMFo2A4f6jvRXxCf" +
                "i5aYWrMNhqVPVf0G+bHiHlbIA+47PiQXrFRWARpWtG8xiic1x8bdl3PjMtLErZATO7abJmZ9/YLX5+y9" +
                "jA7s8Is8j1nsm8IrxXcjxJvtBh+fHa7vEc4V0oiNzWkfhChEICeyK4f2cHMWPf0hus/Tf9HkPP1LcrVP" +
                "WX4sZylUnbVi8XWpeNLze0E3el+acHqJmwtVcmJZPLYcZHVuic+y70NPTrQt6vp8yaN2c1XMfNV5lr7l" +
                "7pLqO+xPk2wxxh5Gub9QfTexqux5sTqWbv6yT1x31pRZs31zq2G8NFzbYZac5PkTpvMoRBJxyDnC5edl" +
                "qlOf+g3OP7OhfQMAAA==";
                
    }
}
