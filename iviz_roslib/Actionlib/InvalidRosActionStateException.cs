namespace Iviz.Roslib.Actionlib;

public class InvalidRosActionStateException : RoslibException
{
    public InvalidRosActionStateException(string message) : base(message)
    {
    }
}