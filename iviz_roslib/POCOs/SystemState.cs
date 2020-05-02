using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Iviz.RoslibSharp
{
    public class SystemStateEntry
    {
        public string Name { get; }
        public ReadOnlyCollection<string> Nodes { get; }

        internal SystemStateEntry(string name, IList<string> nodes)
        {
            Name = name;
            Nodes = new ReadOnlyCollection<string>(nodes);
        }
    }

    public class SystemState : JsonToString
    {
        public ReadOnlyCollection<SystemStateEntry> Publishers { get; }
        public ReadOnlyCollection<SystemStateEntry> Subscribers { get; }
        public ReadOnlyCollection<SystemStateEntry> Services { get; }

        internal SystemState(
            IList<Tuple<string, string[]>> publishers,
            IList<Tuple<string, string[]>> subscribers,
            IList<Tuple<string, string[]>> services)
        {
            Publishers = new ReadOnlyCollection<SystemStateEntry>(
                publishers.Select(x => new SystemStateEntry(x.Item1, x.Item2)).ToArray()
                );
            Subscribers = new ReadOnlyCollection<SystemStateEntry>(
                subscribers.Select(x => new SystemStateEntry(x.Item1, x.Item2)).ToArray()
                );
            Services = new ReadOnlyCollection<SystemStateEntry>(
                services.Select(x => new SystemStateEntry(x.Item1, x.Item2)).ToArray()
                );
        }
    }
}
