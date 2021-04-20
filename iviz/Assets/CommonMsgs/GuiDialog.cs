using System.Runtime.InteropServices;
using Iviz.Msgs.IvizMsgs;
using Iviz.Msgs.StdMsgs;
using Iviz.MsgsWrapper;

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
        ModeMenu,
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

    public sealed class GuiDialog : RosMessageWrapper<GuiDialog>
    {
        [Preserve, MessageName] public const string RosMessageType = "iviz_msgs/GuiDialog";

        public Header Header { get; set; }

        public ActionType Action { get; set; }
        public string Id { get; set; } = "";

        public duration Lifetime { get; set; }
        public float Scale { get; set; } = 1;

        public DialogType Type { get; set; }
        public IconType Icon { get; set; }

        public string Title { get; set; } = "";
        public string Caption { get; set; } = "";
        
        public BindingType BindingType { get; set; }
        
        public Vector3f TfOffset { get; set; }
        public Vector3f DialogDisplacement { get; set; }
        public Vector3f TfDisplacement { get; set; }
    }
}