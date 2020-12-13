using System;
using Iviz.Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace External
{
    public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public float Horizontal => (snapX) ? SnapFloat(input.x, AxisOptions.Horizontal) : input.x;

        public float Vertical => (snapY) ? SnapFloat(input.y, AxisOptions.Vertical) : input.y;

        public Vector2 Direction => new Vector2(Horizontal, Vertical);

        public event Action PointerDown;
        public event Action<Vector2> Changed;
        public event Action PointerUp;

        public float HandleRange
        {
            get => handleRange;
            set => handleRange = Mathf.Abs(value);
        }

        public float DeadZone
        {
            get => deadZone;
            set => deadZone = Mathf.Abs(value);
        }

        public AxisOptions AxisOptions
        {
            get => AxisOptions;
            set => axisOptions = value;
        }

        public bool SnapX
        {
            get => snapX;
            set => snapX = value;
        }

        public bool SnapY
        {
            get => snapY;
            set => snapY = value;
        }

        [SerializeField] float handleRange = 1;
        [SerializeField] float deadZone = 0;
        [SerializeField] AxisOptions axisOptions = AxisOptions.Both;
        [SerializeField] bool snapX = false;
        [SerializeField] bool snapY = false;

        [SerializeField] protected RectTransform background = null;
        [SerializeField] RectTransform handle = null;
        [SerializeField] float maxStretch = 1;
        RectTransform baseRect = null;

        Canvas canvas;
        Camera cam;

        Vector2 input = Vector2.zero;

        protected virtual void Start()
        {
            HandleRange = handleRange;
            DeadZone = deadZone;
            baseRect = GetComponent<RectTransform>();
            canvas = GetComponentInParent<Canvas>();
            if (canvas == null)
                Debug.LogError("The Joystick is not placed inside a canvas");

            Vector2 center = new Vector2(0.5f, 0.5f);
            background.pivot = center;
            handle.anchorMin = center;
            handle.anchorMax = center;
            handle.pivot = center;
            handle.anchoredPosition = Vector2.zero;
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            GameThread.EveryFrame += OnDrag;
            PointerDown?.Invoke();
        }

        void OnDrag()
        {
            Vector2 dragPosition = Settings.IsMobile ? Input.GetTouch(0).position : (Vector2) Input.mousePosition;
            
            cam = null;
            if (canvas.renderMode == RenderMode.ScreenSpaceCamera)
                cam = canvas.worldCamera;

            Vector2 position = RectTransformUtility.WorldToScreenPoint(cam, background.position);
            Vector2 radius = background.sizeDelta / 2;
            input = (dragPosition - position) / (radius * canvas.scaleFactor);
            FormatInput();
            HandleInput(input.magnitude, input.normalized, radius);
            handle.anchoredPosition = input * radius * handleRange;
            
            Changed?.Invoke(Direction);
        }

        protected virtual void HandleInput(float magnitude, Vector2 normalised, Vector2 radius)
        {
            if (magnitude > deadZone)
            {
                if (magnitude > maxStretch)
                    input = normalised * maxStretch;
            }
            else
                input = Vector2.zero;
        }

        void FormatInput()
        {
            if (axisOptions == AxisOptions.Horizontal)
                input = new Vector2(input.x, 0f);
            else if (axisOptions == AxisOptions.Vertical)
                input = new Vector2(0f, input.y);
        }

        float SnapFloat(float value, AxisOptions snapAxis)
        {
            if (value == 0)
                return value;

            if (axisOptions == AxisOptions.Both)
            {
                float angle = Vector2.Angle(input, Vector2.up);
                if (snapAxis == AxisOptions.Horizontal)
                {
                    if (angle < 22.5f || angle > 157.5f)
                        return 0;
                    else
                        return (value > 0) ? 1 : -1;
                }
                else if (snapAxis == AxisOptions.Vertical)
                {
                    if (angle > 67.5f && angle < 112.5f)
                        return 0;
                    else
                        return (value > 0) ? 1 : -1;
                }

                return value;
            }
            else
            {
                if (value > 0)
                    return 1;
                if (value < 0)
                    return -1;
            }

            return 0;
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            input = Vector2.zero;
            handle.anchoredPosition = Vector2.zero;
            GameThread.EveryFrame -= OnDrag;
            PointerUp?.Invoke();
        }

        protected Vector2 ScreenPointToAnchoredPosition(Vector2 screenPosition)
        {
            Vector2 localPoint;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(baseRect, screenPosition, cam, out localPoint))
            {
                Vector2 pivotOffset = baseRect.pivot * baseRect.sizeDelta;
                return localPoint - (background.anchorMax * baseRect.sizeDelta) + pivotOffset;
            }

            return Vector2.zero;
        }
    }

    public enum AxisOptions
    {
        Both,
        Horizontal,
        Vertical
    }
}