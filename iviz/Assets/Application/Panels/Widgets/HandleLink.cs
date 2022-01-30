using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Iviz.App
{
    public sealed class HandleLink : MonoBehaviour, IPointerUpHandler
    {
        public event Action Clicked;

        void IPointerUpHandler.OnPointerUp(PointerEventData _)
        {
            Clicked?.Invoke();
        }
    }
}