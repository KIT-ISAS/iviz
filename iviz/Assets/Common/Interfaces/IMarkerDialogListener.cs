#nullable enable

using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Iviz.Common
{
    public interface IMarkerDialogListener
    {
        string Topic { get; }
        void GenerateLog(StringBuilder description);
        string BriefDescription { get; }
        void Reset();
        bool TryGetMarkerFromId(string id, [NotNullWhen(true)] out IHasBounds? frame);
    }
}