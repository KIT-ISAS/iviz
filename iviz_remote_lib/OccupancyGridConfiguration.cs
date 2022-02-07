using Iviz.Msgs.StdMsgs;

namespace Iviz.RemoteLib;

public sealed class OccupancyGridConfiguration : IConfiguration
{
    public static ModuleType ModuleType => ModuleType.OccupancyGrid;
    public bool? Visible { get; set; } 
    public ColormapId? Colormap { get; set; }
    public bool? FlipMinMax { get; set; }
    public bool? CubesVisible { get; set; }
    public bool? TextureVisible { get; set; }
    public float? ScaleZ { get; set; }
    public bool? RenderAsOcclusionOnly { get; set; }
    public ColorRGBA? Tint { get; set; }
     
    void IConfiguration.Serialize(in ConfigurationSerializer serializer)
    {
        serializer.Serialize(ModuleType);
        serializer.Serialize(Visible);
        serializer.Serialize((int?) Colormap);
        serializer.Serialize(FlipMinMax);
        serializer.Serialize(CubesVisible);
        serializer.Serialize(TextureVisible);
        serializer.Serialize(ScaleZ);
        serializer.Serialize(RenderAsOcclusionOnly);
        serializer.Serialize(Tint);
    }
}

