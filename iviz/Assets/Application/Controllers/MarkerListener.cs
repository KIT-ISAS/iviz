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

        readonly MarkerConfiguration config = new();

        /// List of markers
        readonly Dictionary<(string Ns, int Id), MarkerObject> markers = new();

        /// Temporary buffer to hold incoming marker messages from the network thread
        readonly Dictionary<(string Ns, int Id), Marker> newMarkerBuffer = new();

        public override TfFrame Frame => TfModule.DefaultFrame;

        public MarkerConfiguration Config
        {
            get => config;
            private set
            {
                config.Topic = value.Topic;
                config.Type = value.Type;
                config.Id = value.Id;
                RenderAsOcclusionOnly = value.RenderAsOcclusionOnly;
                Tint = value.Tint.ToUnity();
                Visible = value.Visible;
                TriangleListFlipWinding = value.TriangleListFlipWinding;
                PreferUdp = value.PreferUdp;
                ShowDescriptions = value.ShowDescriptions;

                if (value.VisibleMask.Length != config.VisibleMask.Length)
                {
                    RosLogger.Error($"{this}: Invalid visibility array of size {value.VisibleMask.Length.ToString()}");
                    return;
                }

                VisibleMask = value.VisibleMask;
            }
        }

        public bool RenderAsOcclusionOnly
        {
            get => config.RenderAsOcclusionOnly;
            set
            {
                config.RenderAsOcclusionOnly = value;

                foreach (var (_, marker) in markers)
                {
                    marker.OcclusionOnly = value;
                }
            }
        }

        public Color Tint
        {
            get => config.Tint.ToUnity();
            set
            {
                config.Tint = value.ToRos();

                foreach (var (_, marker) in markers)
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

                foreach (var (_, marker) in markers)
                {
                    marker.Visible = value && config.VisibleMask[(int)marker.MarkerType];
                }
            }
        }

        public bool TriangleListFlipWinding
        {
            get => config.TriangleListFlipWinding;
            set
            {
                config.TriangleListFlipWinding = value;

                foreach (var (_, marker) in markers)
                {
                    marker.TriangleListFlipWinding = value;
                }
            }
        }

        public float Smoothness
        {
            get => config.Smoothness;
            set
            {
                config.Smoothness = value;

                foreach (var (_, marker) in markers)
                {
                    marker.Smoothness = value;
                }
            }
        }

        public float Metallic
        {
            get => config.Metallic;
            set
            {
                config.Metallic = value;

                foreach (var marker in markers.Values)
                {
                    marker.Metallic = value;
                }
            }
        }

        bool PreferUdp
        {
            get => config.PreferUdp;
            set => config.PreferUdp = value;
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
                foreach (var (_, marker) in markers)
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
        
        public IReadOnlyList<bool> VisibleMask
        {
            get => config.VisibleMask;
            set
            {
                if (value.Count != config.VisibleMask.Length)
                {
                    RosLogger.Error($"{this}: Invalid visibility array of size {value.Count.ToString()}");
                    return;
                }

                foreach (int i in ..value.Count)
                {
                    config.VisibleMask[i] = value[i];
                }

                if (!Visible)
                {
                    return;
                }

                foreach (var (_, marker) in markers)
                {
                    int type = (int)marker.MarkerType;
                    marker.Visible = config.VisibleMask[type];
                }
            }
        }

        public int NumEntriesForLog => markers.Count;
        
        public MarkerListener(MarkerConfiguration? config, string topic, string type)
        {
            Config = config ?? new MarkerConfiguration
            {
                Topic = topic,
                Type = type,
                Id = topic
            };

            var rosTransportHint = PreferUdp ? RosTransportHint.PreferUdp : RosTransportHint.PreferTcp;

            Listener = Config.Type switch
            {
                Marker.MessageType => new Listener<Marker>(Config.Topic, Handle, rosTransportHint),
                MarkerArray.MessageType => new Listener<MarkerArray>(Config.Topic, Handle, rosTransportHint),
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
                value.Dispose();
            }
        }

        public override void Dispose()
        {
            base.Dispose();
            DisposeAllMarkers();

            GameThread.EverySecond -= CheckDeadMarkers;
            GameThread.EveryFrame -= HandleAsync;
        }

        public void GenerateLog(StringBuilder description, int minIndex, int numEntries)
        {
            ThrowHelper.ThrowIfNull(description, nameof(description));

            foreach (var (_, marker) in markers.Skip(minIndex).Take(numEntries))
            {
                marker.GenerateLog(description);
            }

            description.AppendLine().AppendLine();
        }

        public bool TryGetBoundsFromId(string id, [NotNullWhen(true)] out IHasBounds? bounds)
        {
            return markers.Values.TryGetFirst(hasBounds => ((MarkerObject)hasBounds).UniqueNodeName == id, out bounds);
        }

        public Dictionary<(string, int), MarkerObject>.ValueCollection GetAllBounds() => markers.Values;

        public override void ResetController()
        {
            base.ResetController();
            DisposeAllMarkers();
        }

        void DisposeAllMarkers()
        {
            foreach (var (_, marker) in markers)
            {
                marker.Dispose();
            }

            markers.Clear();
        }

        bool Handle(MarkerArray msg)
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

        bool Handle(Marker marker)
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

        public void ToggleVisibleMask(int i)
        {
            bool value = !config.VisibleMask[i];
            var markerType = (MarkerType)i;
            var markersToUpdate = markers.Values.Where(marker => marker.MarkerType == markerType);
            foreach (var marker in markersToUpdate)
            {
                marker.Visible = value;
            }

            config.VisibleMask[i] = value;
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
                    if (msg.Pose.IsInvalid())
                    {
                        RosLogger.Info($"{this}:Invalid pose! Rejecting marker update {id.ToString()}.");
                        break;
                    }

                    if (msg.Scale.IsInvalid())
                    {
                        RosLogger.Info($"{this}: Invalid scale! Rejecting marker update {id.ToString()}.");
                        break;
                    }

                    if (msg.Type < 0  || msg.Type >= config.VisibleMask.Length)
                    {
                        RosLogger.Info(
                            $"{this}: Unknown type {msg.Type.ToString()}.  Rejecting marker update {id.ToString()}.");
                        break;
                    }

                    var markerToUpdate = markers.TryGetValue(id, out var existingMarker)
                        ? existingMarker
                        : CreateMarkerObject(id, msg.Type);

                    markerToUpdate.SetAsync(msg);
                    break;
                case Marker.DELETE:
                    if (markers.TryGetValue(id, out var markerToDelete))
                    {
                        markerToDelete.Dispose();
                        markers.Remove(id);
                    }

                    break;
                case Marker.DELETEALL:
                    DisposeAllMarkers();
                    break;
                default:
                    RosLogger.Info($"{this}: Unknown action {msg.Action.ToString()}");
                    break;
            }
        }

        public static (string Ns, int Id) IdFromMessage(Marker marker)
        {
            return (marker.Ns, marker.Id);
        }

        MarkerObject CreateMarkerObject(in (string, int) id, int msgType)
        {
            var marker = new MarkerObject(TfModule.ListenersFrame, id)
            {
                OcclusionOnly = RenderAsOcclusionOnly,
                Tint = Tint,
                Visible = Visible && config.VisibleMask[msgType],
                TriangleListFlipWinding = TriangleListFlipWinding,
                Metallic = Metallic,
                Smoothness = Smoothness,
                ShowDescription = ShowDescriptions
            };
            markers[id] = marker;
            return marker;
        }
    }
}