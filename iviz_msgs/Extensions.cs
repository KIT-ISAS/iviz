using System;
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
            Quaternion q;
            q.X = a.W * b.X + a.X * b.W + a.Y * b.Z - a.Z * b.Y;
            q.Y = a.W * b.Y - a.X * b.Z + a.Y * b.W + a.Z * b.X;
            q.Z = a.W * b.Z + a.X * b.Y - a.Y * b.X + a.Z * b.W;
            q.W = a.W * b.W - a.X * b.X - a.Y * b.Y - a.Z * b.Z;
            return q;
        }

        public static Vector3 Multiply(in Quaternion q, in Vector3 v)
        {
            Vector3 qv;
            qv.X = q.X;
            qv.Y = q.Y;
            qv.Z = q.Z;
            return v + 2 * qv.Cross(qv.Cross(v) + q.W * v);
        }

        public static Vector3 Cross(this in Vector3 p, in Vector3 v)
        {
            //  new(Y * v.Z - Z * v.Y, Z * v.X - X * v.Z, X * v.Y - Y * v.X);
            Vector3 q;
            q.X = p.Y * v.Z - p.Z * v.Y;
            q.Y = p.Z * v.X - p.X * v.Z;
            q.Z = p.X * v.Y - p.Y * v.X;
            return q;
        }

        public static Point Cross(this in Point p, in Point v)
        {
            //  new(Y * v.Z - Z * v.Y, Z * v.X - X * v.Z, X * v.Y - Y * v.X);
            Point q;
            q.X = p.Y * v.Z - p.Z * v.Y;
            q.Y = p.Z * v.X - p.X * v.Z;
            q.Z = p.X * v.Y - p.Y * v.X;
            return q;
        }

        public static double Dot(this in Vector3 p, in Vector3 v)
        {
            return p.X * v.X + p.Y * v.Y + p.Z * v.Z;
        }

        public static double Dot(this in Point p, in Point v)
        {
            return p.X * v.X + p.Y * v.Y + p.Z * v.Z;
        }

        public static Transform RotateAround(this in Quaternion q, in Point p) => new(p - q * p, q);

        public static Quaternion AngleAxis(double angleInRad, in Vector3 axis)
        {
            double halfAngleInRad = angleInRad / 2;
            Quaternion q;
            q.W = System.Math.Cos(halfAngleInRad);

            double s = System.Math.Sin(halfAngleInRad);
            q.X = axis.X * s;
            q.Y = axis.Y * s;
            q.Z = axis.Z * s;
            return q;
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

        public static Quaternion ToRpyRotation(this in Vector3 rpy)
        {
            var q = rpy.Z != 0
                ? AngleAxis(rpy.Z, new VectorUnitZ())
                : Quaternion.Identity;

            if (rpy.Y != 0)
            {
                q *= AngleAxis(rpy.Y, new VectorUnitY());
            }

            if (rpy.X != 0)
            {
                q *= AngleAxis(rpy.X, new VectorUnitX());
            }

            return q;
        }

        public static Quaternion Normalize(Quaternion q)
        {
            double normSq = q.X * q.X + q.Y * q.Y + q.Z * q.Z + q.W * q.W;
            if (normSq < 1e-8 || Math.Abs(1 - normSq) < 1e-8)
            {
                return q;
            }

            double norm = System.Math.Sqrt(normSq);
            q.X /= norm;
            q.Y /= norm;
            q.Z /= norm;
            q.W /= norm;
            return q;
        }

        public static Pose WithPosition(this in Pose pose, in Point position)
        {
            Pose q;
            q.Position = position;
            q.Orientation = pose.Orientation;
            return q;
        }

        public static Pose WithPosition(this in Pose pose, double x, double y, double z)
        {
            Pose q;
            q.Position.X = x;
            q.Position.Y = y;
            q.Position.Z = z;
            q.Orientation = pose.Orientation;
            return q;
        }


        public static Pose WithOrientation(this in Pose pose, in Quaternion orientation)
        {
            Pose q;
            q.Position = pose.Position;
            q.Orientation = orientation;
            return q;
        }

        public static Transform WithTranslation(this in Transform t, in Vector3 translation)
        {
            Transform q;
            q.Translation = translation;
            q.Rotation = t.Rotation;
            return q;
        }

        public static Transform WithRotation(this in Transform t, in Quaternion rotation)
        {
            Transform q;
            q.Translation = t.Translation;
            q.Rotation = rotation;
            return q;
        }

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
        public static void Deconstruct(this in ColorRGBA v, out float R, out float G, out float B, out float A)
        {
            R = v.R;
            G = v.G;
            B = v.B;
            A = v.A;
        }

        public static Point WithX(this in Point p, double x) => new(x, p.Y, p.Z);
        public static Point WithY(this in Point p, double y) => new(p.X, y, p.Z);
        public static Point WithZ(this in Point p, double z) => new(p.X, p.Y, z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Deconstruct(this in Point v, out double X, out double Y, out double Z)
        {
            X = v.X;
            Y = v.Y;
            Z = v.Z;
        }

        public static Vector3 WithX(this in Vector3 p, double x) => new(x, p.Y, p.Z);
        public static Vector3 WithY(this in Vector3 p, double y) => new(p.X, y, p.Z);
        public static Vector3 WithZ(this in Vector3 p, double z) => new(p.X, p.Y, z);

        public static Point32 AsPoint32(this in Vector3 p) => new((float)p.X, (float)p.Y, (float)p.Z);
        public static Point32 AsPoint32(this in Point p) => new((float)p.X, (float)p.Y, (float)p.Z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Deconstruct(this in Vector3 v, out double X, out double Y, out double Z)
        {
            X = v.X;
            Y = v.Y;
            Z = v.Z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Deconstruct(this in Vector3f v, out float X, out float Y, out float Z)
        {
            X = v.X;
            Y = v.Y;
            Z = v.Z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Deconstruct(this in Vector2f v, out float X, out float Y) => (X, Y) = (v.X, v.Y);

        public static Quaternion WithX(this in Quaternion p, double x) => new(x, p.Y, p.Z, p.W);
        public static Quaternion WithY(this in Quaternion p, double y) => new(p.X, y, p.Z, p.W);
        public static Quaternion WithZ(this in Quaternion p, double z) => new(p.X, p.Y, z, p.W);
        public static Quaternion WithW(this in Quaternion p, double w) => new(p.X, p.Y, p.Z, w);

        public static void Deconstruct(this in Quaternion q, out double X, out double Y, out double Z, out double W) =>
            (X, Y, Z, W) = (q.X, q.Y, q.Z, q.W);

        public static Twist WithLinear(this Twist p, Vector3 v) => new(v, p.Angular);
        public static Twist WithAngular(this Twist p, Vector3 v) => new(p.Linear, v);

        public static void Deconstruct(this Twist t, out Vector3 Linear, out Vector3 Angular)
        {
            Linear = t.Linear;
            Angular = t.Angular;
        }

        public static Header WithNextSeq(this in Header h) => new(h.Seq + 1, time.Now(), h.FrameId ?? "");

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Deconstruct(this in Header h, out uint Seq, out time Stamp, out string FrameId)
        {
            Seq = h.Seq;
            Stamp = h.Stamp;
            FrameId = h.FrameId;
        }

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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref readonly Transform AsTransform(in Pose p) => ref As<Pose, Transform>(in p);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref readonly Pose AsPose(in Transform p) => ref As<Transform, Pose>(in p);

        static ref readonly TU As<TT, TU>(in TT tt) where TT : unmanaged where TU : unmanaged =>
            ref Unsafe.As<TT, TU>(ref Unsafe.AsRef(in tt));

        public static string ToString(in Point p) =>
            $"{{\"x\": {p.X.ToString(BuiltIns.Culture)}, \"y\": {p.Y.ToString(BuiltIns.Culture)}, \"z\": {p.Z.ToString(BuiltIns.Culture)}}}";

        public static string ToString(in Vector3 p) =>
            $"{{\"x\": {p.X.ToString(BuiltIns.Culture)}, \"y\": {p.Y.ToString(BuiltIns.Culture)}, \"z\": {p.Z.ToString(BuiltIns.Culture)}}}";

        public static string ToString(in Vector3f p) =>
            $"{{\"x\": {p.X.ToString(BuiltIns.Culture)}, \"y\": {p.Y.ToString(BuiltIns.Culture)}, \"z\": {p.Z.ToString(BuiltIns.Culture)}}}";

        public static string ToString(in Quaternion p) =>
            $"{{\"x\": {p.X.ToString(BuiltIns.Culture)}, \"y\": {p.Y.ToString(BuiltIns.Culture)}, " +
            $"\"z\": {p.Z.ToString(BuiltIns.Culture)}, \"w\": {p.W.ToString(BuiltIns.Culture)}}}";

        public static string ToString(ISerializable t) => t.ToJsonString();

        public static string ToString(IService t) => t.ToJsonString();

        public static string ToString(IRequest t) => t.ToJsonString();

        public static string ToString(IResponse t) => t.ToJsonString();

        public static void WriteValueExtended(this JsonTextWriter writer, in ColorRGBA value)
        {
            writer.WriteStartObject();
            writer.WritePropertyName(nameof(value.R));
            writer.WriteValue(value.R);
            writer.WritePropertyName(nameof(value.G));
            writer.WriteValue(value.G);
            writer.WritePropertyName(nameof(value.B));
            writer.WriteValue(value.B);
            writer.WritePropertyName(nameof(value.A));
            writer.WriteValue(value.A);
            writer.WriteEndObject();
        }

        public static void WriteValueExtended(this JsonTextWriter writer, in Vector3 value)
        {
            writer.WriteStartObject();
            writer.WritePropertyName(nameof(value.X));
            writer.WriteValue(value.X);
            writer.WritePropertyName(nameof(value.Y));
            writer.WriteValue(value.Y);
            writer.WritePropertyName(nameof(value.Z));
            writer.WriteValue(value.Z);
            writer.WriteEndObject();
        }
    }


    public readonly struct VectorUnitX
    {
        public static implicit operator Vector3(VectorUnitX _) => new Vector3(1, 0, 0);
        public static Vector3 operator *(double f, VectorUnitX _) => new Vector3(f, 0, 0);
        public static Vector3 operator *(VectorUnitX _, double f) => new Vector3(f, 0, 0);
        public static Vector3 operator -(VectorUnitX _) => new Vector3(-1, 0, 0);
        public static Vector3 operator /(VectorUnitX _, double f) => new Vector3(1 / f, 0, 0);
        public static string ToString(in Vector3 _) => "{{\"x\": 1, \"y\": 0, \"z\": 0}}";
    }

    public readonly struct VectorUnitY
    {
        public static implicit operator Vector3(VectorUnitY _) => new Vector3(0, 1, 0);
        public static Vector3 operator *(double f, VectorUnitY _) => new Vector3(0, f, 0);
        public static Vector3 operator *(VectorUnitY _, double f) => new Vector3(0, f, 0);
        public static Vector3 operator -(VectorUnitY _) => new Vector3(0, -1, 0);
        public static Vector3 operator /(VectorUnitY _, double f) => new Vector3(0, 1 / f, 0);
        public static string ToString(in Vector3 _) => "{{\"x\": 0, \"y\": 1, \"z\": 0}}";
    }

    public readonly struct VectorUnitZ
    {
        public static implicit operator Vector3(VectorUnitZ _) => new Vector3(0, 0, 1);
        public static Vector3 operator *(double f, VectorUnitZ _) => new Vector3(0, 0, f);
        public static Vector3 operator *(VectorUnitZ _, double f) => new Vector3(0, 0, f);
        public static Vector3 operator -(VectorUnitZ _) => new Vector3(0, 0, -1);
        public static Vector3 operator /(VectorUnitZ _, double f) => new Vector3(0, 0, 1 / f);
        public Vector3 Cross(in Vector3 v) => new Vector3(-v.Y, v.X, 0);
        public static string ToString(in Vector3 _) => "{{\"x\": 0, \"y\": 0, \"z\": 1}}";
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