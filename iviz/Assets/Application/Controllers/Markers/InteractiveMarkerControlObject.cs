#nullable enable

using System;
using System.Collections.Generic;
using System.Text;
using Iviz.App;
using Iviz.Common;
using Iviz.Controllers.Markers;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Displays.Highlighters;
using Iviz.Msgs.VisualizationMsgs;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Iviz.Controllers
{
    public sealed class InteractiveMarkerControlObject
    {
        const string WarnStr = "<b>Warning:</b> ";
        const string ErrorStr = "<color=red>Error:</color> ";

        readonly Dictionary<(string Ns, int Id), MarkerObject> markers = new();
        readonly Dictionary<IBoundsControl, Quaternion> boundsControls = new();
        readonly GameObject node;
        readonly InteractiveMarkerObject parent;
        readonly string rosId;

        InteractiveMarkerControl? lastMessage;

        bool visible;
        bool interactable;
        bool independentMarkerOrientation;

        public Transform Transform { get; }

        public InteractionMode InteractionMode { get; private set; }

        public bool Visible
        {
            get => visible;
            set
            {
                visible = value;
                foreach (var marker in markers.Values)
                {
                    marker.Visible = value;
                }
                
                foreach (var control in boundsControls.Keys)
                {
                    control.Visible = value;
                }
            }
        }

        public float Scale
        {
            set => Transform.localScale = value * Vector3.one;
        }

        public bool Interactable
        {
            get => interactable;
            set
            {
                interactable = value;
                foreach (var control in boundsControls.Keys)
                {
                    control.Interactable = value;
                }
            }
        }

        public InteractiveMarkerControlObject(InteractiveMarkerObject parent, string rosId)
        {
            this.rosId = rosId;
            this.parent = parent;
            node = new GameObject($"[ControlObject '{this.rosId}']");
            Transform = node.transform;
            Transform.SetParentLocal(parent.ControlNode);
            Interactable = true;
        }

        public void Set(InteractiveMarkerControl msg)
        {
            lastMessage = msg;

            ResetState();

            UpdateMarkers(msg.Markers);

            if (ValidateInteractionMode(msg.InteractionMode) is not { } interactionMode
                || ValidateOrientationMode(msg.OrientationMode) is not { } orientationMode)
            {
                InteractionMode = InteractionMode.None;
                return;
            }

            InteractionMode = interactionMode;

            var orientation = msg.Orientation.Ros2Unity();
            UpdateInteractionMode(orientation, orientationMode);
            independentMarkerOrientation = msg.IndependentMarkerOrientation;
        }

        void ResetState()
        {
            foreach (var control in boundsControls.Keys)
            {
                control.Dispose();
            }

            TfModule.AfterFramesUpdatedLate -= PointToCamera;
            Transform.rotation = Quaternion.identity;

            boundsControls.Clear();
        }

        void UpdateMarkers(Marker[] msg)
        {
            int numUnnamed = 0;

            foreach (var marker in msg)
            {
                if (marker.Ns.Length == 0)
                {
                    marker.Ns = "marker";
                }

                if (marker.Id == 0)
                {
                    marker.Id = numUnnamed++;
                }

                var markerId = MarkerListener.IdFromMessage(marker);
                switch (marker.Action)
                {
                    case Marker.ADD:
                        var markerObject = markers.TryGetValue(markerId, out var existingMarker)
                            ? existingMarker
                            : CreateMarker(markerId);

                        markerObject.SetAsync(marker); // TODO: deal with mesh loading
                        markerObject.Transform.SetParent(Transform,
                            marker.Header.FrameId.Length != 0); // world position stays

                        break;
                    case Marker.DELETE:
                        if (markers.TryGetValue(markerId, out var markerToDelete))
                        {
                            markerToDelete.Dispose();
                            markers.Remove(markerId);
                        }

                        break;
                    case Marker.DELETEALL:
                        DisposeAllMarkers();
                        break;
                }
            }
        }

        MarkerObject CreateMarker(in (string, int) markerId)
        {
            var markerObject = new MarkerObject(TfModule.ListenersFrame, markerId)
            {
                Visible = Visible
            };
            markers[markerId] = markerObject;
            return markerObject;
        }

        void UpdateInteractionMode(in Quaternion orientation, OrientationMode orientationMode)
        {
            var target = parent.ControlNode;
            var interactionMode = InteractionMode;

            if (markers.Count != 0)
            {
                foreach (var marker in markers.Values)
                {
                    var control = interactionMode switch
                    {
                        InteractionMode.Button => new StaticBoundsControl(marker),
                        InteractionMode.Menu => new StaticBoundsControl(marker),
                        InteractionMode.MoveAxis => new LineBoundsControl(marker, target),
                        InteractionMode.MovePlane => new PlaneBoundsControl(marker, target),
                        InteractionMode.MoveRotate => new PlaneBoundsControl(marker, target),
                        InteractionMode.RotateAxis => new RotationBoundsControl(marker, target),
                        InteractionMode.Move3D => new FixedDistanceBoundsControl(marker, target),
                        InteractionMode.MoveRotate3D => new FixedDistanceBoundsControl(marker, target),
                        _ => (IBoundsControl?)null
                    };

                    if (control == null)
                    {
                        continue;
                    }

                    var baseOrientation = orientation * marker.LocalRotation.Inverse();
                    control.BaseOrientation = baseOrientation;
                    boundsControls.Add(control, baseOrientation);
                }
            }
            else
            {
                switch (interactionMode)
                {
                    case InteractionMode.MoveAxis:
                        boundsControls.Add(new LineWrapperBoundsControl(Transform, target)
                            { BaseOrientation = orientation }, orientation);
                        break;
                    case InteractionMode.RotateAxis:
                        boundsControls.Add(new RotationWrapperBoundsControl(Transform, target)
                            { BaseOrientation = orientation }, orientation);
                        break;
                    case InteractionMode.MoveRotate:
                        boundsControls.Add(new LineWrapperBoundsControl(Transform, target)
                            { BaseOrientation = orientation }, orientation);
                        boundsControls.Add(new RotationWrapperBoundsControl(Transform, target)
                            { BaseOrientation = orientation }, orientation);
                        break;
                }
            }

            foreach (var control in boundsControls.Keys)
            {
                control.Interactable = Interactable;

                control.Moved += () => parent.OnMoved(rosId);

                control.PointerDown += () =>
                {
                    // disable external updates while dragging
                    parent.PoseUpdateEnabled = false;
                    parent.OnMouseEvent(rosId, null, MouseEventType.Down);
                };

                control.PointerUp += () =>
                {
                    parent.PoseUpdateEnabled = true;
                    parent.OnMouseEvent(rosId, null, MouseEventType.Up);

                    if (InteractionMode is not (InteractionMode.Button or InteractionMode.Menu))
                    {
                        return;
                    }

                    GuiInputModule.PlayClickAudio(parent.ControlNode.position);
                    parent.OnMouseEvent(rosId, null, MouseEventType.Click);

                    if (InteractionMode is InteractionMode.Menu)
                    {
                        parent.ShowMenu();
                    }
                };
            }

            if (orientationMode == OrientationMode.ViewFacing)
            {
                TfModule.AfterFramesUpdatedLate += PointToCamera;
            }
        }

        void PointToCamera()
        {
            if (!parent.PoseUpdateEnabled)
            {
                return;
            }

            if (independentMarkerOrientation)
            {
                var transformPose = Transform.AsPose();
                var forwardWorld = Settings.MainCameraPose.position - transformPose.position;
                var deltaRotationWorld = Quaternion.LookRotation(forwardWorld);
                var deltaRotationLocal = transformPose.rotation.Inverse() * deltaRotationWorld;

                foreach (var (boundsControl, baseOrientation) in boundsControls)
                {
                    boundsControl.BaseOrientation = deltaRotationLocal * baseOrientation;
                }
            }
            else
            {
                Transform.LookAt(Settings.MainCameraPose.position);
            }
        }

        void DisposeAllMarkers()
        {
            foreach (var markerObject in markers.Values)
            {
                markerObject.Dispose();
            }

            markers.Clear();
        }

        public void Dispose()
        {
            TfModule.AfterFramesUpdatedLate -= PointToCamera;
            DisposeAllMarkers();
            Object.Destroy(node);
        }

        public void GenerateLog(StringBuilder description)
        {
            if (lastMessage == null)
            {
                return;
            }

            description.Append("<color=#000080ff><b>- Control ");
            if (string.IsNullOrWhiteSpace(rosId))
            {
                description.Append("(empty name)");
            }
            else
            {
                description.Append(rosId);
            }

            description.Append(" -</b></color>").AppendLine();

            string msgDescription = lastMessage.Description.Length != 0
                ? lastMessage.Description.Replace("\t", "\\t").Replace("\n", "\\n")
                : "[]";
            description.Append("Description: ").Append(msgDescription).AppendLine();

            description.Append("Orientation: [");
            var unityRotation = lastMessage.Orientation.Ros2Unity();
            RosUtils.FormatPose(Pose.identity.WithRotation(unityRotation), description,
                RosUtils.PoseFormat.OnlyRotation);
            description.AppendLine("]");

            description.Append("InteractionMode: ")
                .Append(InteractionModeToString(lastMessage.InteractionMode))
                .AppendLine();

            if (ValidateInteractionMode(lastMessage.InteractionMode) is null)
            {
                description.Append(ErrorStr)
                    .Append("Unknown interaction mode ")
                    .Append(lastMessage.InteractionMode)
                    .AppendLine();
            }

            description.Append("OrientationMode: ")
                .Append(OrientationModeToString(lastMessage.OrientationMode, lastMessage.IndependentMarkerOrientation))
                .AppendLine();

            if (ValidateOrientationMode(lastMessage.OrientationMode) is not { } orientationMode)
            {
                description.Append(ErrorStr)
                    .Append("Unknown orientation mode ")
                    .Append(lastMessage.OrientationMode)
                    .AppendLine();
            }
            else if (orientationMode == OrientationMode.Fixed)
            {
                description.Append(WarnStr)
                    .Append("OrientationMode FIXED not implemented")
                    .AppendLine();
            }

            switch (markers.Count)
            {
                case 0:
                    description.Append("Markers: Empty").AppendLine();
                    break;
                case 1:
                    break;
                default:
                    description.Append("---").AppendLine();
                    break;
            }

            foreach (var marker in markers.Values)
            {
                marker.GenerateLog(description);
            }
        }

        public void GetErrorCount(out int totalErrors, out int totalWarnings)
        {
            totalErrors = 0;
            totalWarnings = 0;

            foreach (var marker in markers.Values)
            {
                marker.GetErrorCount(out int newNumErrors, out int newNumWarnings);
                totalErrors += newNumErrors;
                totalWarnings += newNumWarnings;
            }
        }

        public override string ToString() => $"[{nameof(InteractiveMarkerControlObject)} '{rosId}']";

        internal IEnumerable<MarkerObject> GetAllMarkers() => markers.Values;
        
        static InteractionMode? ValidateInteractionMode(int mode) =>
            mode is < 0 or > (int)InteractionMode.MoveRotate3D ? null : (InteractionMode?)mode;

        static OrientationMode? ValidateOrientationMode(int mode) =>
            mode is < 0 or > (int)OrientationMode.ViewFacing ? null : (OrientationMode?)mode;

        static string OrientationModeToString(int mode, bool independentMarkerOrientation)
        {
            return (OrientationMode)mode switch
            {
                OrientationMode.Inherit => "Inherit",
                OrientationMode.Fixed => "Fixed",
                OrientationMode.ViewFacing when independentMarkerOrientation =>
                    "\n    ViewFacing\n    IndependentMarkerOrientation",
                OrientationMode.ViewFacing => "ViewFacing",
                _ => $"Unknown ({mode.ToString()})"
            };
        }

        static string InteractionModeToString(int mode)
        {
            return (InteractionMode)mode switch
            {
                InteractionMode.None => "None",
                InteractionMode.Menu => "Menu",
                InteractionMode.Button => "Button",
                InteractionMode.MoveAxis => "MoveAxis",
                InteractionMode.MovePlane => "MovePlane",
                InteractionMode.RotateAxis => "RotateAxis",
                InteractionMode.MoveRotate => "MoveRotate",
                InteractionMode.Move3D => "Move3D",
                InteractionMode.Rotate3D => "Rotate3D",
                InteractionMode.MoveRotate3D => "MoveRotate3D",
                _ => $"Unknown ({mode.ToString()})"
            };
        }

        static Color ColorFromOrientation(in Quaternion orientation, in Vector3 direction)
        {
            var (x, y, z) = orientation * direction;
            return new Color(Mathf.Abs(x), Mathf.Abs(y), Mathf.Abs(z));
        }
    }
}