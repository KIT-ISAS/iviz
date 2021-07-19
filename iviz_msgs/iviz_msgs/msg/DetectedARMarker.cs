/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [Preserve, DataContract (Name = "iviz_msgs/DetectedARMarker")]
    public sealed class DetectedARMarker : IDeserializable<DetectedARMarker>, IMessage
    {
        public const byte TYPE_ARUCO = 0;
        public const byte TYPE_QRCODE = 1;
        [DataMember (Name = "header")] public StdMsgs.Header Header { get; set; }
        [DataMember (Name = "type")] public byte Type { get; set; }
        [DataMember (Name = "code")] public string Code { get; set; }
        [DataMember (Name = "corners")] public GeometryMsgs.Vector3[/*4*/] Corners { get; set; } // pixel position with z = 0
        [DataMember (Name = "camera_intrinsic")] public double[/*9*/] CameraIntrinsic { get; set; } // row major intrinsic
        [DataMember (Name = "camera_pose")] public GeometryMsgs.Pose CameraPose { get; set; }
        [DataMember (Name = "has_extrinsic_pose")] public bool HasExtrinsicPose { get; set; }
        [DataMember (Name = "marker_size")] public double MarkerSize { get; set; } // size in mm
        [DataMember (Name = "pose_relative_to_camera")] public GeometryMsgs.Pose PoseRelativeToCamera { get; set; }
    
        /// <summary> Constructor for empty message. </summary>
        public DetectedARMarker()
        {
            Code = string.Empty;
            Corners = new GeometryMsgs.Vector3[4];
            CameraIntrinsic = new double[9];
        }
        
        /// <summary> Explicit constructor. </summary>
        public DetectedARMarker(in StdMsgs.Header Header, byte Type, string Code, GeometryMsgs.Vector3[] Corners, double[] CameraIntrinsic, in GeometryMsgs.Pose CameraPose, bool HasExtrinsicPose, double MarkerSize, in GeometryMsgs.Pose PoseRelativeToCamera)
        {
            this.Header = Header;
            this.Type = Type;
            this.Code = Code;
            this.Corners = Corners;
            this.CameraIntrinsic = CameraIntrinsic;
            this.CameraPose = CameraPose;
            this.HasExtrinsicPose = HasExtrinsicPose;
            this.MarkerSize = MarkerSize;
            this.PoseRelativeToCamera = PoseRelativeToCamera;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public DetectedARMarker(ref Buffer b)
        {
            Header = new StdMsgs.Header(ref b);
            Type = b.Deserialize<byte>();
            Code = b.DeserializeString();
            Corners = b.DeserializeStructArray<GeometryMsgs.Vector3>(4);
            CameraIntrinsic = b.DeserializeStructArray<double>(9);
            CameraPose = new GeometryMsgs.Pose(ref b);
            HasExtrinsicPose = b.Deserialize<bool>();
            MarkerSize = b.Deserialize<double>();
            PoseRelativeToCamera = new GeometryMsgs.Pose(ref b);
        }
        
        public ISerializable RosDeserialize(ref Buffer b)
        {
            return new DetectedARMarker(ref b);
        }
        
        DetectedARMarker IDeserializable<DetectedARMarker>.RosDeserialize(ref Buffer b)
        {
            return new DetectedARMarker(ref b);
        }
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Type);
            b.Serialize(Code);
            b.SerializeStructArray(Corners, 4);
            b.SerializeStructArray(CameraIntrinsic, 9);
            CameraPose.RosSerialize(ref b);
            b.Serialize(HasExtrinsicPose);
            b.Serialize(MarkerSize);
            PoseRelativeToCamera.RosSerialize(ref b);
        }
        
        public void Dispose()
        {
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
                size += BuiltIns.UTF8.GetByteCount(Code);
                size += 24 * Corners.Length;
                return size;
            }
        }
    
        public string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "iviz_msgs/DetectedARMarker";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "ef60ffeab928ad1f2483214bd7ea5f4a";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71UTW/bRhA9a3/FAD7ELmSlTYIANZBDEKetD0WcxAlQBAUxIkfkJuQus7uULP/6vllK" +
                "VBQYSA+tBQJaLue9+Xozy20Suvnr+nXx8t2HV2/oBf1sltPd23ev3ly+xuUvxvwhXEmgJv+NNmnbi4kp" +
                "WFdT6SsxtfhOUtgWXazj449SJh+efnr2N74GJyHOZifU21tpqffRJusdbWxq6C77XbWe0/Nnn36FPXcS" +
                "uLBOyaMtZ4oMfkMdf/aBpvvvPF77KHssPIgxS+9bajgWcruDjB92vsAXvkgoor2TWXaiJ/BT193Hrdgi" +
                "SMvJrqVIvhidzYx58R//zJ/vf7+gmKrR+1h9c0LvE7uKQ0UIjStOTCsUpLF1I+G8lTVqGxN3vVSUv2qP" +
                "4gLAm8ZGwlMLOsFtu6Uhwih5NKfrBmdL1pbaTo7wQKIaTD2HZMuh5QB7Hyrr1HwVkL+y44nydRBXCl1d" +
                "XsDGRSkHLRM8WVcG4ag6ubokM6B/T58owJzcbPw5XqWGtibnlBpOGqzc9kGixsnxAj5+GpNbgBvFEXip" +
                "Ip3muwKv8Qz90xCk92VDp4j8epsayCw1QmsOlpetKHGJCoD1kYIenX3D7DK1Y+f39CPjwce/oXUTr+Z0" +
                "3qBnrWYfhxoFhGEf/NpWMF1uM0nZWnGJWrsMHLZGUaNLc/Kb1hhGQOWO4J9j9KVFA6o8P/sZzN0obPV/" +
                "qfHe8d5LK4i2CkkgPFrnb6qcVRBk0nMpCxXJVW6rdxBFJ4yMob8JCWBlA6BYDAuwShCIW+ZkE1VeIjmf" +
                "wNHxF1AKaqxo7nuQQeiBXdTB1LZ4hZzKol7MadOIG620RlnReQZsScHWthqRcNRNYKZdcnNKqyeocduO" +
                "MY/O0DCj6yhlwNmCrla09QNtNCEcwm70PC1liitLJHk/17nbUXy/YTAIKEuMXOsOiglDvzDTrrqdTtvp" +
                "dPcgrdbdh3BfHlo1lsqv8kY87vNcF4peV7vv46LHCJAPdo+FGsZ89wbm7QBBB5d5D3YPo+Ucyl7JGPvE" +
                "KH8ezCl+5IItmEM+SvcH7XmQ8A+lu28aj+p5HLy+fT3UXYfgh4LbnzbG/AOGqsO8PggAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
