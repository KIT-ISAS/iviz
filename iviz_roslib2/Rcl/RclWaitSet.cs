using System.Runtime.InteropServices;

namespace Iviz.Roslib2.Rcl;

internal sealed class RclWaitSet : IDisposable
{
    readonly IntPtr contextHandle;
    readonly IntPtr waitSetHandle;
    bool disposed;

    public RclWaitSet(IntPtr contextHandle, int maxSubscriptions, int maxGuardConditions)
    {
        this.contextHandle = contextHandle;
        waitSetHandle = Rcl.CreateWaitSet();
        Check(Rcl.WaitSetInit(contextHandle, waitSetHandle, maxSubscriptions, maxGuardConditions, 0, 0, 0, 0));
    }

    public void WaitFor(
        ReadOnlySpan<IntPtr> subscriptions, ReadOnlySpan<IntPtr> guards,
        out ReadOnlySpan<IntPtr> triggeredSubscriptions, out ReadOnlySpan<IntPtr> triggeredGuards)
    {
        IntPtr dummy = IntPtr.Zero;
        
        ref readonly IntPtr subscriptionHandles = ref (subscriptions.Length != 0 ? ref subscriptions[0] : ref dummy);
        ref readonly IntPtr guardHandles = ref (guards.Length != 0 ? ref guards[0] : ref dummy);
        
        Check(Rcl.WaitClearAndAdd(waitSetHandle, 
            in subscriptionHandles, subscriptions.Length,
            in guardHandles, guards.Length));
        Check(Rcl.Wait(waitSetHandle, 5000, out IntPtr changedSubscriptionHandles, out IntPtr changedGuardHandles));

        triggeredSubscriptions =
            MemoryMarshal.CreateReadOnlySpan(ref Rcl.GetArrayRef<IntPtr>(changedSubscriptionHandles),
                subscriptions.Length);
        triggeredGuards =
            MemoryMarshal.CreateReadOnlySpan(ref Rcl.GetArrayRef<IntPtr>(changedGuardHandles), guards.Length);
    }

    void Check(int result) => Rcl.Check(contextHandle, result);

    public void Dispose()
    {
        if (disposed) return;
        disposed = true;
        GC.SuppressFinalize(this);
        Rcl.DestroyWaitSet(waitSetHandle);
    }

    ~RclWaitSet() => Dispose();
}