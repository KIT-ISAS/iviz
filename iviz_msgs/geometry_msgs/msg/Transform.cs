/* This file was created automatically, do not edit! */

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = "geometry_msgs/Transform")]
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct Transform : IMessage, System.IEquatable<Transform>, IDeserializable<Transform>
    {
        // This represents the transform between two coordinate frames in free space.
        [DataMember (Name = "translation")] public Vector3 Translation { get; }
        [DataMember (Name = "rotation")] public Quaternion Rotation { get; }
    
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
                "H4sIAAAAAAAACr1SwUrDQBC9B/IPD3pRKBFUPAiepQdBUbzKtpmki8lOnJ0a69c727SNVQ8exD293cx7" +
                "mfdmJnhY+gihTihS0AhdElRciBVLizlpTxSgPWPBLKUPTgmVuJYifDBEhNi5BRV5lmePtFCWs0Ghceo5" +
                "5NndyjgSDENYt495dvXHJ89u7q8vURO3pLJ+amMdT7YN5dnkm1OH183HLzaQamcKK+bQrNGSCwrlkWrM" +
                "0otxzUhhsiRkYdEUXlGy5RJYk0jrnk2UQrREGa7rTM19jiY9G+eIirqYol9a0JsqH2orTBI1BRK/gPja" +
                "l+Nc9myHrcEptDpF75tm6Hr4mw0zqexSPy4wq7DmFfrkyYCgdGo9sU1635mbN6ljnmKVWh80DmO9ZW8C" +
                "tgPR1WQBRiVXbjagatjpxTneRrge4fs/jX1cuR8nH8DiDQ45Hsw/3V7GhU1p/87XDvap+gNfpn8oWAMA" +
                "AA==";
                
        /// Custom iviz code
        public static readonly Transform Identity = (Vector3.Zero, Quaternion.Identity);
        public static implicit operator Pose(in Transform p) => new Pose(p.Translation, p.Rotation);
        public readonly Transform Inverse => new Transform(-(Rotation.Inverse * Translation), Rotation.Inverse);
        public static Transform operator *(in Transform t, in Transform q) =>
                new Transform(t.Translation + t.Rotation * q.Translation, t.Rotation * q.Rotation);
        public static Vector3 operator *(in Transform t, in Vector3 q) => t.Rotation * q + t.Translation;
        public static Quaternion operator *(in Transform t, in Quaternion q) => t.Rotation * q;
        public static Transform RotateAround(in Quaternion q, in Point p) => new Transform(p - q * p, q);
        public static implicit operator Transform((Vector3 translation, Quaternion rotation) p) => new Transform(p.translation, p.rotation);
    }
}
