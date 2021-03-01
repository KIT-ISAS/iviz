using UnityEngine;

namespace Iviz.Core
{
    public static class SdfUtils
    {
        public static Vector3 ToVector3(this Sdf.Vector3d v)
        {
            return new Vector3((float)v.X, (float)v.Y, (float)v.Z).Ros2Unity();
        }

        static Quaternion ToQuaternion(this Sdf.Vector3d v)
        {
            return new Vector3((float)v.X, (float)v.Y, (float)v.Z).RosRpy2Unity();
        }

        public static Pose ToPose(this Sdf.Pose v)
        {
            return new Pose(v.Position.ToVector3(), v.Orientation.ToQuaternion());
        }

        public static Color ToColor(this Sdf.Color v)
        {
            return new Color((float)v.R, (float)v.G, (float)v.B, (float)v.A);
        }
        
        public static Color32 ToColor32(this Msgs.IvizMsgs.Color32 v)
        {
            return new Color32(v.R, v.G, v.B, v.A);
        }

        public static Vector3 ToVector3(this Msgs.IvizMsgs.Vector3f v)
        {
            return new Vector3(v.X, v.Y, v.Z).Ros2Unity();
        }
        
    }
}