using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;
using Iviz.Msgs.VisualizationMsgs;
using Iviz.Roslib;
using System.Runtime.Serialization;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Resources;
using Iviz.Ros;
using JetBrains.Annotations;

namespace Iviz.Controllers
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

    public sealed class InteractiveMarkerListener : ListenerController
    {
        readonly SimpleDisplayNode node;
        
        public RosSender<InteractiveMarkerFeedback> Publisher { get; private set; }

        public override TfFrame Frame => TfListener.MapFrame;

        readonly Dictionary<string, InteractiveMarkerObject> imarkers =
            new Dictionary<string, InteractiveMarkerObject>();

        public override IModuleData ModuleData { get; }

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

        public InteractiveMarkerListener([NotNull] IModuleData moduleData)
        {
            ModuleData = moduleData ?? throw new ArgumentNullException(nameof(moduleData));
            node = SimpleDisplayNode.Instantiate("[InteractiveMarkerListener]");
        }

        public override void StartListening()
        {
            Listener = new RosListener<InteractiveMarkerUpdate>(config.Topic, Handler);
            GameThread.EverySecond += CheckForExpiredMarkers;
            Publisher = new RosSender<InteractiveMarkerFeedback>(config.Topic + "/feedback");
        }

        public override void StopController()
        {
            base.StopController();
            GameThread.EverySecond -= CheckForExpiredMarkers;

            foreach (var markerObject in imarkers.Values)
            {
                DeleteMarkerObject(markerObject);
            }
            
            imarkers.Clear();
            Publisher.Stop();
            
            node.Stop();
            UnityEngine.Object.Destroy(node.gameObject);
        }

        public override void ResetController()
        {
            base.ResetController();
            foreach (var markerObject in imarkers.Values)
            {
                DeleteMarkerObject(markerObject);
            }
            
            imarkers.Clear();
            
            Publisher?.Reset();
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
                imarker = CreateIMarkerObject();
                imarker.Parent = TfListener.ListenersFrame;
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

        static InteractiveMarkerObject CreateIMarkerObject()
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
                Pose: TfListener.RelativePoseToRoot(controlPose).Unity2RosPose(),
                MenuEntryId: 0,
                MousePoint: position.Unity2RosPoint(),
                MousePointValid: true
            );
            Publisher.Publish(msg);
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
                Pose: TfListener.RelativePoseToRoot(controlPose).Unity2RosPose(),
                MenuEntryId: 0,
                MousePoint: Vector3.zero.Unity2RosPoint(),
                MousePointValid: false
            );
            Publisher.Publish(msg);
        }

        void CheckForExpiredMarkers()
        {
            if (DisableExpiration)
            {
                return;
            }

            DateTime now = DateTime.Now;
            string[] deadMarkers = imarkers.
                    Where(entry => entry.Value.ExpirationTime < now).
                    Select(entry => entry.Key).
                    ToArray();
            
            foreach (string key in deadMarkers)
            {
                DestroyInteractiveMarker(key);
            }
        }
    }
}