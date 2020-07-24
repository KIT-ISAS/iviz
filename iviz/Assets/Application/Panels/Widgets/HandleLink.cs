using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Iviz.App
{
    public class HandleLink : MonoBehaviour, IPointerClickHandler, IPointerUpHandler
    {
        public event Action Clicked;
        
        public void OnPointerClick(PointerEventData eventData)
        {
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Clicked?.Invoke();
        }
    }
}