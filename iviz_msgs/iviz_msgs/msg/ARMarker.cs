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
        [DataMember (Name = "corners")] public GeometryMsgs.Vector3[/*4*/] Corners; // pixel position with z = 0
        [DataMember (Name = "camera_intrinsic")] public double[/*9*/] CameraIntrinsic; // row major intrinsic
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
        
        public ISerializable RosDeserialize(ref Buffer b) => new ARMarker(ref b);
        
        ARMarker IDeserializable<ARMarker>.RosDeserialize(ref Buffer b) => new ARMarker(ref b);
    
        public void RosSerialize(ref Buffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Type);
            b.Serialize(Code);
            b.SerializeStructArray(Corners, 4);
            b.SerializeStructArray(CameraIntrinsic, 9);
            b.Serialize(ref CameraPose);
            b.Serialize(HasExtrinsicPose);
            b.Serialize(MarkerSizeInMm);
            b.Serialize(ref PoseRelativeToCamera);
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
                "H4sIAAAAAAAACr1UTW/bRhA9e3/FAD7ELmS1TYIANZBDEKetD0WcxClQBAWxIkfkNuQuM7uUTP/6vllK" +
                "VBUYbQ+tBQIid+e9+XozqzEx3f5286Z49f7j67f0kr4zq/ns3fvXb6/e4PB7Y35mW7FQk/8mmzT2bGIS" +
                "52sqQ8Wm5tBxkrHoYh2//ZXLFOTZp+e/41Y8Szw5OaXe3XFLfYguueBp61JD99nvug02vXj+6QfY247F" +
                "Fs4reXTliSIlbKmzfwSh+fwrjzch8h4LD2zMKoSWGhsLvttBpoudL/DJZ5YiunuGt6Lr4OkhUgUVwq1N" +
                "bsNFCsXk5cSYl//xz/zy4adLiqmavE9lN6f0IVlfWakIodnKJktrVKJxdcNy0fIGRY3Jdj1XlG+1OXEJ" +
                "4G3jIuGpGS2wbTvSEGGUArrSdYN3pdVeuo6P8EA6T5Z6K8mVQ2sF9kEq59V8Lchf2fFE/jKwL5mury5h" +
                "4yOXg5YJnpwvhW1UgVxfkRnQuGdPFWBOb7fhAp9cQ1Szc0qNTRos3/XCUeO08RI+vpmSW4IbxWF4qSKd" +
                "5bMCn/Gc4AQhcB/Khs4Q+c2YGugrNUwbK86uWlbiEhUA6xMFPTn/C7OGfUne+rCnnxgPPv4NrZ95NaeL" +
                "Bj1rNfs41CggDHsJG1fBdDVmkrJ17BO1biVWRqOoyaU5/VFrDCOgckfwb2MMpUMDqjw4++HL3Shc9X+p" +
                "8cG53ktLWFuFJBAebfKdKmctjEx6W/JSRXKd2xo8RNGxRcbQ34wEsHICKDbCEqwsDHHzglyiKnAkHxI4" +
                "OvsZlIwaK9r2PcggdLE+6mBqW4JCznhZLxe0bdhPVlqjrOg8A64kcbWrJiQcdTPY0i65BaX1U9S4baeY" +
                "J2doGEgkpAw4X9L1msYw0FYTwovsRi/Qiue4skRSCAudux3F1xsGg4CyxGhrqMnHhKFfmnlJ3c1v4/x2" +
                "/yit1t2HcF8dWjWVKqzzRjzu80IXih5Xu/tpw2MEKIjbY6GGKd+9gXk3QNDiM+/B7nG0nEPZKxljnyzK" +
                "nwdzjh+5YAvmkI/SNX/fnkcJ/1C6h6bxqJ7HwevXl0PddQj+UXD7t60xfwKTtPNmNwgAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
