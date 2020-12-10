using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Iviz.App;
using Iviz.Core;
using Iviz.Msgs.VisualizationMsgs;
using Iviz.Resources;
using Iviz.Ros;
using Iviz.Roslib;
using JetBrains.Annotations;
using UnityEngine;
using Logger = Iviz.Core.Logger;
using Object = UnityEngine.Object;

namespace Iviz.Controllers
{
    [DataContract]
    public sealed class MarkerConfiguration : JsonToString, IConfiguration
    {
        [DataMember] public string Topic { get; set; } = "";
        [DataMember] public string Type { get; set; } = "";
        [DataMember] public bool RenderAsOcclusionOnly { get; set; }
        [DataMember] public bool TriangleListFlipWinding { get; set; }
        [DataMember] public SerializableColor Tint { get; set; } = Color.white;
        [DataMember] public string Id { get; set; } = Guid.NewGuid().ToString();
        [DataMember] public Resource.ModuleType ModuleType => Resource.ModuleType.Marker;
        [DataMember] public bool Visible { get; set; } = true;
    }

    public sealed class MarkerListener : ListenerController, IMarkerDialogListener
    {
        readonly MarkerConfiguration config = new MarkerConfiguration();
        readonly Dictionary<(string Ns, int Id), MarkerObject> markers = new Dictionary<(string Ns, int Id), MarkerObject>();

        public MarkerListener([NotNull] IModuleData moduleData)
        {
            ModuleData = moduleData ?? throw new ArgumentNullException(nameof(moduleData));
        }

        public override IModuleData ModuleData { get; }
        public override TfFrame Frame => TfListener.MapFrame;

        public MarkerConfiguration Config
        {
            get => config;
            set
            {
                config.Topic = value.Topic;
                config.Type = value.Type;
                RenderAsOcclusionOnly = value.RenderAsOcclusionOnly;
                Tint = value.Tint;
                Visible = value.Visible;
                TriangleListFlipWinding = value.TriangleListFlipWinding;
            }
        }

        public bool RenderAsOcclusionOnly
        {
            get => config.RenderAsOcclusionOnly;
            set
            {
                config.RenderAsOcclusionOnly = value;

                foreach (var marker in markers.Values)
                {
                    marker.OcclusionOnly = value;
                }
            }
        }

        public Color Tint
        {
            get => config.Tint;
            set
            {
                config.Tint = value;

                foreach (var marker in markers.Values)
                {
                    marker.Tint = value;
                }
            }
        }

        public bool Visible
        {
            get => config.Visible;
            set
            {
                config.Visible = value;

                foreach (var marker in markers.Values)
                {
                    marker.Visible = value;
                }
            }
        }
        
        public bool TriangleListFlipWinding
        {
            get => config.TriangleListFlipWinding;
            set
            {
                config.TriangleListFlipWinding = value;

                foreach (var marker in markers.Values)
                {
                    marker.TriangleListFlipWinding = value;
                }
            }
        }

        public override void StartListening()
        {
            switch (config.Type)
            {
                case Marker.RosMessageType:
                    Listener = new Listener<Marker>(config.Topic, Handler) {MaxQueueSize = 500};
                    break;
                case MarkerArray.RosMessageType:
                    Listener = new Listener<MarkerArray>(config.Topic, Handler) {MaxQueueSize = 500};
                    break;
            }

            GameThread.EverySecond += CheckDeadMarkers;
        }

        void CheckDeadMarkers()
        {
            DateTime now = DateTime.Now;
            var deadEntries = markers
                .Where(entry => entry.Value.ExpirationTime < now)
                .ToArray();
            foreach (var entry in deadEntries)
            {
                markers.Remove(entry.Key);
                Debug.Log("Killing " + entry.Key);
                DeleteMarkerObject(entry.Value);
            }
        }

        public override void StopController()
        {
            base.StopController();
            DestroyAllMarkers();

            GameThread.EverySecond -= CheckDeadMarkers;
        }

        public string Topic => config.Topic;

        public void GenerateLog(StringBuilder description)
        {
            const int maxToDisplay = 50;

            if (description == null)
            {
                throw new ArgumentNullException(nameof(description));
            }

            foreach (var marker in markers.Values.Take(maxToDisplay))
            {
                marker.GenerateLog(description);
            }

            if (markers.Count > maxToDisplay)
            {
                description.Append("<i>... and ").Append(markers.Count - maxToDisplay).Append(" more.</i>")
                    .AppendLine();
            }

            description.AppendLine().AppendLine();
        }

        public string BriefDescription
        {
            get
            {
                string markerStr;
                switch (markers.Count)
                {
                    case 0:
                        markerStr = "<b>No markers →</b>";
                        break;
                    case 1:
                        markerStr = "<b>1 marker →</b>";
                        break;
                    default:
                        markerStr = $"<b>{markers.Count} markers →</b>";
                        break;
                }

                int totalErrors = 0, totalWarnings = 0;
                foreach (var marker in markers.Values)
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
                        warnStr = $"{totalErrors} warnings";
                        break;
                }

                return $"{markerStr}\n{errorStr}, {warnStr}";
            }
        }

        public void Reset()
        {
            DestroyAllMarkers();
        }

        public override void ResetController()
        {
            base.ResetController();
            DestroyAllMarkers();
        }

        void DestroyAllMarkers()
        {
            foreach (var marker in markers.Values)
            {
                DeleteMarkerObject(marker);
            }

            markers.Clear();
        }

        void Handler([NotNull] MarkerArray msg)
        {
            foreach (var marker in msg.Markers)
            {
                Handler(marker);
            }
        }

        void Handler([NotNull] Marker msg)
        {
            var id = IdFromMessage(msg);
            switch (msg.Action)
            {
                case Marker.ADD:
                    if (msg.Pose.HasNaN())
                    {
                        Logger.Debug("MarkerListener: NaN in pose!");
                        return;
                    }

                    if (msg.Scale.HasNaN())
                    {
                        Logger.Debug("MarkerListener: NaN in scale!");
                        return;
                    }

                    if (!markers.TryGetValue(id, out var markerToAdd))
                    {
                        markerToAdd = CreateMarkerObject();
                        //markerToAdd.ModuleData = ModuleData;
                        markerToAdd.Parent = TfListener.ListenersFrame;
                        markerToAdd.OcclusionOnly = RenderAsOcclusionOnly;
                        markerToAdd.Tint = Tint;
                        markerToAdd.Visible = Visible;
                        markerToAdd.Layer = 2;
                        markers[id] = markerToAdd;
                    }

                    markerToAdd.Set(msg);
                    break;
                case Marker.DELETE:
                    if (markers.TryGetValue(id, out var markerToDelete))
                    {
                        DeleteMarkerObject(markerToDelete);
                        markers.Remove(id);
                    }

                    break;
            }
        }

        public static (string Ns, int Id) IdFromMessage([NotNull] Marker marker)
        {
            return (marker.Ns, marker.Id);
        }

        static void DeleteMarkerObject([NotNull] MarkerObject markerToDelete)
        {
            markerToDelete.Stop();
            Object.Destroy(markerToDelete.gameObject);
        }

        static MarkerObject CreateMarkerObject()
        {
            var gameObject = new GameObject("MarkerObject");
            return gameObject.AddComponent<MarkerObject>();
        }
    }
}