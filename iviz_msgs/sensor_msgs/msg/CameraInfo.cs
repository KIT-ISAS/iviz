/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class CameraInfo : IDeserializable<CameraInfo>, IMessage
    {
        // This message defines meta information for a camera. It should be in a
        // camera namespace on topic "camera_info" and accompanied by up to five
        // image topics named:
        //
        //   image_raw - raw data from the camera driver, possibly Bayer encoded
        //   image            - monochrome, distorted
        //   image_color      - color, distorted
        //   image_rect       - monochrome, rectified
        //   image_rect_color - color, rectified
        //
        // The image_pipeline contains packages (image_proc, stereo_image_proc)
        // for producing the four processed image topics from image_raw and
        // camera_info. The meaning of the camera parameters are described in
        // detail at http://www.ros.org/wiki/image_pipeline/CameraInfo.
        //
        // The image_geometry package provides a user-friendly interface to
        // common operations using this meta information. If you want to, e.g.,
        // project a 3d point into image coordinates, we strongly recommend
        // using image_geometry.
        //
        // If the camera is uncalibrated, the matrices D, K, R, P should be left
        // zeroed out. In particular, clients may assume that K[0] == 0.0
        // indicates an uncalibrated camera.
        //######################################################################
        //                     Image acquisition info                          #
        //######################################################################
        // Time of image acquisition, camera coordinate frame ID
        [DataMember (Name = "header")] public StdMsgs.Header Header; // Header timestamp should be acquisition time of image
        // Header frame_id should be optical frame of camera
        // origin of frame should be optical center of camera
        // +x should point to the right in the image
        // +y should point down in the image
        // +z should point into the plane of the image
        //######################################################################
        //                      Calibration Parameters                         #
        //######################################################################
        // These are fixed during camera calibration. Their values will be the #
        // same in all messages until the camera is recalibrated. Note that    #
        // self-calibrating systems may "recalibrate" frequently.              #
        //                                                                     #
        // The internal parameters can be used to warp a raw (distorted) image #
        // to:                                                                 #
        //   1. An undistorted image (requires D and K)                        #
        //   2. A rectified image (requires D, K, R)                           #
        // The projection matrix P projects 3D points into the rectified image.#
        //######################################################################
        // The image dimensions with which the camera was calibrated. Normally
        // this will be the full camera resolution in pixels.
        [DataMember (Name = "height")] public uint Height;
        [DataMember (Name = "width")] public uint Width;
        // The distortion model used. Supported models are listed in
        // sensor_msgs/distortion_models.h. For most cameras, "plumb_bob" - a
        // simple model of radial and tangential distortion - is sufficient.
        [DataMember (Name = "distortion_model")] public string DistortionModel;
        // The distortion parameters, size depending on the distortion model.
        // For "plumb_bob", the 5 parameters are: (k1, k2, t1, t2, k3).
        [DataMember] public double[] D;
        // Intrinsic camera matrix for the raw (distorted) images.
        //     [fx  0 cx]
        // K = [ 0 fy cy]
        //     [ 0  0  1]
        // Projects 3D points in the camera coordinate frame to 2D pixel
        // coordinates using the focal lengths (fx, fy) and principal point
        // (cx, cy).
        [DataMember] public double[/*9*/] K; // 3x3 row-major matrix
        // Rectification matrix (stereo cameras only)
        // A rotation matrix aligning the camera coordinate system to the ideal
        // stereo image plane so that epipolar lines in both stereo images are
        // parallel.
        [DataMember] public double[/*9*/] R; // 3x3 row-major matrix
        // Projection/camera matrix
        //     [fx'  0  cx' Tx]
        // P = [ 0  fy' cy' Ty]
        //     [ 0   0   1   0]
        // By convention, this matrix specifies the intrinsic (camera) matrix
        //  of the processed (rectified) image. That is, the left 3x3 portion
        //  is the normal camera intrinsic matrix for the rectified image.
        // It projects 3D points in the camera coordinate frame to 2D pixel
        //  coordinates using the focal lengths (fx', fy') and principal point
        //  (cx', cy') - these may differ from the values in K.
        // For monocular cameras, Tx = Ty = 0. Normally, monocular cameras will
        //  also have R = the identity and P[1:3,1:3] = K.
        // For a stereo pair, the fourth column [Tx Ty 0]' is related to the
        //  position of the optical center of the second camera in the first
        //  camera's frame. We assume Tz = 0 so both cameras are in the same
        //  stereo image plane. The first camera always has Tx = Ty = 0. For
        //  the right (second) camera of a horizontal stereo pair, Ty = 0 and
        //  Tx = -fx' * B, where B is the baseline between the cameras.
        // Given a 3D point [X Y Z]', the projection (x, y) of the point onto
        //  the rectified image is given by:
        //  [u v w]' = P * [X Y Z 1]'
        //         x = u / w
        //         y = v / w
        //  This holds for both images of a stereo pair.
        [DataMember] public double[/*12*/] P; // 3x4 row-major matrix
        //######################################################################
        //                      Operational Parameters                         #
        //######################################################################
        // These define the image region actually captured by the camera       #
        // driver. Although they affect the geometry of the output image, they #
        // may be changed freely without recalibrating the camera.             #
        //######################################################################
        // Binning refers here to any camera setting which combines rectangular
        //  neighborhoods of pixels into larger "super-pixels." It reduces the
        //  resolution of the output image to
        //  (width / binning_x) x (height / binning_y).
        // The default values binning_x = binning_y = 0 is considered the same
        //  as binning_x = binning_y = 1 (no subsampling).
        [DataMember (Name = "binning_x")] public uint BinningX;
        [DataMember (Name = "binning_y")] public uint BinningY;
        // Region of interest (subwindow of full camera resolution), given in
        //  full resolution (unbinned) image coordinates. A particular ROI
        //  always denotes the same window of pixels on the camera sensor,
        //  regardless of binning settings.
        // The default setting of roi (all values 0) is considered the same as
        //  full resolution (roi.width = width, roi.height = height).
        [DataMember (Name = "roi")] public RegionOfInterest Roi;
    
        /// Constructor for empty message.
        public CameraInfo()
        {
            DistortionModel = string.Empty;
            D = System.Array.Empty<double>();
            K = new double[9];
            R = new double[9];
            P = new double[12];
            Roi = new RegionOfInterest();
        }
        
        /// Explicit constructor.
        public CameraInfo(in StdMsgs.Header Header, uint Height, uint Width, string DistortionModel, double[] D, double[] K, double[] R, double[] P, uint BinningX, uint BinningY, RegionOfInterest Roi)
        {
            this.Header = Header;
            this.Height = Height;
            this.Width = Width;
            this.DistortionModel = DistortionModel;
            this.D = D;
            this.K = K;
            this.R = R;
            this.P = P;
            this.BinningX = BinningX;
            this.BinningY = BinningY;
            this.Roi = Roi;
        }
        
        /// Constructor with buffer.
        internal CameraInfo(ref Buffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Height = b.Deserialize<uint>();
            Width = b.Deserialize<uint>();
            DistortionModel = b.DeserializeString();
            D = b.DeserializeStructArray<double>();
            K = b.DeserializeStructArray<double>(9);
            R = b.DeserializeStructArray<double>(9);
            P = b.DeserializeStructArray<double>(12);
            BinningX = b.Deserialize<uint>();
            BinningY = b.Deserialize<uint>();
            Roi = new RegionOfInterest(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b) => new CameraInfo(ref b);
        
        CameraInfo IDeserializable<CameraInfo>.RosDeserialize(ref Buffer b) => new CameraInfo(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Height);
            b.Serialize(Width);
            b.Serialize(DistortionModel);
            b.SerializeStructArray(D);
            b.SerializeStructArray(K, 9);
            b.SerializeStructArray(R, 9);
            b.SerializeStructArray(P, 12);
            b.Serialize(BinningX);
            b.Serialize(BinningY);
            Roi.RosSerialize(ref b);
        }
        
        public void RosValidate()
        {
            if (DistortionModel is null) throw new System.NullReferenceException(nameof(DistortionModel));
            if (D is null) throw new System.NullReferenceException(nameof(D));
            if (K is null) throw new System.NullReferenceException(nameof(K));
            if (K.Length != 9) throw new RosInvalidSizeForFixedArrayException(nameof(K), K.Length, 9);
            if (R is null) throw new System.NullReferenceException(nameof(R));
            if (R.Length != 9) throw new RosInvalidSizeForFixedArrayException(nameof(R), R.Length, 9);
            if (P is null) throw new System.NullReferenceException(nameof(P));
            if (P.Length != 12) throw new RosInvalidSizeForFixedArrayException(nameof(P), P.Length, 12);
            if (Roi is null) throw new System.NullReferenceException(nameof(Roi));
            Roi.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 281;
                size += Header.RosMessageLength;
                size += BuiltIns.GetStringSize(DistortionModel);
                size += 8 * D.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "sensor_msgs/CameraInfo";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "c9a58c1b0b154e0e6da7578cb991d214";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACsVZbW8buRH+rl8xsD9YysmyHV8L1IU/NGfkzkjbBImBa2sYBrXLlXheLfeWXEubX99n" +
                "htwXvSTNAQ4qJJZEkTPDmWde95julsbRSjunFppSnZlC83evyBSZrVbKG1sQPpGiRK10pWZ068ktbZ2n" +
                "NNfYRmp0HH+jAm+uVIkmnPK2NAkdhZ8emd4RqSIllSR2VarCaFBoqC6xkzLzrEHHrFgQOemEWno1OsY6" +
                "hV8eK7WmU+K/qYKQWWVX5Je65Z9WIFNNqbTOmXne0BvV6Ip0kdhUpz0dvHevU1rZwiZLkNJTSo3ztvLD" +
                "zY+JzaGAuFm+HN5X6cQfJMo/mMzsbY6UO6KDfdh5h3uFvaUpdQ7TYF/hlSkcQcdP+MXROO6obDIl53Wl" +
                "7WO/NAEZNh4+p3ViioUoK7O1LCWwO2ywpXPRaK9r2KuzrphwJmKtNMwHajYbar9UFT5ABkeqYji5pDJz" +
                "5lCASApUmZyUp6X35dXZ2Xq9nlXWzWy1OFubJ3O2fdmzn4TqLTPd0cdCQ62+alo18GWeDfgBpbXT1WlW" +
                "GV2ksL8pIE7GgPSWL2JXsAvZEoQZ2Q7bg1bMPuyB9IwaW9NaFR7np6Rni9kUZMDuN7a1ossUWAMTZmSj" +
                "JhNrq9QUyms3pbWGVSpbLCAMzAv+EAwkAt/t64Rr3m7pFHLVRaJyM4fEOp3Kb5CvMrAe3Uzp3ZQ+TunD" +
                "wCVznXnQ+awrC93b2uMiBRvHm6TOFXCW5FCPx41VQ8q5egX1LGGYd/fnD3R9Teezc/bFIjUJ3wIo2BKi" +
                "jQSj0fHLvMQt9l+3ok6V/F4bZyQQsXUObpXX8YvJw2gz0ArwbXaFmLam6e0Mr8ES3d6MftEqRcBZhjeW" +
                "ieKSBz3n1aocWGp4NT/kN9q/W0tHWD2adEDGlrCsyqMUoBEEPETEVmaBkI09YfM+kQTIAJuvUvlh0x4M" +
                "4Af0GZagvWRPkC9fvMcPzfbh1K6L/33o8/YhcTc+UeYKcTHGoXD8e8OSfoqewGb70Me87w9LiYFOS3TN" +
                "zAaemNYVx5EWkb1gEqdNRc8qr+HBa5PnbGXWEtNxbHzO3ViO6Z8DjUeA3g4+iFmd38/on9bHUBHuRU7n" +
                "2WnHFpK4BkloFULL0eDwEQCnf68BrryZ7enni7r7I68uRzCAC0B5kI8ShDBcH8khZbSuVVUienOGG3eZ" +
                "fBJ9nel4e/Ui8hBdzOhvHD47NpHLmNVhKg7iUhW9m3ydzmvQ6QuEfSIhE0y+QT8xezF6JZFskD3imqPL" +
                "m+BhrnexHaazlw2zrduiokJqdJKU18Yvab00yXKIxrViOw7BiESd5w2bi/P3EONZjc/xHNRj8zrmDyrh" +
                "N7mbjWrc7/I1IjUHrfbb2qR+2YoVLSZ6QvmYC3pm9Kkuy2BIWQ21To69baHjcA1bPa7cwp31NB7D7tly" +
                "Rm9Rka2s81FAVAlHZV6v5o9zOz9CNcgFtTOrMteRMcJbpVIDSDNSvCoW8CP+OhDxlL3V1VlmEk7usxHK" +
                "DnbIXQkO3K73E5SQ5jNXbiWqFCnwQlzeVQUqFbnFQO5Qmfxppwi8ovHTxZSeXuNnvHu8P11OZqMst8r/" +
                "+cf7B7phgW4LFtahXYg2i8Dk2lUgeMhRYcQQOO6zDdE5JZsHLLyja7rHt6yhpHlod2CB/13wwodDWB8C" +
                "bS+xwxFe3wTkSBHZ1Xdd+chFNWfPXBcLv0RZnm2mEGEiBitxt8SUHJGYHUiME/ycNANF/OWBIPoxXW4u" +
                "qbLr05X6jVEiamANfQxOiHps4LfjUPC3OIK18oZLfgQK67d2wm0WUrEfvmaI2m0eRyWt+KKRenDPkGid" +
                "DeFfo0q3qCUBfO4Yob+5hc8OT4j9uVgGIPKcMTO868ev3DUaCOKfbcGhN/eJWDPB+50Y/UM0OlR+Ar1i" +
                "edv08v+C33n5DaBhi2f2IS7nQvEf9ORKnXCsc0ERHSzHQZDJQJJYc/R91LgLlBGgnIOhKwO34p1cl8ud" +
                "OXyAMxMBZ/6pkFDWpd2O7a4b7ERi9hx/OHb/ATx/K6BPGNEnX4I0Yxo7oPwJYpGXOoWLgNRkmZStsVOP" +
                "BQkkfNdGEWmVuTPpA+LdBha9a/DnvA/00/2dEvSZu8oBzaV61kDWdYtiGNg3Iu+H+4uryyn+o7/pGasW" +
                "sKUyaIva3hhARkNerwq6hxyQ4vzhJNRCubQ/wU2YK+YMoXqPYNgvonnVoe+DDJ11AyNTOdFbWD7h1huf" +
                "ZvSrbnuyu898f/Y5ca72ypxvIhEu45jGvqeGPl2YtIxVvlaNg5LctnqhCabRV/DjIPCkPYhrKFqidfjM" +
                "44d8W2mBShwUBMKn7KCv6A263yV20psW5nPlwhxjrv1a6yFIJZj/jOkNatIOyHT/L/o3/ecBuPLbhcsY" +
                "8RPRtfVB2QzhuMc/5CkswEKIzxsMlBAVanqmNcx6jdjxKvJBejgZ1KJ8lZrOaD1Y48s+t2syPFvaPIXt" +
                "gCYxUgx9orKBnvrgd/H6ASw5+P14IPh979blfTv5gBn/L61LmDH23RostWCDqsTX7OOAQ+nrKowGBzGs" +
                "L2HDjA/VcO7RES6kRISTI8pgIsMnuulQ65S1L2sf2AmQGqHDwQnVYrLkcirl9kSDPZeeODDoe7az5nbv" +
                "8qKl8BtTSIquNAImgMWug1CjiqZVgtNexAmlMYZJc8m+DHZcguMi273gknZuq6W1QCaUEEreUNBjzwKR" +
                "6cjVQMJpLIaPOI1A5zXPlGJoG1TNB/QYxmk0loIZ/jAPsj9uJnCbcSiqB8tc6cSyU2eqzn2bBrpz8Ktu" +
                "swQUuBaCkEMUZzAMg5368rELGhcWVfAce0tEmgX4xtK+O7K70ITySlDI8xeO3JjUIArW8zVmYHYt45KD" +
                "DcVkGsOKFP5h00Bx47pgLn1rOci03M71Mzn6+P42pDGJ0Uhd6LRD0JRWvRckGjMW5h0wuOXg0ST7k6rS" +
                "HCUJ746XbJHjds3QIoo7DGtozPOAaJrzyRdsAAMcvCwIzAIerkMjhYE2liIYrmOrBYsEZb/PbltVY9to" +
                "dP3Cr9E/Pv18hRichk4sjM8g+Cf4SgoVybw3PEVAAF5CNjhErp/Rb8mcjqcr/KtvytBqSLjnRKIL6Jxj" +
                "VTtO4LFuXciwtJ/ztedlkopsMDD2bkHG1KVx5AEJZtW3N1eieJ1AtRCI59hJpZXUZrc31EIYB0bHd2t7" +
                "yqBdbA0ZpUqHsHpTQsUsp3Kc+V6Fy81AG8qRPI/iTtYe8dVN0AGyCLrE0ws8XcCEq0FEDGh7VpVRc7Sl" +
                "DAwu6lM64UMnkwFlFvsKz24K25IPFHse30K26OjynU4RpFP2Z/g24M0b48i/zxMy0yaJ2VUzknGqsBwd" +
                "vw1FL5tPLIJ3VFg2MVLNccRvm+V2uPrd0DgYDex6wWjnaRxP/iO+QleCJNdmy2GcYvkZYEXXFIDSr0sE" +
                "pd3jrCbEmaHTD8crfKTPy5i2sPLVk5YWSUf3lWo6eDnKKy594lxWQwwthUyc2vT7QWfrRNvODIwgPP+K" +
                "8XR7LIYQTgVbjyIE192EZzjaaaXuKgg8n4h+snm0WYZb8yT57+jCZPgigbTVAYff/eqHxshD3QZ2wrzm" +
                "p0xdN6fTxfbwedKybAYs72z5EhzxhO4rDKPehMgv4TP2MZfhbIv4BXjI5/g7A6+qQY9rVh6ygKsUQLGC" +
                "Fsh0zwngoBzE2GhdT3eE+cwRZ1ERuX2aJjBGH9LgwaKES1nvKb1FzyZcO3OG+T3M3RuRxkwU2VDQjHzL" +
                "+OvF7XP07hl5xoYTs9HcWkzKrDxxhRuMRv8Ff1S2+/ceAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
