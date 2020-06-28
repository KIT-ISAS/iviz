using System.Collections.Generic;
using UnityEngine;

namespace Iviz.App.Listeners
{
    public abstract class ListenerController : MonoBehaviour, IController, IHasFrame
    {
        public RosListener Listener { get; protected set; }

        public abstract TFFrame Frame { get; }

        public int NumPublishers =>
            (!ConnectionManager.Connected || Listener == null) ? -1 : Listener.NumPublishers;
        
        public abstract ModuleData ModuleData { get; set; }

        public virtual void StartListening()
        {
        }

        public virtual void Stop()
        {
            Listener?.Stop();
            Listener = null;
        }
    }

}