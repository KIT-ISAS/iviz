#nullable enable

using System;
using Iviz.Core;
using TMPro;
using UnityEngine;

namespace Iviz.Displays.XR
{
    public sealed class XRIconDialog : XRDialog,
        IDialogWithTitle, IDialogWithCaption, IDialogWithAlignment, IDialogWithIcon, IDialogWithButtonSetup, 
        IDialogCanBeClicked
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
        XRButton Button1 => button1.AssertNotNull(nameof(button1));
        XRButton Button2 => button2.AssertNotNull(nameof(button2));
        XRButton Button3 => button3.AssertNotNull(nameof(button3));

        public event Action<int>? Clicked;

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

        public override bool Interactable
        {
            set
            {
                Button1.Interactable = value;
                Button2.Interactable = value;
                Button3.Interactable = value;
            }
        }

        protected override void Awake()
        {
            base.Awake();
            // button 1 only appears alone
            Button1.Clicked += () => Clicked?.Invoke(0);

            // button 2 and 3 only appear in pairs
            Button2.Clicked += () => Clicked?.Invoke(0);
            Button3.Clicked += () => Clicked?.Invoke(1);
        }
        
        public ButtonSetup ButtonSetup
        {
            set => SetupButtons(Button1, Button2, Button3, value);
        }
    }
}