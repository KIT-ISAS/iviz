#nullable enable

using System;
using System.Runtime.Serialization;
using Iviz.Common;
using Iviz.Core;
using Iviz.Core.Configurations;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Roslib.Utils;

namespace Iviz.Controllers
{
    [DataContract]
    public sealed class ARConfiguration : JsonToString, IConfiguration
    {
        [DataMember] public float WorldScale { get; set; } = 1.0f;
        [IgnoreDataMember] public Vector3 WorldOffset { get; set; } = ARController.DefaultWorldOffset.ToRos();

        [DataMember] public bool EnableQrDetection { get; set; } = true;
        [DataMember] public bool EnableArucoDetection { get; set; } = true;
        [DataMember] public bool EnableMeshing { get; set; } = false;
        [DataMember] public bool EnablePlaneDetection { get; set; } = true;
        [DataMember] public OcclusionQualityType OcclusionQuality { get; set; } = OcclusionQualityType.Fast;

        [IgnoreDataMember] public float WorldAngle { get; set; }
        [IgnoreDataMember] public bool ShowARJoystick { get; set; }
        [IgnoreDataMember] public bool PinRootMarker { get; set; }

        [DataMember] public string Id { get; set; } = nameof(ModuleType.AR);
        [DataMember] public ModuleType ModuleType => ModuleType.AR;
        [DataMember] public bool Visible { get; set; } = true;
    }
}