/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.Msgs2.SensorMsgs
{
    [DataContract]
    public sealed class Image : IDeserializableRos2<Image>, IMessageRos2
    {
        // This message contains an uncompressed image
        // (0, 0) is at top-left corner of image
        /// <summary> Header timestamp should be acquisition time of image </summary>
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // Header frame_id should be optical frame of camera
        // origin of frame should be optical center of cameara
        // +x should point to the right in the image
        // +y should point down in the image
        // +z should point into to plane of the image
        // If the frame_id here and the frame_id of the CameraInfo
        // message associated with the image conflict
        // the behavior is undefined
        /// <summary> Image height, that is, number of rows </summary>
        [DataMember (Name = "height")] public uint Height;
        /// <summary> Image width, that is, number of columns </summary>
        [DataMember (Name = "width")] public uint Width;
        // The legal values for encoding are in file src/image_encodings.cpp
        // If you want to standardize a new string format, join
        // ros-users@lists.ros.org and send an email proposing a new encoding.
        /// <summary> Encoding of pixels -- channel meaning, ordering, size </summary>
        [DataMember (Name = "encoding")] public string Encoding;
        // taken from the list of strings in include/sensor_msgs/image_encodings.hpp
        /// <summary> Is this data bigendian? </summary>
        [DataMember (Name = "is_bigendian")] public byte IsBigendian;
        /// <summary> Full row length in bytes </summary>
        [DataMember (Name = "step")] public uint Step;
        /// <summary> Actual matrix data, size is (step * rows) </summary>
        [DataMember (Name = "data")] public byte[] Data;
    
        /// Constructor for empty message.
        public Image()
        {
            Encoding = "";
            Data = System.Array.Empty<byte>();
        }
        
        /// Constructor with buffer.
        public Image(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Height);
            b.Deserialize(out Width);
            b.DeserializeString(out Encoding);
            b.Deserialize(out IsBigendian);
            b.Deserialize(out Step);
            b.DeserializeStructArray(out Data);
        }
        
        public Image RosDeserialize(ref ReadBuffer2 b) => new Image(ref b);
    
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Height);
            b.Serialize(Width);
            b.Serialize(Encoding);
            b.Serialize(IsBigendian);
            b.Serialize(Step);
            b.SerializeStructArray(Data);
        }
        
        public void RosValidate()
        {
            if (Encoding is null) BuiltIns.ThrowNullReference();
            if (Data is null) BuiltIns.ThrowNullReference();
        }
    
        public int RosMessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRosMessageLength(ref int c)
        {
            Header.AddRosMessageLength(ref c);
            WriteBuffer2.AddLength(ref c, Height);
            WriteBuffer2.AddLength(ref c, Width);
            WriteBuffer2.AddLength(ref c, Encoding);
            WriteBuffer2.AddLength(ref c, IsBigendian);
            WriteBuffer2.AddLength(ref c, Step);
            WriteBuffer2.AddLength(ref c, Data);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "sensor_msgs/Image";
    
        public string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
