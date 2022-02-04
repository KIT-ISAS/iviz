using Iviz.Msgs.StdMsgs;

namespace Iviz.RemoteLib;

public sealed class RobotConfiguration : IConfiguration
{
    public string? SourceParameter { get; set; }
    public string? SavedRobotName { get; set; }
    public string? FramePrefix { get; set; }
    public string? FrameSuffix { get; set; }
    public bool? AttachedToTf { get; set; }
    public bool? RenderAsOcclusionOnly { get; set; }
    public ColorRGBA? Tint { get; set; }
    public double? Metallic { get; set; }
    public double? Smoothness { get; set; }
    public bool? Interactable { get; set; }
    public bool? KeepMeshMaterials { get; set; }
    public bool? Visible { get; set; }
    public static ModuleType ModuleType => ModuleType.Robot;

    void IConfiguration.Serialize(in ConfigurationSerializer s)
    {
        s.Serialize(SourceParameter);
        s.Serialize(SavedRobotName);
        s.Serialize(FramePrefix);
        s.Serialize(FrameSuffix);
        s.Serialize(AttachedToTf);
        s.Serialize(RenderAsOcclusionOnly);
        s.Serialize(Tint);
        s.Serialize(Metallic);
        s.Serialize(Smoothness);
        s.Serialize(Visible);
        s.Serialize(Interactable);
        s.Serialize(KeepMeshMaterials);
        s.Serialize(ModuleType);
    }
}