#nullable enable

using System;
using System.Runtime.Serialization;
using Iviz.Common;
using Iviz.Common.Configurations;
using Iviz.Core;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Roslib.Utils;

namespace Iviz.Controllers
{
    [DataContract]
    public sealed class ARConfiguration : JsonToString, IConfiguration
    {
        [IgnoreDataMember] public float WorldScale { get; set; } = 1.0f;
        [IgnoreDataMember] public Vector3 WorldOffset { get; set; } = ARController.DefaultWorldOffset.ToRos();

        [DataMember] public bool EnableQrDetection { get; set; } = true;
        [DataMember] public bool EnableArucoDetection { get; set; } = true;
        [DataMember] public bool EnableMeshing { get; set; } = true;
        [DataMember] public bool EnablePlaneDetection { get; set; } = true;
        [DataMember] public OcclusionQualityType OcclusionQuality { get; set; } = OcclusionQualityType.Fast;
        [DataMember] public PublicationFrequency PublicationFrequency { get; set; } = PublicationFrequency.Fps15;

        [IgnoreDataMember] public float WorldAngle { get; set; }
        [IgnoreDataMember] public bool ShowARJoystick { get; set; }
        [IgnoreDataMember] public bool PinRootMarker { get; set; }

        [DataMember] public string Id { get; set; } = Guid.NewGuid().ToString();
        [DataMember] public ModuleType ModuleType => ModuleType.AugmentedReality;
        [DataMember] public bool Visible { get; set; } = true;
    }
}