/* This file was created automatically, do not edit! */

using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = RosMessageType)]
    [StructLayout(LayoutKind.Sequential)]
    public struct Transform : IMessage, System.IEquatable<Transform>, IDeserializable<Transform>
    {
        // This represents the transform between two coordinate frames in free space.
        [DataMember (Name = "translation")] public Vector3 Translation;
        [DataMember (Name = "rotation")] public Quaternion Rotation;
    
        /// Explicit constructor.
        public Transform(in Vector3 Translation, in Quaternion Rotation)
        {
            this.Translation = Translation;
            this.Rotation = Rotation;
        }
        
        /// Constructor with buffer.
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal Transform(ref Buffer b)
        {
            b.Deserialize(out this);
        }
        
        public readonly ISerializable RosDeserialize(ref Buffer b) => new Transform(ref b);
        
        readonly Transform IDeserializable<Transform>.RosDeserialize(ref Buffer b) => new Transform(ref b);
        
        public override readonly int GetHashCode() => (Translation, Rotation).GetHashCode();
        
        public override readonly bool Equals(object? o) => o is Transform s && Equals(s);
        
        public readonly bool Equals(Transform o) => (Translation, Rotation) == (o.Translation, o.Rotation);
        
        public static bool operator==(in Transform a, in Transform b) => a.Equals(b);
        
        public static bool operator!=(in Transform a, in Transform b) => !a.Equals(b);
    
        public void RosSerialize(ref Buffer b)
        {
            b.Serialize(ref this);
        }
        
        public readonly void RosValidate()
        {
        }
    
        /// Constant size of this message.
        [Preserve] public const int RosFixedMessageLength = 56;
        
        public readonly int RosMessageLength => RosFixedMessageLength;
    
        public readonly string RosType => RosMessageType;
    
        /// Full ROS name of this message.
        [Preserve] public const string RosMessageType = "geometry_msgs/Transform";
    
        /// MD5 hash of a compact representation of the message.
        [Preserve] public const string RosMd5Sum = "ac9eff44abf714214112b05d54a3cf9b";
    
        /// Base64 of the GZip'd compression of the concatenated dependencies file.
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACr1SwUrDUBC85ysGelEIEVQ8CJ6lB0FRvMprskkfJm/jvq0xfr2bpk2sCl7EnCYvO5PZ" +
                "mbfAw9pHCLVCkYJG6Jqg4kIsWRqsSDuiAO0YObMUPjgllOIaivDBEBFi63LKkuSRcmU5G/m1U88hudsY" +
                "QYJBCOt4llz98ZPc3F9foiJuSKV/amIVT3ZmksW3FR1et9+++IeNLhU2y6Hu0ZALCuWZacTCi1Fth8xU" +
                "SchCohReUbDlEVhNo3HPJkkhWpAM17Ym5j5nMhwb5YiyKkvRrS3f7ZQPlQ2aQkWBxOcQX/libmMiO+yW" +
                "S6HlKTpf16Pn8WdWoYns0z7OsCzR8wbdsJABQeHUHLHVO/lyq3rwyyk2g/GtxGGgt+yNb71HV5FlF5Vc" +
                "Ya2XNTu9OMfbhPoJvf9L1fMd+6ntABZvcIzvoPPh7WW+oEPIvy60R12SfAD/PmSRPgMAAA==";
                
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
