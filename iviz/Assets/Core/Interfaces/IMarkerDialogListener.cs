#nullable enable

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Iviz.Common
{
    public interface IMarkerDialogListener
    {
        string Title { get; }
        string Topic { get; }
        int NumEntriesForLog { get; }
        void GenerateLog(StringBuilder description, int minIndex, int numEntries);
        string BriefDescription { get; }
        void ResetController();
        void HighlightId(string id);
    }
}