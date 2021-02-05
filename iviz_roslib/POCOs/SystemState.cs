using System.Collections.ObjectModel;
using Iviz.Roslib.XmlRpc;

namespace Iviz.Roslib
{
    public sealed class SystemState : JsonToString
    {
        public ReadOnlyCollection<TopicTuple> Publishers { get; }
        public ReadOnlyCollection<TopicTuple> Subscribers { get; }
        public ReadOnlyCollection<TopicTuple> Services { get; }

        internal SystemState(
            ReadOnlyCollection<TopicTuple> publishers,
            ReadOnlyCollection<TopicTuple> subscribers,
            ReadOnlyCollection<TopicTuple> services)
        {
            Publishers = publishers;
            Subscribers = subscribers;
            Services = services;
        }
    }
}
