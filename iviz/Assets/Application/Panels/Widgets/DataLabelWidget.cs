#nullable enable

using System;
using Iviz.Core;
using Iviz.Resources;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public sealed class DataLabelWidget : MonoBehaviour, IWidget
    {
        [SerializeField] TMP_Text? label = null;
        bool interactable = true;

        TMP_Text Label => label.AssertNotNull(nameof(label));
        
        public string Text
        {
            get => Label.text;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                name = "DataLabel:" + value;
                Label.text = value;
            }
        }

        public bool Interactable
        {
            get => interactable;
            set
            {
                interactable = value;
                Label.color = value ? Resource.Colors.FontEnabled : Resource.Colors.FontDisabled;
            }
        }

        public bool HasRichText
        {
            get => Label.richText;
            set => Label.richText = value;
        }

        TextAlignmentOptions Alignment
        {
            get => Label.alignment;
            set => Label.alignment = value;
        }

        public DataLabelWidget SetLabel(string f)
        {
            Text = f;
            return this;
        }

        public void ClearSubscribers()
        {
        }

        public DataLabelWidget SetHasRichText(bool b)
        {
            HasRichText = b;
            return this;
        }

        public DataLabelWidget SetCentered()
        {
            Alignment = TextAlignmentOptions.Midline;
            return this;
        }
    }
}