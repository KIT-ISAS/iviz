using System.Runtime.Serialization;

namespace Iviz.Msgs
{
    public interface IService
    {
        /// <summary>
        /// Create an empty service message
        /// </summary>
        /// <returns>New service message</returns>
        IService Create();

        /// <summary>
        /// Returns the request message
        /// </summary>
        /// <returns>Request message</returns>
        IRequest Request { get; }

        IResponse Response { get; }

        string ErrorMessage { get; set; }
    }

    public interface IRequest : ISerializable
    {
    }


    public interface IResponse : ISerializable
    {
    }
}