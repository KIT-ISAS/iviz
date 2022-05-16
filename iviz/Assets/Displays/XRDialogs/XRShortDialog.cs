#nullable enable

using Iviz.Core;
using TMPro;
using UnityEngine;

namespace Iviz.Displays.XR
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
        
        public override bool Interactable
        {
            set { } // no interactable components
        }        
    }
}