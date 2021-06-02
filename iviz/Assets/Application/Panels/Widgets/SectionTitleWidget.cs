using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public sealed class SectionTitleWidget : MonoBehaviour, IWidget
    {
        [SerializeField] Text label = null;

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