/* This file was created automatically, do not edit! */

using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal Transform(ref Buffer b)
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
                "H4sIAAAAAAAACr1Sy0rEQBC8B/YfCvaiECKoeBA8yx4ERfEqs0knO5hMx55eY/x6O5s18QVexJwqk65K" +
                "V9UscbfxEUKtUKSgEbohqLgQS5YGa9KOKEA7Rs4shQ9OCaW4hiJ8MESE2LqcsiS5p1xZTkZ+7dRzSG62" +
                "RpBgEMI6ni2Siz9+FsnV7eU5KuKGVPqHJlbxaL/OIll+c+nwvPv4xQJsdKWwWQ51j4ZcUCjPTCMWXoxq" +
                "NjJTJSHLiVJ4RcEWSWA1jcY9miSFaFkyXNuamPsYy3BslAPKqixFt7GId1M+VDZoChUFEp9DfOWLuZCJ" +
                "7LB3l0LLY3S+rsedx59ZiybyHvhhhlWJnrfoBkMGBIVT24it4Wkvt66HfTnFdlh8J/E50Wv2xrfqo6vI" +
                "sotKrrDiy5qdnp3iZUL9hF7/qe35ov1YeACLNzgm+Kn24e1pvqZDzr95mlBnl/kNSp54Z0UDAAA=";
                
        public override string ToString() => Extensions.ToString(this);
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
