#nullable enable

using Iviz.Core;
using Iviz.Tools;
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
                foreach (var child in transform.GetAllChildren()) // includes 'this'
                {
                    child.gameObject.layer = value;
                }
            }
        }
    }
}