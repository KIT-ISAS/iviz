namespace Iviz.RemoteLib;

public sealed class TfConfiguration : IConfiguration
{
    public static ModuleType ModuleType => ModuleType.TF;
    public double? FrameSize { get; set; }
    public bool? FrameLabelsVisible { get; set; }
    public bool? ParentConnectorVisible { get; set; }
    public bool? KeepAllFrames { get; set; }
    public bool? Visible { get; set; }
    public bool? Interactable { get; set; }
    public bool? FlipZ { get; set; }

    void IConfiguration.Serialize(in ConfigurationSerializer s)
    {
        s.Serialize(ModuleType);
        s.Serialize(FrameSize);
        s.Serialize(FrameLabelsVisible);
        s.Serialize(ParentConnectorVisible);
        s.Serialize(KeepAllFrames);
        s.Serialize(Visible);
        s.Serialize(Interactable);
        s.Serialize(FlipZ);
    }
}