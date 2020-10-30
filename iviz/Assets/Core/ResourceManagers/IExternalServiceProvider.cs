using Iviz.Msgs;
using JetBrains.Annotations;

namespace Iviz.Displays
{
    public interface IExternalServiceProvider
    {
        bool CallService<T>([NotNull] string service, [NotNull] T srv) where T : IService;
    }
}