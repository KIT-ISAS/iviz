
namespace Iviz.App.Listeners
{
    public interface IController
    {
        ModuleData ModuleData { get; set; }
        void Stop();
    }
}
