/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;
using Iviz.Msgs;
using ISerializable = Iviz.Msgs.ISerializable;

namespace Iviz.Msgs2.SensorMsgs
{
    [DataContract]
    public sealed class CameraInfo : IDeserializableRos2<CameraInfo>, IMessageRos2
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
        // The image dimensions with which the camera was calibrated.
        // Normally this will be the full camera resolution in pixels.
        [DataMember (Name = "height")] public uint Height;
        [DataMember (Name = "width")] public uint Width;
        // The distortion model used. Supported models are listed in
        // sensor_msgs/distortion_models.hpp. For most cameras, "plumb_bob" - a
        // simple model of radial and tangential distortion - is sufficent.
        [DataMember (Name = "distortion_model")] public string DistortionModel;
        // The distortion parameters, size depending on the distortion model.
        // For "plumb_bob", the 5 parameters are: (k1, k2, t1, t2, k3).
        [DataMember (Name = "d")] public double[] D;
        // Intrinsic camera matrix for the raw (distorted) images.
        //     [fx  0 cx]
        // K = [ 0 fy cy]
        //     [ 0  0  1]
        // Projects 3D points in the camera coordinate frame to 2D pixel
        // coordinates using the focal lengths (fx, fy) and principal point
        // (cx, cy).
        /// <summary> 3x3 row-major matrix </summary>
        [DataMember (Name = "k")] public double[/*9*/] K;
        // Rectification matrix (stereo cameras only)
        // A rotation matrix aligning the camera coordinate system to the ideal
        // stereo image plane so that epipolar lines in both stereo images are
        // parallel.
        /// <summary> 3x3 row-major matrix </summary>
        [DataMember (Name = "r")] public double[/*9*/] R;
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
        [DataMember (Name = "p")] public double[/*12*/] P;
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
            DistortionModel = "";
            D = System.Array.Empty<double>();
            K = new double[9];
            R = new double[9];
            P = new double[12];
            Roi = new RegionOfInterest();
        }
        
        /// Constructor with buffer.
        public CameraInfo(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Height);
            b.Deserialize(out Width);
            b.DeserializeString(out DistortionModel);
            b.DeserializeStructArray(out D);
            b.DeserializeStructArray(9, out K);
            b.DeserializeStructArray(9, out R);
            b.DeserializeStructArray(12, out P);
            b.Deserialize(out BinningX);
            b.Deserialize(out BinningY);
            Roi = new RegionOfInterest(ref b);
        }
        
        public CameraInfo RosDeserialize(ref ReadBuffer2 b) => new CameraInfo(ref b);
    
        public void RosSerialize(ref WriteBuffer2 b)
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
            if (DistortionModel is null) BuiltIns.ThrowNullReference();
            if (D is null) BuiltIns.ThrowNullReference();
            if (K is null) BuiltIns.ThrowNullReference();
            if (K.Length != 9) BuiltIns.ThrowInvalidSizeForFixedArray(K.Length, 9);
            if (R is null) BuiltIns.ThrowNullReference();
            if (R.Length != 9) BuiltIns.ThrowInvalidSizeForFixedArray(R.Length, 9);
            if (P is null) BuiltIns.ThrowNullReference();
            if (P.Length != 12) BuiltIns.ThrowInvalidSizeForFixedArray(P.Length, 12);
            if (Roi is null) BuiltIns.ThrowNullReference();
            Roi.RosValidate();
        }
    
        public int RosMessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRosMessageLength(ref int c)
        {
            Header.AddRosMessageLength(ref c);
            WriteBuffer2.AddLength(ref c, Height);
            WriteBuffer2.AddLength(ref c, Width);
            WriteBuffer2.AddLength(ref c, DistortionModel);
            WriteBuffer2.AddLength(ref c, D);
            WriteBuffer2.AddLength(ref c, K, 9);
            WriteBuffer2.AddLength(ref c, R, 9);
            WriteBuffer2.AddLength(ref c, P, 12);
            WriteBuffer2.AddLength(ref c, BinningX);
            WriteBuffer2.AddLength(ref c, BinningY);
            Roi.AddRosMessageLength(ref c);
        }
    
        /// <summary> Full ROS name of this message. </summary>
        public const string MessageType = "sensor_msgs/CameraInfo";
    
        public string RosMessageType => MessageType;
    
        public override string ToString() => Extensions.ToString(this);
    }
}
