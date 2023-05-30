#nullable enable

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using Iviz.Common;
using Iviz.Controllers.Markers;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Core.Configurations;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.StdMsgs;
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
        public override TfFrame Frame => TfModule.DefaultFrame;

        public InteractiveMarkerConfiguration Config
        {
            get => config;
            private set
            {
                config.Topic = value.Topic;
                DescriptionsVisible = value.DescriptionsVisible;
                Visible = value.Visible;
                Alpha = value.Alpha;
                Tint = value.Tint.ToUnity();
                Smoothness = value.Smoothness;
                Metallic = value.Metallic;
                TriangleListFlipWinding = value.TriangleListFlipWinding;
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
                foreach (var marker in interactiveMarkers.Values)
                {
                    marker.DescriptionVisible = value;
                }
            }
        }

        public override bool Visible
        {
            get => config.Visible;
            set
            {
                config.Visible = value;

                foreach (var marker in interactiveMarkers.Values)
                {
                    marker.Visible = value;
                }
            }
        }

        public Color Tint
        {
            get => config.Tint.ToUnity();
            set
            {
                config.Tint = value.ToRos();

                foreach (var marker in GetAllMarkers())
                {
                    marker.Tint = value.WithAlpha(Alpha);
                }
            }
        }

        public float Alpha
        {
            get => config.Alpha;
            set
            {
                config.Alpha = value;

                foreach (var marker in GetAllMarkers())
                {
                    marker.Tint = Tint.WithAlpha(value);
                }
            }
        }

        public float Metallic
        {
            get => config.Metallic;
            set
            {
                config.Metallic = value;

                foreach (var marker in GetAllMarkers())
                {
                    marker.Metallic = value;
                }
            }
        }

        public float Smoothness
        {
            get => config.Smoothness;
            set
            {
                config.Smoothness = value;

                foreach (var marker in GetAllMarkers())
                {
                    marker.Smoothness = value;
                }
            }
        }

        public bool TriangleListFlipWinding
        {
            get => config.TriangleListFlipWinding;
            set
            {
                config.TriangleListFlipWinding = value;

                foreach (var marker in GetAllMarkers())
                {
                    marker.TriangleListFlipWinding = value;
                }
            }
        }

        public bool Interactable
        {
            get => interactable;
            set
            {
                interactable = value;
                foreach (var marker in interactiveMarkers.Values)
                {
                    marker.Interactable = value;
                }
            }
        }

        public string Title => "Interactive Markers";

        public string Topic => config.Topic;

        public int NumEntriesForLog => interactiveMarkers.Count;

        public void GenerateLog(StringBuilder description, int minIndex, int numEntries)
        {
            ThrowHelper.ThrowIfNull(description, nameof(description));

            foreach (var marker in interactiveMarkers.Values.Skip(minIndex).Take(numEntries))
            {
                marker.GenerateLog(description);
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
                    0 => "<b>No interactive markers</b>",
                    1 => "<b>1 interactive marker</b>",
                    _ => $"<b>{interactiveMarkers.Count.ToString()} interactive markers</b>"
                };

                int totalErrors = 0, totalWarnings = 0;
                foreach (var marker in interactiveMarkers.Values)
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

        public override Listener Listener { get; }

        public InteractiveMarkerListener(InteractiveMarkerConfiguration? config, string topic)
        {
            node = new FrameNode(nameof(InteractiveMarkerListener));
            Config = config ?? new InteractiveMarkerConfiguration
            {
                Topic = topic,
                Id = topic
            };

            Listener = new Listener<InteractiveMarkerUpdate>(Config.Topic, HandleUpdate, 50);

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
            FullListener = new Listener<InteractiveMarkerInit>(fullTopic, HandleUpdateFull);
        }

        public bool TryGetBoundsFromId(string id, [NotNullWhen(true)] out IHasBounds? bounds)
        {
            if (!GetAllMarkers().TryGetFirst(markerObject => markerObject.UniqueNodeName == id, out var marker))
            {
                bounds = null;
                return false;
            }

            bounds = marker;
            return true;
        }

        IEnumerable<MarkerObject> GetAllMarkers() =>
            interactiveMarkers.Values.SelectMany(interactiveMarker => interactiveMarker.GetAllMarkers());

        public override void Dispose()
        {
            base.Dispose();

            foreach (var marker in interactiveMarkers.Values)
            {
                marker.Dispose();
            }

            interactiveMarkers.Clear();
            Publisher?.Dispose();

            node.Dispose();
        }

        public override void ResetController()
        {
            DestroyAllMarkers();
            FullListener?.Reset();
            Publisher?.Reset();
        }

        void HandleUpdate(InteractiveMarkerUpdate msg)
        {
            if (msg.Type == InteractiveMarkerUpdate.KEEP_ALIVE)
            {
                if (msg.Poses.Length != 0 || msg.Erases.Length != 0 || msg.Markers.Length != 0)
                {
                    RosLogger.Info($"{this}: A keep-alive message with non-empty payload was sent. " +
                                   $"The payload will be ignored.");
                }

                return;
            }

            foreach (var marker in msg.Markers)
            {
                CreateInteractiveMarker(marker);
            }

            foreach (var pose in msg.Poses)
            {
                UpdateInteractiveMarkerPose(pose);
            }

            foreach (string name in msg.Erases)
            {
                DestroyInteractiveMarker(name);
            }
        }

        void HandleUpdateFull(InteractiveMarkerInit msg)
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
            
            InteractiveMarkerObject markerObject;
            if (interactiveMarkers.TryGetValue(id, out var existingMarkerObject))
            {
                markerObject = existingMarkerObject;
                markerObject.Set(msg);
            }
            else
            {
                markerObject = new InteractiveMarkerObject(this, id, TfModule.ListenersFrame)
                {
                    Visible = Visible,
                    Interactable = Interactable
                };
                interactiveMarkers[id] = markerObject;
                markerObject.Set(msg);
            }

            var allMarkers = markerObject.GetAllMarkers();
            foreach (var marker in allMarkers)
            {
                marker.Metallic = Metallic;
                marker.Smoothness = Smoothness;
                marker.Tint = Tint.WithAlpha(Alpha);
                marker.TriangleListFlipWinding = TriangleListFlipWinding;
            }
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
            var msg = new InteractiveMarkerFeedback
            {
                Header = TfListener.CreateHeader(feedSeq++, frameId ?? ""),
                ClientId = RosManager.MyId ?? "",
                MarkerName = interactiveMarkerId,
                ControlName = controlId,
                EventType = (byte)eventType,
                Pose = relativeControlPose.Unity2RosPose(),
                MousePoint = position?.Unity2RosPoint() ?? Point.Zero,
                MousePointValid = position != null
            };
            Publisher?.Publish(msg);
        }

        internal void OnControlMoved(
            string interactiveMarkerId, string controlId,
            string? frameId, in Pose relativeControlPose)
        {
            var msg = new InteractiveMarkerFeedback
            {
                Header = TfListener.CreateHeader(feedSeq++, frameId ?? ""),
                ClientId = RosManager.MyId ?? "",
                MarkerName = interactiveMarkerId,
                ControlName = controlId,
                EventType = InteractiveMarkerFeedback.POSE_UPDATE,
                Pose = relativeControlPose.Unity2RosPose(),
            };
            Publisher?.Publish(msg);
        }

        internal void OnControlMenuSelect(
            string interactiveMarkerId, string? frameId,
            uint menuEntryId, in Pose relativeControlPose)
        {
            var msg = new InteractiveMarkerFeedback
            {
                Header = TfListener.CreateHeader(feedSeq++, frameId ?? ""),
                ClientId = RosManager.MyId ?? "",
                MarkerName = interactiveMarkerId,
                EventType = InteractiveMarkerFeedback.MENU_SELECT,
                Pose = relativeControlPose.Unity2RosPose(),
                MenuEntryId = menuEntryId
            };
            Publisher?.Publish(msg);
        }

        void DestroyAllMarkers()
        {
            foreach (var marker in interactiveMarkers.Values)
            {
                marker.Dispose();
            }

            interactiveMarkers.Clear();
        }

        public override string ToString()
        {
            return $"[{nameof(InteractiveMarkerListener)} '{Topic}']";
        }
    }
}