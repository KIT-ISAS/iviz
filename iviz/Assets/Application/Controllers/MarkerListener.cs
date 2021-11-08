#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Iviz.App;
using Iviz.Msgs.IvizCommonMsgs;
using Iviz.Core;
using Iviz.Msgs.VisualizationMsgs;
using Iviz.Resources;
using Iviz.Ros;
using Iviz.Roslib;
using Iviz.Tools;
using UnityEngine;
using Logger = Iviz.Core.Logger;

namespace Iviz.Controllers
{
    public sealed class MarkerListener : ListenerController, IMarkerDialogListener
    {
        readonly MarkerConfiguration config = new();

        /// List of markers
        readonly Dictionary<(string Ns, int Id), MarkerObject> markers = new();

        /// Temporary buffer to hold incoming marker messages from the network thread
        readonly Dictionary<(string Ns, int Id), Marker> newMarkerBuffer = new();

        public MarkerListener(IModuleData moduleData)
        {
            ModuleData = moduleData ?? throw new ArgumentNullException(nameof(moduleData));
        }

        public override IModuleData ModuleData { get; }
        public override TfFrame Frame => TfListener.DefaultFrame;

        public MarkerConfiguration Config
        {
            get => config;
            set
            {
                config.Topic = value.Topic;
                config.Type = value.Type;
                RenderAsOcclusionOnly = value.RenderAsOcclusionOnly;
                Tint = value.Tint.ToUnityColor();
                Visible = value.Visible;
                TriangleListFlipWinding = value.TriangleListFlipWinding;
                PreferUdp = value.PreferUdp;

                if (value.VisibleMask.Length != config.VisibleMask.Length)
                {
                    Logger.Error($"{this}: Invalid visibility array of size {value.VisibleMask.Length.ToString()}");
                    return;
                }

                for (int i = 0; i < value.VisibleMask.Length; i++)
                {
                    config.VisibleMask[i] = value.VisibleMask[i];
                }
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
            get => config.Tint.ToUnityColor();
            set
            {
                config.Tint = value.ToRos();

                foreach (var marker in markers.Values)
                {
                    marker.Tint = value;
                }
            }
        }

        public override bool Visible
        {
            get => config.Visible;
            set
            {
                config.Visible = value;

                foreach (var marker in markers.Values)
                {
                    marker.Visible = value && config.VisibleMask[(int) marker.MarkerType];
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

        public bool PreferUdp
        {
            get => config.PreferUdp;
            set
            {
                config.PreferUdp = value;
                if (Listener != null)
                {
                    Listener.TransportHint = value ? RosTransportHint.PreferUdp : RosTransportHint.PreferTcp;
                }
            }
        }

        public override void StartListening()
        {
            var rosTransportHint = PreferUdp ? RosTransportHint.PreferUdp : RosTransportHint.PreferTcp;

            Listener = config.Type switch
            {
                Marker.RosMessageType => new Listener<Marker>(config.Topic, Handler, rosTransportHint),
                MarkerArray.RosMessageType => new Listener<MarkerArray>(config.Topic, Handler, rosTransportHint),
                _ => null
            };

            GameThread.EverySecond += CheckDeadMarkers;
            GameThread.EveryFrame += HandleAsync;
        }

        void CheckDeadMarkers()
        {
            var deadEntries = markers
                .Where(entry => entry.Value.ExpirationTime < GameThread.Now)
                .ToArray();
            foreach (var (key, value) in deadEntries)
            {
                markers.Remove(key);
                DeleteMarkerObject(value);
            }
        }

        public override void StopController()
        {
            base.StopController();
            DestroyAllMarkers();

            GameThread.EverySecond -= CheckDeadMarkers;
            GameThread.EveryFrame -= HandleAsync;
        }

        public string Topic => config.Topic;

        public void GenerateLog(StringBuilder description)
        {
            const int maxToDisplay = 50;

            if (description == null)
            {
                throw new ArgumentNullException(nameof(description));
            }

            int i = 0;
            foreach (var marker in markers.Values)
            {
                marker.GenerateLog(description);
                if (i++ > maxToDisplay)
                {
                    break;
                }
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
                string markerStr = markers.Count switch
                {
                    0 => "<b>No markers →</b>",
                    1 => "<b>1 marker →</b>",
                    _ => $"<b>{markers.Count.ToString()} markers →</b>"
                };

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

        bool Handler(MarkerArray msg)
        {
            lock (newMarkerBuffer)
            {
                foreach (var marker in msg.Markers)
                {
                    newMarkerBuffer[IdFromMessage(marker)] = marker;
                }
            }

            return true;
        }

        bool Handler(Marker marker)
        {
            lock (newMarkerBuffer)
            {
                newMarkerBuffer[IdFromMessage(marker)] = marker;
            }

            return true;
        }

        public IEnumerable<string> GetMaskEntries()
        {
            yield return "---";
            for (int i = 0; i < config.VisibleMask.Length; i++)
            {
                string name = ((MarkerType) i).ToString();
                yield return config.VisibleMask[i] ? name : $"<color=#A0A0A0>({name})</color>";
            }
        }

        public void ToggleMaskEntry(int i)
        {
            bool newVisible = !config.VisibleMask[i];
            config.VisibleMask[i] = newVisible;

            MarkerType markerType = (MarkerType) i;
            foreach (var marker in markers.Values)
            {
                if (marker.MarkerType == markerType)
                {
                    marker.Visible = newVisible;
                }
            }
        }

        void HandleAsync()
        {
            // ReSharper disable once InconsistentlySynchronizedField
            if (newMarkerBuffer.Count == 0)
            {
                return;
            }

            RentAndClear<Marker> newMarkers;
            lock (newMarkerBuffer)
            {
                int i = 0;
                newMarkers = new RentAndClear<Marker>(newMarkerBuffer.Count);
                foreach (var marker in newMarkerBuffer.Values)
                {
                    newMarkers[i++] = marker;
                }

                newMarkerBuffer.Clear();
            }

            using (newMarkers)
                foreach (var marker in newMarkers)
                {
                    HandleAsync(marker!);
                }
        }

        void HandleAsync(Marker msg)
        {
            var id = IdFromMessage(msg);
            switch (msg.Action)
            {
                case Marker.ADD:
                    if (msg.Pose.HasNaN())
                    {
                        Logger.Debug("MarkerListener: NaN in pose!");
                        break;
                    }

                    if (msg.Scale.HasNaN())
                    {
                        Logger.Debug("MarkerListener: NaN in scale!");
                        break;
                    }

                    if (msg.Type >= config.VisibleMask.Length)
                    {
                        Logger.Debug($"MarkerListener: Unknown type {msg.Type.ToString()}");
                        break;
                    }

                    var markerToUpdate = markers.TryGetValue(id, out var existingMarker)
                        ? existingMarker
                        : CreateMarkerObject(id, msg.Type);

                    _ = markerToUpdate.SetAsync(msg).AwaitNoThrow(this);
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

        public static (string Ns, int Id) IdFromMessage(Marker marker)
        {
            return (marker.Ns, marker.Id);
        }

        static void DeleteMarkerObject(MarkerObject markerToDelete)
        {
            markerToDelete.DestroySelf();
        }

        MarkerObject CreateMarkerObject(in (string, int) id, int msgType)
        {
            var marker = new GameObject("MarkerObject").AddComponent<MarkerObject>();
            marker.Parent = TfListener.ListenersFrame;
            marker.OcclusionOnly = RenderAsOcclusionOnly;
            marker.Tint = Tint;
            marker.Visible = Visible && config.VisibleMask[msgType];
            marker.Layer = LayerType.IgnoreRaycast;
            marker.TriangleListFlipWinding = TriangleListFlipWinding;
            markers[id] = marker;
            return marker;
        }
    }
}