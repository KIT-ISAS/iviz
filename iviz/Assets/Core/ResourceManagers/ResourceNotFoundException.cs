using System;

namespace Iviz.Resources
{
    public class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException(string message) : base(message)
        {
        }
    }
    
    public class InvalidResourceException : Exception
    {
        public InvalidResourceException(string message, Exception e) : base(message, e)
        {
        }
    }
}