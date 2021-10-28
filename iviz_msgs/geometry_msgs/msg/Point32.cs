/* This file was created automatically, do not edit! */

using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal Point32(ref Buffer b)
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
                "H4sIAAAAAAAAEz1QO27DMAzdfYoHZGmBIkNyhE7dOvQCskXbRGVREOmk6elLKWkADRTJ9+MBXysrJskW" +
                "OCtsJRRRNpYMmRH8x9nAGXMlgpYw0cuVbcX5hJFN21apNLE65PU4HPDh6wpvybZRpAgT7Er47EzXlSpd" +
                "qDYZ5TGRc6tRiI2or5xPR8B5/HVzD6YcQ3flHScsVTaxBjaqUqiGkRPbrUP/kRuphoUaJJLyku9mLHwT" +
                "9oLk43ui5ipDXYPz4ugkj2DNjyIYJE/0hqDtEu1IU/BE/UDd83uSPTbtYU4SPAJ+ntXtWf0Of3eAjDBw" +
                "AQAA";
                
        public override string ToString() => Extensions.ToString(this);
    }
}
