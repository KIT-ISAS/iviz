using System;
using System.Linq;
using Iviz.Core;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Iviz.App
{
    public class PopupButton : MonoBehaviour
    {
        static readonly Color DisabledColorFrame = new Color(0.75f, 0.75f, 0.75f, 1);
        static readonly Color DisabledColor = new Color(0.25f, 0.25f, 0.25f, 0.5f); 
        static readonly Color EnabledColor = Color.black; 
        static readonly Color EnabledColorFrame = new Color(0.2f, 0.3f, 0.4f); 
        
        [SerializeField, CanBeNull] LauncherButton parent = null;
        Button button;

        [SerializeField] Image image = null;
        [SerializeField] Image frame = null;
        [SerializeField] TMP_Text text = null;

        bool? viewEnabled;

        public event Action Clicked;
        
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
                    image.color = value ? EnabledColor : DisabledColor;
                }

                if (text != null)
                {
                    text.color = value ? EnabledColor : DisabledColor;
                }

                if (frame != null)
                {
                    frame.color = value ? EnabledColorFrame : DisabledColorFrame;
                }
            }
        }

        void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(OnClick);
            Enabled = viewEnabled ?? true;
        }

        void OnClick()
        {
            if (parent != null)
            {
                parent.OnChildButtonClicked();
            }
            
            Clicked?.Invoke();
        }
    }
}