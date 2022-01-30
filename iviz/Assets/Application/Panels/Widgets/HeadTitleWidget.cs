#nullable enable

using System;
using Iviz.Core;
using TMPro;
using UnityEngine;

namespace Iviz.App
{
    public sealed class HeadTitleWidget : MonoBehaviour, IWidget
    {
        [SerializeField] TMP_Text? text;

        TMP_Text Text => text.AssertNotNull(nameof(text));
        
        public string Label
        {
            get => Text.text;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                Text.text = value;
                name = $"HeadTitle:{value}";
            }
        }

        public HeadTitleWidget SetLabel(string f)
        {
            Label = f;
            return this;
        }

        public void ClearSubscribers() { }

    }
}