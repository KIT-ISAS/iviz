using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.StdMsgs;

namespace Iviz.Msgs
{
    public static class MsgUtils
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
        public static Pose WithOrientation(this Pose pose, in Quaternion orientation) => new Pose(pose.Position, orientation);
        
        public static Transform WithTranslation(this Transform t, in Vector3 translation) => new Transform(translation, t.Rotation);
        public static Transform WithRotation(this Transform t, in Quaternion rotation) => new Transform(t.Translation, rotation);
        public static Transform Translate(this Transform t, in Vector3 translation) => new Transform(t.Translation + translation, t.Rotation);
        public static Transform Rotate(this Transform t, in Quaternion rotation) => new Transform(rotation * t.Translation, rotation * t.Rotation);
        
        public static ColorRGBA WithRed(this ColorRGBA c, float red) => new ColorRGBA(red, c.G, c.B, c.A);
        public static ColorRGBA WithGreen(this ColorRGBA c, float green) => new ColorRGBA(c.R, green, c.B, c.A);
        public static ColorRGBA WithBlue(this ColorRGBA c, float blue) => new ColorRGBA(c.R, c.G, blue, c.A);
        public static ColorRGBA WithAlpha(this ColorRGBA c, float alpha) => new ColorRGBA(c.R, c.G, c.B, alpha);

        public static Point WithX(this Point p, float x) => new Point(x, p.Y, p.Z);
        public static Point WithY(this Point p, float y) => new Point(p.X, y, p.Z);
        public static Point WithZ(this Point p, float z) => new Point(p.X, p.Y, z);
        
        public static Vector3 WithX(this Vector3 p, float x) => new Vector3(x, p.Y, p.Z);
        public static Vector3 WithY(this Vector3 p, float y) => new Vector3(p.X, y, p.Z);
        public static Vector3 WithZ(this Vector3 p, float z) => new Vector3(p.X, p.Y, z);
        
        public static Quaternion WithX(this Quaternion p, float x) => new Quaternion(x, p.Y, p.Z, p.W);
        public static Quaternion WithY(this Quaternion p, float y) => new Quaternion(p.X, y, p.Z, p.W);
        public static Quaternion WithZ(this Quaternion p, float z) => new Quaternion(p.X, p.Y, z, p.W);
        public static Quaternion WithW(this Quaternion p, float w) => new Quaternion(p.X, p.Y, p.Z, w);

    }
}