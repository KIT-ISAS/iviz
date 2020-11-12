using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Iviz.App;
using Iviz.Core;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.VisualizationMsgs;
using Iviz.Resources;
using Iviz.Ros;
using Iviz.Roslib;
using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;
using Pose = UnityEngine.Pose;
using Vector3 = UnityEngine.Vector3;

namespace Iviz.Controllers
{
    [DataContract]
    public class InteractiveMarkerConfiguration : JsonToString, IConfiguration
    {
        [DataMember] public string Topic { get; set; } = "";
        [DataMember] public bool DescriptionsVisible { get; set; }
        [DataMember] public string Id { get; set; } = Guid.NewGuid().ToString();
        [DataMember] public Resource.ModuleType ModuleType => Resource.ModuleType.InteractiveMarker;
        [DataMember] public bool Visible { get; set; } = true;
    }

    public sealed class InteractiveMarkerListener : ListenerController, IMarkerDialogListener
    {
        readonly InteractiveMarkerConfiguration config = new InteractiveMarkerConfiguration();

        readonly Dictionary<string, InteractiveMarkerObject> imarkers =
            new Dictionary<string, InteractiveMarkerObject>();

        readonly SimpleDisplayNode node;

        uint feedSeq;

        public InteractiveMarkerListener([NotNull] IModuleData moduleData)
        {
            ModuleData = moduleData ?? throw new ArgumentNullException(nameof(moduleData));
            node = SimpleDisplayNode.Instantiate("[InteractiveMarkerListener]");
        }

        public Listener<InteractiveMarkerInit> FullListener { get; private set; }
        public Sender<InteractiveMarkerFeedback> Publisher { get; private set; }
        public override TfFrame Frame => TfListener.MapFrame;
        public override IModuleData ModuleData { get; }
        public string Topic => config.Topic;
        
        public InteractiveMarkerConfiguration Config
        {
            get => config;
            set
            {
                config.Topic = value.Topic;
                DescriptionsVisible = value.DescriptionsVisible;
            }
        }

        public bool DescriptionsVisible
        {
            get => config.DescriptionsVisible;
            set
            {
                if (config.DescriptionsVisible == value)
                {
                    return;
                }
                
                config.DescriptionsVisible = value;
                foreach (var imarkerObject in imarkers.Values)
                {
                    imarkerObject.DescriptionVisible = value;
                }                
            }
        }

        public void GenerateLog(StringBuilder description)
        {
            if (description == null)
            {
                throw new ArgumentNullException(nameof(description));
            }

            foreach (var imarker in imarkers.Values)
            {
                imarker.GenerateLog(description);
            }

            description.AppendLine().AppendLine();
        }

        public string BriefDescription
        {
            get
            {
                switch (imarkers.Count)
                {
                    case 0:
                        return "<b>No interactive markers →</b>";
                    case 1:
                        return "<b>1 interactive marker →</b>";
                    default:
                        return $"<b>{imarkers.Values.Count} interactive markers →</b>";
                }
            }
        }

        public void Reset()
        {
            DestroyAllMarkers();
            FullListener?.Unpause();
        }

        public override void StartListening()
        {
            Listener = new Listener<InteractiveMarkerUpdate>(config.Topic, HandlerUpdate);
            GameThread.EverySecond += CheckForExpiredMarkers;

            int lastSlash = config.Topic.LastIndexOf('/');
            string root = lastSlash == -1 ? config.Topic : config.Topic.Substring(0, lastSlash);

            string feedbackTopic = $"{root}/feedback";
            string fullTopic = $"{root}/update_full";

            Publisher = new Sender<InteractiveMarkerFeedback>(feedbackTopic);
            FullListener = new Listener<InteractiveMarkerInit>(fullTopic, HandlerUpdateFull);
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
            Object.Destroy(node.gameObject);
        }

        public override void ResetController()
        {
            base.ResetController();
            DestroyAllMarkers();
            FullListener?.Reset();
            Publisher?.Reset();
        }

        void HandlerUpdate([NotNull] InteractiveMarkerUpdate msg)
        {
            if (msg.Type == InteractiveMarkerUpdate.KEEP_ALIVE)
            {
                return;
            }

            msg.Markers.ForEach(CreateInteractiveMarker);
            msg.Poses.ForEach(UpdateInteractiveMarkerPose);
            msg.Erases.ForEach(DestroyInteractiveMarker);
        }

        void HandlerUpdateFull([NotNull] InteractiveMarkerInit msg)
        {
            msg.Markers.ForEach(CreateInteractiveMarker);
            FullListener.Pause();
        }

        void CreateInteractiveMarker([NotNull] InteractiveMarker msg)
        {
            string id = msg.Name;
            if (imarkers.TryGetValue(id, out InteractiveMarkerObject existingMarkerObject))
            {
                existingMarkerObject.Set(msg);
                return;
            }

            InteractiveMarkerObject newMarkerObject = CreateInteractiveMarkerObject();
            newMarkerObject.Initialize(this, id);
            newMarkerObject.Parent = TfListener.ListenersFrame;
            newMarkerObject.transform.SetParentLocal(node.transform);
            imarkers[id] = newMarkerObject;
            newMarkerObject.Set(msg);
        }

        static InteractiveMarkerObject CreateInteractiveMarkerObject()
        {
            GameObject gameObject = new GameObject("InteractiveMarkerObject");
            return gameObject.AddComponent<InteractiveMarkerObject>();
        }

        static void DeleteMarkerObject([NotNull] InteractiveMarkerObject imarker)
        {
            imarker.Stop();
            Object.Destroy(imarker.gameObject);
        }

        void UpdateInteractiveMarkerPose([NotNull] InteractiveMarkerPose msg)
        {
            string id = msg.Name;
            if (!imarkers.TryGetValue(id, out InteractiveMarkerObject im))
            {
                return;
            }

            im.Set(msg.Pose);
            //im.transform.SetLocalPose(msg.Pose.Ros2Unity());
            //im.UpdateExpirationTime();
        }

        void DestroyInteractiveMarker([NotNull] string id)
        {
            if (!imarkers.TryGetValue(id, out InteractiveMarkerObject imarker))
            {
                return;
            }

            imarker.Stop();
            Object.Destroy(imarker.gameObject);
            imarkers.Remove(id);
        }

        internal void OnInteractiveControlObjectMouseEvent(
            string imarkerId, string controlId,
            in Pose controlPose, in Vector3? position,
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
                RosUtils.CreateHeader(feedSeq++),
                ConnectionManager.MyId ?? "",
                imarkerId,
                controlId,
                eventType,
                TfListener.RelativePoseToRoot(controlPose).Unity2RosPose(),
                0,
                position?.Unity2RosPoint() ?? Point.Zero, 
                position != null
            );
            Publisher.Publish(msg);
        }

        internal void OnInteractiveControlObjectMoved([NotNull] string imarkerId, [NotNull] string controlId, in Pose controlPose)
        {
            //Debug.Log("--> " + controlPose);
            InteractiveMarkerFeedback msg = new InteractiveMarkerFeedback
            (
                RosUtils.CreateHeader(feedSeq++),
                ConnectionManager.MyId ?? "",
                imarkerId,
                controlId,
                InteractiveMarkerFeedback.POSE_UPDATE,
                TfListener.RelativePoseToRoot(controlPose).Unity2RosPose(),
                0,
                Vector3.zero.Unity2RosPoint(),
                false
            );
            Publisher.Publish(msg);
        }

        void CheckForExpiredMarkers()
        {
            /*
            if (!EnableAutoExpiration)
            {
                return;
            }

            DateTime now = DateTime.Now;
            string[] deadMarkers = imarkers
                .Where(entry => entry.Value.ExpirationTime < now)
                .Select(entry => entry.Key)
                .ToArray();

            foreach (string key in deadMarkers) DestroyInteractiveMarker(key);
            */    
        }

        void DestroyAllMarkers()
        {
            foreach (var markerObject in imarkers.Values)
            {
                DeleteMarkerObject(markerObject);
            }

            imarkers.Clear();
        }
    }
}