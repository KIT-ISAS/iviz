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
    public sealed class VizWidgetListener : ListenerController, IMarkerDialogListener, IWidgetFeedback, IDialogFeedback, IBoundaryFeedback
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
        public override IListener Listener { get; }

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

        public string Topic => Config.Topic;

        public int NumEntriesForLog => vizHandler.Count;

        public string BriefDescription => vizHandler.BriefDescription;

        public VizWidgetListener(VizWidgetConfiguration? configuration, string topic, string type)
        {
            string configTopic = configuration?.Topic ?? topic; 
            string configType = configuration?.Type ?? type;

            switch (configType)
            {
                case Widget.MessageType:
                {
                    var handler = new WidgetHandler(this);
                    vizHandler = handler;
                    Listener = new Listener<Widget>(configTopic, handler.Handler,50);
                    break;
                }
                case WidgetArray.MessageType:
                {
                    var handler = new WidgetHandler(this);
                    vizHandler = handler;
                    Listener = new Listener<WidgetArray>(configTopic, handler.Handler,50);
                    break;
                }
                case Dialog.MessageType:
                {
                    var handler = new DialogHandler(this);
                    vizHandler = handler;
                    Listener = new Listener<Dialog>(configTopic, handler.Handler,50);
                    break;
                }
                case DialogArray.MessageType:
                {
                    var handler = new DialogHandler(this);
                    vizHandler = handler;
                    Listener = new Listener<DialogArray>(configTopic, handler.Handler,50);
                    break;
                }
                case RobotPreview.MessageType:
                {
                    var handler = new RobotPreviewHandler();
                    vizHandler = handler;
                    Listener = new Listener<RobotPreview>(configTopic, handler.Handler,50);
                    break;
                }
                case Boundary.MessageType:
                {
                    var handler = new BoundaryHandler(this);
                    vizHandler = handler;
                    Listener = new Listener<Boundary>(configTopic, handler.Handler,50);
                    break;
                }
                case BoundaryArray.MessageType:
                {
                    var handler = new BoundaryHandler(this);
                    vizHandler = handler;
                    Listener = new Listener<BoundaryArray>(configTopic, handler.Handler,50);
                    break;
                }
                default:
                    Ros.Listener.ThrowUnsupportedMessageType(configTopic);
                    break; // unreachable
            }

            Config = configuration ?? new VizWidgetConfiguration
            {
                Topic = topic,
                Id = topic,
                Type = type,
            };

            FeedbackSender = new Sender<Feedback>($"{config.Topic}/feedback");
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

        public void OnWidgetProvidedTrajectory(string id, string frameId, List<Vector3> points, float periodInSec)
        {
            Debug.Log($"{ToString()}: Sending trajectory");
            FeedbackSender?.Publish(new Feedback
            {
                Header = CreateHeader(frameId),
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
}