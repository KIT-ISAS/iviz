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
                    Control.Layer = value ? LayerType.Clickable : LayerType.IgnoreRaycast;
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

            transform.localRotation = msg.Orientation.Ros2Unity();

            InteractionMode interactionMode = (InteractionMode) msg.InteractionMode;
            OrientationMode orientationMode = (OrientationMode) msg.OrientationMode;
            description.Append("InteractionMode: ").Append(EnumToString(interactionMode)).AppendLine();
            description.Append("OrientationMode: ").Append(EnumToString(orientationMode)).AppendLine();

            UpdateMarkers(msg.Markers);
            RecalculateBounds();

            UpdateInteractionMode(interactionMode, orientationMode, msg.IndependentMarkerOrientation);

            if (markers.Count == 0)
            {
                description.Append("Markers: Empty").AppendLine();
            }
            else
            {
                description.Append("Markers: ").Append(markers.Count).AppendLine();
            }
        }

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

                var assetHolder = UnityEngine.Resources.Load<GameObject>("App Asset Holder")
                    .GetComponent<AppAssetHolder>();
                AudioSource.PlayClipAtPoint(assetHolder.Click, transform.position);
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
            ResourcePool.Return(Resource.Displays.InteractiveControl, ((MonoBehaviour) Control).gameObject);
            Control = null;
        }

        void UpdateInteractionMode(InteractionMode interactionMode, OrientationMode orientationMode,
            bool independentMarkerOrientation)
        {
            if (interactionMode < 0 || interactionMode > InteractionMode.MoveRotate3D)
            {
                description.Append(ErrorStr).Append("Unknown interaction mode ").Append((int) interactionMode)
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

                switch (interactionMode)
                {
                    case InteractionMode.Menu:
                    case InteractionMode.Button:
                        mControl.InteractionMode = InteractionModeType.ClickOnly;
                        break;
                    case InteractionMode.MoveAxis:
                        mControl.InteractionMode = InteractionModeType.MoveAxisX;
                        break;
                    case InteractionMode.MovePlane:
                        mControl.InteractionMode = InteractionModeType.MovePlaneYz;
                        break;
                    case InteractionMode.RotateAxis:
                        mControl.InteractionMode = InteractionModeType.RotateAxisX;
                        break;
                    case InteractionMode.MoveRotate:
                        mControl.InteractionMode = InteractionModeType.MovePlaneYzRotateAxisX;
                        break;
                    case InteractionMode.Move3D:
                        mControl.InteractionMode = InteractionModeType.Move3D;
                        break;
                    case InteractionMode.Rotate3D:
                        mControl.InteractionMode = InteractionModeType.Rotate3D;
                        break;
                    case InteractionMode.MoveRotate3D:
                        mControl.InteractionMode = InteractionModeType.MoveRotate3D;
                        break;
                }
            }

            if (orientationMode < 0 || orientationMode > OrientationMode.ViewFacing)
            {
                description.Append(ErrorStr).Append("Unknown orientation mode ").Append((int) orientationMode)
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

            if (Control is null)
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
                    Control.KeepAbsoluteRotation = true;
                    break;
            }
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
            foreach (var markerObject in markers.Values)
            {
                DeleteMarkerObject(markerObject);
            }

            markers.Clear();

            if (Control == null)
            {
                return;
            }

            Control.Suspend();
            GameObject controlObject = ((MonoBehaviour) Control).gameObject;
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
        static string EnumToString(InteractionMode mode)
        {
            switch (mode)
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
                    return $"Unknown";
            }
        }

        [NotNull]
        static string EnumToString(OrientationMode mode)
        {
            switch (mode)
            {
                case OrientationMode.Inherit:
                    return "Inherit";
                case OrientationMode.Fixed:
                    return "Fixed";
                case OrientationMode.ViewFacing:
                    return "ViewFacing";
                default:
                    return "Unknown";
            }
        }

        void RecalculateBounds()
        {
            IEnumerable<(Bounds? Bounds, Transform Transform)> innerBounds = markers.Values
                .Select(marker => (marker.Bounds, marker.Transform));

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