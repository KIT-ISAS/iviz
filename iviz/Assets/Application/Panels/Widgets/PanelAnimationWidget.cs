using Iviz.Core;
using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.App
{
    public sealed class PanelAnimationWidget : MonoBehaviour
    {
        [SerializeField] float start;
        [SerializeField] float end;
        [SerializeField] float durationInSec = 0.1f;

        RectTransform mTransform;
        [NotNull] RectTransform Transform => mTransform != null ? mTransform : (mTransform = (RectTransform)transform);

        enum Action
        {
            Open,
            Close
        }

        Action action;
        float startTime;

        void Open()
        {
            action = Action.Open;
            enabled = true;
            startTime = Time.time;
            Transform.anchoredPosition = Transform.anchoredPosition.WithX(start * ModuleListPanel.CanvasScale);
        }

        void Close()
        {
            action = Action.Close;
            enabled = true;
            startTime = Time.time;
            Transform.anchoredPosition = Transform.anchoredPosition.WithX(end * ModuleListPanel.CanvasScale);
        }

        bool active;
        public bool Active
        {
            set
            {
                if (active == value)
                {
                    return;
                }

                active = value;
                if (active)
                {
                    Open();
                }
                else
                {
                    Close();
                }
            }
        }

        void Update()
        {
            float t = (Time.time - startTime) / durationInSec;
            t = t * t;
            if (t > 1)
            {
                t = 1;
                enabled = false; 
            }
            
            float x;
            switch (action)
            {
                case Action.Close:
                    x = end + (start - end) * t;
                    break;
                case Action.Open:
                    x = start + (end - start) * t;
                    break;
                default:
                    return;
            }

            Transform.anchoredPosition = Transform.anchoredPosition.WithX(x * ModuleListPanel.CanvasScale);
        }
    }
}