namespace Iviz.Roslib2.RclInterop;

public class Signalizable
{
    protected readonly SemaphoreSlim signal = new(0);
    internal void Signal() => signal.Release();
}

internal interface IHasHandle
{
    IntPtr Handle { get; }
}