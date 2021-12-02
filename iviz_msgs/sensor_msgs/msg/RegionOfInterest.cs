/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
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
    
        /// Constructor for empty message.
        public RegionOfInterest()
        {
        }
        
        /// Explicit constructor.
        public RegionOfInterest(uint XOffset, uint YOffset, uint Height, uint Width, bool DoRectify)
        {
            this.XOffset = XOffset;
            this.YOffset = YOffset;
            this.Height = Height;
            this.Width = Width;
            this.DoRectify = DoRectify;
        }
        
        /// Constructor with buffer.
        internal RegionOfInterest(ref Buffer b)
        {
            XOffset = b.Deserialize<uint>();
            YOffset = b.Deserialize<uint>();
            Height = b.Deserialize<uint>();
            Width = b.Deserialize<uint>();
            DoRectify = b.Deserialize<bool>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new RegionOfInterest(ref b);
        
        RegionOfInterest IDeserializable<RegionOfInterest>.RosDeserialize(ref Buffer b) => new RegionOfInterest(ref b);
    
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
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 17;
        
        public int RosMessageLength => RosFixedMessageLength;
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "sensor_msgs/RegionOfInterest";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "bdb633039d588fcccb441a4d43ccfe09";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACq1STWsbMRC97694ZC8JBBPaY8m1NBAoFEOOQZZGlohWWqQRG//7jrQbu00gp/gka+d9" +
                "6M0bsXe+YKJS1JEgx1rIgBPKTNrbExQyHX2KSBY+MmUqjMWz8xEqwk+C2w3jMOLJUfwAZ0f48/sBhZh9" +
                "PDaWdqXVRFlhaZD2v9NgUUV4WL1QvO3XjvzRsegYkTTsYD0FU1BcqsGAxAZlTIq1ezcvPP8hbMp9QpWS" +
                "tFcsLrvmD8iHDXa/Qe5xJ3AfjdcyWASnuINtDUHiKClUbpGcXct7Zq6ZzG4YqqT0/Rten5O18mpgxCNZ" +
                "npLkNvtXCm8ZSCwD3v9GXN/BnwfEhQ7VdBOEIDwgI5obRTdw8yZ5+kdyn+avUOQ0fyK45dZJfq1nmWsq" +
                "28Ca5zrw1M/bd8l3n6vwWSmY8UW6oVmi1exlYWatzLrkQ2tL0DX0pdmcpu7kKqvlSmhWy3J1qfEO+9Ms" +
                "uwuh9U/uL0w/VShd9bzO/qK27ssScd1IY+Le5pvb3r+L3VIPi5QjLR8wQtMRu+GQUoBJz+uLTsPwF7Ji" +
                "8htpAwAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
