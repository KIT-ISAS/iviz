using Iviz.Tools;

namespace Iviz.Roslib2.RclInterop;

internal sealed class RclWaitSet : IDisposable
{
    readonly IntPtr contextHandle;
    readonly IntPtr waitSetHandle;

    readonly int maxSubscriptions;
    readonly int maxGuardConditions;
    readonly int maxClients;
    readonly int maxServers;
    
    bool disposed;

    public RclWaitSet(IntPtr contextHandle, int maxSubscriptions, int maxGuardConditions, int maxClients,
        int maxServers)
    {
        this.contextHandle = contextHandle;
        this.maxSubscriptions = maxSubscriptions;
        this.maxGuardConditions = maxGuardConditions;
        this.maxClients = maxClients;
        this.maxServers = maxServers;
        waitSetHandle = Rcl.Impl.CreateWaitSet();
        Check(Rcl.Impl.WaitSetInit(contextHandle, waitSetHandle, maxSubscriptions, maxGuardConditions, 0, 
            maxClients, maxServers, 0));
    }

    public bool WaitFor(
        IntPtr[] subscriptions, IntPtr[] guards,
        IntPtr[] clients, IntPtr[] servers,
        out Span<IntPtr> triggeredSubscriptions, out Span<IntPtr> triggeredGuards,
        out Span<IntPtr> triggeredClients, out Span<IntPtr> triggeredServers,
        int timeoutInMs)
    {
        if (disposed) ThrowObjectDisposed();

        if (subscriptions.Length > maxSubscriptions
            || guards.Length > maxGuardConditions
            || clients.Length > maxClients
            || servers.Length > maxServers)
        {
            ThrowHandlesOutOfRange();
        }

        IntPtr dummy = default;
        ref readonly IntPtr subscriptionHandles = ref (subscriptions.Length > 0 ? ref subscriptions[0] : ref dummy);
        ref readonly IntPtr guardHandles = ref (guards.Length > 0 ? ref guards[0] : ref dummy);
        ref readonly IntPtr clientHandles = ref (clients.Length > 0 ? ref clients[0] : ref dummy);
        ref readonly IntPtr serviceHandles = ref (servers.Length > 0 ? ref servers[0] : ref dummy);

        Check(Rcl.Impl.WaitClearAndAdd(waitSetHandle,
            in subscriptionHandles, subscriptions.Length,
            in guardHandles, guards.Length,
            in clientHandles, clients.Length,
            in serviceHandles, servers.Length));

        int ret = Rcl.Impl.Wait(waitSetHandle, timeoutInMs,
            out var changedSubscriptionHandles,
            out var changedGuardHandles,
            out var changedClientHandles,
            out var changedServiceHandles);

        switch ((RclRet)ret)
        {
            case RclRet.Timeout:
                triggeredSubscriptions = default;
                triggeredGuards = default;
                triggeredClients = default;
                triggeredServers = default;
                return false;
            case RclRet.Ok:
                triggeredSubscriptions = Rcl.CreateIntPtrSpan(changedSubscriptionHandles, subscriptions.Length);
                triggeredGuards = Rcl.CreateIntPtrSpan(changedGuardHandles, guards.Length);
                triggeredClients = Rcl.CreateIntPtrSpan(changedClientHandles, clients.Length);
                triggeredServers = Rcl.CreateIntPtrSpan(changedServiceHandles, servers.Length);
                return true;
            default: 
                Check(ret); // throws
                goto case RclRet.Timeout; // unreachable
        }
    }

    static void ThrowHandlesOutOfRange() => throw new IndexOutOfRangeException("Handle sizes are larger than allocated!");
    static void ThrowObjectDisposed() => throw new ObjectDisposedException(nameof(RclWaitSet));
    
    void Check(int result) => Rcl.Check(contextHandle, result);

    public void Dispose()
    {
        if (disposed) return;
        disposed = true;
        GC.SuppressFinalize(this);
        Rcl.Impl.DestroyWaitSet(waitSetHandle);
    }

    public override string ToString() => $"[{nameof(RclWaitSet)}]";

    ~RclWaitSet() => Logger.LogErrorFormat("{0} has not been disposed!", this);
}