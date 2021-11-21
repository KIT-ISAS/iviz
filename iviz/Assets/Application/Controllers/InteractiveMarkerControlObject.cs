#nullable enable

using System.Collections.Generic;
using System.Linq;
using System.Text;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Msgs.VisualizationMsgs;
using Iviz.Resources;
using UnityEngine;

namespace Iviz.Controllers
{
    public sealed class InteractiveMarkerControlObject
    {
        const string WarnStr = "<b>Warning:</b> ";
        const string ErrorStr = "<color=red>Error:</color> ";
        const string FloatFormat = "#,0.###";

        readonly StringBuilder description = new(250);
        readonly Dictionary<(string Ns, int Id), MarkerObject> markers = new();
        readonly GameObject markerNode;
        readonly FrameNode node;
        readonly InteractiveMarkerObject parent;
        readonly string rosId;

        bool visible;
        bool interactable;
        int numWarnings;
        int numErrors;

        internal InteractiveControl? ControlMarker { get; private set; }
        public Bounds? Bounds { get; private set; }
        public Transform Transform => node.transform;

        public InteractionModeType ControlInteractionMode =>
            ControlMarker != null ? ControlMarker.InteractionMode : InteractionModeType.None;

        public bool ControlColliderCanInteract => ControlMarker != null && ControlMarker.ColliderCanInteract;

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

                if (ControlMarker != null)
                {
                    ControlMarker.Visible = value;
                }
            }
        }

        public bool Interactable
        {
            get => interactable;
            set
            {
                interactable = value;
                if (ControlMarker != null)
                {
                    ControlMarker.Interactable = value;
                }
            }
        }

        public InteractiveMarkerControlObject(InteractiveMarkerObject parentObject, string realId)
        {
            node = FrameNode.Instantiate("[InteractiveMarkerControlObject]");

            parent = parentObject;
            rosId = realId;

            markerNode = new GameObject("[MarkerNode]");
            markerNode.transform.SetParentLocal(Transform);
            markerNode.AddComponent<Billboard>().enabled = false;
            Interactable = true;
        }

        public void Set(InteractiveMarkerControl msg)
        {
            node.gameObject.name = $"[ControlObject '{msg.Name}']";

            numErrors = 0;
            numWarnings = 0;

            description.Clear();
            description.Append("<color=#000080ff><b>* Control ");
            if (string.IsNullOrEmpty(msg.Name))
            {
                description.Append("(empty name)");
            }
            else
            {
                description.Append("'").Append(msg.Name).Append("'");
            }

            description.Append("</b></color>").AppendLine();

            string msgDescription = msg.Description.Length != 0
                ? msg.Description.Replace("\t", "\\t").Replace("\n", "\\n")
                : "[]";
            description.Append("Description: ").Append(msgDescription).AppendLine();
            description.Append("Orientation: ")
                .Append(msg.Orientation.X.ToString(FloatFormat)).Append(" | ")
                .Append(msg.Orientation.Y.ToString(FloatFormat)).Append(" | ")
                .Append(msg.Orientation.Z.ToString(FloatFormat)).Append(" | ")
                .Append(msg.Orientation.W.ToString(FloatFormat)).AppendLine();


            Transform.localRotation = msg.Orientation.Ros2Unity();

            description.Append("InteractionMode: ").Append(InteractionModeToString(msg.InteractionMode)).AppendLine();
            description.Append("OrientationMode: ").Append(OrientationModeToString(msg.OrientationMode)).AppendLine();

            UpdateMarkers(msg.Markers);
            RecalculateBounds();

            var interactionMode = ValidateInteractionMode(msg.InteractionMode);
            var orientationMode = ValidateOrientationMode(msg.OrientationMode);
            if (interactionMode != null && orientationMode != null)
            {
                UpdateInteractionMode(interactionMode.Value, orientationMode.Value, msg.IndependentMarkerOrientation);
            }

            if (markers.Count == 0)
            {
                description.Append("Markers: Empty").AppendLine();
            }
            else
            {
                description.Append("Markers: ").Append(markers.Count).AppendLine();
            }
        }

        static InteractionMode? ValidateInteractionMode(int mode) =>
            mode is < 0 or > (int)InteractionMode.MoveRotate3D ? null : (InteractionMode?)mode;

        static OrientationMode? ValidateOrientationMode(int mode) =>
            mode is < 0 or > (int)OrientationMode.ViewFacing ? null : (OrientationMode?)mode;

        IControlMarker EnsureControlDisplayExists()
        {
            if (ControlMarker != null)
            {
                return ControlMarker;
            }

            ControlMarker = ResourcePool.Rent<InteractiveControl>(Resource.Displays.InteractiveControl, Transform);

            ControlMarker.TargetTransform = Transform.parent;
            ControlMarker.Visible = Visible;
            Interactable = Interactable;

            ControlMarker.Moved += () =>
            {
                parent.OnMoved(rosId);
            };

            // disable external updates while dragging
            ControlMarker.PointerDown += () =>
            {
                parent.PoseUpdateEnabled = false;
                parent.OnMouseEvent(rosId, null, MouseEventType.Down);
            };
            ControlMarker.PointerUp += () =>
            {
                parent.PoseUpdateEnabled = true;
                parent.OnMouseEvent(rosId, null, MouseEventType.Up);

                if (ControlMarker.InteractionMode == InteractionModeType.ClickOnly)
                {
                    parent.OnMouseEvent(rosId, null, MouseEventType.Click);
                }
            };

            ControlMarker.MenuClicked += unityPositionHint => parent.ShowMenu(unityPositionHint);

            return ControlMarker;
        }

        void DisposeControlDisplay()
        {
            if (ControlMarker == null)
            {
                return;
            }

            ControlMarker.Suspend();
            ResourcePool.Return(Resource.Displays.InteractiveControl, ControlMarker.gameObject);
            ControlMarker = null;
        }

        void UpdateInteractionMode(InteractionMode interactionMode, OrientationMode orientationMode,
            bool independentMarkerOrientation)
        {
            if (interactionMode is < 0 or > InteractionMode.MoveRotate3D)
            {
                description.Append(ErrorStr).Append("Unknown interaction mode ").Append((int)interactionMode)
                    .AppendLine();
                numErrors++;
                DisposeControlDisplay();
            }
            else if (interactionMode == InteractionMode.None)
            {
                DisposeControlDisplay();
            }
            else
            {
                IControlMarker mControl = EnsureControlDisplayExists();
                mControl.EnableMenu = false;

                var interactionModeType = interactionMode switch
                {
                    InteractionMode.Menu => InteractionModeType.ClickOnly,
                    InteractionMode.Button => InteractionModeType.ClickOnly,
                    InteractionMode.MoveAxis => InteractionModeType.MoveAxisX,
                    InteractionMode.MovePlane => InteractionModeType.MovePlaneYz,
                    InteractionMode.RotateAxis => InteractionModeType.RotateAxisX,
                    InteractionMode.MoveRotate => InteractionModeType.MovePlaneYzRotateAxisX,
                    InteractionMode.Move3D => InteractionModeType.Move3D,
                    InteractionMode.Rotate3D => InteractionModeType.Rotate3D,
                    InteractionMode.MoveRotate3D => InteractionModeType.MoveRotate3D,
                    _ => InteractionModeType.None
                };

                mControl.InteractionMode = interactionModeType;
            }

            if (orientationMode is < 0 or > OrientationMode.ViewFacing)
            {
                description.Append(ErrorStr).Append("Unknown orientation mode ").Append((int)orientationMode)
                    .AppendLine();
                numErrors++;

                markerNode.GetComponent<Billboard>().enabled = false;
                markerNode.transform.localRotation = Quaternion.identity;

                if (ControlMarker != null)
                {
                    ControlMarker.PointsToCamera = false;
                    ControlMarker.KeepAbsoluteRotation = false;
                }

                return;
            }

            switch (orientationMode)
            {
                case OrientationMode.ViewFacing:
                    markerNode.GetComponent<Billboard>().enabled = true;
                    break;
                case OrientationMode.Inherit:
                case OrientationMode.Fixed:
                    markerNode.GetComponent<Billboard>().enabled = false;
                    markerNode.transform.localRotation = Quaternion.identity;
                    break;
            }

            if (ControlMarker == null)
            {
                return;
            }

            switch (orientationMode)
            {
                case OrientationMode.ViewFacing:
                    ControlMarker.KeepAbsoluteRotation = false;
                    ControlMarker.PointsToCamera = true;
                    ControlMarker.HandlesPointToCamera = !independentMarkerOrientation;
                    break;
                case OrientationMode.Inherit:
                    ControlMarker.PointsToCamera = false;
                    ControlMarker.KeepAbsoluteRotation = false;
                    break;
                case OrientationMode.Fixed:
                    ControlMarker.PointsToCamera = false;
                    ControlMarker.KeepAbsoluteRotation = false;
                    //Control.KeepAbsoluteRotation = true; // TODO: fix this
                    break;
            }
        }

        void ClearMarkers()
        {
            foreach (var markerObject in markers.Values)
            {
                DeleteMarkerObject(markerObject);
            }

            markers.Clear();
        }

        void UpdateMarkers(IEnumerable<Marker> msg)
        {
            int numUnnamed = 0;

            foreach (Marker marker in msg)
            {
                var markerId = marker.Ns.Length == 0 && marker.Id == 0
                    ? ("Unnamed-", numUnnamed++)
                    : MarkerListener.IdFromMessage(marker);
                switch (marker.Action)
                {
                    case Marker.ADD:
                        MarkerObject markerObject;
                        if (markers.TryGetValue(markerId, out MarkerObject existingMarker))
                        {
                            markerObject = existingMarker;
                        }
                        else
                        {
                            markerObject = CreateMarkerObject();
                            markers[markerId] = markerObject;
                        }

                        _ = markerObject.SetAsync(marker); // TODO: deal with mesh loading
                        if (string.IsNullOrEmpty(marker.Header.FrameId))
                        {
                            markerObject.Transform.SetParentLocal(markerNode.transform);
                        }

                        break;
                    case Marker.DELETE:
                        if (markers.TryGetValue(markerId, out MarkerObject markerToDelete))
                        {
                            DeleteMarkerObject(markerToDelete);
                            markers.Remove(markerId);
                        }

                        break;
                }

                if (numUnnamed > 1)
                {
                    description.Append(WarnStr).Append(numUnnamed).Append(" interactive markers have empty ids")
                        .AppendLine();
                    numWarnings++;
                }
            }
        }

        public void Stop()
        {
            ClearMarkers();

            if (ControlMarker == null)
            {
                return;
            }

            DisposeControlDisplay();

            node.DestroySelf();
        }

        public void GenerateLog(StringBuilder baseDescription)
        {
            baseDescription.Append(description);
            foreach (var marker in markers.Values)
            {
                marker.GenerateLog(baseDescription);
            }
        }

        public void GetErrorCount(out int totalErrors, out int totalWarnings)
        {
            totalErrors = numErrors;
            totalWarnings = numWarnings;

            foreach (var marker in markers.Values)
            {
                marker.GetErrorCount(out int newNumErrors, out int newNumWarnings);
                totalErrors += newNumErrors;
                totalWarnings += newNumWarnings;
            }
        }

        void DeleteMarkerObject(MarkerObject marker)
        {
            marker.BoundsChanged -= RecalculateBounds;
            marker.Stop();
        }

        MarkerObject CreateMarkerObject()
        {
            var markerObject = new MarkerObject(TfListener.ListenersFrame)
            {
                Visible = visible
            };
            markerObject.BoundsChanged += RecalculateBounds;
            return markerObject;
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

        void RecalculateBounds()
        {
            var innerBounds = markers.Values.Select(marker => (marker.Bounds, marker.Transform));

            Bounds? totalBounds =
                innerBounds.Select(tuple => tuple.Bounds.TransformBound(tuple.Transform)).CombineBounds();

            Bounds = totalBounds;
        }

        public void UpdateControlBounds(Bounds? bounds)
        {
            if (ControlMarker != null)
            {
                ControlMarker.Bounds = bounds.TransformBoundWithInverse(Transform);
            }
        }

        enum InteractionMode
        {
            None = 0,
            Menu = 1,
            Button = 2,
            MoveAxis = 3,
            MovePlane = 4,
            RotateAxis = 5,
            MoveRotate = 6,
            Move3D = 7,
            Rotate3D = 8,
            MoveRotate3D = 9
        }

        enum OrientationMode
        {
            Inherit = 0,
            Fixed = 1,
            ViewFacing = 2
        }
    }
}