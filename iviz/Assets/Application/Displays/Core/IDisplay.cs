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
        /// Name of the display. Used for the selection caption.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Bounds of the display in local coordinates.
        /// </summary>
        Bounds Bounds { get; }
        /// <summary>
        /// Bounds of the display in world coordinates.
        /// </summary>
        Bounds WorldBounds { get; }
        /// <summary>
        /// Pose of the display in world coordinates.
        /// </summary>
        Pose WorldPose { get; }
        /// <summary>
        /// Approximate scale of the display in world coordinates.
        /// </summary>
        Vector3 WorldScale { get; }
        /// <summary>
        /// Unity layer of the display.
        /// </summary>
        int Layer { get; set; }
        /// <summary>
        /// Parent transform of the display.
        /// </summary>
        Transform Parent { get; set; }
        /// <summary>
        /// Gets or sets whether the display collider is enabled.
        /// </summary>
        bool ColliderEnabled { get; set; }
        /// <summary>
        /// Tells the display that it is about to be sent to the Resource Pool, and that it should undo
        /// any changes that would prevent it from being reused.
        /// </summary>
        void Suspend();
        /// <summary>
        /// Gets or sets whether the display is visible. 
        /// </summary>
        bool Visible { get; set; }
    }
    
    /// <summary>
    /// Interface for displays that support the AR occlusion mode.
    /// </summary>
    public interface ISupportsAROcclusion : IDisplay
    {
        /// <summary>
        /// Gets or sets whether the occlusion mode is active. 
        /// </summary>
        bool OcclusionOnly { get; set; }
    }

    /// <summary>
    /// Interface for displays that support color tinting.
    /// </summary>
    public interface ISupportsTint : IDisplay
    {
        /// <summary>
        /// Gets or sets the color tint.
        /// </summary>
        Color Tint { get; set; }
    }

    /// <summary>
    /// Interface for displays that support both occlusion mode and color tinting.
    /// </summary>
    public interface ISupportsTintAndAROcclusion : ISupportsAROcclusion, ISupportsTint
    {
    }

}
