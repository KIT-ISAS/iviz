/* This file was created automatically, do not edit! */

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [DataContract (Name = "geometry_msgs/Twist")]
    [StructLayout(LayoutKind.Sequential)]
    public struct Twist : IMessage, System.IEquatable<Twist>, IDeserializable<Twist>
    {
        // This expresses velocity in free space broken into its linear and angular parts.
        [DataMember (Name = "linear")] public Vector3 Linear { get; set; }
        [DataMember (Name = "angular")] public Vector3 Angular { get; set; }
    
        /// <summary> Explicit constructor. </summary>
        public Twist(in Vector3 Linear, in Vector3 Angular)
        {
            this.Linear = Linear;
            this.Angular = Angular;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Twist(ref Buffer b)
        {
            b.Deserialize(out this);
        }
        
        public readonly ISerializable RosDeserialize(ref Buffer b)
        {
            return new Twist(ref b);
        }
        
        readonly Twist IDeserializable<Twist>.RosDeserialize(ref Buffer b)
        {
            return new Twist(ref b);
        }
        
        public override readonly int GetHashCode() => (Linear, Angular).GetHashCode();
        
        public override readonly bool Equals(object? o) => o is Twist s && Equals(s);
        
        public readonly bool Equals(Twist o) => (Linear, Angular) == (o.Linear, o.Angular);
        
        public static bool operator==(in Twist a, in Twist b) => a.Equals(b);
        
        public static bool operator!=(in Twist a, in Twist b) => !a.Equals(b);
    
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
        [Preserve] public const string RosMessageType = "geometry_msgs/Twist";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "9f195f881246fdfa2798d1d3eebca84a";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACq1SwUrDQBC9B/YfBnpRCBFUPAiepQdBULzKNJlsl252wuzUNn69s01o9W4gMOy+9+a9" +
                "l6zgfRsy0HEUypkyfFHkNugEIUEvRJBHbAk2wjtKdqgMQTPEkAgFMHX2+n20eUTR3Ljqg1pluYMF8+tg" +
                "QbrKVU///Ljq5e35ETzxQCrT55B9vlkWu2o1pxQqKSmZf7Sg5fJvzAYKdq1gYE5xgoEwKVjmM9WYXRDj" +
                "Bk6NyZJQz0K1tQIdW4GJtYgMuDNRSpkKHcfR1BBUMOWIhVyOjXNFjW9qOGyt3RMqJG/AIuEpkYQWJPjQ" +
                "zVRbNZzZCEvAGrS/hUOIcXY9b9MtFRVhPTGuG1j3MPEeDiWTDQIdqnli2JjJxRluYnHMNeyL9Vnjb62v" +
                "bL+BVZMzerICsxJ29uFd1UdGfbiH42WcLuO3q34AsQC+J28CAAA=";
                
    }
}
