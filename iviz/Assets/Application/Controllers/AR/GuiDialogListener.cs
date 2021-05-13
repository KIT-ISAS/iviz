using System;
using System.Collections.Generic;
using System.Linq;
using Iviz.App.ARDialogs;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Msgs.IvizCommonMsgs;
using Iviz.Resources;
using Iviz.Ros;
using JetBrains.Annotations;
using UnityEngine;
using Logger = Iviz.Core.Logger;
using Quaternion = Iviz.Msgs.GeometryMsgs.Quaternion;

namespace Iviz.Controllers
{
    public sealed class GuiDialogListener : ListenerController
    {
        readonly struct Data<T> where T : MonoBehaviour, IDisplay
        {
            public T Object { get; }
            public Info<GameObject> Info { get; }
            public DateTime ExpirationTime { get; }

            public Data(T obj, Info<GameObject> info, DateTime expirationTime) =>
                (Object, Info, ExpirationTime) = (obj, info, expirationTime);
        }

        static GuiDialogListener defaultHandler;
        public static GuiDialogListener DefaultHandler => defaultHandler ?? (defaultHandler = new GuiDialogListener());


        public override TfFrame Frame => TfListener.Instance.FixedFrame;

        [CanBeNull] IModuleData moduleData;

        public override IModuleData ModuleData =>
            moduleData ?? throw new InvalidOperationException("Listener has no module data");

        readonly GuiDialogConfiguration config = new GuiDialogConfiguration();

        readonly Dictionary<string, Data<ARDialog>> dialogs = new Dictionary<string, Data<ARDialog>>();
        readonly Dictionary<string, Data<ARWidget>> widgets = new Dictionary<string, Data<ARWidget>>();

        [CanBeNull] public ISender FeedbackSender { get; private set; }

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
                case Dialog.RosMessageType:
                    Listener = new Listener<Dialog>(config.Topic, Handler) {MaxQueueSize = 50};
                    break;
                case GuiArray.RosMessageType:
                    Listener = new Listener<GuiArray>(config.Topic, Handler) {MaxQueueSize = 50};
                    break;
            }

            FeedbackSender = new Sender<Feedback>($"{config.Topic}/feedback");
        }

        public GuiDialogListener([NotNull] IModuleData moduleData)
        {
            this.moduleData = moduleData;
            Config = new GuiDialogConfiguration();

            GameThread.EveryFrame += CheckDeadDialogs;
        }

        GuiDialogListener()
        {
            moduleData = null;
            Config = new GuiDialogConfiguration();

            GameThread.EveryFrame += CheckDeadDialogs;
        }

        void Handler(GuiArray msg)
        {
            foreach (var dialog in msg.Dialogs)
            {
                Handler(dialog);
            }

            foreach (var widget in msg.Widgets)
            {
                Handler(widget);
            }
        }

        public void Handler(Widget msg)
        {
            switch (msg.Action)
            {
                case ActionType.Remove:
                {
                    if (dialogs.TryGetValue(msg.Id, out var dialog))
                    {
                        dialog.Object.Suspend();
                        ResourcePool.Return(dialog.Info, dialog.Object.gameObject);
                        dialogs.Remove(msg.Id);
                    }

                    break;
                }
                case ActionType.RemoveAll:
                {
                    DestroyAll(widgets);
                    break;
                }
                case ActionType.Add:
                {
                    if (widgets.TryGetValue(msg.Id, out var oldWidget))
                    {
                        oldWidget.Object.Suspend();
                        ResourcePool.Return(oldWidget.Info, oldWidget.Object.gameObject);
                    }

                    Info<GameObject> info;
                    ARWidget widget;
                    switch (msg.Type)
                    {
                        case WidgetType.RotationDisc:
                        {
                            info = Resource.Displays.RotationDisc;
                            var disc = ResourcePool.RentDisplay<RotationDisc>();
                            disc.Moved += OnRotationDiscMoved;
                            widget = disc;
                            break;
                        }
                        case WidgetType.SpringDisc:
                        {
                            info = Resource.Displays.SpringDisc;
                            var disc = ResourcePool.RentDisplay<SpringDisc>();
                            disc.Moved += OnSpringDiscMoved;
                            widget = disc;
                            break;
                        }
                        default:
                            return;
                    }

                    widget.Id = msg.Id;
                    widget.AttachTo(msg.Header.FrameId);
                    widget.Transform.SetLocalPose(msg.Pose.Ros2Unity());
                    widget.transform.localScale = (float) msg.Scale * Vector3.one;
                    widgets[msg.Id] = new Data<ARWidget>(widget, info, DateTime.MaxValue);
                    widget.Initialize();
                    break;
                }
                default:
                    Logger.Info($"{this}: Unknown action id {((int) msg.Action).ToString()}");
                    break;
            }
        }


        [CanBeNull]
        public ARDialog AddDialog(Dialog msg)
        {
            Handler(msg);
            return msg.Action == ActionType.Add
                ? dialogs[msg.Id].Object
                : null;
        }

        void Handler(Dialog msg)
        {
            switch (msg.Action)
            {
                case ActionType.Remove:
                {
                    if (dialogs.TryGetValue(msg.Id, out var dialog))
                    {
                        dialog.Object.Suspend();
                        ResourcePool.Return(dialog.Info, dialog.Object.gameObject);
                        dialogs.Remove(msg.Id);
                    }

                    break;
                }
                case ActionType.RemoveAll:
                {
                    DestroyAll(dialogs);
                    break;
                }
                case ActionType.Add:
                {
                    if (dialogs.TryGetValue(msg.Id, out var oldDialog))
                    {
                        oldDialog.Object.Suspend();
                        ResourcePool.Return(oldDialog.Info, oldDialog.Object.gameObject);
                    }

                    Info<GameObject> info;
                    ARDialog dialog;

                    switch (msg.Type)
                    {
                        case DialogType.Dialog:
                            info = msg.Icon == IconType.None
                                ? Resource.Displays.ARDialog
                                : Resource.Displays.ARDialogIcon;
                            dialog = ResourcePool.Rent<ARDialog>(info);
                            dialog.SetButtonMode(msg.Buttons);
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
                            dialog.SetIconMode(msg.Icon);
                            break;
                        case DialogType.Button:
                            info = Resource.Displays.ARButtonDialog;
                            dialog = ResourcePool.Rent<ARDialog>(info);
                            dialog.SetButtonMode(msg.Buttons);
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
                    dialog.ButtonClicked += OnDialogButtonClicked;
                    dialog.MenuEntryClicked += OnDialogMenuEntryClicked;
                    dialog.Active = true;
                    dialog.Caption = msg.Caption;
                    dialog.CaptionAlignment = msg.CaptionAlignment;
                    dialog.Title = msg.Title;
                    dialog.Scale = (float) msg.Scale;
                    dialog.BackgroundColor = msg.BackgroundColor.ToUnityColor();
                    dialog.PivotFrameId = msg.Header.FrameId;
                    dialog.PivotFrameOffset = msg.TfOffset.Ros2Unity();
                    dialog.PivotDisplacement = AdjustDisplacement(msg.TfDisplacement);
                    dialog.DialogDisplacement = AdjustDisplacement(msg.DialogDisplacement);
                    dialog.Initialize();

                    DateTime expirationTime = msg.Lifetime == default
                        ? DateTime.MaxValue
                        : GameThread.Now + msg.Lifetime.ToTimeSpan();

                    dialogs[msg.Id] = new Data<ARDialog>(dialog, info, expirationTime);
                    break;
                }
                default:
                    Logger.Info($"{this}: Unknown action id {((int) msg.Action).ToString()}");
                    break;
            }
        }

        static Vector3 AdjustDisplacement(in Msgs.GeometryMsgs.Vector3 displacement)
        {
            (float x, float y, float z) = displacement.ToUnity();
            return new Vector3(x, y, -z);
        }

        public override void ResetController()
        {
            base.ResetController();
            DestroyAll(dialogs);
            DestroyAll(widgets);
        }

        static void DestroyAll<T>(Dictionary<string, Data<T>> dict) where T : MonoBehaviour, IDisplay
        {
            foreach (var dialog in dict.Values)
            {
                dialog.Object.Suspend();
                ResourcePool.Return(dialog.Info, dialog.Object.gameObject);
            }

            dict.Clear();
        }

        void CheckDeadDialogs()
        {
            var deadEntries = dialogs
                .Where(pair => pair.Value.ExpirationTime < GameThread.Now)
                .ToArray();
            foreach (var pair in deadEntries)
            {
                var dialog = pair.Value;
                dialog.Object.Suspend();
                ResourcePool.Return(dialog.Info, dialog.Object.gameObject);
                dialogs.Remove(pair.Key);

                OnDialogExpired(dialog.Object);
            }
        }

        void OnDialogButtonClicked(ARDialog dialog, int buttonId)
        {
            FeedbackSender?.Publish(new Feedback
            {
                VizId = ConnectionManager.MyId ?? "",
                Id = dialog.Id,
                FeedbackType = FeedbackType.ButtonClick,
                EntryId = buttonId,
            });
            
            MakeExpired(dialog.Id);
        }

        void OnDialogMenuEntryClicked(ARDialog dialog, int buttonId)
        {
            FeedbackSender?.Publish(new Feedback
            {
                VizId = ConnectionManager.MyId ?? "",
                Id = dialog.Id,
                FeedbackType = FeedbackType.MenuEntryClick,
                EntryId = buttonId,
            });
            
            MakeExpired(dialog.Id);
        }

        void MakeExpired(string dialogId)
        {
            if (dialogs.TryGetValue(dialogId, out var entry))
            {
                dialogs[dialogId] = new Data<ARDialog>(entry.Object, entry.Info, DateTime.MinValue);
            }
        }

        void OnDialogExpired(ARDialog dialog)
        {
            FeedbackSender?.Publish(new Feedback
            {
                VizId = ConnectionManager.MyId ?? "",
                Id = dialog.Id,
                FeedbackType = FeedbackType.Expired,
            });
        }


        void OnRotationDiscMoved(ARWidget widget, float angleInDeg)
        {
            FeedbackSender?.Publish(new Feedback
            {
                VizId = ConnectionManager.MyId ?? "",
                Id = widget.Id,
                FeedbackType = FeedbackType.AngleChanged,
                Orientation = Quaternion.AngleAxis(angleInDeg * Mathf.Deg2Rad, Msgs.GeometryMsgs.Vector3.UnitZ)
            });
        }

        void OnSpringDiscMoved(ARWidget widget, Vector3 direction)
        {
            FeedbackSender?.Publish(new Feedback
            {
                VizId = ConnectionManager.MyId ?? "",
                Id = widget.Id,
                FeedbackType = FeedbackType.PositionChanged,
                Position = direction.Unity2RosVector3()
            });
        }

        public override void StopController()
        {
            base.StopController();
            GameThread.EveryFrame -= CheckDeadDialogs;
            DestroyAll(dialogs);
            DestroyAll(widgets);
        }

        public static void ClearResources()
        {
            defaultHandler = null;
        }
    }
}