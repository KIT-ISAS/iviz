//#define SEND_TEST_MSG

using UnityEngine;

using System.Collections.Generic;
using System.Linq;
using System;
using Iviz.Msgs.VisualizationMsgs;
using Iviz.RoslibSharp;
using System.Runtime.Serialization;
using Iviz.Resources;

namespace Iviz.App.Listeners
{
    [DataContract]
    public class InteractiveMarkerConfiguration : JsonToString, IConfiguration
    {
        [DataMember] public Guid Id { get; set; } = Guid.NewGuid();
        [DataMember] public Resource.Module Module => Resource.Module.InteractiveMarker;
        [DataMember] public string Topic { get; set; } = "";
        [DataMember] public bool DisableExpiration { get; set; } = false;
    }

    public class InteractiveMarkerListener : TopicListener
    {
        RosSender<InteractiveMarkerFeedback> rosSender;

        readonly Dictionary<string, InteractiveMarkerObject> imarkers =
            new Dictionary<string, InteractiveMarkerObject>();

        public bool DisableExpiration
        {
            get => config.DisableExpiration;
            set
            {
                config.DisableExpiration = value;
            }
        }

        readonly InteractiveMarkerConfiguration config = new InteractiveMarkerConfiguration();
        public InteractiveMarkerConfiguration Config
        {
            get => config;
            set
            {
                config.Topic = value.Topic;
                DisableExpiration = value.DisableExpiration;
            }
        }

        public override void StartListening()
        {
            base.StartListening();
            Listener = new RosListener<InteractiveMarkerUpdate>(config.Topic, Handler);
            GameThread.EverySecond += CheckForExpiredMarkers;
            rosSender = new RosSender<InteractiveMarkerFeedback>("/interactive_markers/feedback");
        }

        public override void Stop()
        {
            base.Stop();
            GameThread.EverySecond -= CheckForExpiredMarkers;

            foreach (InteractiveMarkerObject imarker in imarkers.Values)
            {
                imarker.Stop();
                ResourcePool.Dispose(Resource.Listeners.MarkerObject, imarker.gameObject);
            }
            imarkers.Clear();
            rosSender.Stop();
        }

        public void ClearAll()
        {
            foreach (InteractiveMarkerObject imarker in imarkers.Values)
            {
                imarker.Stop();
                ResourcePool.Dispose(Resource.Listeners.MarkerObject, imarker.gameObject);
            }
            imarkers.Clear();
        }

        void Handler(InteractiveMarkerUpdate msg)
        {
            if (msg.Type == InteractiveMarkerUpdate.KEEP_ALIVE)
            {
                return;
            }

            msg.Markers.ForEach(CreateInteractiveMarker);
            msg.Poses.ForEach(UpdateInteractiveMarkerPose);
            msg.Erases.ForEach(DestroyInteractiveMarker);
        }

        void CreateInteractiveMarker(InteractiveMarker msg)
        {
            string id = msg.Name;
            if (!imarkers.TryGetValue(id, out InteractiveMarkerObject imarker))
            {
                imarker = ResourcePool.
                    GetOrCreate(Resource.Listeners.InteractiveMarkerObject, transform).
                    GetComponent<InteractiveMarkerObject>();
                imarker.Parent = TFListener.ListenersFrame;
                imarker.Clicked += (control, pose, point, button) => OnInteractiveControlObjectClicked(id, pose, control, point, button);
                imarker.transform.SetParentLocal(transform);
                imarkers[id] = imarker;
            }
            imarker.Set(msg);
        }

        void UpdateInteractiveMarkerPose(InteractiveMarkerPose msg)
        {
            string id = msg.Name;
            if (!imarkers.TryGetValue(id, out InteractiveMarkerObject im))
            {
                return;
            }
            im.transform.SetLocalPose(msg.Pose.Ros2Unity());
            im.UpdateExpirationTime();
        }

        void DestroyInteractiveMarker(string id)
        {
            if (!imarkers.TryGetValue(id, out InteractiveMarkerObject imarker))
            {
                return;
            }

            imarker.Stop();
            ResourcePool.Dispose(Resource.Listeners.InteractiveMarkerObject, imarker.gameObject);
            imarkers.Remove(id);
        }

        uint feedSeq = 0;
        void OnInteractiveControlObjectClicked(string imarkerId, Pose controlPose, string controlId, Vector3 position, int button)
        {
            InteractiveMarkerFeedback msg = new InteractiveMarkerFeedback
            {
                Header = RosUtils.CreateHeader(feedSeq++),
                ClientId = "iviz",
                MarkerName = imarkerId,
                ControlName = controlId,
                EventType = InteractiveMarkerFeedback.BUTTON_CLICK,
                Pose = controlPose.Unity2RosPose(),
                MousePoint = position.Unity2RosPoint(),
                MousePointValid = true
            };
            rosSender.Publish(msg);
            Debug.Log("Publishing feedback! " + msg.Pose);
        }

        void CheckForExpiredMarkers()
        {
            if (DisableExpiration)
            {
                return;
            }

            DateTime now = DateTime.Now;
            var deadMarkers = imarkers.Where(x => x.Value.ExpirationTime < now).ToArray();
            foreach (var entry in deadMarkers)
            {
                DestroyInteractiveMarker(entry.Key);
            }
        }
    }

}
