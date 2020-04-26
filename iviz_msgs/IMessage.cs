

namespace Iviz.Msgs
{
    /// <summary>
    /// Interface for all ROS messages.
    /// All classes or structs representing ROS messages derive from this.
    /// </summary>
    public interface IMessage : ISerializable
    {
        /// <summary>
        /// Create a new instance of a message of this type.
        /// </summary>
        /// <returns>New message</returns>
        IMessage Create();

        string RosType { get; }
    }

}