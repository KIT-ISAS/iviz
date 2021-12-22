#nullable enable

using System;
using System.Runtime.Serialization;
using Iviz.Common;
using Iviz.MsgsWrapper;
using JetBrains.Annotations;

namespace Iviz.Msgs.IvizCommonMsgs
{
    public sealed class DialogArray : RosMessageWrapper<DialogArray>
    {
        [Preserve, MessageName] public const string RosMessageType = "iviz_msgs/DialogArray";

        [DataMember] public Dialog[] Dialogs { get; set; } = Array.Empty<Dialog>();
        [DataMember] public Widget[] Widgets { get; set; } = Array.Empty<Widget>();
    }
}