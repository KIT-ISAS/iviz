using System;
using Iviz.Core;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Iviz.App
{
    public sealed class MovableButtonWidget : MonoBehaviour, IWidget, IDragHandler, IEndDragHandler
    {
        const float MoveThresholdLeft = 40;
        const float MoveThresholdRight = 80;

        [SerializeField] RectTransform targetTransform;
        [SerializeField] Text buttonText;
        bool isDragging;
        bool stuckRight;
        bool stuckLeft;
        float startX;

        [NotNull] public RectTransform Transform => (RectTransform) GameObject.transform;
        public Text Text => buttonText;
        [NotNull] public GameObject GameObject => transform.parent.gameObject;
        
        public event Action RevealedLeft;
        public event Action RevealedRight;
        public event Action Clicked;

        [NotNull]
        public string Name
        {
            get => GameObject.name;
            set => GameObject.name = value;
        }

        public bool Visible
        {
            get => GameObject.activeSelf;
            set => GameObject.SetActive(value);
        }

        void Awake()
        {
            if (targetTransform == null)
            {
                targetTransform = (RectTransform) transform;
            }
            
            GameObject.GetComponentInChildren<Button>().onClick.AddListener(OnClicked);
        }

        void IDragHandler.OnDrag([NotNull] PointerEventData eventData)
        {
            if (!isDragging)
            {
                startX = targetTransform.localPosition.x;
                isDragging = true;
            }

            if (targetTransform == null)
            {
                return;
            }

            targetTransform.localPosition += eventData.delta.x * Vector3.right;
            if (targetTransform.localPosition.x > startX + MoveThresholdLeft)
            {
                if (!stuckRight)
                {
                    RevealedLeft?.Invoke();
                    stuckRight = true;
                }

                targetTransform.localPosition = targetTransform.localPosition.WithX(startX + MoveThresholdLeft);
            }
            else
            {
                stuckRight = false;
            }

            if (targetTransform.localPosition.x < startX - MoveThresholdRight)
            {
                if (!stuckLeft)
                {
                    RevealedRight?.Invoke();
                    stuckLeft = true;
                }

                targetTransform.localPosition = targetTransform.localPosition.WithX(startX - MoveThresholdRight);
            }
            else
            {
                stuckLeft = false;
            }
        }

        void IEndDragHandler.OnEndDrag(PointerEventData _)
        {
            isDragging = false;
            stuckRight = false;
            targetTransform.localPosition = targetTransform.localPosition.WithX(startX);
        }

        void OnClicked()
        {
            if (!isDragging)
            {
                Clicked?.Invoke();
            }
        }

        public void ClearSubscribers()
        {
            RevealedRight = null;
            RevealedLeft = null;
            Clicked = null;
        }
    }
}