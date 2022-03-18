/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    public sealed class ARMarker : IDeserializable<ARMarker>, IMessage
    {
        public const byte TYPE_ARUCO = 0;
        public const byte TYPE_QRCODE = 1;
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "type")] public byte Type;
        /// <summary> Text code if QR, integer as string if Aruco. </summary>
        [DataMember (Name = "code")] public string Code;
        /// <summary> Corner pixel positions, with z = 0. </summary>
        [DataMember (Name = "corners")] public GeometryMsgs.Vector3[/*4*/] Corners;
        /// <summary> Camera intrinsic matrix, row major. </summary>
        [DataMember (Name = "camera_intrinsic")] public double[/*9*/] CameraIntrinsic;
        /// <summary> Pose of the camera in relation to the frame in the header. Y points down, Z forward. </summary>
        [DataMember (Name = "camera_pose")] public GeometryMsgs.Pose CameraPose;
        /// <summary> If true, the next two fields use a user-given size. If false, they were estimated using 3d data. </summary>
        [DataMember (Name = "has_reliable_pose")] public bool HasReliablePose;
        /// <summary> Marker size in mm. </summary>
        [DataMember (Name = "marker_size_in_mm")] public double MarkerSizeInMm;
        /// <summary> Pose relative to the camera_pose field. Y points down, Z forward. </summary>
        [DataMember (Name = "pose_relative_to_camera")] public GeometryMsgs.Pose PoseRelativeToCamera;
    
        /// Constructor for empty message.
        public ARMarker()
        {
            Code = "";
            Corners = new GeometryMsgs.Vector3[4];
            CameraIntrinsic = new double[9];
        }
        
        /// Constructor with buffer.
        public ARMarker(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Type = b.Deserialize<byte>();
            Code = b.DeserializeString();
            Corners = b.DeserializeStructArray<GeometryMsgs.Vector3>(4);
            CameraIntrinsic = b.DeserializeStructArray<double>(9);
            b.Deserialize(out CameraPose);
            HasReliablePose = b.Deserialize<bool>();
            MarkerSizeInMm = b.Deserialize<double>();
            b.Deserialize(out PoseRelativeToCamera);
        }
        
        ISerializable ISerializable.RosDeserializeBase(ref ReadBuffer b) => new ARMarker(ref b);
        
        public ARMarker RosDeserialize(ref ReadBuffer b) => new ARMarker(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Type);
            b.Serialize(Code);
            b.SerializeStructArray(Corners, 4);
            b.SerializeStructArray(CameraIntrinsic, 9);
            b.Serialize(in CameraPose);
            b.Serialize(HasReliablePose);
            b.Serialize(MarkerSizeInMm);
            b.Serialize(in PoseRelativeToCamera);
        }
        
        public void RosValidate()
        {
            if (Code is null) BuiltIns.ThrowNullReference(nameof(Code));
            if (Corners is null) BuiltIns.ThrowNullReference(nameof(Corners));
            if (Corners.Length != 4) throw new RosInvalidSizeForFixedArrayException(nameof(Corners), Corners.Length, 4);
            if (CameraIntrinsic is null) BuiltIns.ThrowNullReference(nameof(CameraIntrinsic));
            if (CameraIntrinsic.Length != 9) throw new RosInvalidSizeForFixedArrayException(nameof(CameraIntrinsic), CameraIntrinsic.Length, 9);
        }
    
        public int RosMessageLength
        {
            get {
                int size = 198;
                size += Header.RosMessageLength;
                size += BuiltIns.GetStringSize(Code);
                size += 24 * Corners.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "iviz_msgs/ARMarker";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "c8f6b41386c19105b6644958405c417b";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71VUW/bNhB+nn7FAXloMjjq1hQDGqAPQdJteSiapFmBrhgEWjpLbCVSJSnbyq/fd2Qk" +
                "10Ww7qGNYVgUyfvu7rvvzssxMN2+v3pVnN38df6GXtIv2XLeu745f3PxCpu/ZtmfrCp21MRHuhPGnjMf" +
                "nDY1lbbin+RzQLe8DfGd9IqubxakTeAatsrT/W0cnLmhtHlWs+04uLHofO2fvuMyWHfy4fk/AHCGnRfA" +
                "87ikXm+5pd56HbQ1fkEbHRq6k5jzbNVaFX57/uEFLFXHThXwCl9elzGo87hJ8yZ1CqvtgpzdYP3Ruq9j" +
                "ubKeJyw45QgTN+2KQjOdAZIct0piomDjycrhSA7kJTGW03uEDveeKrsxC/qbVtZtlKvyLFta21KjfAEg" +
                "rZYtTw7h8RLO3MCLiGWE2rCxtNLcVp4GRKPk1x3Xes2GvL7jXGxWqvXJaKQNOyb2QSNnrnBdSnBSUaWC" +
                "mpkDCe4Tu0IQwF3RddH967gbcSWhrnuQJgm3SCysuQi2SNxMhE0nEz9fsJoy+U92Xn7nT/b67R+nUGKV" +
                "EkjCzg7obVCmgktCdkq4kRio0XUDelteQ3w+qK7nxFyUv89heNtoT/jWDJmqth2lIpUkW9quG4wulXSL" +
                "7njPHpZgVFGvXNDl0CqH+9ZV2sj1qCFBx9fz54FNyXR5cYo7xnM5CJ/wpE3pWMWKXl5QNoDDk2dikB3c" +
                "buzx1Hqzc/CvggTL296xlziVP4WPn1NyObBBDsML9HUY9wq8+iNoQELg3pYNHSLyqzE0Nml8rVzUrQCX" +
                "YACoT8ToydEXyCZCG2XsBJ8Qdz7+D6yZcSWn4wY1ayV7P9QgEBd7Z9e6wtXlmMTWajaBWr10yo2ZWCWX" +
                "2cHvsU+DlC9WBE/lvS117BMZL9N4i9UodPWj1PjgGJyk5VhKxdIditbxTJSzcoxMelWi43H1MpbVGoii" +
                "Y4WMob/ZEoaVdjDFlMqBiokAcWNA6ICOY0/GBmB06hMgGRyLtep7gEHoThm/m3AwOeS8zjGAG4yceEs4" +
                "ioqOPYDx6nStq2QJR91srOg+Ocym1TNw3LYp5uQMBQOIsyEaHMVRNtqBNpIQFu6+9SwteY4rSiRYu4jz" +
                "MEF8PaTQCKDFe1XLHPMBTY/RMs2+7bwa59Xdo5Ra5iPCPduVKlGF/5g4HvfqvJCBItvV/Xn8JyS0AFmn" +
                "J1uoIeU7XciuBwjamYi7u/c4Wo6hTEpG2wcF+mNjzvEjF5XG/3663yjPo4S/o+6hbtzjcz94efu8412a" +
                "4JuCm1abLPsXXcfcmJkJAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
