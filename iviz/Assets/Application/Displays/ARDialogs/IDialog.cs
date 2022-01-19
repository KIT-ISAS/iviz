#nullable enable

using System;
using Iviz.Displays;
using Iviz.Displays.XRDialogs;
using UnityEngine;

namespace Iviz.App.ARDialogs
{
    public interface IDialog : IDisplay
    {
        event Action? Expired;
        float Scale { set; }
        Color Color { set; }
        Vector3 DialogDisplacement { set; }
        Vector3 PivotFrameOffset { set; }
        Vector3 PivotDisplacement { set; }
        string? PivotFrameId { set; }
        void Initialize();
    }
    
    public interface IDialogWithCaption
    {
        string Caption { set; }
    }

    public interface IDialogWithIcon
    {
        XRButtonIcon Icon { set; }
    }    
    
    public interface IDialogCanBeClicked
    {
        event Action<int>? Clicked;
    }  
}