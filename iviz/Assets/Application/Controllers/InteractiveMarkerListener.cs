#nullable enable

using System;
using System.Collections.Generic;
using System.Text;
using Iviz.App;
using Iviz.Common;
using Iviz.Controllers.TF;
using Iviz.Msgs.IvizCommonMsgs;
using Iviz.Core;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.VisualizationMsgs;
using Iviz.Ros;
using Iviz.Tools;
using UnityEngine;
using Pose = UnityEngine.Pose;
using Vector3 = UnityEngine.Vector3;

namespace Iviz.Controllers
{
    public sealed class InteractiveMarkerListener : ListenerController, IMarkerDialogListener
    {
        const string FeedbackFormatStr = "{0}/feedback";
        const string UpdateFullFormatStr = "{0}/update_full";

        readonly InteractiveMarkerConfiguration config = new();
        readonly Dictionary<string, InteractiveMarkerObject> interactiveMarkers = new();
        readonly FrameNode node;

        uint feedSeq;
        bool interactable;

        public Listener<InteractiveMarkerInit>? FullListener { get; private set; }
        public Sender<InteractiveMarkerFeedback>? Publisher { get; private set; }
        public override TfFrame Frame => TfListener.DefaultFrame;
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
                string markerStr = interactiveMarkers.Count switch
                {
                    0 => "<b>No interactive markers →</b>",
                    1 => "<b>1 interactive marker →</b>",
                    _ => $"<b>{interactiveMarkers.Values.Count.ToString()} interactive markers →</b>"
                };

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

                string errorStr = totalErrors switch
                {
                    0 => "No errors",
                    1 => "1 error",
                    _ => $"{totalErrors} errors"
                };

                string warnStr = totalWarnings switch
                {
                    0 => "No warnings",
                    1 => "1 warning",
                    _ => $"{totalWarnings} warnings"
                };

                return $"{markerStr}\n{errorStr}, {warnStr}";
            }
        }

        public InteractiveMarkerListener(IModuleData moduleData)
        {
            ModuleData = moduleData ?? throw new ArgumentNullException(nameof(moduleData));
            node = FrameNode.Instantiate("[InteractiveMarkerListener]");
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
                root = config.Topic[..lastSlash];
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
            Publisher?.Stop();

            node.DestroySelf();
        }

        public override void ResetController()
        {
            base.ResetController();
            DestroyAllMarkers();
            FullListener?.Reset();
            Publisher?.Reset();
        }

        void HandlerUpdate(InteractiveMarkerUpdate msg)
        {
            if (msg.Type == InteractiveMarkerUpdate.KEEP_ALIVE)
            {
                if (msg.Poses.Length != 0 || msg.Erases.Length != 0 || msg.Markers.Length != 0)
                {
                    RosLogger.Info(
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

        void HandlerUpdateFull(InteractiveMarkerInit msg)
        {
            foreach (var marker in msg.Markers)
            {
                CreateInteractiveMarker(marker);
            }

            FullListener?.Suspend();
        }

        void CreateInteractiveMarker(InteractiveMarker msg)
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

        static void DeleteMarkerObject(InteractiveMarkerObject marker)
        {
            marker.DestroySelf();
        }

        void UpdateInteractiveMarkerPose(InteractiveMarkerPose msg)
        {
            string id = msg.Name;
            if (!interactiveMarkers.TryGetValue(id, out InteractiveMarkerObject im))
            {
                return;
            }

            im.Set(msg.Header.FrameId, msg.Pose);
        }

        void DestroyInteractiveMarker(string id)
        {
            if (!interactiveMarkers.TryGetValue(id, out InteractiveMarkerObject interactiveMarker))
            {
                return;
            }

            interactiveMarker.DestroySelf();
            interactiveMarkers.Remove(id);
        }

        internal void OnInteractiveControlObjectMouseEvent(
            string interactiveMarkerId, string controlId, string? frameId,
            in Pose relativeControlPose, in Vector3? position,
            MouseEventType eventType)
        {
            var msg = new InteractiveMarkerFeedback(
                (feedSeq++, frameId ?? ""),
                ConnectionManager.MyId ?? "",
                interactiveMarkerId,
                controlId,
                (byte) eventType,
                relativeControlPose.Unity2RosPose(),
                0,
                position?.Unity2RosPoint() ?? Point.Zero,
                position != null
            );
            Publisher?.Publish(msg);
            RosLogger.Debug($"{this}: ButtonFeedback Marker:{interactiveMarkerId} Type:{eventType}");
        }

        internal void OnInteractiveControlObjectMoved(
            string interactiveMarkerId, string controlId,
            string? frameId, in Pose relativeControlPose)
        {
            var msg = new InteractiveMarkerFeedback
            (
                (feedSeq++, frameId ?? ""),
                ConnectionManager.MyId ?? "",
                interactiveMarkerId,
                controlId,
                InteractiveMarkerFeedback.POSE_UPDATE,
                relativeControlPose.Unity2RosPose(),
                0,
                Point.Zero,
                false
            );
            Publisher?.Publish(msg);
        }

        internal void OnInteractiveControlObjectMenuSelect(
            string interactiveMarkerId, string? frameId,
            uint menuEntryId, in Pose relativeControlPose)
        {
            var msg = new InteractiveMarkerFeedback
            (
                (feedSeq++, frameId ?? ""),
                ConnectionManager.MyId ?? "",
                interactiveMarkerId,
                "",
                InteractiveMarkerFeedback.MENU_SELECT,
                relativeControlPose.Unity2RosPose(),
                menuEntryId,
                Point.Zero,
                false
            );
            Publisher?.Publish(msg);
            RosLogger.Debug($"{this}: MenuFeedback Marker:{interactiveMarkerId} Entry:{menuEntryId}");
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