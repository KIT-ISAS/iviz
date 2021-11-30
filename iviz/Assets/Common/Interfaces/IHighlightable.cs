namespace Iviz.Common
{
    public interface IHighlightable
    {
        void Highlight();
        bool IsAlive { get; }
    }
}