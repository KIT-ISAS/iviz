using System;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public class TFDialogContents : MonoBehaviour, IDialogPanelContents
    {
        [SerializeField] TrashButtonWidget close = null;
        [SerializeField] TFLog tfLog = null;

        public TrashButtonWidget Close => close;
        public TFLog TFLog => tfLog;

        public bool Active
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        public void ClearSubscribers()
        {
            Close.ClearSubscribers();
            TFLog.ClearSubscribers();
        }
    }
}
