using System;

namespace Iviz.Msgs
{
    /// <summary>
    /// Interface for all ROS messages.
    /// All classes or structs representing ROS messages derive from this.
    /// </summary>
    public interface IMessage : IDisposable
    {
        /// <summary>
        /// Full ROS name of the message.
        /// </summary>
        string RosMessageType { get; }
        
        void IDisposable.Dispose()
        {
        }
    }

    public interface IMessageRos1 : IMessage, ISerializableRos1
    {
        /// <summary> MD5 hash of a compact representation of the message. </summary>
        public string RosMd5Sum => throw new RosInvalidMessageForVersion();

        /// <summary> Base64 of the GZip'd compression of the concatenated dependencies file. </summary>
        public string RosDependenciesBase64 => throw new RosInvalidMessageForVersion();
    }
    
    public interface IMessageRos2 : IMessage, ISerializableRos1
    {
        void AddRos2MessageLength(ref int c);
    }
}