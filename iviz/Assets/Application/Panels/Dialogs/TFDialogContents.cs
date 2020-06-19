using System;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public class TFDialogContents : MonoBehaviour, IDialogPanelContents
    {
        public TrashButtonWidget Close;
        public TFLog TFLog;

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
