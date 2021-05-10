using System;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Msgs;
using JetBrains.Annotations;

namespace Iviz.Displays
{
    public interface IExternalServiceProvider
    {
        ValueTask<bool> CallServiceAsync<T>([NotNull] string service, [NotNull] T srv, int timeoutInMs,
            CancellationToken token) where T : IService;
    }
}