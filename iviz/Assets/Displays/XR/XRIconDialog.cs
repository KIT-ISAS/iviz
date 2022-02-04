#nullable enable

using Iviz.Core;
using TMPro;
using UnityEngine;

namespace Iviz.Displays.XR
{
    public sealed class XRIconDialog : XRDialog, 
        IDialogWithTitle, IDialogWithCaption, IDialogWithAlignment, IDialogWithIcon, IDialogHasButtonSetup
    {
        [SerializeField] TMP_Text? title;
        [SerializeField] TMP_Text? caption;
        [SerializeField] XRIconPlane? iconObject;
        [SerializeField] XRButton? button1;
        [SerializeField] XRButton? button2;
        [SerializeField] XRButton? button3;
        
        TMP_Text TitleObject => title.AssertNotNull(nameof(title));
        TMP_Text CaptionObject => caption.AssertNotNull(nameof(caption));
        XRIconPlane IconObject => iconObject.AssertNotNull(nameof(iconObject));

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

        public XRIcon Icon
        {
            set => IconObject.Icon = value;
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