using System;
using System.Collections.Generic;
using System.Linq;
using Iviz.App.ARDialogs;
using Iviz.Common;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Msgs;
using Iviz.Msgs.IvizCommonMsgs;
using Iviz.Resources;
using Iviz.Ros;
using JetBrains.Annotations;
using UnityEngine;
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

            public void Deconstruct(out T obj, out Info<GameObject> info, out DateTime expirationTime) =>
                (obj, info, expirationTime) = (Object, Info, ExpirationTime);
        }

        static GuiDialogListener defaultHandler;

        [NotNull]
        public static GuiDialogListener DefaultHandler => defaultHandler ??= new GuiDialogListener();
        
        [NotNull] public override TfFrame Frame => TfListener.Instance.FixedFrame;

        [CanBeNull] readonly IModuleData moduleData;

        public override IModuleData ModuleData =>
            moduleData ?? throw new InvalidOperationException("Listener has no module data");

        readonly GuiDialogConfiguration config = new GuiDialogConfiguration();

        readonly Dictionary<string, Data<ARDialog>> dialogs = new Dictionary<string, Data<ARDialog>>();
        readonly Dictionary<string, Data<ARWidget>> widgets = new Dictionary<string, Data<ARWidget>>();

        uint feedbackSeq = 0;

        [CanBeNull] public Sender<Feedback> FeedbackSender { get; private set; }

        public bool Interactable { get; set; }
        
        public GuiDialogConfiguration Config
        {
            get => config;
            set
            {
                config.Topic = value.Topic;
                config.Type = value.Type;
            }
        }
        
        public void StartListening()
        {
            Listener = config.Type switch
            {
                Dialog.RosMessageType => new Listener<Dialog>(config.Topic, Handler) { MaxQueueSize = 50 },
                GuiArray.RosMessageType => new Listener<GuiArray>(config.Topic, Handler) { MaxQueueSize = 50 },
                _ => Listener
            };

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

        void Handler([NotNull] GuiArray msg)
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

        void Handler([NotNull] Widget msg)
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
                case ActionType.Add when widgets.TryGetValue(msg.Id, out var tooltipData):
                {
                    switch (msg.Type)
                    {
                        /*
                        case WidgetType.Tooltip when tooltipData.Object is Tooltip widget:
                            widget.AttachTo(msg.Header.FrameId);
                            widget.Transform.SetLocalPose(msg.Pose.Ros2Unity());
                            widget.transform.localScale = (float) msg.Scale * Vector3.one;
                            widget.Caption = msg.Caption;
                            if (msg.MainColor.A != 0)
                            {
                                widget.MainColor = msg.MainColor.ToUnityColor();
                            }

                            return;
                            */
                    }

                    goto case ActionType.Add;
                }
                case ActionType.Add:
                {
                    if (widgets.TryGetValue(msg.Id, out var oldData))
                    {
                        oldData.Object.Suspend();
                        ResourcePool.Return(oldData.Info, oldData.Object.gameObject);
                    }

                    Info<GameObject> info;
                    ARWidget widget;
                    switch (msg.Type)
                    {
                        case WidgetType.RotationDisc:
                        {
                            info = Resource.Displays.RotationDisc;
                            var disc = ResourcePool.RentDisplay<RotationDisc>();
                            disc.Moved += OnDiscRotated;
                            widget = disc;
                            break;
                        }
                        case WidgetType.SpringDisc:
                        {
                            info = Resource.Displays.SpringDisc;
                            var disc = ResourcePool.RentDisplay<SpringDisc>();
                            disc.Moved += OnDiscMoved;
                            widget = disc;
                            break;
                        }
                        case WidgetType.SpringDisc3D:
                        {
                            info = Resource.Displays.SpringDisc3D;
                            var disc = ResourcePool.RentDisplay<SpringDisc3D>();
                            disc.Moved += OnDiscMoved;
                            widget = disc;
                            break;
                        }
                        case WidgetType.TrajectoryDisc:
                        {
                            info = Resource.Displays.TrajectoryDisc;
                            var disc = ResourcePool.RentDisplay<TrajectoryDisc>();
                            disc.Moved += OnTrajectoryDiscMoved;
                            if (!string.IsNullOrEmpty(msg.Caption))
                            {
                                disc.MainButtonCaption = msg.Caption;
                            }

                            widget = disc;
                            break;
                        }
                        /*
                        case WidgetType.Tooltip:
                        {
                            info = Resource.Displays.Tooltip;
                            var tooltip = ResourcePool.RentDisplay<Tooltip>();
                            tooltip.Caption = msg.Caption;
                            //widget = tooltip;
                            break;
                        }
                        */
                        case WidgetType.TargetArea:
                        {
                            info = Resource.Displays.TargetArea;
                            var area = ResourcePool.RentDisplay<TargetWidget>();
                            area.Moved += OnTargetAreaMoved;
                            area.Cancelled += OnTargetAreaCanceled;
                            if (!string.IsNullOrEmpty(msg.Caption))
                            {
                                area.MainButtonCaption = msg.Caption;
                            }

                            widget = area;
                            break;
                        }
                        case WidgetType.PositionDisc3D:
                        {
                            info = Resource.Displays.PositionDisc3D;
                            var disc = ResourcePool.RentDisplay<PositionDisc3D>();
                            disc.Moved += OnDiscMoved;
                            if (!string.IsNullOrEmpty(msg.Caption))
                            {
                                disc.MainButtonCaption = msg.Caption;
                            }

                            widget = disc;
                            break;
                        }
                        case WidgetType.PositionDisc:
                        {
                            info = Resource.Displays.PositionDisc;
                            var disc = ResourcePool.RentDisplay<PositionDisc>();
                            disc.Moved += OnDiscMoved;
                            if (!string.IsNullOrEmpty(msg.Caption))
                            {
                                disc.MainButtonCaption = msg.Caption;
                            }

                            widget = disc;
                            break;
                        }
                        default:
                            RosLogger.Error($"{this}: Widget '{msg.Id}' has unknown type {((int) msg.Type).ToString()}");
                            return;
                    }

                    widget.Id = msg.Id;
                    widget.AttachTo(msg.Header.FrameId);
                    widget.Transform.SetLocalPose(msg.Pose.Ros2Unity());
                    widget.Scale = (float) msg.Scale;
                    if (msg.MainColor.A != 0)
                    {
                        widget.MainColor = msg.MainColor.ToUnityColor();
                    }

                    widgets[msg.Id] = new Data<ARWidget>(widget, info, DateTime.MaxValue);
                    widget.Initialize();
                    break;
                }
                default:
                    RosLogger.Error($"{this}: Widget '{msg.Id}' requested unknown action {((int) msg.Action).ToString()}");
                    break;
            }
        }


        [CanBeNull]
        public ARDialog AddDialog([NotNull] Dialog msg)
        {
            Handler(msg);
            return msg.Action == ActionType.Add
                ? dialogs[msg.Id].Object
                : null;
        }

        void Handler([NotNull] Dialog msg)
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
                    RosLogger.Info($"{this}: Unknown action id {((int) msg.Action).ToString()}");
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

        public override bool Visible
        {
            get => false;
            set { }
        }

        static void DestroyAll<T>([NotNull] Dictionary<string, Data<T>> dict) where T : MonoBehaviour, IDisplay
        {
            foreach (var (display, info, _) in dict.Values)
            {
                display.Suspend();
                ResourcePool.Return(info, display.gameObject);
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
                var (arDialog, info, _) = pair.Value;
                arDialog.Suspend();
                ResourcePool.Return(info, arDialog.gameObject);
                dialogs.Remove(pair.Key);

                OnDialogExpired(arDialog);
            }
        }

        void OnDialogButtonClicked([NotNull] ARDialog dialog, int buttonId)
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

        void OnDialogMenuEntryClicked([NotNull] ARDialog dialog, int buttonId)
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

        void MakeExpired([NotNull] string dialogId)
        {
            if (dialogs.TryGetValue(dialogId, out var entry))
            {
                dialogs[dialogId] = new Data<ARDialog>(entry.Object, entry.Info, DateTime.MinValue);
            }
        }

        void OnDialogExpired([NotNull] ARDialog dialog)
        {
            FeedbackSender?.Publish(new Feedback
            {
                Header = (feedbackSeq++, dialog.ParentFrame.Id),
                VizId = ConnectionManager.MyId ?? "",
                Id = dialog.Id,
                FeedbackType = FeedbackType.Expired,
            });
        }


        void OnDiscRotated([NotNull] ARWidget widget, float angleInDeg)
        {
            FeedbackSender?.Publish(new Feedback
            {
                Header = (feedbackSeq++, widget.ParentFrame.Id),
                VizId = ConnectionManager.MyId ?? "",
                Id = widget.Id,
                FeedbackType = FeedbackType.OrientationChanged,
                Orientation = Quaternion.AngleAxis(angleInDeg * Mathf.Deg2Rad, Msgs.GeometryMsgs.Vector3.UnitZ)
            });
        }

        void OnDiscMoved(ARWidget widget, Vector3 direction)
        {
            FeedbackSender?.Publish(new Feedback
            {
                Header = (feedbackSeq++, widget.ParentFrame.Id),
                VizId = ConnectionManager.MyId ?? "",
                Id = widget.Id,
                FeedbackType = FeedbackType.PositionChanged,
                Position = direction.Unity2RosPoint()
            });
        }

        void OnTrajectoryDiscMoved(ARWidget widget, Vector3[] points, float periodInSec)
        {
            FeedbackSender?.Publish(new Feedback
            {
                Header = (feedbackSeq++, widget.ParentFrame.Id),
                VizId = ConnectionManager.MyId ?? "",
                Id = widget.Id,
                FeedbackType = FeedbackType.TrajectoryChanged,
                Trajectory = new Trajectory
                {
                    Poses = points
                        .Select(point => Msgs.GeometryMsgs.Pose.Identity.WithPosition(point.Unity2RosVector3()))
                        .ToArray(),
                    Timestamps = Enumerable.Range(0, points.Length)
                        .Select(i => SecsToTime(i * periodInSec))
                        .ToArray()
                }
            });
        }

        void OnTargetAreaMoved(TargetWidget widget, Vector2 scale, Vector3 position)
        {
            FeedbackSender?.Publish(new Feedback
            {
                Header = (feedbackSeq++, widget.ParentFrame.Id),
                VizId = ConnectionManager.MyId ?? "",
                Id = widget.Id,
                FeedbackType = FeedbackType.ScaleChanged,
                Scale = new Vector3(scale.x, 0, scale.y).Unity2RosVector3(),
                Position = position.Unity2RosPoint()
            });
        }

        void OnTargetAreaCanceled([NotNull] TargetWidget widget)
        {
            FeedbackSender?.Publish(new Feedback
            {
                VizId = ConnectionManager.MyId ?? "",
                Id = widget.Id,
                FeedbackType = FeedbackType.ButtonClick,
                EntryId = -1,
            });
        }

        static time SecsToTime(float time)
        {
            uint numSecs = (uint) time;
            uint numNSecs = (uint) ((time - numSecs) * 10000000);
            return new time(numSecs, numNSecs);
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