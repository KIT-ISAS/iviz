#nullable enable

using System;
using UnityEngine;

namespace Iviz.Core
{
    public static class UrdfUtils
    {
        public static Vector3 ToVector3(this Urdf.Vector3f v)
        {
            ThrowHelper.ThrowIfNull(v, nameof(v));
            return new Vector3(v.X, v.Y, v.Z).Ros2Unity();
        }

        static Quaternion ToQuaternion(this Urdf.Vector3f v)
        {
            ThrowHelper.ThrowIfNull(v, nameof(v));
            return new Vector3(v.X, v.Y, v.Z).RosRpy2Unity();
        }

        public static Pose ToPose(this Urdf.Origin v)
        {
            ThrowHelper.ThrowIfNull(v, nameof(v));
            return new Pose(v.Xyz.ToVector3(), v.Rpy.ToQuaternion());
        }

        public static Color ToColor(this Urdf.Color v)
        {
            ThrowHelper.ThrowIfNull(v, nameof(v));
            var (r, g, b, a) = v.Rgba;
            return new Color(r, g, b, a);
        }

        public static bool IsReference(this Urdf.Material? material)
        {
            return material is (_, null, null);
        }
    }
}