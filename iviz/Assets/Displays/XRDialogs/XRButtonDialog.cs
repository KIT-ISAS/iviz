#nullable enable

using System;
using Iviz.Core;
using UnityEngine;

namespace Iviz.Displays.XR
{
    public sealed class XRButtonDialog : XRDialog, IDialogWithCaption, IDialogWithIcon, IDialogCanBeClicked,
        IDialogIsInteractable
    {
        [SerializeField] XRButton? button;

        XRButton Button => button.AssertNotNull(nameof(button));

        public event Action<int>? Clicked;

        public string Caption
        {
            set
            {
                Button.Caption = value;
                UpdateSize();
            }
        }

        public XRIcon Icon
        {
            set => Button.Icon = value;
        }

        public bool Interactable
        {
            set => Button.Interactable = value;
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
        }

        void UpdateSize()
        {
            var (center, size) = GetButtonBounds(Button);

            const float padding = 0.05f;
            Background.Transform.localPosition = center;
            Background.Size = size + padding * Vector2.one;
            SocketPosition = new Vector3(0, center.y - size.y / 2 - padding, 0);
        }
    }
}