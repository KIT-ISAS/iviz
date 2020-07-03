using System.Collections.Generic;
using UnityEngine;

namespace Iviz.App.Listeners
{
    public abstract class ListenerController : MonoBehaviour, IController, IHasFrame
    {
        public RosListener Listener { get; protected set; }

        public abstract TFFrame Frame { get; }

        public abstract ModuleData ModuleData { get; set; }

        public abstract void StartListening();

        public virtual void Stop()
        {
            Listener?.Stop();
            Listener = null;
        }
    }

}