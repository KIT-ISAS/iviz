using System;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public class AnchorToggleButton : MonoBehaviour
    {
        static readonly Color EnabledColor = Settings.IsHololens
            ? new Color(0.45f, 0.75f, 0.75f, 1.0f)
            : new Color(0, 0, 0, 1f);

        static readonly Color DisabledColor = Settings.IsHololens
            ? new Color(0.75f, 0.75f, 0.75f, 1.0f)
            : new Color(0.75f, 0.75f, 0.75f, 0.25f);

        [SerializeField] bool state;

        [SerializeField] Image image = null;

        [SerializeField] Button button = null;
        Button Button => button == null ? (button = GetComponent<Button>()) : button;

        [SerializeField] Text text = null;
        [SerializeField] string enabledText = null;
        [SerializeField] string disabledText = null;

        public event Action Clicked;

        public bool State
        {
            get => state;
            set
            {
                state = value;
                if (image != null)
                {
                    image.color = state ? EnabledColor : DisabledColor;
                }

                if (text != null)
                {
                    text.color = state ? EnabledColor : DisabledColor;
                    if (value && !string.IsNullOrEmpty(enabledText))
                    {
                        text.text = enabledText;
                    }
                    else if (!value && !string.IsNullOrEmpty(disabledText))
                    {
                        text.text = disabledText;
                    }
                }
            }
        }

        public bool Visible
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        public bool Interactable
        {
            get => Button.interactable;
            set => Button.interactable = value;
        }

        void Awake()
        {
            if (image == null)
            {
                image = transform.GetChild(0).GetComponent<Image>();
            }
            State = State;
            Button.onClick.AddListener(() =>
            {
                State = !State;
                Clicked?.Invoke();
            });
        }
    }
}