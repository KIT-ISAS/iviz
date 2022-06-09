#nullable enable

using System.Runtime.Serialization;
using Iviz.Common;
using Iviz.Msgs.StdMsgs;
using Iviz.Roslib.Utils;

namespace Iviz.Core.Configurations
{
    [DataContract]
    public sealed class CameraConfiguration : JsonToString, IConfiguration
    {
        [DataMember] public string Id { get; set; } = nameof(ModuleType.Camera);
        [DataMember] public ModuleType ModuleType => ModuleType.Camera;
        [DataMember] public bool EnableSun { get; set; } = true;
        [DataMember] public float SunDirectionX { get; set; } = 10;
        [DataMember] public float SunDirectionY { get; set; }
        [DataMember] public float EquatorIntensity { get; set; } = 0.5f;
        [DataMember] public bool EnableShadows { get; set; } = true;
        [DataMember] public float CameraFieldOfView { get; set; } = 90;
        [DataMember] public ColorRGBA BackgroundColor { get; set; } = new(0.145f, 0.168f, 0.207f, 1);
    }
}