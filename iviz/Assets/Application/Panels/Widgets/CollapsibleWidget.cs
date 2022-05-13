using System;
using System.Linq;
using Iviz.Core;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public sealed class CollapsibleWidget : MonoBehaviour, IWidget
    {
        [SerializeField] TMP_Text label;
        [SerializeField] Button button;
        [SerializeField] RectTransform childrenTransform;
        [SerializeField] RectTransform ownTransform;

        bool open;
        string text;
        [CanBeNull] DataPanelWidgets parent;

        [NotNull]
        public string Label
        {
            get => label.text;
            set
            {
                ThrowHelper.ThrowIfNull(value, nameof(value));
                name = "CollapsibleWidget:" + value;
                text = value;
                UpdateText();
            }
        }

        void Awake()
        {
            button.onClick.AddListener(OnClicked);
        }

        void UpdateText()
        {
            label.text = open
                ? "- <u>" + text + "</u>"
                : "<u>" + text + "...</u>";            
        }

        void OnClicked()
        {
            open = !open;
            UpdateText();
            
            var children = childrenTransform.Cast<RectTransform>();
            foreach (var child in children)
            {
                child.gameObject.SetActive(open);
            }
            
            FinishAttaching();
            if (parent != null)
            {
                parent.UpdateSize();
            }
        }

        [NotNull]
        public CollapsibleWidget Attach([NotNull] MonoBehaviour o)
        {
            o.transform.SetParent(childrenTransform);
            o.gameObject.SetActive(open);
            return this;
        }
        
        [NotNull]
        public CollapsibleWidget FinishAttaching()
        {
            const float yOffset = 5;

            var children = childrenTransform.Cast<RectTransform>();
            float y = 0;

            if (open)
            {
                foreach (var child in children)
                {
                    child.anchoredPosition = new Vector2(child.anchoredPosition.x, -y);
                    y += child.rect.height + yOffset;
                }
            }

            y += ((RectTransform)button.transform).rect.height + yOffset;
            ownTransform.sizeDelta = new Vector2(ownTransform.sizeDelta.x, y);
            
            return this;
        }        

        [NotNull]
        public CollapsibleWidget SetLabel([NotNull] string f)
        {
            Label = f;
            return this;
        }
        
        [NotNull]
        public CollapsibleWidget SetParent([NotNull] DataPanelWidgets p)
        {
            parent = p;
            return this;
        }        
        
        public void ClearSubscribers()
        {
        }
    }
}