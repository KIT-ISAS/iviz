using System.Collections.Generic;

namespace Iviz.Msgs
{
    /// <summary>
    /// Generic information about a ROS data connection.
    /// </summary>
    public interface IRosConnection
    {
        /// <summary>
        /// The connection topic
        /// </summary>
        string Topic { get; }

        /// <summary>
        /// The ROS message type
        /// </summary>
        string Type { get; }

        /// <summary>
        /// List of TCPROS header entries in the form of strings a=b
        /// </summary>
        IReadOnlyCollection<string> RosHeader { get; }
    }
}