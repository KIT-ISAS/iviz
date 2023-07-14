/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.SensorMsgs
{
    [DataContract]
    public sealed class CameraInfo : IHasSerializer<CameraInfo>, IMessage
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
        /// <summary> Header timestamp should be acquisition time of image </summary>
        [DataMember (Name = "header")] public StdMsgs.Header Header;
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
        /// <summary> 3x3 row-major matrix </summary>
        [DataMember] public double[/*9*/] K;
        // Rectification matrix (stereo cameras only)
        // A rotation matrix aligning the camera coordinate system to the ideal
        // stereo image plane so that epipolar lines in both stereo images are
        // parallel.
        /// <summary> 3x3 row-major matrix </summary>
        [DataMember] public double[/*9*/] R;
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
        /// <summary> 3x4 row-major matrix </summary>
        [DataMember] public double[/*12*/] P;
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
    
        public CameraInfo()
        {
            DistortionModel = "";
            D = EmptyArray<double>.Value;
            K = new double[9];
            R = new double[9];
            P = new double[12];
            Roi = new RegionOfInterest();
        }
        
        public CameraInfo(ref ReadBuffer b)
        {
            Header = new StdMsgs.Header(ref b);
            b.Deserialize(out Height);
            b.Deserialize(out Width);
            DistortionModel = b.DeserializeString();
            {
                int n = b.DeserializeArrayLength();
                double[] array;
                if (n == 0) array = EmptyArray<double>.Value;
                else
                {
                    array = new double[n];
                    b.DeserializeStructArray(array);
                }
                D = array;
            }
            {
                var array = new double[9];
                b.DeserializeStructArray(array);
                K = array;
            }
            {
                var array = new double[9];
                b.DeserializeStructArray(array);
                R = array;
            }
            {
                var array = new double[12];
                b.DeserializeStructArray(array);
                P = array;
            }
            b.Deserialize(out BinningX);
            b.Deserialize(out BinningY);
            Roi = new RegionOfInterest(ref b);
        }
        
        public CameraInfo(ref ReadBuffer2 b)
        {
            Header = new StdMsgs.Header(ref b);
            b.Align4();
            b.Deserialize(out Height);
            b.Deserialize(out Width);
            DistortionModel = b.DeserializeString();
            {
                b.Align4();
                int n = b.DeserializeArrayLength();
                double[] array;
                if (n == 0) array = EmptyArray<double>.Value;
                else
                {
                    array = new double[n];
                    b.Align8();
                    b.DeserializeStructArray(array);
                }
                D = array;
            }
            {
                var array = new double[9];
                b.DeserializeStructArray(array);
                K = array;
            }
            {
                var array = new double[9];
                b.DeserializeStructArray(array);
                R = array;
            }
            {
                var array = new double[12];
                b.DeserializeStructArray(array);
                P = array;
            }
            b.Deserialize(out BinningX);
            b.Deserialize(out BinningY);
            Roi = new RegionOfInterest(ref b);
        }
        
        public CameraInfo RosDeserialize(ref ReadBuffer b) => new CameraInfo(ref b);
        
        public CameraInfo RosDeserialize(ref ReadBuffer2 b) => new CameraInfo(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Height);
            b.Serialize(Width);
            b.Serialize(DistortionModel);
            b.Serialize(D.Length);
            b.SerializeStructArray(D);
            b.SerializeStructArray(K, 9);
            b.SerializeStructArray(R, 9);
            b.SerializeStructArray(P, 12);
            b.Serialize(BinningX);
            b.Serialize(BinningY);
            Roi.RosSerialize(ref b);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Align4();
            b.Serialize(Height);
            b.Serialize(Width);
            b.Serialize(DistortionModel);
            b.Align4();
            b.Serialize(D.Length);
            b.Align8();
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
            BuiltIns.ThrowIfNull(DistortionModel, nameof(DistortionModel));
            BuiltIns.ThrowIfNull(D, nameof(D));
            BuiltIns.ThrowIfNull(K, nameof(K));
            BuiltIns.ThrowIfWrongSize(K, nameof(K), 9);
            BuiltIns.ThrowIfNull(R, nameof(R));
            BuiltIns.ThrowIfWrongSize(R, nameof(R), 9);
            BuiltIns.ThrowIfNull(P, nameof(P));
            BuiltIns.ThrowIfWrongSize(P, nameof(P), 12);
            BuiltIns.ThrowIfNull(Roi, nameof(Roi));
            Roi.RosValidate();
        }
    
        [IgnoreDataMember]
        public int RosMessageLength
        {
            get
            {
                int size = 281;
                size += Header.RosMessageLength;
                size += WriteBuffer.GetStringSize(DistortionModel);
                size += 8 * D.Length;
                return size;
            }
        }
        
        [IgnoreDataMember] public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Header.AddRos2MessageLength(size);
            size = WriteBuffer2.Align4(size);
            size += 4; // Height
            size += 4; // Width
            size = WriteBuffer2.AddLength(size, DistortionModel);
            size = WriteBuffer2.Align4(size);
            size += 4; // D.Length
            size = WriteBuffer2.Align8(size);
            size += 8 * D.Length;
            size += 8 * 9; // K
            size += 8 * 9; // R
            size += 8 * 12; // P
            size += 4; // BinningX
            size += 4; // BinningY
            size += 17; // Roi
            return size;
        }
    
        public const string MessageType = "sensor_msgs/CameraInfo";
    
        [IgnoreDataMember] public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "c9a58c1b0b154e0e6da7578cb991d214";
    
        [IgnoreDataMember] public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        [IgnoreDataMember]
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE8VZbW8buRH+rl8xsD9YyslyHF8L1IU/NBfkauTaM3IG0tYwDGqXK/GyWu4tuZY3v77P" +
                "DMndleSkd0CCCnEkccmZ4bw+Mzqm27VxtNHOqZWmXBem0vzdKzJVYZuN8sZWhE+kKFMb3agFXXtya9uW" +
                "OS01tpGaHMdnVOHN1SrThFPe1iajo/DogekdkapyUllmN7WqjAaFjtoaO6kwjxp0zIYFkZNOqOWXk2Os" +
                "U3jy0KgtnRL/nysIWTR2Q36tE/+8AZlmTrV1zizLjl6rTjekq8zmOh/o0Oh1Shtb2WwNUnpOuXHeNn68" +
                "+SGzJRQQN8uX5/c1OvPPEuUHpjAHmyPlnuhoH3be4l5hb21qXcI02Fd5ZSpH0PFHPHE0jTsam83Jed1o" +
                "+zAszUCGjYfPeZuZaiXKKmwrSxnsDhvs6Fw0Ouga9uqtKyZciFgbDfOBmi3G2q9Vgw+QwZFq2J1c1pgl" +
                "c6hAJIdXmZKUp7X39eXZ2Xa7XTTWLWyzOtuaj+Zs97JnPwjVa2a6p4+Vhlp90yU18GUeDfjBS1unm9Oi" +
                "MbrKYX9TQZyCHdJbvojdwC5kaxBmz3bYHrRiDt0enl5QZ1vaqsrj/Jz0YrWYgwzY/cq2VnSRw9fAhBnZ" +
                "qMnM2iY3lfLazWmrYZXGVisIA/OCvxaVBr671wnXvN7RKeRqq0yVZgmJdT6XZ5CvMbAevZnTuzm9n9PN" +
                "KCRLXXjQ+aQbC93b1uMiFRvHm6wtFfwsK6EejxurjpRz7QbqWcMw7+5e3tPVFb1cvORYrHKT8S3gBTtC" +
                "pEwwmRx/nZeExeHrWtSpst9a44wkIrbOs1vldfzV5GFvM9AK/NvsCzFPphnsjKjBEl2/mfxdqxwJZx3e" +
                "WCaKSx70nFebemSp8dX8mN/k8G6JjrB6MPmIjK1hWVVGKUAjCPgcEduYFVI29oTNh0QyzSHzZSrfPaWD" +
                "wfnh+uyWoL3mSJAvn73Hd93u4dxuq/996NPuIQk3PlGXqtIpD4Xj39ot6YcYCWy2myHnfXu3lBzotGTX" +
                "wjwhEvO24TySPHIQTPK0aehRlS0ieGvKkq3MWmI6jo3PtRvLsfxzovFI0LvJBzmrj/sF/dP6mCrCvcjp" +
                "sjjt2UIS16EIbUJqORodPoLD6d9aOFfZLQ7081nd/ZFXXyPYgSu48qgeZUhhuH7L9Q6es1VNjezNFW7a" +
                "V/JZjHWm4+3lV5GH6HxBf+P02bOJXKasDtNwEhdU9G72ZTqvQGcACIdEQiX4LJGRfmL1Yu+VQvKE6hHX" +
                "HF28CRHmhhDbY7r4umk2hS0QFUqjk6K8NX5N27XJ1mNv3CpHu86IQl2WHZuL6/fYx4sWn+M5qMeWbawf" +
                "VCNuSreYtLjfxStkak5a6dvW5H6dxIoWEz0BPpbiPQv6pa3rYEhZDVinxN4EdByuYZuHjVu5s4HGQ9i9" +
                "WC/oLRDZxjofBQRKOKrLdrN8WNrlEdAgA2pnNnWpI2Okt0blBi7NnuJVtUIc8deRiKccra4tCpNxcV9M" +
                "ADs4IPcleOZ2Q5wAQppPjNxqoBQBeCEv76sCSEVuMZI7IJM/7YHAS5p+PJ/Tx1d4jHeP948Xs8WkKK3y" +
                "f/7+7p7esEDXFQvr0C5Em0XHZOwqLvhcoLpFTBx3xRPRS8qe7rHwjq7oDt+KjrLuPu3AAv8754Wb53x9" +
                "7GgHhR2B8OpN8BwBkT2+6+Ejg2qunqWuVn4NWF48zSHCTAxW426ZqTkjMTuQmGZ4nHUjRfzlniD6MV08" +
                "XVBjt6cb9St7iaiBNfQ+BGGmxnE7DYA/+RGsVXYM+ZEorN/ZibBZVUnUw2uGrJ3qOJC04otG6iE8Q6F1" +
                "NqR/DZRugSWplI4R+ltaxOz4hNifwTIcoizZZ8Z3ff+Fu970Cepsxx0Gc5+INTO834rRb6LRofIT6BXL" +
                "u6aXv3N+5+XXHTdSjxxDDOcC+A96crXOONe5oIjeLadBkNlIkog5hj5q2ifK6KBcg6Er40JsMC6XO9ch" +
                "lJiICYwqSWV92e3Z7ofBXibmyPHP5+4/4M+/16FP2KNPPufS7NMn7NTYccoEnBYQkJuiENgaO/UISCDh" +
                "u5RFpFXmzmRIiLdPsOhtR9yL9Il+frhTkj5zVyVcc60eNTzrKnkxDOw7kffm7vzyYo4/9DcDY5Uctlam" +
                "mfe9MRwZDXm7qegOckCKl/cnAQuV0v6EMGGutY3oPTrDIYjmVYe+r8oH6wZGpnGit7B84oJtFvRBp57s" +
                "9hPfn2NOgitdmetNJMIwjmkcRmro04VJYqzKreoclOR21QtNMI0BwU+DwLN0ENdQtEbr8InHD+Wu0gKV" +
                "OCgIhE85QF/Qa3S/a+yk18nNl8qFOcZS+63WYyeVZP6jQVByVx0dme7+Rf+m/9yfzFOoJeAyRf5Edk0x" +
                "KJshnO0vsgeUIMBKiC+7S95z19IjbWHWK+SOF5EPysPJCIvyVVo6o+1ojS/7mNZkeLa2Ze4kQsVIMfWJ" +
                "ykZ6GpLf+at7sOTk9/0zye9bty4/p8kHzPh/aV3CjHHo1mCpFRtUZb7lGIc71L5twmhwlMMGCBtmfEDD" +
                "pUdHuBKIiCBHlsm8nOinQykoW1+3PrCbh91Mh5MT0GK2ZjiVc3uiwZ6hJw6M+p7dqrn4RvrhsmQqKdGN" +
                "LtgmEjpINarqkhKc9iJOgMaZ3Syl+rKz4xKcF9nuFUPapW3W1ubiiQHyBkCPPStkpiPXwhNOIxg+4jIC" +
                "nbdZKHxMZYSan9FjGKfRVAAz4mEZZH94miFspgFUj5YZ6UTYqQvVlj6Vgf4c4qrfLAkFoYUk5JDF2RnG" +
                "yU59/tg5TSsLFLzE3hqZZjXrgX5/ZH+hC/BqFW8q3aN2nAXb5dZUud3KuOTZhmI2j2lFgH/YNFLctK2Y" +
                "y9Bajiott3PDTI7e/3wdypjkaJQu6yMIkVZ9ECQa0+5U+NByzIPdVqrJS0AS3h0vmTzH7ZsheRR3GNbQ" +
                "lOcB0TQvZ5+xAQzw7GVBYBH84So0UnOmuYjOcBVbLVgkKPvn4jqpGtsmk6uv/Jr845cfL5GD89CJhfEZ" +
                "BP8FsZJDRTLvDb8iIAGvIRsCotSPmgsc3IenK/zUd7WOeoM+uJDoSjeSq9I4gce6bSXD0mHOl87LJJXU" +
                "2Nj7gIypS+PIA5KMB4mXonidQbWPWubYWaOVYLPrN5RcGAdQSe7eW8eNze3WnrL3rnamjT5AUNJPdRNw" +
                "qnJcAl+EWy7A5DIiFMcNBdYe8NXN0AqyLLq2yDVTXOGmQ2qsIohrjFqWUlczRvc5nfChk9mIciWkK1XZ" +
                "RD5QHHj8HrJVT5fvdIpsnZfi0S38nDfG2f9QMGS4TZK8m24ic1VhOTl+G9Av21FMg3dALZsZgXWc+lPX" +
                "nKas38wtRzOC/XBIrpZ+luOfAKKjhfak49HVYcJi+dnTqr47AKUPa2Sn/eOsJiSccfSP5yxrPZrG8tiF" +
                "la8+6irAsBjQDKtDuANnMQaKA1oNMbQgmji+GfaDzs6J1NeMjCA8/0ockSlvpJyy+5uE+HU/6hnPeJLU" +
                "PZRYTFLAPD3YosCteaT8E9oxmcJIRk064Dx8CINoioLUb+BoLNs85mdp63S+2p1CzxLLbsTy1tZfg6O3" +
                "9RcYRr1R+M1APmMfcxkPuSKXD/I5PmfHa1rNfJWMfMDVj6C0uEz/gwEClLOZF+wUm7ujRm2PuJyKyOln" +
                "NXFjNCRdzc1R2YX1gdJbNG863VbMGQb5xyEPRDw4ZaIoi+LNKLzsf4O4Q7HePyM/tuHEYrK0tqTcPoQb" +
                "oe7/F7iUd/8AHwAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<CameraInfo> CreateSerializer() => new Serializer();
        public Deserializer<CameraInfo> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<CameraInfo>
        {
            public override void RosSerialize(CameraInfo msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(CameraInfo msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(CameraInfo msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(CameraInfo msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(CameraInfo msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<CameraInfo>
        {
            public override void RosDeserialize(ref ReadBuffer b, out CameraInfo msg) => msg = new CameraInfo(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out CameraInfo msg) => msg = new CameraInfo(ref b);
        }
    }
}
