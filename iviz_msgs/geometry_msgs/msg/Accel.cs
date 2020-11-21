/* This file was created automatically, do not edit! */

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [DataContract (Name = "geometry_msgs/Accel")]
    [StructLayout(LayoutKind.Sequential)]
    public struct Accel : IMessage, System.IEquatable<Accel>, IDeserializable<Accel>
    {
        // This expresses acceleration in free space broken into its linear and angular parts.
        [DataMember (Name = "linear")] public Vector3 Linear { get; set; }
        [DataMember (Name = "angular")] public Vector3 Angular { get; set; }
    
        /// <summary> Explicit constructor. </summary>
        public Accel(in Vector3 Linear, in Vector3 Angular)
        {
            this.Linear = Linear;
            this.Angular = Angular;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Accel(ref Buffer b)
        {
            b.Deserialize(out this);
        }
        
        public readonly ISerializable RosDeserialize(ref Buffer b)
        {
            return new Accel(ref b);
        }
        
        readonly Accel IDeserializable<Accel>.RosDeserialize(ref Buffer b)
        {
            return new Accel(ref b);
        }
        
        public override readonly int GetHashCode() => (Linear, Angular).GetHashCode();
        
        public override readonly bool Equals(object? o) => o is Accel s && Equals(s);
        
        public readonly bool Equals(Accel o) => (Linear, Angular) == (o.Linear, o.Angular);
        
        public static bool operator==(in Accel a, in Accel b) => a.Equals(b);
        
        public static bool operator!=(in Accel a, in Accel b) => !a.Equals(b);
    
        public readonly void RosSerialize(ref Buffer b)
        {
            b.Serialize(this);
        }
        
        public readonly void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        public const int RosFixedMessageLength = 48;
        
        public readonly int RosMessageLength => RosFixedMessageLength;
    
        public readonly string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "geometry_msgs/Accel";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "9f195f881246fdfa2798d1d3eebca84a";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACq1SwUrDQBS8B/IPA70olAgqHgTP0oMgKF7lNXlJl252w9tX2/j1vk1Cq3cDgcfuzLyZ" +
                "SVZ437kEPg3CKXEC1TV7FlIXA1xAK8xIA9WMrcQ950ONcJrgXWASUGjs7Q7e5oFEU1UWH1xrlDssmF8H" +
                "C7IsyuLpn5+yeHl7fkTHsWeV8bNPXbpZFpfFak4qnJNyMP+Er+nyb8wKGbtRGDgGP6JnCgrLfKYas3Fi" +
                "XOuoMlkWbqPw2lpBE63EEDWL9LQ3UQ6JM52GwdQIKhSSnwuemsQVV121xnFn7U4oFzoDZomOA4urIa5z" +
                "zUy1Vf2ZTVgCrqHtLY7O+9n1vE13nFUk6sS4rrBpMcYDjjmTDYKG1DxFbM3k4oy2PjuOaxyy9Vnjb62v" +
                "0X4DqyYl6tgKTMrU2Icvi9ZH0od7nC7jeBm/y+IHZO64V3MCAAA=";
                
    }
}
