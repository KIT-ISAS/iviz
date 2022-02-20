using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.Displays
{
    /// <summary>
    /// Marker resource with minimal functionality. Attached as a last resort to wrap an unknown loaded asset.
    /// </summary>
    public sealed class AssetWrapperDisplay : MarkerDisplay
    {
        public override int Layer
        {
            set
            {
                base.Layer = value;
                SetLayer(transform, value);
            }
        }

        static void SetLayer([NotNull] Transform transform, int layer)
        {
            transform.gameObject.layer = layer;
            for (int i = 0; i < transform.childCount; i++)
            {
                SetLayer(transform.GetChild(i), layer);
            }
        }
    }
}