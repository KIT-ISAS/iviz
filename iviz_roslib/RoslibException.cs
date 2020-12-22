using System;

namespace Iviz.Roslib
{
    /// <summary>
    /// Parent class for the exceptions of this library.
    /// </summary>
    public class RoslibException : Exception
    {
        protected RoslibException(string message) : base(message)
        {
        }

        public RoslibException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public RoslibException()
        {
        }
    }
    
    /// <summary>
    /// Thrown when the provided message type is not correct.
    /// </summary>
    public class InvalidMessageTypeException : RoslibException
    {
        public InvalidMessageTypeException(string message) : base(message)
        {
        }

        public InvalidMessageTypeException()
        {
        }

        public InvalidMessageTypeException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    /// <summary>
    /// Thrown when an error happened during the connection.
    /// </summary>
    public class ConnectionException : RoslibException
    {
        public ConnectionException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ConnectionException()
        {
        }

        public ConnectionException(string message) : base(message)
        {
        }
    }
    
    /// <summary>
    /// Thrown when an error happened during the connection.
    /// </summary>
    public class UriBindingException : ConnectionException
    {
        public UriBindingException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public UriBindingException()
        {
        }

        public UriBindingException(string message) : base(message)
        {
        }
    }

    /// <summary>
    /// Thrown when the uri provided for the caller (this node) is not reachable.
    /// </summary>
    public class UnreachableUriException : RoslibException
    {
        public UnreachableUriException(string message) : base(message)
        {
        }

        public UnreachableUriException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public UnreachableUriException()
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

        public RosRpcException()
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

        public RosHandshakeException()
        {
        }

        public RosHandshakeException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }    
    
    public class RosServiceCallFailed : RoslibException
    {
        public RosServiceCallFailed(string message) : base(message)
        {
        }

        public RosServiceCallFailed()
        {
        }

        public RosServiceCallFailed(string message, Exception innerException) : base(message, innerException)
        {
        }
    }  
    public class RosInvalidPackageSizeException : RoslibException
    {
        public RosInvalidPackageSizeException(string message) : base(message)
        {
        }

        public RosInvalidPackageSizeException()
        {
        }

        public RosInvalidPackageSizeException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }    
    
    
}
