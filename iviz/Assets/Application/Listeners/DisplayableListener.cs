using System.Collections.Generic;
using System.Collections.ObjectModel;
using Iviz.Msgs.sensor_msgs;
using Iviz.Msgs.visualization_msgs;
using UnityEngine;

namespace Iviz.App
{
    public abstract class DisplayableListener : DisplayNode
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

        public string Topic { get; set; }
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
            /*
            if (Listener == null)
            {
                return;
            }

            Connected = Listener.Connected;
            Subscribed = Listener.Subscribed;

            var stats = Listener.UpdateStats();
            MessagesPerSecond = stats.MessagesPerSecond;
            MessagesJitterMax = stats.JitterMax;
            MessagesJitterMin = stats.JitterMin;
            */
        }

    }

    public abstract class TopicListener : MonoBehaviour
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

        public static ReadOnlyDictionary<string, Resource.Module> ResourceByRosMessageType { get; } = 
            new ReadOnlyDictionary<string, Resource.Module>(resourceByRosMessageType);

        public RosListener Listener { get; protected set; }

        public bool Subscribed => Listener != null;
        public string Topic => Listener?.Topic;
        public float MessagesPerSecond => Listener?.Stats.MessagesPerSecond ?? 0;
        public float MessagesJitterMax => Listener?.Stats.JitterMax ?? 0;
        public float MessagesJitterMin => Listener?.Stats.JitterMin ?? 0;


        public virtual void StartListening()
        {
            GameThread.EverySecond += UpdateStats;
        }

        public virtual void Stop()
        {
            GameThread.EverySecond -= UpdateStats;
            Listener?.Stop();
            Listener = null;
        }

        void UpdateStats()
        {
            Listener?.UpdateStats();
        }
    }

}