/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [DataContract]
    public sealed class CompressedImage : IDeserializable<CompressedImage>, IHasSerializer<CompressedImage>, IMessage
    {
        // This message contains a compressed image
        /// <summary> Header timestamp should be acquisition time of image </summary>
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // Header frame_id should be optical frame of camera
        // origin of frame should be optical center of camera
        // +x should point to the right in the image
        // +y should point down in the image
        // +z should point into to plane of the image
        /// <summary> Specifies the format of the data </summary>
        [DataMember (Name = "format")] public string Format;
        //   Acceptable values:
        //     jpeg, png
        /// <summary> Compressed image buffer </summary>
        [DataMember (Name = "data")] public byte[] Data;
    
        public CompressedImage()
        {
            Format = "";
            Data = EmptyArray<byte>.Value;
        }
        
        public CompressedImage(in StdMsgs.Header Header, string Format, byte[] Data)
        {
            this.Header = Header;
            this.Format = Format;
            this.Data = Data;
        }
        
        public CompressedImage(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.DeserializeString(out Format);
            unsafe
            {
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<byte>.Value
                    : new byte[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), n * 1);
                }
                Data = array;
            }
        }
        
        public CompressedImage(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Align4();
            b.DeserializeString(out Format);
            unsafe
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                var array = n == 0
                    ? EmptyArray<byte>.Value
                    : new byte[n];
                if (n != 0)
                {
                    b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), n * 1);
                }
                Data = array;
            }
        }
        
        public CompressedImage RosDeserialize(ref ReadBuffer b) => new CompressedImage(ref b);
        
        public CompressedImage RosDeserialize(ref ReadBuffer2 b) => new CompressedImage(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Format);
            b.SerializeStructArray(Data);
        }
        
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
    
        public int RosMessageLength
        {
            get
            {
                int size = 8;
                size += Header.RosMessageLength;
                size += WriteBuffer.GetStringSize(Format);
                size += Data.Length;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Header.AddRos2MessageLength(size);
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Format);
            size = WriteBuffer2.Align4(size);
            size += 4; // Data.Length
            size += 1 * Data.Length;
            return size;
        }
    
        public const string MessageType = "sensor_msgs/CompressedImage";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "8f7a12909da2c9d3332d540a0977563f";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE62Uz08UMRTH7/0rXrIHQASjXgyJByNROZgY4WYIedu+nanptKXtLIx/vd92dllQUQ5O" +
                "ZtOd9r3P+90FXfQ20yA5cyekgy9sfSbG3yEmbIshO+BMqU/CRhL187J5FrTZLhaQwkOk3IfRGVoKsb4e" +
                "bbbFBt/OKaw2MPrTc8daJR7kypp7qBCL1ezmo8rRWBM/BgrJdtZXuVnhd5AWX2Dqn6TD261yDNYXKoFK" +
                "LwR+Xwgm6sdfYzqcHgJMuPFPU/zxUBG/UM1Hx76lYEdQuSTrO1qFNHDZIc6jaLuykpvs5nSjabg8GjTR" +
                "O60lFl46oTW7UfLJ47JE36N0zyn6To3w8s23y0a/J/T+l26i5bhaSVLq7X9+1OfzjyeUi7kacpdfzA2l" +
                "kInC3nAyaPXCzTlkg3pUUdKRk7U4at0LB9tpmaLkYyi2+cDbiUeXODfRWKNAHTAhw+jRTEV23b/VhyZq" +
                "zBQ5od9GxwnyIRnrq3hrykrHm+V6FK+Fzk5P6gBm0WOxcGgCQSfhXCt7dkott69fVQVk9NvXkF9eqsXF" +
                "TTjCvnQPZrD0KDS8lttt2hkVXNCzOcpjGEGWBOZMpv22d4XPfECwBl8kBt3TPkL4MpU+zN265mRbRwCM" +
                "EXKg7lWlvYN7ZN/Qnn3Y4mfizsZTsP6OW2M66lE8V9OQxw6ZhGBMYW0NRJdTg2hnMdDk7DJxmlS7bZpJ" +
                "tfjQboBS69hKg5VzDtqiEoZubOnvxmdz7yj1E6YtGYIYBQAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<CompressedImage> CreateSerializer() => new Serializer();
        public Deserializer<CompressedImage> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<CompressedImage>
        {
            public override void RosSerialize(CompressedImage msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(CompressedImage msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(CompressedImage msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(CompressedImage msg) => msg.Ros2MessageLength;
            public override void RosValidate(CompressedImage msg) => msg.RosValidate();
        }
        sealed class Deserializer : Deserializer<CompressedImage>
        {
            public override void RosDeserialize(ref ReadBuffer b, out CompressedImage msg) => msg = new CompressedImage(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out CompressedImage msg) => msg = new CompressedImage(ref b);
        }
    }
}
