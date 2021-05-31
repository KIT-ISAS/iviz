using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Iviz.App
{
    public sealed class LinkResolver : MonoBehaviour, IPointerClickHandler
    {
        TMP_Text text;

        public event Action<string> LinkClicked;

        void Awake()
        {
            text = GetComponent<TMP_Text>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            int linkIndex = TMP_TextUtilities.FindIntersectingLink(text, Input.mousePosition, null);
            if (linkIndex == -1)
            {
                return;
            }
            
            TMP_LinkInfo linkInfo = text.textInfo.linkInfo[linkIndex];
            LinkClicked?.Invoke(linkInfo.GetLinkID());
        }
    }
}
