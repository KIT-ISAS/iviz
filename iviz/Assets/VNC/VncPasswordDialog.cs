#nullable enable

using System;
using Iviz.Core;
using TMPro;
using UnityEngine;

namespace Iviz.Displays.XR
{
    public sealed class VncPasswordDialog : XRDialog, IDialogIsInteractable
    {
        [SerializeField] TMP_Text? title;
        [SerializeField] TMP_InputField? password;
        [SerializeField] XRButton? button1;
        [SerializeField] XRButton? button2;
        [SerializeField] XRButton? button3;

        XRButton Button1 => button1.AssertNotNull(nameof(button1));
        XRButton Button2 => button2.AssertNotNull(nameof(button2));
        XRButton Button3 => button3.AssertNotNull(nameof(button3));
        
        TMP_InputField PasswordObject => password.AssertNotNull(nameof(password));

        public event Action<int>? Clicked;
        
        public string Password
        {
            get => PasswordObject.text;
            set => PasswordObject.text = value;
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

        ButtonSetup ButtonSetup
        {
            set => SetupButtons(Button1, Button2, Button3, value);
        }

        protected override void Awake()
        {
            base.Awake();

            PasswordObject.onSubmit.AddListener(_ => Clicked?.Invoke(0));

            // button 1 only appears alone
            Button1.Clicked += () => Clicked?.Invoke(0);
            
            // button 2 and 3 only appear in pairs
            Button2.Clicked += () => Clicked?.Invoke(0);
            Button3.Clicked += () => Clicked?.Invoke(1);

            ButtonSetup = ButtonSetup.Forward;
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