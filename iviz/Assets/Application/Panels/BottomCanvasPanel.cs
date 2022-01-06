#nullable enable

using System;
using Iviz.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public sealed class BottomCanvasPanel : MonoBehaviour
    {
        [SerializeField] TMP_Text? cameraText;
        [SerializeField] TMP_Text? bottomTime;
        [SerializeField] TMP_Text? bottomBattery;
        [SerializeField] TMP_Text? bottomFps;
        [SerializeField] TMP_Text? bottomBandwidth;
        [SerializeField] Button? cameraButton;

        public event Action? CameraButtonClicked;
        
        public TMP_Text CameraText => cameraText.AssertNotNull(nameof(cameraText));
        public TMP_Text Time => bottomTime.AssertNotNull(nameof(bottomTime));
        public TMP_Text Battery => bottomBattery.AssertNotNull(nameof(bottomBattery));
        public TMP_Text Fps => bottomFps.AssertNotNull(nameof(bottomFps));
        public TMP_Text Bandwidth => bottomBandwidth.AssertNotNull(nameof(bottomBandwidth));
        Button CameraButton => cameraButton.AssertNotNull(nameof(cameraButton));

        void Awake()
        {
            CameraButton.onClick.AddListener(() => CameraButtonClicked?.Invoke());
        }
    }
}