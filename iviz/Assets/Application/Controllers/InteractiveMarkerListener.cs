#nullable enable

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using Iviz.App;
using Iviz.Common;
using Iviz.Common.Configurations;
using Iviz.Controllers.Markers;
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

        public Listener<InteractiveMarkerInit>? FullListener { get; }
        public Sender<InteractiveMarkerFeedback>? Publisher { get; }
        public override TfFrame Frame => TfListener.DefaultFrame;

        public InteractiveMarkerConfiguration Config
        {
            get => config;
            private set
            {
                config.Topic = value.Topic;
                DescriptionsVisible = value.DescriptionsVisible;
                Visible = value.Visible;
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
                foreach (var interactiveMarker in interactiveMarkers.Values)
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

            const int maxToDisplay = 50;

            foreach (var interactiveMarker in interactiveMarkers.Values.Take(maxToDisplay))
            {
                interactiveMarker.GenerateLog(description);
                description.AppendLine();
            }

            if (interactiveMarkers.Count > maxToDisplay)
            {
                description.Append("<i>... and ")
                    .Append(interactiveMarkers.Count - maxToDisplay)
                    .Append(" more.</i>")
                    .AppendLine();
            }

            description.AppendLine().AppendLine();
        }

        public string BriefDescription
        {
            get
            {
                string markerStr = interactiveMarkers.Count switch
                {
                    0 => "<b>No interactive markers</b>",
                    1 => "<b>1 interactive marker</b>",
                    _ => $"<b>{interactiveMarkers.Values.Count.ToString()} interactive markers</b>"
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
        
        public override IListener Listener { get; }

        public InteractiveMarkerListener(InteractiveMarkerConfiguration? config, string topic)
        {
            node = new FrameNode("InteractiveMarkerListener");
            Config = config ?? new InteractiveMarkerConfiguration
            {
                Topic = topic,
            };
            
            Listener = new Listener<InteractiveMarkerUpdate>(Config.Topic, HandlerUpdate) { MaxQueueSize = 50 };

            string root;
            if (Config.Topic.HasSuffix("/update"))
            {
                int lastSlash = Config.Topic.LastIndexOf('/');
                root = Config.Topic[..lastSlash];
            }
            else
            {
                root = Config.Topic;
            }

            string feedbackTopic = string.Format(FeedbackFormatStr, root);
            string fullTopic = string.Format(UpdateFullFormatStr, root);

            Publisher = new Sender<InteractiveMarkerFeedback>(feedbackTopic);
            FullListener = new Listener<InteractiveMarkerInit>(fullTopic, HandlerUpdateFull);
        }

        public void Reset()
        {
            DestroyAllMarkers();
            FullListener?.Subscribe();
        }

        public bool TryGetBoundsFromId(string id, [NotNullWhen(true)] out IHasBounds? bounds)
        {
            bounds = GetAllBounds().FirstOrDefault(markerBounds => ((MarkerObject)markerBounds).UniqueNodeName == id);
            return bounds != null;
        }

        public IEnumerable<IHasBounds> GetAllBounds() =>
            interactiveMarkers.Values.SelectMany(interactiveMarker => interactiveMarker.GetAllBounds());

        public override void Dispose()
        {
            base.Dispose();

            foreach (InteractiveMarkerObject markerObject in interactiveMarkers.Values)
            {
                markerObject.Dispose();
            }

            interactiveMarkers.Clear();
            Publisher?.Dispose();

            node.Dispose();
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

            FullListener?.Unsubscribe();
        }

        void CreateInteractiveMarker(InteractiveMarker msg)
        {
            string id = msg.Name;
            if (interactiveMarkers.TryGetValue(id, out InteractiveMarkerObject existingMarkerObject))
            {
                existingMarkerObject.Set(msg);
                return;
            }

            var newMarkerObject = new InteractiveMarkerObject(this, id, TfListener.ListenersFrame)
            {
                Visible = Visible,
                Interactable = Interactable
            };
            interactiveMarkers[id] = newMarkerObject;
            newMarkerObject.Set(msg);
        }

        void UpdateInteractiveMarkerPose(InteractiveMarkerPose msg)
        {
            string id = msg.Name;
            if (!interactiveMarkers.TryGetValue(id, out var interactiveMarker))
            {
                return;
            }

            interactiveMarker.Set(msg.Header.FrameId, msg.Pose);
        }

        void DestroyInteractiveMarker(string id)
        {
            if (!interactiveMarkers.TryGetValue(id, out var interactiveMarker))
            {
                return;
            }

            interactiveMarker.Dispose();
            interactiveMarkers.Remove(id);
        }

        internal void OnControlMouseEvent(
            string interactiveMarkerId, string controlId, string? frameId,
            in Pose relativeControlPose, in Vector3? position,
            MouseEventType eventType)
        {
            var msg = new InteractiveMarkerFeedback(
                TfListener.CreateHeader(feedSeq++, frameId ?? ""),
                ConnectionManager.MyId ?? "",
                interactiveMarkerId,
                controlId,
                (byte)eventType,
                relativeControlPose.Unity2RosPose(),
                0,
                position?.Unity2RosPoint() ?? Point.Zero,
                position != null
            );
            Publisher?.Publish(msg);
            RosLogger.Debug($"{this}: ButtonFeedback Marker:{interactiveMarkerId} Type:{eventType}");
        }

        internal void OnControlMoved(
            string interactiveMarkerId, string controlId,
            string? frameId, in Pose relativeControlPose)
        {
            var msg = new InteractiveMarkerFeedback
            (
                TfListener.CreateHeader(feedSeq++, frameId ?? ""),
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

        internal void OnControlMenuSelect(
            string interactiveMarkerId, string? frameId,
            uint menuEntryId, in Pose relativeControlPose)
        {
            var msg = new InteractiveMarkerFeedback
            (
                TfListener.CreateHeader(feedSeq++, frameId ?? ""),
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
            RosLogger.Debug($"{this}: MenuFeedback Marker:{interactiveMarkerId} Entry:{menuEntryId.ToString()}");
        }

        void DestroyAllMarkers()
        {
            foreach (InteractiveMarkerObject markerObject in interactiveMarkers.Values)
            {
                markerObject.Dispose();
            }

            interactiveMarkers.Clear();
        }

        public override string ToString()
        {
            return $"[InteractiveMarkerListener '{Topic}']";
        }
    }
}