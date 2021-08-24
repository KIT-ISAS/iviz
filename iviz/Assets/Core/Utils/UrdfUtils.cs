using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.Core
{
    public static class UrdfUtils
    {
        public static Vector3 ToVector3([NotNull] this Urdf.Vector3f v)
        {
            if (v == null)
            {
                throw new ArgumentNullException(nameof(v));
            }

            return new Vector3(v.X, v.Y, v.Z).Ros2Unity();
        }

        static Quaternion ToQuaternion([NotNull] this Urdf.Vector3f v)
        {
            if (v == null)
            {
                throw new ArgumentNullException(nameof(v));
            }

            return new Vector3(v.X, v.Y, v.Z).RosRpy2Unity();
        }

        public static Pose ToPose([NotNull] this Urdf.Origin v)
        {
            if (v == null)
            {
                throw new ArgumentNullException(nameof(v));
            }

            return new Pose(v.Xyz.ToVector3(), v.Rpy.ToQuaternion());
        }

        public static Color ToColor([NotNull] this Urdf.Color v)
        {
            if (v == null)
            {
                throw new ArgumentNullException(nameof(v));
            }

            var (r, g, b, a) = v.Rgba;
            return new Color(r, g, b, a);
        }

        public static bool IsReference([CanBeNull] this Urdf.Material material)
        {
            return material != null && material.Color is null && material.Texture is null;
        }
    }
}