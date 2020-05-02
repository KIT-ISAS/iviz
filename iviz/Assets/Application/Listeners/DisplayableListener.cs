using System.Collections.Generic;
using Iviz.Msgs.sensor_msgs;
using Iviz.Msgs.visualization_msgs;

namespace Iviz.App
{
    public abstract class DisplayableListener : Display
    {
        static readonly Dictionary<string, Resource.Module> resourceByRosMessageType = new Dictionary<string, Resource.Module>
        {
            { PointCloud2.RosMessageType, Resource.Module.PointCloud },
            { Image.RosMessageType, Resource.Module.Image },
            { CompressedImage.RosMessageType, Resource.Module.Image },
            { Marker.RosMessageType, Resource.Module.Marker },
            { MarkerArray.RosMessageType, Resource.Module.Marker },
            { InteractiveMarkerUpdate.RosMessageType, Resource.Module.InteractiveMarker },
            { JointState.RosMessageType, Resource.Module.JointState },
            { LaserScan.RosMessageType, Resource.Module.LaserScan },
        };

        public static IReadOnlyDictionary<string, Resource.Module> ResourceByRosMessageType => resourceByRosMessageType;

        public string Topic;
        public bool Connected;
        public bool Subscribed;
        public float MessagesPerSecond;
        public float MessagesJitterMax;
        public float MessagesJitterMin;

        public RosListener Listener { get; protected set; }

        public abstract void StartListening();

        public abstract void Unsubscribe();

        public override void Stop()
        {
            base.Stop();
            Unsubscribe();
        }

        public void UpdateStats()
        {
            if (Listener == null)
            {
                return;
            }

            Connected = Listener.Connected;
            Subscribed = Listener.Subscribed;

            var stats = Listener.CalculateStats();
            MessagesPerSecond = stats.MessagesPerSecond;
            MessagesJitterMax = stats.JitterMax;
            MessagesJitterMin = stats.JitterMin;
        }

    }
}