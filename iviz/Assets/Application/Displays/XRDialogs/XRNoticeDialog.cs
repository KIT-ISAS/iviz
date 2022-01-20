#nullable enable

using System;
using Iviz.Core;
using Iviz.Displays.XRDialogs;
using Iviz.Msgs.IvizCommonMsgs;
using TMPro;
using UnityEngine;

namespace Iviz.Displays.XRDialogs
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