
namespace Iviz.App.Listeners
{
    public interface IController
    {
        ModuleData ModuleData { get; }
        void Stop();
        void Reset();
    }
}
