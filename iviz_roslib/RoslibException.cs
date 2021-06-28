using System;
using Iviz.Msgs;

namespace Iviz.Roslib
{
    /// <summary>
    /// Parent class for the exceptions of this library.
    /// </summary>
    public class RoslibException : RosException
    {
        protected RoslibException(string message) : base(message)
        {
        }

        public RoslibException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    /// <summary>
    /// Thrown when the provided message type is not correct.
    /// </summary>
    public class RosInvalidMessageTypeException : RoslibException
    {
        public RosInvalidMessageTypeException(string message) : base(message)
        {
        }

        public RosInvalidMessageTypeException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    /// <summary>
    /// Thrown when an error happened during the connection.
    /// </summary>
    public class RosConnectionException : RoslibException
    {
        public RosConnectionException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public RosConnectionException(string message) : base(message)
        {
        }
    }

    /// <summary>
    /// Thrown when an error happened during the connection.
    /// </summary>
    public class RosUriBindingException : RosConnectionException
    {
        public RosUriBindingException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public RosUriBindingException(string message) : base(message)
        {
        }
    }

    /// <summary>
    /// Thrown when the uri provided for the caller (this node) is not reachable.
    /// </summary>
    public class RosUnreachableUriException : RoslibException
    {
        public RosUnreachableUriException(string message) : base(message)
        {
        }

        public RosUnreachableUriException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    /// <summary>
    /// Wrapper around a <see cref="Iviz.XmlRpc.XmlRpcException"/>
    /// </summary>
    public class RosRpcException : RoslibException
    {
        public RosRpcException(string message) : base(message)
        {
        }

        public RosRpcException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    public class RosHandshakeException : RosRpcException
    {
        public RosHandshakeException(string message) : base(message)
        {
        }
    }

    public class RosServiceNotFoundException : RoslibException
    {
        public string ServiceName { get; }
        public string ErrorMessage { get; }

        public RosServiceNotFoundException(string service, string message)
            : base($"Failed to call service {service}. Reason: {message}")
        {
            ServiceName = service;
            ErrorMessage = message;
        }

        public RosServiceNotFoundException(string service, string message, Exception innerException)
            : base($"Failed to call service {service}. Reason: {message}", innerException)
        {
            ServiceName = service;
            ErrorMessage = message;
        }
    }

    public class RosServiceCallFailed : RoslibException
    {
        public string ServiceName { get; }
        public string ServerMessage { get; }

        public RosServiceCallFailed(string service, string message) : base(
            $"Service call to '{service}' failed. Reason: {message}")
        {
            ServiceName = service;
            ServerMessage = message;
        }
    }

    public class RosInvalidPackageSizeException : RoslibException
    {
        public RosInvalidPackageSizeException(string message) : base(message)
        {
        }

        public RosInvalidPackageSizeException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    public class RosQueueOverflowException : RosQueueException
    {
        public RosQueueOverflowException(string message, IRosTcpSender sender) : base(message, sender)
        {
        }
    }
    
    public class RosQueueException : RoslibException
    {
        public IRosTcpSender Sender { get; }
        
        public RosQueueException(string message, IRosTcpSender sender) : base(message)
        {
            Sender = sender;
        }

        public RosQueueException(string message, Exception innerException, IRosTcpSender sender) : base(message, innerException)
        {
            Sender = sender;
        }
    }    
    
    /// <summary>
    /// Thrown when an error happened during the connection.
    /// </summary>
    public class RosServiceRequestTimeout : RoslibException
    {
        public RosServiceRequestTimeout(string message, Exception innerException) : base(message, innerException)
        {
        }

        public RosServiceRequestTimeout(string message) : base(message)
        {
        }
    }    
    
    public class RosInvalidResourceName : RoslibException
    {
        public RosInvalidResourceName(string message, Exception innerException) : base(message, innerException)
        {
        }

        public RosInvalidResourceName(string message) : base(message)
        {
        }
    }    
}

