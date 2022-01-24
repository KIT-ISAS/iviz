namespace Iviz.RemoteLib;

internal readonly struct BasicConfiguration : IConfiguration
{
    // ReSharper disable once InconsistentNaming
    readonly bool Visible;

    public BasicConfiguration(bool visible) => Visible = visible;

    void IConfiguration.Serialize(in ConfigurationSerializer s) => Serialize(in s);
    
    public void Serialize(in ConfigurationSerializer s)
    {
        s.Serialize(Visible);
    }
}