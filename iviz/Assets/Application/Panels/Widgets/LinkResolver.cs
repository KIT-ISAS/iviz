#nullable enable

using System;
using Iviz.Core;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Iviz.App
{
    public sealed class LinkResolver : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] TMP_Text? text;

        TMP_Text Text => text != null ? text : text = GetComponent<TMP_Text>();

        public event Action<string>? LinkClicked;
        public event Action<string>? LinkDoubleClicked;

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            int linkIndex = Settings.IsXR
                ? TMP_TextUtilities.FindIntersectingLink(text, eventData.pressPosition, Settings.MainCamera)
                : TMP_TextUtilities.FindIntersectingLink(text, Input.mousePosition, null);

            if (linkIndex == -1)
            {
                return;
            }

            var linkInfo = Text.textInfo.linkInfo[linkIndex];
            var eventToInvoke = eventData.clickCount == 1
                ? LinkClicked
                : LinkDoubleClicked;
            eventToInvoke?.Invoke(linkInfo.GetLinkID());
        }
    }
}