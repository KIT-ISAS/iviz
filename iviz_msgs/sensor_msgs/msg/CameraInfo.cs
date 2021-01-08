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
            Header = new StdMsgs.Header();
            DistortionModel = "";
            D = System.Array.Empty<double>();
            K = new double[9];
            R = new double[9];
            P = new double[12];
            Roi = new RegionOfInterest();
        }
        
        /// <summary> Explicit constructor. </summary>
        public CameraInfo(StdMsgs.Header Header, uint Height, uint Width, string DistortionModel, double[] D, double[] K, double[] R, double[] P, uint BinningX, uint BinningY, RegionOfInterest Roi)
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
        
        public void RosValidate()
        {
            if (Header is null) throw new System.NullReferenceException(nameof(Header));
            Header.RosValidate();
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
                "H4sIAAAAAAAACsVZbW8buRH+HiD/YeB8sJSTZTu+FqgLf2jOuDsjbRMkBtLWMARqlyvxvFruLbmWNr++" +
                "zwy5L3pxmgN8VyGxJIqcGc4887qv6HZpHK20c2qhKdWZKTR/94pMkdlqpbyxBeETKUrUSldqSjee3NLW" +
                "eUpzjW2kXr54FX+kAm+uVIkmHPO2NAkdhZ9mTPCIVJGSShK7KlVhNEg0VJfYSZl51EzIrFgUOeqEXHqJ" +
                "Zf6Fwm+zSq3phPhvqiBoVtkV+aVuRUgrUKomVFrnzDxv6K1qdEW6SGyq0wEhvHevE1rZwiZL0NITSo3z" +
                "tvJbu2eJzaGGuFu+PLGx0ok/SJZ/MJnZ3x1pd2SHG3nvLW4Xdpem1DmMhJ2FV6ZwBGU/4BdHo7ijssmE" +
                "nNeVtrN+acx02I74ktaJKRais8zWspQAArDGlvJFsb3KYbne0GLNqQi20rAkyNlsaIVSVfgAKRypiqHl" +
                "ksrMmUXBVFJAzOSkPC29Ly9PT9fr9bSybmqrxenaPJjT7fue/iBkb5jrnlIWGur1VdPqgi/0aMASoK2d" +
                "rk6yyugiBRRMAYkyhqe3chm7goHIlqDNSHfYH1Rj9t0AyM+osTWtVeFBYEJ6uphOmA4Y/sJWV3SRAnhg" +
                "w6xs1GdibZWaQnntJrTWME5liwXEgZ0hAERjGoHz9o3au95s6Ray1UWicjOH1DqdyG+QsTIwI11P6N2E" +
                "Pk7ow8BNc515JvRFVxZGsLXHbQq2kjdJnSuALsmhJI9rq4aUc/UKSlrCQO/uzu7p6orOpmfin0VqEr4K" +
                "ALElRhsfXr7Atud5BT/Zf92IWlXya22ckQjFZjq4VV6vnlEigZ6BcoB3syvHpDVRb3K4EZbo5vrli5+1" +
                "ShGJluGN5aK45EHQebUqByYbXs8PGb58sX/BlpAwm5l0QMeWMLHKoxwgEkQ8SMVWZoGIjk1h9z6VBBgB" +
                "n6+T+W7Tngy+AE9giIL4kh1Dvjx9le+a7dOpXRffcOrL9inxPz5S5grxMkaneP4PwCj9ED2D7fehj4Z/" +
                "BEYlODotkTczG/hmWlccXVp09qJJDDcVPaq8hk+vTZ6zuVlXQsgxDDjJYz3WCRx9PIL3dkRCLOtCwZT+" +
                "aX0MH/Fq5HSenXSMIYtrkKRWId4cDU4fAXv61xo4y5vpvo6eVOBvefUJhNFcANeDfJUgsEEFyBwpI3et" +
                "qhKBnVPgqMv34+j7Qsjby+eRiOh8Sn/jsNoxinxGrBJTcXiXEurd+H8QegNCfR2xTyUkifG36CgmN8ax" +
                "JJkNMktcc3RxHfzN9Q63w3X67NG39WOUX0ieTvL22vglrZcmWQ5xuVZsziEskcvzvBGrcY4f4j2r8Tke" +
                "hI5sXsfUQiV8KHfIbDUuefEGEZwjWfd1bVK/7GWLthN9oeTMBUlT+lSXZTCprIa6KMferihyuIytZiu3" +
                "cKc9kVnYPl1O6UfUbyvrfJQS1cRRmder+Wxu50coH6USd2ZV5jqyRtirVGoAcEaNV8UCfsVfB0KesPu6" +
                "OstMwhUArokChT10V4aDV+wdB1Wn+cKlXomKRirCELN39cFFjVxlIHyoYf60UzZe0ujhfEIPb/Az3j3e" +
                "Hy7GOJ/lVvk/f393T0irUiAVLLJDuxENGJHKFa9g8pD3skVDPLnLNkRnlGzueeUdXdEdvmYNJc19twcr" +
                "/O9cVj4ccoAh9PYqAHjHm+sApVB6dkVhV3RyPc5JNtfFwi9R0mebCaQYi/FKXDAxJccq5sc0Rgl+T5qh" +
                "Rv5yT5D/FV1sLqiy65OV+oVBI9oIqvoY3BM13MCjR6FhaIEF0+WNtAwIItZvbYU7LaTgP3zZENbblI8q" +
                "XMl1I/3gtyElOxsyhEaRb1GCwhm4+4Qa5xbOPDwhaJBCG/jIc8HQ8MYfv3rjaCvc4XQLHQPrH4tpE7zf" +
                "Bgx8iBiA+o+hYqzvIEH+n/O7rL8FVmzxyN7FZWBoIILCXKkTjocuaKRD6igIMx5KE8uUviMbddE0gpYT" +
                "NpRm4G68kwt7uTrHFrAWKuDNvxUS7boc3THe9Y2deC3+5A+H+N+C8G+F+DFj/PhJkDPKsQU2GCNUeSls" +
                "uGZITZZJxRvb/1jBQMZ3XYSR7pvbmz5i3m5g2dsGf876fDDZ3ympQfirHEhdqkcNmF21qIadfSMif7g7" +
                "v7yY4D+6pAFr1QK4VAbdVdtsA9ho8utVQXeQBHKc3R+H8imXJir4jfDFACNU/xEU+yU4rzr0kJCiM3Lg" +
                "ZCoXlBfWj7mbx6cpfdZtc3f7hXXAXiju1l6bs1KkwrWfENl33tD6C5uWtcrXqnFQlNvWMZQhRPoOYBRk" +
                "HrcncRNFS/QeX3iokW8rLpBppw+B9Ak77Gt6i256ia30tkX8XLkwHplrv9Z6CNcQ7H/CbAjFbIdpuvsX" +
                "/Zv+cw+A+e1SZ4TQisjbOqRshngyNjjkNizCQqjPG55YIUzU9EhrmPcK0eR15IT0cTysYvk6NZ3SerjI" +
                "V37sFmVGt7R5CiMCWGKtGBVFcwN1DcLi+Zt7sOWw+P3BsPj7N0Dv26kKTPp/aoDCRLPv/GC0BRtXJb5m" +
                "vwc2Sl9XYQ45CG2DCjiME1FN5x7t5ULKSzg+Yg/mPXykmz61blr7svaBn6CqCYQ4ZqHSTJZchKXc5GgI" +
                "wHUrTgzap+3Uut0BPXcp/dYUksorjVAKkLEzIQKpomk14bQXiUJpjXHVXJI0gx/34IAp9i+4Ip7bamkt" +
                "YApNhJI5NAXYtEDAOnI1EHESi+kjzjHQfM0zqzbkDcruA9qMQzsaScEN/5gH8WebMfxoFKrywbIURrFg" +
                "1Zmqc98mie4gHK3bLXEGrobY5BDhGRVbUVA9fe6cRoVFDT3H5hIBaMGcY3fQndlbadqCTDDJ4x0O7BgE" +
                "IULW8zUmbXYts5iDjcl4EuNNaB7CroECR3XBjPpedZCOuTnsZ3/08f1NzHQSwpHd0L6HiCr9fy9KNGus" +
                "7DuMcN8ig1B2MFWlOYoX3h5v2qLI7dmjhRf3KdbQiMcM0UZn4yeMAUMcvjAoTAM0rkJPhkk6liIurmLb" +
                "xqYJKn+f3bQKxz42xtUzv16++Menny4RodPQ1YUpHUv/Ce6TQlMyZQ7PMRCelxAQLpLrR/RuMhDk2Q3/" +
                "6psy9iuSDjjZ6ALK5yDWTip4mFwXMp3tJ4otgTC7RboY2H23ghP6oRPlAQzG5DfXl2ICnUDHEIpH6Eml" +
                "lVRzN9fUYRoncPB2bU8YxIutmaZU+ZBYb0oom4VVTlLk63DHKchDSVIUoCKUtRm+ujE6SpZCl3iGggcc" +
                "GKQ1CJcBfI+qMmqOTpdRwk1BSsd86Hg8JM2iX+I5UmFb+oFkz+Rb6BY9Yb7WCWJ4ym4OlwfceWd85NAn" +
                "Epmmk4T0Co4uE1xhCiI/hnKZDSm2wTuKMpsYKQE5JXQ9eDvQ/T3RORg77DpGB7j2GSE/e4hwC30NcmGb" +
                "VYcRjC/BcCv6poJpfV4iYO0SYH0hBA2DwXCGw0f6DI6RjphBPWjps3R0a6nFg/ejKONiKY6DNSTRUvnE" +
                "2VC/nwltHWlbooE1hOtfMRlvz8XYwtli+3mIwLwbJA0nSK3gXbkhD0mi52xmNstwdR5i/x3dnAx4JM62" +
                "ipDwvF8v0QjpqtvBjpnX/NCrawt1utgefI87ps2A6a0tn4UnHht+jWVUn1D5OXzGRuEznKNFPp/lc7tB" +
                "YFjVoMkFL89xwFmKpliCC3y6hxVwWw5wbL6uPTzCCOhI0q3I3T7iE1Sjm2nwyFOCqaz3pH5E9ydsO8PG" +
                "RwiwfG9PGjFVZE3BNjIzg7EXuM/mu2fCgz8cASLm1mIuZ+WJMNyCb/1fsD3mMaEfAAA=";
                
    }
}
