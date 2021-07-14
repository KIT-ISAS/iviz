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
        static readonly Color DisabledColor = new Color(0.3f, 0.3f, 0.3f, 1); 
        
        [SerializeField] int id;
        [SerializeField, CanBeNull] LauncherButton parent;
        Button button;

        [SerializeField] Image image;
        [SerializeField] Image frame;
        [SerializeField] TMP_Text text;

        bool viewEnabled = true;

        public event Action Clicked;
        
        public bool Enabled
        {
            get => viewEnabled;
            set
            {
                viewEnabled = value;
                if (image != null)
                {
                    image.color = value ? Color.black : DisabledColor;
                }

                if (text != null)
                {
                    text.color = value ? Color.black : DisabledColor;
                }

                if (frame != null)
                {
                    frame.color = value ? Color.black : DisabledColor;
                }
            }
        }

        void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(OnClick);
            Enabled = Enabled;
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