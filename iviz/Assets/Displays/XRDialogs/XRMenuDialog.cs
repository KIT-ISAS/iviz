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
    public sealed class XRMenuDialog : XRDialog, IDialogWithCaption, IDialogWithTitle, IDialogCanBeMenuClicked,
        IDialogWithEntries
    {
        [SerializeField] XRButton[]? menuButtons;
        [SerializeField] XRButton? upButton;
        [SerializeField] XRButton? downButton;
        [SerializeField] TMP_Text? titleObject;
        [SerializeField] TMP_Text? captionObject;

        XRButton[] Buttons => menuButtons.AssertNotNull(nameof(menuButtons));
        XRButton UpButton => upButton.AssertNotNull(nameof(upButton));
        XRButton DownButton => downButton.AssertNotNull(nameof(downButton));
        TMP_Text TitleObject => titleObject.AssertNotNull(nameof(titleObject));
        TMP_Text CaptionObject => captionObject.AssertNotNull(nameof(captionObject));

        int menuPage;
        string[] menuEntries = Array.Empty<string>();

        public event Action<int>? MenuClicked;

        public string Caption
        {
            set => CaptionObject.text = value;
        }

        public string Title
        {
            set => TitleObject.text = value;
        }

        protected override void Awake()
        {
            base.Awake();
            foreach (int i in ..Buttons.Length)
            {
                Buttons[i].Clicked += () => MenuClicked?.Invoke(i);
            }
        }

        public override void Suspend()
        {
            base.Suspend();
            MenuClicked = null;
            //Button.ResetHighlights();
        }

        int MenuPage
        {
            set
            {
                menuPage = value;
                int offset = menuPage * Buttons.Length;
                int numActives = Mathf.Min(menuEntries.Length - offset, Buttons.Length);
                foreach (int i in ..numActives)
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
                menuEntries = value.ToArray();
                MenuPage = 0;
            }
        }
    }
}