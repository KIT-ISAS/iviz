/* This file was created automatically, do not edit! */

using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Iviz.Msgs.GeometryMsgs
{
    [DataContract]
    [StructLayout(LayoutKind.Sequential)]
    public struct Point32 : IMessage, IDeserializable<Point32>, IHasSerializer<Point32>
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
    
        public Point32(float X, float Y, float Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Point32(ref ReadBuffer b)
        {
            b.Deserialize(out this);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Point32(ref ReadBuffer2 b)
        {
            b.Deserialize(out this);
        }
        
        public readonly Point32 RosDeserialize(ref ReadBuffer b) => new Point32(ref b);
        
        public readonly Point32 RosDeserialize(ref ReadBuffer2 b) => new Point32(ref b);
    
        public readonly void RosSerialize(ref WriteBuffer b)
        {
            b.Serialize(in this);
        }
        
        public readonly void RosSerialize(ref WriteBuffer2 b)
        {
            b.Serialize(in this);
        }
        
        public readonly void RosValidate()
        {
        }
    
        public const int RosFixedMessageLength = 12;
        
        public readonly int RosMessageLength => RosFixedMessageLength;
        
        public const int Ros2FixedMessageLength = 12;
        
        public readonly int Ros2MessageLength => Ros2FixedMessageLength;
        
        public readonly int AddRos2MessageLength(int c) => WriteBuffer2.Align4(c) + Ros2FixedMessageLength;
        
    
        public const string MessageType = "geometry_msgs/Point32";
    
        public readonly string RosMessageType => MessageType;
    
        /// MD5 hash of a compact representation of the ROS1 message
        public const string Md5Sum = "cc153912f1453b708d221682bc23d9ac";
    
        public readonly string RosMd5Sum => Md5Sum;
    
        /// Base64 of the GZip'd compression of the concatenated ROS1 dependencies file
        public readonly string RosDependenciesBase64 =>
                "H4sIAAAAAAAAEz1QO27DMAzdfYoHZGmBIkNyhE7dOvQCskXbRGVREOmk6elLKWkADRTJ9+MBXysrJskW" +
                "OCtsJRRRNpYMmRH8x9nAGXMlgpYw0cuVbcX5hJFN21apNLE65PU4HPDh6wpvybZRpAgT7Er47EzXlSpd" +
                "qDYZ5TGRc6tRiI2or5xPR8B5/HVzD6YcQ3flHScsVTaxBjaqUqiGkRPbrUP/kRuphoUaJJLyku9mLHwT" +
                "9oLk43ui5ipDXYPz4ugkj2DNjyIYJE/0hqDtEu1IU/BE/UDd83uSPTbtYU4SPAJ+ntXtWf0Of3eAjDBw" +
                "AQAA";
                
        public override string ToString() => Extensions.ToString(this);
    
        /// Custom iviz code
        public static Point32 Zero => new();
        public static Point32 One => new(1, 1, 1);
        public static Point32 UnitX => new(1, 0, 0);
        public static Point32 UnitY => new(0, 1, 0);
        public static Point32 UnitZ => new(0, 0, 1);
        public static implicit operator Point(in Point32 p) => new(p.X, p.Y, p.Z);
        public static implicit operator Vector3(in Point32 p) => new(p.X, p.Y, p.Z);
        public static implicit operator Point32(in Vector3 p) => new((float)p.X, (float)p.Y, (float)p.Z);
        public static Point32 operator +(in Point32 v, in Point32 w) => new(v.X + w.X, v.Y + w.Y, v.Z + w.Z);
        public static Point32 operator -(in Point32 v, in Point32 w) => new(v.X - w.X, v.Y - w.Y, v.Z - w.Z);
        public static Point32 operator *(float f, in Point32 v) => new(f * v.X, f * v.Y, f * v.Z);
        public static Point32 operator *(in Point32 v, float f) => new(f * v.X, f * v.Y, f * v.Z);
        public static Point32 operator /(in Point32 v, float f) => new(v.X / f, v.Y / f, v.Z / f);
        public static Point32 operator -(in Point32 v) => new(-v.X, -v.Y, -v.Z);
        public readonly float Dot(in Point32 v) => X * v.X + Y * v.Y + Z * v.Z;
        public readonly float SquaredNorm => Dot(this);
        public readonly float Norm => (float)System.Math.Sqrt(SquaredNorm);
        public readonly Point32 Normalized => this / Norm;
        public readonly Point32 Cross(in Point32 v) => new(Y * v.Z - Z * v.Y, Z * v.X - X * v.Z, X * v.Y - Y * v.X);
        public static implicit operator Point32(in (float X, float Y, float Z) p) => new(p.X, p.Y, p.Z);
    
        public Serializer<Point32> CreateSerializer() => new Serializer();
        public Deserializer<Point32> CreateDeserializer() => new Deserializer();
    
        sealed class Serializer : Serializer<Point32>
        {
            public override void RosSerialize(Point32 msg, ref WriteBuffer b) => msg.RosSerialize(ref b);
            public override void RosSerialize(Point32 msg, ref WriteBuffer2 b) => msg.RosSerialize(ref b);
            public override int RosMessageLength(Point32 msg) => msg.RosMessageLength;
            public override int Ros2MessageLength(Point32 msg) => msg.Ros2MessageLength;
        }
        sealed class Deserializer : Deserializer<Point32>
        {
            public override void RosDeserialize(ref ReadBuffer b, out Point32 msg) => msg = new Point32(ref b);
            public override void RosDeserialize(ref ReadBuffer2 b, out Point32 msg) => msg = new Point32(ref b);
        }
    }
}
