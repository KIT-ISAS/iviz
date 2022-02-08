using Iviz.Msgs.Tf2Msgs;
using Iviz.Msgs.VisualizationMsgs;

namespace Iviz.RemoteLib;

public enum ModuleType
{
    /// <summary>
    /// A grid plane. See <see cref="GridConfiguration"/>.
    /// </summary>
    Grid = 1,

    /// <summary>
    /// Manager for TF frames, listens to <see cref="TFMessage"/> messages.
    /// See <see cref="TFConfiguration"/>.
    /// </summary>
    TF = 2,
    PointCloud = 3,
    Image = 4,

    /// <summary>
    /// Manager for markers, listens to <see cref="Msgs.VisualizationMsgs.Marker"/> and <see cref="MarkerArray"/> messages.
    /// See <see cref="MarkerConfiguration"/>.
    /// </summary>
    Marker = 5,
    InteractiveMarker = 6,
    DepthCloud = 8,
    LaserScan = 9,
    AugmentedReality = 10,
    Magnitude = 11,
    
    /// <summary>
    /// Manager for occupancy grids, listens to <see cref="Msgs.NavMsgs.OccupancyGrid"/> messages.
    /// See <see cref="OccupancyGridConfiguration"/>.
    /// </summary>
    OccupancyGrid = 12,
    
    Joystick = 13,
    Path = 14,
    GridMap = 15,

    /// <summary>
    /// Visualizer for robots. See <see cref="RobotConfiguration"/>. 
    /// </summary>
    Robot = 16,
    GuiWidget = 18,
    XR = 19,
    Camera = 20,
    TFPublisher = 21
}

public enum AddModuleType
{
    /// <inheritdoc cref="ModuleType.Grid"/>
    Grid = ModuleType.Grid,
    AugmentedReality = ModuleType.AugmentedReality,
    Joystick = ModuleType.Joystick,

    /// <inheritdoc cref="ModuleType.Robot"/>
    Robot = ModuleType.Robot,
    
    DepthCloud = ModuleType.DepthCloud,
}
