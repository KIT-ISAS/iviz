#nullable enable

using System.Text;

namespace Iviz.Common
{
    public interface IMarkerDialogListener
    {
        string Topic { get; }
        void GenerateLog(StringBuilder description);
        string BriefDescription { get; }
        void Reset();
    }
}