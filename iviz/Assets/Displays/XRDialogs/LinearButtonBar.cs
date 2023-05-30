#nullable enable

using System;
using System.Threading;
using Iviz.Common;
using Iviz.Core;
using UnityEngine;

namespace Iviz.Displays.XR
{
    public sealed class LinearButtonBar : MonoBehaviour
    {
        [SerializeField] XRButton[] buttons = Array.Empty<XRButton>();
        [SerializeField] float padding = 0.1f;

        Transform? mTransform;
        public Transform Transform => this.EnsureHasTransform(ref mTransform);

        public event Action<int>? Clicked;

        public bool Visible
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        void Awake()
        {
            const float scale = 1;
            
            float y = -(scale + padding) * (buttons.Length - 1) / 2;  
            
            foreach (var (button, index) in buttons.WithIndex())
            {
                button.Transform.SetParentLocal(Transform);
                button.Clicked += () => Clicked?.Invoke(index);

                button.Transform.localScale = scale * Vector3.one;
                button.Transform.localPosition = Vector3.down * y;

                y += scale + padding;
            }
        }
    }
}