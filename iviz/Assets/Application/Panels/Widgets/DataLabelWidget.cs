#nullable enable

using System;
using Iviz.Core;
using Iviz.Resources;
using Iviz.Tools;
using TMPro;
using UnityEngine;

namespace Iviz.App
{
    public sealed class DataLabelWidget : MonoBehaviour, IWidget
    {
        [SerializeField] TMP_Text? label;
        bool interactable = true;
        uint? textHash;

        TMP_Text Label => label.AssertNotNull(nameof(label));

        public string Text
        {
            get => Label.text;
            set
            {
                ThrowHelper.ThrowIfNull(value, nameof(value));
                uint newHash = Crc32Calculator.Compute(value);
                if (newHash == textHash)
                {
                    return;
                }

                textHash = newHash;
                Label.text = value;
            }
        }

        public void SetText(BuilderPool.BuilderRent builder)
        {
            uint newHash = Crc32Calculator.Compute(builder);
            if (newHash == textHash)
            {
                return;
            }

            textHash = newHash;
            Label.SetTextRent(builder);
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