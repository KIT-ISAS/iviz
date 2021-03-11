/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [Preserve, DataContract (Name = "sensor_msgs/CameraInfo")]
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
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; } // Header timestamp should be acquisition time of image
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
        [DataMember (Name = "height")] public uint Height { get; set; }
        [DataMember (Name = "width")] public uint Width { get; set; }
        // The distortion model used. Supported models are listed in
        // sensor_msgs/distortion_models.h. For most cameras, "plumb_bob" - a
        // simple model of radial and tangential distortion - is sufficient.
        [DataMember (Name = "distortion_model")] public string DistortionModel { get; set; }
        // The distortion parameters, size depending on the distortion model.
        // For "plumb_bob", the 5 parameters are: (k1, k2, t1, t2, k3).
        [DataMember] public double[] D { get; set; }
        // Intrinsic camera matrix for the raw (distorted) images.
        //     [fx  0 cx]
        // K = [ 0 fy cy]
        //     [ 0  0  1]
        // Projects 3D points in the camera coordinate frame to 2D pixel
        // coordinates using the focal lengths (fx, fy) and principal point
        // (cx, cy).
        [DataMember] public double[/*9*/] K { get; set; } // 3x3 row-major matrix
        // Rectification matrix (stereo cameras only)
        // A rotation matrix aligning the camera coordinate system to the ideal
        // stereo image plane so that epipolar lines in both stereo images are
        // parallel.
        [DataMember] public double[/*9*/] R { get; set; } // 3x3 row-major matrix
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
        [DataMember] public double[/*12*/] P { get; set; } // 3x4 row-major matrix
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
        [DataMember (Name = "binning_x")] public uint BinningX { get; set; }
        [DataMember (Name = "binning_y")] public uint BinningY { get; set; }
        // Region of interest (subwindow of full camera resolution), given in
        //  full resolution (unbinned) image coordinates. A particular ROI
        //  always denotes the same window of pixels on the camera sensor,
        //  regardless of binning settings.
        // The default setting of roi (all values 0) is considered the same as
        //  full resolution (roi.width = width, roi.height = height).
        [DataMember (Name = "roi")] public RegionOfInterest Roi { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public CameraInfo()
        {
            DistortionModel = string.Empty;
            D = System.Array.Empty<double>();
            K = new double[9];
            R = new double[9];
            P = new double[12];
            Roi = new RegionOfInterest();
        }
        
        /// <summary> Explicit constructor. </summary>
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
        
        /// <summary> Constructor with buffer. </summary>
        public CameraInfo(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
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
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new CameraInfo(ref b);
        }
        
        CameraInfo IDeserializable<CameraInfo>.RosDeserialize(ref Buffer b)
        {
            return new CameraInfo(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Height);
            b.Serialize(Width);
            b.Serialize(DistortionModel);
            b.SerializeStructArray(D, 0);
            b.SerializeStructArray(K, 9);
            b.SerializeStructArray(R, 9);
            b.SerializeStructArray(P, 12);
            b.Serialize(BinningX);
            b.Serialize(BinningY);
            Roi.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (DistortionModel is null) throw new System.NullReferenceException(nameof(DistortionModel));
            if (D is null) throw new System.NullReferenceException(nameof(D));
            if (K is null) throw new System.NullReferenceException(nameof(K));
            if (K.Length != 9) throw new System.IndexOutOfRangeException();
            if (R is null) throw new System.NullReferenceException(nameof(R));
            if (R.Length != 9) throw new System.IndexOutOfRangeException();
            if (P is null) throw new System.NullReferenceException(nameof(P));
            if (P.Length != 12) throw new System.IndexOutOfRangeException();
            if (Roi is null) throw new System.NullReferenceException(nameof(Roi));
            Roi.RosValidate();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 281;
                size += Header.RosMessageLength;
                size += BuiltIns.UTF8.GetByteCount(DistortionModel);
                size += 8 * D.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "sensor_msgs/CameraInfo";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "c9a58c1b0b154e0e6da7578cb991d214";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACsVZbW8buRH+LiD/YWB/sJSTZTu+O6Au/KE5I3dG2iZIDKStYRjULlfiebXcW3Itb359" +
                "nxlyX/SSNAc4qJBYEkXODGeeed1DulkaRyvtnFpoSnVmCs3fvSJTZLZaKW9sQfhEihK10pWa0bUnt7R1" +
                "ntJcYxup0WH8jQq8uVIlmnDK29IkdBB+umd6B6SKlFSS2FWpCqNBoaG6xE7KzKMGHbNiQeSkE2rpxegQ" +
                "6xR+ua/Umo6J/6YKQmaVXZFf6pZ/WoFMNaXSOmfmeUOvVaMr0kViU532dPDevY5pZQubLEFKTyk1ztvK" +
                "DzffJzaHAuJm+bJ/X6UTv5co/2Ays7M5Uu6IDvZh5w3uFfaWptQ5TIN9hVemcAQdP+AXR+O4o7LJlJzX" +
                "lbb3/dIEZNh4+JzWiSkWoqzM1rKUwO6wwYbORaO9rmGvzrpiwpmItdIwH6jZbKj9UlX4ABkcqYrh5JLK" +
                "zJlDASIpUGVyUp6W3pcXJyfr9XpWWTez1eJkbR7MyeZlT34RqtfMdEsfCw21+qpp1cCXeTTgB5TWTlfH" +
                "WWV0kcL+poA4GQPSW76IXcEuZEsQZmQ7bA9aMbuwB9IzamxNa1V4nJ+Sni1mU5ABu9/Z1orOU2ANTJiR" +
                "jZpMrK1SUyiv3ZTWGlapbLGAMDAv+EMwkAh8N68Trnm9oVPIVReJys0cEut0Kr9BvsrAenQ1pbdT+jCl" +
                "9wOXzHXmQeezrix0b2uPixRsHG+SOlfAWZJDPR43Vg0p5+oV1LOEYd7ent7R5SWdzk7ZF4vUJHwLoGBD" +
                "iDYSjEaHz/MSt9h9XYs6VfJHbZyRQMTW2btVXofPJg+jzUArwLfZFmLamqa3M7wGS3R9NfpNqxQBZxne" +
                "WCaKSx70nFercmCp4dX8kN9o924tHWF1b9IBGVvCsiqPUoBGEHAfEVuZBUI29oTNu0QSIANsvkrlh6f2" +
                "YAA/oM+wBO0le4J8+eI9fmg2D6d2XfzvQ583D4m78YkyV4iLMQ6F498blvRL9AQ22/s+5n1/WEoMdFqi" +
                "a2ae4IlpXXEcaRHZCyZx2lT0qPIaHrw2ec5WZi0xHcfG59yN5Zj+OdB4BOjN4IOY1fn9jP5pfQwV4V7k" +
                "dJ4dd2whiWuQhFYhtBwMDh8AcPqPGuDKm9mOfr6ouz/z6nIEA7gAlAf5KEEIw/WRHFJG61pVJaI3Z7hx" +
                "l8kn0deZjrcXzyIP0dmM/sbhs2MTuYxZHabiIC5V0dvJ1+m8Ap2+QNglEjLB5Bv0E7MXo1cSyROyR1xz" +
                "dH4VPMz1LrbFdPa8YbZ1W1RUSI1OkvLa+CWtlyZZDtG4VmzHIRiRqPO8YXNx/h5iPKvxOZ6Demxex/xB" +
                "Jfwmd7NRjfudv0Kk5qDVflub1C9bsaLFRE8oH3NBz4w+1mUZDCmrodbJsbctdByuYav7lVu4k57Gfdg9" +
                "W87oDSqylXU+Cogq4aDM69X8fm7nB6gGuaB2ZlXmOjJGeKtUagBpRopXxQJ+xF8HIh6zt7o6y0zCyX02" +
                "QtnBDrktwZ7b9X6CEtJ85sqtRJUiBV6Iy9uqQKUitxjIHSqTn7aKwAsaP5xN6eEVfsa7x/vD+WQ2ynKr" +
                "/M8/3t7RFQt0XbCwDu1CtFkEJteuAsF9jgojhsBxmz0RnVLydIeFt3RJt/iWNZQ0d+0OLPC/M154vw/r" +
                "Q6DtJHY4wqurgBwpIrv6risfuajm7JnrYuGXKMuzpylEmIjBStwtMSVHJGYHEuMEPyfNQBF/uSOIfkjn" +
                "T+dU2fXxSv3OKBE1sIY+BCdEPTbw23Eo+FscwVp5wyU/AoX1GzvhNgup2PdfM0TtNo+jklZ80Ug9uGdI" +
                "tM6G8K9RpVvUkgA+d4zQ39zCZ4cnxP5cLAMQec6YGd71w1fuGg0E8U824NCb+0ismeD9Roz+PhodKj+C" +
                "XrG8aXr5f8bvvPwa0LDFI/sQl3Oh+A96cqVOONa5oIgOluMgyGQgSaw5+j5q3AXKCFDOwdCVgVvxTq7L" +
                "5c4cPsCZiYAz/1RIKOvSbsd22w22IjF7jt8fu/8Enr8V0EeM6KMvQZoxjR1Q/gSxyEudwkVAarJMytbY" +
                "qceCBBK+baOItMrcmfQB8eYJFr1p8Oe0D/TT3Z0S9Jm7ygHNpXrUQNZli2IY2Dci7/vbs4vzKf6jv+kZ" +
                "qxawpTJoi9reGEBGQ16vCrqFHJDi9O4o1EK5tD/BTZgr5gyheo9g2C2iedWh74MMnXUDI1M50VtYPuLW" +
                "G59m9Em3PdnNZ74/+5w4V3tlzjeRCJdxTGPXU0OfLkxaxipfq8ZBSW5TvdAE0+gr+HEQeNIexDUULdE6" +
                "fObxQ76ptEAlDgoC4WN20Jf0Gt3vEjvpdQvzuXJhjjHXfq31EKQSzH/F9AY1aQdkuv0X/Zv+cwdc+c3C" +
                "ZYz4ieja+qBshnDc4+/zFBZgIcTnDQZKiAo1PdIaZr1E7HgZ+SA9HA1qUb5KTSe0HqzxZR/bNRmeLW2e" +
                "wnZAkxgphj5R2UBPffA7e3UHlhz8ftwT/L536/KunXzAjP+X1iXMGPtuDZZasEFV4mv2ccCh9HUVRoOD" +
                "GNaXsGHGh2o49+gIF1IiwskRZTCR4RPddKh1ytqXtQ/sBEiN0OHghGoxWXI5lXJ7osGeS08cGPQ9m1lz" +
                "s3d51lL4tSkkRVcaARPAYtdBqFFF0yrBaS/ihNIYw6S5ZF8GOy7BcZHtXnBJO7fV0logE0oIJW8o6LFn" +
                "gch04Gog4TgWwwecRqDzmmdKMbQNquY9egzjNBpLwQx/mAfZ758mcJtxKKoHy1zpxLJTZ6rOfZsGunPw" +
                "q26zBBS4FoKQQxRnMAyDnfrysTMaFxZV8Bx7S0SaBfjG0r47sr3QhPJKUMjzF47cmNQgCtbzNWZgdi3j" +
                "kr0NxWQaw4oU/mHTQHHjumAufWs5yLTczvUzOfrw7jqkMYnRSF3otEPQlFa9FyQaMxbmHTC45eDRJPuT" +
                "qtIcJQnvjpdskeO2zdAiijsMa2jM84BomtPJF2wAA+y9LAjMAh4uQyOFgTaWIhguY6sFiwRlv8uuW1Vj" +
                "2+jF6PKZXy9G//j46wWicBp6sTBAewHZP8JdUmhJRr7hQQJi8BLiwSdy/YiWS0Z1PGDhX31Thm5DIj7n" +
                "El1A7Ryu2okCT3brQual/aivPS/DVCSEgb23azKmLr0jz0gwrr6+uhDd6wTahUA8yk4qraQ8u76iFsU4" +
                "MDq8Wdtjxu1iY84ohTqE1U8ltMxyKsfJ72W43Ay0oR1J9ajvZO0eX90ETSCLoEs8wMADBgy5GgTFALhH" +
                "VRk1R2fK2OC6PqUjPnQ0GVBmsS/w+KawLflAsefxLWSLji7f6RhxOmWXhnsD4bwxTv37VCFjbZKwXTUj" +
                "magKy9Hhm1D3svnEInhHkWUTIwUdB/22X27nq98RkIP5wLYrMDo3nsnx/D9CLPQmSHVtzhxGK74CY6zo" +
                "WgNQ+rREaNo+zppCtBm6/nDIwkf67IyZC+tfPWhplHR0Yqmpg6+jyOICKE5nNcTQUs7E2U2/H3Q2TrRN" +
                "zcAOwvOvGFK3x2Ig4YSw8UBCoN3NeYYDnlbqro7AU4roKk/3Nstwa54n/x29mIxgJJy2OuAgvFsD0RjZ" +
                "qNvAfpjX/Kyp6+l0utgcQU9als2A5Y0tn4MjntN9hWHUmxD5LXzGPuYynHBFLp/kc/ydgVfVoMeVK49a" +
                "wFXKoFhHC2S6pwXwUY5jbLSuszvAlOaAc6mI3D5TExijG2nweFEipqz3lN6gcxOunTnDFB/m7o1IYyaK" +
                "nChoRtZl/PXi9pl6+4w8acOJ2WhuLeZlVp67wg3g3v8FZ4tisv4eAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
