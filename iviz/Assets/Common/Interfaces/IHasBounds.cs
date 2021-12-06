#nullable enable

using System;
using UnityEngine;

namespace Iviz.Common
{
    public interface IHasBounds
    {
        Bounds? Bounds { get; }
        Transform? BoundsTransform { get; }
        string? Caption => null;
        bool AcceptsHighlighter => false;
        event Action? BoundsChanged;
    }
}