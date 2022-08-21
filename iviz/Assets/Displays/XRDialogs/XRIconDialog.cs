#nullable enable

using System;
using Iviz.Core;
using TMPro;
using UnityEngine;

namespace Iviz.Displays.XR
{
    public sealed class XRIconDialog : XRDialog,
        IDialogWithTitle, IDialogWithCaption, IDialogWithIcon, IDialogWithButtonSetup,
        IDialogCanBeClicked, IIsInteractable
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

        public XRIcon Icon
        {
            set => IconObject.Icon = value;
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

            Button1.BackgroundVisible = false;
            Button2.BackgroundVisible = false;
            Button3.BackgroundVisible = false;

            // button 1 only appears alone
            Button1.Clicked += () => Clicked?.Invoke(0);

            // button 2 and 3 only appear in pairs
            Button2.Clicked += () => Clicked?.Invoke(0);
            Button3.Clicked += () => Clicked?.Invoke(1);
        }

        public ButtonSetup ButtonSetup
        {
            set
            {
                SetupButtons(Button1, Button2, Button3, value);
                UpdateSize();
            }
        }

        void UpdateSize()
        {
            Span<Rect> upperBounds = stackalloc[]
            {
                GetTitleBoundsLeft(TitleObject),
                GetCaptionBoundsLeft(CaptionObject),
                GetIconBounds(IconObject)
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
            SocketPosition = new Vector3(center.x, center.y - size.y / 2 - padding, 0);
        }
    }
}