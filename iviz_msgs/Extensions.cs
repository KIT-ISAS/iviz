using System.Runtime.CompilerServices;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.IvizMsgs;
using Iviz.Msgs.StdMsgs;
using Newtonsoft.Json;

namespace Iviz.Msgs
{
    public static class Extensions
    {
        public static Quaternion Multiply(in Quaternion a, in Quaternion b)
        {
            return new(
                a.W * b.X + a.X * b.W + a.Y * b.Z - a.Z * b.Y,
                a.W * b.Y - a.X * b.Z + a.Y * b.W + a.Z * b.X,
                a.W * b.Z + a.X * b.Y - a.Y * b.X + a.Z * b.W,
                a.W * b.W - a.X * b.X - a.Y * b.Y - a.Z * b.Z
            );
        }

        public static Vector3 Multiply(in Quaternion q, in Vector3 v) => Multiply(q.XYZ, q.W, v);

        static Vector3 Multiply(in Vector3 qv, double w, in Vector3 v)
        {
            return v + 2 * qv.Cross(qv.Cross(v) + w * v);
        }

        public static Quaternion AngleAxis(double angleInRad, in Vector3 axis)
        {
            double halfAngleInRad = angleInRad / 2;
            return (System.Math.Sin(halfAngleInRad) * axis, System.Math.Cos(halfAngleInRad));
        }

        public static Quaternion AngleAxis(double angleInRad, VectorUnitX _)
        {
            double halfAngleInRad = angleInRad / 2;
            return new Quaternion(System.Math.Sin(halfAngleInRad), 0, 0, System.Math.Cos(halfAngleInRad));
        }

        public static Quaternion AngleAxis(double angleInRad, VectorUnitY _)
        {
            double halfAngleInRad = angleInRad / 2;
            return new Quaternion(0, System.Math.Sin(halfAngleInRad), 0, System.Math.Cos(halfAngleInRad));
        }

        public static Quaternion AngleAxis(double angleInRad, VectorUnitZ _)
        {
            double halfAngleInRad = angleInRad / 2;
            return new Quaternion(0, 0, System.Math.Sin(halfAngleInRad), System.Math.Cos(halfAngleInRad));
        }

        public static Quaternion Rodrigues(in Vector3 rod)
        {
            double angle = rod.Norm;
            return angle == 0
                ? Quaternion.Identity
                : AngleAxis(angle, rod / angle);
        }

        public static Quaternion Normalize(in Quaternion q)
        {
            double norm = System.Math.Sqrt(q.X * q.X + q.Y * q.Y + q.Z * q.Z + q.W * q.W);
            return norm == 0 ? q : new Quaternion(q.X / norm, q.Y / norm, q.Z / norm, q.W / norm);
        }

        public static Pose WithPosition(this in Pose pose, in Point position) => new(position, pose.Orientation);

        public static Pose WithPosition(this in Pose pose, double x, double y, double z) =>
            new(new Point(x, y, z), pose.Orientation);

        public static Pose WithOrientation(this in Pose pose, in Quaternion orientation) =>
            new(pose.Position, orientation);

        public static Transform WithTranslation(this in Transform t, in Vector3 translation) =>
            new(translation, t.Rotation);

        public static Transform WithRotation(this in Transform t, in Quaternion rotation) =>
            new(t.Translation, rotation);

        public static Transform Translate(this in Transform t, in Vector3 translation) =>
            new(t.Translation + translation, t.Rotation);

        public static Transform Rotate(this in Transform t, in Quaternion rotation) =>
            new(rotation * t.Translation, rotation * t.Rotation);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Deconstruct(this in Transform t, out Vector3 Translation, out Quaternion Rotation)
        {
            Translation = t.Translation;
            Rotation = t.Rotation;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Deconstruct(this in Pose t, out Point Position, out Quaternion Orientation)
        {
            Position = t.Position;
            Orientation = t.Orientation;
        }

        public static ColorRGBA WithRed(this in ColorRGBA c, float red) => new(red, c.G, c.B, c.A);
        public static ColorRGBA WithGreen(this in ColorRGBA c, float green) => new(c.R, green, c.B, c.A);
        public static ColorRGBA WithBlue(this in ColorRGBA c, float blue) => new(c.R, c.G, blue, c.A);
        public static ColorRGBA WithAlpha(this in ColorRGBA c, float alpha) => new(c.R, c.G, c.B, alpha);

        public static ColorRGBA Interpolate(this in ColorRGBA c, in ColorRGBA v, float f) =>
            new ColorRGBA(c.R + f * (v.R - c.R), c.G + f * (v.G - c.G), c.B + f * (v.B - c.B), c.A + f * (v.A - c.A));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Deconstruct(this in ColorRGBA v, out float R, out float G, out float B, out float A) =>
            (R, G, B, A) = (v.R, v.G, v.B, v.A);

        public static Point WithX(this in Point p, double x) => new(x, p.Y, p.Z);
        public static Point WithY(this in Point p, double y) => new(p.X, y, p.Z);
        public static Point WithZ(this in Point p, double z) => new(p.X, p.Y, z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Deconstruct(this in Point v, out double X, out double Y, out double Z) =>
            (X, Y, Z) = (v.X, v.Y, v.Z);

        public static Vector3 WithX(this in Vector3 p, double x) => new(x, p.Y, p.Z);
        public static Vector3 WithY(this in Vector3 p, double y) => new(p.X, y, p.Z);
        public static Vector3 WithZ(this in Vector3 p, double z) => new(p.X, p.Y, z);

        public static Point32 AsPoint32(this in Vector3 p) => new((float) p.X, (float) p.Y, (float) p.Z);
        public static Point32 AsPoint32(this in Point p) => new((float) p.X, (float) p.Y, (float) p.Z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Deconstruct(this in Vector3 v, out double X, out double Y, out double Z) =>
            (X, Y, Z) = (v.X, v.Y, v.Z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Deconstruct(this in Vector3f v, out float X, out float Y, out float Z) =>
            (X, Y, Z) = (v.X, v.Y, v.Z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Deconstruct(this in Vector2f v, out float X, out float Y) => (X, Y) = (v.X, v.Y);

        public static Quaternion WithX(this in Quaternion p, double x) => new(x, p.Y, p.Z, p.W);
        public static Quaternion WithY(this in Quaternion p, double y) => new(p.X, y, p.Z, p.W);
        public static Quaternion WithZ(this in Quaternion p, double z) => new(p.X, p.Y, z, p.W);
        public static Quaternion WithW(this in Quaternion p, double w) => new(p.X, p.Y, p.Z, w);

        public static void Deconstruct(this in Quaternion q, out double X, out double Y, out double Z, out double W) =>
            (X, Y, Z, W) = (q.X, q.Y, q.Z, q.W);

        public static Twist WithLinear(this in Twist p, Vector3 v) => new(v, p.Angular);
        public static Twist WithAngular(this in Twist p, Vector3 v) => new(p.Linear, v);

        public static void Deconstruct(this in Twist t, out Vector3 Linear, out Vector3 Angular)
        {
            Linear = t.Linear;
            Angular = t.Angular;
        }

        public static Header WithNextSeq(this in Header h) => new(h.Seq + 1, time.Now(), h.FrameId ?? "");

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Deconstruct(this in Header h, out uint Seq, out time Stamp, out string FrameId) =>
            (Seq, Stamp, FrameId) = (h.Seq, h.Stamp, h.FrameId ?? "");

        public static TransformStamped WithTransform(this in TransformStamped ts, in Transform t) =>
            new(ts.Header, ts.ChildFrameId ?? "", t);

        public static TransformStamped WithNextTransform(this in TransformStamped ts, in Transform t) =>
            new(ts.Header.WithNextSeq(), ts.ChildFrameId ?? "", t);

        public static TransformStamped WithHeader(this in TransformStamped ts, in Header h) =>
            new(h, ts.ChildFrameId ?? "", ts.Transform);

        public static TransformStamped WithChildFrameId(this in TransformStamped ts, string? s) =>
            new(ts.Header, s ?? "", ts.Transform);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Deconstruct(this in TransformStamped t, out Header Header, out string ChildFrameId,
            out Transform Transform)
        {
            Header = t.Header;
            ChildFrameId = t.ChildFrameId ?? "";
            Transform = t.Transform;
        }

        public static string ToString(in Point p) =>
            $"{{\"x\": {p.X.ToString(BuiltIns.Culture)}, \"y\": {p.Y.ToString(BuiltIns.Culture)}, \"z\": {p.Z.ToString(BuiltIns.Culture)}}}";

        public static string ToString(in Vector3 p) =>
            $"{{\"x\": {p.X.ToString(BuiltIns.Culture)}, \"y\": {p.Y.ToString(BuiltIns.Culture)}, \"z\": {p.Z.ToString(BuiltIns.Culture)}}}";

        public static string ToString(in Vector3f p) =>
            $"{{\"x\": {p.X.ToString(BuiltIns.Culture)}, \"y\": {p.Y.ToString(BuiltIns.Culture)}, \"z\": {p.Z.ToString(BuiltIns.Culture)}}}";

        public static string ToString(in Quaternion p) =>
            $"{{\"x\": {p.X.ToString(BuiltIns.Culture)}, \"y\": {p.Y.ToString(BuiltIns.Culture)}, " +
            $"\"z\": {p.Z.ToString(BuiltIns.Culture)}, \"w\": {p.W.ToString(BuiltIns.Culture)}}}";

        public static string ToString(in ISerializable t) => JsonConvert.SerializeObject(t);

        public static string ToString(IService t) => JsonConvert.SerializeObject(t);
    }


    public readonly struct VectorUnitX
    {
        public static implicit operator Vector3(VectorUnitX _) => new Vector3(1, 0, 0);
        public static Vector3 operator *(double f, VectorUnitX _) => new Vector3(f, 0, 0);
        public static Vector3 operator *(VectorUnitX _, double f) => new Vector3(f, 0, 0);
        public static Vector3 operator -(VectorUnitX _) => new Vector3(-1, 0, 0);
        public static Vector3 operator /(VectorUnitX _, double f) => new Vector3(1 / f, 0, 0);
        public static string ToString(in Vector3 _) => $"{{\"x\": 1, \"y\": 0, \"z\": 0}}";
    }

    public readonly struct VectorUnitY
    {
        public static implicit operator Vector3(VectorUnitY _) => new Vector3(0, 1, 0);
        public static Vector3 operator *(double f, VectorUnitY _) => new Vector3(0, f, 0);
        public static Vector3 operator *(VectorUnitY _, double f) => new Vector3(0, f, 0);
        public static Vector3 operator -(VectorUnitY _) => new Vector3(0, -1, 0);
        public static Vector3 operator /(VectorUnitY _, double f) => new Vector3(0, 1 / f, 0);
        public static string ToString(in Vector3 _) => $"{{\"x\": 0, \"y\": 1, \"z\": 0}}";
    }

    public readonly struct VectorUnitZ
    {
        public static implicit operator Vector3(VectorUnitZ _) => new Vector3(0, 0, 1);
        public static Vector3 operator *(double f, VectorUnitZ _) => new Vector3(0, 0, f);
        public static Vector3 operator *(VectorUnitZ _, double f) => new Vector3(0, 0, f);
        public static Vector3 operator -(VectorUnitZ _) => new Vector3(0, 0, -1);
        public static Vector3 operator /(VectorUnitZ _, double f) => new Vector3(0, 0, 1 / f);
        public Vector3 Cross(in Vector3 v) => new Vector3(-v.Y, v.X, 0);
        public static string ToString(in Vector3 _) => $"{{\"x\": 0, \"y\": 0, \"z\": 1}}";
    }

    public readonly struct VectorOne
    {
        public static implicit operator Vector3(VectorOne _) => new Vector3(1, 1, 1);
        public static Vector3 operator *(double f, VectorOne _) => new Vector3(f, f, f);
        public static Vector3 operator *(VectorOne _, double f) => new Vector3(f, f, f);
        public static Vector3 operator -(VectorOne _) => new Vector3(-1, -1, -1);

        public static Vector3 operator /(VectorOne _, double f)
        {
            double iF = 1 / f;
            return new Vector3(iF, iF, iF);
        }

        public static string ToString(in VectorOne _) => $"{{\"x\": 1, \"y\": 1, \"z\": 1}}";
    }
}