namespace Iviz.Rosbag.Writer
{
    public interface IRosbagConnection
    {
        /// <summary>
        /// The connection topic
        /// </summary>
        string Topic { get; }
        /// <summary>
        /// List of TCPROS header entries in the form of strings a=b. May be null if the receiver is reconnecting
        /// </summary>
        string[] TcpHeader { get; }
    }
}