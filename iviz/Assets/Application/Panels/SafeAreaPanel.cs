using Iviz.Core;
using Iviz.Msgs.IvizMsgs;
using UnityEngine;
using UnityEngine.UI;

#nullable enable

namespace Iviz.App
{
    public class SafeAreaPanel : MonoBehaviour
    {
        [SerializeField] RectTransform? fullCanvas;
        [SerializeField] RectTransform? canvas;
        [SerializeField] RectTransform? leftBlack;
        [SerializeField] RectTransform? rightBlack;
        [SerializeField] CanvasScaler? scaler;

        RectTransform FullCanvas => fullCanvas.AssertNotNull(nameof(fullCanvas));
        RectTransform Canvas => canvas.AssertNotNull(nameof(canvas));
        RectTransform LeftBlack => leftBlack.AssertNotNull(nameof(leftBlack));
        RectTransform RightBlack => rightBlack.AssertNotNull(nameof(rightBlack));
        CanvasScaler Scaler => scaler.AssertNotNull(nameof(scaler));

        ScreenOrientation currentOrientation;
        Vector2Int currentResolution;

        void Awake()
        {
            bool isPhoneDevice = Settings.IsMobile && Screen.width / (float)Screen.height > 1.6f; // is phone not tablet

            if (isPhoneDevice)
            {
                // landscape phone mode!
                Scaler.referenceResolution = new Vector2(600, 720);
                Scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            }
            else
            {
                // landscape tablet mode! 
                Scaler.referenceResolution = new Vector2(1100, 720);
                Scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
            }            
        }
        
        void Start()
        {
            UpdateSize();
        }

        void UpdateSize()
        {
            currentOrientation = Screen.orientation;
            currentResolution = CurrentResolution();
            
            float fullSize = FullCanvas.rect.width;
            float scale = fullSize / Screen.width;

            float leftSafeSize = Screen.safeArea.min.x * scale;
            float rightSafeSize = Screen.safeArea.max.x * scale;
            float safeSize = rightSafeSize - leftSafeSize;

            Canvas.anchoredPosition = new Vector2(leftSafeSize, 0);
            Canvas.sizeDelta = new Vector2(safeSize, 0);

            float leftWidth = leftSafeSize * 1.5f;
            LeftBlack.anchoredPosition = new Vector2(-leftWidth / 2, 0);
            LeftBlack.sizeDelta = LeftBlack.sizeDelta.WithX(leftWidth);

            float rightWidth = (fullSize - safeSize) * 1.5f;
            RightBlack.anchoredPosition = new Vector2(safeSize + rightWidth/2, 0);
            RightBlack.sizeDelta = RightBlack.sizeDelta.WithX(rightWidth);
        }

        void Update()
        {
            if (Screen.orientation != currentOrientation || CurrentResolution()  == currentResolution)
            {
                UpdateSize();
            }
        }

        static Vector2Int CurrentResolution()
        {
            var resolution = Screen.currentResolution;
            return new Vector2Int(resolution.width, resolution.height);
        }
    }
}