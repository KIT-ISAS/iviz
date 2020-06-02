using UnityEngine;

namespace Iviz.Displays
{
    public interface IDisplay
    {
        string Name { get; }
        Bounds Bounds { get; }
        Bounds WorldBounds { get; }
        bool ColliderEnabled { get; set; }
        void Stop();
        Transform Parent { get; set; }
        bool Visible { get; set; }
    }
}
