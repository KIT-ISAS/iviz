/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.Msgs2.SensorMsgs
{
    [DataContract]
    public sealed class RegionOfInterest : IDeserializable<RegionOfInterest>, IMessageRos2
    {
        // This message is used to specify a region of interest within an image.
        //
        // When used to specify the ROI setting of the camera when the image was
        // taken, the height and width fields should either match the height and
        // width fields for the associated image; or height = width = 0
        // indicates that the full resolution image was captured.
        /// <summary> Leftmost pixel of the ROI </summary>
        [DataMember (Name = "x_offset")] public uint XOffset;
        // (0 if the ROI includes the left edge of the image)
        /// <summary> Topmost pixel of the ROI </summary>
        [DataMember (Name = "y_offset")] public uint YOffset;
        // (0 if the ROI includes the top edge of the image)
        /// <summary> Height of ROI </summary>
        [DataMember (Name = "height")] public uint Height;
        /// <summary> Width of ROI </summary>
        [DataMember (Name = "width")] public uint Width;
        // True if a distinct rectified ROI should be calculated from the "raw"
        // ROI in this message. Typically this should be False if the full image
        // is captured (ROI not used), and True if a subwindow is captured (ROI
        // used).
        [DataMember (Name = "do_rectify")] public bool DoRectify;
    
        /// Constructor for empty message.
        public RegionOfInterest()
        {
        }
        
        /// Constructor with buffer.
        public RegionOfInterest(ref ReadBuffer2 b)
        {
            b.Deserialize(out XOffset);
            b.Deserialize(out YOffset);
            b.Deserialize(out Height);
            b.Deserialize(out Width);
            b.Deserialize(out DoRectify);
        }
        
        public RegionOfInterest RosDeserialize(ref ReadBuffer2 b) => new RegionOfInterest(ref b);
    
        public void RosSerialize(ref WriteBuffer2 b)
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
        public const int RosFixedMessageLength = 17;
        
        public void GetRosMessageLength(ref int c)
        {
            WriteBuffer2.Advance(ref c, XOffset);
            WriteBuffer2.Advance(ref c, YOffset);
            WriteBuffer2.Advance(ref c, Height);
            WriteBuffer2.Advance(ref c, Width);
            WriteBuffer2.Advance(ref c, DoRectify);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "sensor_msgs/RegionOfInterest";
    
        public string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
