using Iviz.Roslib2.RclInterop.Wrappers;
using Iviz.Tools;

namespace Iviz.Roslib2.RclInterop;

internal sealed class RclGuardCondition : IDisposable
{
    readonly IntPtr guardHandle;
    bool disposed;
    
    internal IntPtr Handle => disposed 
        ? throw new ObjectDisposedException(ToString()) 
        : guardHandle;

    public RclGuardCondition(IntPtr contextHandle)
    {
        guardHandle = Rcl.Impl.CreateGuard(contextHandle);
    }

    public void Trigger()
    {
        Rcl.Impl.TriggerGuard(Handle);
    }

    public void Dispose()
    {
        if (disposed) return;
        disposed = true;
        GC.SuppressFinalize(this);
        Rcl.Impl.DestroyGuard(guardHandle);
    }

    public override string ToString() => $"[{nameof(RclGuardCondition)}]";
    
    ~RclGuardCondition() => Logger.LogErrorFormat("{0} has not been disposed!", this);
}