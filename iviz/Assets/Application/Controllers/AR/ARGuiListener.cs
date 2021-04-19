using System.Collections.Generic;
using System.Runtime.Serialization;
using Iviz.App.ARDialogs;
using Iviz.Msgs.IvizCommonMsgs;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Msgs.GridMapMsgs;
using Iviz.Msgs.IvizMsgs;
using Iviz.MsgsWrapper;
using Iviz.Resources;
using Iviz.Ros;
using Iviz.Roslib.Utils;
using JetBrains.Annotations;
using UnityEngine.Scripting;

namespace Iviz.Controllers
{
    [DataContract]
    public class ARGuiConfiguration : RosMessageWrapper<ARGuiConfiguration>, IConfiguration
    {
        [Preserve, MessageName] public const string RosMessageType = "iviz_msgs/ARGuiConfiguration";

        [DataMember] public string Id { get; set; } = System.Guid.NewGuid().ToString();
        [DataMember] public Resource.ModuleType ModuleType => Resource.ModuleType.ARGuiSystem;
        [DataMember] public bool Visible { get; set; } = true;
        [DataMember] public string Topic { get; set; } = "";
    }

    public class IvizDialog : RosMessageWrapper<IvizDialog>
    {
        [Preserve, MessageName] public const string RosMessageType = "iviz_msgs/Dialog";

        public const byte ActionAdd = 0;
        public const byte ActionRemove = 1;
        public const byte ActionRemoveAll = 2;

        public byte Action { get; set; }
        public string Id { get; set; } = "";

        public const byte ModeOk = 0;
        public const byte ModeYesNo = 1;
        public const byte ModeOkCancel = 2;

        public const byte ModeInfo = 10;
        public const byte ModeWarn = 11;
        public const byte ModeError = 12;

        public const byte ModeMenu = 20;

        public byte Mode { get; set; }
        public string Title { get; set; } = "";
        public string Caption { get; set; } = "";

        public const byte BindNone = 0;
        public const byte BindToTf = 1;
        public const byte BindToUser = 2;

        public byte BindingType { get; set; }
        public string TfPivot { get; set; } = "";
        public Vector3f TfOffset { get; set; }
        public Vector3f DialogDisplacement { get; set; }
    }

    public class IvizDialogFeedback : RosMessageWrapper<IvizDialogFeedback>
    {
        [Preserve, MessageName] public const string RosMessageType = "iviz_msgs/DialogFeedback";
        public string Id { get; set; } = "";
    }

    public sealed class ARGuiListener : ListenerController
    {
        public override TfFrame Frame => TfListener.Instance.FixedFrame;
        public override IModuleData ModuleData { get; }

        readonly ARGuiConfiguration config = new ARGuiConfiguration();

        readonly Dictionary<string, ARDialog> dialogs = new Dictionary<string, ARDialog>();

        public ISender FeedbackSender { get; private set; }

        public ARGuiConfiguration Config
        {
            get => config;
            set { }
        }

        public override void StartListening()
        {
            Listener = new Listener<IvizDialog>(config.Topic, Handler);
            FeedbackSender = new Sender<IvizDialogFeedback>(config.Topic + "/feedback");
        }

        public ARGuiListener([NotNull] IModuleData moduleData)
        {
            ModuleData = moduleData ?? throw new System.ArgumentNullException(nameof(moduleData));
            Config = new ARGuiConfiguration();
        }

        void Handler(IvizDialog msg)
        {
            switch (msg.Action)
            {
                case IvizDialog.ActionRemove:
                {
                    if (dialogs.TryGetValue(msg.Id, out var dialog))
                    {
                        dialog.Shutdown();
                        ResourcePool.ReturnDisplay(dialog);
                        dialogs.Remove(msg.Id);
                    }

                    break;
                }
                case IvizDialog.ActionRemoveAll:
                {
                    foreach (var dialog in dialogs.Values)
                    {
                        dialog.Shutdown();
                        ResourcePool.ReturnDisplay(dialog);
                    }

                    dialogs.Clear();
                    break;
                }
                case IvizDialog.ActionAdd:
                {
                    if (dialogs.TryGetValue(msg.Id, out var oldDialog))
                    {
                        oldDialog.Shutdown();
                        ResourcePool.ReturnDisplay(oldDialog);
                    }

                    var dialog = ResourcePool.RentDisplay<ARDialog>();

                    dialog.Active = true;
                    dialog.Caption = msg.Caption;
                    dialog.Title = msg.Title;
                    dialog.Mode = (ARDialog.ModeType) msg.Mode;
                    dialog.PivotFrameId = msg.TfPivot;
                    dialog.PivotFrameOffset = msg.TfOffset.ToVector3();
                    dialog.DialogDisplacement = msg.DialogDisplacement.ToVector3();

                    dialogs[msg.Id] = dialog;
                    break;
                }
            }
        }
    }
}