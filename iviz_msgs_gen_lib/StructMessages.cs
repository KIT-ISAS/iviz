using System.Collections.Generic;

namespace Iviz.MsgsGen;

public static class StructMessages
{
    public static readonly HashSet<string> BlittableStructs = new()
    {
        "geometry_msgs/Vector3",
        "geometry_msgs/Point",
        "geometry_msgs/Point32",
        "geometry_msgs/Quaternion",
        "geometry_msgs/Pose",
        "geometry_msgs/Transform",
        "std_msgs/ColorRGBA",
        
        "iviz_msgs/Color32",
        "iviz_msgs/Triangle",
        "iviz_msgs/BoundingBox",
        
        "mesh_msgs/TriangleIndices",
    };

    public static readonly HashSet<string> ForceStructs = new()
    {
        "std_msgs/Header",
    };
}