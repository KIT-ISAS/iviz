//#define SEND_TEST_MSG

using UnityEngine;

using System.Collections.Generic;
using System.Linq;
using System;
using Iviz.Msgs.visualization_msgs;

namespace Iviz.App
{

    public class InteractiveMarkerListener : DisplayableListener
    {
        RosSender<InteractiveMarkerFeedback> rosSender;

        readonly Dictionary<string, InteractiveMarkerObject> imarkers =
            new Dictionary<string, InteractiveMarkerObject>();

        [Serializable]
        public class Configuration
        {
            public Resource.Module module => Resource.Module.InteractiveMarker;
            public string topic = "";
            public bool disableExpiration;
        }

        public bool DisableExpiration
        {
            get => config.disableExpiration;
            set
            {
                config.disableExpiration = value;
            }
        }

        readonly Configuration config = new Configuration();
        public Configuration Config
        {
            get => config;
            set
            {
                config.topic = value.topic;
                DisableExpiration = value.disableExpiration;
            }
        }

        public override void StartListening()
        {
            Topic = config.topic;
            Listener = new RosListener<InteractiveMarkerUpdate>(config.topic, Handler);
            GameThread.EverySecond += UpdateStats;
            GameThread.EverySecond += CheckForExpiredMarkers;

            rosSender = new RosSender<InteractiveMarkerFeedback>("/interactive_markers/feedback");
        }

        public override void Unsubscribe()
        {
            GameThread.EverySecond -= UpdateStats;
            GameThread.EverySecond -= CheckForExpiredMarkers;
            Listener?.Stop();
            Listener = null;

            foreach (InteractiveMarkerObject imarker in imarkers.Values)
            {
                imarker.Stop();
                ResourcePool.Dispose(Resource.Displays.MarkerObject, imarker.gameObject);
            }
            imarkers.Clear();
            rosSender.Stop();
        }

        public void ClearAll()
        {
            foreach (InteractiveMarkerObject imarker in imarkers.Values)
            {
                imarker.Stop();
                ResourcePool.Dispose(Resource.Displays.MarkerObject, imarker.gameObject);
            }
            imarkers.Clear();
        }

        void Handler(InteractiveMarkerUpdate msg)
        {
            if (msg.type == InteractiveMarkerUpdate.KEEP_ALIVE)
            {
                return;
            }

            msg.markers.ForEach(CreateInteractiveMarker);
            msg.poses.ForEach(UpdateInteractiveMarkerPose);
            msg.erases.ForEach(DestroyInteractiveMarker);
        }

        void CreateInteractiveMarker(InteractiveMarker msg)
        {
            string id = msg.name;
            if (!imarkers.TryGetValue(id, out InteractiveMarkerObject imarker))
            {
                imarker = ResourcePool.
                    GetOrCreate(Resource.Displays.InteractiveMarkerObject, transform).
                    GetComponent<InteractiveMarkerObject>();
                imarker.Parent = TFListener.DisplaysFrame;
                imarker.Clicked += (control, pose, point, button) => OnInteractiveControlObjectClicked(id, pose, control, point, button);
                imarker.transform.SetParentLocal(transform);
                imarkers[id] = imarker;
            }
            imarker.Set(msg);
        }

        void UpdateInteractiveMarkerPose(InteractiveMarkerPose msg)
        {
            string id = msg.name;
            if (!imarkers.TryGetValue(id, out InteractiveMarkerObject im))
            {
                return;
            }
            im.transform.SetLocalPose(msg.pose.Ros2Unity());
            im.UpdateExpirationTime();
        }

        void DestroyInteractiveMarker(string id)
        {
            if (!imarkers.TryGetValue(id, out InteractiveMarkerObject imarker))
            {
                return;
            }

            imarker.Stop();
            ResourcePool.Dispose(Resource.Displays.InteractiveMarkerObject, imarker.gameObject);
            imarkers.Remove(id);
        }

        void OnInteractiveControlObjectClicked(string imarkerId, Pose controlPose, string controlId, Vector3 position, int button)
        {
            InteractiveMarkerFeedback msg = new InteractiveMarkerFeedback
            {
                header = Utils.CreateHeader(),
                client_id = "iviz",
                marker_name = imarkerId,
                control_name = controlId,
                event_type = InteractiveMarkerFeedback.BUTTON_CLICK,
                pose = controlPose.Unity2RosPose(),
                mouse_point = position.Unity2RosPoint(),
                mouse_point_valid = true
            };
            rosSender.Publish(msg);
            Debug.Log("Publishing feedback! " + msg.pose);
        }

        void CheckForExpiredMarkers()
        {
            if (DisableExpiration)
            {
                return;
            }

            DateTime now = DateTime.Now;
            var deadMarkers = imarkers.Where(x => x.Value.ExpirationTime < now).ToArray();
            foreach(var entry in deadMarkers)
            {
                DestroyInteractiveMarker(entry.Key);
            }
        }
    }

}
