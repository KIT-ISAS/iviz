using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Iviz.Common
{
    /// <summary>
    /// Module type.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ModuleType
    {
        Invalid = 0,
        Grid,
        TF,
        PointCloud,
        Image,
        Marker,
        InteractiveMarker,
        JointState,
        DepthCloud,
        LaserScan,
        AR,
        Magnitude,
        OccupancyGrid,
        Joystick,
        Path,
        GridMap,
        Robot,
        Octomap,
        VizWidget,
        XR,
        Camera,
        TFPublisher,
    }
}