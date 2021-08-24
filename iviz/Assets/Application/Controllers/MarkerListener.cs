using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Iviz.App;
using Iviz.Msgs.IvizCommonMsgs;
using Iviz.Core;
using Iviz.Msgs;
using Iviz.Msgs.VisualizationMsgs;
using Iviz.Resources;
using Iviz.Ros;
using Iviz.Roslib;
using Iviz.Tools;
using Iviz.XmlRpc;
using JetBrains.Annotations;
using UnityEngine;
using Logger = Iviz.Core.Logger;
using Object = UnityEngine.Object;

namespace Iviz.Controllers
{
    public sealed class MarkerListener : ListenerController, IMarkerDialogListener
    {
        readonly MarkerConfiguration config = new MarkerConfiguration();

        /// List of markers
        readonly Dictionary<(string Ns, int Id), MarkerObject> markers =
            new Dictionary<(string Ns, int Id), MarkerObject>();

        /// Temporary buffer to hold incoming marker messages from the network thread
        readonly Dictionary<(string Ns, int Id), Marker> markerBuffer = new Dictionary<(string Ns, int Id), Marker>();

        public MarkerListener([NotNull] IModuleData moduleData)
        {
            ModuleData = moduleData ?? throw new ArgumentNullException(nameof(moduleData));
        }

        public override IModuleData ModuleData { get; }
        [NotNull] public override TfFrame Frame => TfListener.DefaultFrame;

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
                
                if (value.VisibleMask.Length != config.VisibleMask.Length)
                {
                    Logger.Error($"{this}: Invalid visibility array of size {value.VisibleMask.Length}");
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

        public override void StartListening()
        {
            switch (config.Type)
            {
                case Marker.RosMessageType:
                    Listener = new Listener<Marker>(config.Topic, Handler);
                    break;
                case MarkerArray.RosMessageType:
                    Listener = new Listener<MarkerArray>(config.Topic, Handler);
                    break;
            }

            GameThread.EverySecond += CheckDeadMarkers;
            GameThread.EveryFrame += HandleAsync;
        }

        void CheckDeadMarkers()
        {
            var deadEntries = markers
                .Where(entry => entry.Value.ExpirationTime < GameThread.Now)
                .ToArray();
            foreach (var entry in deadEntries)
            {
                markers.Remove(entry.Key);
                DeleteMarkerObject(entry.Value);
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
                        markerStr = $"<b>{markers.Count.ToString()} markers →</b>";
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
                        warnStr = $"{totalWarnings} warnings";
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

        bool Handler([NotNull] MarkerArray msg)
        {
            lock (markerBuffer)
            {
                foreach (var marker in msg.Markers)
                {
                    markerBuffer[IdFromMessage(marker)] = marker;
                }
            }

            return true;
        }

        bool Handler([NotNull] Marker marker)
        {
            lock (markerBuffer)
            {
                markerBuffer[IdFromMessage(marker)] = marker;
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

        async void HandleAsync()
        {
            UniqueRef<Marker> markerList;
            lock (markerBuffer)
            {
                if (markerBuffer.Count == 0)
                {
                    return;
                }

                markerList = new UniqueRef<Marker>(markerBuffer.Count);
                markerBuffer.Values.CopyTo(markerList.Array, 0);
                markerBuffer.Clear();
            }

            try
            {
                await markerList.Select(HandleAsync).WhenAll();
            }
            finally
            {
                markerList.Dispose();
            }
        }

        async Task HandleAsync([NotNull] Marker msg)
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

                    if (msg.Type >= config.VisibleMask.Length)
                    {
                        Logger.Debug($"MarkerListener: Unknown type {msg.Type.ToString()}");
                        return;
                    }

                    if (!markers.TryGetValue(id, out var markerToAdd))
                    {
                        markerToAdd = CreateMarkerObject();
                        markerToAdd.Parent = TfListener.ListenersFrame;
                        markerToAdd.OcclusionOnly = RenderAsOcclusionOnly;
                        markerToAdd.Tint = Tint;
                        markerToAdd.Visible = Visible && config.VisibleMask[msg.Type];
                        markerToAdd.Layer = LayerType.IgnoreRaycast;
                        markerToAdd.TriangleListFlipWinding = TriangleListFlipWinding;
                        markers[id] = markerToAdd;
                    }

                    await markerToAdd.SetAsync(msg).AwaitNoThrow(this);
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
            markerToDelete.DestroySelf();
        }

        static MarkerObject CreateMarkerObject()
        {
            var gameObject = new GameObject("MarkerObject");
            return gameObject.AddComponent<MarkerObject>();
        }
    }
}