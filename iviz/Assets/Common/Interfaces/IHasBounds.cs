#nullable enable

using System;
using UnityEngine;

namespace Iviz.Common
{
    public interface IHasBounds
    {
        Bounds? Bounds { get; }
        Transform? BoundsTransform { get; }
        string? Caption { get; }
        bool HasPermanentHighlighter { get; }
        event Action? BoundsChanged;
    }
}