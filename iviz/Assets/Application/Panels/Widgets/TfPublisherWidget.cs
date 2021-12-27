using Iviz.Common;
using Iviz.Core;
using UnityEngine;
using UnityEngine.UI;
using JetBrains.Annotations;

namespace Iviz.App
{
    public sealed class TfPublisherWidget : MonoBehaviour, IWidget
    {
        [SerializeField] Text text;
        [SerializeField] Button button;

        void Awake()
        {
            button.onClick.AddListener(OnClick);
        }

        void OnClick()
        {
            ModuleListPanel.Instance.ShowTfPublisherDialog();
        }

        public void UpdateText()
        {
            int numTfFrames = ModuleListPanel.Instance.NumTfFramesPublished;
            string frameStr = numTfFrames switch
            {
                0 => "Publisher Manager\n<b>No published frames</b>",
                1 => "Publisher Manager\n<b>1 published frame</b>",
                _ => $"Publisher Manager\n<b>{numTfFrames.ToString()} published frames</b>"
            };
            text.text = frameStr;
        }

        public void ClearSubscribers()
        {
        }
    }
}