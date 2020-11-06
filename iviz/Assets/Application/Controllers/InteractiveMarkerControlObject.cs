using System.Collections.Generic;
using System.Text;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Msgs.VisualizationMsgs;
using Iviz.Resources;
using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.Controllers
{
    public sealed class InteractiveMarkerControlObject : MonoBehaviour
    {
        public delegate void MouseEventAction(in Pose pose, in Vector3 point, MouseEventType type);

        readonly StringBuilder description = new StringBuilder();
        readonly Dictionary<string, MarkerObject> markers = new Dictionary<string, MarkerObject>();
        //readonly HashSet<string> markersToDelete = new HashSet<string>();
        InteractiveControl control;

        bool markerIsInteractive;
        GameObject markerNode;

        void Awake()
        {
            markerNode = new GameObject("[MarkerNode]");
            markerNode.transform.SetParent(transform, false);
            markerNode.AddComponent<Billboard>().enabled = false;
        }

        public event MouseEventAction MouseEvent;
        public event InteractiveControl.MovedAction Moved;

        public void Set([NotNull] InteractiveMarkerControl msg)
        {
            name = $"[ControlObject '{msg.Name}']";

            description.Clear();
            description.Append("<b>** Control '").Append(msg.Name).Append("'</b>").AppendLine();
            description.Append("Description: ").Append(msg.Description.Length == 0 ? "[]" : msg.Description)
                .AppendLine();

            transform.localRotation = msg.Orientation.Ros2Unity();

            InteractionMode interactionMode = (InteractionMode) msg.InteractionMode;
            OrientationMode orientationMode = (OrientationMode) msg.OrientationMode;
            description.Append("InteractionMode: ").Append(EnumToString(interactionMode)).AppendLine();
            description.Append("OrientationMode: ").Append(EnumToString(orientationMode)).AppendLine();
            
            UpdateMarkers(msg.Markers);
            UpdateInteractionMode(interactionMode, orientationMode, msg.IndependentMarkerOrientation);

            description.Append("Size: ").Append(markers.Count).AppendLine();
        }

        void EnsureControlDisplayExists()
        {
            if (control != null)
            {
                return;
            }

            control = ResourcePool.GetOrCreate<InteractiveControl>(Resource.Displays.InteractiveControl, transform);
            control.TargetTransform = transform.parent;
            control.Moved += (in Pose pose) => Moved?.Invoke(transform.AsPose());
        }

        void UpdateInteractionMode(InteractionMode interactionMode, OrientationMode orientationMode,
            bool independentMarkerOrientation)
        {
            bool clickable = interactionMode == InteractionMode.Button;
            markers.Values.ForEach(marker => marker.Clickable = clickable);
            
            switch (interactionMode)
            {
                case InteractionMode.None:
                    markerIsInteractive = false;
                    break;
                case InteractionMode.Menu:
                case InteractionMode.Button:
                    markerIsInteractive = true;
                    break;
                case InteractionMode.MoveAxis:
                    EnsureControlDisplayExists();
                    control.InteractionMode = InteractiveControl.InteractionModeType.MoveAxisX;
                    break;
                case InteractionMode.MovePlane:
                    EnsureControlDisplayExists();
                    control.InteractionMode = InteractiveControl.InteractionModeType.MovePlaneYZ;
                    break;
                case InteractionMode.RotateAxis:
                    EnsureControlDisplayExists();
                    control.InteractionMode = InteractiveControl.InteractionModeType.RotateAxisX;
                    break;
                case InteractionMode.MoveRotate:
                    EnsureControlDisplayExists();
                    control.InteractionMode = InteractiveControl.InteractionModeType.MovePlaneYZ_RotateAxisX;
                    break;
                case InteractionMode.Move3D:
                    EnsureControlDisplayExists();
                    control.InteractionMode = InteractiveControl.InteractionModeType.Move3D;
                    break;
                case InteractionMode.Rotate3D:
                    EnsureControlDisplayExists();
                    control.InteractionMode = InteractiveControl.InteractionModeType.Rotate3D;
                    break;
                case InteractionMode.MoveRotate3D:
                    EnsureControlDisplayExists();
                    control.InteractionMode = InteractiveControl.InteractionModeType.MoveRotate3D;
                    break;
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

            if (control is null)
            {
                return;
            }

            switch (orientationMode)
            {
                case OrientationMode.ViewFacing:
                    control.KeepAbsoluteRotation = false;
                    control.PointsToCamera = true;
                    control.CameraPivotIsParent = !independentMarkerOrientation;
                    break;
                case OrientationMode.Inherit:
                    control.PointsToCamera = false;
                    control.KeepAbsoluteRotation = false;
                    break;
                case OrientationMode.Fixed:
                    control.PointsToCamera = false;
                    control.KeepAbsoluteRotation = true;
                    break;
            }
        }

        void UpdateMarkers([NotNull] Marker[] msg)
        {
            /*
            markersToDelete.Clear();
            foreach (string id in markers.Keys)
            {
                markersToDelete.Add(id);
            }
            */

            foreach (Marker marker in msg)
            {
                string id = MarkerListener.IdFromMessage(marker);
                switch (marker.Action)
                {
                    case Marker.ADD:
                        MarkerObject markerObject;
                        if (markers.TryGetValue(id, out MarkerObject existingMarker))
                        {
                            markerObject = existingMarker;
                        }
                        else 
                        {
                            markerObject = CreateMarkerObject();
                            markerObject.MouseEvent += OnMarkerClicked;
                            markers[id] = markerObject;
                        }

                        markerObject.Set(marker);
                        if (marker.Header.FrameId.Length == 0)
                        {
                            markerObject.transform.SetParentLocal(markerNode.transform);
                        }

                        //markersToDelete.Remove(id);
                        break;
                    case Marker.DELETE:
                        if (markers.TryGetValue(id, out MarkerObject markerToDelete))
                        {
                            DeleteMarkerObject(markerToDelete);
                            markers.Remove(id);
                            //markersToDelete.Remove(id);
                        }

                        break;
                }
            }
            
            /*
            foreach (string id in markersToDelete)
            {
                MarkerObject markerToDelete = markers[id];
                DeleteMarkerObject(markerToDelete);
                markers.Remove(id);
            }
            */
        }

        void OnMarkerClicked(in Vector3 point, MouseEventType type)
        {
            if (!markerIsInteractive)
            {
                return;
            }

            MouseEvent?.Invoke(transform.AsPose(), point, type);
        }

        public void Stop()
        {
            markers.Values.ForEach(DeleteMarkerObject);
            markers.Clear();
            //markersToDelete.Clear();

            if (!(control is null))
            {
                control.Stop();
                ResourcePool.Dispose(Resource.Displays.InteractiveControl, control.gameObject);
            }

            MouseEvent = null;
            Moved = null;
        }

        public void GenerateLog([NotNull] StringBuilder baseDescription)
        {
            baseDescription.Append(description);

            foreach (var marker in markers.Values) marker.GenerateLog(baseDescription);
        }
        
        static void DeleteMarkerObject([NotNull] MarkerObject marker)
        {
            marker.Stop();
            Destroy(marker.gameObject);
        }

        static MarkerObject CreateMarkerObject()
        {
            GameObject gameObject = new GameObject("MarkerObject");
            return gameObject.AddComponent<MarkerObject>();
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
                    return "Rotat3D";
                case InteractionMode.MoveRotate3D:
                    return "MoveRotate3D";
                default:
                    return $"Unknown ({(int) mode})";
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
                    return $"Unknown ({(int) mode})";
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