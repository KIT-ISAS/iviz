#nullable enable

using System;
using Iviz.Core;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Iviz.App
{
    public sealed class LinkResolver : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] TMP_Text? text;

        TMP_Text Text => this.EnsureHasComponent(ref text, nameof(text));

        public event Action<string>? LinkClicked;
        public event Action<string>? LinkDoubleClicked;

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            var mainCamera = Settings.IsXR ? Settings.MainCamera : null;
            int linkIndex = TMP_TextUtilities.FindIntersectingLink(text, eventData.pressPosition, mainCamera);

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