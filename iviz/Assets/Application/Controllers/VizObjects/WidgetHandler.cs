#nullable enable
using Iviz.Core;
using Iviz.Displays.XR;
using Iviz.Msgs.IvizMsgs;
using Iviz.Resources;
using UnityEngine;

namespace Iviz.Controllers
{
    internal interface IHandles<in T>
    {
        void Handle(T msg);
    }

    internal interface IHandlesArray<in T>
    {
        void Handle(T msg);
    }

    public sealed class WidgetHandler : VizHandler, IHandles<Widget>, IHandlesArray<WidgetArray>
    {
        readonly IWidgetFeedback feedback;

        public override string Title => "Widgets";

        public override string BriefDescription
        {
            get
            {
                string vizObjectsStr = vizObjects.Count switch
                {
                    0 => "<b>No widgets</b>",
                    1 => "<b>1 widget</b>",
                    _ => $"<b>{vizObjects.Count.ToString()} widgets</b>"
                };

                const string errorStr = "No errors";

                return $"{vizObjectsStr}\n{errorStr}";
            }
        }

        public WidgetHandler(IWidgetFeedback feedback)
        {
            this.feedback = feedback;
        }

        public void Handle(WidgetArray msg)
        {
            foreach (var widget in msg.Widgets)
            {
                Handle(widget);
            }
        }

        public void Handle(Widget msg)
        {
            switch ((ActionType)msg.Action)
            {
                case ActionType.Remove:
                    HandleRemove(msg.Id);
                    break;
                case ActionType.RemoveAll:
                    RemoveAll();
                    break;
                case ActionType.Add:
                    HandleAdd(msg);
                    break;
                default:
                    RosLogger.Error(
                        $"{ToString()}: Object '{msg.Id}' requested unknown action {msg.Action.ToString()}");
                    break;
            }
        }

        void HandleAdd(Widget msg)
        {
            if (string.IsNullOrWhiteSpace(msg.Id))
            {
                RosLogger.Info($"{ToString()}: Cannot add widget with empty id");
                return;
            }

            if (msg.Color.IsInvalid() || msg.SecondColor.IsInvalid())
            {
                RosLogger.Info($"{ToString()}: Color of widget '{msg.Id}' contains invalid values");
                return;
            }

            if (msg.Scale.IsInvalid() || msg.SecondScale.IsInvalid())
            {
                RosLogger.Info($"{ToString()}: Scale of widget '{msg.Id}' contains invalid values");
                return;
            }

            if (msg.Pose.IsInvalid())
            {
                RosLogger.Info($"{ToString()}: Pose of widget '{msg.Id}' contains invalid values");
                return;
            }

            var widgetType = (WidgetType)msg.Type;
            if (vizObjects.TryGetValue(msg.Id, out var existingObject))
            {
                var widgetObject = (WidgetObject)existingObject;
                if (widgetObject.Type == widgetType)
                {
                    widgetObject.Update(msg);
                    return;
                }

                RosLogger.Info($"{ToString()}: Widget '{msg.Id}' of type {widgetObject.Type} " +
                               $"is being replaced with type {widgetType}");
                widgetObject.Dispose();
                vizObjects.Remove(msg.Id);
                // pass through
            }

            var resourceKey = widgetType switch
            {
                WidgetType.RotationDisc => Resource.Displays.RotationDisc,
                WidgetType.SpringDisc => Resource.Displays.SpringDisc,
                WidgetType.SpringDisc3D => Resource.Displays.SpringDisc3D,
                //WidgetType.TrajectoryDisc => Resource.Displays.TrajectoryDisc,
                WidgetType.TrajectoryDisc3D => Resource.Displays.TrajectoryDisc3D,
                WidgetType.TargetArea => Resource.Displays.TargetArea,
                WidgetType.PositionDisc3D => Resource.Displays.PositionDisc3D,
                WidgetType.PositionDisc => Resource.Displays.PositionDisc,
                WidgetType.Tooltip => Resource.Displays.TooltipWidget,
                _ => null
            };

            if (resourceKey == null)
            {
                RosLogger.Error($"{ToString()}: Widget '{msg.Id}' has unknown type {msg.Type.ToString()}");
                return;
            }

            var vizObject = new WidgetObject(feedback, msg, resourceKey, "Widget." + (WidgetType)msg.Type)
                { Interactable = Interactable, Visible = Visible };

            vizObjects[vizObject.id] = vizObject;
        }

        public override string ToString() => $"[{nameof(WidgetHandler)}]";

        // ----------------------------------------------

        sealed class WidgetObject : VizObject
        {
            public WidgetType Type { get; }

            public WidgetObject(IWidgetFeedback feedback, Widget msg, ResourceKey<GameObject> resourceKey,
                string typeDescription)
                : base(msg.Id, typeDescription, resourceKey)
            {
                Type = (WidgetType)msg.Type;

                // ReSharper disable once ConvertIfStatementToSwitchStatement
                if (display is not IXRWidget)
                {
                    ThrowHelper.ThrowMissingAssetField("Viz object does not have a widget!");
                }

                if (display is IWidgetCanBeMoved canBeMoved)
                {
                    canBeMoved.Moved += direction => feedback.OnWidgetMoved(id, FrameId, direction * scale);
                }

                if (display is IWidgetCanBeRotated canBeRotated)
                {
                    canBeRotated.Moved += angle => feedback.OnWidgetRotated(id, FrameId, angle);
                }

                if (display is IWidgetCanBeResized canBeResized)
                {
                    canBeResized.Resized += bounds =>
                    {
                        var localPose = node.Transform.AsLocalPose();
                        var transformedCenter = localPose.Multiply(scale * bounds.center);
                        feedback.OnWidgetResized(id, FrameId, new Bounds(transformedCenter, bounds.size * scale));
                    };
                }

                if (display is IWidgetCanBeClicked canBeClicked)
                {
                    canBeClicked.Clicked += entry => feedback.OnWidgetClicked(id, FrameId, entry);
                }

                if (display is IWidgetProvidesTrajectory providesTrajectory)
                {
                    providesTrajectory.ProvidedTrajectory += points =>
                        feedback.OnWidgetProvidedTrajectory(id, FrameId, points);
                }

                Update(msg);
            }

            public void Update(Widget msg)
            {
                node.AttachTo(msg.Header.FrameId);

                scale = msg.Scale == 0 ? 1f : (float)msg.Scale;

                if (display is IWidgetWithColor withColor)
                {
                    if (msg.Color.A != 0)
                    {
                        withColor.Color = msg.Color.ToUnity();
                    }

                    if (msg.SecondColor.A != 0)
                    {
                        withColor.SecondColor = msg.SecondColor.ToUnity();
                    }
                }

                if (display is IWidgetWithScale withScale)
                {
                    if (msg.Scale != 0)
                    {
                        withScale.Scale = (float)msg.Scale;
                    }

                    if (msg.SecondScale != 0)
                    {
                        withScale.SecondScale = (float)msg.SecondScale;
                    }
                }

                if (display is IWidgetWithCaption withCaption)
                {
                    withCaption.Caption = msg.Caption;
                    withCaption.SecondCaption = msg.SecondCaption;
                }

                var transform = node.Transform;
                transform.SetLocalPose(msg.Pose.Ros2Unity());
                transform.localScale = Vector3.one * scale;
            }
        }
    }
}