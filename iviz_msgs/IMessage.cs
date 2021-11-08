

namespace Iviz.Msgs
{

    /// <summary>
    /// Interface for all ROS messages.
    /// All classes or structs representing ROS messages derive from this.
    /// </summary>
    public interface IMessage : ISerializable
    {
        /// <summary>
        /// Full ROS name of the message.
        /// </summary>
        string RosType { get; }
    }

}