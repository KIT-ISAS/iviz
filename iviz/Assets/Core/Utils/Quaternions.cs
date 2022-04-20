using UnityEngine;

namespace Iviz.Core
{
    public static class Quaternions
    {
        public static readonly Quaternion Rotate90AroundX = new(0.707106769f, 0, 0, 0.707106769f);
        public static readonly Quaternion Rotate180AroundX = new(1, 0, 0, 0);
        public static readonly Quaternion Rotate270AroundX = new(-0.707106769f, 0, 0, 0.707106769f);
        public static readonly Quaternion Rotate90AroundY = new(0, 0.707106769f, 0, 0.707106769f);
        public static readonly Quaternion Rotate180AroundY = new(0, 1, 0, 0);
        public static readonly Quaternion Rotate270AroundY = new(0, 0.707106769f, 0, -0.707106769f);
        public static readonly Quaternion Rotate180AroundZ = new(0, 0, 1, 0);
    }
}