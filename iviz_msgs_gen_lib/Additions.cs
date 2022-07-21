using System.Collections.Generic;

namespace Iviz.MsgsGen
{
    internal static class Additions
    {
        public static readonly Dictionary<string, string[]> Contents = new()
        {
            ["geometry_msgs/Point"] = new[]
            {
                "public static Point Zero => new();",
                "public static Point One => new(1, 1, 1);",
                "public static Point UnitX => new(1, 0, 0);",
                "public static Point UnitY => new(0, 1, 0);",
                "public static Point UnitZ => new(0, 0, 1);",
                "public static implicit operator Vector3(in Point p) => new(p.X, p.Y, p.Z);",
                "public static bool operator !=(in Point a, in Point b) => !(a == b);",
                "public static bool operator ==(in Point a, in Point b) => a.X == b.X && a.Y == b.Y && a.Z == b.Z;",
                "public static Point operator +(in Point v, in Vector3 w) => new(v.X + w.X, v.Y + w.Y, v.Z + w.Z);",
                "public static Point operator -(in Point v, in Vector3 w) => new(v.X - w.X, v.Y - w.Y, v.Z - w.Z);",
                "public static Point operator *(double f, in Point v) => new(f * v.X, f * v.Y, f * v.Z);",
                "public static Point operator *(in Point v, double f) => new(f * v.X, f * v.Y, f * v.Z);",
                "public static Point operator /(in Point v, double f) => new(v.X / f, v.Y / f, v.Z / f);",
                "public static Point operator -(in Point v) => new(-v.X, -v.Y, -v.Z);",
                "public static implicit operator Point(in (double X, double Y, double Z) p) => new(p.X, p.Y, p.Z);",
                "public readonly double SquaredNorm => X * X + Y * Y + Z * Z;",
                "public readonly double Norm => System.Math.Sqrt(SquaredNorm);",
                "public readonly Vector3 Normalized => this / Norm;",
            },

            ["geometry_msgs/Vector3"] = new[]
            {
                "public static Vector3 Zero => new();",
                "public static Vector3 One => new(1, 1, 1);",
                "public static Vector3 UnitX => new(1, 0, 0);",
                "public static Vector3 UnitY => new(0, 1, 0);",
                "public static Vector3 UnitZ => new(0, 0, 1);",
                "public static implicit operator Point(in Vector3 p) => new(p.X, p.Y, p.Z);",
                "public static bool operator !=(in Vector3 a, in Vector3 b) => !(a == b);",
                "public static bool operator ==(in Vector3 a, in Vector3 b) => a.X == b.X && a.Y == b.Y && a.Z == b.Z;",
                "public static Vector3 operator +(in Vector3 v, in Vector3 w) => new(v.X + w.X, v.Y + w.Y, v.Z + w.Z);",
                "public static Vector3 operator -(in Vector3 v, in Vector3 w) => new(v.X - w.X, v.Y - w.Y, v.Z - w.Z);",
                "public static Vector3 operator *(double f, in Vector3 v) => new(f * v.X, f * v.Y, f * v.Z);",
                "public static Vector3 operator *(in Vector3 v, double f) => new(f * v.X, f * v.Y, f * v.Z);",
                "public static Vector3 operator /(in Vector3 v, double f) => new(v.X / f, v.Y / f, v.Z / f);",
                "public static Vector3 operator -(in Vector3 v) => new(-v.X, -v.Y, -v.Z);",
                "public readonly double SquaredNorm => X * X + Y * Y + Z * Z;",
                "public readonly double Norm => System.Math.Sqrt(SquaredNorm);",
                "public readonly Vector3 Normalized => this / Norm;",
                "public static implicit operator Vector3(in (double X, double Y, double Z) p) => new(p.X, p.Y, p.Z);",
            },

            ["geometry_msgs/Quaternion"] = new[]
            {
                "public readonly Quaternion Inverse => new(-X, -Y, -Z, W);",
                "public static Quaternion Identity => new(0, 0, 0, 1);",
                "public static Quaternion operator *(in Quaternion a, in Quaternion b) => Extensions.Multiply(a, b).Normalized;",
                "public static Vector3 operator *(in Quaternion q, in Vector3 v) => Extensions.Multiply(q, v);",
                "public static Point operator *(in Quaternion q, in (double X, double Y, double Z) v) => q * (Vector3) v;",
                "public static Point operator *(in Quaternion q, in Point v) => q * (Vector3) v;",
                "public readonly Quaternion Normalized => Extensions.Normalize(this);",
                "public static implicit operator Quaternion(in (double X, double Y, double Z, double W) p) => new Quaternion(p.X, p.Y, p.Z, p.W);",
                "public static implicit operator Quaternion(in (Vector3 p, double W) q) => new Quaternion(q.p.X, q.p.Y, q.p.Z, q.W);",
                "public static Quaternion AngleAxis(double angleInRad, in Vector3 axis) => Extensions.AngleAxis(angleInRad, axis);",
                "public static Quaternion Rodrigues(in Vector3 rod) => Extensions.Rodrigues(rod);",
            },

            ["geometry_msgs/Transform"] = new[]
            {
                "public static Transform Identity => new(Vector3.Zero, Quaternion.Identity);",
                "public static implicit operator Pose(in Transform p) => Extensions.AsPose(in p);",
                "public readonly Transform Inverse => new(-(Rotation.Inverse * Translation), Rotation.Inverse);",
                "public static Transform operator *(in Transform t, in Transform q) =>",
                "        new Transform(t.Translation + t.Rotation * q.Translation, t.Rotation * q.Rotation);",
                "public static Vector3 operator *(in Transform t, in Vector3 q) => t.Rotation * q + t.Translation;",
                "public static Point operator *(in Transform t, in Point q) => t.Rotation * q + t.Translation;",
                "public static Vector3 operator *(in Transform t, in (double X, double Y, double Z) q) => t * (Vector3) q;",
                "public static Quaternion operator *(in Transform t, in Quaternion q) => t.Rotation * q;",
                "public static Transform RotateAround(in Quaternion q, in Point p) => new(p - q * p, q);",
                "public static implicit operator Transform(in (Vector3 translation, Quaternion rotation) p) => new(p.translation, p.rotation);",
            },

            ["geometry_msgs/Pose"] = new[]
            {
                "public static Pose Identity => new(Point.Zero, Quaternion.Identity);",
                "public static implicit operator Transform(in Pose p) => Extensions.AsTransform(in p);",
                "public static implicit operator Pose(in (Point position, Quaternion orientation) p) => new(p.position, p.orientation);",
            },

            ["geometry_msgs/Twist"] = new[]
            {
                "public static Twist Zero => new(Vector3.Zero, Vector3.Zero);",
                "public static implicit operator Twist(in (Vector3 linear, Vector3 angular) p) => new(p.linear, p.angular);",
            },

            ["std_msgs/ColorRGBA"] = new[]
            {
                "public static ColorRGBA White => new(1, 1, 1, 1);",
                "public static ColorRGBA Black => new(0, 0, 0, 1);",
                "public static ColorRGBA Red => new(1, 0, 0, 1);",
                "public static ColorRGBA Green => new(0, 1, 0, 1);",
                "public static ColorRGBA Blue => new(0, 0, 1, 1);",
                "public static ColorRGBA Yellow => new(1, 1, 0, 1);",
                "public static ColorRGBA Cyan => new(0, 1, 1, 1);",
                "public static ColorRGBA Magenta => new(1, 0, 1, 1);",
                "public static ColorRGBA Grey => new(0.5f, 0.5f, 0.5f, 1);",
                "public static ColorRGBA operator *(in ColorRGBA v, in ColorRGBA w) => new(v.R * w.R, v.G * w.G, v.B * w.B, v.A * w.A);",
                "public static implicit operator ColorRGBA(in (float R, float G, float B, float A) p) => new(p.R, p.G, p.B, p.A);",
                "public static implicit operator ColorRGBA(in ((float R, float G, float B) p, float A) q) => new(q.p.R, q.p.G, q.p.B, q.A);",
                "public static implicit operator ColorRGBA(in (float R, float G, float B) p) => new(p.R, p.G, p.B, 1);",
                "public (float R, float G, float B) RGB { readonly get => (R, G, B); set => (R, G, B) = value; }",
            },

            ["iviz_msgs/Vector3f"] = new[]
            {
                "public static Vector3f Zero => new();",
                "public static Vector3f One => new(1, 1, 1);",
                "public static Vector3f UnitX => new(1, 0, 0);",
                "public static Vector3f UnitY => new(0, 1, 0);",
                "public static Vector3f UnitZ => new(0, 0, 1);",
                "public static implicit operator GeometryMsgs.Point(in Vector3f p) => new(p.X, p.Y, p.Z);",
                "public static implicit operator GeometryMsgs.Vector3(in Vector3f p) => new(p.X, p.Y, p.Z);",
                "public static implicit operator Vector3f(in GeometryMsgs.Vector3 p) => new((float)p.X, (float)p.Y, (float)p.Z);",
                "public static Vector3f operator +(in Vector3f v, in Vector3f w) => new(v.X + w.X, v.Y + w.Y, v.Z + w.Z);",
                "public static Vector3f operator -(in Vector3f v, in Vector3f w) => new(v.X - w.X, v.Y - w.Y, v.Z - w.Z);",
                "public static Vector3f operator *(float f, in Vector3f v) => new(f * v.X, f * v.Y, f * v.Z);",
                "public static Vector3f operator *(in Vector3f v, float f) => new(f * v.X, f * v.Y, f * v.Z);",
                "public static Vector3f operator /(in Vector3f v, float f) => new(v.X / f, v.Y / f, v.Z / f);",
                "public static Vector3f operator -(in Vector3f v) => new(-v.X, -v.Y, -v.Z);",
                "public readonly float Dot(in Vector3f v) => X * v.X + Y * v.Y + Z * v.Z;",
                "public readonly float SquaredNorm => Dot(this);",
                "public readonly float Norm => (float)System.Math.Sqrt(SquaredNorm);",
                "public readonly Vector3f Normalized => this / Norm;",
                "public readonly Vector3f Cross(in Vector3f v) => new(Y * v.Z - Z * v.Y, Z * v.X - X * v.Z, X * v.Y - Y * v.X);",
                "public static implicit operator Vector3f(in (float X, float Y, float Z) p) => new(p.X, p.Y, p.Z);",
            },

            ["iviz_msgs/Vector2f"] = new[]
            {
                "public static Vector2f Zero => new(0, 0);",
                "public static Vector2f One => new(1, 1);",
                "public static Vector2f UnitX => new(1, 0);",
                "public static Vector2f UnitY => new(0, 1);",
                "public static Vector2f operator +(in Vector2f v, in Vector2f w) => new(v.X + w.X, v.Y + w.Y);",
                "public static Vector2f operator -(in Vector2f v, in Vector2f w) => new(v.X - w.X, v.Y - w.Y);",
                "public static Vector2f operator *(float f, in Vector2f v) => new(f * v.X, f * v.Y);",
                "public static Vector2f operator *(in Vector2f v, float f) => new(f * v.X, f * v.Y);",
                "public static Vector2f operator /(in Vector2f v, float f) => new(v.X / f, v.Y / f);",
                "public static Vector2f operator -(in Vector2f v) => new(-v.X, -v.Y);",
                "public readonly float Dot(in Vector2f v) => X * v.X + Y * v.Y;",
                "public readonly float SquaredNorm => Dot(this);",
                "public readonly float Norm => (float)System.Math.Sqrt(SquaredNorm);",
                "public readonly Vector2f Normalized => this / Norm;",
                "public static implicit operator Vector2f(in (float X, float Y) p) => new(p.X, p.Y);",
            },

            ["std_msgs/Header"] = new[]
            {
                "public static implicit operator Header((uint seqId, string frameId) p) => new(p.seqId, time.Now(), p.frameId);",
                "public static implicit operator Header((uint seqId, time stamp, string frameId) p) => new(p.seqId, p.stamp, p.frameId);",
                "public static implicit operator Header(string frameId) => new(0, time.Now(), frameId);"
            },

            ["actionlib_msgs/GoalID"] = new[]
            {
                "public bool Equals(GoalID? other) => ReferenceEquals(this, other) || (other != null && Stamp == other.Stamp && Id == other.Id);",
                "public override bool Equals(object? obj) => ReferenceEquals(this, obj) || obj is GoalID other && Equals(other);",
                "public override int GetHashCode() => System.HashCode.Combine(Stamp, Id);",
                "public static bool operator ==(GoalID? left, GoalID? right) => ReferenceEquals(left, right) || !ReferenceEquals(left, null) && left.Equals(right);",
                "public static bool operator !=(GoalID? left, GoalID? right) => !(left == right);"
            },
        };
    }
}