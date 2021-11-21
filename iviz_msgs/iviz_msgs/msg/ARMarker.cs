/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [Preserve, DataContract (Name = "iviz_msgs/ARMarker")]
    public sealed class ARMarker : IDeserializable<ARMarker>, IMessage
    {
        public const byte TYPE_ARUCO = 0;
        public const byte TYPE_QRCODE = 1;
        [DataMember (Name = "header")] public StdMsgs.Header Header;
        [DataMember (Name = "type")] public byte Type;
        [DataMember (Name = "code")] public string Code;
        [DataMember (Name = "corners")] public GeometryMsgs.Vector3[/*4*/] Corners; // pixel position with z = 0
        [DataMember (Name = "camera_intrinsic")] public double[/*9*/] CameraIntrinsic; // row major intrinsic
        [DataMember (Name = "camera_pose")] public GeometryMsgs.Pose CameraPose;
        [DataMember (Name = "has_extrinsic_pose")] public bool HasExtrinsicPose;
        [DataMember (Name = "marker_size_in_mm")] public double MarkerSizeInMm;
        [DataMember (Name = "pose_relative_to_camera")] public GeometryMsgs.Pose PoseRelativeToCamera;
    
        /// <summary> Constructor for empty message. </summary>
        public ARMarker()
        {
            Code = string.Empty;
            Corners = new GeometryMsgs.Vector3[4];
            CameraIntrinsic = new double[9];
        }
        
        /// <summary> Explicit constructor. </summary>
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
        
        /// <summary> Constructor with buffer. </summary>
        internal ARMarker(ref Buffer b)
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
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new ARMarker(ref b);
        }
        
        ARMarker IDeserializable<ARMarker>.RosDeserialize(ref Buffer b)
        {
            return new ARMarker(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Type);
            b.Serialize(Code);
            b.SerializeStructArray(Corners, 4);
            b.SerializeStructArray(CameraIntrinsic, 9);
            b.Serialize(CameraPose);
            b.Serialize(HasExtrinsicPose);
            b.Serialize(MarkerSizeInMm);
            b.Serialize(PoseRelativeToCamera);
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
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "iviz_msgs/ARMarker";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "6a580236aac5980b59d2bd3a8bd81ca8";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1UTW/bRhA9ewH/hwF8iB3IapsEAWoghyBOWx+KOIlboAgCYkWOyG3IXWZ2JZn+9X2z" +
                "lKgqMNocEgsExI95bz7em10Mienmr+vXxct3f7x6Qy/oR7OY3r199+rN5Wu8/MmY39hWLNTkvzEmDT2b" +
                "mMT5mspQsak5dJxkKLpYxx/+5DIFefrh2Ud8Fc8Sj45OqHe33FIfoksueNq41NBdzrtsg03Pn334GfG2" +
                "Y7GF80oeXXmkSAkb6uzfQWh6/0XG6xB5h0UGNmYRQkuNjQXfbiHjh20u8MknliK6O0a2ouuQ6T5SBRXC" +
                "rU1uzUUKxZjlyBybF9/4d2x+f//rBcVUjfnHwR+bE3qfrK+sVITqbGWTpSWG0bi6YTlveY25xmS7nivK" +
                "X1WfOAfwpnGRcNUMFWzbDrSKCEoBwnTdyrvSqpyu4wM8kM6Tpd5KcuWqtYL4IJXzGr4UjEDZcUX+vGJf" +
                "Ml1dXiDGRy5XOilkcr4UtlE9cnVJZgXtnj5RgDm52YRzPHINX03JKTU2abF82wtHrdPGC+R4PDY3Bzem" +
                "w8hSRTrN7wo8xjNCEpTAfSgbOkXl10NqYLHUMK2tOLtoWYlLTACsjxT06OxfzFr2BXnrw45+ZNzn+Bpa" +
                "P/FqT+cNNGu1+7iqMUAE9hLWrkLoYsgkZevYJ2rdQqwMRlFjSnPyi84YQUBlRfBvYwylgwBV3p3d/mU1" +
                "Cld9P0Peu9zHO3cJq1roAxXSOn9U8yyF0UxvS56rT66yssHDFx1bNA0LTkgAKyeA4lyYg5WF4W+ekUtU" +
                "BY7kQwJHZz+BkjFmRdu+Bxm8LtZHXU9VJijklOf1fEabhv0YpWPKps5r4EoSV7tqRCJRN4EtbbubUVo+" +
                "wZjbdqx5TAbNQCIhZcDZnK6WNIQVbbQh3Mh2+wIteKoruySFMNPV21J8ec5gFzCWGG0NQ/mYsPhzMx1V" +
                "t9PdMN3dPZDaegaq1C/3ao3TCst8NB5KPdNjRV9X2+/jUY9FoCBuh4UhxpZ3AebtCrYWn3n3cQ/l6FzM" +
                "5Gfsf7IQIW/o1ALawXGYqz7o2PynSA/VwX5+967lwVQP69enz/vp6zb8n/Omuw3a+wdl2ButRwgAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
