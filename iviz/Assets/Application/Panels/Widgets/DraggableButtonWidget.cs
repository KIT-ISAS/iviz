#nullable enable

using System;
using Iviz.Core;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Iviz.App
{
    public class DraggableButtonWidget : MonoBehaviour, IWidget, IDragHandler, IEndDragHandler, IBeginDragHandler,
        IPointerUpHandler
    {
        [SerializeField] Button? button = null;
        [SerializeField] RectTransform? targetTransform = null;
        [SerializeField] RectTransform? storeTransform = null;
        [SerializeField] TMP_Text? buttonLabel = null;
        [SerializeField] bool allowRevealLeft = true;
        [SerializeField] bool allowRevealRight = true;
        [SerializeField] float moveThresholdLeft = 40;
        [SerializeField] float moveThresholdRight = 60;
        [SerializeField] float minMotionThreshold = 5;
        [SerializeField] ScrollRect? parentScrollRect = null;
        [SerializeField] GameObject[] detachables = Array.Empty<GameObject>();

        bool isDragging;
        bool stuckRight;
        bool stuckLeft;
        float startX;
        float movedX;
        float movedY;

        bool raiseLeftOnRelease;
        bool raiseRightOnRelease;

        float ScaledMoveThresholdLeft => moveThresholdLeft;
        float ScaledMoveThresholdRight => moveThresholdRight;
        float ScaledMinMotionThreshold => minMotionThreshold;
        static float ScaledMaxMotionYThreshold => 10;
        Button Button => button.AssertNotNull(nameof(button));
        RectTransform TargetTransform => targetTransform.AssertNotNull(nameof(targetTransform));
        RectTransform StoreTransform => storeTransform.AssertNotNull(nameof(storeTransform));
        ScrollRect ParentScrollRect => parentScrollRect.AssertNotNull(nameof(parentScrollRect));
        TMP_Text ButtonLabel => buttonLabel.AssertNotNull(nameof(buttonLabel));
        public RectTransform Transform => (RectTransform)GameObject.transform;
        public GameObject GameObject => transform.parent.gameObject;

        public event Action? RevealedLeft;
        public event Action? RevealedRight;
        public event Action? Clicked;

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
        
        public string ButtonText
        {
            get => ButtonLabel.text;
            set => ButtonLabel.text = value;
        }

        public float ButtonFontSize
        {
            get => ButtonLabel.fontSize;
            set => ButtonLabel.fontSize = value;
        }

        protected virtual void Awake()
        {
            Button.onClick.AddListener(() =>
            {
                if (!isDragging)
                {
                    OnClicked();
                }
            });

            if (parentScrollRect == null)
            {
                parentScrollRect = GetComponentInParent<ScrollRect>();
            }
        }

        void Start()
        {
            foreach (var detachable in detachables)
            {
                detachable.transform.SetParent(StoreTransform);
                detachable.transform.SetAsFirstSibling();
            }
        }

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            movedX += eventData.delta.x;
            movedY += eventData.delta.y;

            if (movedX > 0 && !allowRevealLeft
                || movedX < 0 && !allowRevealRight
                || Math.Abs(movedX) < ScaledMinMotionThreshold && !isDragging
                || Math.Abs(movedY) > ScaledMaxMotionYThreshold && !isDragging)
            {
                ParentScrollRect.OnDrag(eventData);
                return;
            }

            if (!isDragging)
            {
                startX = TargetTransform.anchoredPosition.x;
                //startX = targetTransform.position.x;
                isDragging = true;
            }

            if (TargetTransform == null)
            {
                return;
            }

            //targetTransform.position = targetTransform.position.WithX(startX + movedX);
            TargetTransform.anchoredPosition = TargetTransform.anchoredPosition.WithX(startX + movedX);

            if (movedX > ScaledMoveThresholdLeft)
            {
                if (!stuckRight)
                {
                    raiseLeftOnRelease = true;
                    stuckRight = true;
                }

                //targetTransform.position = targetTransform.position.WithX(startX + ScaledMoveThresholdLeft);
                TargetTransform.anchoredPosition =
                    TargetTransform.anchoredPosition.WithX(startX + ScaledMoveThresholdLeft);
            }
            else
            {
                raiseLeftOnRelease = false;
                stuckRight = false;
            }

            if (movedX < -ScaledMoveThresholdRight)
            {
                if (!stuckLeft)
                {
                    raiseRightOnRelease = true;
                    stuckLeft = true;
                }

                //targetTransform.position = targetTransform.position.WithX(startX - ScaledMoveThresholdRight);
                TargetTransform.anchoredPosition =
                    TargetTransform.anchoredPosition.WithX(startX - ScaledMoveThresholdRight);
            }
            else
            {
                raiseRightOnRelease = false;
                stuckLeft = false;
            }
        }

        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            movedX = 0;
            movedY = 0;

            if (!isDragging)
            {
                ParentScrollRect.OnEndDrag(eventData);
                return;
            }

            isDragging = false;
            stuckLeft = false;
            stuckRight = false;
            //targetTransform.position = targetTransform.position.WithX(startX);
            TargetTransform.anchoredPosition = TargetTransform.anchoredPosition.WithX(startX);
            if (raiseLeftOnRelease)
            {
                OnRevealedLeft();
                raiseLeftOnRelease = false;
            }

            if (raiseRightOnRelease)
            {
                OnRevealedRight();
                raiseRightOnRelease = false;
            }
        }

        protected virtual void OnClicked()
        {
            Clicked?.Invoke();
        }

        protected virtual void OnRevealedLeft()
        {
            RevealedLeft?.Invoke();
        }

        protected virtual void OnRevealedRight()
        {
            RevealedRight?.Invoke();
        }

        public virtual void ClearSubscribers()
        {
            RevealedRight = null;
            RevealedLeft = null;
            Clicked = null;

            if (isDragging)
            {
                isDragging = false;
                stuckLeft = false;
                stuckRight = false;
                //targetTransform.position = targetTransform.position.WithX(startX);
                TargetTransform.anchoredPosition = TargetTransform.anchoredPosition.WithX(startX);
            }
        }

        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            ParentScrollRect.OnBeginDrag(eventData);
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            if (Settings.IsXR && isDragging)
            {
                // end dragging sometimes does not get triggered
                ((IEndDragHandler)this).OnEndDrag(eventData);
            }
        }
    }
}