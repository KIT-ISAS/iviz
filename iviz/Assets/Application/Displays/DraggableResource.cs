using Iviz.App;
using Iviz.App.Listeners;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

namespace Application.Displays
{
    public class DraggableResource : MonoBehaviour, IPointerDownHandler, IDraggable
    {
        Vector3 line = Vector3.right;

        static Vector3 ClosestPoint(in Ray ray, in Ray other)
        {
            Matrix4x4 M = Matrix4x4.identity;
            M.SetColumn(0, ray.direction);
            M.SetColumn(1, Vector3.Cross(ray.direction, other.direction));
            M.SetColumn(2, -other.direction);

            Vector3 t = M.inverse * (other.origin - ray.origin);

            return ray.origin + t.x * ray.direction;
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {

        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("Starg!");
            TFListener.GuiManager.DraggedObject = this;
        }

        public void OnPointerMove(Vector2 pos)
        {
            Ray ray = new Ray(transform.position, line);
            Ray other = TFListener.MainCamera.ScreenPointToRay(pos);
            Debug.Log(TFListener.MainCamera.gameObject.name);
            transform.position = ClosestPoint(ray, other);
            
        }
    }
}