#nullable enable

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    [RequireComponent(typeof(Button))]
    public class ARSideButton : MonoBehaviour
    {
        [SerializeField] Image? image;
        [SerializeField] Image? frame;
        [SerializeField] TMP_Text? text;

        bool? viewEnabled;

        public event Action? Clicked;

        public bool Enabled
        {
            get => viewEnabled ?? false;
            set
            {
                if (viewEnabled == value)
                {
                    return;
                }

                viewEnabled = value;

                if (image != null)
                {
                    image.color = value
                        ? Resources.Resource.Colors.EnabledSideFont
                        : Resources.Resource.Colors.DisabledSideFont;
                }

                if (text != null)
                {
                    text.color = value
                        ? Resources.Resource.Colors.EnabledSideFont
                        : Resources.Resource.Colors.DisabledSideFont;
                }

                if (frame != null)
                {
                    frame.color = value
                        ? Resources.Resource.Colors.EnabledSideFrame
                        : Resources.Resource.Colors.DisabledSideFrame;
                }
            }
        }

        void Awake()
        {
            GetComponent<Button>().onClick.AddListener(OnClick);
            Enabled = viewEnabled ?? true;
        }

        void OnClick()
        {
            Clicked?.Invoke();
        }

        void OnDestroy()
        {
            Clicked = null;
        }
    }
}