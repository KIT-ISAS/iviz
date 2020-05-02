using System;
using System.Linq;

namespace Iviz.RoslibSharp
{
    public class SystemStateEntry
    {
        public string Name { get; }
        public string[] Nodes { get; }

        internal SystemStateEntry(string name, string[] nodes)
        {
            Name = name;
            Nodes = nodes;
        }
    }

    public class SystemState : JsonToString
    {
        public SystemStateEntry[] Publishers { get; }
        public SystemStateEntry[] Subscribers { get; }
        public SystemStateEntry[] Services { get; }

        internal SystemState(
            Tuple<string, string[]>[] publishers,
            Tuple<string, string[]>[] subscribers,
            Tuple<string, string[]>[] services)
        {
            Publishers = publishers.Select(x => new SystemStateEntry(x.Item1, x.Item2)).ToArray();
            Subscribers = subscribers.Select(x => new SystemStateEntry(x.Item1, x.Item2)).ToArray();
            Services = services.Select(x => new SystemStateEntry(x.Item1, x.Item2)).ToArray();
        }
    }
}
