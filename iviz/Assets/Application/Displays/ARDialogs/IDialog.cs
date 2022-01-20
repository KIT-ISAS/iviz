#nullable enable

using System;
using Iviz.Displays;
using Iviz.Displays.XRDialogs;
using Iviz.Msgs.IvizMsgs;
using UnityEngine;

namespace Iviz.Displays.XRDialogs
{
    public interface IDialog : IDisplay
    {
        event Action? Expired;
        float Scale { set; }
        Color Color { set; }
        Vector3 DialogDisplacement { set; }
        Vector3 PivotFrameOffset { set; }
        Vector3 PivotDisplacement { set; }
        string PivotFrameId { set; }
        void Initialize();
    }
    
    public interface IDialogWithTitle
    {
        string Title { set; }
    }

    public interface IDialogWithAlignment
    {
        CaptionAlignmentType CaptionAlignment { set; }
    }
    
    public interface IDialogWithCaption
    {
        string Caption { set; }
    }

    public interface IDialogWithIcon
    {
        XRIcon Icon { set; }
    }    
    
    public interface IDialogHasButtonSetup
    {
        XRButtonSetup ButtonSetup { set; }
    }  
    
    public interface IDialogCanBeClicked
    {
        event Action<int>? Clicked;
    }  
    
    [Flags]
    public enum CaptionAlignmentType : ushort
    {
        Default = 0x0,
        
        Left = 0x1,
        Center = 0x2,
        Right = 0x4,
        Justified = 0x8,
        Flush = 0x10,
        GeometryCenter = 0x20,
            
        Top = 0x100,
        Mid = 0x200,
        Bottom = 0x400,
    } 
    
    public enum XRButtonSetup : byte
    {
        Ok = Dialog.BUTTONS_OK,
        YesNo = Dialog.BUTTONS_YESNO,
        OkCancel = Dialog.BUTTONS_OKCANCEL,
        Forward = Dialog.BUTTONS_FORWARD,
        ForwardBackward = Dialog.BUTTONS_FORWARDBACKWARD,
        Backward = Dialog.BUTTONS_BACKWARD,
    }
    
    public enum IconType : byte
    {
        None,
        Info,
        Warn,
        Error,
        Dialog,
        Dialogs,
        Question,
    }    
    
    public enum DialogType : byte
    {
        Plain = Dialog.TYPE_PLAIN,
        Short = Dialog.TYPE_SHORT,
        Notice = Dialog.TYPE_NOTICE,
        Menu = Dialog.TYPE_MENU,
        Button = Dialog.TYPE_BUTTON,
        Icon = Dialog.TYPE_ICON
    }
}