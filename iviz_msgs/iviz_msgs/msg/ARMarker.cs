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
        [DataMember (Name = "code")] public string Code;
        /// pixel position with z = 0
        [DataMember (Name = "corners")] public GeometryMsgs.Vector3[/*4*/] Corners;
        /// row major intrinsic
        [DataMember (Name = "camera_intrinsic")] public double[/*9*/] CameraIntrinsic;
        [DataMember (Name = "camera_pose")] public GeometryMsgs.Pose CameraPose;
        [DataMember (Name = "has_extrinsic_pose")] public bool HasExtrinsicPose;
        [DataMember (Name = "marker_size_in_mm")] public double MarkerSizeInMm;
        [DataMember (Name = "pose_relative_to_camera")] public GeometryMsgs.Pose PoseRelativeToCamera;
    
        /// Constructor for empty message.
        public ARMarker()
        {
            Code = string.Empty;
            Corners = new GeometryMsgs.Vector3[4];
            CameraIntrinsic = new double[9];
        }
        
        /// Explicit constructor.
        public ARMarker(in StdMsgs.Header Header, byte Type, string Code, GeometryMsgs.Vector3[] Corners, double[] CameraIntrinsic, in GeometryMsgs.Pose CameraPose, bool HasExtrinsicPose, double MarkerSizeInMm, in GeometryMsgs.Pose PoseRelativeToCamera)
        {
            this.Header = Header;
            this.Type = Type;
            this.Code = Code;
            this.Corners = Corners;
            this.CameraIntrinsic = CameraIntrinsic;
            this.CameraPose = CameraPose;
            this.HasExtrinsicPose = HasExtrinsicPose;
            this.MarkerSizeInMm = MarkerSizeInMm;
            this.PoseRelativeToCamera = PoseRelativeToCamera;
        }
        
        /// Constructor with buffer.
        internal ARMarker(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            Type = b.Deserialize<byte>();
            Code = b.DeserializeString();
            Corners = b.DeserializeStructArray<GeometryMsgs.Vector3>(4);
            CameraIntrinsic = b.DeserializeStructArray<double>(9);
            b.Deserialize(out CameraPose);
            HasExtrinsicPose = b.Deserialize<bool>();
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
            b.Serialize(HasExtrinsicPose);
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
        [Preserve] public const string RosMd5Sum = "6a580236aac5980b59d2bd3a8bd81ca8";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71UTW/bRhA9e3/FAD7ELmS1TYICNZBDEKetD0WcxA1QBAUxIkfkNuQus7uUTP/6vllK" +
                "VBQYSA+tBQIid+e9+XozqzEJ3f5587p4+e6PV2/oBf1gVvPZ23ev3ly9xuGPxvwmXEmgJv9NNmnsxcQU" +
                "rKup9JWYWnwnKYxFF+v4/Qcpkw/PPj7/C7fBSYgnJ6fU2ztpqffRJusdbW1q6D77Xbee00/PP/4Me+4k" +
                "cGGdkkdbnigy+C11/LcPNJ9/5fHGR9lj4UGMWXnfUsOxkLsdZLrY+QJf+CShiPZe4K3oOnh6iFRBRZCW" +
                "k91IkXwxeTkx5sV//DO/v//1kmKqJu9T2c0pvU/sKg4VITSuODGtUYnG1o2Ei1Y2KGpM3PVSUb7V5sQl" +
                "gLeNjYSnFrSA23akIcIoeXSl6wZnS9Ze2k6O8EBaR0w9h2TLoeUAex8q69R8HZC/suOJ8nkQVwpdX13C" +
                "xkUpBy0TPFlXBuGoArm+IjOgcc+eKsCc3m79BT6lhqhm55QaThqs3PVBosbJ8RI+vpuSW4IbxRF4qSKd" +
                "5bMCn/Gc4AQhSO/Lhs4Q+c2YGugrNUIbDpZXrShxiQqA9YmCnpx/wewytWPn9/QT48HHv6F1M6/mdNGg" +
                "Z61mH4caBYRhH/zGVjBdjZmkbK24RK1dBQ6jUdTk0pz+ojWGEVC5I/jnGH1p0YAqD85++HI3Clv9X2p8" +
                "cK730gqirUISCI82+U6Vsw6CTHouZakiuc5t9Q6i6ISRMfQ3IwGsbAAUG2EJVgkCccuCbKLKSyTnEzg6" +
                "/gRKQY0VzX0PMgg9sIs6mNoWr5AzWdbLBW0bcZOV1igrOs+ALSnY2lYTEo66Gcy0S25Baf0UNW7bKebJ" +
                "GRpmdA+lDDhf0vWaRj/QVhPCS9iNnqeVzHFliSTvFzp3O4qvNwwGAWWJkWuoycWEoV+aeUndzW/j/Hb/" +
                "KK3W3YdwXx5aNZXKr/NGPO7zQheKHle7+2nDYwTIB7vHQg1TvnsD83aAoIPLvAe7x9FyDmWvZIx9YpQ/" +
                "D+YcP3LBFswhH6X7jfY8SviH0j00jUf1PA5evz4f6q5D8E3B7d+2xvwDk7TzZjcIAAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
