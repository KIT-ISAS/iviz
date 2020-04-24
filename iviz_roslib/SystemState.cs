using System;
using System.Linq;

namespace Iviz.RoslibSharp
{
    [Serializable]
    public class SystemState : JsonToString
    {
        [Serializable]
        public readonly struct Entry
        {
            public readonly string name;
            public readonly string[] nodes;

            public Entry(string name, string[] nodes)
            {
                this.name = name;
                this.nodes = nodes;
            }
        }

        public readonly Entry[] publishers;
        public readonly Entry[] subscribers;
        public readonly Entry[] services;

        public SystemState(
            Tuple<string, string[]>[] publishers,
            Tuple<string, string[]>[] subscribers,
            Tuple<string, string[]>[] services)
        {
            this.publishers = publishers.Select(x => new Entry(x.Item1, x.Item2)).ToArray();
            this.subscribers = subscribers.Select(x => new Entry(x.Item1, x.Item2)).ToArray();
            this.services = services.Select(x => new Entry(x.Item1, x.Item2)).ToArray();
        }
    }
}
