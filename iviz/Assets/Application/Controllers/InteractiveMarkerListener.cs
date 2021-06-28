using System;
using System.Collections.Generic;
using System.Text;
using Iviz.App;
using Iviz.Msgs.IvizCommonMsgs;
using Iviz.Core;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.VisualizationMsgs;
using Iviz.Ros;
using Iviz.Roslib;
using Iviz.Roslib.Utils;
using JetBrains.Annotations;
using UnityEngine;
using Logger = Iviz.Core.Logger;
using Object = UnityEngine.Object;
using Pose = UnityEngine.Pose;
using Vector3 = UnityEngine.Vector3;

namespace Iviz.Controllers
{
    public sealed class InteractiveMarkerListener : ListenerController, IMarkerDialogListener
    {
        const string FeedbackFormatStr = "{0}/feedback";
        const string UpdateFullFormatStr = "{0}/update_full";

        readonly InteractiveMarkerConfiguration config = new InteractiveMarkerConfiguration();

        readonly Dictionary<string, InteractiveMarkerObject> interactiveMarkers =
            new Dictionary<string, InteractiveMarkerObject>();

        readonly FrameNode node;

        uint feedSeq;
        bool interactable;

        public InteractiveMarkerListener([NotNull] IModuleData moduleData)
        {
            ModuleData = moduleData ?? throw new ArgumentNullException(nameof(moduleData));
            node = FrameNode.Instantiate("[InteractiveMarkerListener]");
        }

        public Listener<InteractiveMarkerInit> FullListener { get; private set; }
        public Sender<InteractiveMarkerFeedback> Publisher { get; private set; }
        [NotNull] public override TfFrame Frame => TfListener.DefaultFrame;
        public override IModuleData ModuleData { get; }

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
                foreach (InteractiveMarkerObject interactiveMarker in interactiveMarkers.Values)
                {
                    interactiveMarker.DescriptionVisible = value;
                }
            }
        }

        public override bool Visible
        {
            get => config.Visible;
            set
            {
                config.Visible = value;

                foreach (InteractiveMarkerObject marker in interactiveMarkers.Values)
                {
                    marker.Visible = value;
                }
            }
        }

        public bool Interactable
        {
            get => interactable;
            set
            {
                interactable = value;
                foreach (var interactiveMarker in interactiveMarkers.Values)
                {
                    interactiveMarker.Interactable = value;
                }
            }
        }

        public string Topic => config.Topic;

        public void GenerateLog(StringBuilder description)
        {
            if (description == null)
            {
                throw new ArgumentNullException(nameof(description));
            }

            foreach (InteractiveMarkerObject interactiveMarker in interactiveMarkers.Values)
            {
                interactiveMarker.GenerateLog(description);
                description.AppendLine();
            }

            description.AppendLine().AppendLine();
        }

        public string BriefDescription
        {
            get
            {
                string markerStr;
                switch (interactiveMarkers.Count)
                {
                    case 0:
                        markerStr = "<b>No interactive markers →</b>";
                        break;
                    case 1:
                        markerStr = "<b>1 interactive marker →</b>";
                        break;
                    default:
                        markerStr = $"<b>{interactiveMarkers.Values.Count} interactive markers →</b>";
                        break;
                }

                int totalErrors = 0, totalWarnings = 0;
                foreach (InteractiveMarkerObject marker in interactiveMarkers.Values)
                {
                    marker.GetErrorCount(out int numErrors, out int numWarnings);
                    totalErrors += numErrors;
                    totalWarnings += numWarnings;
                }

                if (totalErrors == 0 && totalWarnings == 0)
                {
                    return $"{markerStr}\nNo errors";
                }

                string errorStr, warnStr;
                switch (totalErrors)
                {
                    case 0:
                        errorStr = "No errors";
                        break;
                    case 1:
                        errorStr = "1 error";
                        break;
                    default:
                        errorStr = $"{totalErrors} errors";
                        break;
                }

                switch (totalWarnings)
                {
                    case 0:
                        warnStr = "No warnings";
                        break;
                    case 1:
                        warnStr = "1 warning";
                        break;
                    default:
                        warnStr = $"{totalWarnings} warnings";
                        break;
                }

                return $"{markerStr}\n{errorStr}, {warnStr}";
            }
        }

        public void Reset()
        {
            DestroyAllMarkers();
            FullListener?.Unsuspend();
        }

        public override void StartListening()
        {
            Listener = new Listener<InteractiveMarkerUpdate>(config.Topic, HandlerUpdate) {MaxQueueSize = 50};

            string root;
            if (config.Topic.HasSuffix("/update"))
            {
                int lastSlash = config.Topic.LastIndexOf('/');
                root = config.Topic.Substring(0, lastSlash);
            }
            else
            {
                root = config.Topic;
            }

            string feedbackTopic = string.Format(FeedbackFormatStr, root);
            string fullTopic = string.Format(UpdateFullFormatStr, root);

            Publisher = new Sender<InteractiveMarkerFeedback>(feedbackTopic);
            FullListener = new Listener<InteractiveMarkerInit>(fullTopic, HandlerUpdateFull);
        }

        public override void StopController()
        {
            base.StopController();

            foreach (InteractiveMarkerObject markerObject in interactiveMarkers.Values)
            {
                DeleteMarkerObject(markerObject);
            }

            interactiveMarkers.Clear();
            Publisher.Stop();

            node.DestroySelf();
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
                if (msg.Poses.Length != 0 || msg.Erases.Length != 0 || msg.Markers.Length != 0)
                {
                    Logger.Info(
                        $"{this}: A keep-alive message with non-empty payload was sent. The payload will be ignored.");
                }

                return;
            }

            foreach (InteractiveMarker marker in msg.Markers)
            {
                CreateInteractiveMarker(marker);
            }

            foreach (InteractiveMarkerPose pose in msg.Poses)
            {
                UpdateInteractiveMarkerPose(pose);
            }

            foreach (string name in msg.Erases)
            {
                DestroyInteractiveMarker(name);
            }
        }

        void HandlerUpdateFull([NotNull] InteractiveMarkerInit msg)
        {
            foreach (var marker in msg.Markers)
            {
                CreateInteractiveMarker(marker);
            }

            FullListener.Suspend();
        }

        void CreateInteractiveMarker([NotNull] InteractiveMarker msg)
        {
            string id = msg.Name;
            if (interactiveMarkers.TryGetValue(id, out InteractiveMarkerObject existingMarkerObject))
            {
                existingMarkerObject.Set(msg);
                return;
            }

            InteractiveMarkerObject newMarkerObject = CreateInteractiveMarkerObject();
            newMarkerObject.Initialize(this, id);
            newMarkerObject.Parent = TfListener.ListenersFrame;
            newMarkerObject.Visible = Visible;
            newMarkerObject.Interactable = Interactable;
            newMarkerObject.transform.SetParentLocal(node.transform);
            interactiveMarkers[id] = newMarkerObject;
            newMarkerObject.Set(msg);
        }

        static InteractiveMarkerObject CreateInteractiveMarkerObject()
        {
            GameObject gameObject = new GameObject("InteractiveMarkerObject");
            return gameObject.AddComponent<InteractiveMarkerObject>();
        }

        static void DeleteMarkerObject([NotNull] InteractiveMarkerObject marker)
        {
            marker.DestroySelf();
        }

        void UpdateInteractiveMarkerPose([NotNull] InteractiveMarkerPose msg)
        {
            string id = msg.Name;
            if (!interactiveMarkers.TryGetValue(id, out InteractiveMarkerObject im))
            {
                return;
            }

            im.Set(msg.Header.FrameId, msg.Pose);
        }

        void DestroyInteractiveMarker([NotNull] string id)
        {
            if (!interactiveMarkers.TryGetValue(id, out InteractiveMarkerObject interactiveMarker))
            {
                return;
            }

            interactiveMarker.DestroySelf();
            interactiveMarkers.Remove(id);
        }

        internal void OnInteractiveControlObjectMouseEvent(
            [NotNull] string interactiveMarkerId, [NotNull] string controlId, [CanBeNull] string frameId,
            in Pose relativeControlPose, in Vector3? position,
            MouseEventType eventType)
        {
            InteractiveMarkerFeedback msg = new InteractiveMarkerFeedback
            (
                (feedSeq++, frameId),
                ConnectionManager.MyId ?? "",
                interactiveMarkerId,
                controlId,
                (byte) eventType,
                relativeControlPose.Unity2RosPose(),
                0,
                position?.Unity2RosPoint() ?? Point.Zero,
                position != null
            );
            Publisher.Publish(msg);
            Logger.Debug($"{this}: ButtonFeedback Marker:{interactiveMarkerId} Type:{eventType}");
        }

        internal void OnInteractiveControlObjectMoved(
            [NotNull] string interactiveMarkerId, [NotNull] string controlId,
            [CanBeNull] string frameId, in Pose relativeControlPose)
        {
            InteractiveMarkerFeedback msg = new InteractiveMarkerFeedback
            (
                (feedSeq++, frameId),
                ConnectionManager.MyId ?? "",
                interactiveMarkerId,
                controlId,
                InteractiveMarkerFeedback.POSE_UPDATE,
                relativeControlPose.Unity2RosPose(),
                0,
                Point.Zero,
                false
            );
            Publisher.Publish(msg);
            //Logger.Debug($"{this}: PoseFeedback Marker:{interactiveMarkerId} UnityPose:{relativeControlPose}");
        }

        internal void OnInteractiveControlObjectMenuSelect(
            [NotNull] string interactiveMarkerId, [CanBeNull] string frameId,
            uint menuEntryId, in Pose relativeControlPose)
        {
            InteractiveMarkerFeedback msg = new InteractiveMarkerFeedback
            (
                (feedSeq++, frameId),
                ConnectionManager.MyId ?? "",
                interactiveMarkerId,
                "",
                InteractiveMarkerFeedback.MENU_SELECT,
                relativeControlPose.Unity2RosPose(),
                menuEntryId,
                Point.Zero,
                false
            );
            Publisher.Publish(msg);
            Logger.Debug($"{this}: MenuFeedback Marker:{interactiveMarkerId} Entry:{menuEntryId}");
        }

        void DestroyAllMarkers()
        {
            foreach (InteractiveMarkerObject markerObject in interactiveMarkers.Values)
            {
                DeleteMarkerObject(markerObject);
            }

            interactiveMarkers.Clear();
        }

        public override string ToString()
        {
            return $"[InteractiveMarkerListener '{Topic}']";
        }
    }
}