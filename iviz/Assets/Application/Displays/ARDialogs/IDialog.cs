using System;
using UnityEngine;

namespace Iviz.App.ARDialogs
{
    public interface IDialog
    {
        event Action<IDialog>? Expired;
        Color Color { set; }
        Vector3 DialogDisplacement { set; }
        Vector3 PivotFrameOffset { set; }
        Vector3 PivotDisplacement { set; }
        string? PivotFrameId { set; }
        void Initialize();
    }
}