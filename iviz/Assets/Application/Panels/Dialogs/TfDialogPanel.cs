#nullable enable

using System;
using Iviz.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public sealed class TfDialogPanel : DetachableDialogPanel
    {
        [SerializeField] SimpleButtonWidget? close;
        [SerializeField] SimpleButtonWidget? reset;
        [SerializeField] TfLog? tfLog;
        [SerializeField] ToggleWidget? showOnlyUsed;

        [SerializeField] Button? createFrame;
        [SerializeField] InputFieldWidget? frameName;
        
        public event Action? CreateFrameClicked; 

        Button CreateFrame => createFrame.AssertNotNull(nameof(createFrame));
        public SimpleButtonWidget Close => close.AssertNotNull(nameof(close));
        public SimpleButtonWidget Reset => reset.AssertNotNull(nameof(reset));
        public TfLog TfLog => tfLog.AssertNotNull(nameof(tfLog));
        public ToggleWidget ShowOnlyUsed => showOnlyUsed.AssertNotNull(nameof(showOnlyUsed));
        public InputFieldWidget FrameName => frameName.AssertNotNull(nameof(frameName));

        void Awake()
        {
            CreateFrame.onClick.AddListener(() => CreateFrameClicked?.Invoke());
        }
        
        public override void ClearSubscribers()
        {
            Close.ClearSubscribers();
            Reset.ClearSubscribers();
            TfLog.ClearSubscribers();
            ShowOnlyUsed.ClearSubscribers();
            FrameName.ClearSubscribers();
            CreateFrameClicked = null;
        }
    }
}
