#nullable enable

using Iviz.Core;
using TMPro;
using UnityEngine;

namespace Iviz.Displays.XR
{
    public sealed class XRNoticeDialog : XRDialog, IDialogWithCaption, IDialogWithAlignment, IDialogWithIcon
    {
        [SerializeField] TMP_Text? caption;
        [SerializeField] XRIconPlane? iconObject;
        
        TMP_Text CaptionObject => caption.AssertNotNull(nameof(caption));
        XRIconPlane IconObject => iconObject.AssertNotNull(nameof(iconObject));

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
    }
}