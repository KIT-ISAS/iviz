using System.Collections.Generic;

namespace Iviz.MsgsGen
{
    internal static class Additions
    {
        public static readonly Dictionary<string, string[]> Contents = new Dictionary<string, string[]>
        {
            ["geometry_msgs/Point"] = new[]
            {
                "public static readonly Point Zero = new Point(0, 0, 0);",
                "public static readonly Point One = new Point(1, 1, 1);",
                "public static implicit operator Vector3(in Point p) => new Vector3(p.X, p.Y, p.Z);",
                "public static Point operator +(in Point v, in Point w) => new Point(v.X + w.X, v.Y + w.Y, v.Z + w.Z);",
                "public static Point operator -(in Point v, in Point w) => new Point(v.X - w.X, v.Y - w.Y, v.Z - w.Z);",
                "public static Point operator *(double f, in Point v) => new Point(f * v.X, f * v.Y, f * v.Z);",
                "public static Point operator *(in Point v, double f) => new Point(f * v.X, f * v.Y, f * v.Z);",
                "public static Point operator /(in Point v, double f) => new Point(v.X / f, v.Y / f, v.Z / f);",
                "public static Point operator -(in Point v) => new Point(-v.X, -v.Y, -v.Z);",
            },

            ["geometry_msgs/Vector3"] = new[]
            {
                "public static readonly Vector3 Zero = new Vector3(0, 0, 0);",
                "public static readonly Vector3 One = new Vector3(1, 1, 1);",
                "public static implicit operator Point(in Vector3 p) => new Point(p.X, p.Y, p.Z);",
                "public static Vector3 operator +(in Vector3 v, in Vector3 w) => new Vector3(v.X + w.X, v.Y + w.Y, v.Z + w.Z);",
                "public static Vector3 operator -(in Vector3 v, in Vector3 w) => new Vector3(v.X - w.X, v.Y - w.Y, v.Z - w.Z);",
                "public static Vector3 operator *(double f, in Vector3 v) => new Vector3(f * v.X, f * v.Y, f * v.Z);",
                "public static Vector3 operator *(in Vector3 v, double f) => new Vector3(f * v.X, f * v.Y, f * v.Z);",
                "public static Vector3 operator /(in Vector3 v, double f) => new Vector3(v.X / f, v.Y / f, v.Z / f);",
                "public static Vector3 operator -(in Vector3 v) => new Vector3(-v.X, -v.Y, -v.Z);",
                "public readonly double Dot(in Vector3 v) => X * v.X + Y * v.Y + Z * v.Z;",
                "public readonly double SquaredNorm => Dot(this);",
                "public readonly double Norm => System.Math.Sqrt(SquaredNorm);",
                "public readonly Vector3 Normalized => this / Norm;",
                "public readonly Vector3 Cross(in Vector3 v) => new Vector3(Y * v.Z - Z * v.Y, Z * v.X - X * v.Z, X * v.Y - Y * v.X);",
            },
            
            ["geometry_msgs/Quaternion"] = new[]
            {
                "public static readonly Quaternion Identity = new Quaternion(0, 0, 0, 1);", 
                "public static Quaternion operator *(in Quaternion a, in Quaternion b) =>",
                "    new Quaternion(",
                "        a.W * b.X + a.X * b.W + a.Y * b.Z - a.Z * b.Y,",
                "        a.W * b.Y - a.X * b.Z + a.Y * b.W + a.Z * b.X,",
                "        a.W * b.Z + a.X * b.Y - a.Y * b.X + a.Z * b.W,",
                "        a.W * b.W - a.X * b.X - a.Y * b.Y - a.Z * b.Z",
                "    );"
            },
            
            ["geometry_msgs/Transform"] = new[]
            {
                "public static readonly Transform Identity = new Transform(Point.Zero, Quaternion.Identity);", 
                "public static implicit operator Pose(in Transform p) => new Pose(p.Translation, p.Rotation);"
            },    
            
            ["geometry_msgs/Pose"] = new[]
            {
                "public static readonly Pose Identity = new Pose(Point.Zero, Quaternion.Identity);", 
                "public static implicit operator Transform(in Pose p) => new Transform(p.Position, p.Orientation);"  
            },          
            
            ["std_msgs/ColorRGBA"] = new[]
            {
                "public static readonly ColorRGBA White = new ColorRGBA(1, 1, 1, 1);", 
                "public static readonly ColorRGBA Black = new ColorRGBA(0, 0, 0, 1);", 
                "public static readonly ColorRGBA Red = new ColorRGBA(1, 0, 0, 1);", 
                "public static readonly ColorRGBA Green = new ColorRGBA(0, 1, 0, 1);", 
                "public static readonly ColorRGBA Blue = new ColorRGBA(0, 0, 1, 1);", 
                "public static readonly ColorRGBA Yellow = new ColorRGBA(1, 1, 0, 1);", 
                "public static readonly ColorRGBA Cyan = new ColorRGBA(0, 1, 1, 1);", 
                "public static readonly ColorRGBA Magenta = new ColorRGBA(1, 0, 1, 1);", 
                "public static readonly ColorRGBA Grey = new ColorRGBA(0.5f, 0.5f, 0.5f, 1);", 
                "public static ColorRGBA operator *(in ColorRGBA v, in ColorRGBA w) => new ColorRGBA(v.R * w.R, v.G * w.G, v.B * w.B, v.A * w.A);",
            },                 
        };
    }
}