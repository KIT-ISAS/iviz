#nullable enable

using System;
using System.Collections.Generic;
using Iviz.Msgs.IvizMsgs;
using UnityEngine;

namespace Iviz.Displays.XR
{
    public interface IDialog : IDisplay
    {
        BindingType BindingType { set; }
        event Action? Expired;
        float Scale { set; }
        Color Color { set; }
        Vector3 DialogDisplacement { set; }
        Vector3 TfFrameOffset { set; }
        Vector3 TfDisplacement { set; }
        string PivotFrameId { set; }
        void Initialize();
    }

    public interface IDialogIsInteractable
    {
        bool Interactable { set; }
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
    
    public interface IDialogWithButtonSetup
    {
        ButtonSetup ButtonSetup { set; }
    }  
    
    public interface IDialogCanBeClicked
    {
        event Action<int>? Clicked;
    }  
    
    public interface IDialogWithEntries
    {
        IEnumerable<string> Entries { set; }
    }
    
    public interface IDialogCanBeMenuClicked
    {
        event Action<int>? MenuClicked;
    }

    [Flags]
    public enum CaptionAlignmentType
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
    
    public enum ButtonSetup
    {
        Ok = Dialog.BUTTONS_OK,
        YesNo = Dialog.BUTTONS_YESNO,
        OkCancel = Dialog.BUTTONS_OKCANCEL,
        Forward = Dialog.BUTTONS_FORWARD,
        ForwardBackward = Dialog.BUTTONS_FORWARDBACKWARD,
        Backward = Dialog.BUTTONS_BACKWARD,
    }
    
    public enum IconType
    {
        None,
        Info,
        Warn,
        Error,
        Dialog,
        Dialogs,
        Question,
    }    
    
    public enum DialogType
    {
        Plain = Dialog.TYPE_PLAIN,
        Short = Dialog.TYPE_SHORT,
        Notice = Dialog.TYPE_NOTICE,
        Menu = Dialog.TYPE_MENU,
        Button = Dialog.TYPE_BUTTON,
        Icon = Dialog.TYPE_ICON
    }
    
    public enum BindingType
    {
        None,
        Tf,
        User
    }
}