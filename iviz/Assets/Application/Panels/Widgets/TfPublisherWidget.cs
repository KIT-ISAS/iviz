#nullable enable

using Iviz.Controllers.TF;
using Iviz.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public sealed class TfPublisherWidget : MonoBehaviour, IWidget
    {
        [SerializeField] TMP_Text? text;
        [SerializeField] Button? button;

        TMP_Text Text => text.AssertNotNull(nameof(text));
        Button Button => button.AssertNotNull(nameof(button));

        void Awake()
        {
            Button.onClick.AddListener(OnClick);
        }

        void OnClick()
        {
            ModuleListPanel.Instance.ShowTfDialog();
        }

        public void UpdateText()
        {
            int numTfFrames = TfModule.Instance.NumFrames;
            int publishedFrames = TfPublisher.Instance.NumPublishedFrames;
            string frameStr = (numTfFrames, publishedFrames) switch
            {
                (0, 0) => "Transform Frames\n<b>No frames</b>",
                (1, 0) => "Transform Frames\n<b>1 frame</b>",
                (1, _) => $"Transform Frames\n<b>1 frame, {numTfFrames.ToString()} published</b>",
                (_, 0) => $"Transform Frames\n<b>{numTfFrames.ToString()} frames</b>",
                _ => $"Transform Frames\n<b>{numTfFrames.ToString()} frames, {numTfFrames.ToString()} published</b>"
            };
            
            Text.text = frameStr;
        }

        public void ClearSubscribers()
        {
        }
    }
}