using System;
using Iviz.App.Listeners;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Iviz.App
{
    public class TFLink : MonoBehaviour, IPointerClickHandler
    {
        TMP_Text text;

        void Awake()
        {
            text = GetComponent<TMP_Text>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            int linkIndex = TMP_TextUtilities.FindIntersectingLink(text, Input.mousePosition, TFListener.MainCamera);
            if (linkIndex != -1)
            { 
                TMP_LinkInfo linkInfo = text.textInfo.linkInfo[linkIndex];
                Debug.Log(linkInfo.GetLinkText());
            }
        }
    }
}
