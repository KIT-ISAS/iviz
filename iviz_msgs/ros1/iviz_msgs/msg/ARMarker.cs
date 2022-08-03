/* This file was created automatically, do not edit! */

using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract]
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
    
        public ARMarker()
        {
            Code = "";
            Corners = new GeometryMsgs.Vector3[4];
            CameraIntrinsic = new double[9];
        }
        
        public ARMarker(ref ReadBuffer b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Type);
            b.DeserializeString(out Code);
            b.DeserializeStructArray(4, out Corners);
            b.DeserializeStructArray(9, out CameraIntrinsic);
            b.Deserialize(out CameraPose);
            b.Deserialize(out HasReliablePose);
            b.Deserialize(out MarkerSizeInMm);
            b.Deserialize(out PoseRelativeToCamera);
        }
        
        public ARMarker(ref ReadBuffer2 b)
        {
            StdMsgs.Header.Deserialize(ref b, out Header);
            b.Deserialize(out Type);
            b.DeserializeString(out Code);
            b.DeserializeStructArray(4, out Corners);
            b.DeserializeStructArray(9, out CameraIntrinsic);
            b.Deserialize(out CameraPose);
            b.Deserialize(out HasReliablePose);
            b.Deserialize(out MarkerSizeInMm);
            b.Deserialize(out PoseRelativeToCamera);
        }
        
        public ARMarker RosDeserialize(ref ReadBuffer b) => new ARMarker(ref b);
        
        public ARMarker RosDeserialize(ref ReadBuffer2 b) => new ARMarker(ref b);
    
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
        
        public void RosSerialize(ref WriteBuffer2 b)
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
            if (Code is null) BuiltIns.ThrowNullReference();
            if (Corners is null) BuiltIns.ThrowNullReference();
            if (Corners.Length != 4) BuiltIns.ThrowInvalidSizeForFixedArray(Corners.Length, 4);
            if (CameraIntrinsic is null) BuiltIns.ThrowNullReference();
            if (CameraIntrinsic.Length != 9) BuiltIns.ThrowInvalidSizeForFixedArray(CameraIntrinsic.Length, 9);
        }
    
        public int RosMessageLength
        {
            get {
                int size = 198;
                size += Header.RosMessageLength;
                size += WriteBuffer.GetStringSize(Code);
                size += 24 * Corners.Length;
                return size;
            }
        }
        
        public int Ros2MessageLength => WriteBuffer2.GetRosMessageLength(this);
        
        public void AddRos2MessageLength(ref int c)
        {
            Header.AddRos2MessageLength(ref c);
            WriteBuffer2.AddLength(ref c, Type);
            WriteBuffer2.AddLength(ref c, Code);
            WriteBuffer2.AddLength(ref c, Corners, 4);
            WriteBuffer2.AddLength(ref c, CameraIntrinsic, 9);
            WriteBuffer2.AddLength(ref c, CameraPose);
            WriteBuffer2.AddLength(ref c, HasReliablePose);
            WriteBuffer2.AddLength(ref c, MarkerSizeInMm);
            WriteBuffer2.AddLength(ref c, PoseRelativeToCamera);
        }
    
        public const string MessageType = "iviz_msgs/ARMarker";
    
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
    }
}
