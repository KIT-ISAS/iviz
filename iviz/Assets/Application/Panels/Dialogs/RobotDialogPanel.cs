#nullable enable

using System;
using Iviz.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public sealed class RobotDialogPanel : DetachableDialogPanel
    {
        [SerializeField] SimpleButtonWidget? close;
        [SerializeField] Button? reset;
        [SerializeField] DataLabelWidget? label;
        [SerializeField] TMP_Text? text;
        [SerializeField] LinkResolver? linkResolver;

        Button Reset => reset.AssertNotNull(nameof(reset));
        LinkResolver LinkResolver => linkResolver.AssertNotNull(nameof(linkResolver));
        public SimpleButtonWidget Close => close.AssertNotNull(nameof(close));
        public DataLabelWidget Label => label.AssertNotNull(nameof(label));
        public TMP_Text Text => text.AssertNotNull(nameof(text));
        
        public event Action? ResetAll;
        public event Action<string>? LinkClicked;

        void Awake()
        {
            Reset.onClick.AddListener(() => ResetAll.TryRaise(this));
            LinkResolver.LinkClicked += s => LinkClicked.TryRaise(s, this);
        }
        
        public override void ClearSubscribers()
        {
            Close.ClearSubscribers();
            ResetAll = null;
            LinkClicked = null;
        }
    }
}
