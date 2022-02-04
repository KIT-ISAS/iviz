using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.StdMsgs;

namespace Iviz.RemoteLib;

public class GridConfiguration : IConfiguration
{
    public static ModuleType ModuleType => ModuleType.Grid;
    public bool? Visible { get; set; }
    public ColorRGBA? GridColor { get; set; }
    public ColorRGBA? InteriorColor { get; set; }
    public bool? InteriorVisible { get; set; } 
    public bool? FollowCamera { get; set; }
    public bool? HideInARMode { get; set; } 
    public bool? Interactable { get; set; } 
    public Vector3? Offset { get; set; }
    
    void IConfiguration.Serialize(in ConfigurationSerializer s)
    {
        s.Serialize(ModuleType);
        s.Serialize(GridColor);
        s.Serialize(InteriorColor);
        s.Serialize(InteriorVisible);
        s.Serialize(FollowCamera);
        s.Serialize(HideInARMode);
        s.Serialize(Visible);
        s.Serialize(Interactable);
        s.Serialize(Offset);
    }    
}