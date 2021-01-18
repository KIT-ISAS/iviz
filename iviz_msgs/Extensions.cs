using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.StdMsgs;

namespace Iviz.Msgs
{
    public static class Extensions
    {
        public static Quaternion Multiply(in Quaternion a, in Quaternion b)
        {
            return new Quaternion(
                a.W * b.X + a.X * b.W + a.Y * b.Z - a.Z * b.Y,
                a.W * b.Y - a.X * b.Z + a.Y * b.W + a.Z * b.X,
                a.W * b.Z + a.X * b.Y - a.Y * b.X + a.Z * b.W,
                a.W * b.W - a.X * b.X - a.Y * b.Y - a.Z * b.Z
            );
        }

        public static Vector3 Multiply(in Quaternion q, in Vector3 v)
        {
            Vector3 qv = new Vector3(q.X, q.Y, q.Z);
            return v + 2 * qv.Cross(qv.Cross(v) + q.W * v);
        }

        public static Quaternion AngleAxis(double angleInRad, Vector3 axis)
        {
            return (System.Math.Sin(angleInRad / 2) * axis, System.Math.Cos(angleInRad / 2));
        }

        public static Quaternion Normalize(in Quaternion q)
        {
            double norm = System.Math.Sqrt(q.X * q.X + q.Y * q.Y + q.Z * q.Z + q.W * q.W);
            return norm == 0 ? q : new Quaternion(q.X / norm, q.Y / norm, q.Z / norm, q.W / norm);
        }

        public static Pose WithPosition(this Pose pose, in Point position) => new Pose(position, pose.Orientation);

        public static Pose WithOrientation(this Pose pose, in Quaternion orientation) =>
            new Pose(pose.Position, orientation);

        public static Transform WithTranslation(this Transform t, in Vector3 translation) =>
            new Transform(translation, t.Rotation);

        public static Transform WithRotation(this Transform t, in Quaternion rotation) =>
            new Transform(t.Translation, rotation);

        public static Transform Translate(this Transform t, in Vector3 translation) =>
            new Transform(t.Translation + translation, t.Rotation);

        public static Transform Rotate(this Transform t, in Quaternion rotation) =>
            new Transform(rotation * t.Translation, rotation * t.Rotation);
        
        public static void Deconstruct(this Transform t, out Vector3 Translation, out Quaternion Rotation)
        {
            Translation = t.Translation;
            Rotation = t.Rotation;
        } 
        
        public static void Deconstruct(this Pose t, out Point Position, out Quaternion Orientation)
        {
            Position = t.Position;
            Orientation = t.Orientation;
        }         

        public static ColorRGBA WithRed(this ColorRGBA c, float red) => new ColorRGBA(red, c.G, c.B, c.A);
        public static ColorRGBA WithGreen(this ColorRGBA c, float green) => new ColorRGBA(c.R, green, c.B, c.A);
        public static ColorRGBA WithBlue(this ColorRGBA c, float blue) => new ColorRGBA(c.R, c.G, blue, c.A);
        public static ColorRGBA WithAlpha(this ColorRGBA c, float alpha) => new ColorRGBA(c.R, c.G, c.B, alpha);
        public static void Deconstruct(this ColorRGBA v, out float R, out float G, out float B, out float A)
        {
            R = v.R;
            G = v.G;
            B = v.B;
            A = v.A;
        } 
        
        public static Point WithX(this Point p, double x) => new Point(x, p.Y, p.Z);
        public static Point WithY(this Point p, double y) => new Point(p.X, y, p.Z);
        public static Point WithZ(this Point p, double z) => new Point(p.X, p.Y, z);
        public static void Deconstruct(this Point v, out double X, out double Y, out double Z)
        {
            X = v.X;
            Y = v.Y;
            Z = v.Z;
        } 
        
        public static Vector3 WithX(this Vector3 p, double x) => new Vector3(x, p.Y, p.Z);
        public static Vector3 WithY(this Vector3 p, double y) => new Vector3(p.X, y, p.Z);
        public static Vector3 WithZ(this Vector3 p, double z) => new Vector3(p.X, p.Y, z);
        public static void Deconstruct(this Vector3 v, out double X, out double Y, out double Z)
        {
            X = v.X;
            Y = v.Y;
            Z = v.Z;
        } 

        public static Quaternion WithX(this Quaternion p, double x) => new Quaternion(x, p.Y, p.Z, p.W);
        public static Quaternion WithY(this Quaternion p, double y) => new Quaternion(p.X, y, p.Z, p.W);
        public static Quaternion WithZ(this Quaternion p, double z) => new Quaternion(p.X, p.Y, z, p.W);
        public static Quaternion WithW(this Quaternion p, double w) => new Quaternion(p.X, p.Y, p.Z, w);
        public static void Deconstruct(this Quaternion q, out double X, out double Y, out double Z, out double W)
        {
            X = q.X;
            Y = q.Y;
            Z = q.Z;
            W = q.W;
        }             
        
        public static Twist WithLinear(this Twist p, Vector3 v) => new Twist(v, p.Angular);
        public static Twist WithAngular(this Twist p, Vector3 v) => new Twist(p.Linear, v);
        public static void Deconstruct(this Twist t, out Vector3 Linear, out Vector3 Angular)
        {
            Linear = t.Linear;
            Angular = t.Angular;
        }        
        
        public static Header WithNextSeq(this Header h) => new Header(h.Seq + 1, time.Now(), h.FrameId);
        public static void Deconstruct(this Header h, out uint Seq, out time Stamp, out string FrameId)
        {
            Seq = h.Seq;
            Stamp = h.Stamp;
            FrameId = h.FrameId;
        }
    }
}