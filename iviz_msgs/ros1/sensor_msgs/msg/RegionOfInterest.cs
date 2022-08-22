/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [DataContract]
    public sealed class RegionOfInterest : IDeserializable<RegionOfInterest>, IMessage
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
    
        public RegionOfInterest()
        {
        }
        
        public RegionOfInterest(ref ReadBuffer b)
        {
            b.Deserialize(out XOffset);
            b.Deserialize(out YOffset);
            b.Deserialize(out Height);
            b.Deserialize(out Width);
            b.Deserialize(out DoRectify);
        }
        
        public RegionOfInterest(ref ReadBuffer2 b)
        {
            b.Align4();
            b.Deserialize(out XOffset);
            b.Deserialize(out YOffset);
            b.Deserialize(out Height);
            b.Deserialize(out Width);
            b.Deserialize(out DoRectify);
        }
        
        public RegionOfInterest RosDeserialize(ref ReadBuffer b) => new RegionOfInterest(ref b);
        
        public RegionOfInterest RosDeserialize(ref ReadBuffer2 b) => new RegionOfInterest(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(XOffset);
            b.Serialize(YOffset);
            b.Serialize(Height);
            b.Serialize(Width);
            b.Serialize(DoRectify);
        }
        
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
    
        public const int RosFixedMessageLength = 17;
        
        public int RosMessageLength => RosFixedMessageLength;
        
        public const int Ros2FixedMessageLength = 17;
        
        public int Ros2MessageLength => Ros2FixedMessageLength;
        
        public int AddRos2MessageLength(int c) => WriteBuffer2.Align4(c) + Ros2FixedMessageLength;
        
    
        public const string MessageType = "sensor_msgs/RegionOfInterest";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "bdb633039d588fcccb441a4d43ccfe09";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE61STWvcMBC9+1c84ksCYQntseRaWigUykKOQSuNViKyZKQRjv99R7I32ybQU32SpXkf" +
                "82ZGHJ0vmKgUdSbIsRYy4IQyk/Z2hUKms08RycJHpkyFsXh2PkJF+Elwh2EcRjw5ih/g7Ai/fn5HIWYf" +
                "z42lXWk1UVZYGqT9dxosqggPqxeK9/3akT87Fh0jkoYdrKdgCopLNRiQ2KCMSbF27+qF5y+ETblXqFKS" +
                "9orFZdf8AnnYYY875BEPAvfReC2FRXCKO9jWECSOkkLlFsmba+ln5prJHIahSkqfP+H1OVkrXQMjfpDl" +
                "KUlus3+lcMlAYhnw/htx+wD/ViAudKimmyAE4QEZ0dwpuoG7i+T6h+Qxzf9DkdP8D8E9t07ybTtLXVPZ" +
                "C7Y8t4Knft7fJd9jrtR0FYwvshuaJVrNXgZmtpXZhnxq2xJ0DX1oNqepO7nJarkRms2yXF3X+IDjOsvs" +
                "Qli3+yvTVxUKXbrt4+wdtXFfh4jbRhoT922+u+/7d7Vb6mmR5UjLB4zQdMRhOKUUYNLz1tE6DL8BsmLy" +
                "G2kDAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
