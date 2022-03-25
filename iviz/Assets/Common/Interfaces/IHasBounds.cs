#nullable enable

using UnityEngine;

namespace Iviz.Common
{
    public interface IHasBounds : ISupportsDynamicBounds
    {
        Bounds? Bounds { get; }
        Transform? BoundsTransform { get; }
        string? Caption => null;
        bool AcceptsHighlighter => false;
    }
}