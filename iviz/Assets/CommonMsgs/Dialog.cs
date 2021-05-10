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
    public enum ActionType : byte
    {
        Add,
        Remove,
        RemoveAll
    }

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

    [DataContract]
    public sealed class Dialog : RosMessageWrapper<Dialog>
    {
        [Preserve, MessageName] public const string RosMessageType = "iviz_msgs/Dialog";

        [DataMember] public Header Header { get; set; }

        [DataMember] public ActionType Action { get; set; }
        [DataMember, NotNull] public string Id { get; set; } = "";

        [DataMember] public duration Lifetime { get; set; }
        [DataMember] public double Scale { get; set; } = 1;

        [DataMember] public DialogType Type { get; set; }
        [DataMember] public ButtonType Buttons { get; set; }
        [DataMember] public IconType Icon { get; set; }
        [DataMember] public ColorRGBA BackgroundColor { get; set; }

        [DataMember, NotNull] public string Title { get; set; } = "";
        [DataMember, NotNull] public string Caption { get; set; } = "";
        [DataMember] public CaptionAlignmentType CaptionAlignment { get; set; }
        [DataMember, NotNull] public string[] MenuEntries { get; set; } = Array.Empty<string>();
        
        [DataMember] public BindingType BindingType { get; set; }

        [DataMember] public Vector3 TfOffset { get; set; }
        [DataMember] public Vector3 DialogDisplacement { get; set; }
        [DataMember] public Vector3 TfDisplacement { get; set; }
    }
}