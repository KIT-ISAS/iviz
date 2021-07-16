using System;
using Iviz.Core;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Iviz.App
{
    public class DraggableButtonWidget : MonoBehaviour, IWidget, IDragHandler, IEndDragHandler, IBeginDragHandler
    {
        [SerializeField] Button button;
        [SerializeField] RectTransform targetTransform;
        [SerializeField] Text buttonText;
        [SerializeField] bool allowRevealLeft = true;
        [SerializeField] bool allowRevealRight = true;
        [SerializeField] float moveThresholdLeft = 40;
        [SerializeField] float moveThresholdRight = 60;
        [SerializeField] float minMotionThreshold = 5;
        [SerializeField] ScrollRect parentScrollRect;
        [SerializeField] GameObject[] detachables;

        float ScaledMoveThresholdLeft => moveThresholdLeft * ModuleListPanel.CanvasScale;
        float ScaledMoveThresholdRight => moveThresholdRight * ModuleListPanel.CanvasScale;
        float ScaledMinMotionThreshold => minMotionThreshold * ModuleListPanel.CanvasScale;
        float ScaledMaxMotionYThreshold => 10 * ModuleListPanel.CanvasScale;
        
        bool isDragging;
        bool stuckRight;
        bool stuckLeft;
        float startX;
        float movedX;
        float movedY;

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

        protected virtual void Awake()
        {
            float thresholdScale = ModuleListPanel.CanvasScale;

            
            if (targetTransform == null)
            {
                targetTransform = (RectTransform) transform;
            }

            if (button == null)
            {
                button = GameObject.GetComponentInChildren<Button>();
            }

            button.onClick.AddListener(() =>
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
            if (detachables != null)
            {
                foreach (var detachable in detachables)
                {
                    detachable.transform.SetParent(transform.parent);
                    detachable.transform.SetAsFirstSibling();
                }
            }
        }

        void IDragHandler.OnDrag([NotNull] PointerEventData eventData)
        {
            movedX += eventData.delta.x;
            movedY += eventData.delta.y;
            Debug.Log(movedX + " " + movedY);
            
            if (movedX > 0 && !allowRevealLeft
                || movedX < 0 && !allowRevealRight
                || Mathf.Abs(movedX) < ScaledMinMotionThreshold && !isDragging
                || Mathf.Abs(movedY) > ScaledMaxMotionYThreshold && !isDragging) 
            {
                parentScrollRect.OnDrag(eventData);
                return;
            }

            if (!isDragging)
            {
                startX = targetTransform.position.x;
                isDragging = true;
            }

            if (targetTransform == null)
            {
                return;
            }

            targetTransform.position = targetTransform.position.WithX(startX + movedX);
            if (movedX > ScaledMoveThresholdLeft)
            {
                if (!stuckRight)
                {
                    raiseLeftOnRelease = true;
                    stuckRight = true;
                }

                targetTransform.position = targetTransform.position.WithX(startX + ScaledMoveThresholdLeft);
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

                targetTransform.position = targetTransform.position.WithX(startX - ScaledMoveThresholdRight);
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
                parentScrollRect.OnEndDrag(eventData);
                return;
            }

            isDragging = false;
            stuckLeft = false;
            stuckRight = false;
            targetTransform.position = targetTransform.position.WithX(startX);
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
                targetTransform.position = targetTransform.position.WithX(startX);
            }
        }

        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            parentScrollRect.OnBeginDrag(eventData);
        }
    }
}