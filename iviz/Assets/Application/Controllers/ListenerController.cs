using System.Collections.Generic;
using UnityEngine;

namespace Iviz.App.Listeners
{
    public abstract class ListenerController : IController, IHasFrame
    {
        public RosListener Listener { get; protected set; }

        public abstract TFFrame Frame { get; }

        public abstract ModuleData ModuleData { get; }

        public abstract void StartListening();

        public virtual void Stop()
        {
            Listener?.Stop();
            Listener = null;
        }

        public virtual void Reset()
        {
            Debug.Log(this + ": Resetting!");
            Listener?.Reset();
        }

        public override string ToString()
        {
            return $"[{GetType().Name}:'{Listener?.Topic}']";
        }
    }
}