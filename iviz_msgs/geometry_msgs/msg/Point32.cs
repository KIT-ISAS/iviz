/* This file was created automatically, do not edit! */

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = "geometry_msgs/Point32")]
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct Point32 : IMessage, System.IEquatable<Point32>, IDeserializable<Point32>
    {
        // This contains the position of a point in free space(with 32 bits of precision).
        // It is recommeded to use Point wherever possible instead of Point32.  
        // 
        // This recommendation is to promote interoperability.  
        //
        // This message is designed to take up less space when sending
        // lots of points at once, as in the case of a PointCloud.  
        [DataMember (Name = "x")] public float X { get; }
        [DataMember (Name = "y")] public float Y { get; }
        [DataMember (Name = "z")] public float Z { get; }
    
        /// <summary> Explicit constructor. </summary>
        public Point32(float X, float Y, float Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }
        
        /// <summary> Constructor with buffer. </summary>
        public Point32(ref Buffer b)
        {
            b.Deserialize(out this);
        }
        
        public readonly ISerializable RosDeserialize(ref Buffer b)
        {
            return new Point32(ref b);
        }
        
        readonly Point32 IDeserializable<Point32>.RosDeserialize(ref Buffer b)
        {
            return new Point32(ref b);
        }
        
        public override readonly int GetHashCode() => (X, Y, Z).GetHashCode();
        
        public override readonly bool Equals(object? o) => o is Point32 s && Equals(s);
        
        public readonly bool Equals(Point32 o) => (X, Y, Z) == (o.X, o.Y, o.Z);
        
        public static bool operator==(in Point32 a, in Point32 b) => a.Equals(b);
        
        public static bool operator!=(in Point32 a, in Point32 b) => !a.Equals(b);
    
        public readonly void RosSerialize(ref Buffer b)
        {
            b.Serialize(this);
        }
        
        public readonly void RosValidate()
        {
        }
    
        /// <summary> Constant size of this message. </summary>
        [Preserve] public const int RosFixedMessageLength = 12;
        
        public readonly int RosMessageLength => RosFixedMessageLength;
    
        public readonly string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "geometry_msgs/Point32";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "cc153912f1453b708d221682bc23d9ac";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAACkVQMW7DMAzcA/gPB2RpgSJD8oRO3Tr0A7JF20Rl0RDppOnrS8lBMwigyLvjHY/4mlkx" +
                "SLbAWWEzYRVlY8mQEcF/nA2cMRYi6BoGermxzbic0bNpRa2FBlanvJ66wxEfjld4T5aFIkWYYFPCZ5O6" +
                "zVToSqXuUe4TubgahViVGuRyPgFVqL7m76GVY2jGvOOSa5FFrNKNiqxUQs+J7b6T/7kLqYbJYYpIylPe" +
                "DVn4Jmwrko/3WNVZhvoWzlOlJ3nEq6YUwSB5oDcErfeopxqCx2pnasbfk2yxre8OY5LgSfDzLO/P8rc7" +
                "/AHZwZnMewEAAA==";
                
    }
}
