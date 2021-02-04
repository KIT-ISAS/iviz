/* This file was created automatically, do not edit! */

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = "geometry_msgs/Transform")]
    [StructLayout(LayoutKind.Sequential)]
    public struct Transform : IMessage, System.IEquatable<Transform>, IDeserializable<Transform>
    {
        // This represents the transform between two coordinate frames in free space.
        [DataMember (Name = "translation")] public Vector3 Translation;
        [DataMember (Name = "rotation")] public Quaternion Rotation;
    
        /// <summary> Explicit constructor. </summary>
        public Transform(in Vector3 Translation, in Quaternion Rotation)
        {
            this.Translation = Translation;
            this.Rotation = Rotation;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Transform(ref Buffer b)
        {
            b.Deserialize(out this);
        }
        
        public readonly ISerializable RosDeserialize(ref Buffer b)
        {
            return new Transform(ref b);
        }
        
        readonly Transform IDeserializable<Transform>.RosDeserialize(ref Buffer b)
        {
            return new Transform(ref b);
        }
        
        public override readonly int GetHashCode() => (Translation, Rotation).GetHashCode();
        
        public override readonly bool Equals(object? o) => o is Transform s && Equals(s);
        
        public readonly bool Equals(Transform o) => (Translation, Rotation) == (o.Translation, o.Rotation);
        
        public static bool operator==(in Transform a, in Transform b) => a.Equals(b);
        
        public static bool operator!=(in Transform a, in Transform b) => !a.Equals(b);
    
        public readonly void RosSerialize(ref Buffer b)
        {
            b.Serialize(this);
        }
        
        public readonly void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        [Preserve] public const int RosFixedMessageLength = 56;
        
        public readonly int RosMessageLength => RosFixedMessageLength;
    
        public readonly string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "geometry_msgs/Transform";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "ac9eff44abf714214112b05d54a3cf9b";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE71SwUrDQBC971cM9KIQIqh4EDxLD4KieJVpMkkXk504OzXGr3e2aROrghcxp5fNvJc3" +
                "7+0CHtY+glAnFCloBF0TqGCIFUsLK9KeKID2DAWzlD6gElSCLUXwwRARxA4Lyp17pEJZzkZ+g+o5uLuN" +
                "ESQYBGEdz9zVHz/u5v76EmrillSGpzbW8WRnxi2+rYjwuv32xT/Y6FLBZjk0A7SEQUF5Zhqx9GJU2yE3" +
                "VRKykCgDr1Cy5RFYTaPFZ5OkECmxsetMDD9nko6NckR5nWfQry3f7ZQPtQ2aQk2BxBcgvvbl3MZERtgt" +
                "l4FWp9D7phk9jz+zCk1kn/ZxDssKBt5AnxYyIFCiYhJa0eQLV03yyxlskvGtxGGgt+yNb71HrMmyi0pY" +
                "WutVw6gX5/A2oWFC7/9S9XzHfmo7AIs3OMZ30Hl6e5kvaAr514X2qHfuA/8+ZJE+AwAA";
                
        /// Custom iviz code
        public static readonly Transform Identity = (Vector3.Zero, Quaternion.Identity);
        public static implicit operator Pose(in Transform p) => new Pose(p.Translation, p.Rotation);
        public readonly Transform Inverse => new Transform(-(Rotation.Inverse * Translation), Rotation.Inverse);
        public static Transform operator *(in Transform t, in Transform q) =>
                new Transform(t.Translation + t.Rotation * q.Translation, t.Rotation * q.Rotation);
        public static Vector3 operator *(in Transform t, in Vector3 q) => t.Rotation * q + t.Translation;
        public static Point operator *(in Transform t, in Point q) => t.Rotation * q + t.Translation;
        public static Vector3 operator *(in Transform t, in (double X, double Y, double Z) q) => t * (Vector3) q;
        public static Quaternion operator *(in Transform t, in Quaternion q) => t.Rotation * q;
        public static Transform RotateAround(in Quaternion q, in Point p) => new Transform(p - q * p, q);
        public static implicit operator Transform(in (Vector3 translation, Quaternion rotation) p) => new Transform(p.translation, p.rotation);
    }
}
