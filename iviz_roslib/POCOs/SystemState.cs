using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Iviz.RoslibSharp.XmlRpc;

namespace Iviz.RoslibSharp
{
    public class SystemState : JsonToString
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
