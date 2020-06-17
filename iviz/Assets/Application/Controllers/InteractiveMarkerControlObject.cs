using UnityEngine;
using System.Collections.Generic;
using System;
using Iviz.App.Displays;
using Iviz.Msgs.VisualizationMsgs;
using Iviz.Resources;

namespace Iviz.App.Listeners
{
    public sealed class InteractiveMarkerControlObject : MonoBehaviour
    {
        public string Description { get; private set; }
        public string Id { get; private set; }
        public int InteractionMode { get; private set; }

        readonly Dictionary<string, MarkerObject> markers = new Dictionary<string, MarkerObject>();
        readonly HashSet<string> markersToDelete = new HashSet<string>();

        public event Action<Pose, Vector3, int> Clicked;

        public void Set(InteractiveMarkerControl msg)
        {
            Description = msg.Description;
            name = msg.Name;
            Id = msg.Name;

            transform.localRotation = msg.Orientation.Ros2Unity();

            UpdateMarkers(msg.Markers);

            InteractionMode = msg.InteractionMode;
            UpdateInteractionMode();
        }

        void UpdateInteractionMode()
        {
            switch (InteractionMode)
            {
                case InteractiveMarkerControl.NONE:
                    markers.Values.ForEach(x => x.ColliderEnabled = false);
                    break;
                case InteractiveMarkerControl.MENU:
                case InteractiveMarkerControl.BUTTON:
                    markers.Values.ForEach(x => x.ColliderEnabled = true);
                    break;
                case InteractiveMarkerControl.MOVE_AXIS:
                case InteractiveMarkerControl.MOVE_PLANE:
                case InteractiveMarkerControl.ROTATE_AXIS:
                case InteractiveMarkerControl.MOVE_ROTATE:
                case InteractiveMarkerControl.MOVE_3D:
                case InteractiveMarkerControl.ROTATE_3D:
                    break;
            }
        }

        public void UpdateMarkers(Marker[] msg)
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
                            markerToAdd.Clicked += (point, button) => Clicked?.Invoke(transform.AsPose(), point, button);
                            markers[id] = markerToAdd;
                        }
                        markerToAdd.Set(marker);
                        if (marker.Header.FrameId == "")
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
            markersToDelete.ForEach(x =>
            {
                MarkerObject markerToDelete = markers[x];
                DeleteMarkerObject(markerToDelete);
                markers.Remove(x);
            });
        }

        public void Stop()
        {
            markers.Values.ForEach(DeleteMarkerObject);
            markers.Clear();
            markersToDelete.Clear();
            Clicked = null;
        }

        static void DeleteMarkerObject(MarkerObject marker)
        {
            marker.Stop();
            Destroy(marker.gameObject);
        }

        static MarkerObject CreateMarkerObject()
        {
            GameObject gameObject = new GameObject();
            gameObject.name = "MarkerObject";
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
