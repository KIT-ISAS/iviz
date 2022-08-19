#nullable enable

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using Iviz.Common;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Core.Configurations;
using Iviz.Displays.XR;
using Iviz.Msgs;
using Iviz.Msgs.IvizMsgs;
using Iviz.Ros;
using Iviz.Tools;
using UnityEngine;
using Logger = UnityEngine.Logger;
using Pose = Iviz.Msgs.GeometryMsgs.Pose;

namespace Iviz.Controllers
{
    public sealed class GuiWidgetListener : ListenerController, IMarkerDialogListener, IWidgetFeedback, IDialogFeedback
    {
        static GuiWidgetListener? defaultHandler;
        public static void DisposeDefaultHandler() => defaultHandler?.Dispose();

        public static GuiWidgetListener DefaultHandler =>
            defaultHandler ??= new GuiWidgetListener(null, "~dialogs", Dialog.MessageType);

        readonly GuiWidgetConfiguration config = new();

        readonly VizHandler vizHandler;
        
        //readonly Dictionary<string, GuiObject> widgets = new();
        //readonly Dictionary<string, GuiObject> dialogs = new();
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
                config.Type = value.Type;
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
                vizHandler.Visible = value;
            }
        }

        public bool Interactable
        {
            get => config.Interactable;
            set
            {
                config.Interactable = value;
                vizHandler.Interactable = value;
            }
        }

        public string Topic => Config.Topic;

        public int NumEntriesForLog => vizHandler.Count;

        public string BriefDescription => vizHandler.BriefDescription;

        public GuiWidgetListener(GuiWidgetConfiguration? configuration, string topic, string type)
        {
            Config = configuration ?? new GuiWidgetConfiguration
            {
                Topic = topic,
                Id = topic,
                Type = type,
            };

            /*
            widgetHandler = new WidgetHandler(this);
            dialogHandler = new DialogHandler(this);
            */

            switch (config.Type)
            {
                case Widget.MessageType:
                {
                    var handler = new WidgetHandler(this);
                    vizHandler = handler;
                    Listener = new Listener<Widget>(Config.Topic, handler.Handler) { MaxQueueSize = 50 };
                    break;
                }
                case Dialog.MessageType:
                {
                    var handler = new DialogHandler(this);
                    vizHandler = handler;
                    Listener = new Listener<Dialog>(Config.Topic, handler.Handler) { MaxQueueSize = 50 };
                    break;
                }
                default:
                    throw new InvalidOperationException("Invalid message type");
            }
            
            FeedbackSender = new Sender<Feedback>($"{config.Topic}/feedback");
        }

        /*
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
            if (string.IsNullOrWhiteSpace(msg.Id))
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
                //WidgetType.TrajectoryDisc => Resource.Displays.TrajectoryDisc,
                WidgetType.TargetArea => Resource.Displays.TargetArea,
                WidgetType.PositionDisc3D => Resource.Displays.PositionDisc3D,
                WidgetType.PositionDisc => Resource.Displays.PositionDisc,
                WidgetType.Boundary => Resource.Displays.Boundary,
                WidgetType.BoundaryCheck => Resource.Displays.BoundaryCheck,
                WidgetType.Tooltip => Resource.Displays.TooltipWidget,
                _ => null
            };

            if (resourceKey == null)
            {
                RosLogger.Error($"{this}: Widget '{msg.Id}' has unknown type {msg.Type.ToString()}");
                return;
            }


            var guiObject = new GuiObject(this, msg, resourceKey, "Widget." + (WidgetType)msg.Type)
                { Interactable = Interactable };
            widgets[guiObject.Id] = guiObject;
        }

        public IDialog? AddDialog(Dialog msg)
        {
            Handler(msg);
            return (ActionType)msg.Action == ActionType.Add
                ? dialogs[msg.Id].AsDialog()
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
            if (string.IsNullOrWhiteSpace(msg.Id))
            {
                RosLogger.Info($"{this}: Cannot add dialog with empty id");
                return;
            }

            if (msg.Icon > (byte)XRIcon.Question)
            {
                RosLogger.Info($"{this}: Dialog '{msg.Id}' has unknown icon id {msg.Action.ToString()}");
                return;
            }

            if (msg.Buttons > (byte)ButtonSetup.Backward)
            {
                RosLogger.Info($"{this}: Dialog '{msg.Id}' has unknown button setup id {msg.Buttons.ToString()}");
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
                DialogType.Button => Resource.Displays.XRButtonDialog,
                DialogType.Notice => Resource.Displays.XRDialogNotice,
                DialogType.Plain => Resource.Displays.XRDialog,
                DialogType.Short => Resource.Displays.XRDialogShort,
                DialogType.Menu => Resource.Displays.XRDialogMenu,
                DialogType.Icon => Resource.Displays.XRDialogIcon,
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

            var guiObject = new GuiObject(this, msg, resourceKey, "Dialog." + (DialogType)msg.Type);
            dialogs[guiObject.Id] = guiObject;
        }
        */

        public override void ResetController()
        {
            base.ResetController();
            vizHandler.RemoveAll();
            
            //DestroyAll(dialogs);
            //DestroyAll(widgets);
        }

        /*
        static void DestroyAll(Dictionary<string, GuiObject> dict)
        {
            foreach (var guiObject in dict.Values)
            {
                guiObject.Dispose();
            }

            dict.Clear();
        }
        */

        /*
        void CheckDeadDialogs()
        {
            List<GuiObject>? deadObjects = null;
            foreach (var guiObject in dialogs.Values)
            {
                if (!guiObject.Expired)
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
        */

        public IDialog? AddDialog(Dialog msg)
        {
            if (vizHandler is DialogHandler dialogHandler)
            {
                return dialogHandler.AddDialog(msg);
            }

            RosLogger.Error($"{this}: {nameof(AddDialog)} was called on a topic with a " +
                            $"{vizHandler.GetType().Name}!");
            return null;
        }


        
        public void OnDialogButtonClicked(string id, string frameId, int buttonId)
        {
            FeedbackSender?.Publish(new Feedback
            {
                Header = (feedbackSeq++, frameId),
                VizId = RosManager.MyId ?? "",
                Id = id,
                Type = (byte)FeedbackType.ButtonClick,
                EntryId = buttonId,
            });

            if (vizHandler is DialogHandler dialogHandler) // should be
            {
                dialogHandler.MarkAsExpired(id);
            }
        }

        public void OnDialogMenuEntryClicked(string id, string frameId, int buttonId)
        {
            FeedbackSender?.Publish(new Feedback
            {
                Header = (feedbackSeq++, frameId),
                VizId = RosManager.MyId ?? "",
                Id = id,
                Type = (byte)FeedbackType.MenuEntryClick,
                EntryId = buttonId,
            });

            if (vizHandler is DialogHandler dialogHandler) // should be
            {
                dialogHandler.MarkAsExpired(id);
            }
        }

        public void OnDialogExpired(string id, string frameId)
        {
            FeedbackSender?.Publish(new Feedback
            {
                Header = (feedbackSeq++, frameId),
                VizId = RosManager.MyId ?? "",
                Id = id,
                Type = (byte)FeedbackType.Expired,
            });
        }

        public void OnWidgetRotated(string id, string frameId, float angleInRad)
        {
            FeedbackSender?.Publish(new Feedback
            {
                Header = (feedbackSeq++, frameId),
                VizId = RosManager.MyId ?? "",
                Id = id,
                Type = (byte)FeedbackType.OrientationChanged,
                Angle = angleInRad,
                Orientation = Extensions.AngleAxisZ(angleInRad)
            });
        }

        public void OnWidgetMoved(string id, string frameId, in Vector3 direction)
        {
            FeedbackSender?.Publish(new Feedback
            {
                Header = (feedbackSeq++, frameId),
                VizId = RosManager.MyId ?? "",
                Id = id,
                Type = (byte)FeedbackType.PositionChanged,
                Position = direction.Unity2RosPoint()
            });
        }

        public void OnTrajectoryDiscMoved(string id, string frameId, IReadOnlyList<Vector3> points, float periodInSec)
        {
            FeedbackSender?.Publish(new Feedback
            {
                Header = (feedbackSeq++, frameId),
                VizId = RosManager.MyId ?? "",
                Id = id,
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

        public void OnWidgetResized(string id, string frameId, in Bounds bounds)
        {
            FeedbackSender?.Publish(new Feedback
            {
                Header = (feedbackSeq++, frameId),
                VizId = RosManager.MyId ?? "",
                Id = id,
                Type = (byte)FeedbackType.ScaleChanged,
                Scale = bounds.size.Unity2RosVector3().Abs(),
                Position = bounds.center.Unity2RosPoint()
            });
        }

        public void OnWidgetClicked(string id, string frameId, int entryId)
        {
            FeedbackSender?.Publish(new Feedback
            {
                Header = (feedbackSeq++, frameId),
                VizId = RosManager.MyId ?? "",
                Id = id,
                Type = (byte)FeedbackType.ButtonClick,
                EntryId = entryId,
            });
        }

        public void GenerateLog(StringBuilder description, int minIndex, int numEntries)
        {
            vizHandler.GenerateLog(description, minIndex, numEntries);
        }

        public bool TryGetBoundsFromId(string id, [NotNullWhen(true)] out IHasBounds? bounds)
        {
            bounds = null;
            return false;
        }

        static time SecsToTime(float time)
        {
            int numSecs = (int)time;
            int numNSecs = (int)((time - numSecs) * 10_000_000);
            return new time(numSecs, numNSecs);
        }

        public override void Dispose()
        {
            base.Dispose();
            vizHandler.Dispose();
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
        Boundary = Widget.TYPE_BOUNDARY,
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