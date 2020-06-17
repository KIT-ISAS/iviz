
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
        [DataMember] public bool Visible { get; set; } = true;
        [DataMember] public string Topic { get; set; } = "";
        [DataMember] public bool DisableExpiration { get; set; } = false;
    }

    public class InteractiveMarkerListener : ListenerController
    {
        RosSender<InteractiveMarkerFeedback> rosSender;

        public override TFFrame Frame => TFListener.BaseFrame;

        readonly Dictionary<string, InteractiveMarkerObject> imarkers =
            new Dictionary<string, InteractiveMarkerObject>();

        public override ModuleData ModuleData { get; set; }

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

            imarkers.Values.ForEach(DeleteMarkerObject);
            imarkers.Clear();
            rosSender.Stop();
        }

        public void ClearAll()
        {
            imarkers.Values.ForEach(DeleteMarkerObject);
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
                imarker = CreateMarkerObject();
                imarker.Parent = TFListener.ListenersFrame;
                imarker.Clicked += (control, pose, point, button) => OnInteractiveControlObjectClicked(id, pose, control, point, button);
                imarker.transform.SetParentLocal(transform);
                imarkers[id] = imarker;
            }
            imarker.Set(msg);
        }

        static InteractiveMarkerObject CreateMarkerObject()
        {
            GameObject gameObject = new GameObject();
            gameObject.name = "InteractiveMarkerObject";
            return gameObject.AddComponent<InteractiveMarkerObject>();
        }

        static void DeleteMarkerObject(InteractiveMarkerObject imarker)
        {
            imarker.Stop();
            Destroy(imarker.gameObject);
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
            Destroy(imarker.gameObject);
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
