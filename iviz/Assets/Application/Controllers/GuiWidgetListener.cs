#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using Iviz.App.ARDialogs;
using Iviz.Common;
using Iviz.Common.Configurations;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Displays.ARDialogs;
using Iviz.Msgs;
using Iviz.Msgs.IvizCommonMsgs;
using Iviz.Resources;
using Iviz.Ros;
using Iviz.Tools;
using UnityEngine;
using Quaternion = Iviz.Msgs.GeometryMsgs.Quaternion;

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

        public override TfFrame Frame => TfListener.Instance.FixedFrame;
        public Sender<Feedback>? FeedbackSender { get; }
        public bool Interactable { get; set; }
        public override IListener Listener { get; }

        public GuiWidgetConfiguration Config
        {
            get => config;
            set { config.Topic = value.Topic; }
        }

        public GuiWidgetListener(GuiWidgetConfiguration? configuration, string topic)
        {
            Config = configuration ?? new GuiWidgetConfiguration
            {
                Topic = topic
            };

            GameThread.EveryFrame += CheckDeadDialogs;

            Listener = new Listener<DialogArray>(config.Topic, Handler) { MaxQueueSize = 50 };
            FeedbackSender = new Sender<Feedback>($"{config.Topic}/feedback");
        }

        void Handler(DialogArray msg)
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
            switch (msg.Action)
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
                    RosLogger.Error(
                        $"{this}: Widget '{msg.Id}' requested unknown action {((int)msg.Action).ToString()}");
                    break;
            }
        }

        void HandleAddWidget(Widget msg)
        {
            if (widgets.TryGetValue(msg.Id, out var oldGuiObject))
            {
                oldGuiObject.Dispose();
            }

            var info = msg.Type switch
            {
                WidgetType.RotationDisc => Resource.Displays.RotationDisc,
                WidgetType.SpringDisc => Resource.Displays.SpringDisc,
                WidgetType.SpringDisc3D => Resource.Displays.SpringDisc3D,
                WidgetType.TrajectoryDisc => Resource.Displays.TrajectoryDisc,
                WidgetType.TargetArea => Resource.Displays.TargetArea,
                WidgetType.PositionDisc3D => Resource.Displays.PositionDisc3D,
                WidgetType.PositionDisc => Resource.Displays.PositionDisc,
                _ => null
            };

            if (info == null)
            {
                RosLogger.Error($"{this}: Widget '{msg.Id}' has unknown type {((int)msg.Type).ToString()}");
                return;
            }

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

            var guiObject = new GuiObject(msg, info);

            switch (msg.Type)
            {
                case WidgetType.RotationDisc:
                    guiObject.Get<RotationDisc>().Moved += angle => OnDiscRotated(guiObject, angle);
                    break;
                case WidgetType.SpringDisc:
                case WidgetType.SpringDisc3D:
                    guiObject.Get<SpringDisc>().Moved += direction => OnDiscMoved(guiObject, direction);
                    break;
                case WidgetType.TrajectoryDisc:
                    guiObject.Get<TrajectoryDisc>().Moved += (direction, period) =>
                        OnTrajectoryDiscMoved(guiObject, direction, period);
                    break;
                case WidgetType.TargetArea:
                    var targetWidget = guiObject.Get<TargetWidget>();
                    targetWidget.Moved += (scale, position) => OnTargetAreaMoved(guiObject, scale, position);
                    targetWidget.Cancelled += () => OnTargetAreaCanceled(guiObject);
                    break;
                case WidgetType.PositionDisc3D:
                    guiObject.Get<PositionDisc3D>().Moved += direction => OnDiscMoved(guiObject, direction);
                    break;
                case WidgetType.PositionDisc:
                    guiObject.Get<PositionDisc>().Moved += direction => OnDiscMoved(guiObject, direction);
                    break;
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
            }

            guiObject.Transform.SetLocalPose(msg.Pose.Ros2Unity());
            guiObject.Transform.localScale = Vector3.one * (float)msg.Scale;
            widgets[guiObject.Id] = guiObject;
        }


        public ARDialog? AddDialog(Dialog msg)
        {
            Handler(msg);
            return msg.Action == ActionType.Add
                ? dialogs[msg.Id].Get<ARDialog>()
                : null;
        }

        void Handler(Dialog msg)
        {
            /*
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
            */
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
            var deadEntries = dialogs.Values
                .Where(pair => pair.ExpirationTime < GameThread.Now)
                .ToArray();
            foreach (var guiObject in deadEntries)
            {
                guiObject.Dispose();
                dialogs.Remove(guiObject.Id);
                OnDialogExpired(guiObject);
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
                dialogs[dialogId] = entry.AsExpired();
            }
        }

        void OnDialogExpired(GuiObject dialog)
        {
            FeedbackSender?.Publish(new Feedback
            {
                Header = (feedbackSeq++, dialog.ParentId),
                VizId = ConnectionManager.MyId ?? "",
                Id = dialog.Id,
                FeedbackType = FeedbackType.Expired,
            });
        }

        void OnDiscRotated(GuiObject widget, float angleInDeg)
        {
            FeedbackSender?.Publish(new Feedback
            {
                Header = (feedbackSeq++, widget.ParentId),
                VizId = ConnectionManager.MyId ?? "",
                Id = widget.Id,
                FeedbackType = FeedbackType.OrientationChanged,
                Orientation = Quaternion.AngleAxis(angleInDeg * Mathf.Deg2Rad, Msgs.GeometryMsgs.Vector3.UnitZ)
            });
        }

        void OnDiscMoved(GuiObject widget, in Vector3 direction)
        {
            FeedbackSender?.Publish(new Feedback
            {
                Header = (feedbackSeq++, widget.ParentId),
                VizId = ConnectionManager.MyId ?? "",
                Id = widget.Id,
                FeedbackType = FeedbackType.PositionChanged,
                Position = direction.Unity2RosPoint()
            });
        }

        void OnTrajectoryDiscMoved(GuiObject widget, IReadOnlyList<Vector3> points, float periodInSec)
        {
            FeedbackSender?.Publish(new Feedback
            {
                Header = (feedbackSeq++, widget.ParentId),
                VizId = ConnectionManager.MyId ?? "",
                Id = widget.Id,
                FeedbackType = FeedbackType.TrajectoryChanged,
                Trajectory = new Trajectory
                {
                    Poses = points
                        .Select(point => Msgs.GeometryMsgs.Pose.Identity.WithPosition(point.Unity2RosVector3()))
                        .ToArray(),
                    Timestamps = (..points.Count)
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
                VizId = ConnectionManager.MyId ?? "",
                Id = widget.Id,
                FeedbackType = FeedbackType.ScaleChanged,
                Scale = new Vector3(scale.x, 0, scale.y).Unity2RosVector3(),
                Position = position.Unity2RosPoint()
            });
        }

        void OnTargetAreaCanceled(GuiObject widget)
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
            uint numSecs = (uint)time;
            uint numNSecs = (uint)((time - numSecs) * 10000000);
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


        // ----------------------------------------------------------------------------------------

        sealed class GuiObject
        {
            readonly FrameNode node;
            readonly Info<GameObject> info;
            readonly IDisplay display;

            public Transform Transform => node.Transform;
            public string ParentId => node.Parent != null ? node.Parent.Id : TfListener.DefaultFrame.Id;
            public string Id { get; }
            public DateTime ExpirationTime { get; }

            public GuiObject(Widget msg, Info<GameObject> info)
            {
                this.info = info;
                node = FrameNode.Instantiate("Widget Node");
                node.AttachTo(msg.Header.FrameId);
                Id = msg.Id;

                display = ResourcePool.Rent(info, node.Transform).GetComponent<IDisplay>();
                if (display is IWidgetWithCaption withCaption && msg.Caption.Length != 0)
                {
                    withCaption.Caption = msg.Caption;
                }

                if (display is IWidgetWithColor withColor)
                {
                    if (msg.MainColor.A != 0)
                    {
                        withColor.Color = msg.MainColor.ToUnityColor();
                    }

                    if (msg.SecondaryColor.A != 0)
                    {
                        withColor.SecondaryColor = msg.SecondaryColor.ToUnityColor();
                    }
                }

                ExpirationTime = DateTime.MaxValue;
            }

            GuiObject(GuiObject source)
            {
                node = source.node;
                info = source.info;
                display = source.display;
                Id = source.Id;
                ExpirationTime = DateTime.MinValue;
            }

            public T Get<T>() where T : IDisplay
            {
                return (T)display;
            }

            public void Dispose()
            {
                display.ReturnToPool(info);
                node.DestroySelf();
            }

            public GuiObject AsExpired() => new(this);
        }
    }
}