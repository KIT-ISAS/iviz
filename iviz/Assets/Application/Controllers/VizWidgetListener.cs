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
using Iviz.Msgs.StdMsgs;
using Iviz.Ros;
using Iviz.Tools;
using UnityEngine;
using Logger = UnityEngine.Logger;
using Pose = Iviz.Msgs.GeometryMsgs.Pose;

namespace Iviz.Controllers
{
    public sealed class VizWidgetListener : ListenerController, IMarkerDialogListener, IWidgetFeedback, IDialogFeedback,
        IBoundaryFeedback
    {
        static VizWidgetListener? defaultHandler;
        public static void DisposeDefaultHandler() => defaultHandler?.Dispose();

        public static VizWidgetListener DefaultHandler =>
            defaultHandler ??= new VizWidgetListener(null, "~dialogs", Dialog.MessageType);

        readonly VizWidgetConfiguration config = new();
        readonly VizHandler vizHandler;

        uint feedbackSeq;

        public override TfFrame Frame => TfModule.FixedFrame;
        public Sender<Feedback>? FeedbackSender { get; }
        public override Listener Listener { get; }

        public VizWidgetConfiguration Config
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

        public string Title => vizHandler.Title;

        public string Topic => Config.Topic;

        public int NumEntriesForLog => vizHandler.Count;

        public string BriefDescription => vizHandler.BriefDescription;

        public VizWidgetListener(VizWidgetConfiguration? configuration, string topic, string type)
        {
            string configTopic = configuration?.Topic ?? topic;
            string configType = configuration?.Type ?? type;

            (vizHandler, Listener) = configType switch
            {
                Widget.MessageType => Create(new WidgetHandler(this)),
                WidgetArray.MessageType => CreateForArray(new WidgetHandler(this)),

                Dialog.MessageType => Create(new DialogHandler(this)),
                DialogArray.MessageType => CreateForArray(new DialogHandler(this)),

                RobotPreview.MessageType => Create(new RobotPreviewHandler()),
                RobotPreviewArray.MessageType => CreateForArray(new RobotPreviewHandler()),

                Boundary.MessageType => Create(new BoundaryHandler(this)),
                BoundaryArray.MessageType => CreateForArray(new BoundaryHandler(this)),

                DetectionBox.MessageType => Create(new DetectionHandler()),
                DetectionBoxArray.MessageType => CreateForArray(new DetectionHandler()),
                
                _ =>
                    ((VizHandler)null!, Listener.ThrowUnsupportedMessageType(configType)),
            };

            Config = configuration ?? new VizWidgetConfiguration
            {
                Topic = topic,
                Id = topic,
                Type = type,
            };

            FeedbackSender = new Sender<Feedback>($"{config.Topic}/feedback");
            
            
            
            const int maxQueueSize = 50;

            (VizHandler, Listener<T>) Create<T>(IHandles<T> handler) where T : IMessage, new() =>
                ((VizHandler)handler, new(configTopic, handler.Handle, maxQueueSize));

            (VizHandler, Listener<T>) CreateForArray<T>(IHandlesArray<T> handler) where T : IMessage, new() =>
                ((VizHandler)handler, new(configTopic, handler.Handle, maxQueueSize));
        }

        public override void ResetController()
        {
            base.ResetController();
            vizHandler.RemoveAll();
        }

        public IDialog? AddDialog(Dialog msg)
        {
            if (vizHandler is DialogHandler dialogHandler)
            {
                return dialogHandler.AddDialog(msg);
            }

            RosLogger.Error($"{ToString()}: {nameof(AddDialog)} was called on a topic with a " +
                            $"{vizHandler.GetType().Name}!");
            return null;
        }


        public void OnDialogButtonClicked(string id, string frameId, int buttonId)
        {
            FeedbackSender?.Publish(new Feedback
            {
                Header = CreateHeader(frameId),
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
                Header = CreateHeader(frameId),
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
                Header = CreateHeader(frameId),
                VizId = RosManager.MyId ?? "",
                Id = id,
                Type = (byte)FeedbackType.Expired,
            });
        }

        public void OnWidgetRotated(string id, string frameId, float angleInRad)
        {
            FeedbackSender?.Publish(new Feedback
            {
                Header = CreateHeader(frameId),
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
                Header = CreateHeader(frameId),
                VizId = RosManager.MyId ?? "",
                Id = id,
                Type = (byte)FeedbackType.PositionChanged,
                Position = direction.Unity2RosPoint()
            });
        }

        public void OnWidgetProvidedTrajectory(string id, string frameId, List<Vector3> points)
        {
            //Debug.Log($"{ToString()}: Sending trajectory");
            FeedbackSender?.Publish(new Feedback
            {
                Header = CreateHeader(frameId),
                VizId = RosManager.MyId ?? "",
                Id = id,
                Type = (byte)FeedbackType.TrajectoryChanged,
                Trajectory = points
                    .Select(point => Pose.Identity.WithPosition(point.Unity2RosPoint()))
                    .ToArray(),
            });
        }

        public void OnWidgetResized(string id, string frameId, in Bounds bounds)
        {
            FeedbackSender?.Publish(new Feedback
            {
                Header = CreateHeader(frameId),
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
                Header = CreateHeader(frameId),
                VizId = RosManager.MyId ?? "",
                Id = id,
                Type = (byte)FeedbackType.ButtonClick,
                EntryId = entryId,
            });
        }

        public void OnBoundaryColliderEntered(string id, string otherId)
        {
            FeedbackSender?.Publish(new Feedback
            {
                Header = CreateHeader(""),
                VizId = RosManager.MyId ?? "",
                Id = id,
                ColliderId = otherId,
                Type = (byte)FeedbackType.ColliderEntered,
            });
        }

        public void OnBoundaryColliderExited(string id, string otherId)
        {
            FeedbackSender?.Publish(new Feedback
            {
                Header = CreateHeader(""),
                VizId = RosManager.MyId ?? "",
                Id = id,
                ColliderId = otherId,
                Type = (byte)FeedbackType.ColliderExited,
            });
        }

        Header CreateHeader(string frameId) => new(feedbackSeq++, time.Now(), frameId);

        public void GenerateLog(StringBuilder description, int minIndex, int numEntries)
        {
            vizHandler.GenerateLog(description, minIndex, numEntries);
        }

        public bool TryGetBoundsFromId(string id, [NotNullWhen(true)] out IHasBounds? bounds)
        {
            bounds = null;
            return false;
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
}