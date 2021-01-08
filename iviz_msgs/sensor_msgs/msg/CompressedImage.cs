/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [Preserve, DataContract (Name = "sensor_msgs/CompressedImage")]
    public sealed class CompressedImage : IDeserializable<CompressedImage>, IMessage
    {
        // This message contains a compressed image
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; } // Header timestamp should be acquisition time of image
        // Header frame_id should be optical frame of camera
        // origin of frame should be optical center of camera
        // +x should point to the right in the image
        // +y should point down in the image
        // +z should point into to plane of the image
        [DataMember (Name = "format")] public string Format { get; set; } // Specifies the format of the data
        //   Acceptable values:
        //     jpeg, png
        [DataMember (Name = "data")] public byte[] Data { get; set; } // Compressed image buffer
    
        /// <summary> Constructor for empty message. </summary>
        public CompressedImage()
        {
            Header = new StdMsgs.Header();
            Format = "";
            Data = System.Array.Empty<byte>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public CompressedImage(StdMsgs.Header Header, string Format, byte[] Data)
        {
            this.Header = Header;
            this.Format = Format;
            this.Data = Data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public CompressedImage(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Format = b.DeserializeString();
            Data = b.DeserializeStructArray<byte>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new CompressedImage(ref b);
        }
        
        CompressedImage IDeserializable<CompressedImage>.RosDeserialize(ref Buffer b)
        {
            return new CompressedImage(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Format);
            b.SerializeStructArray(Data, 0);
        }
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
            if (Format is null) throw new System.NullReferenceException(nameof(Format));
            if (Data is null) throw new System.NullReferenceException(nameof(Data));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 8;
                size += Header.RosMessageLength;
                size += BuiltIns.UTF8.GetByteCount(Format);
                size += 1 * Data.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "sensor_msgs/CompressedImage";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "8f7a12909da2c9d3332d540a0977563f";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACq2UTW8TMRCG7yvtfxiph7aUFgkuKBIHRAX0gITU3hCqJvZk18hru7Y37fLree1NSAJq" +
                "6QErK8f2zDPj+fAR3fQm0SApcSekvMtsXCLG3yFEbIsmM+Csbdrms7CWSP08bcYRbbazASbzECj1frSa" +
                "lkKs7kaTTDbe1XPyqy1uq38wfsNWkQe5NXqP5UM2iu18VEAKc+RHST6azrgiOGv8TVLiMmz9G3X2sNUO" +
                "3rhM2VPuhWCgzwQbZfH0tc6mQ4L29+6Zmj8PNfHBuqdg2dUw7CHaJuVoXEcrHwfOO8h1EGVWRlKV3pxu" +
                "dDXnx29O9F4pCZmXVmjNdpS0eEKY6EeQ7iUF17XNCFfffvteDexJffijsmg5rlYSi/fv/vNomy/XnxaU" +
                "sr4dUpdezcXVNghIZqc5apR+5uoggkI9Eirx3MpaLNVahpP1NE9B0kXRrA2DXycOJWPtRGO5CjKClhlG" +
                "h8rKsmuGLaCoIt9MgSOqb7QcoeCjNq7I1xKt/PIluRvFKaGry0VpyiRqzAZOTWCoKJxKkq8uaQ7ym9dF" +
                "A4o39/4ca+kO2jH3SDY8lodt3Lkk8YhezHe8AB5BEhjSiU7q3i2W6ZRgB15I8KqnE7j/dcp9aWWUzZqj" +
                "qVUBMprJAntclI5P99HF9QU5dn7Ln5E7I8/hFsoGXK513iN5toQgjR3iCMkQ/dpoyC6nSlHWoLnJmmXk" +
                "OLVNfXyqUUA+1vcgl0TW3GDmlLwyyISme5P7XSNt3qFSnb8Alc1/qi0FAAA=";
                
    }
}
