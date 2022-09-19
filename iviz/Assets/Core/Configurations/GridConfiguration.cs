using System;
using System.Runtime.Serialization;
using Iviz.Common;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.StdMsgs;
using Iviz.Roslib.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Iviz.Core.Configurations
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum GridOrientation
    {
        XY,
        YZ,
        XZ
    }

    [DataContract]
    public sealed class GridConfiguration : JsonToString, IConfiguration
    {
        [DataMember] public string Id { get; set; } = Guid.NewGuid().ToString();
        [DataMember] public ModuleType ModuleType => ModuleType.Grid;
        [DataMember] public bool Visible { get; set; } = true;
        [DataMember] public GridOrientation Orientation { get; set; } = GridOrientation.XY;
        [DataMember] public ColorRGBA GridColor { get; set; } = new(0.3f, 0.3f,0.3f, 1);
        [DataMember] public ColorRGBA InteriorColor { get; set; } = new(0.7f, 0.7f, 0.7f, 1);
        [DataMember] public bool InteriorVisible { get; set; } = true;
        [DataMember] public bool FollowCamera { get; set; } = true;
        [DataMember] public bool HideInARMode { get; set; } = true;
        [DataMember] public bool Interactable { get; set; } = true;
        [DataMember] public bool DarkMode { get; set; } = true;
        [DataMember] public Vector3 Offset { get; set; } = Vector3.Zero;
        [DataMember] public float Smoothness { get; set; } = 0.5f;
        [DataMember] public float Metallic { get; set; } = 0.5f;
        [DataMember] public bool RenderAsOcclusionOnly { get; set; }
    }
}