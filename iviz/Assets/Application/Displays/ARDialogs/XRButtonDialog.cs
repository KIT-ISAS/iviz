#nullable enable

using System;
using Iviz.Core;
using Iviz.Displays.XRDialogs;
using TMPro;
using UnityEngine;

namespace Iviz.App.ARDialogs
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

        public XRButtonIcon Icon
        {
            set => Button.Icon = value;
        }

        void Awake()
        {
            Button.Clicked += () => Clicked?.Invoke(0);
        }

        public override void Suspend()
        {
            base.Suspend();
            Clicked = null;
        }
    }
}