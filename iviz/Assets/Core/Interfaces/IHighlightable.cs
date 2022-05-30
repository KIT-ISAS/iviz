
using UnityEngine;

namespace Iviz.Common
{
    /// <summary>
    /// Interface for classes that can be "highlighted" or react in some way to a user's click / tap / VR squeeze.
    /// For example by displaying a tooltip or a frame boundary. 
    /// </summary>
    public interface IHighlightable
    {
        /// <summary>
        /// Tells the class to react to a user click.
        /// </summary>
        /// <param name="hitPoint">The point on the boundary where the click happened.</param>
        void Highlight(in Vector3 hitPoint);
        
        /// <summary>
        /// Called right before <see cref="Highlight"/>, asks the class whether it is in a position to react to the
        /// click. Otherwise, the next one in the pointer ray will be highlighted.  
        /// </summary>
        bool IsAlive { get; }
    }
}