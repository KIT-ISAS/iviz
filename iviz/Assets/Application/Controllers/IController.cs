
namespace Iviz.Controllers
{
    public interface IModuleData
    {
        void ResetPanel();
        void ShowPanel();
    }
    
    public interface IController
    {
        IModuleData ModuleData { get; }
        void Stop();
        void Reset();
    }
}
