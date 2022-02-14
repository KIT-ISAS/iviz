using System;
using System.Runtime.Serialization;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.StdMsgs;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Iviz.Common.Configurations
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum GridOrientation
    {
        XY,
        YZ,
        XZ
    }

    [DataContract]
    public sealed class GridConfiguration : IConfiguration
    {
        [DataMember] public string Id { get; set; } = Guid.NewGuid().ToString();
        [DataMember] public ModuleType ModuleType => ModuleType.Grid;
        [DataMember] public bool Visible { get; set; } = true;
        [DataMember] public GridOrientation Orientation { get; set; } = GridOrientation.XY;
        [DataMember] public ColorRGBA GridColor { get; set; } = new(0.3f, 0.3f,0.3f, 1);
        [DataMember] public ColorRGBA InteriorColor { get; set; } = new(0.4f, 0.4f, 0.4f, 1);
        [DataMember] public bool InteriorVisible { get; set; } = true;
        [DataMember] public bool FollowCamera { get; set; } = true;
        [DataMember] public bool HideInARMode { get; set; } = true;
        [DataMember] public bool Interactable { get; set; } = true;
        [DataMember] public Vector3 Offset { get; set; } = Vector3.Zero;
    }
}