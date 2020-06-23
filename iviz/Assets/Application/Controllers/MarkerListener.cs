using System.Collections.Generic;
using System;
using Iviz.Msgs.VisualizationMsgs;
using System.Runtime.Serialization;
using Iviz.RoslibSharp;
using Iviz.Resources;
using UnityEngine;

namespace Iviz.App.Listeners
{
    [DataContract]
    public sealed class MarkerConfiguration : JsonToString, IConfiguration
    {
        [DataMember] public Guid Id { get; set; } = Guid.NewGuid();
        [DataMember] public Resource.Module Module => Resource.Module.Marker;
        [DataMember] public bool Visible { get; set; } = true;
        [DataMember] public string Topic { get; set; } = "";
        [DataMember] public string Type { get; set; } = "";
        [DataMember] public bool RenderAsOcclusionOnly { get; set; } = false;
        [DataMember] public SerializableColor Tint { get; set; } = Color.white;
    }

    public class MarkerListener : ListenerController
    {
        readonly Dictionary<string, MarkerObject> markers = new Dictionary<string, MarkerObject>();

        public override ModuleData ModuleData { get; set; }

        public override TFFrame Frame => TFListener.BaseFrame;

        readonly MarkerConfiguration config = new MarkerConfiguration();
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
            }
        }

        public bool RenderAsOcclusionOnly
        {
            get => config.RenderAsOcclusionOnly;
            set
            {
                config.RenderAsOcclusionOnly = value;

                foreach (MarkerObject marker in markers.Values)
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

                foreach (MarkerObject marker in markers.Values)
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

                foreach (MarkerObject marker in markers.Values)
                {
                    marker.Visible = value;
                }
            }
        }

        public override void StartListening()
        {
            base.StartListening();
            if (config.Type == Marker.RosMessageType)
            {
                Listener = new RosListener<Marker>(config.Topic, Handler);
            }
            else if (config.Type == MarkerArray.RosMessageType)
            {
                Listener = new RosListener<MarkerArray>(config.Topic, ArrayHandler);
            }
        }

        public override void Stop()
        {
            base.Stop();

            foreach (MarkerObject marker in markers.Values)
            {
                marker.Stop();
                Destroy(marker.gameObject);
            }
            markers.Clear();
        }

        public static string IdFromMessage(Marker marker)
        {
            return $"{marker.Ns}/{marker.Id}";
        }

        void ArrayHandler(MarkerArray msg)
        {
            msg.Markers.ForEach(Handler);
        }

        void Handler(Marker msg)
        {
            string id = IdFromMessage(msg);
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
                    if (!markers.TryGetValue(id, out MarkerObject markerToAdd))
                    {
                        markerToAdd = CreateMarkerObject();
                        markerToAdd.DisplayData = ModuleData;
                        markerToAdd.Parent = TFListener.ListenersFrame;
                        markerToAdd.OcclusionOnly = RenderAsOcclusionOnly;
                        markerToAdd.Tint = Tint;
                        markerToAdd.Visible = Visible;
                        markers[id] = markerToAdd;
                    }
                    markerToAdd.Set(msg);
                    break;
                case Marker.DELETE:
                    if (markers.TryGetValue(id, out MarkerObject markerToDelete))
                    {
                        DeleteMarkerObject(markerToDelete);
                        markers.Remove(id);
                    }
                    break;
            }
        }

        static void DeleteMarkerObject(MarkerObject markerToDelete)
        {
            markerToDelete.Stop();
            Destroy(markerToDelete.gameObject);
        }

        static MarkerObject CreateMarkerObject()
        {
            GameObject gameObject = new GameObject();
            gameObject.name = "MarkerObject";
            return gameObject.AddComponent<MarkerObject>();
        }
    }

}
