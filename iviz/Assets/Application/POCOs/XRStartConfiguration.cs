#nullable enable

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Iviz.Common;
using Iviz.Common.Configurations;
using Iviz.Core;
using Iviz.Roslib.Utils;

namespace Iviz.App
{
    [DataContract]
    public sealed class XRStartConfiguration : JsonToString
    {
        [DataMember] public SerializableVector3 AnchorPosition { get; set; }
        [DataMember] public SerializableQuaternion AnchorOrientation { get; set; }
    }
}