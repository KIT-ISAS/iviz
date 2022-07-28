using System.Runtime.InteropServices;
using Iviz.Tools;

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

    public bool WaitFor(
        ReadOnlySpan<IntPtr> subscriptions, ReadOnlySpan<IntPtr> guards,
        out ReadOnlySpan<IntPtr> triggeredSubscriptions, out ReadOnlySpan<IntPtr> triggeredGuards)
    {
        if (disposed)
        {
            throw new ObjectDisposedException(ToString());
        }

        ref readonly IntPtr subscriptionHandles = ref MemoryMarshal.GetReference(subscriptions);
        ref readonly IntPtr guardHandles = ref MemoryMarshal.GetReference(guards);

        Check(Rcl.WaitClearAndAdd(waitSetHandle,
            in subscriptionHandles, subscriptions.Length,
            in guardHandles, guards.Length));

        int ret = Rcl.Wait(waitSetHandle, 5000, 
            out var changedSubscriptionHandles, 
            out var changedGuardHandles);

        switch ((RclRet)ret)
        {
            case RclRet.Timeout:
                triggeredSubscriptions = default;
                triggeredGuards = default;
                return false;
            case RclRet.Ok:
                triggeredSubscriptions = Rcl.CreateSpan<IntPtr>(changedSubscriptionHandles, subscriptions.Length);
                triggeredGuards = Rcl.CreateSpan<IntPtr>(changedGuardHandles, guards.Length);
                return true;
            default:
                Check(ret); // throws
                triggeredSubscriptions = default;
                triggeredGuards = default;
                return false;
        }
    }

    void Check(int result) => Rcl.Check(contextHandle, result);

    public void Dispose()
    {
        if (disposed) return;
        disposed = true;
        GC.SuppressFinalize(this);
        Rcl.DestroyWaitSet(waitSetHandle);
    }

    public override string ToString() => $"[{nameof(RclWaitSet)}]";

    ~RclWaitSet() => Logger.LogErrorFormat("{0} has not been disposed!", this);
}