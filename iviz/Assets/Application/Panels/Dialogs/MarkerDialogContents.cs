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
        [SerializeField] TrashButtonWidget? close = null;
        [SerializeField] Button? reset = null;
        [SerializeField] DataLabelWidget? label = null;
        [SerializeField] TMP_Text? text = null;
        [SerializeField] LinkResolver? linkResolver = null;

        public TrashButtonWidget Close => close.AssertNotNull(nameof(close));
        public DataLabelWidget Label => label.AssertNotNull(nameof(label));
        public TMP_Text Text => text.AssertNotNull(nameof(text));

        public event Action? ResetAll;
        public event Action<string>? LinkClicked;

        void Awake()
        {
            reset.AssertNotNull(nameof(reset)).onClick.AddListener(() => ResetAll?.Invoke());
            linkResolver.AssertNotNull(nameof(linkResolver)).LinkClicked += s => LinkClicked?.Invoke(s);
        }
        
        public override void ClearSubscribers()
        {
            Close.ClearSubscribers();
            LinkClicked = null;
            ResetAll = null;
        }
    }
}
