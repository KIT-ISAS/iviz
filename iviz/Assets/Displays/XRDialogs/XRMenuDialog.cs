#nullable enable

using System;
using Iviz.Core;
using UnityEngine;

namespace Iviz.Displays.XR
{
    public sealed class XRMenuDialog : XRDialog, IDialogWithCaption, IDialogWithTitle, IDialogCanBeClicked
    {
        [SerializeField] XRButton? button;

        XRButton Button => button.AssertNotNull(nameof(button));

        public event Action<int>? Clicked;
        
        public string Caption
        {
            set => Button.Caption = value;
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