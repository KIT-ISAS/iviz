#nullable enable

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using Iviz.Common;
using Iviz.Common.Configurations;
using Iviz.Controllers.Markers;
using Iviz.Controllers.TF;
using Iviz.Msgs.IvizCommonMsgs;
using Iviz.Core;
using Iviz.Msgs.VisualizationMsgs;
using Iviz.Ros;
using Iviz.Roslib;
using Iviz.Tools;
using UnityEngine;

namespace Iviz.Controllers
{
    public sealed class MarkerListener : ListenerController, IMarkerDialogListener
    {
        const int MaxMarkersTotal = 10000;
        const int MaxMarkersPerFrame = 100;
        const int MaxMarkersInLog = 50;

        readonly MarkerConfiguration config = new();

        /// List of markers
        readonly Dictionary<(string Ns, int Id), MarkerObject> markers = new();

        /// Temporary buffer to hold incoming marker messages from the network thread
        readonly Dictionary<(string Ns, int Id), Marker> newMarkerBuffer = new();

        public override IModuleData ModuleData { get; }
        public override TfFrame Frame => TfListener.DefaultFrame;

        public MarkerConfiguration Config
        {
            get => config;
            private set
            {
                config.Topic = value.Topic;
                config.Type = value.Type;
                RenderAsOcclusionOnly = value.RenderAsOcclusionOnly;
                Tint = value.Tint.ToUnityColor();
                Visible = value.Visible;
                TriangleListFlipWinding = value.TriangleListFlipWinding;
                PreferUdp = value.PreferUdp;
                ShowDescriptions = value.ShowDescriptions;

                if (value.VisibleMask.Length != config.VisibleMask.Length)
                {
                    RosLogger.Error($"{this}: Invalid visibility array of size {value.VisibleMask.Length.ToString()}");
                    return;
                }

                foreach (int i in ..value.VisibleMask.Length)
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
        
        public bool ShowDescriptions
        {
            get => config.ShowDescriptions;
            set
            {
                config.ShowDescriptions = value;
                foreach (var marker in markers.Values)
                {
                    marker.ShowDescription = value;
                }                
            }
        }        
        
        public string BriefDescription
        {
            get
            {
                string markerStr = markers.Count switch
                {
                    0 => "<b>No markers</b>",
                    1 => "<b>1 marker</b>",
                    _ => $"<b>{markers.Count.ToString()} markers</b>"
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
        
        public string Topic => config.Topic;        
        
        public override IListener Listener { get; }
        
        public MarkerListener(IModuleData moduleData, MarkerConfiguration? config, string topic, string type)
        {        
            ModuleData = moduleData ?? throw new ArgumentNullException(nameof(moduleData));
            
            Config = config ?? new MarkerConfiguration
            {
                Topic = topic,
                Type = type
            };            

            var rosTransportHint = PreferUdp ? RosTransportHint.PreferUdp : RosTransportHint.PreferTcp;

            Listener = Config.Type switch
            {
                Marker.RosMessageType => new Listener<Marker>(Config.Topic, Handler, rosTransportHint),
                MarkerArray.RosMessageType => new Listener<MarkerArray>(Config.Topic, Handler, rosTransportHint),
                _ => throw new InvalidOperationException("Invalid message type")
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
                value.Stop();
            }
        }

        public override void Dispose()
        {
            base.Dispose();
            DestroyAllMarkers();

            GameThread.EverySecond -= CheckDeadMarkers;
            GameThread.EveryFrame -= HandleAsync;
        }

        public void GenerateLog(StringBuilder description)
        {

            if (description == null)
            {
                throw new ArgumentNullException(nameof(description));
            }

            foreach (var marker in markers.Values.Take(MaxMarkersInLog))
            {
                marker.GenerateLog(description);
            }

            if (markers.Count > MaxMarkersInLog)
            {
                description.Append("<i>... and ")
                    .Append(markers.Count - MaxMarkersInLog)
                    .Append(" more.</i>")
                    .AppendLine();
            }

            description.AppendLine().AppendLine();
        }

        public void Reset()
        {
            DestroyAllMarkers();
        }

        public bool TryGetBoundsFromId(string id, [NotNullWhen(true)] out IHasBounds? bounds)
        {
            bounds = markers.Values.FirstOrDefault(marker => marker.UniqueNodeName == id);
            return bounds != null;
        }
        
        public Dictionary<(string, int), MarkerObject>.ValueCollection GetAllBounds() => markers.Values;
        
        IEnumerable<IHasBounds> IMarkerDialogListener.GetAllBounds() => GetAllBounds();

        public override void ResetController()
        {
            base.ResetController();
            DestroyAllMarkers();
        }

        void DestroyAllMarkers()
        {
            foreach (var marker in markers.Values)
            {
                marker.Stop();
            }

            markers.Clear();
        }

        bool Handler(MarkerArray msg)
        {
            lock (newMarkerBuffer)
            {
                if (newMarkerBuffer.Count > MaxMarkersTotal)
                {
                    return false;
                }
                
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
                if (newMarkerBuffer.Count > MaxMarkersTotal)
                {
                    return false;
                }
                
                newMarkerBuffer[IdFromMessage(marker)] = marker;
            }

            return true;
        }

        public IEnumerable<string> GetMaskEntries()
        {
            yield return "---";
            foreach (int i in ..config.VisibleMask.Length)
            {
                string name = ((MarkerType) i).ToString();
                yield return config.VisibleMask[i] ? name : $"<color=#A0A0A0>({name})</color>";
            }
        }

        public void ToggleMaskEntry(int i)
        {
            bool newVisible = !config.VisibleMask[i];
            config.VisibleMask[i] = newVisible;

            var markerType = (MarkerType) i;
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
                if (newMarkerBuffer.Count <= MaxMarkersPerFrame)
                {
                    newMarkers = new RentAndClear<Marker>(newMarkerBuffer.Count);
                    foreach (var (marker, i) in newMarkerBuffer.Values.WithIndex())
                    {
                        newMarkers[i] = marker;
                    }

                    newMarkerBuffer.Clear();
                }
                else
                {
                    using var ids = new RentAndClear<(string, int)>(MaxMarkersPerFrame);
                    newMarkers = new RentAndClear<Marker>(MaxMarkersPerFrame);

                    foreach (var ((key, value), i) in newMarkerBuffer.Take(MaxMarkersPerFrame).WithIndex())
                    {
                        ids[i] = key;
                        newMarkers[i] = value;
                    }
                    
                    foreach (var id in ids)
                    {
                        newMarkerBuffer.Remove(id);
                    }
                }
            }

            using (newMarkers)
            {
                foreach (var marker in newMarkers)
                {
                    HandleAsync(marker!);
                }
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
                        RosLogger.Debug($"{this}: NaN in pose!");
                        break;
                    }

                    if (msg.Scale.HasNaN())
                    {
                        RosLogger.Debug($"{this}: NaN in scale!");
                        break;
                    }

                    if (msg.Type >= config.VisibleMask.Length)
                    {
                        RosLogger.Debug($"{this}: Unknown type {msg.Type.ToString()}");
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
                        markerToDelete.Stop();
                        markers.Remove(id);
                    }

                    break;
                case Marker.DELETEALL:
                    DestroyAllMarkers();
                    break;
                default:
                    RosLogger.Debug($"{this}: Unknown action {msg.Action.ToString()}");
                    break;
            }
        }

        public static (string Ns, int Id) IdFromMessage(Marker marker)
        {
            return (marker.Ns, marker.Id);
        }

        MarkerObject CreateMarkerObject(in (string, int) id, int msgType)
        {
            var marker = new MarkerObject(TfListener.ListenersFrame, id)
            {
                OcclusionOnly = RenderAsOcclusionOnly,
                Tint = Tint,
                Visible = Visible && config.VisibleMask[msgType],
                TriangleListFlipWinding = TriangleListFlipWinding
            };
            markers[id] = marker;
            return marker;
        }
    }
}