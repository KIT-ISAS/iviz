#nullable enable

using System;
using Iviz.Core;
using UnityEngine;

namespace Iviz.Displays.XR
{
    public sealed class XRButtonDialog : XRDialog, IDialogWithCaption, IDialogWithIcon, IDialogCanBeClicked
    {
        [SerializeField] XRButton? button;

        XRButton Button => button.AssertNotNull(nameof(button));

        public event Action<int>? Clicked;
        
        public string Caption
        {
            set => Button.Caption = value;
        }

        public XRIcon Icon
        {
            set => Button.Icon = value;
        }

        protected override void Awake()
        {
            base.Awake();
            Button.Clicked += () => Clicked?.Invoke(0);
        }

        public override void Suspend()
        {
            base.Suspend();
            Clicked = null;
            //Button.ResetHighlights();
        }
    }
}