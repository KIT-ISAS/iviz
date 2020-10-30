using System;
using Iviz.Resources;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public class DataLabelWidget : MonoBehaviour, IWidget
    {
        [SerializeField] Text label = null;
        bool interactable = true;

        [NotNull]
        public string Label
        {
            get => label.text;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }
                
                name = "DataLabel:" + value;
                label.text = value;
            }
        }
        public bool Interactable
        {
            get => interactable;
            set
            {
                interactable = value;
                label.color = value ? Resource.Colors.EnabledFontColor : Resource.Colors.DisabledFontColor;
            }
        }
        public bool HasRichText
        {
            get => label.supportRichText;
            set => label.supportRichText = value;
        }

        public TextAnchor Alignment
        {
            get => label.alignment;
            set => label.alignment = value;
        }

        [NotNull]
        public DataLabelWidget SetLabel([NotNull] string f)
        {
            Label = f;
            return this;
        }

        public void ClearSubscribers()
        {
        }

        [NotNull]
        public DataLabelWidget SetHasRichText(bool b)
        {
            HasRichText = b;
            return this;
        }
        
        [NotNull]
        public DataLabelWidget SetAlignment(TextAnchor t)
        {
            Alignment = t;
            return this;
        }
    }
}