using System;
using Iviz.Core;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public sealed class SectionTitleWidget : MonoBehaviour, IWidget
    {
        [SerializeField] Text label;

        [NotNull]
        public string Label
        {
            get => label.text;
            set
            {
                ThrowHelper.ThrowIfNull(value, nameof(value));
                label.text = value;
                name = "SectionTitle:" + value;
            }
        }

        public void ClearSubscribers() { }

        [NotNull]
        public SectionTitleWidget SetLabel([NotNull] string f)
        {
            Label = f;
            return this;
        }
    }
}