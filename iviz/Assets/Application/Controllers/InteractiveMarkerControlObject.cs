using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Hololens;
using Iviz.Msgs.VisualizationMsgs;
using Iviz.Resources;
using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.Controllers
{
    public sealed class InteractiveMarkerControlObject : MonoBehaviour
    {
        const string WarnStr = "<b>Warning:</b> ";
        const string ErrorStr = "<color=red>Error:</color> ";

        readonly StringBuilder description = new StringBuilder(250);
        int numWarnings;
        int numErrors;

        readonly Dictionary<(string Ns, int Id), MarkerObject> markers =
            new Dictionary<(string Ns, int Id), MarkerObject>();

        [CanBeNull] internal IControlMarker Control { get; private set; }
        GameObject markerNode;
        string rosId;
        bool visible;

        InteractiveMarkerObject parent;
        bool interactable;

        [CanBeNull] public Bounds? Bounds { get; private set; }

        public InteractionModeType ControlInteractionMode => Control?.InteractionMode ?? InteractionModeType.None;
        public bool ControlColliderCanInteract => Control?.ColliderCanInteract ?? false;

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

                if (Control != null)
                {
                    Control.Visible = value;
                }
            }
        }

        public bool Interactable
        {
            get => interactable;
            set
            {
                interactable = value;
                if (Control != null)
                {
                    Control.Interactable = value;
                }
            }
        }

        void Awake()
        {
            markerNode = new GameObject("[MarkerNode]");
            markerNode.transform.SetParentLocal(transform);
            markerNode.AddComponent<Billboard>().enabled = false;
            Interactable = true;
        }

        internal void Initialize(InteractiveMarkerObject newIMarkerObject, string realId)
        {
            parent = newIMarkerObject;
            rosId = realId;
        }

        const string FloatFormat = "#,0.###";

        public void Set([NotNull] InteractiveMarkerControl msg)
        {
            name = $"[ControlObject '{msg.Name}']";

            numErrors = 0;
            numWarnings = 0;

            description.Clear();
            description.Append("<color=#000080ff><font=Bold>* Control ");
            if (string.IsNullOrEmpty(msg.Name))
            {
                description.Append("(empty name)");
            }
            else
            {
                description.Append("'").Append(msg.Name).Append("'");
            }

            description.Append("</font></color>").AppendLine();

            string msgDescription = msg.Description.Length != 0
                ? msg.Description.ToString().Replace("\t", "\\t").Replace("\n", "\\n")
                : "[]";
            description.Append("Description: ").Append(msgDescription).AppendLine();
            description.Append("Orientation: ")
                .Append(msg.Orientation.X.ToString(FloatFormat)).Append(" | ")
                .Append(msg.Orientation.Y.ToString(FloatFormat)).Append(" | ")
                .Append(msg.Orientation.Z.ToString(FloatFormat)).Append(" | ")
                .Append(msg.Orientation.W.ToString(FloatFormat)).AppendLine();


            transform.localRotation = msg.Orientation.Ros2Unity();

            description.Append("InteractionMode: ").Append(InteractionModeToString(msg.InteractionMode)).AppendLine();
            description.Append("OrientationMode: ").Append(OrientationModeToString(msg.OrientationMode)).AppendLine();

            UpdateMarkers(msg.Markers);
            RecalculateBounds();

            InteractionMode? interactionMode = ValidateInteractionMode(msg.InteractionMode);
            OrientationMode? orientationMode = ValidateOrientationMode(msg.OrientationMode);
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
            (mode < 0 || mode > (int)InteractionMode.MoveRotate3D) ? null : (InteractionMode?)mode;

        static OrientationMode? ValidateOrientationMode(int mode) =>
            (mode < 0 || mode > (int)OrientationMode.ViewFacing) ? null : (OrientationMode?)mode;

        [NotNull]
        IControlMarker EnsureControlDisplayExists()
        {
            if (Control != null)
            {
                return Control;
            }

            GameObject controlObject = Settings.IsHololens
                ? HololensManager.ResourcePool.GetOrCreate(transform)
                : ResourcePool.Rent(Resource.Displays.InteractiveControl, transform);

            Control = controlObject.GetComponent<IControlMarker>()
                      ?? throw new InvalidOperationException("Control marker has no control component!");

            Control.TargetTransform = transform.parent;
            Control.Visible = Visible;
            Interactable = Interactable;

            Control.Moved += (in Pose _) =>
            {
                if (parent != null)
                {
                    parent.OnMoved(rosId);
                }
            };

            // disable external updates while dragging
            Control.PointerDown += () =>
            {
                parent.PoseUpdateEnabled = false;
                parent.OnMouseEvent(rosId, null, MouseEventType.Down);
            };
            Control.PointerUp += () =>
            {
                parent.PoseUpdateEnabled = true;
                parent.OnMouseEvent(rosId, null, MouseEventType.Up);

                if (Control.InteractionMode == InteractionModeType.ClickOnly)
                {
                    parent.OnMouseEvent(rosId, null, MouseEventType.Click);
                }

                /*
                var assetHolder = UnityEngine.Resources.Load<GameObject>("App Asset Holder")
                    .GetComponent<AppAssetHolder>();
                AudioSource.PlayClipAtPoint(assetHolder.Click, transform.position);
                */
            };

            Control.MenuClicked += unityPositionHint => { parent.ShowMenu(unityPositionHint); };

            return Control;
        }

        void DisposeControlDisplay()
        {
            if (Control == null)
            {
                return;
            }

            Control.Suspend();
            ResourcePool.Return(Resource.Displays.InteractiveControl, ((MonoBehaviour)Control).gameObject);
            Control = null;
        }

        void UpdateInteractionMode(InteractionMode interactionMode, OrientationMode orientationMode,
            bool independentMarkerOrientation)
        {
            if (interactionMode < 0 || interactionMode > InteractionMode.MoveRotate3D)
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

                InteractionModeType interactionModeType;
                switch (interactionMode)
                {
                    case InteractionMode.Menu:
                    case InteractionMode.Button:
                        interactionModeType = InteractionModeType.ClickOnly;
                        break;
                    case InteractionMode.MoveAxis:
                        interactionModeType = InteractionModeType.MoveAxisX;
                        break;
                    case InteractionMode.MovePlane:
                        interactionModeType = InteractionModeType.MovePlaneYz;
                        break;
                    case InteractionMode.RotateAxis:
                        interactionModeType = InteractionModeType.RotateAxisX;
                        break;
                    case InteractionMode.MoveRotate:
                        interactionModeType = InteractionModeType.MovePlaneYzRotateAxisX;
                        break;
                    case InteractionMode.Move3D:
                        interactionModeType = InteractionModeType.Move3D;
                        break;
                    case InteractionMode.Rotate3D:
                        interactionModeType = InteractionModeType.Rotate3D;
                        break;
                    case InteractionMode.MoveRotate3D:
                        interactionModeType = InteractionModeType.MoveRotate3D;
                        break;
                    default:
                        interactionModeType = InteractionModeType.None; // unreachable
                        break;
                }

                mControl.InteractionMode = interactionModeType;
            }

            if (orientationMode < 0 || orientationMode > OrientationMode.ViewFacing)
            {
                description.Append(ErrorStr).Append("Unknown orientation mode ").Append((int)orientationMode)
                    .AppendLine();
                numErrors++;

                markerNode.GetComponent<Billboard>().enabled = false;
                markerNode.transform.localRotation = Quaternion.identity;

                if (Control != null)
                {
                    Control.PointsToCamera = false;
                    Control.KeepAbsoluteRotation = false;
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

            if (Control == null)
            {
                return;
            }

            switch (orientationMode)
            {
                case OrientationMode.ViewFacing:
                    Control.KeepAbsoluteRotation = false;
                    Control.PointsToCamera = true;
                    Control.HandlesPointToCamera = !independentMarkerOrientation;
                    break;
                case OrientationMode.Inherit:
                    Control.PointsToCamera = false;
                    Control.KeepAbsoluteRotation = false;
                    break;
                case OrientationMode.Fixed:
                    Control.PointsToCamera = false;
                    Control.KeepAbsoluteRotation = false;
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

        void UpdateMarkers([NotNull] IEnumerable<Marker> msg)
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

                        Task _ = markerObject.SetAsync(marker); // TODO: deal with mesh loading
                        if (string.IsNullOrEmpty(marker.Header.FrameId))
                        {
                            markerObject.transform.SetParentLocal(markerNode.transform);
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

            if (Control == null)
            {
                return;
            }

            Control.Suspend();
            GameObject controlObject = ((MonoBehaviour)Control).gameObject;
            if (Settings.IsHololens)
            {
                HololensManager.ResourcePool.Dispose(controlObject);
            }
            else
            {
                ResourcePool.Return(Resource.Displays.InteractiveControl, controlObject);
            }

            Control = null;
        }

        public void GenerateLog([NotNull] StringBuilder baseDescription)
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

        void DeleteMarkerObject([NotNull] MarkerObject marker)
        {
            marker.BoundsChanged -= RecalculateBounds;
            marker.DestroySelf();
        }

        [NotNull]
        MarkerObject CreateMarkerObject()
        {
            var markerObject = new GameObject("MarkerObject").AddComponent<MarkerObject>();
            markerObject.Visible = visible;
            markerObject.BoundsChanged += RecalculateBounds;
            return markerObject;
        }

        [NotNull]
        static string InteractionModeToString(int mode)
        {
            switch ((InteractionMode)mode)
            {
                case InteractionMode.None:
                    return "None";
                case InteractionMode.Menu:
                    return "Menu";
                case InteractionMode.Button:
                    return "Button";
                case InteractionMode.MoveAxis:
                    return "MoveAxis";
                case InteractionMode.MovePlane:
                    return "MovePlane";
                case InteractionMode.RotateAxis:
                    return "RotateAxis";
                case InteractionMode.MoveRotate:
                    return "MoveRotate";
                case InteractionMode.Move3D:
                    return "Move3D";
                case InteractionMode.Rotate3D:
                    return "Rotate3D";
                case InteractionMode.MoveRotate3D:
                    return "MoveRotate3D";
                default:
                    return $"Unknown ({mode.ToString()})";
            }
        }

        [NotNull]
        static string OrientationModeToString(int mode)
        {
            switch ((OrientationMode)mode)
            {
                case OrientationMode.Inherit:
                    return "Inherit";
                case OrientationMode.Fixed:
                    return "Fixed";
                case OrientationMode.ViewFacing:
                    return "ViewFacing";
                default:
                    return $"Unknown ({mode.ToString()})";
            }
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
            if (Control != null)
            {
                Control.Bounds = bounds.TransformBoundWithInverse(transform);
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