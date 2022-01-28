namespace Iviz.Ros
{
    public enum ConnectionState
    {
        /// <summary>
        /// Iviz is disconnected.
        /// </summary>
        Disconnected,

        /// <summary>
        /// Iviz is in the middle of establishing a connection.
        /// </summary>
        Connecting,

        /// <summary>
        /// Iviz is connected.
        /// </summary>
        Connected
    }
}