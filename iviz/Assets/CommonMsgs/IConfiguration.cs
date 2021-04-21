using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Iviz.Msgs.IvizCommonMsgs
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ColormapId : byte
    {
        lines,
        pink,
        copper,
        bone,
        gray,
        winter,
        autumn,
        summer,
        spring,
        cool,
        hot,
        hsv,
        jet,
        parula
    }    
    
    /// <summary>
    /// Module type.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ModuleType : byte
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
        AugmentedReality,
        Magnitude,
        OccupancyGrid,
        Joystick,
        Path,
        GridMap,
        Robot,
        Octomap,
        GuiDialog,
    }    
    
    /// <summary>
    /// Common interface for the configuration classes of the controllers.
    /// </summary>
    public interface IConfiguration
    {
        /// <summary>
        /// GUID of the controller.
        /// </summary>
        string Id { get; set; }
        
        /// <summary>
        /// Module type of the controller. 
        /// </summary>
        ModuleType ModuleType { get; }
        
        /// <summary>
        /// Whether the controller is visible. 
        /// </summary>
        bool Visible { get; }
    }
}
