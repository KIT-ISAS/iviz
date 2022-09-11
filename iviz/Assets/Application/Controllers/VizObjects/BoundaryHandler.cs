#nullable enable
using Iviz.Core;
using Iviz.Displays.XR;
using Iviz.Msgs.IvizMsgs;
using Iviz.Resources;
using UnityEngine;

namespace Iviz.Controllers
{
    public interface IBoundaryFeedback
    {
        void OnBoundaryColliderEntered(string id, string otherId);
        void OnBoundaryColliderExited(string id, string otherId);
    }
    
    public sealed class BoundaryHandler : VizHandler
    {
        readonly IBoundaryFeedback feedback;

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

        public void Handler(BoundaryArray msg)
        {
            foreach (var boundary in msg.Boundaries)
            {
                Handler(boundary);
            }
        }

        public void Handler(Boundary msg)
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
                    RosLogger.Error($"{ToString()}: Object '{msg.Id}' requested unknown " +
                                    $"action {((int)msg.Action).ToString()}");
                    break;
            }
        }

        void HandleAdd(Boundary msg)
        {
            if (string.IsNullOrWhiteSpace(msg.Id))
            {
                RosLogger.Info($"{ToString()}: Cannot add dialog with empty id");
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

            var boundaryType = (BoundaryType)msg.Type;
            if (vizObjects.TryGetValue(msg.Id, out var existingObject))
            {
                var boundaryObject = (BoundaryObject)existingObject;
                if (boundaryObject.Type == boundaryType)
                {
                    boundaryObject.Update(msg);
                    return;
                }

                RosLogger.Info($"{ToString()}: Widget '{msg.Id}' of type {boundaryObject.Type} " +
                               $"is being replaced with type {boundaryType}");
                boundaryObject.Dispose();
                // pass through
            }

            var resourceKey = boundaryType switch
            {
                BoundaryType.Simple => Resource.Displays.SimpleBoundary,
                BoundaryType.Collider => Resource.Displays.ColliderBoundary,
                BoundaryType.Collidable => Resource.Displays.CollidableBoundary,
                _ => null
            };

            if (resourceKey == null)
            {
                RosLogger.Error($"{ToString()}: Boundary '{msg.Id}' has unknown type {msg.Type.ToString()}");
                return;
            }

            var vizObject = new BoundaryObject(feedback, msg, resourceKey, "Widget." + (BoundaryType)msg.Type)
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
                if (display is not IBoundary boundary)
                {
                    ThrowHelper.ThrowMissingAssetField("Viz object does not have a boundary!");
                    return; // unreachable
                }

                boundary.Id = msg.Id;

                if (boundary is IBoundaryCanCollide canCollide)
                {
                    canCollide.EnteredCollision += otherId => feedback.OnBoundaryColliderEntered(id, otherId);
                    canCollide.ExitedCollision += otherId => feedback.OnBoundaryColliderEntered(id, otherId);
                }

                Update(msg);
            }

            public void Update(Boundary msg)
            {
                node.AttachTo(msg.Header.FrameId);

                if (display is not IBoundary boundary)
                {
                    ThrowHelper.ThrowInvalidOperation("Viz object is not a boundary!");
                    return;
                }
                
                boundary.Color = msg.Color.ToUnity();
                boundary.SecondColor = msg.SecondColor.ToUnity();
                boundary.Caption = msg.Caption;
                
                if (!msg.Scale.ApproximatelyZero())
                {
                    boundary.Scale = msg.Scale.Abs().Ros2Unity();
                }
                
                var transform = node.Transform;
                transform.SetLocalPose(msg.Pose.Ros2Unity());
            }
        }
    }
}