using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
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
        ButtonOk,
        ButtonYesNo,
        ButtonOkCancel,
        ButtonForward,
        ButtonForwardBackward,

        Short,
        Notice,
        MenuMode,
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
        BindNone,
        BindToTf,
        BindToUser,
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

    [DataContract]
    public sealed class GuiDialog : RosMessageWrapper<GuiDialog>
    {
        [Preserve, MessageName] public const string RosMessageType = "iviz_msgs/GuiDialog";

        [DataMember] public Header Header { get; set; }

        [DataMember] public ActionType Action { get; set; }
        [DataMember, NotNull] public string Id { get; set; } = "";

        [DataMember] public duration Lifetime { get; set; }
        [DataMember] public float Scale { get; set; } = 1;

        [DataMember] public DialogType Type { get; set; }
        [DataMember] public IconType Icon { get; set; }
        [DataMember] public ColorRGBA BackgroundColor { get; set; }

        [DataMember, NotNull] public string Title { get; set; } = "";
        [DataMember, NotNull] public string Caption { get; set; } = "";
        [DataMember] public CaptionAlignmentType CaptionAlignment { get; set; }
        [DataMember, NotNull] public string[] MenuEntries { get; set; } = Array.Empty<string>();


        [DataMember] public BindingType BindingType { get; set; }

        [DataMember] public Vector3f TfOffset { get; set; }
        [DataMember] public Vector3f DialogDisplacement { get; set; }
        [DataMember] public Vector3f TfDisplacement { get; set; }
    }
}