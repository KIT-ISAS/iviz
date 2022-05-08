/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StereoMsgs
{
    [DataContract]
    public sealed class DisparityImage : IDeserializable<DisparityImage>, IMessage
    {
        // Separate header for compatibility with current TimeSynchronizer.
        // Likely to be removed in a later release, use image.header instead.
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        // Floating point disparity image. The disparities are pre-adjusted for any
        // x-offset between the principal points of the two cameras (in the case
        // that they are verged). That is: d = x_l - x_r - (cx_l - cx_r)
        [DataMember (Name = "image")] public SensorMsgs.Image Image;
        // Stereo geometry. For disparity d, the depth from the camera is Z = fT/d.
        /// <summary> Focal length, pixels </summary>
        [DataMember (Name = "f")] public float F;
        /// <summary> Baseline, world units </summary>
        [DataMember] public float T;
        // Subwindow of (potentially) valid disparity values.
        [DataMember (Name = "valid_window")] public SensorMsgs.RegionOfInterest ValidWindow;
        // The range of disparities searched.
        // In the disparity image, any disparity less than min_disparity is invalid.
        // The disparity search range defines the horopter, or 3D volume that the
        // stereo algorithm can "see". Points with Z outside of:
        //     Z_min = fT / max_disparity
        //     Z_max = fT / min_disparity
        // could not be found.
        [DataMember (Name = "min_disparity")] public float MinDisparity;
        [DataMember (Name = "max_disparity")] public float MaxDisparity;
        // Smallest allowed disparity increment. The smallest achievable depth range
        // resolution is delta_Z = (Z^2/fT)*delta_d.
        [DataMember (Name = "delta_d")] public float DeltaD;
    
        /// Constructor for empty message.
        public DisparityImage()
        {
            Image = new SensorMsgs.Image();
            ValidWindow = new SensorMsgs.RegionOfInterest();
        }
        
        /// Constructor with buffer.
        public DisparityImage(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Image = new SensorMsgs.Image(ref b);
            b.Deserialize(out F);
            b.Deserialize(out T);
            ValidWindow = new SensorMsgs.RegionOfInterest(ref b);
            b.Deserialize(out MinDisparity);
            b.Deserialize(out MaxDisparity);
            b.Deserialize(out DeltaD);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new DisparityImage(ref b);
        
        public DisparityImage RosDeserialize(ref ReadBuffer b) => new DisparityImage(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            Image.RosSerialize(ref b);
            b.Serialize(F);
            b.Serialize(T);
            ValidWindow.RosSerialize(ref b);
            b.Serialize(MinDisparity);
            b.Serialize(MaxDisparity);
            b.Serialize(DeltaD);
        }
        
        public void RosValidate()
        {
            if (Image is null) BuiltIns.ThrowNullReference();
            Image.RosValidate();
            if (ValidWindow is null) BuiltIns.ThrowNullReference();
            ValidWindow.RosValidate();
        }
    
        public int RosMessageLength => 37 + Header.RosMessageLength + Image.RosMessageLength;
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "stereo_msgs/DisparityImage";
    
        public string RosMessageType => MessageType;
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public const string Md5Sum = "04a177815f75271039fa21f16acad8c9";
    
        public string RosMd5Sum => Md5Sum;
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71XbY/UNhD+nl9hcR+4hb09VL4gKtSqpdeeRAWCkyodoitvPNkYHDvYzr7w6/uMnWSz" +
                "e0D5QLsS3K4zb5555pnJmXhDrfQykqhJKvKicl6Urmll1CttdNyLrY61KDvvyUZxoxt6s7dl7Z3Vn8gv" +
                "ijPxQn8gsxfRiRUJT43bkBLaCikMDHscGZKB5qILJHQj17TonWkbIr4tij/y73xcwOaVcYjArkXrNNwq" +
                "HRAmR5P1xU1N46GmIKQn0Xq6kOp9B5sq3UPaPUztLlxVBYqILm6JrIg1y2pb6laa7CAIV6XzuHWilA15" +
                "GcS5zrIlgoedWMvIv/fJ2Yb8mtSMI8GxDk+FEs/EbmnEBf73+P+8zL/wx8+KQDY4v2zCOlxe8x3yTfiu" +
                "b5AkcmJNrqHo9wtxhdgPN1bzFIWiFnWovGv6oDhIOBa38FvdXCKLFSft8Q+iEkigK3E5Q3Yd67lo9Y5M" +
                "GAVuIPALbmW0RVm2zhslOqtjSOF0q622ym05J+eti6i7lsbsZ2IjjVaT0PC7o7A4utxrWmtnX1bXlq8V" +
                "YlZaZpNsn0vnpUUGYH9aw0DSlzUpxtR1Tv1J3edc0smhoRC4LlY02i4nwgHQSn4XvcPDs+ylj0BRhRSE" +
                "5Kt23rWIeS6Q/sfPxcaZrqGx7DAUcqGkWTuYqhsUwYp7gejeQrzKMErNcitcF4NWfMOn0OPP7RIhpkqJ" +
                "S9HI3SHag4DcjQLT60CgdB1KZB2DGNDu7KTax7Lj6ZELLmuDGnI98MdtaVpGtALaFmXOfRVGybLWtJEr" +
                "M6AvJQ22UFckJ6LOnGpFJsolw/D89u8fLqub2YN8NAmyPyiKZ9/5U/z55venqIzK6MtEkppKWiW9Eugp" +
                "qWSUiRFqva7JXxjakIGSbFpOBD+N+5aRzGhh9AS0o0WDAfZMW4rZDbTYoEtKZssIHjzSh2aiPKQ06rIz" +
                "kmnUeaUti1ce7crWGUX0sSNbkrh+/hQyNlCJVG6YQlMhZGDeu34uig6QQu6gUJzdbN0FftIaNDk6z+BE" +
                "sLQD+QWOUwZG3IN8uQVsIzkELwp8ls6W+BlmAk4QArUOvcBE92ofa5ebbgNUpKLDcMlQUOI+K92fTSzb" +
                "ZNpK6wbz2eLBx7eYtaNdvtMFOlkZvn3o1kggBFvvNmgkJVb7THtG8xgyeuWl3xeslV0WZ1ecYwgxIrki" +
                "+CtDcKWWPA+4MYsQPVtP1Vjq/w6Np1w/wAplC0z9SFiUGH6gM/Auj9u+erqXPn80F49m6QogH9cCsVWE" +
                "mgcmmTZ7ueJ4cIr+cyb64wNQQp0YBOwhy4+dDjo1b0rfaE587jPaGpI2MQW61Dxl0iO2k4fSlwyBNNeA" +
                "BOSywl1DJfHQ+HdLD3eDcl4P0JwMDtivo+in9lfv9HB/bACjyX6b4qdjRfxz7L410tKwRHzVwnWWGdMJ" +
                "PkJRrDo+7S39mtJwbSv3JXMDpE6gfoiDwVYZXcYvWWDJFdVyo11qOQyXNBXRHj0B1ZQSe1DJhvPxfCCh" +
                "ubBds8rl824bBu2tVojnjnY6/qxyyYPXhmFXMLQGNPKqkUgc5OkU9zHvYShapUEqwZeXyfByeBwWZdsW" +
                "KeF714mtzEAJ/WDA9gq6trQVAys430hc5z0KyzPOhQswvw8/Gx1iWATX+ZIghPXTYpvkkqHRFfcwNVIb" +
                "5qrWJfLOdodAFsVAPGPkQyp+Gw5w7byjiQtsjOBBiwHVkLR4yAsJOjB9Cwj784XkSsoPWG/HFZHjZsPZ" +
                "Oa9EPGBMp+hyylCnWatz3Z+gKsuVxhhUGlfMlQsTfh2f/TQOqkjtUUBXnTGMhX4P5QhW+0gZGk/evsuG" +
                "JgpvX4MB3oGjYoeaoxxe75JQvjkHcJ68PEgQm/0fDH660J6SObdMvyGElkpd4QUBGxIrJW4d9mBuS14R" +
                "bP8OA/o+E3/VqNipOhfv9ctrRBFjD47Jyr+taUJUwHUo+tLnF4W+WxmfufUqTYbnc+YtQhjoM+S2rE/k" +
                "YedIg5uNJSbcknz+yBtyr/asV3kmHqUVSKX9KIxrs6gSBiYL4xA17tPGzvO+PwBot+xf1QCFF5h4jUPe" +
                "Ul8MOUBa7sKf56XQo8CA8rzWp8lJan3MzrPB5X7i8sa138Mj5vVXHB7IlEdr+g459nKHLwGP9L1/zsDz" +
                "HbFfyds7sFFGpLaMGgVTGTLjTMU85TU0vQsPjHDPy+09mMkh517uYYzVf9/yEOY3eT4/WLqSJtBw21TO" +
                "YU3RhyKKczbKbyiM5tk84e8QbhhfKk91YCZpLIqVcwaTeJlvhJeWfwDtTzPDnhAAAA==";
                
    
        public override string ToString() => Extensions.ToString(this);
    
        public void Dispose()
        {
        }
    }
}
