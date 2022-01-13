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
        /// Text code if QR, integer as string if Aruco.
        [DataMember (Name = "code")] public string Code;
        /// Corner pixel positions, with z = 0.
        [DataMember (Name = "corners")] public GeometryMsgs.Vector3[/*4*/] Corners;
        /// Camera intrinsic matrix, row major.
        [DataMember (Name = "camera_intrinsic")] public double[/*9*/] CameraIntrinsic;
        /// Pose of the camera in relation to the frame in the header. Y points up, Z forward.
        [DataMember (Name = "camera_pose")] public GeometryMsgs.Pose CameraPose;
        /// If true, the next two fields use a user-given size. If false, they were estimated using 3d data.
        [DataMember (Name = "has_reliable_pose")] public bool HasReliablePose;
        /// Marker size in mm.
        [DataMember (Name = "marker_size_in_mm")] public double MarkerSizeInMm;
        /// Pose relative to the camera_pose field. Y points up, Z forward.
        [DataMember (Name = "pose_relative_to_camera")] public GeometryMsgs.Pose PoseRelativeToCamera;
    
        /// Constructor for empty message.
        public ARMarker()
        {
            Code = string.Empty;
            Corners = new GeometryMsgs.Vector3[4];
            CameraIntrinsic = new double[9];
        }
        
        /// Explicit constructor.
        public ARMarker(in StdMsgs.Header Header, byte Type, string Code, GeometryMsgs.Vector3[] Corners, double[] CameraIntrinsic, in GeometryMsgs.Pose CameraPose, bool HasReliablePose, double MarkerSizeInMm, in GeometryMsgs.Pose PoseRelativeToCamera)
        {
            this.Header = Header;
            this.Type = Type;
            this.Code = Code;
            this.Corners = Corners;
            this.CameraIntrinsic = CameraIntrinsic;
            this.CameraPose = CameraPose;
            this.HasReliablePose = HasReliablePose;
            this.MarkerSizeInMm = MarkerSizeInMm;
            this.PoseRelativeToCamera = PoseRelativeToCamera;
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
            if (Code is null) throw new System.NullReferenceException(nameof(Code));
            if (Corners is null) throw new System.NullReferenceException(nameof(Corners));
            if (Corners.Length != 4) throw new RosInvalidSizeForFixedArrayException(nameof(Corners), Corners.Length, 4);
            if (CameraIntrinsic is null) throw new System.NullReferenceException(nameof(CameraIntrinsic));
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
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "iviz_msgs/ARMarker";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "c8f6b41386c19105b6644958405c417b";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71VUW/bNhB+nn7FAXloMjjq1hQDGqAPQdJteSiapFmBrhgEWjpLbCVSJSnbyq/fd2Qk" +
                "10Ww7qGNYVgUyfvu7rvvzssxMN2+v3pVnN38df6GXtIv2XLeu745f3PxCpu/ZtmfrCp21MRHuhPGnjMf" +
                "nDY1lbbin+RzQLe8DfGd9IqubxakTeAatsrT/W0cnLmhtHlWs+04uLHofO2fvuMyWHfy4fk/AHCGnRfA" +
                "87ikXm+5pd56HbQ1fkEbHRq6k5jzbNVaFX57/uEFLFXHThXwCl9elzGo87hJ8yZ1CqvtgpzdYP3Ruq9j" +
                "ubKeJyw45QgTN+2KQjOdAZIct0piomDjycrhSA7kJTGW03uEDveehn5Bf9PKuo1yVZ5lS2tbapQvAKPV" +
                "suXJHfxdwpUbeBGRjBAbNpZWmtsKQIhFya87rvWaDXl9x7nYrFTrk9FIG3ZM7INGxlzhuhTgpKJKBTXz" +
                "BgrcJ3aFIIC5ouui+9dxN+JKOl33IEkSbpE4WHMRbJGYmeiaTiZ2vuA0ZfIf3Lz8zp/s9ds/TqHCKoWf" +
                "RJ0d0NugTAWXhNyUMCMxUKPrBuS2vIbwfFBdz4m3KH2fw/C20Z7wrRkSVW07Sj0qSbW0XTcYXSrpFN3x" +
                "nj0swaeiXrmgy6FVDvetq7SR61E/go6v588Dm5Lp8uIUd4znchA24Umb0rGK9by8oGwAgyfPxCA7uN3Y" +
                "46ntZudgXwUJlre9Yy9xKn8KHz+n5HJggxyGF6jrMO4VePVHUICEwL0tGzpE5FdjaGzS91q5qFoBLsEA" +
                "UJ+I0ZOjL5BNhDbK2Ak+Ie58/B9YM+NKTscNatZK9n6oQSAu9s6udYWryzFJrdVsArV66ZQbM7FKLrOD" +
                "32OPBilfrAieyntb6tglMlqm0RarUejqR6nxwRE4ScuxlIqlNxSt45koZ+UYmfSqRL/j6mUsqzUQRccK" +
                "GUN/syUMK+1gigmVAxXzAOLGeNCBKsuejA3A6NQnQDI4FmvV9wCD0J0yfjfdYHLIeZ1j+DYYOPGWcBQV" +
                "HXsAo9XpWlfJEo662VjRfXKYTKtn4LhtU8zJGQoGEGdDNDiKg2y0A20kISzcfetZWvIcV5RIsHYRp2GC" +
                "+HpEoRFAi/eqlinmA5oeo2WafNt5Nc6ru0cptUxHhHu2K1WiCv8vcTju1XkhA0W2q/vz+C9IaAGyTk+2" +
                "UEPKd7qQXQ8QtDMRd3fvcbQcQ5mUjLYPCvTHxpzjRy4qDf/9dL9RnkcJf0fdQ924x+d+8PL2ece7NME3" +
                "BTetNln2L5pGodeVCQAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
