#nullable enable

using System;
using Iviz.Core;
using Iviz.Displays.XRDialogs;
using Iviz.Msgs.IvizCommonMsgs;
using TMPro;
using UnityEngine;

namespace Iviz.Displays.XRDialogs
{
    public sealed class XRPlainDialog : XRDialog,
        IDialogWithTitle, IDialogWithCaption, IDialogWithAlignment, IDialogHasButtonSetup
    {
        [SerializeField] TMP_Text? title;
        [SerializeField] TMP_Text? caption;
        [SerializeField] XRButton? button1;
        [SerializeField] XRButton? button2;
        [SerializeField] XRButton? button3;

        TMP_Text TitleObject => title.AssertNotNull(nameof(title));
        TMP_Text CaptionObject => caption.AssertNotNull(nameof(caption));

        public string Title
        {
            set => TitleObject.text = value;
        }
        
        public string Caption
        {
            set => CaptionObject.text = value;
        }
        
        public CaptionAlignmentType CaptionAlignment
        {
            set => CaptionObject.alignment = (TextAlignmentOptions)value;
        }

        public XRButtonSetup ButtonSetup
        {
            set => SetupButtons(
                button1.AssertNotNull(nameof(button1)),
                button2.AssertNotNull(nameof(button2)),
                button3.AssertNotNull(nameof(button3)),
                value);
        }
    }
}