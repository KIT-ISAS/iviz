/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [Preserve, DataContract (Name = "sensor_msgs/Image")]
    public sealed class Image : IDeserializable<Image>, IMessage
    {
        // This message contains an uncompressed image
        // (0, 0) is at top-left corner of image
        //
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; } // Header timestamp should be acquisition time of image
        // Header frame_id should be optical frame of camera
        // origin of frame should be optical center of camera
        // +x should point to the right in the image
        // +y should point down in the image
        // +z should point into to plane of the image
        // If the frame_id here and the frame_id of the CameraInfo
        // message associated with the image conflict
        // the behavior is undefined
        [DataMember (Name = "height")] public uint Height { get; set; } // image height, that is, number of rows
        [DataMember (Name = "width")] public uint Width { get; set; } // image width, that is, number of columns
        // The legal values for encoding are in file src/image_encodings.cpp
        // If you want to standardize a new string format, join
        // ros-users@lists.sourceforge.net and send an email proposing a new encoding.
        [DataMember (Name = "encoding")] public string Encoding { get; set; } // Encoding of pixels -- channel meaning, ordering, size
        // taken from the list of strings in include/sensor_msgs/image_encodings.h
        [DataMember (Name = "is_bigendian")] public byte IsBigendian { get; set; } // is this data bigendian?
        [DataMember (Name = "step")] public uint Step { get; set; } // Full row length in bytes
        [DataMember (Name = "data")] public byte[] Data { get; set; } // actual matrix data, size is (step * rows)
    
        /// <summary> Constructor for empty message. </summary>
        public Image()
        {
            Header = new StdMsgs.Header();
            Encoding = "";
            Data = System.Array.Empty<byte>();
        }
        
        /// <summary> Explicit constructor. </summary>
        public Image(StdMsgs.Header Header, uint Height, uint Width, string Encoding, byte IsBigendian, uint Step, byte[] Data)
        {
            this.Header = Header;
            this.Height = Height;
            this.Width = Width;
            this.Encoding = Encoding;
            this.IsBigendian = IsBigendian;
            this.Step = Step;
            this.Data = Data;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Image(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Height = b.Deserialize<uint>();
            Width = b.Deserialize<uint>();
            Encoding = b.DeserializeString();
            IsBigendian = b.Deserialize<byte>();
            Step = b.Deserialize<uint>();
            Data = b.DeserializeStructArray<byte>();
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new Image(ref b);
        }
        
        Image IDeserializable<Image>.RosDeserialize(ref Buffer b)
        {
            return new Image(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Height);
            b.Serialize(Width);
            b.Serialize(Encoding);
            b.Serialize(IsBigendian);
            b.Serialize(Step);
            b.SerializeStructArray(Data, 0);
        }
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
            if (Encoding is null) throw new System.NullReferenceException(nameof(Encoding));
            if (Data is null) throw new System.NullReferenceException(nameof(Data));
        }
    
        public int RosMessageLength
        {
            get {
                int size = 21;
                size += Header.RosMessageLength;
                size += BuiltIns.UTF8.GetByteCount(Encoding);
                size += 1 * Data.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "sensor_msgs/Image";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "060021388200f6f0f447d0fcd9c64743";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACq1VTW8bNxC9C9B/IOBD7MRSguYSGChaIGlaHwoUiG9BIIzI0S5TLrnmh2Tl1/cNqZUs" +
                "uG5y6MKSvCTnzcd7M7xQd71NauCUqGOlg89kfVLkVfE6DGPEDhtlB2zPZxfq8s21enOlYENZ5TAuHG8y" +
                "7KLnqMLmeHA+m8/+YDJY7dvP4blQh+Vs4TXTMKrUh+KMWrMifV9sstkGX/cfIU72Z88RbBNp4JU1j7DC" +
                "mK0m17YESOM30rNIIdrOejnYLJ4iafa5ZfkdqFcPk/UYrJc6qdyzgoM+K/iQl/9O69X+HMGEnf9By2/n" +
                "lvjAe1CjI1/L8H2I23boWNOeI6jx5nz1APW+luLWb8KzeJO6KKWgLWXoaWdzf4pEdLdxVudnIeTomnva" +
                "2hBFfMUb3ljPRnRWkOPbnxBmre/JqGG35WtAQLE2XStfhnWjMYZdOprvrEFMT8zr8r9a6+DK4AEgfXGH" +
                "AB13UMmWXOGkNgiU0ULG+k4RCgj2NtZBV1G/rtCraTst9TgKCAq/D0XtqGkG3eENRWO/oXbK8w4rUeCA" +
                "PRBS+gqGxS6GtCiJY/rV2ZTTMoUSNeNUx0vPuXKXGF9oax7IOjXGMIZUQ6vAUyhLyebg5Rj9VJDfpgUk" +
                "P9oHdkktFkr35D07sEwem9doJHRk/S8h8mcoFU7pb0ZNYhgqvRK6IDfvSeplvXbF8GvEnkJcDalLT0rX" +
                "Twp4B3pWa9shT4s8G4UJ0PgylEkd9345cp4yj2cxfSzOiSzApe8gBwSx3mc+qOTd5y8N6pEF6VzAOviI" +
                "9qHutrzF92XFf1l1diVx/vw/P/PZn59+v0EaphWnTUORxKeDdkBLphqzCLJHK3DEzN6CsDp80Yt1N+9H" +
                "TsumZESOPxQLje3cXkFa6P0AwQ9D8RiFmU/TewIQU1SL1EgR47I4ijCAFqyX83VsVHz5JL4voJDV7Ycb" +
                "af7EumSLoPbCeWSq0rz9oE5M8T0M73ZhgXfuzu6PQ28qfpiuK0o34uZly3EJeBSJ4chUUrC2wmu6AlMS" +
                "BY9B9+oS4f+1z73cPdDjlqKlNRoWyJj+DrAvxOgFmDxBS+g3ypMPE36DPDn5EVxBOQBLWgu0lHFSglQ6" +
                "1BEn0bBba3B2va8o2lncRmiadaS4n8/qbVmdAuRjvcBOypfb+nz2Hpt8Gueizn8ASmrTDQ0IAAA=";
                
    }
}
