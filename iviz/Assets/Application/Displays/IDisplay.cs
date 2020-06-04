using UnityEngine;

namespace Iviz.Displays
{
    public interface IDisplay
    {
        string Name { get; }
        Bounds Bounds { get; }
        Bounds WorldBounds { get; }
        Pose WorldPose { get; }
        Vector3 WorldScale { get; }
        int Layer { get; set; }
        Transform Parent { get; set; }
        bool ColliderEnabled { get; set; }
        void Stop();
        bool Visible { get; set; }
    }
}
