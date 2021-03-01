using Iviz.Core;
using UnityEngine;
using UnityEngine.EventSystems;

#if UNITY_WSA
using Microsoft.MixedReality.Toolkit.Input;
#endif

namespace Iviz.Displays
{
    /// <summary>
    /// Simple node that makes a resource brighter if the pointer is on top of it.
    /// </summary>
    public sealed class MouseOverHighlighter : MonoBehaviour,
        IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler
    {
        MeshMarkerResource resource;

        static Color GetHighlightColor(Color c)
        {
            const float factor = 0.5f;
            float r = Mathf.Min(c.r + factor, 1);
            float g = Mathf.Min(c.g + factor, 1);
            float b = Mathf.Min(c.b + factor, 1);
            float a = Mathf.Min(c.a + factor, 1);
            return new Color(r, g, b, a);
        }

        void Start()
        {
            resource = GetComponent<MeshMarkerResource>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (Settings.IsHololens)
            {
                // hololens uses gaze, not pointer                
                return;
            }

            resource.EmissiveColor = GetHighlightColor(Color.black);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (Settings.IsHololens)
            {
                // hololens uses gaze, not pointer                
                return;
            }

            resource.EmissiveColor = Color.black;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!Settings.IsPhone)
            {
                return;
            }

            resource.EmissiveColor = GetHighlightColor(Color.black);
        }
        
        public void OnPointerUp(PointerEventData eventData)
        {
            if (!Settings.IsPhone)
            {
                return;
            }

            resource.EmissiveColor = Color.black;
        }


    }
}