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
        Rcl.TriggerGuard(guardHandle);
    }

    public void AddHandle(out IntPtr handle)
    {
        handle = guardHandle;
    }
    
    public void Dispose()
    {
        if (disposed) return;
        disposed = true;
        GC.SuppressFinalize(this);
        Rcl.DestroyGuard(guardHandle);
    }

    ~RclGuardCondition() => Dispose();
}