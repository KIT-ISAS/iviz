using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public sealed class MarkerDialogContents : MonoBehaviour, IDialogPanelContents
    {
        [SerializeField] TrashButtonWidget close = null;
        [SerializeField] Button reset = null;
        [SerializeField] DataLabelWidget label = null;
        //[SerializeField] Text text = null;
        [SerializeField] TMP_Text text = null;

        public TrashButtonWidget Close => close;
        public DataLabelWidget Label => label;
        //public Text Text => text;
        public TMP_Text Text => text;

        public event Action ResetAll;
        
        void Awake()
        {
            reset.onClick.AddListener(RaiseResetAll);
        }

        public bool Active
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        public void ClearSubscribers()
        {
            Close.ClearSubscribers();
            ResetAll = null;
        }

        void RaiseResetAll()
        {
            ResetAll?.Invoke();
        }
    }
}
