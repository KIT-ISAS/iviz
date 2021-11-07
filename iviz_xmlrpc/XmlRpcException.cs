using System;

namespace Iviz.XmlRpc
{
    /// <summary>
    /// Parent class for the exceptions from this library.
    /// </summary>
    public class XmlRpcException : Exception
    {
        protected XmlRpcException(string message) : base(message)
        {
        }

        protected XmlRpcException(string message, Exception inner) : base(message, inner)
        {
        }
    }
    
    /// <summary>
    /// Thrown when the remote call reported an exception. 
    /// </summary>
    public class FaultException : XmlRpcException
    {
        public FaultException(string message) : base(message)
        {
        }
    }

    /// <summary>
    /// Thrown when the XML could not be parsed.
    /// </summary>
    public class ParseException : XmlRpcException
    {
        public ParseException(string message) : base(message)
        {
        }

        public ParseException(string message, Exception inner) : base(message, inner)
        {
        }
    }

    /// <summary>
    /// Thrown when an error happened during the connection.
    /// </summary>    
    public class HttpConnectionException : XmlRpcException
    {
        public HttpConnectionException(string message) : base(message)
        {
        }

        public HttpConnectionException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    public class RpcConnectionException : HttpConnectionException
    {
        public RpcConnectionException(string message) : base(message)
        {
        }

        public RpcConnectionException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}