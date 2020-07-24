using UnityEngine;

namespace Iviz.Controllers
{
    public abstract class ListenerController : IController, IHasFrame
    {
        public RosListener Listener { get; protected set; }

        public abstract TFFrame Frame { get; }

        public abstract IModuleData ModuleData { get; }

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