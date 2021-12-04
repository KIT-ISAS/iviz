#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Iviz.App;
using Iviz.Common;
using Iviz.Controllers.Markers;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Displays.Highlighters;
using Iviz.Msgs;
using Iviz.Msgs.VisualizationMsgs;
using Iviz.Resources;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Iviz.Controllers
{
    public sealed class InteractiveMarkerControlObject
    {
        const string WarnStr = "<b>Warning:</b> ";
        const string ErrorStr = "<color=red>Error:</color> ";

        readonly Dictionary<(string Ns, int Id), MarkerObject> markers = new();
        readonly List<IBoundsControl> boundsControls = new();
        readonly GameObject node;
        readonly InteractiveMarkerObject parent;
        readonly string rosId;

        InteractiveMarkerControl? lastMessage;

        bool visible;
        bool interactable;

        public Transform Transform => node.transform;

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
            }
        }

        public float Scale
        {
            set => node.transform.localScale = value * Vector3.one;
        }

        public bool Interactable
        {
            get => interactable;
            set
            {
                interactable = value;
                foreach (var control in boundsControls)
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
            Transform.SetParentLocal(parent.ControlNode);
            Interactable = true;
        }

        public void Set(InteractiveMarkerControl msg)
        {
            lastMessage = msg;

            foreach (var control in boundsControls)
            {
                control.Dispose();
            }

            boundsControls.Clear();

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
        }


        void UpdateMarkers(Marker[] msg)
        {
            int numUnnamed = 0;

            foreach (var marker in msg)
            {
                var markerId = marker.Ns.Length == 0 && marker.Id == 0
                    ? ("Unnamed", numUnnamed++)
                    : MarkerListener.IdFromMessage(marker);
                switch (marker.Action)
                {
                    case Marker.ADD:
                        var markerObject = markers.TryGetValue(markerId, out var existingMarker)
                            ? existingMarker
                            : CreateMarker(markerId);

                        _ = markerObject.SetAsync(marker); // TODO: deal with mesh loading
                        if (string.IsNullOrEmpty(marker.Header.FrameId))
                        {
                            markerObject.Transform.SetParentLocal(Transform);
                        }

                        break;
                    case Marker.DELETE:
                        if (markers.TryGetValue(markerId, out var markerToDelete))
                        {
                            markerToDelete.Stop();
                            markers.Remove(markerId);
                        }

                        break;
                    case Marker.DELETEALL:
                        DestroyAllMarkers();
                        break;
                }
            }
        }

        MarkerObject CreateMarker(in (string, int) markerId)
        {
            var markerObject = new MarkerObject(TfListener.ListenersFrame, markerId)
            {
                Visible = Visible
            };
            markers[markerId] = markerObject;
            return markerObject;
        }

        void UpdateInteractionMode(in Quaternion orientation, OrientationMode orientationMode)
        {
            var nodeTransform = node.transform;
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
                        InteractionMode.MoveAxis => new LineBoundsControl(marker, target, orientation),
                        InteractionMode.MovePlane => new PlaneBoundsControl(marker, target, orientation),
                        InteractionMode.MoveRotate => new PlaneBoundsControl(marker, target, orientation),
                        InteractionMode.RotateAxis => new RotationBoundsControl(marker, target, orientation),
                        InteractionMode.Move3D => new FixedDistanceBoundsControl(marker, target),
                        InteractionMode.MoveRotate3D => new FixedDistanceBoundsControl(marker, target),
                        _ => (IBoundsControl?)null
                    };
                    if (control != null)
                    {
                        boundsControls.Add(control);
                    }
                }
            }
            else
            {
                switch (interactionMode)
                {
                    case InteractionMode.MoveAxis:
                        boundsControls.Add(new LineWrapperBoundsControl(nodeTransform, target, orientation));
                        break;
                    case InteractionMode.RotateAxis:
                        boundsControls.Add(new RotationWrapperBoundsControl(nodeTransform, target, orientation));
                        break;
                    case InteractionMode.MoveRotate:
                        boundsControls.Add(new LineWrapperBoundsControl(nodeTransform, target, orientation));
                        boundsControls.Add(new RotationWrapperBoundsControl(nodeTransform, target, orientation));
                        break;
                }
            }

            foreach (var control in boundsControls)
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

                    if (InteractionMode == InteractionMode.Button)
                    {
                        GuiInputModule.PlayClickAudio(parent.ControlNode.position);
                        parent.OnMouseEvent(rosId, null, MouseEventType.Click);
                    }
                };
            }
        }

        void DestroyAllMarkers()
        {
            foreach (var markerObject in markers.Values)
            {
                markerObject.Stop();
            }

            markers.Clear();
        }

        public void Stop()
        {
            DestroyAllMarkers();
            Object.Destroy(node);
        }

        public void GenerateLog(StringBuilder description)
        {
            if (lastMessage == null)
            {
                return;
            }

            description.Append("<color=#000080ff><b>* Control ");
            if (string.IsNullOrEmpty(rosId))
            {
                description.Append("(empty name)");
            }
            else
            {
                description.Append("'").Append(rosId).Append("'");
            }

            description.Append("</b></color>").AppendLine();

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
                .Append(OrientationModeToString(lastMessage.OrientationMode))
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

            if (lastMessage.IndependentMarkerOrientation)
            {
                description.Append(WarnStr)
                    .Append("IndependentMarkerOrientation not implemented")
                    .AppendLine();
            }

            switch (markers.Count)
            {
                case 0:
                    description.Append("Markers: Empty").AppendLine();
                    break;
                case 1:
                    description.Append("+1 marker ---").AppendLine();
                    break;
                default:
                    description.Append("+").Append(markers.Count).Append(" markers ---").AppendLine();
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

        internal IEnumerable<IHasBounds> GetAllBounds() => markers.Values;

        static InteractionMode? ValidateInteractionMode(int mode) =>
            mode is < 0 or > (int)InteractionMode.MoveRotate3D ? null : (InteractionMode?)mode;

        static OrientationMode? ValidateOrientationMode(int mode) =>
            mode is < 0 or > (int)OrientationMode.ViewFacing ? null : (OrientationMode?)mode;

        static string OrientationModeToString(int mode)
        {
            return (OrientationMode)mode switch
            {
                OrientationMode.Inherit => "Inherit",
                OrientationMode.Fixed => "Fixed",
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