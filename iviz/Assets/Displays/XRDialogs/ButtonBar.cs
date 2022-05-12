#nullable enable

using System;
using System.Threading;
using Iviz.Common;
using Iviz.Core;
using UnityEngine;

namespace Iviz.Displays.XR
{
    public class ButtonBar : MonoBehaviour
    {
        [SerializeField] XRButton[] buttons = Array.Empty<XRButton>();
        [SerializeField] GameObject? draggableObject;
        [SerializeField] float scale = 0.4f;
        [SerializeField] float padding = 0.1f;

        IHasBounds? frame;
        CancellationTokenSource? tokenSource;

        Transform? mTransform;
        public Transform Transform => this.EnsureHasTransform(ref mTransform);
        public float? Damping { get; set; } = 0.1f;
        public event Action<int>? Clicked;
        
        public virtual bool Visible
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }
        
        void Awake()
        {
            if (draggableObject != null)
            {
                var draggable = draggableObject.GetComponent<IDraggable>();
                draggable.StartDragging += () => Activate(false);
                draggable.EndDragging += () => Activate(true);

                frame = draggableObject.GetComponent<IHasBounds>();
            }

            const float angleOpening = Mathf.PI / 4;
            float radius = (0.5f + padding) * scale * buttons.Length / angleOpening;
            float delta = (buttons.Length - 1) / 2f;

            foreach (var (button, index) in buttons.WithIndex())
            {
                button.Transform.SetParentLocal(Transform);
                button.Clicked += () => Clicked?.Invoke(index);

                int reverseIndex = buttons.Length - 1 - index; // 0-180 is right to left, we need left to right
                float a = (reverseIndex - delta) / delta * angleOpening / 2 + Mathf.PI / 2;
                button.Transform.localScale = scale * Vector3.one;
                button.Transform.localPosition = radius * new Vector3(Mathf.Cos(a), Mathf.Sin(a), 0);
            }

            Transform.SetParentLocal(null);
            Activate(true);
        }

        void Activate(bool open)
        {
            tokenSource?.Cancel();
            tokenSource = new CancellationTokenSource();

            void Interpolate(float t)
            {
                float scaledT = Mathf.Sqrt(open ? t : 1 - t);
                Transform.localScale = scaledT * Vector3.one;
            }

            FAnimator.Spawn(tokenSource.Token, 0.1f, Interpolate);
        }

        void Update()
        {
            if (frame?.BoundsTransform is not { } boundsTransform
                || frame.Bounds is not { } bounds)
            {
                return;
            }

            var worldBounds = bounds.TransformBound(boundsTransform.AsPose(), Vector3.one);
            var targetPosition = worldBounds.center + worldBounds.size.y * Vector3.up;
            var targetRotation = Quaternion.LookRotation(targetPosition - Settings.MainCameraPose.position);

            if (Damping is { } damping)
            {
                Transform.SetPositionAndRotation(
                    Vector3.Lerp(Transform.position, targetPosition, damping),
                    Quaternion.Lerp(Transform.rotation, targetRotation, damping));
            }
            else
            {
                Transform.SetPositionAndRotation(targetPosition, targetRotation);
            }
        }
    }
}