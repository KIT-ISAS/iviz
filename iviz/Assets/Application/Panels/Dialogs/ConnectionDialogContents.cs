using System;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public class ConnectionDialogContents : MonoBehaviour, IDialogPanelContents
    {
        public InputFieldWidget MasterUri;
        public InputFieldWidget MyUri;
        public InputFieldWidget MyId;
        public TrashButtonWidget RefreshMyUri;
        public TrashButtonWidget RefreshMyId;
        public TrashButtonWidget Connect;
        public TrashButtonWidget Close;
        public TrashButtonWidget Stop;
        //public Text Text;

        public bool Active
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        public void ClearSubscribers()
        {
            MasterUri.ClearSubscribers();
            MyUri.ClearSubscribers();
            MyId.ClearSubscribers();
            RefreshMyUri.ClearSubscribers();
            RefreshMyId.ClearSubscribers();
            Connect.ClearSubscribers();
            Close.ClearSubscribers();
            Stop.ClearSubscribers();
        }
    }
}
