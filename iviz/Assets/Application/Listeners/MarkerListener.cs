using System.Collections.Generic;
using System;
using Iviz.Msgs.visualization_msgs;

namespace Iviz.App
{
    public class MarkerListener : DisplayableListener
    {
        readonly Dictionary<string, MarkerObject> markers = new Dictionary<string, MarkerObject>();

        [Serializable]
        public class Configuration
        {
            public Resource.Module module => Resource.Module.Marker;
            public string topic = "";
            public string type = "";
        }

        readonly Configuration config = new Configuration();
        public Configuration Config
        {
            get => config;
            set
            {
                config.topic = value.topic;
                config.type = value.type;
            }
        }

        public override void StartListening()
        {
            Topic = config.topic;

            if (config.type == Marker.RosMessageType)
            {
                Listener = new RosListener<Marker>(config.topic, Handler);
            }
            else if (config.type == MarkerArray.RosMessageType)
            {
                Listener = new RosListener<MarkerArray>(config.topic, ArrayHandler);
            }
            GameThread.EverySecond += UpdateStats;
        }

        public override void Unsubscribe()
        {
            GameThread.EverySecond -= UpdateStats;
            Listener?.Stop();
            Listener = null;

            foreach (MarkerObject marker in markers.Values)
            {
                marker.Stop();
                ResourcePool.Dispose(Resource.Displays.MarkerObject, marker.gameObject);
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
            return $"{marker.ns}/{marker.id}";
        }


        void ArrayHandler(MarkerArray msg)
        {
            msg.markers.ForEach(Handler);
        }

        void Handler(Marker msg)
        {
            string id = IdFromMessage(msg);
            switch (msg.action)
            {
                case Marker.ADD:
                    if (!markers.TryGetValue(id, out MarkerObject markerToAdd))
                    {
                        markerToAdd = ResourcePool.GetOrCreate(Resource.Displays.MarkerObject, transform).GetComponent<MarkerObject>();
                        markerToAdd.Parent = TFListener.DisplaysFrame;
                        markers[id] = markerToAdd;
                    }
                    markerToAdd.Set(msg);
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
