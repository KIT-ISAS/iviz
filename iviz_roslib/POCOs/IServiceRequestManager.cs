using System;
using System.Threading;
using System.Threading.Tasks;

namespace Iviz.Roslib
{
    internal interface IServiceRequestManager
    {
        string Service { get; }
        string ServiceType { get; }
        Uri Uri { get; }
        ValueTask DisposeAsync(CancellationToken token);
    }
}