/* This file was created automatically, do not edit! */

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
    public sealed class XRMarker : IHasSerializer<XRMarker>, IMessage
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
    
        public XRMarker()
        {
            Code = "";
            Corners = new GeometryMsgs.Vector3[4];
            CameraIntrinsic = new double[9];
        }
        
        public XRMarker(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Type);
            b.DeserializeString(out Code);
            unsafe
            {
                var array = new GeometryMsgs.Vector3[4];
                b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), 4 * 24);
                Corners = array;
            }
            unsafe
            {
                var array = new double[9];
                b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), 9 * 8);
                CameraIntrinsic = array;
            }
            b.Deserialize(out CameraPose);
            b.Deserialize(out HasReliablePose);
            b.Deserialize(out MarkerSizeInMm);
            b.Deserialize(out PoseRelativeToCamera);
        }
        
        public XRMarker(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Type);
            b.Align4();
            b.DeserializeString(out Code);
            unsafe
            {
                b.Align8();
                var array = new GeometryMsgs.Vector3[4];
                b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), 4 * 24);
                Corners = array;
            }
            unsafe
            {
                var array = new double[9];
                b.DeserializeStructArray(Unsafe.AsPointer(ref array[0]), 9 * 8);
                CameraIntrinsic = array;
            }
            b.Deserialize(out CameraPose);
            b.Deserialize(out HasReliablePose);
            b.Align8();
            b.Deserialize(out MarkerSizeInMm);
            b.Deserialize(out PoseRelativeToCamera);
        }
        
        public XRMarker RosDeserialize(ref ReadBuffer b) => new XRMarker(ref b);
        
        public XRMarker RosDeserialize(ref ReadBuffer2 b) => new XRMarker(ref b);
    
        public void RosSerialize(ref WriteBuffer b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Type);
            b.Serialize(Code);
            b.SerializeStructArray(Corners);
            b.SerializeStructArray(CameraIntrinsic, 9);
            b.Serialize(in CameraPose);
            b.Serialize(HasReliablePose);
            b.Serialize(MarkerSizeInMm);
            b.Serialize(in PoseRelativeToCamera);
        }
        
        public void RosSerialize(ref WriteBuffer2 b)
        {
            Header.RosSerialize(ref b);
            b.Serialize(Type);
            b.Serialize(Code);
            b.SerializeStructArray(Corners);
            b.SerializeStructArray(CameraIntrinsic, 9);
            b.Serialize(in CameraPose);
            b.Serialize(HasReliablePose);
            b.Serialize(MarkerSizeInMm);
            b.Serialize(in PoseRelativeToCamera);
        }
        
        public void RosValidate()
        {
            if (Code is null) BuiltIns.ThrowNullReference();
            if (Corners is null) BuiltIns.ThrowNullReference();
            if (Corners.Length != 4) BuiltIns.ThrowInvalidSizeForFixedArray(Corners.Length, 4);
            if (CameraIntrinsic is null) BuiltIns.ThrowNullReference();
            if (CameraIntrinsic.Length != 9) BuiltIns.ThrowInvalidSizeForFixedArray(CameraIntrinsic.Length, 9);
        }
    
        public int RosMessageLength
        {
            get
            {
                int size = 198;
                size += Header.RosMessageLength;
                size += WriteBuffer.GetStringSize(Code);
                size += 24 * Corners.Length;
                return size;
            }
        }
        
        public int Ros2MessageLength => AddRos2MessageLength(0);
        
        public int AddRos2MessageLength(int c)
        {
            int size = c;
            size = Header.AddRos2MessageLength(size);
            size += 1; // Type
            size = WriteBuffer2.Align4(size);
            size = WriteBuffer2.AddLength(size, Code);
            size = WriteBuffer2.Align8(size);
            size += 24 * 4; // Corners
            size += 8 * 9; // CameraIntrinsic
            size += 56; // CameraPose
            size += 1; // HasReliablePose
            size = WriteBuffer2.Align8(size);
            size += 8; // MarkerSizeInMm
            size += 56; // PoseRelativeToCamera
            return size;
        }
    
        public const string MessageType = "iviz_msgs/XRMarker";
    
        public string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "c8f6b41386c19105b6644958405c417b";
    
        public string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71VUW/bNhB+nn7FAXloMjja2hQDGqAPQdJteSiapFmBLigEWjpLXCVSJSnbyq/fd2Qk" +
                "10Ww7mGLYdgUyfvu7rvvTssxMN1+vHpTnN38cf6OXtPP2XLeu745f3fxBpvPs+x3VhU7auJfuhPGnjMf" +
                "nDY1lbbiH+RzQLe8DfGZ9IqubxakTeAatsrTw20cnLmhtHlWs+04uLHofO1/+sBlsO7k7uUnADjDzgvg" +
                "eVxSr7fcUm+9Dtoav6CNDg3dS8x5tmqtCr+8vHsFS9WxUwW8wpfXZQzqPG7SvEmdwmq7IGc3WP9l3bex" +
                "XFnPExaccoSJm3ZFoZnOAEmOWyUxUbDxZOVwJAfykBjL6SNCh3tPld2YBf1JK+s2ylV5li2tbalRvgCQ" +
                "VsuWJ4fweAlnbuBFxDJCbdhYWmluK08DolHy645rvWZDXt9zLjYr1fpkNNKGHRP7oJEzV7guJTipqFJB" +
                "zcyBBPeZXSEI4K7ouuj+bdyNuJJQ1z1Kk4RbJBbWXARbJG4mwqaTiZ+vWE2Z/CM7r//jT/b2/W+nUGKV" +
                "EkjCzg7ofVCmgktCdkq4kRio0XUDelteQ3w+qK7nxFyUv89heNtoT/jWDJmqth2lIpUkW9quG4wulXSL" +
                "7njPHpZgVFGvXNDl0CqH+9ZV2sj1qCFBx9fzl4FNyXR5cYo7xnM5CJ/wpE3pWMWKXl5QNoDDkxdiQAd0" +
                "d2P980/Zwe3GHk89OEeBQqggUfO2d+wlYOVP4ezHlGUOJ2CJ4Q5CO4x7BR79EcQgsXBvy4YOkcLVGBqb" +
                "xL5WLgpYgEtQAdRnYvTs6CtkE6GNMnaCT4g7H/8G1sy4ktNxg+K1QoMfajCJi72za13h6nJMqms1m0Ct" +
                "Xjrlxkysksvs4NfYsEHqGEuDf+W9LXVsGJkz05yLZSl09X/J8tF5OGnMsZSKpU0UreOZSGjlGJn0qkTr" +
                "4+plLKs1UEfHChlDiLMlDCvtYIpxlQMVowEqx6TQAa3HnowNwOjUZ0AyOBZr1fcAg+KdMn436mByyHmd" +
                "YxI3mD3xlnAUpR2bAXPW6VpXyRKOutlY0UNyGFKrF+C4bVPMyRkKBhBnQzQ4ijNttANtJCEs3EMPWlry" +
                "HFeUSLB2EQdjgvh2WqERQIv3qpaB5gO6HzNmGoLbeTXOq/snKbUMSoR7titVogovmzgn9+q8kMki29XD" +
                "eXwlElqArNOTLdSQ8p0uZNcDBO1MxN3dexotx1AmJaPtgwL9sTHn+JGLSu+B/XS/U54nCX9H3WPduMfn" +
                "fvDy9GXHuzTBdwU3rTZZ9jfFmbkAogkAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    
        public Serializer<XRMarker> CreateSerializer() => new Serializer();
        public Deserializer<XRMarker> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<XRMarker>
        {
            public override void RosSerialize(XRMarker msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(XRMarker msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(XRMarker msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(XRMarker msg) => msg.AddRos2MessageLength(0);
            public override void RosValidate(XRMarker msg) => msg.RosValidate();
        }
    
        sealed class Deserializer : Deserializer<XRMarker>
        {
            public override void RosDeserialize(ref ReadBuffer b, out XRMarker msg) => msg = new XRMarker(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out XRMarker msg) => msg = new XRMarker(ref b);
        }
    }
}
