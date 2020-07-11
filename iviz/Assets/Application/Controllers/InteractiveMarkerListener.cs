using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;
using Iviz.Msgs.VisualizationMsgs;
using Iviz.RoslibSharp;
using System.Runtime.Serialization;
using Iviz.App.Displays;
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
        [DataMember] public bool DisableExpiration { get; set; } = true;
    }

    public class InteractiveMarkerListener : ListenerController
    {
        readonly SimpleDisplayNode node;
        
        public RosSender<InteractiveMarkerFeedback> RosSender { get; private set; }

        public override TFFrame Frame => TFListener.MapFrame;

        readonly Dictionary<string, InteractiveMarkerObject> imarkers =
            new Dictionary<string, InteractiveMarkerObject>();

        public override ModuleData ModuleData { get; }

        public bool DisableExpiration
        {
            get => config.DisableExpiration;
            set => config.DisableExpiration = value;
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

        public InteractiveMarkerListener(ModuleData moduleData)
        {
            ModuleData = moduleData;
            node = SimpleDisplayNode.Instantiate("[InteractiveMarkerListener]");
        }

        public override void StartListening()
        {
            Listener = new RosListener<InteractiveMarkerUpdate>(config.Topic, Handler);
            GameThread.EverySecond += CheckForExpiredMarkers;
            RosSender = new RosSender<InteractiveMarkerFeedback>("/interactive_markers/feedback");
        }

        public override void Stop()
        {
            base.Stop();
            GameThread.EverySecond -= CheckForExpiredMarkers;

            imarkers.Values.ForEach(DeleteMarkerObject);
            imarkers.Clear();
            RosSender.Stop();
            
            node.Stop();
            UnityEngine.Object.Destroy(node.gameObject);
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
                imarker.MouseEvent += (string controlId, in Pose pose, in Vector3 point, MouseEventType type) =>
                {
                    OnInteractiveControlObjectMouseEvent(id, pose, controlId, point, type);
                };
                imarker.Moved += (string controlId, in Pose pose) =>
                {
                    OnInteractiveControlObjectMoved(id, pose, controlId);
                };
                imarker.transform.SetParentLocal(node.transform);
                imarkers[id] = imarker;
            }

            imarker.Set(msg);
        }

        static InteractiveMarkerObject CreateMarkerObject()
        {
            GameObject gameObject = new GameObject("InteractiveMarkerObject");
            return gameObject.AddComponent<InteractiveMarkerObject>();
        }

        static void DeleteMarkerObject(InteractiveMarkerObject imarker)
        {
            imarker.Stop();
            UnityEngine.Object.Destroy(imarker.gameObject);
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
            UnityEngine.Object.Destroy(imarker.gameObject);
            imarkers.Remove(id);
        }

        uint feedSeq = 0;

        void OnInteractiveControlObjectMouseEvent(
            string imarkerId, in Pose controlPose,
            string controlId, in Vector3 position,
            MouseEventType type)
        {
            byte eventType;
            switch (type)
            {
                case MouseEventType.Click:
                    eventType = InteractiveMarkerFeedback.BUTTON_CLICK;
                    break;
                case MouseEventType.Down:
                    eventType = InteractiveMarkerFeedback.MOUSE_DOWN;
                    break;
                case MouseEventType.Up:
                    eventType = InteractiveMarkerFeedback.MOUSE_UP;
                    break;
                default:
                    return; // shouldn't happen
            }

            InteractiveMarkerFeedback msg = new InteractiveMarkerFeedback
            (
                Header: RosUtils.CreateHeader(feedSeq++),
                ClientId: ConnectionManager.MyId,
                MarkerName: imarkerId,
                ControlName: controlId,
                EventType: eventType,
                Pose: TFListener.RelativePose(controlPose).Unity2RosPose(),
                MenuEntryId: 0,
                MousePoint: position.Unity2RosPoint(),
                MousePointValid: true
            );
            RosSender.Publish(msg);
        }

        void OnInteractiveControlObjectMoved(string imarkerId, in Pose controlPose, string controlId)
        {
            InteractiveMarkerFeedback msg = new InteractiveMarkerFeedback
            (
                Header: RosUtils.CreateHeader(feedSeq++),
                ClientId: ConnectionManager.MyId,
                MarkerName: imarkerId,
                ControlName: controlId,
                EventType: InteractiveMarkerFeedback.POSE_UPDATE,
                Pose: controlPose.Unity2RosPose(),
                MenuEntryId: 0,
                MousePoint: Vector3.zero.Unity2RosPoint(),
                MousePointValid: false
            );
            RosSender.Publish(msg);
        }

        void CheckForExpiredMarkers()
        {
            if (DisableExpiration)
            {
                return;
            }

            DateTime now = DateTime.Now;
            string[] deadMarkers =
                imarkers.Where(x => x.Value.ExpirationTime < now).Select(x => x.Key).ToArray();
            foreach (string key in deadMarkers)
            {
                DestroyInteractiveMarker(key);
            }
        }
    }
}