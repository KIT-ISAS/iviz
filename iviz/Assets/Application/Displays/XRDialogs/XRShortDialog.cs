#nullable enable

using Iviz.Core;
using Iviz.Msgs.IvizCommonMsgs;
using TMPro;
using UnityEngine;

namespace Iviz.Displays.XRDialogs
{
    public sealed class XRShortDialog : XRDialog, IDialogWithCaption, IDialogWithTitle, IDialogWithAlignment
    {
        [SerializeField] TMP_Text? title;
        [SerializeField] TMP_Text? caption;

        TMP_Text TitleObject => title.AssertNotNull(nameof(title));
        TMP_Text CaptionObject => caption.AssertNotNull(nameof(caption));

        public string Caption
        {
            set => CaptionObject.text = value;
        }

        public string Title
        {
            set => TitleObject.text = value;
        }

        public CaptionAlignmentType CaptionAlignment
        {
            set => CaptionObject.alignment = (TextAlignmentOptions)value;
        }
    }
}