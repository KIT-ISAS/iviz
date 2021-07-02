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
        [SerializeField] RectTransform targetTransform;
        [SerializeField] Text buttonText;
        [SerializeField] bool allowRevealLeft = true;
        [SerializeField] bool allowRevealRight = true;
        [SerializeField] float moveThresholdLeft = 40;
        [SerializeField] float moveThresholdRight = 80;
        bool isDragging;
        bool stuckRight;
        bool stuckLeft;
        float startX;

        bool raiseLeftOnRelease;
        bool raiseRightOnRelease;

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
            if (eventData.delta.x > 0 && !allowRevealLeft)
            {
                return;
            }

            if (eventData.delta.x < 0 && !allowRevealRight)
            {
                return;
            }

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
            if (targetTransform.localPosition.x > startX + moveThresholdLeft)
            {
                if (!stuckRight)
                {
                    //RevealedLeft?.Invoke();
                    raiseLeftOnRelease = true;
                    stuckRight = true;
                }

                targetTransform.localPosition = targetTransform.localPosition.WithX(startX + moveThresholdLeft);
            }
            else
            {
                raiseLeftOnRelease = false;
                stuckRight = false;
            }

            if (targetTransform.localPosition.x < startX - moveThresholdRight)
            {
                if (!stuckLeft)
                {
                    raiseRightOnRelease = true;
                    stuckLeft = true;
                }

                targetTransform.localPosition = targetTransform.localPosition.WithX(startX - moveThresholdRight);
            }
            else
            {
                raiseRightOnRelease = false;
                stuckLeft = false;
            }
        }

        void IEndDragHandler.OnEndDrag(PointerEventData _)
        {
            isDragging = false;
            stuckLeft = false;
            stuckRight = false;
            targetTransform.localPosition = targetTransform.localPosition.WithX(startX);
            if (raiseLeftOnRelease)
            {
                RevealedLeft?.Invoke();
                raiseLeftOnRelease = false;
            }

            if (raiseRightOnRelease)
            {
                RevealedRight?.Invoke();
                raiseRightOnRelease = false;
            }
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