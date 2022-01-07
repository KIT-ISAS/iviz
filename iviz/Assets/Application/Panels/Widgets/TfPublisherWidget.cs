using Iviz.Common;
using Iviz.Controllers.TF;
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
            ModuleListPanel.Instance.ShowTfDialog();
        }

        public void UpdateText()
        {
            int numTfFrames = TfListener.Instance.NumFrames;
            string frameStr = numTfFrames switch
            {
                0 => "TF Frames\n<b>No frames</b>",
                1 => "TF Frames\n<b>1 frame</b>",
                _ => $"TF Frames\n<b>{numTfFrames.ToString()} frames</b>"
            };
            text.text = frameStr;
        }

        public void ClearSubscribers()
        {
        }
    }
}