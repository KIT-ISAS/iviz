/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.Msgs2.SensorMsgs
{
    [DataContract]
    public sealed class CompressedImage : IDeserializable<CompressedImage>, IMessageRos2
    {
        // This message contains a compressed image.
        /// <summary> Header timestamp should be acquisition time of image </summary>
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // Header frame_id should be optical frame of camera
        // origin of frame should be optical center of cameara
        // +x should point to the right in the image
        // +y should point down in the image
        // +z should point into to plane of the image
        /// <summary> Specifies the format of the data </summary>
        [DataMember (Name = "format")] public string Format;
        //   Acceptable values:
        //     jpeg, png
        /// <summary> Compressed image buffer </summary>
        [DataMember (Name = "data")] public byte[] Data;
    
        /// Constructor for empty message.
        public CompressedImage()
        {
            Format = "";
            Data = System.Array.Empty<byte>();
        }
        
        /// Explicit constructor.
        public CompressedImage(in StdMsgs.Header Header, string Format, byte[] Data)
        {
            this.Header = Header;
            this.Format = Format;
            this.Data = Data;
        }
        
        /// Constructor with buffer.
        public CompressedImage(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.DeserializeString(out Format);
            b.DeserializeStructArray(out Data);
        }
        
        public CompressedImage RosDeserialize(ref ReadBuffer2 b) => new CompressedImage(ref b);
    
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Format);
            b.SerializeStructArray(Data);
        }
        
        public void RosValidate()
        {
            if (Format is null) BuiltIns.ThrowNullReference();
            if (Data is null) BuiltIns.ThrowNullReference();
        }
    
        public void GetRosMessageLength(ref int c)
        {
            Header.GetRosMessageLength(ref c);
            WriteBuffer2.Advance(ref c, Format);
            WriteBuffer2.Advance(ref c, Data);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "sensor_msgs/CompressedImage";
    
        public string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
