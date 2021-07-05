using System.Linq;
using Iviz.Core;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Iviz.App.SideCanvas
{
    public class PopupButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        Button button;
        GameObject children;
        float? animationStart;
        
        void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(OnClick);
            children = transform.Cast<Transform>().FirstOrDefault(t => t.gameObject.name == "Children")?.gameObject;
        }

        void OnClick()
        {
            animationStart = GameThread.GameTime;
        }

        void Update()
        {
            const float duration = 0.1f;

            if (animationStart == null)
            {
                return;
            }

            children.SetActive(true);
            float t = (GameThread.GameTime - animationStart.Value) / duration;
            if (t >= 1)
            {
                children.transform.localScale = Vector3.one;
                animationStart = null;
            }
            else
            {
                children.transform.localScale = Mathf.Sqrt(t) * Vector3.one;
            }
        }
        
        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("pointer down");
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            Debug.Log("pointer up");
        }
    }
}