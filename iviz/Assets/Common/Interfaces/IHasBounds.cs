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
        bool AcceptsHighlighter { get; }
        event Action? BoundsChanged;
    }
}