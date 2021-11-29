#nullable enable

using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Iviz.App
{
    public sealed class LinkResolver : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] TMP_Text? text;

        TMP_Text Text => text != null ? text : text = GetComponent<TMP_Text>();

        public event Action<string>? LinkClicked;

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            int linkIndex = TMP_TextUtilities.FindIntersectingLink(text, Input.mousePosition, null);
            if (linkIndex == -1)
            {
                return;
            }

            var linkInfo = Text.textInfo.linkInfo[linkIndex];
            LinkClicked?.Invoke(linkInfo.GetLinkID());
        }
    }
}