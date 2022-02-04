using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public sealed class ARMarkerWidget : MonoBehaviour, IWidget
    {
        [SerializeField] Text text;
        [SerializeField] Button button;

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