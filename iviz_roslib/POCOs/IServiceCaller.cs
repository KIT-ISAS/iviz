namespace Iviz.Roslib
{
    internal interface IServiceCaller
    {
        string ServiceType { get; }
        void Stop();
    }
}