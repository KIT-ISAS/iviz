using System;
using System.Threading.Tasks;

namespace Iviz.Roslib
{
    internal interface IServiceRequestManager : IDisposable
    {
        string Service { get; }
        string ServiceType { get; }
        Uri Uri { get; }
        Task DisposeAsync();
    }
}