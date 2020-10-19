/* This file was created automatically, do not edit! */

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [DataContract (Name = "geometry_msgs/Point")]
    [StructLayout(LayoutKind.Sequential)]
    public struct Point : IMessage, System.IEquatable<Point>
    {
        // This contains the position of a point in free space
        [DataMember (Name = "x")] public double X { get; set; }
        [DataMember (Name = "y")] public double Y { get; set; }
        [DataMember (Name = "z")] public double Z { get; set; }
    
        /// <summary> Explicit constructor. </summary>
        public Point(double X, double Y, double Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }
        
        /// <summary> Constructor with buffer. </summary>
        internal Point(ref Buffer b)
        {
            b.Deserialize(out this);
        }
        
        public readonly ISerializable RosDeserialize(ref Buffer b)
        {
            return new Point(ref b);
        }
        
        public override readonly int GetHashCode() => (X, Y, Z).GetHashCode();
        
        public override readonly bool Equals(object o) => o is Point s && Equals(s);
        
        public readonly bool Equals(Point o) => (X, Y, Z) == (o.X, o.Y, o.Z);
        
        public static bool operator==(in Point a, in Point b) => a.Equals(b);
        
        public static bool operator!=(in Point a, in Point b) => !a.Equals(b);
    
        public readonly void RosSerialize(ref Buffer b)
        {
            b.Serialize(this);
        }
        
        public readonly void RosValidate()
        {
        }
    
        public readonly int RosMessageLength => 24;
    
        public readonly string RosType => RosMessageType;
    
        /// <summary> Full ROS name of this message. </summary>
        [Preserve] public const string RosMessageType = "geometry_msgs/Point";
    
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        [Preserve] public const string RosMd5Sum = "4a842b65f413084dc2b10fb484ea7f17";
    
        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        [Preserve] public const string RosDependenciesBase64 =
                "H4sIAAAAAAAAEz3HwQmAMAwF0Hum+OAK4iQuEEpCA5KUJgd1ej319t6Gs1uihRebJ6oLRqSVhSMU/M+8" +
                "YA6dIsjBTUiv4Dp23EvP0kv0AQQdt/JVAAAA";
                
        /// Custom iviz code
        public static readonly Point Zero = new Point(0, 0, 0);
        public static readonly Point One = new Point(1, 1, 1);
        public static implicit operator Vector3(in Point p) => new Vector3(p.X, p.Y, p.Z);
        public static Point operator +(in Point v, in Point w) => new Point(v.X + w.X, v.Y + w.Y, v.Z + w.Z);
        public static Point operator -(in Point v, in Point w) => new Point(v.X - w.X, v.Y - w.Y, v.Z - w.Z);
        public static Point operator *(double f, in Point v) => new Point(f * v.X, f * v.Y, f * v.Z);
        public static Point operator *(in Point v, double f) => new Point(f * v.X, f * v.Y, f * v.Z);
        public static Point operator /(in Point v, double f) => new Point(v.X / f, v.Y / f, v.Z / f);
        public static Point operator -(in Point v) => new Point(-v.X, -v.Y, -v.Z);
    }
}
