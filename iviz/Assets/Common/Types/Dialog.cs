using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Msgs.IvizMsgs;
using Iviz.Msgs.StdMsgs;
using Iviz.MsgsWrapper;
using JetBrains.Annotations;

namespace Iviz.Msgs.IvizCommonMsgs
{
    public enum DialogType : byte
    {
        Dialog,
        Short,
        Notice,
        MenuMode,
        Button,
    }

    public enum ButtonType : byte
    {
        Ok,
        YesNo,
        OkCancel,
        Forward,
        ForwardBackward,
        Backward,
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

    public enum BindingType : byte
    {
        BindToTf,
        BindToUser,
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
}