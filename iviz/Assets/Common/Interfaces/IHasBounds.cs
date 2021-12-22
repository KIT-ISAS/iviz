#nullable enable

using System;
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
    
    
    /// <summary>
    /// Interface for displays whose bounds can change after initial setup (for example, async mesh loading)
    /// </summary>
    public interface ISupportsDynamicBounds
    {
        event Action? BoundsChanged;
    }     
}