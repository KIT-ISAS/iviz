#nullable enable

using Iviz.Core;
using TMPro;
using UnityEngine;

namespace Iviz.App
{
    public sealed class BottomCanvasPanel : MonoBehaviour
    {
        [SerializeField] TMP_Text? cameraText;
        [SerializeField] TMP_Text? bottomTime;
        [SerializeField] TMP_Text? bottomBattery;
        [SerializeField] TMP_Text? bottomFps;
        [SerializeField] TMP_Text? bottomBandwidth;

        public TMP_Text CameraText => cameraText.AssertNotNull(nameof(cameraText));
        public TMP_Text Time => bottomTime.AssertNotNull(nameof(bottomTime));
        public TMP_Text Battery => bottomBattery.AssertNotNull(nameof(bottomBattery));
        public TMP_Text Fps => bottomFps.AssertNotNull(nameof(bottomFps));
        public TMP_Text Bandwidth => bottomBandwidth.AssertNotNull(nameof(bottomBandwidth));
    }
}