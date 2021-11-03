namespace Iviz.Roslib
{
    /// <summary>
    /// Hint on what kind of connection will be requested
    /// </summary>
    public enum RosTransportHint
    {
        /// <summary>
        /// Take TCP if offered, otherwise take anything
        /// </summary>
        PreferTcp,

        /// <summary>
        /// Take UDP if offered, otherwise take anything
        /// </summary>
        PreferUdp,
        
        /// <summary>
        /// Only allow TCP
        /// </summary>
        OnlyTcp,

        /// <summary>
        /// Only allow UDP
        /// </summary>
        OnlyUdp,
    }
}