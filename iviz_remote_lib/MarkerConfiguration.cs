using Iviz.Msgs.StdMsgs;

#nullable enable

namespace Iviz.RemoteLib;

public sealed class MarkerConfiguration : IConfiguration
{
    public static ModuleType ModuleType => ModuleType.Marker;
    public bool? RenderAsOcclusionOnly { get; set; }
    public bool? TriangleListFlipWinding { get; set; }
    public bool? ShowDescriptions { get; set; }
    public ColorRGBA? Tint { get; set; }
    public float? Smoothness { get; set; }
    public float? Metallic { get; set; }
    public bool[]? VisibleMask { get; set; }
    public bool? Visible { get; set; }
    
    void IConfiguration.Serialize(in ConfigurationSerializer serializer)
    {
        serializer.Serialize(ModuleType);
        serializer.Serialize(RenderAsOcclusionOnly);
        serializer.Serialize(TriangleListFlipWinding);
        serializer.Serialize(ShowDescriptions);
        serializer.Serialize(Tint);
        serializer.Serialize(Smoothness);
        serializer.Serialize(Metallic);
        serializer.Serialize(VisibleMask);
        serializer.Serialize(Visible);
    }
}