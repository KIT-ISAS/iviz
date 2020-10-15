using UnityEngine;

namespace Iviz
{
    public static class UrdfUtils
    {
        public static Vector3 ToVector3(this Urdf.Vector3 v)
        {
            return new Vector3(v.X, v.Y, v.Z).Ros2Unity();
        }

        static Quaternion ToQuaternion(this Urdf.Vector3 v)
        {
            return new Vector3(v.X, v.Y, v.Z).RosRpy2Unity();
        }

        public static Pose ToPose(this Urdf.Origin v)
        {
            return new Pose(v.Xyz.ToVector3(), v.Rpy.ToQuaternion());
        }

        public static Color ToColor(this Urdf.Color v)
        {
            return new Color(v.Rgba.R, v.Rgba.G, v.Rgba.B, v.Rgba.A);
        }

        public static bool IsReference(this Urdf.Material material)
        {
            return material.Color is null && material.Texture is null && !(material.Name is null);
        }
    }
}