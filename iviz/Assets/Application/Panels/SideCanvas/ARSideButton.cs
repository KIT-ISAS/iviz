#nullable enable

using System;
using Iviz.Resources;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    [RequireComponent(typeof(Button))]
    public sealed class ARSideButton : MonoBehaviour
    {
        [SerializeField] Image? image;
        [SerializeField] Image? frame;
        [SerializeField] TMP_Text? text;

        bool? isEnabled;
        bool interactable = true;

        public event Action? Clicked;

        public bool Visible
        {
            set => gameObject.SetActive(value);
        }
        
        public bool Enabled
        {
            get => isEnabled ?? false;
            set
            {
                isEnabled = value;
                UpdateGraphics();
            }
        }

        public bool Interactable
        {
            get => interactable;
            set
            {
                interactable = value;
                UpdateGraphics();
            }
        }

        void UpdateGraphics()
        {
            if (image != null)
            {
                image.color = Enabled && Interactable
                    ? Resource.Colors.EnabledSideFont
                    : Resource.Colors.DisabledSideFont;
            }

            if (text != null)
            {
                text.color = Enabled && Interactable
                    ? Resource.Colors.EnabledSideFont
                    : Resource.Colors.DisabledSideFont;
            }

            if (frame != null)
            {
                frame.color = Enabled && Interactable
                    ? Resource.Colors.EnabledSideFrame
                    : Resource.Colors.DisabledSideFrame;
            }
            
        }

        void Awake()
        {
            GetComponent<Button>().onClick.AddListener(OnClick);
            Enabled = isEnabled ?? true;
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