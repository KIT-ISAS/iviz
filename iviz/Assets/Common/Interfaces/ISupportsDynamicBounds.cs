using System;

namespace Iviz.Common
{
    /// <summary>
    /// Interface for displays whose bounds can change after initial setup (for example, async mesh loading)
    /// </summary>
    public interface ISupportsDynamicBounds
    {
        event Action? BoundsChanged;
    }
}