#nullable enable

#if false

using System;
using Iviz.Core;
using TMPro;
using UnityEngine;

namespace Iviz.Displays.XR
{
    public sealed class VncConnectionDialog : XRDialog, IIsInteractable
    {
        [SerializeField] TMP_Text? title;
        [SerializeField] TMP_InputField? hostname;
        [SerializeField] TMP_InputField? port;
        [SerializeField] XRButton? button1;
        [SerializeField] XRButton? button2;
        [SerializeField] XRButton? button3;

        XRButton Button1 => button1.AssertNotNull(nameof(button1));
        XRButton Button2 => button2.AssertNotNull(nameof(button2));
        XRButton Button3 => button3.AssertNotNull(nameof(button3));
        
        TMP_Text TitleObject => title.AssertNotNull(nameof(title));
        TMP_InputField HostnameObject => hostname.AssertNotNull(nameof(hostname));
        TMP_InputField PortObject => port.AssertNotNull(nameof(port));

        public event Action<int>? Clicked;
        
        public string Port
        {
            get => PortObject.text;
            set => PortObject.text = value;
        }

        public string Hostname
        {
            get => HostnameObject.text;
            set => HostnameObject.text = value;
        }

        ButtonSetup ButtonSetup
        {
            set => SetupButtons(Button1, Button2, Button3, value);
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

            HostnameObject.onSubmit.AddListener(_ => PortObject.Select());
            PortObject.onSubmit.AddListener(_ => Clicked?.Invoke(0));
            
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
#endif