#nullable enable

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
        [SerializeField] TMP_Text? label;
        [SerializeField] Button? button;
        [SerializeField] RectTransform? childrenTransform;
        [SerializeField] RectTransform? ownTransform;

        bool open;
        bool visible = true;
        string text = "";
        DataPanelWidgets? parent;
        
        TMP_Text LabelObject => label.AssertNotNull(nameof(label));
        Button Button => button.AssertNotNull(nameof(button));
        RectTransform ChildrenTransform => childrenTransform.AssertNotNull(nameof(childrenTransform));
        RectTransform OwnTransform => ownTransform.AssertNotNull(nameof(ownTransform));

        public bool Visible
        {
            get => visible;
            set
            {
                visible = value;
                UpdateSize();
                if (parent != null)
                {
                    parent.UpdateSize();
                }
            }
        }
        
        public bool Open
        {
            get => open;
            set
            {
                open = value;
                UpdateText();
            
                var children = ChildrenTransform.Cast<RectTransform>();
                foreach (var child in children)
                {
                    child.gameObject.SetActive(open);
                }
            
                UpdateSize();
                if (parent != null)
                {
                    parent.UpdateSize();
                }
            }
        }
        
        public string Label
        {
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
            Button.onClick.AddListener(OnClicked);
        }

        void UpdateText()
        {
            LabelObject.text = open
                ? "- <u>" + text + "</u>"
                : "<u>" + text + "...</u>";            
        }
        void OnClicked()
        {
            Open = !Open;
        }

        public CollapsibleWidget Attach(MonoBehaviour o)
        {
            o.transform.SetParent(childrenTransform);
            o.gameObject.SetActive(Open);
            return this;
        }

        void UpdateSize()
        {
            if (!Visible)
            {
                OwnTransform.sizeDelta = new Vector2(OwnTransform.sizeDelta.x, 0);
                return;
            }
            
            const float yOffset = 5;

            var children = ChildrenTransform.Cast<RectTransform>();
            float y = 0;

            if (Open)
            {
                foreach (var child in children)
                {
                    child.anchoredPosition = new Vector2(child.anchoredPosition.x, -y);
                    y += child.rect.height + yOffset;
                }
            }

            y += ((RectTransform)Button.transform).rect.height + yOffset;
            OwnTransform.sizeDelta = new Vector2(OwnTransform.sizeDelta.x, y);
        }
        
        public CollapsibleWidget FinishAttaching()
        {
            UpdateSize();
            return this;
        }        

        public CollapsibleWidget SetLabel(string f)
        {
            Label = f;
            return this;
        }
        
        public CollapsibleWidget SetParent(DataPanelWidgets p)
        {
            parent = p;
            return this;
        }        
        
        public void ClearSubscribers()
        {
        }
    }
}