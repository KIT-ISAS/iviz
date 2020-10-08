namespace Iviz.Displays
{
    /// <summary>
    /// Tells the Resource Pool that, when this resource is destroyed for inactivity, it can be broken down into smaller resources.
    /// For example, when destroyed, a TF Frame can produce three cubes, which can be reused by other displays.
    /// </summary>
    public interface IRecyclable
    {
        /// <summary>
        /// Tells the resource to break down and release its resources.
        /// Called right before the resource pool destroys this.
        /// </summary>
        void SplitForRecycle();
    }
}
