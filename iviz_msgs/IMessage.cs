using System;

namespace Iviz.Msgs
{
    /// <summary>
    /// Interface for all ROS messages.
    /// All classes or structs representing ROS messages derive from this.
    /// </summary>
    public interface IMessage : ISerializable, IDisposable
    {
        /// <summary>
        /// Full ROS name of the message.
        /// </summary>
        string RosType { get; }

        void IDisposable.Dispose()
        {
        }
    }
}