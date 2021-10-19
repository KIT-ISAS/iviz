using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public sealed class MarkerDialogContents : PanelContents
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
        
        public override void ClearSubscribers()
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
