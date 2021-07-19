using Iviz.Core;
using UnityEngine;
using UnityEngine.UI;
using JetBrains.Annotations;

namespace Iviz.App
{
    public sealed class ARMarkerWidget : MonoBehaviour, IWidget
    {
        [SerializeField] Text text = null;
        [SerializeField] Button button = null;

        public string Description
        {
            set => text.text = value;
        }
        
        void Awake()
        {
            button.onClick.AddListener(OnClick);
        }

        void OnClick()
        {
            ModuleListPanel.Instance.ShowARMarkerDialog();
        }

        public void ClearSubscribers()
        {
        }
    }
}