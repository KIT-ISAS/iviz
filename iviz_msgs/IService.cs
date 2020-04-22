namespace Iviz.Msgs
{
    public interface IService
    {
        /// <summary>
        /// Create an empty response to this message
        /// </summary>
        /// <returns>New message</returns>
        IResponse CreateResponse();

        /// <summary>
        /// Returns the request message
        /// </summary>
        /// <returns>Request message</returns>
        IRequest GetRequest();

        /// <summary>
        /// Sets the response message
        /// </summary>
        void SetResponse(IResponse response);
    }

}