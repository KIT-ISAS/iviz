using System;
using System.Runtime.Serialization;
using Iviz.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UnityEngine;

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
        [DataMember] public SerializableColor GridColor { get; set; } = new Color(0.3f, 0.3f,0.3f);
        [DataMember] public SerializableColor InteriorColor { get; set; } = new Color(0.6f, 0.6f, 0.6f);
        [DataMember] public bool InteriorVisible { get; set; } = true;
        [DataMember] public bool FollowCamera { get; set; } = true;
        [DataMember] public bool HideInARMode { get; set; } = true;
        [DataMember] public bool Interactable { get; set; } = true;
        [DataMember] public SerializableVector3 Offset { get; set; } = Vector3.zero;
    }
}