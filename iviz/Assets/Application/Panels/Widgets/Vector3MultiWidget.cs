#nullable enable

using System;
using Iviz.Core;
using Iviz.Resources;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    internal interface IVector3Widget : IWidget
    {
        Vector3 Value { get; set; }
        bool Visible { set; }
    }

    public sealed class Vector3MultiWidget : MonoBehaviour, IWidget
    {
        [SerializeField] TMP_Text? label;
        [SerializeField] Vector3SliderWidget? slider;
        [SerializeField] Vector3Widget? plain;
        [SerializeField] Button? toggle;

        IVector3Widget? activeWidget;

        IVector3Widget ActiveWidget
        {
            get => activeWidget ??= Slider;
            set => activeWidget = value;
        }

        IVector3Widget InactiveWidget => ReferenceEquals(ActiveWidget, Slider) ? Plain : Slider;
        Vector3SliderWidget Slider => slider.AssertNotNull(nameof(slider));
        Vector3Widget Plain => plain.AssertNotNull(nameof(plain));
        Button Toggle => toggle.AssertNotNull(nameof(toggle));
        TMP_Text LabelObject => label.AssertNotNull(nameof(label));

        public Vector3 Value
        {
            get => ActiveWidget.Value;
            set => ActiveWidget.Value = value;
        }

        public string Label
        {
            set => LabelObject.text = value;
        }

        public event Action<Vector3>? ValueChanged;

        void Start()
        {
            void OnValueChanged(Vector3 f) => ValueChanged?.Invoke(f);

            Slider.ValueChanged += OnValueChanged;
            Plain.ValueChanged += OnValueChanged;
            ActiveWidget.Visible = true;
            InactiveWidget.Visible = false;

            Toggle.onClick.AddListener(() =>
            {
                var value = ActiveWidget.Value;
                ActiveWidget = InactiveWidget;
                ActiveWidget.Visible = true;
                InactiveWidget.Visible = false;
                ActiveWidget.Value = value;
            });
        }

        public void ClearSubscribers()
        {
            ValueChanged = null;
        }

        public bool Interactable
        {
            set
            {
                Slider.Interactable = value;
                Plain.Interactable = value;
                Toggle.interactable = value;
            }
        }

        public Vector3MultiWidget SetLabel(string label)
        {
            Label = label;
            return this;
        }

        public Vector3MultiWidget SetRange(float f)
        {
            Slider.SetRange(f);
            return this;
        }
    }
}