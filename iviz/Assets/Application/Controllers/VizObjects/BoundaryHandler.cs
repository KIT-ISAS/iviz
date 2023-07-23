#nullable enable
using System;
using Iviz.Core;
using Iviz.Displays.XR;
using Iviz.Msgs.IvizMsgs;
using Iviz.Resources;
using UnityEngine;
using Vector3 = Iviz.Msgs.GeometryMsgs.Vector3;

namespace Iviz.Controllers
{
    public interface IBoundaryFeedback
    {
        void OnBoundaryColliderEntered(string id, string otherId);
        void OnBoundaryColliderExited(string id, string otherId);
    }

    public sealed class BoundaryHandler : VizHandler, IHandles<Boundary>, IHandlesArray<BoundaryArray>
    {
        readonly IBoundaryFeedback feedback;

        public override string Title => "Boundaries";

        public override string BriefDescription
        {
            get
            {
                string vizObjectsStr = vizObjects.Count switch
                {
                    0 => "<b>No boundaries</b>",
                    1 => "<b>1 boundary</b>",
                    _ => $"<b>{vizObjects.Count.ToString()} boundaries</b>"
                };

                const string errorStr = "No errors";

                return $"{vizObjectsStr}\n{errorStr}";
            }
        }

        public BoundaryHandler(IBoundaryFeedback feedback)
        {
            this.feedback = feedback;
        }

        public void Handle(BoundaryArray msg)
        {
            foreach (var boundary in msg.Boundaries)
            {
                Handle(boundary);
            }
        }

        public void Handle(Boundary msg)
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

        void HandleAdd(Boundary msg)
        {
            if (string.IsNullOrWhiteSpace(msg.Id))
            {
                RosLogger.Info($"{ToString()}: Cannot add boundary with empty id");
                return;
            }

            if (msg.Color.IsInvalid() || msg.SecondColor.IsInvalid())
            {
                RosLogger.Info($"{ToString()}: Color of boundary '{msg.Id}' contains invalid values");
                return;
            }

            if (msg.Scale.IsInvalid())
            {
                RosLogger.Info($"{ToString()}: Scale of boundary '{msg.Id}' contains invalid values");
                return;
            }

            if (msg.Pose.IsInvalid())
            {
                RosLogger.Info($"{ToString()}: Pose of boundary '{msg.Id}' contains invalid values");
                return;
            }

            if (msg.Scale == Vector3.Zero)
            {
                RosLogger.Info($"{ToString()}: Scale of boundary '{msg.Id}' has not been set");
                return;
            }

            var boundaryType = (BoundaryType)msg.Type;
            if (vizObjects.TryGetValue(msg.Id, out var existingObject))
            {
                var boundaryObject = (BoundaryObject)existingObject;
                if (boundaryObject.Type == boundaryType)
                {
                    boundaryObject.Update(msg);
                    return;
                }

                RosLogger.Info($"{ToString()}: Widget '{msg.Id}' of type {boundaryObject.Type.ToString()} " +
                               $"is being replaced with type {boundaryType.ToString()}");
                boundaryObject.Dispose();
                vizObjects.Remove(msg.Id);
                // pass through
            }

            var resourceKey = boundaryType switch
            {
                BoundaryType.Simple => Resource.Displays.SimpleBoundary,
                BoundaryType.CircleHighlight or BoundaryType.SquareHighlight => Resource.Displays.AreaHighlightBoundary,
                _ => null
            };

            if (resourceKey == null)
            {
                RosLogger.Error($"{ToString()}: Boundary '{msg.Id}' has unknown type {msg.Type.ToString()}");
                return;
            }

            var vizObject =
                new BoundaryObject(feedback, msg, resourceKey, nameof(Boundary) + "." + (BoundaryType)msg.Type)
                    { Interactable = Interactable, Visible = Visible };

            vizObjects[vizObject.id] = vizObject;
        }

        // ----------------------------------------------

        sealed class BoundaryObject : VizObject
        {
            public BoundaryType Type { get; }

            public BoundaryObject(IBoundaryFeedback feedback, Boundary msg, ResourceKey<GameObject> resourceKey,
                string typeDescription)
                : base(msg.Id, typeDescription, resourceKey)
            {
                Type = (BoundaryType)msg.Type;

                // ReSharper disable once ConvertIfStatementToSwitchStatement
                if (display is not BaseBoundary boundary)
                {
                    ThrowHelper.ThrowMissingAssetField("Viz object does not have a boundary!");
                    return; // unreachable
                }

                boundary.Id = msg.Id;
                boundary.EnteredCollision += otherId => feedback.OnBoundaryColliderEntered(id, otherId);
                boundary.ExitedCollision += otherId => feedback.OnBoundaryColliderExited(id, otherId);

                Update(msg);
            }

            public void Update(Boundary msg)
            {
                node.AttachTo(msg.Header.FrameId);

                if (display is not BaseBoundary boundary)
                {
                    ThrowHelper.ThrowInvalidOperation("Viz object is not a boundary!");
                    return;
                }

                if (boundary is AreaHighlightBoundary areaHighlightBoundary)
                {
                    areaHighlightBoundary.Mode = ((BoundaryType)msg.Type) switch
                    {
                        BoundaryType.CircleHighlight => PolyGlowModeType.Circle,
                        BoundaryType.SquareHighlight => PolyGlowModeType.Square,
                        _ => throw new IndexOutOfRangeException()
                    };
                }

                boundary.FrameColor = msg.Color.ToUnity();
                boundary.InteriorColor = msg.SecondColor.ToUnity();
                boundary.Caption = msg.Caption;
                boundary.Scale = msg.Scale.Ros2Unity().Abs();
                boundary.Behavior = (BehaviorType)msg.Behavior;
                boundary.FrameWidth = 0.015f;
                boundary.Initialize();

                var transform = node.Transform;
                transform.SetLocalPose(msg.Pose.Ros2Unity());
            }
        }
    }
}