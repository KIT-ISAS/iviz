/* This file was created automatically, do not edit! */

using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [DataContract]
    [StructLayout(LayoutKind.Sequential)]
    public struct Transform : IMessage, IHasSerializer<Transform>
    {
        // This represents the transform between two coordinate frames in free space.
        [DataMember (Name = "translation")] public Vector3 Translation;
        [DataMember (Name = "rotation")] public Quaternion Rotation;
    
        public Transform(in Vector3 Translation, in Quaternion Rotation)
        {
            this.Translation = Translation;
            this.Rotation = Rotation;
        }
        
        public Transform(ref ReadBuffer b)
        {
            b.Deserialize(out this);
        }
        
        public Transform(ref ReadBuffer2 b)
        {
            b.Deserialize(out this);
        }
        
        public readonly Transform RosDeserialize(ref ReadBuffer b) => new Transform(ref b);
        
        public readonly Transform RosDeserialize(ref ReadBuffer2 b) => new Transform(ref b);
    
        public readonly void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(in this);
        }
        
        public readonly void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(in this);
        }
        
        public readonly void RosValidate()
        {
        }
    
        public const int RosFixedMessageLength = 56;
        
        [IgnoreDataMember] public readonly int RosMessageLength => RosFixedMessageLength;
        
        public const int Ros2FixedMessageLength = 56;
        
        [IgnoreDataMember] public readonly int Ros2MessageLength => Ros2FixedMessageLength;
        
        public readonly int AddRos2MessageLength(int c) => WriteBuffer2.Align8(c) + Ros2FixedMessageLength;
        
    
        public const string MessageType = "geometry_msgs/Transform";
    
        [IgnoreDataMember] public readonly string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "ac9eff44abf714214112b05d54a3cf9b";
    
        [IgnoreDataMember] public readonly string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        [IgnoreDataMember]
        public readonly string RosDependenciesBase64 =>
                "H4sIAAAAAAAAE71SwUrDQBC971cM9KIQIqh4EDxLD4KieJVpMkkXk504OzXGr3e2aROrghcxp5fNvJc3" +
                "7+0CHtY+glAnFCloBF0TqGCIFUsLK9KeKID2DAWzlD6gElSCLUXwwRARxA4Lyp17pEJZzkZ+g+o5uLuN" +
                "ESQYBGEdz9zVHz/u5v76EmrillSGpzbW8WRnxi2+rYjwuv32xT/Y6FLBZjk0A7SEQUF5Zhqx9GJU2yE3" +
                "VRKykCgDr1Cy5RFYTaPFZ5OkECmxsetMDD9nko6NckR5nWfQry3f7ZQPtQ2aQk2BxBcgvvbl3MZERtgt" +
                "l4FWp9D7phk9jz+zCk1kn/ZxDssKBt5AnxYyIFCiYhJa0eQLV03yyxlskvGtxGGgt+yNb71HrMmyi0pY" +
                "WutVw6gX5/A2oWFC7/9S9XzHfmo7AIs3OMZ30Hl6e5kvaAr514X2qHfuA/8+ZJE+AwAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        /// Custom iviz code
        public static Transform Identity => new(Vector3.Zero, Quaternion.Identity);
        public static implicit operator Pose(in Transform p) => p.AsPose();
        public readonly Transform Inverse => new(-(Rotation.Inverse * Translation), Rotation.Inverse);
        public static Transform operator *(in Transform t, in Transform q) =>
                new Transform(t.Translation + t.Rotation * q.Translation, t.Rotation * q.Rotation);
        public static Vector3 operator *(in Transform t, in Vector3 q) => t.Rotation * q + t.Translation;
        public static Point operator *(in Transform t, in Point q) => t.Rotation * q + t.Translation;
        public static Vector3 operator *(in Transform t, in (double X, double Y, double Z) q) => t * (Vector3) q;
        public static Quaternion operator *(in Transform t, in Quaternion q) => t.Rotation * q;
        public static Transform RotateAround(in Quaternion q, in Point p) => new(p - q * p, q);
        public static implicit operator Transform(in (Vector3 translation, Quaternion rotation) p) => new(p.translation, p.rotation);
    
        public Serializer<Transform> CreateSerializer() => new Serializer();
        public Deserializer<Transform> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<Transform>
        {
            public override void RosSerialize(Transform msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(Transform msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(Transform _) => RosFixedMessageLength;
            public override int Ros2MessageLength(Transform _) => Ros2FixedMessageLength;
        }
    
        sealed class Deserializer : Deserializer<Transform>
        {
            public override void RosDeserialize(ref ReadBuffer b, out Transform msg) => msg = new Transform(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out Transform msg) => msg = new Transform(ref b);
        }
    }
}
