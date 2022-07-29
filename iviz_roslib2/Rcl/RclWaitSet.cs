using System.Runtime.InteropServices;
using Iviz.Tools;

namespace Iviz.Roslib2.Rcl;

internal sealed class RclWaitSet : IDisposable
{
    readonly IntPtr contextHandle;
    readonly IntPtr waitSetHandle;
    bool disposed;

    public RclWaitSet(IntPtr contextHandle, int maxSubscriptions, int maxGuardConditions, int maxClients,
        int maxServers)
    {
        this.contextHandle = contextHandle;
        waitSetHandle = Rcl.CreateWaitSet();
        Check(Rcl.WaitSetInit(contextHandle, waitSetHandle, maxSubscriptions, maxGuardConditions, 0, 
            maxClients, maxServers, 0));
    }

    public bool WaitFor(
        Span<IntPtr> subscriptions, Span<IntPtr> guards,
        Span<IntPtr> clients, Span<IntPtr> services,
        out Span<long> triggeredSubscriptions, out Span<long> triggeredGuards,
        out Span<long> triggeredClients, out Span<long> triggeredServices)
    {
        if (disposed)
        {
            throw new ObjectDisposedException(ToString());
        }

        triggeredClients = default;
        triggeredServices = default;

        ref readonly IntPtr subscriptionHandles = ref MemoryMarshal.GetReference(subscriptions);
        ref readonly IntPtr guardHandles = ref MemoryMarshal.GetReference(guards);
        ref readonly IntPtr clientHandles = ref MemoryMarshal.GetReference(clients);
        ref readonly IntPtr servicesHandles = ref MemoryMarshal.GetReference(services);

        Check(Rcl.WaitClearAndAdd(waitSetHandle,
            in subscriptionHandles, subscriptions.Length,
            in guardHandles, guards.Length,
            in clientHandles, clients.Length,
            in servicesHandles, services.Length));

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
                triggeredSubscriptions = Rcl.CreateSpan<long>(changedSubscriptionHandles, subscriptions.Length);
                triggeredGuards = Rcl.CreateSpan<long>(changedGuardHandles, guards.Length);
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