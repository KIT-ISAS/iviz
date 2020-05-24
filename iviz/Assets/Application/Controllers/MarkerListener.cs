using System.Collections.Generic;
using System;
using Iviz.Msgs.VisualizationMsgs;
using System.Runtime.Serialization;
using Iviz.RoslibSharp;
using Iviz.Resources;

namespace Iviz.App.Listeners
{
    [DataContract]
    public class MarkerConfiguration : JsonToString, IConfiguration
    {
        [DataMember] public Guid Id { get; set; } = Guid.NewGuid();
        [DataMember] public Resource.Module Module => Resource.Module.Marker;
        [DataMember] public bool Visible { get; set; } = true;
        [DataMember] public string Topic { get; set; } = "";
        [DataMember] public string Type { get; set; } = "";
    }

    public class MarkerListener : TopicListener
    {
        readonly Dictionary<string, MarkerObject> markers = new Dictionary<string, MarkerObject>();

        readonly MarkerConfiguration config = new MarkerConfiguration();
        public MarkerConfiguration Config
        {
            get => config;
            set
            {
                config.Topic = value.Topic;
                config.Type = value.Type;
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
                ResourcePool.Dispose(Resource.Listeners.MarkerObject, marker.gameObject);
            }
            markers.Clear();
        }

        /*
        public List<MarkerObject> ManageMarkers(IEnumerable<Marker> markers)
        {
            return ProcessMessages(markers);
        }

        List<MarkerObject> ProcessMessages(IEnumerable<Marker> ms)
        {
            List<MarkerObject> created = new List<MarkerObject>();

            foreach (Marker marker in ms)
            {
                string id = IdFromMessage(marker);
                switch (marker.action)
                {
                    case Marker.ADD:
                        if (!markers.TryGetValue(id, out MarkerObject markerToAdd))
                        {
                            markerToAdd = ResourcePool.GetOrCreate(Resource.Displays.MarkerObject, transform).GetComponent<MarkerObject>();
                            markerToAdd.Parent = TFListener.DisplaysFrame;
                            markers[id] = markerToAdd;
                        }
                        created.Add(markerToAdd);
                        break;
                    case Marker.DELETE:
                        if (markers.TryGetValue(id, out MarkerObject markerToDelete))
                        {
                            markerToDelete.Stop();
                            ResourcePool.Dispose(Resource.Displays.MarkerObject, markerToDelete.gameObject);
                            markers.Remove(id);
                        }
                        break;
                }
            }

            return created;
        }
        */

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
                    if (!markers.TryGetValue(id, out MarkerObject markerToAdd))
                    {
                        markerToAdd = ResourcePool.GetOrCreate<MarkerObject>(Resource.Listeners.MarkerObject, transform);
                        markerToAdd.Parent = TFListener.ListenersFrame;
                        markers[id] = markerToAdd;
                    }
                    markerToAdd.Set(msg);
                    break;
                case Marker.DELETE:
                    if (markers.TryGetValue(id, out MarkerObject markerToDelete))
                    {
                        markerToDelete.Stop();
                        ResourcePool.Dispose(Resource.Listeners.MarkerObject, markerToDelete.gameObject);
                        markers.Remove(id);
                    }
                    break;
            }
        }

        void CheckForExpiredMarkers()
        {
            /*
            float time = Time.realtimeSinceStartup;
            if (time - lastExpirationCheck > 1)
            {
                IEnumerable<string> deadMarkers = expirationTimes.Where(x => x.Value < time).Select(x => x.Key);
                deadMarkers.ForEach(DestroyMarker);
                lastExpirationCheck = time;
            }
            */
        }

    }

}
