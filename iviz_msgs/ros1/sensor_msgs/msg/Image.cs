/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [DataContract]
    public sealed class Image : IDeserializable<Image>, IHasSerializer<Image>, IMessage
    {
        // This message contains an uncompressed image
        // (0, 0) is at top-left corner of image
        //
        /// <summary> Header timestamp should be acquisition time of image </summary>
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // Header frame_id should be optical frame of camera
        // origin of frame should be optical center of camera
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
        // ros-users@lists.sourceforge.net and send an email proposing a new encoding.
        /// <summary> Encoding of pixels -- channel meaning, ordering, size </summary>
        [DataMember (Name = "encoding")] public string Encoding;
        // taken from the list of strings in include/sensor_msgs/image_encodings.h
        /// <summary> Is this data bigendian? </summary>
        [DataMember (Name = "is_bigendian")] public byte IsBigendian;
        /// <summary> Full row length in bytes </summary>
        [DataMember (Name = "step")] public uint Step;
        /// <summary> [Rent] actual matrix data, size is (step * rows) </summary>
        [DataMember (Name = "data")] public Tools.SharedRent Data;
    
        public Image()
        {
            Encoding = "";
            Data = Tools.SharedRent.Empty;
        }
        
        public Image(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Height);
            b.Deserialize(out Width);
            b.DeserializeString(out Encoding);
            b.Deserialize(out IsBigendian);
            b.Deserialize(out Step);
            b.DeserializeStructRent(out Data);
        }
        
        public Image(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Align4();
            b.Deserialize(out Height);
            b.Deserialize(out Width);
            b.DeserializeString(out Encoding);
            b.Deserialize(out IsBigendian);
            b.Align4();
            b.Deserialize(out Step);
            b.DeserializeStructRent(out Data);
        }
        
        public Image RosDeserialize(ref ReadBuffer b) => new Image(ref b);
        
        public Image RosDeserialize(ref ReadBuffer2 b) => new Image(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Height);
            b.Serialize(Width);
            b.Serialize(Encoding);
            b.Serialize(IsBigendian);
            b.Serialize(Step);
            b.SerializeStructArray(Data);
        }
        
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
    
        public int RosMessageLength
        {
            get {
                int size = 21;
                size += Header.RosMessageLength;
                size += WriteBuffer.GetStringSize(Encoding);
                size += Data.Length;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int d)
        {
            int c = d;
            c = Header.AddRos2MessageLength(c);
            c = WriteBuffer2.Align4(c);
            c += 4; // Height
            c += 4; // Width
            c = WriteBuffer2.AddLength(c, Encoding);
            c += 1; // IsBigendian
            c = WriteBuffer2.Align4(c);
            c += 4; // Step
            c += 4; // Data length
            c += 1 * Data.Length;
            return c;
        }
    
        public const string MessageType = "sensor_msgs/Image";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "060021388200f6f0f447d0fcd9c64743";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE61VTW8bNxC9768goEPsxFLS9hIYKFqgaVofChSNb4EhjJajXbZccs0hJW9+fR9JrWTB" +
                "dppDF/okOW8+3pvhQt32RtTAItSxar2LZJwociq51g9jwA5rZQZsNwt18e5KvbtUMKGooh+XlrcRZsFx" +
                "UH47n2ua35k0lvr6dXgW6rAcDTxGGkYlvU9Wqw0rau+TERONd2X/BKeee45Y20ADr41+BOXHaFqydSvj" +
                "tPgO9BKQD6YzLp+rBk+BWnaxJvh1pDcPs/HojcsVUrFnBfw+KrjIf76a05vpHED7vfs2wy/nhnj77H60" +
                "5EoJ/hPhpp45lrPnAFKcPl89IP1SynDjtv4luFlSJOJbQxEi2pvYn+LIYtta08aXEPLJDfe0Mz5kxSWn" +
                "eWsc66ZJSO+H7xFiKezJpALX5SsAQKRGrpRLw6bSF/xeZuu90YjniXVZfta49TYNThr0wS1is9xBGjuy" +
                "iUVtESOjY7RxnSJUDqRtjYWYQvu2AK/nbVm149iUgk8+qT1VoaAfnKagzRcUTTneYyVkNEAPhHT+BrEw" +
                "C16WSTjIz9ZIlJX4FFrGoY5XjmOhTBgf6GEeyFg1Bj96KYEV3DmQVdMcXBwjn0vx67yAtEfzwFbUcqna" +
                "npxjC27JYfMKnYMOLL8EYT9PZGaS/mGUI/ihkJrjzsDVueRSGdfapPktAhcf1oN08qRqfeX9PVhZb0yH" +
                "FA1SrMwJgPGhKZI67v00My2Rx7OAPiZrsxbAoesgAkSwmSJXabz/fFeBHhl8/gsT4A4zKiZwDjqCeSiH" +
                "auY5gIvi5XWR2GXT/Pg/P80fn367Ria6FqcOP8jh00E2ICVSCTtLsUcHcMB03oGuMmnRf2U3TiPLqkgY" +
                "QeOFYqGVrZ1UypMeSsTcH5LD4It8mtSzPSxRLVIjBczGZCngPHRgXD5e5kRGx0v4PoE9VjcfrnOzC7cp" +
                "GgQ0ZboDU5HkzQd1pInvS6m9fHfXLG73fol17s7ui0NbKn6YryaSazh7XbNcwQmqxHCnCyVYW+OvXIKn" +
                "HAuPvu3VBVL4c4q9r5N1R8HQxhYaMe4tUF9lo1eXj5BdgXbk/AxfEU8+vgXWHXFzTks0lLa5DJI6KlMO" +
                "zbozGkc3UwFprYH00DObQGFqys1YXDaLj+W2Ogk/38rn03bu73l8N82/vLSet/EHAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    
        public void Dispose()
        {
            Data.Dispose();
        }
    
        public Serializer<Image> CreateSerializer() => new Serializer();
        public Deserializer<Image> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<Image>
        {
            public override void RosSerialize(Image msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(Image msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(Image msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(Image msg) => msg.Ros2MessageLength;
        }
        sealed class Deserializer : Deserializer<Image>
        {
            public override void RosDeserialize(ref ReadBuffer b, out Image msg) => msg = new Image(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out Image msg) => msg = new Image(ref b);
        }
    }
}
