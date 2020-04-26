using UnityEngine;
using System.Collections.Generic;
using System;
using Iviz.Msgs.visualization_msgs;

namespace Iviz.App
{
    public class InteractiveMarkerControlObject : Display
    {
        public string Description { get; private set; }
        public string Id { get; private set; }

        public int InteractionMode { get; private set; }

        readonly Dictionary<string, MarkerObject> markers = new Dictionary<string, MarkerObject>();
        readonly HashSet<string> markersToDelete = new HashSet<string>();

        public event Action<Pose, Vector3, int> Clicked;

        public void Set(InteractiveMarkerControl msg)
        {
            Description = msg.description;
            name = msg.name;
            Id = msg.name;

            transform.localRotation = msg.orientation.Ros2Unity();

            UpdateMarkers(msg.markers);

            InteractionMode = msg.interaction_mode;
            UpdateInteractionMode();
        }

        void UpdateInteractionMode()
        {
            switch (InteractionMode)
            {
                case InteractiveMarkerControl.NONE:
                    markers.Values.ForEach(x => x.EnableColliders(false));
                    break;
                case InteractiveMarkerControl.MENU:
                case InteractiveMarkerControl.BUTTON:
                    markers.Values.ForEach(x => x.EnableColliders(true));
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
                switch (marker.action)
                {
                    case Marker.ADD:
                        if (!markers.TryGetValue(id, out MarkerObject markerToAdd))
                        {
                            markerToAdd = ResourcePool.GetOrCreate(Resource.Displays.MarkerObject, transform).GetComponent<MarkerObject>();
                            markerToAdd.Parent = TFListener.DisplaysFrame;
                            markerToAdd.Clicked += (point, button) => Clicked?.Invoke(transform.AsPose(), point, button);
                            markers[id] = markerToAdd;
                        }
                        markerToAdd.Set(marker);
                        if (marker.header.frame_id == "")
                        {
                            markerToAdd.transform.SetParentLocal(transform);
                        }
                        markersToDelete.Remove(id);
                        break;
                    case Marker.DELETE:
                        if (markers.TryGetValue(id, out MarkerObject markerToDelete))
                        {
                            markerToDelete.Stop();
                            ResourcePool.Dispose(Resource.Displays.MarkerObject, markerToDelete.gameObject);
                            markers.Remove(id);
                            markersToDelete.Remove(id);
                        }
                        break;
                }
            }
            markersToDelete.ForEach(x =>
            {
                MarkerObject markerToDelete = markers[x];
                markerToDelete.Stop();
                ResourcePool.Dispose(Resource.Displays.MarkerObject, markerToDelete.gameObject);
                markers.Remove(x);
            });
        }

        public override void Stop()
        {
            markers.Values.ForEach(markerToDelete =>
            {
                markerToDelete.Stop();
                ResourcePool.Dispose(Resource.Displays.MarkerObject, markerToDelete.gameObject);
            });
            markers.Clear();
            markersToDelete.Clear();
            Clicked = null;
        }

        /*
        public override InteractiveMarkerFeedback OnClick(Vector3 point, int button)
        {
            //if (button == 1)
            {
                Vector3 hint = Bounds.center;
                Parent.ShowMenu(hint);
            }
            if (button != responseButton)
            {
                return null;
            }

            Debug.Log("InteractiveMarker Control: Sending feedback");

            return new InteractiveMarkerFeedback
            {
                header = Utils.CreateHeader(),
                client_id = RosConfig.MyId,
                marker_name = Parent.Id,
                control_name = Id,
                event_type = InteractiveMarkerFeedback.BUTTON_CLICK,
                pose = Parent.transform.AsPose().Unity2RosPose(),
                mouse_point = point.Unity2RosPoint(),
                mouse_point_valid = true
            };
        }

        public override void Select()
        {
            if (selectFrame != null)
            {
                Destroy(selectFrame);
            }
            selectFrame = Instantiate(Resources.Load<GameObject>("SelectFrame"));
            selectFrame.transform.localScale = Bounds.size;
            //selectFrame.transform.localPosition = Bounds.center;
            selectFrame.transform.SetParentLocal(transform);
        }

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
