/* This file was created automatically, do not edit! */

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [Preserve, DataContract (Name = "geometry_msgs/Point32")]
    [StructLayout(LayoutKind.Sequential)]
    public struct Point32 : IMessage, System.IEquatable<Point32>, IDeserializable<Point32>
    {
        // This contains the position of a point in free space(with 32 bits of precision).
        // It is recommeded to use Point wherever possible instead of Point32.  
        // 
        // This recommendation is to promote interoperability.  
        //
        // This message is designed to take up less space when sending
        // lots of points at once, as in the case of a PointCloud.  
        [DataMember (Name = "x")] public float X;
        [DataMember (Name = "y")] public float Y;
        [DataMember (Name = "z")] public float Z;
    
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
        
        public readonly void Dispose()
        {
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
                "H4sIAAAAAAAACj1QMW7DMAzcDfQPB2RpgSJD8oRO3Tr0A7JF20RlURDppunrSylpAA0UeXe84wGfKysm" +
                "yRY4K2wlFFE2lgyZEfzH2cAZcyWCljDR84VtxfmEkU0bqlSaWJ3ychwOeHe4wluybRQpwgS7Ej660mWl" +
                "St9U2xrlMZFrq1GITahDzqcj4Dr+urm7Uo6hu/KOC5Yqm1gjG1UpVMPIie3aqf/MjVTD4iBFJOUl38xY" +
                "+CLsBcnHt0TNVYb6Ds6Ls5PcgzU/imCQPNErgrZLtCNNwRP1A3XPb0n22HYPc5LgEfDzqK6P6vdp+ANL" +
                "+MezcQEAAA==";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
