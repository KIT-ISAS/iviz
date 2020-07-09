using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;
using Iviz.App.Displays;
using Iviz.Displays;
using Iviz.Msgs.VisualizationMsgs;
using Iviz.Resources;

namespace Iviz.App.Listeners
{
    public sealed class InteractiveMarkerControlObject : MonoBehaviour
    {
        public string Description { get; private set; }
        public string Id { get; private set; }

        readonly Dictionary<string, MarkerObject> markers = new Dictionary<string, MarkerObject>();
        readonly HashSet<string> markersToDelete = new HashSet<string>();

        InteractiveControl control;

        public event Action<Pose, Vector3, int> Clicked;
        public event Action<Pose> Moved;

        public void Set(InteractiveMarkerControl msg)
        {
            Description = msg.Description;
            name = msg.Name;
            Id = msg.Name;

            transform.localRotation = msg.Orientation.Ros2Unity();

            UpdateMarkers(msg.Markers);
            UpdateInteractionMode(msg.InteractionMode);
        }

        void EnsureControlExists()
        {
            if (!(control is null))
            {
                return;
            }

            control = ResourcePool.GetOrCreate<InteractiveControl>(Resource.Displays.InteractiveControl, transform);
            control.ParentTransform = transform.parent;
            control.Moved += pose => Moved?.Invoke(pose);
        }

        void UpdateInteractionMode(int mode)
        {
            switch (mode)
            {
                case InteractiveMarkerControl.NONE:
                    markers.Values.ForEach(x => x.Clickable = false);
                    break;
                case InteractiveMarkerControl.MENU:
                case InteractiveMarkerControl.BUTTON:
                    markers.Values.ForEach(x => x.Clickable = true);
                    break;
                case InteractiveMarkerControl.MOVE_AXIS:
                    EnsureControlExists();
                    control.InteractionMode = InteractiveControl.InteractionModeType.MoveAxisX;
                    break;
                case InteractiveMarkerControl.MOVE_PLANE:
                    EnsureControlExists();
                    control.InteractionMode = InteractiveControl.InteractionModeType.MovePlaneYZ;
                    break;
                case InteractiveMarkerControl.ROTATE_AXIS:
                    EnsureControlExists();
                    control.InteractionMode = InteractiveControl.InteractionModeType.MoveAxisX;
                    break;
                case InteractiveMarkerControl.MOVE_ROTATE:
                    EnsureControlExists();
                    control.InteractionMode = InteractiveControl.InteractionModeType.MovePlaneYZ_RotateAxisX;
                    break;
                case InteractiveMarkerControl.MOVE_3D:
                    EnsureControlExists();
                    control.InteractionMode = InteractiveControl.InteractionModeType.Move3D;
                    break;
                case InteractiveMarkerControl.ROTATE_3D:
                    EnsureControlExists();
                    control.InteractionMode = InteractiveControl.InteractionModeType.Rotate3D;
                    break;
                case InteractiveMarkerControl.MOVE_ROTATE_3D:
                    EnsureControlExists();
                    control.InteractionMode = InteractiveControl.InteractionModeType.MoveRotate3D;
                    break;
            }
        }

        void UpdateMarkers(Marker[] msg)
        {
            markersToDelete.Clear();
            markers.Keys.ForEach(x => markersToDelete.Add(x));

            foreach (Marker marker in msg)
            {
                string id = MarkerListener.IdFromMessage(marker);
                switch (marker.Action)
                {
                    case Marker.ADD:
                        if (!markers.TryGetValue(id, out MarkerObject markerToAdd))
                        {
                            markerToAdd = CreateMarkerObject();
                            markerToAdd.Clicked += (point, button) =>
                                Clicked?.Invoke(transform.AsPose(), point, button);
                            markers[id] = markerToAdd;
                        }

                        markerToAdd.Set(marker);
                        if (marker.Header.FrameId.Length == 0)
                        {
                            markerToAdd.transform.SetParentLocal(transform);
                        }

                        markersToDelete.Remove(id);
                        break;
                    case Marker.DELETE:
                        if (markers.TryGetValue(id, out MarkerObject markerToDelete))
                        {
                            DeleteMarkerObject(markerToDelete);
                            markers.Remove(id);
                            markersToDelete.Remove(id);
                        }

                        break;
                }
            }

            foreach (string id in markersToDelete)
            {
                MarkerObject markerToDelete = markers[id];
                DeleteMarkerObject(markerToDelete);
                markers.Remove(id);
            }
        }

        public void Stop()
        {
            markers.Values.ForEach(DeleteMarkerObject);
            markers.Clear();
            markersToDelete.Clear();

            if (!(control is null))
            {
                control.Stop();
                ResourcePool.Dispose(Resource.Displays.InteractiveControl, control.gameObject);
            }
            Clicked = null;
            Moved = null;
        }

        static void DeleteMarkerObject(MarkerObject marker)
        {
            marker.Stop();
            Destroy(marker.gameObject);
        }

        static MarkerObject CreateMarkerObject()
        {
            GameObject gameObject = new GameObject("MarkerObject");
            return gameObject.AddComponent<MarkerObject>();
        }

        /*
        public override void Deselect(InteractableObject newSelection)
        {
            if (newSelection == null || newSelection.Parent != Parent)
            {
                Parent.HideMenu();
            }
            if (selectFrame != null)
            {
                Destroy(selectFrame);
                selectFrame = null;
            }
        }
        */
    }
}