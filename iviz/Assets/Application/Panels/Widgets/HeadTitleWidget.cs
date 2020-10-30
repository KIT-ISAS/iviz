using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public class HeadTitleWidget : MonoBehaviour, IWidget
    {
        [SerializeField] Text label;

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
                name = $"HeadTitle:{value}";
            }
        }

        [NotNull]
        public HeadTitleWidget SetLabel([NotNull] string f)
        {
            Label = f;
            return this;
        }

        public void ClearSubscribers() { }

    }
}