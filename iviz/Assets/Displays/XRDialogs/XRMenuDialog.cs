#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using Iviz.Core;
using Iviz.Tools;
using UnityEngine;
using TMPro;

namespace Iviz.Displays.XR
{
    public sealed class XRMenuDialog : XRDialog, IDialogWithCaption, IDialogWithTitle,
        IDialogCanBeMenuClicked, IDialogCanBeClicked, IDialogWithEntries, IIsInteractable
    {
        [SerializeField] XRButton[] menuButtons = Array.Empty<XRButton>();
        [SerializeField] XRButton? upButton;
        [SerializeField] XRButton? downButton;
        [SerializeField] XRButton? closeButton;
        [SerializeField] TMP_Text? titleObject;
        [SerializeField] TMP_Text? captionObject;

        XRButton[] Buttons => menuButtons;
        XRButton UpButton => upButton.AssertNotNull(nameof(upButton));
        XRButton DownButton => downButton.AssertNotNull(nameof(downButton));
        XRButton CloseButton => closeButton.AssertNotNull(nameof(closeButton));
        TMP_Text TitleObject => titleObject.AssertNotNull(nameof(titleObject));
        TMP_Text CaptionObject => captionObject.AssertNotNull(nameof(captionObject));

        int menuPage;
        string[] menuEntries = Array.Empty<string>();

        public event Action<int>? Clicked;
        public event Action<int>? MenuClicked;

        public string Caption
        {
            set => CaptionObject.text = value;
        }

        public string Title
        {
            set => TitleObject.text = value;
        }

        int MenuPage
        {
            get => menuPage;
            set
            {
                menuPage = value;
                int offset = menuPage * Buttons.Length;
                int numActives = Mathf.Min(menuEntries.Length - offset, Buttons.Length);
                for (int i = 0; i < numActives; i++)
                {
                    Buttons[i].Caption = menuEntries[i + offset];
                    Buttons[i].Visible = true;
                }

                foreach (var menuButton in Buttons.Skip(numActives))
                {
                    menuButton.Visible = false;
                }

                UpButton.Visible = menuPage > 0;
                DownButton.Visible = offset + Buttons.Length < menuEntries.Length;
            }
        }

        public IEnumerable<string> Entries
        {
            set
            {
                menuEntries = value as string[] ?? value.ToArray();
                MenuPage = 0;
            }
        }
        
        public bool Interactable
        {
            set
            {
                foreach (var button in Buttons)
                {
                    button.Interactable = value;
                }

                UpButton.Interactable = value;
                DownButton.Interactable = value;
                CloseButton.Interactable = value;
            }
        }

        protected override void Awake()
        {
            base.Awake();
            CloseButton.Clicked += () => Clicked?.Invoke(0);
            UpButton.Clicked += OnScrollUpClick;
            DownButton.Clicked += OnScrollDownClick;
            for (int i = 0; i < Buttons.Length; i++)
            {
                int j = i;
                Buttons[i].Clicked += () => MenuClicked?.Invoke(j);
            }
        }

        public override void Suspend()
        {
            base.Suspend();
            MenuClicked = null;
        }

        void OnScrollUpClick()
        {
            if (MenuPage > 0)
            {
                MenuPage--;
            }
        }

        void OnScrollDownClick()
        {
            if ((MenuPage + 1) * Buttons.Length < menuEntries.Length)
            {
                MenuPage++;
            }
        }
    }
}