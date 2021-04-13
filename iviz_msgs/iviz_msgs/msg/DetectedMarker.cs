/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [Preserve, DataContract (Name = "iviz_msgs/DetectedMarker")]
    public sealed class DetectedMarker : IDeserializable<DetectedMarker>, IMessage
    {
        public const byte MARKER_ARUCO = 0;
        public const byte MARKER_QR = 1;
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "marker_type")] public byte MarkerType { get; set; }
        [DataMember (Name = "aruco_id")] public uint ArucoId { get; set; }
        [DataMember (Name = "qr_code")] public string QrCode { get; set; }
        [DataMember (Name = "corners")] public GeometryMsgs.Vector3[/*4*/] Corners { get; set; }
        [DataMember (Name = "intrinsic")] public double[/*9*/] Intrinsic { get; set; }
        [DataMember (Name = "camera_pose")] public GeometryMsgs.Pose CameraPose { get; set; }
        [DataMember (Name = "marker_size")] public double MarkerSize { get; set; }
        [DataMember (Name = "marker_pose")] public GeometryMsgs.Pose MarkerPose { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public DetectedMarker()
        {
            QrCode = string.Empty;
            Corners = new GeometryMsgs.Vector3[4];
            Intrinsic = new double[9];
        }
        
        /// <summary> Explicit constructor. </summary>
        public DetectedMarker(in StdMsgs.Header Header, byte MarkerType, uint ArucoId, string QrCode, GeometryMsgs.Vector3[] Corners, double[] Intrinsic, in GeometryMsgs.Pose CameraPose, double MarkerSize, in GeometryMsgs.Pose MarkerPose)
        {
            this.Header = Header;
            this.MarkerType = MarkerType;
            this.ArucoId = ArucoId;
            this.QrCode = QrCode;
            this.Corners = Corners;
            this.Intrinsic = Intrinsic;
            this.CameraPose = CameraPose;
            this.MarkerSize = MarkerSize;
            this.MarkerPose = MarkerPose;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public DetectedMarker(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            MarkerType = b.Deserialize<byte>();
            ArucoId = b.Deserialize<uint>();
            QrCode = b.DeserializeString();
            Corners = b.DeserializeStructArray<GeometryMsgs.Vector3>(4);
            Intrinsic = b.DeserializeStructArray<double>(9);
            CameraPose = new GeometryMsgs.Pose(ref b);
            MarkerSize = b.Deserialize<double>();
            MarkerPose = new GeometryMsgs.Pose(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new DetectedMarker(ref b);
        }
        
        DetectedMarker IDeserializable<DetectedMarker>.RosDeserialize(ref Buffer b)
        {
            return new DetectedMarker(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(MarkerType);
            b.Serialize(ArucoId);
            b.Serialize(QrCode);
            b.SerializeStructArray(Corners, 4);
            b.SerializeStructArray(Intrinsic, 9);
            CameraPose.RosSerialize(ref b);
            b.Serialize(MarkerSize);
            MarkerPose.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
        }
        
        public void RosValidate()
        {
            if (QrCode is null) throw new System.NullReferenceException(nameof(QrCode));
            if (Corners is null) throw new System.NullReferenceException(nameof(Corners));
            if (Corners.Length != 4) throw new System.IndexOutOfRangeException();
            if (Intrinsic is null) throw new System.NullReferenceException(nameof(Intrinsic));
            if (Intrinsic.Length != 9) throw new System.IndexOutOfRangeException();
        }
    
        public int RosMessageLength
        {
            get {
                int size = 201;
                size += Header.RosMessageLength;
                size += BuiltIns.UTF8.GetByteCount(QrCode);
                size += 24 * Corners.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "iviz_msgs/DetectedMarker";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "76505d41185dead476238cc5bf5cd971";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71UwW7cNhC96ysG8CF2sN62iVG0BnwwmiY1gqCJ7eQSBIsxNSsRkUh5SHkjf30fqZU2" +
                "axhID60FAaLIeW9m3sywty7+Ru/OL9/+ebk6v/z4x990Rj8X/ffbHy6x90tR/CVcilKdP1uTlvWr6CoO" +
                "neSdly+ItTd+ZcsiRLWuoltdGV9KUYlvJeqwakMVfvokJnp9+fnkCxmvTjQU68Zz/PXk8+9fCEzABmse" +
                "oN77IGS4FeVVh/WEmeII9v6howzZHmcInrP/+CneXb05pRDL0eUoVHFAV5FdyVoS4uGSI9PaQ0Bb1aLH" +
                "jdxJAxC3nZSUT5OMYQngdW0D4a0EynDTDNQHGEUPsdq2d9ZwFIq2lT08kNYRU8carekbVth7La1L5muF" +
                "cIkdb5DbXpwRunh1ChsXxPTRIqABDEaFQyrdxSuaqgpAcXC98cf4lQptMDunWHNMwcq3TiWkODmcwsfz" +
                "MbkluCGOwEsZ6DDvrfAbjghOEIJ03tR0iMjfD7H2DoRCd6yWbxpJxAYKgPVZAj07+o7ZZWrHzk/0I+PO" +
                "x7+hdTNvyum4Rs2alH3oKwgIw079nS1hejNkEtNYcZEae6OsQ5FQo8vi4HXSGEZA5YrgyyF4Y1GAkjY2" +
                "1tNY5GqkMfmfuvHRcZtaSyWVCkkgPLrLZ6lz1irIpGMjy9QkF7ms3qEpWmFkjP6bkQCWVgG13i3BKipo" +
                "blmQjVR6CeR8BEfLX0Ep0DihuetAhkZXdqHhhE3bgBzKslouaFOLG62SRrmj8wxYQ2orW45IOGpnMNM2" +
                "uQXF9Qto3DRjzKMzFAwk6mMGHC3pYk2D72mTEsJCt6Pn6UbmuHKLRO8Xae62FA+vFQwCZAmBK3STCxFD" +
                "vyzmC+nbvBrm1f2TlDpdeAj3fFeqUSq/pnT/7dd5kS6UtF1uz222xQiQVzth0Q1jvpNB8aFHQ6vLvDu7" +
                "p+nlHMrUyRj7yJA/D+YcP3LBLZhD3kv3B+V5kvB30j02jXt67gef/m53uqch+GHDTatNUfwDSEW3gusH" +
                "AAA=";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
