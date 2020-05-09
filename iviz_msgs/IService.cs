﻿using System.Runtime.Serialization;

namespace Iviz.Msgs
{
    /// <summary>
    /// Interface for all ROS services.
    /// All classes representing ROS services derive from this.
    /// </summary>
    public interface IService
    {
        /// <summary>
        /// Create an empty service message.
        /// </summary>
        /// <returns>New service message</returns>
        IService Create();

        /// <summary>
        /// The request message.
        /// </summary>
        IRequest Request { get; set; }

        /// <summary>
        /// The response message.
        /// </summary>
        IResponse Response { get; set; }

        /// <summary>
        /// The error message. Can be null. If not null, overrides the response.
        /// </summary>
        string ErrorMessage { get; set; }

        /// <summary>
        /// Full ROS name of the service.
        /// </summary>
        string RosType { get; }
    }

    /// <summary>
    /// Interface for all ROS service requests.
    /// All classes representing ROS requests derive from this.
    /// </summary>
    public interface IRequest : ISerializable<IRequest>
    {
    }

    /// <summary>
    /// Interface for all ROS service responses.
    /// All classes representing ROS responses derive from this.
    /// </summary>
    public interface IResponse : ISerializable<IResponse>
    {
    }

    namespace Internal
    {
        /// <summary>
        /// Class that represents an empty service request.
        /// </summary>
        public class EmptyRequest : IRequest
        {
            public IRequest Deserialize(Buffer b)
            {
                return new EmptyRequest();
            }

            public void Serialize(Buffer b)
            {
            }

            [IgnoreDataMember]
            public int RosMessageLength => 0;
        }

        /// <summary>
        /// Class that represents an empty service response.
        /// </summary>
        public class EmptyResponse : IResponse
        {
            public IResponse Deserialize(Buffer b)
            {
                return new EmptyResponse();
            }

            public void Serialize(Buffer b)
            {
            }

            [IgnoreDataMember]
            public int RosMessageLength => 0;
        }
    }
}