using Iviz.Msgs;

namespace Iviz.Displays
{
    public interface IExternalServiceProvider
    {
        bool CallService<T>(string service, T srv) where T : IService;
    }
}