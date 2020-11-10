using System;
using System.Threading.Tasks;

namespace Iviz.Roslib
{
    internal interface IServiceRequestManager
    {
        string Service { get; }
        string ServiceType { get; }
        Uri Uri { get; }
        void Stop();
        Task StopAsync();
    }
}