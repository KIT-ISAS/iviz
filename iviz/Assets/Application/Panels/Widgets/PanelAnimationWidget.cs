using Iviz.Core;
using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.App
{
    public class PanelAnimationWidget : MonoBehaviour
    {
        [SerializeField] float start;
        [SerializeField] float end;
        [SerializeField] float durationInSec = 0.5f;

        RectTransform mTransform;
        [NotNull] RectTransform Transform => mTransform != null ? mTransform : (mTransform = (RectTransform)transform);

        enum Action
        {
            Open,
            Close
        }

        Action action;
        float startTime;

        public void Open()
        {
            action = Action.Open;
            enabled = true;
            startTime = Time.time;
            mTransform.position = mTransform.position.WithX(start);
        }

        public void Close()
        {
            action = Action.Close;
            enabled = true;
            startTime = Time.time;
            mTransform.position = mTransform.position.WithX(end);
        }

        void Update()
        {
            float t = (Time.time - startTime) / durationInSec;
            float x;
            switch (action)
            {
                case Action.Close:
                    x = start + (end - start) * t;
                    break;
                case Action.Open:
                    x = start + (end - start) * t;
                    break;
                default:
                    return;
            }

            mTransform.position = mTransform.position.WithX(x);
        }
    }
}