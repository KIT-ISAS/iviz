using System;
using System.Linq;
using Iviz.Core;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Iviz.App
{
    public class LauncherButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        static readonly Color EnabledColorFrame = new Color(0.2f, 0.3f, 0.4f); 
        
        [SerializeField] ARSidePanel arSidePanel = null;
        [SerializeField] GameObject children = null;
        [SerializeField] Image frame = null;
        
        float? popupAnimationStart;
        float clickStart;
        
        void Awake()
        {
            children.SetActive(false);
            if (frame != null)
            {
                frame.color = EnabledColorFrame;
            }            
        }

        public void HideChildren()
        {
            children.SetActive(false);
        }

        void OnClick()
        {
            if (children.activeSelf)
            {
                HideChildren();
                return;
            }

            arSidePanel.OnChildSelected(this);
            popupAnimationStart = GameThread.GameTime;
        }

        void OnButtonLongClick()
        {
            if (children.activeSelf)
            {
                HideChildren();
                return;
            }
        }

        void Update()
        {
            if (children == null || popupAnimationStart == null)
            {
                return;
            }

            const float duration = 0.1f;

            children.SetActive(true);
            float t = (GameThread.GameTime - popupAnimationStart.Value) / duration;
            if (t >= 1)
            {
                children.transform.localScale = Vector3.one;
                popupAnimationStart = null;
            }
            else
            {
                children.transform.localScale = Mathf.Sqrt(t) * Vector3.one;
            }
        }

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            clickStart = GameThread.GameTime;
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            float time = GameThread.GameTime;
            if (time - clickStart > 1)
            {
                OnButtonLongClick();
            }
            else
            {
                OnClick();
            }

            clickStart = 0;
        }

        public void OnChildButtonClicked()
        {
            HideChildren();
            arSidePanel.Hide();
        }
    }
}