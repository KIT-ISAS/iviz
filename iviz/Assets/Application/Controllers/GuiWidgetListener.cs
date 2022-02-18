#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using Iviz.Common.Configurations;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Displays.XR;
using Iviz.Msgs;
using Iviz.Msgs.IvizMsgs;
using Iviz.Resources;
using Iviz.Ros;
using Iviz.Tools;
using UnityEngine;
using Pose = Iviz.Msgs.GeometryMsgs.Pose;

namespace Iviz.Controllers
{
    public sealed class GuiWidgetListener : ListenerController
    {
        static GuiWidgetListener? defaultHandler;
        public static void DisposeDefaultHandler() => defaultHandler?.Dispose();

        public static GuiWidgetListener DefaultHandler =>
            defaultHandler ??= new GuiWidgetListener(null, "~dialogs");


        readonly GuiWidgetConfiguration config = new();
        readonly Dictionary<string, GuiObject> widgets = new();
        readonly Dictionary<string, GuiObject> dialogs = new();
        uint feedbackSeq;

        public override TfFrame Frame => TfModule.FixedFrame;
        public Sender<Feedback>? FeedbackSender { get; }
        public override IListener Listener { get; }

        public GuiWidgetConfiguration Config
        {
            get => config;
            private set
            {
                config.Topic = value.Topic;
                config.Id = value.Id;
                Visible = value.Visible;
                Interactable = value.Interactable;
            }
        }

        public override bool Visible
        {
            get => config.Visible;
            set
            {
                config.Visible = value;
                foreach (var widget in widgets.Values)
                {
                    widget.Visible = value;
                }
            }
        }

        public bool Interactable
        {
            get => config.Interactable;
            set
            {
                config.Interactable = value;
                foreach (var widget in widgets.Values)
                {
                    widget.Interactable = value;
                }
            }
        }

        public GuiWidgetListener(GuiWidgetConfiguration? configuration, string topic)
        {
            Config = configuration ?? new GuiWidgetConfiguration
            {
                Topic = topic,
                Id = topic
            };

            GameThread.EveryFrame += CheckDeadDialogs;

            Listener = new Listener<WidgetArray>(config.Topic, Handler) { MaxQueueSize = 50 };
            FeedbackSender = new Sender<Feedback>($"{config.Topic}/feedback");
        }

        void Handler(WidgetArray msg)
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

        void Handler(Widget msg)
        {
            switch ((ActionType)msg.Action)
            {
                case ActionType.Remove:
                {
                    if (widgets.TryGetValue(msg.Id, out var guiObject))
                    {
                        guiObject.Dispose();
                        widgets.Remove(msg.Id);
                    }

                    break;
                }
                case ActionType.RemoveAll:
                    DestroyAll(widgets);
                    break;
                case ActionType.Add:
                    HandleAddWidget(msg);
                    break;
                default:
                    RosLogger.Error($"{this}: Widget '{msg.Id}' requested unknown " +
                                    $"action {((int)msg.Action).ToString()}");
                    break;
            }
        }

        void HandleAddWidget(Widget msg)
        {
            if (string.IsNullOrEmpty(msg.Id))
            {
                RosLogger.Info($"{this}: Cannot add dialog with empty id");
                return;
            }

            if (msg.Color.IsInvalid() || msg.SecondaryColor.IsInvalid())
            {
                RosLogger.Info($"{this}: Color of widget '{msg.Id}' contains invalid values");
                return;
            }

            if (msg.Scale.IsInvalid() || msg.SecondaryScale.IsInvalid())
            {
                RosLogger.Info($"{this}: Scale of widget '{msg.Id}' contains invalid values");
                return;
            }

            if (msg.Pose.IsInvalid())
            {
                RosLogger.Info($"{this}: Pose of widget '{msg.Id}' contains invalid values");
                return;
            }

            var widgetType = (WidgetType)msg.Type;
            if (widgets.TryGetValue(msg.Id, out var existingGuiObject))
            {
                if (existingGuiObject.Type == widgetType)
                {
                    existingGuiObject.UpdateWidget(msg);
                    return;
                }
                
                RosLogger.Info($"{this}: Widget '{msg.Id}' of type {existingGuiObject.Type} " +
                                $"is being replaced with type {widgetType}");
                existingGuiObject.Dispose();
                // pass through
            }

            var resourceKey = widgetType switch
            {
                WidgetType.RotationDisc => Resource.Displays.RotationDisc,
                WidgetType.SpringDisc => Resource.Displays.SpringDisc,
                WidgetType.SpringDisc3D => Resource.Displays.SpringDisc3D,
                WidgetType.TrajectoryDisc => Resource.Displays.TrajectoryDisc,
                WidgetType.TargetArea => Resource.Displays.TargetArea,
                WidgetType.PositionDisc3D => Resource.Displays.PositionDisc3D,
                WidgetType.PositionDisc => Resource.Displays.PositionDisc,
                WidgetType.BoundaryCheck => Resource.Displays.BoundaryCheck,
                _ => null
            };

            if (resourceKey == null)
            {
                RosLogger.Error($"{this}: Widget '{msg.Id}' has unknown type {msg.Type.ToString()}");
                return;
            }


            var guiObject = new GuiObject(this, msg, resourceKey) { Interactable = Interactable };
            widgets[guiObject.Id] = guiObject;
        }

        public IDialog? AddDialog(Dialog msg)
        {
            Handler(msg);
            return (ActionType)msg.Action == ActionType.Add
                ? dialogs[msg.Id].As<IDialog>()
                : null;
        }

        void Handler(Dialog msg)
        {
            switch ((ActionType)msg.Action)
            {
                case ActionType.Remove:
                    if (dialogs.TryGetValue(msg.Id, out var guiObject))
                    {
                        guiObject.Dispose();
                        dialogs.Remove(msg.Id);
                    }

                    break;
                case ActionType.RemoveAll:
                    DestroyAll(dialogs);
                    break;
                case ActionType.Add:
                    HandleAddDialog(msg);
                    break;
                default:
                    RosLogger.Info($"{this}: Unknown action id {msg.Action.ToString()}");
                    break;
            }
        }

        void HandleAddDialog(Dialog msg)
        {
            if (string.IsNullOrEmpty(msg.Id))
            {
                RosLogger.Info($"{this}: Cannot add dialog with empty id");
                return;
            }

            if (msg.Icon > (byte)XRIcon.Question)
            {
                RosLogger.Info($"{this}: Dialog '{msg.Id}' has unknown icon id {msg.Action.ToString()}");
                return;
            }

            if (msg.Buttons > (byte)XRButtonSetup.Backward)
            {
                RosLogger.Info($"{this}: Dialog '{msg.Id}' has unknown button setup id {msg.Action.ToString()}");
                return;
            }

            if (msg.BackgroundColor.IsInvalid())
            {
                RosLogger.Info($"{this}: Color of dialog '{msg.Id}' contains invalid values");
                return;
            }

            if (msg.TfDisplacement.IsInvalid() || msg.TfOffset.IsInvalid() || msg.DialogDisplacement.IsInvalid())
            {
                RosLogger.Info($"{this}: One of the offset fields of dialog '{msg.Id}' contains invalid values");
                return;
            }

            var resourceKey = (DialogType)msg.Type switch
            {
                DialogType.Button => Resource.Displays.ARButtonDialog,
                DialogType.Notice => Resource.Displays.ARDialogNotice,
                DialogType.Plain => Resource.Displays.ARDialog,
                DialogType.Short => Resource.Displays.ARDialogShort,
                DialogType.Menu => Resource.Displays.ARDialogMenu,
                DialogType.Icon => Resource.Displays.ARDialogIcon,
                _ => null
            };

            if (resourceKey == null)
            {
                RosLogger.Error($"{this}: Dialog '{msg.Id}' has unknown type {msg.Type.ToString()}");
                return;
            }

            if (dialogs.TryGetValue(msg.Id, out var existingGuiObject))
            {
                if (msg.Lifetime.ToTimeSpan() < TimeSpan.Zero)
                {
                    MarkAsExpired(existingGuiObject);
                    return;
                }

                existingGuiObject.Dispose();
            }

            var guiObject = new GuiObject(this, msg, resourceKey);
            dialogs[guiObject.Id] = guiObject;
        }

        public override void ResetController()
        {
            base.ResetController();
            DestroyAll(dialogs);
            DestroyAll(widgets);
        }

        static void DestroyAll(Dictionary<string, GuiObject> dict)
        {
            foreach (var guiObject in dict.Values)
            {
                guiObject.Dispose();
            }

            dict.Clear();
        }

        void CheckDeadDialogs()
        {
            var now = GameThread.Now;

            List<GuiObject>? deadObjects = null;
            foreach (var guiObject in dialogs.Values)
            {
                if (guiObject.ExpirationTime > now)
                {
                    return;
                }

                deadObjects ??= new List<GuiObject>();
                deadObjects.Add(guiObject);
            }

            if (deadObjects == null)
            {
                return;
            }

            foreach (var guiObject in deadObjects)
            {
                guiObject.Dispose();
                dialogs.Remove(guiObject.Id);
            }
        }

        internal void OnDialogButtonClicked(GuiObject dialog, int buttonId)
        {
            FeedbackSender?.Publish(new Feedback
            {
                VizId = RosManager.MyId ?? "",
                Id = dialog.Id,
                Type = (byte)FeedbackType.ButtonClick,
                EntryId = buttonId,
            });

            MarkAsExpired(dialog);
        }

        internal void OnDialogMenuEntryClicked(GuiObject dialog, int buttonId)
        {
            FeedbackSender?.Publish(new Feedback
            {
                VizId = RosManager.MyId ?? "",
                Id = dialog.Id,
                Type = (byte)FeedbackType.MenuEntryClick,
                EntryId = buttonId,
            });

            MarkAsExpired(dialog);
        }

        void MarkAsExpired(GuiObject dialog)
        {
            dialogs[dialog.Id] = dialog.AsExpired();
        }

        internal void OnDialogExpired(GuiObject dialog)
        {
            FeedbackSender?.Publish(new Feedback
            {
                Header = (feedbackSeq++, dialog.ParentId),
                VizId = RosManager.MyId ?? "",
                Id = dialog.Id,
                Type = (byte)FeedbackType.Expired,
            });
        }

        internal void OnWidgetRotated(GuiObject widget, float angleInRad)
        {
            FeedbackSender?.Publish(new Feedback
            {
                Header = (feedbackSeq++, widget.ParentId),
                VizId = RosManager.MyId ?? "",
                Id = widget.Id,
                Type = (byte)FeedbackType.OrientationChanged,
                Orientation = Extensions.AngleAxis(angleInRad, default(VectorUnitZ))
            });
        }

        internal void OnWidgetMoved(GuiObject widget, in Vector3 direction)
        {
            FeedbackSender?.Publish(new Feedback
            {
                Header = (feedbackSeq++, widget.ParentId),
                VizId = RosManager.MyId ?? "",
                Id = widget.Id,
                Type = (byte)FeedbackType.PositionChanged,
                Position = direction.Unity2RosPoint()
            });
        }

        void OnTrajectoryDiscMoved(GuiObject widget, IReadOnlyList<Vector3> points, float periodInSec)
        {
            FeedbackSender?.Publish(new Feedback
            {
                Header = (feedbackSeq++, widget.ParentId),
                VizId = RosManager.MyId ?? "",
                Id = widget.Id,
                Type = (byte)FeedbackType.TrajectoryChanged,
                Trajectory = new Trajectory
                {
                    Poses = points
                        .Select(point => Pose.Identity.WithPosition(point.Unity2RosVector3()))
                        .ToArray(),
                    Timestamps = Enumerable.Range(0, points.Count)
                        .Select(i => SecsToTime(i * periodInSec))
                        .ToArray()
                }
            });
        }

        void OnTargetAreaMoved(GuiObject widget, in Vector2 scale, Vector3 position)
        {
            FeedbackSender?.Publish(new Feedback
            {
                Header = (feedbackSeq++, widget.ParentId),
                VizId = RosManager.MyId ?? "",
                Id = widget.Id,
                Type = (byte)FeedbackType.ScaleChanged,
                Scale = new Vector3(scale.x, 0, scale.y).Unity2RosVector3(),
                Position = position.Unity2RosPoint()
            });
        }

        void OnTargetAreaCanceled(GuiObject widget)
        {
            FeedbackSender?.Publish(new Feedback
            {
                VizId = RosManager.MyId ?? "",
                Id = widget.Id,
                Type = (byte)FeedbackType.ButtonClick,
                EntryId = -1,
            });
        }

        static time SecsToTime(float time)
        {
            uint numSecs = (uint)time;
            uint numNSecs = (uint)((time - numSecs) * 10_000_000);
            return new time(numSecs, numNSecs);
        }

        public override void Dispose()
        {
            base.Dispose();
            GameThread.EveryFrame -= CheckDeadDialogs;
            DestroyAll(dialogs);
            DestroyAll(widgets);
        }

        public static void ClearResources()
        {
            defaultHandler = null;
        }
    }


    public enum ActionType : byte
    {
        Add = Widget.ACTION_ADD,
        Remove = Widget.ACTION_REMOVE,
        RemoveAll = Widget.ACTION_REMOVEALL
    }

    public enum WidgetType : byte
    {
        RotationDisc = Widget.TYPE_ROTATIONDISC,
        SpringDisc = Widget.TYPE_SPRINGDISC,
        SpringDisc3D = Widget.TYPE_SPRINGDISC3D,
        TrajectoryDisc = Widget.TYPE_TRAJECTORYDISC,
        Tooltip = Widget.TYPE_TOOLTIP,
        TargetArea = Widget.TYPE_TARGETAREA,
        PositionDisc = Widget.TYPE_POSITIONDISC,
        PositionDisc3D = Widget.TYPE_POSITIONDISC3D,
        BoundaryCheck = Widget.TYPE_BOUNDARYCHECK,
    }

    public enum FeedbackType : byte
    {
        Expired = Feedback.TYPE_EXPIRED,
        ButtonClick = Feedback.TYPE_BUTTON_CLICK,
        MenuEntryClick = Feedback.TYPE_MENUENTRY_CLICK,
        PositionChanged = Feedback.TYPE_POSITION_CHANGED,
        OrientationChanged = Feedback.TYPE_ORIENTATION_CHANGED,
        ScaleChanged = Feedback.TYPE_SCALE_CHANGED,
        TrajectoryChanged = Feedback.TYPE_TRAJECTORY_CHANGED,
    }
}

/*
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
                    dialog.Scale = (float)msg.Scale;
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

 */

/*
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
        dialog.Scale = (float)msg.Scale;
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
        RosLogger.Info($"{this}: Unknown action id {((int)msg.Action).ToString()}");
        break;
}
}
*/


/*
case ActionType.Add when widgets.TryGetValue(msg.Id, out var tooltipData):
{
switch (msg.Type)
{
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
}

goto case ActionType.Add;
}
*/


/*
switch ((WidgetType)msg.Type)
{
    case WidgetType.RotationDisc:
        guiObject.As<RotationDisc>().Moved += angle => OnDiscRotated(guiObject, angle);
        break;
    case WidgetType.SpringDisc:
        guiObject.As<SpringDisc>().Moved +=
            direction => OnDiscMoved(guiObject, direction * guiObject.Scale);
        break;
    case WidgetType.SpringDisc3D:
        guiObject.As<SpringDisc3D>().Moved +=
            direction => OnDiscMoved(guiObject, direction * guiObject.Scale);
        break;
    case WidgetType.TrajectoryDisc:
        guiObject.As<TrajectoryDisc>().Moved += (direction, period) =>
            OnTrajectoryDiscMoved(guiObject, direction, period);
        break;
    case WidgetType.TargetArea:
        var targetWidget = guiObject.As<TargetWidget>();
        targetWidget.Moved += (scale, position) => OnTargetAreaMoved(guiObject, scale, position);
        targetWidget.Cancelled += () => OnTargetAreaCanceled(guiObject);
        break;
    case WidgetType.PositionDisc3D:
        guiObject.As<PositionDisc3D>().Moved +=
            direction => OnDiscMoved(guiObject, direction * guiObject.Scale);
        break;
    case WidgetType.PositionDisc:
        guiObject.As<PositionDisc>().Moved +=
            direction => OnDiscMoved(guiObject, direction * guiObject.Scale);
        break;
    case WidgetType.Tooltip:
    {
        info = Resource.Displays.Tooltip;
        var tooltip = ResourcePool.RentDisplay<Tooltip>();
        tooltip.Caption = msg.Caption;
        //widget = tooltip;
        break;
    }
}
*/