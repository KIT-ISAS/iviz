using Iviz.App;
using Iviz.Displays;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Application.Displays
{
    /// <summary>
    /// Simple node that makes a resource brighter if the pointer is on top of it.
    /// Self-disables in smartphones and tablets because they have no concept of pointer 'hover'.
    /// </summary>
    public sealed class MouseOverHighlighter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        Color color;   
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
            if (GuiCamera.IsPhone)
            {
                enabled = false;
                return;
            }

            resource = GetComponent<MeshMarkerResource>();
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            color = resource.EmissiveColor;
            resource.EmissiveColor = GetHighlightColor(color);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            resource.EmissiveColor = color;
        }
    }
}