#nullable enable

using System;
using Iviz.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public sealed class MarkerDialogContents : DetachablePanelContents
    {
        [SerializeField] TrashButtonWidget? close;
        [SerializeField] Button? reset;
        [SerializeField] DataLabelWidget? label;
        [SerializeField] TMP_Text? text;
        [SerializeField] LinkResolver? linkResolver;

        public TrashButtonWidget Close => close.AssertNotNull(nameof(close));
        public DataLabelWidget Label => label.AssertNotNull(nameof(label));
        public TMP_Text Text => text.AssertNotNull(nameof(text));
        LinkResolver LinkResolver => linkResolver.AssertNotNull(nameof(linkResolver));

        public event Action? ResetAll;
        public event Action<string>? LinkClicked;

        void Awake()
        {
            reset.AssertNotNull(nameof(reset)).onClick.AddListener(() => ResetAll?.Invoke());
            LinkResolver.LinkClicked += s => LinkClicked?.Invoke(s);
        }
        
        public override void ClearSubscribers()
        {
            Close.ClearSubscribers();
            LinkClicked = null;
            ResetAll = null;
        }
    }
}
