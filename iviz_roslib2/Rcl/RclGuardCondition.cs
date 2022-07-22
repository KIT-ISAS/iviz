using Iviz.Tools;

namespace Iviz.Roslib2.Rcl;

internal sealed class RclGuardCondition : IDisposable
{
    readonly IntPtr guardHandle;
    bool disposed;
    
    public RclGuardCondition(IntPtr contextHandle)
    {
        guardHandle = Rcl.CreateGuard(contextHandle);
    }

    public void Trigger()
    {
        if (disposed)
        {
            throw new ObjectDisposedException(ToString());
        }
        
        Rcl.TriggerGuard(guardHandle);
    }

    public void AddHandle(out IntPtr handle)
    {
        if (disposed)
        {
            throw new ObjectDisposedException(ToString());
        }

        handle = guardHandle;
    }
    
    public void Dispose()
    {
        if (disposed) return;
        disposed = true;
        GC.SuppressFinalize(this);
        Rcl.DestroyGuard(guardHandle);
    }

    public override string ToString() => $"[{nameof(RclGuardCondition)}]";
    
    ~RclGuardCondition() => Logger.LogErrorFormat("{0} has not been disposed!", this);
}