/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.StereoMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
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
            F = b.Deserialize<float>();
            T = b.Deserialize<float>();
            ValidWindow = new SensorMsgs.RegionOfInterest(ref b);
            MinDisparity = b.Deserialize<float>();
            MaxDisparity = b.Deserialize<float>();
            DeltaD = b.Deserialize<float>();
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
            if (Image is null) BuiltIns.ThrowNullReference(nameof(Image));
            Image.RosValidate();
            if (ValidWindow is null) BuiltIns.ThrowNullReference(nameof(ValidWindow));
            ValidWindow.RosValidate();
        }
    
        public int RosMessageLength => 37 + Header.RosMessageLength + Image.RosMessageLength;
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "stereo_msgs/DisparityImage";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "04a177815f75271039fa21f16acad8c9";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71XW4/UNhR+bn6FxT6wA7OzCF4qqlWrlm67EhUIVqoEoiNPfDIxOHZqO3Ph1/c7dpLJ" +
                "zALlgXak3Zk45375zvGZeE2t9DKSqEkq8qJyXpSuaWXUK2103IutjrUoO+/JRnGrG3q9t2XtndUfyS+K" +
                "M/FcfyCzF9GJFQlPjduQEtoKKQwEexwZkoHmogskdCPXtOiVaRsifi2K3/NzPi4g89o4WGDXonUaapUO" +
                "MJOtyfzitqbxUFMQ0pNoPV1I9b6DTJX8kHYPUbsLV1WBIqyLWyIrYs202pa6lSYrCMJV6TxunShlQ14G" +
                "ca4zbQnjISfWMvLzPinbkF+TmrElONbhqVDiSuyWRlzgv8f/8zI/4cvPikA2OL9swjpc3rAP2RP29TWC" +
                "RE6syTUU/X4hrmH7wWM1T1YoapGHyrumN4qNhGLxBnqr20tEseKgPXksKoEAuhLOGbLrWM9Fq3dkwkhw" +
                "C4Kf4ZXRFmnZOm+U6KyOIZnTrbbaKrflmJy3LiLvWhqzn4mNNFpNTMNzR2Fx5NwrWmtnX1Q3lt0KMTMt" +
                "s0iWz6nz0iICkD/NYSDpy5oU19RNDv1J3uec0smhoRA4L1Y02i4nxAGllfQueoWHd1lLb4GiCiEISVft" +
                "vGth81wg/E+eiY0zXUNj2iEo5ERJs3YQVTdIghX3AtG9hXiZyyg1yxvhuhi0Yg+fgo8/b5YwMWVKXIpG" +
                "7g7WHgjkbiSYugOC0nVIkXVcxCjtzk6yfUw7nh6p4LQ2yCHnA19uS9M0ohXQtkhz7qswUpa1po1cmaH6" +
                "UtAgC3lFcCLyzKFWZKJcchmev/nr8WV1O3uQjyZG9gdFcfWNP8Ufr397isyoXH0ZSFJTSaukVwI9JZWM" +
                "MiFCrdc1+QtDGzJgkk3LgeC3cd9yJXO1cPUEtKNFg6HsGbYUoxtgsUGXlIyWETh4xA/OBHkIadRlZyTD" +
                "qPNKWyavPNqVpXMV0d8d2ZLEzbOnoLGBSoRywxCaEiED497NM1F0KCnEDgzF2e3WXeCR1oDJUXkuThhL" +
                "O4BfYDtl4Ip7kJ1bQDaCQ9CigGfpbInHMBNQAhOodegFBrqX+1i73HQbVEVKOgSXXApK3Gem+7OJZJtE" +
                "W2ndID5LPOj4GrF2lMs+XaCTlWHvQ7dGAEHYerdBIymx2mfYM5rHkNErL/2+YK6ssji75hiDiCuSM4Jv" +
                "GYIrteR5wI1ZhOhZesrGUv931XiK9UNZIW2BoR8BixLDD3AG3OVx22dP99Tnj+bi0Sy5APBxLSq2imDz" +
                "qEmGzZ6uOB6cov+cif74UCihTggC9JDl350OOjVvCt8oTnzqM8oagjYRBbjUPGXSK5aTh9LnBAE01ygJ" +
                "0GWGu4JK4qHx75Ie7gbmvB6gObk4IL+Oop/aX/Tp4f5YAEaT/TrGj8eM+HOsvjXS0rBEfFHCTaYZwwk8" +
                "QlKsOj7tJf2SwnBjK/c5cUNJnZT6wQ4utsroMn5OAlOuqJYb7VLLYbikqYj26AGophTYA0sWnI/nAwjN" +
                "he2aVU6fd9swcG+1gj13uNPxJ5lLHrw2DLuCoTVKI68aCcQBnk5xH/MehqRVGqASfHmZBC+H12FRtm2R" +
                "Ar53ndjKXCihHwzYXgHXlrZiQAXnGwl33iOxPONcuADy+/CT0SGGRXCdLwlEWD8ttklOGRpdcQ9TI7Vh" +
                "rGpdAu8sdzBkUQzAM1o+hOLX4QBu5x1NXGBjBA5aDKiGpMVLXkjQgelXgNmfTiRnUn7AejuuiGw3C87K" +
                "eSXiAWM6RZdThDqNWp3z/j2yslxpjEGl4WLOXJjg6/jux3FQRWqPDLrujOFa6PdQtmC1jxSKs6Th7bss" +
                "acIhy9gh20iE17v0NvvMqs+T/AepuGbFVMJ3A/vbV0CQd/8Hrp+uuacQz43U7w2hpVJXuDZgb2KmhLjD" +
                "dszNyouD7W82APUz8WeNPJ6yc0pfvbiBFTH2JTO5CGxrmsAXqj0UfUHk60Pfw1y1uSErTYandkYzghno" +
                "PsS9rE/oIeeIg1uQKSaIk3T+wHtzz3bVs1yJR2kxUmlrCuMyLapUGZM1crAa/rSx83wLGMpqt+wvcEjw" +
                "c8zBxiFuqVuGGCAsd5uCp6jQI8FQ+3nZT/OU1PoYs2eDyv1E5a1rv4VGTPEvKDxALA/c9Bt0rOUOiqI8" +
                "0u/+PRee74j1St7pURtlRGjLqJEwlUtmnLSYsrycphvygBP3vNzeg5hscu7wvoxxIdi3PJr5fs/nB0nX" +
                "0gQavE3pHJYXfUiiOGehfG/hap7NU/0dzA3jVfOUB2ISx6JYOWcwn5fZI1xl/gEydzEatBAAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
