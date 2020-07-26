using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.IvizMsgs
{
    [DataContract (Name = "iviz_msgs/Vector2")]
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector2 : IMessage, System.IEquatable<Vector2>
    {
        [DataMember (Name = "x")] public float X { get; set; }
        [DataMember (Name = "y")] public float Y { get; set; }
    
        /// <summary> Explicit constructor. </summary>
        public Vector2(float X, float Y)
        {
            this.X = X;
            this.Y = Y;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Vector2(Buffer b)
        {
            b.Deserialize(out this);
        }
        
        public readonly ISerializable RosDeserialize(Buffer b)
        {
            return new Vector2(b ?? throw new System.ArgumentNullException(nameof(b)));
        }
        
        public override readonly int GetHashCode() => (X, Y).GetHashCode();
        
        public override readonly bool Equals(object o) => o is Vector2 s && Equals(s);
        
        public readonly bool Equals(Vector2 o) => (X, Y) == (o.X, o.Y);
        
        public static bool operator==(in Vector2 a, in Vector2 b) => a.Equals(b);
        
        public static bool operator!=(in Vector2 a, in Vector2 b) => !a.Equals(b);
    
        public readonly void RosSerialize(Buffer b)
        {
            if (b is null) throw new System.ArgumentNullException(nameof(b));
            b.Serialize(this);
        }
        
        public readonly void RosValidate()
        {
        }
    
        public readonly int RosMessageLength => 8;
    
        public readonly string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "iviz_msgs/Vector2";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "ff8d7d66dd3e4b731ef14a45d38888b6";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAE0vLyU8sMTZSqOBKg7IqubgAEeFgKBUAAAA=";
                
    }
}
