#nullable enable

using System.Runtime.Serialization;
using Iviz.Msgs.StdMsgs;

namespace Iviz.Common.Configurations
{
    [DataContract]
    public sealed class CameraConfiguration : IConfiguration
    {
        [DataMember] public string Id { get; set; } = nameof(ModuleType.Camera);
        [DataMember] public ModuleType ModuleType => ModuleType.Camera;
        [DataMember] public float SunDirectionX { get; set; } = 10;
        [DataMember] public float SunDirectionY { get; set; }
        [DataMember] public bool EnableShadows { get; set; } = true;
        [DataMember] public float CameraFieldOfView { get; set; } = 90;
        [DataMember] public ColorRGBA BackgroundColor { get; set; } = new(0.145f, 0.168f, 0.207f, 1);
    }
}