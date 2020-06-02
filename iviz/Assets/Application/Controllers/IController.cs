
namespace Iviz.App
{
    public interface IController
    {
        DisplayData DisplayData { get; set; }
        void Stop();
    }
}
