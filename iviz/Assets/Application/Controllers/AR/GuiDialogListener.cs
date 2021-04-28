using System;
using System.Collections.Generic;
using System.Linq;
using Iviz.App.ARDialogs;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Msgs.IvizCommonMsg;
using Iviz.Msgs.IvizCommonMsgs;
using Iviz.Resources;
using Iviz.Ros;
using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.Controllers
{
    public sealed class GuiDialogListener : ListenerController
    {
        readonly struct DialogInfo
        {
            public ARDialog Dialog { get; }
            public Info<GameObject> Info { get; }
            public DateTime ExpirationTime { get; }

            public DialogInfo(ARDialog dialog, Info<GameObject> info, DateTime expirationTime) =>
                (Dialog, Info, ExpirationTime) = (dialog, info, expirationTime);
        }


        public override TfFrame Frame => TfListener.Instance.FixedFrame;
        public override IModuleData ModuleData { get; }

        readonly GuiDialogConfiguration config = new GuiDialogConfiguration();

        readonly Dictionary<string, DialogInfo> dialogs = new Dictionary<string, DialogInfo>();

        public ISender FeedbackSender { get; private set; }

        public GuiDialogConfiguration Config
        {
            get => config;
            set
            {
                config.Topic = value.Topic;
                config.Type = value.Type;
            }
        }

        public override void StartListening()
        {
            switch (config.Type)
            {
                case GuiDialog.RosMessageType:
                    Listener = new Listener<GuiDialog>(config.Topic, Handler);
                    break;
                case GuiDialogArray.RosMessageType:
                    Listener = new Listener<GuiDialogArray>(config.Topic, Handler);
                    break;
            }

            FeedbackSender = new Sender<GuiDialogFeedback>(config.Topic + "/feedback");
        }

        public GuiDialogListener([NotNull] IModuleData moduleData)
        {
            ModuleData = moduleData ?? throw new ArgumentNullException(nameof(moduleData));
            Config = new GuiDialogConfiguration();

            GameThread.EverySecond += CheckDeadDialogs;
        }

        void Handler(GuiDialogArray msg)
        {
            foreach (var dialog in msg.Dialogs)
            {
                Handler(dialog);
            }
        }

        void Handler(GuiDialog msg)
        {
            switch (msg.Action)
            {
                case ActionType.Remove:
                {
                    if (dialogs.TryGetValue(msg.Id, out var dialog))
                    {
                        dialog.Dialog.Shutdown();
                        ResourcePool.Return(dialog.Info, dialog.Dialog.gameObject);
                        dialogs.Remove(msg.Id);
                    }

                    break;
                }
                case ActionType.RemoveAll:
                {
                    DestroyAllDialogs();
                    break;
                }
                case ActionType.Add:
                {
                    if (dialogs.TryGetValue(msg.Id, out var oldDialog))
                    {
                        oldDialog.Dialog.Shutdown();
                        ResourcePool.Return(oldDialog.Info, oldDialog.Dialog.gameObject);
                    }

                    Info<GameObject> info;
                    ARDialog dialog;

                    switch (msg.Type)
                    {
                        case DialogType.ButtonOk:
                        case DialogType.ButtonOkCancel:
                        case DialogType.ButtonYesNo:
                        case DialogType.ButtonForward:
                        case DialogType.ButtonForwardBackward:
                            info = msg.Icon == IconType.None
                                ? Resource.Displays.ARDialog
                                : Resource.Displays.ARDialogIcon;
                            dialog = ResourcePool.Rent<ARDialog>(info);
                            dialog.SetButtonMode(msg.Type);
                            if (msg.Icon != IconType.None)
                            {
                                dialog.SetIconMode(msg.Icon);
                            }

                            break;
                        case DialogType.Short:
                            info = Resource.Displays.ARDialogShort;
                            dialog = ResourcePool.Rent<ARDialog>(info);
                            break;
                        case DialogType.Notice:
                            info = Resource.Displays.ARDialogNotice;
                            dialog = ResourcePool.Rent<ARDialog>(info);
                            break;
                        case DialogType.MenuMode:
                            info = Resource.Displays.ARDialogMenu;
                            dialog = ResourcePool.Rent<ARDialog>(info);
                            dialog.MenuEntries = msg.MenuEntries;
                            break;
                        default:
                            return;
                    }

                    dialog.Id = msg.Id;
                    dialog.Listener = this;
                    dialog.Active = true;
                    dialog.Caption = msg.Caption;
                    dialog.CaptionAlignment = msg.CaptionAlignment;
                    dialog.Title = msg.Title;
                    dialog.Scale = msg.Scale;
                    dialog.PivotFrameId = msg.Header.FrameId;
                    dialog.PivotFrameOffset = msg.TfOffset.Ros2Unity();
                    dialog.PivotDisplacement = AdjustDisplacement(msg.TfDisplacement);
                    dialog.DialogDisplacement = AdjustDisplacement(msg.DialogDisplacement);
                    dialog.Initialize();

                    DateTime expirationTime = msg.Lifetime == default
                        ? DateTime.MaxValue
                        : GameThread.Now + msg.Lifetime.ToTimeSpan();

                    dialogs[msg.Id] = new DialogInfo(dialog, info, expirationTime);
                    break;
                }
            }
        }

        static Vector3 AdjustDisplacement(in Msgs.IvizMsgs.Vector3f displacement)
        {
            (float x, float y, float z) = displacement.ToUnity();
            return new Vector3(x, y, -z);
        }

        public override void ResetController()
        {
            base.ResetController();
            DestroyAllDialogs();
        }

        void DestroyAllDialogs()
        {
            foreach (var dialog in dialogs.Values)
            {
                dialog.Dialog.Shutdown();
                ResourcePool.Return(dialog.Info, dialog.Dialog.gameObject);
            }

            dialogs.Clear();
        }

        void CheckDeadDialogs()
        {
            var deadEntries = dialogs
                .Where(pair => pair.Value.ExpirationTime < GameThread.Now)
                .ToArray();
            foreach (var pair in deadEntries)
            {
                var dialog = pair.Value;
                dialog.Dialog.Shutdown();
                ResourcePool.Return(dialog.Info, dialog.Dialog.gameObject);
                dialogs.Remove(pair.Key);
            }
        }

        internal void OnDialogButtonClicked(ARDialog dialog, int button)
        {
            FeedbackSender.Publish(new GuiDialogFeedback
            {
                EngineId = ConnectionManager.MyId ?? "",
                DialogId = dialog.Id,
                FeedbackType = FeedbackType.ButtonClick,
                EntryId = button,
            });
        }

        internal void OnDialogMenuEntryClicked(ARDialog dialog, int entry)
        {
            FeedbackSender.Publish(new GuiDialogFeedback
            {
                EngineId = ConnectionManager.MyId ?? "",
                DialogId = dialog.Id,
                FeedbackType = FeedbackType.ButtonClick,
                EntryId = entry,
            });
        }

        public override void StopController()
        {
            base.StopController();
            GameThread.EverySecond += CheckDeadDialogs;
            DestroyAllDialogs();
        }
    }
}