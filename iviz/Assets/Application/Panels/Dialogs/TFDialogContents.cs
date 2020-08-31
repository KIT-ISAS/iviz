﻿using UnityEngine;

namespace Iviz.App
{
    public sealed class TFDialogContents : MonoBehaviour, IDialogPanelContents
    {
        [SerializeField] TrashButtonWidget close = null;
        [SerializeField] TFLog tfLog = null;
        [SerializeField] ToggleWidget showOnlyUsed = null;

        public TrashButtonWidget Close => close;
        public TFLog TFLog => tfLog;
        public ToggleWidget ShowOnlyUsed => showOnlyUsed;

        public bool Active
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        public void ClearSubscribers()
        {
            Close.ClearSubscribers();
            TFLog.ClearSubscribers();
            ShowOnlyUsed.ClearSubscribers();
        }
    }
}