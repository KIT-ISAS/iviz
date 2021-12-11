using System;

namespace Iviz.Roslib;

internal interface IServiceCaller : IDisposable
{
    string ServiceType { get; }
}