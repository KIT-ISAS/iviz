using System;
using Iviz.Msgs;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Iviz.Roslib;

namespace Iviz.Bridge
{
    public class IdCollection
    {
        readonly HashSet<string> ids = new();

        public void AddId(string id) => ids.Add(id);
        public void RemoveId(string id) => ids.Remove(id);
        public bool Empty => ids.Count == 0;
    }
}