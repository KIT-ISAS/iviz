#nullable enable

using System;
using UnityEngine;

namespace Iviz.Displays
{
    /// <summary>
    /// Common interface for all displays.
    /// A display is a component in charge of preparing and showing raw visual data, such as meshes, lines, or point clouds.
    /// </summary>
    public interface IDisplay
    {
        /// <summary>
        /// Bounds of the display in local coordinates, or null if the display is empty.
        /// </summary>
        Bounds? Bounds { get; }
        /// <summary>
        /// Unity layer of the display.
        /// </summary>
        int Layer { set; }
        /// <summary>
        /// Tells the display that it is about to be sent to the Resource Pool, and that it should undo
        /// any changes that would prevent it from being reused.
        /// </summary>
        void Suspend();
    }
    
    /// <summary>
    /// Interface for displays that support the AR occlusion mode.
    /// </summary>
    public interface ISupportsAROcclusion
    {
        /// <summary>
        /// Gets or sets whether the occlusion mode is active. 
        /// </summary>
        bool OcclusionOnly { set; }
    }

    /// <summary>
    /// Interface for displays that support color tinting.
    /// </summary>
    public interface ISupportsTint
    {
        /// <summary>
        /// Gets or sets the color tint.
        /// </summary>
        Color Tint { set; }
    }
    
    public interface ISupportsColor
    {
        Color Color { set; }
        Color EmissiveColor { set; }
    }
    
    public interface ISupportsPbr
    {
        float Metallic { set; }
        float Smoothness { set; }
    }
    
    public interface ISupportsShadows
    {
        bool ShadowsEnabled { set; }
    }
}
