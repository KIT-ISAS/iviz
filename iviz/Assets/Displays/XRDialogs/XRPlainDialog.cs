#nullable enable

using System;
using Iviz.Core;
using TMPro;
using UnityEngine;

namespace Iviz.Displays.XR
{
    public sealed class XRPlainDialog : XRDialog,
        IDialogWithTitle, IDialogWithCaption, IDialogWithButtonSetup, IDialogCanBeClicked,
        IIsInteractable
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

        public ButtonSetup ButtonSetup
        {
            set
            {
                SetupButtons(Button1, Button2, Button3, value);
                UpdateSize();
            }
        }

        public bool Interactable
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

        public override void Suspend()
        {
            base.Suspend();
            Clicked = null;
        }
        
        void UpdateSize()
        {
            Span<Rect> upperBounds = stackalloc[]
            {
                GetTitleBounds(TitleObject),
                GetCaptionBounds(CaptionObject),
            };

            var upperRect = upperBounds.Combine();

            const float buttonPadding = 0.05f;
            float upperRectMinY = upperRect.yMin - buttonPadding;
            float upperRectCenterX = upperRect.center.x;

            Span<Rect> buttonBounds = stackalloc[]
            {
                GetButtonBounds(Button1),
                GetButtonBounds(Button2),
                GetButtonBounds(Button3),
            };

            float buttonsHeight = float.MinValue;
            if (Button1.Visible) buttonsHeight = Mathf.Max(buttonsHeight, buttonBounds[0].height);
            if (Button2.Visible) buttonsHeight = Mathf.Max(buttonsHeight, buttonBounds[1].height);
            if (Button3.Visible) buttonsHeight = Mathf.Max(buttonsHeight, buttonBounds[2].height);
            buttonsHeight += buttonPadding;

            const float buttonDistance = 0.16f;
            Button1.Transform.localPosition +=
                new Vector3(upperRectCenterX - buttonBounds[0].center.x, upperRectMinY - buttonBounds[0].yMax);
            Button2.Transform.localPosition +=
                new Vector3(upperRectCenterX - buttonDistance - buttonBounds[1].center.x, upperRectMinY - buttonBounds[1].yMax);
            Button3.Transform.localPosition +=
                new Vector3(upperRectCenterX + buttonDistance - buttonBounds[2].center.x, upperRectMinY - buttonBounds[2].yMax);
 
            upperRect.y -= buttonsHeight;
            upperRect.height += buttonsHeight;

            const float padding = 0.1f;

            var (center, size) = upperRect;

            Background.Transform.localPosition = center; 
            Background.Size = size + Vector2.one * (2*padding);
            SocketPosition = new Vector3(0, center.y - size.y / 2 - padding, 0);
        }        
    }
}