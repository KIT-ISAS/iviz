using System;

namespace Iviz.MarkerDetection
{
    public class CvMarkerException : Exception
    {
        public CvMarkerException() : base("An error happened in the native call")
        {
        }

        protected CvMarkerException(string message, Exception e) : base(message, e)
        {
        }
    }
    
    public sealed class CvNotAvailableException : CvMarkerException
    {
        public CvNotAvailableException(Exception e) : base("The OpenCV library is not available", e)
        {
        }
    }

}