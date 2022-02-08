using Iviz.Msgs.StdMsgs;

namespace Iviz.RemoteLib;

public sealed class MagnitudeConfiguration : IConfiguration
{
    public static ModuleType ModuleType => ModuleType.Magnitude;
    public bool? Visible { get; set; }
    public bool? TrailVisible { get; set; }
    public bool? AngleVisible { get; set; }
    public bool? FrameVisible { get; set; }
    public bool? VectorVisible { get; set; }
    public float? Scale { get; set; }
    public float? VectorScale { get; set; }
    public ColorRGBA? Color { get; set; }
    public float? TrailTime { get; set; }

    void IConfiguration.Serialize(in ConfigurationSerializer serializer)
    {
        serializer.Serialize(ModuleType);
        serializer.Serialize(Visible);
        serializer.Serialize(TrailVisible);
        serializer.Serialize(AngleVisible);
        serializer.Serialize(FrameVisible);
        serializer.Serialize(VectorVisible);
        serializer.Serialize(Scale);
        serializer.Serialize(VectorScale);
        serializer.Serialize(Color);
        serializer.Serialize(TrailTime);
    }
}