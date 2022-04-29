#nullable enable

using System;
using Iviz.Core;
using TMPro;
using UnityEngine;

namespace Iviz.Displays.XR
{
    public sealed class XRPlainDialog : XRDialog,
        IDialogWithTitle, IDialogWithCaption, IDialogWithAlignment, IDialogWithButtonSetup, IDialogCanBeClicked
    {
        [SerializeField] TMP_Text? title;
        [SerializeField] TMP_Text? caption;
        [SerializeField] XRButton? button1;
        [SerializeField] XRButton? button2;
        [SerializeField] XRButton? button3;

        XRButton Button1 => button1.AssertNotNull(nameof(button1));
        XRButton Button2 => button2.AssertNotNull(nameof(button2));
        XRButton Button3 => button3.AssertNotNull(nameof(button3));
        
        TMP_Text TitleObject => title.AssertNotNull(nameof(title));
        TMP_Text CaptionObject => caption.AssertNotNull(nameof(caption));

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

        public ButtonSetup ButtonSetup
        {
            set => SetupButtons(Button1, Button2, Button3, value);
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

        public override void Suspend()
        {
            base.Suspend();
            Clicked = null;
            //Button1.ResetHighlights();
            //Button2.ResetHighlights();
            //Button3.ResetHighlights();
        }
    }
}