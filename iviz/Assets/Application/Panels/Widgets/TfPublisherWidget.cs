﻿using Iviz.Controllers;
using Iviz.Controllers.TF;
using UnityEngine;
using UnityEngine.UI;

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
            int publishedFrames = TfPublisher.Instance.NumPublishedFrames;
            string frameStr = (numTfFrames, publishedFrames) switch
            {
                (0, 0) => "Transform Frames\n<b>No frames</b>",
                (1, 0) => "Transform Frames\n<b>1 frame</b>",
                (1, _) => $"Transform Frames\n<b>1 frame, {numTfFrames.ToString()} published</b>",
                (_, 0) => $"Transform Frames\n<b>{numTfFrames.ToString()} frames</b>",
                _ => $"Transform Frames\n<b>{numTfFrames.ToString()} frames, {numTfFrames.ToString()} published</b>"
            };
            
            text.text = frameStr;
        }

        public void ClearSubscribers()
        {
        }
    }
}