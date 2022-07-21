namespace Iviz.Msgs
{
    /// <summary>
    /// Interface for all ROS services.
    /// All classes representing ROS services derive from this.
    /// </summary>
    public interface IService
    {
        /// <summary>
        /// The request message.
        /// </summary>
        IRequest Request { get; set; }

        /// <summary>
        /// The response message.
        /// </summary>
        IResponse Response { get; set; }

        /// <summary>
        /// Full ROS name of the service.
        /// </summary>
        string RosServiceType { get; }
        
        /// <summary>
        /// MD5 hash of a compact representation of the service."
        /// </summary>
        string RosMd5Sum { get; }
        
    }

    /// <summary>
    /// Interface for all ROS service requests.
    /// All classes representing ROS requests derive from this.
    /// </summary>
    public interface IRequest : ISerializable
    {
    }

    public interface IRequest<TT, TU> : IRequest where TT : IService, new() where TU : IResponse
    {
    }
    
    /// <summary>
    /// Interface for all ROS service responses.
    /// All classes representing ROS responses derive from this.
    /// </summary>
    public interface IResponse : ISerializable
    {
    }
}