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

        Image image;

        Button button;
        Button Button => button == null ? (button = GetComponent<Button>()) : button;

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
            image = transform.GetChild(0).GetComponent<Image>();
            State = State;
            Button.onClick.AddListener(() =>
            {
                State = !State;
                Clicked?.Invoke();
            });
        }
    }
}