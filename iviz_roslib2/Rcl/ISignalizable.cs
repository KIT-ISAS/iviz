namespace Iviz.Roslib2.Rcl;

internal interface ISignalizable
{
    void Signal();
}

internal interface IHasHandle
{
    IntPtr Handle { get; }
}