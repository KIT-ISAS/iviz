using UnityEngine;
using External;

namespace Iviz.Controllers
{
    public interface IBlocksPointer
    {
        bool IsPointerOnGui(in Vector2 pointerPosition);
    }

    public sealed class Joystick : MonoBehaviour, IBlocksPointer
    {
        [SerializeField] FixedJoystick left = null;
        [SerializeField] FixedJoystick right = null;
        [SerializeField] Canvas canvas = null;

        bool visible;
        public bool Visible
        {
            get => visible;
            set
            {
                visible = value;
                canvas.gameObject.SetActive(visible);
            }
        }

        public Vector2 Left => left.Direction;

        public Vector2 Right => right.Direction;

        void Awake()
        {
            Visible = false;
        }

        public bool IsPointerOnGui(in Vector2 pointerPosition)
        {
            Camera mainCamera = TfListener.MainCamera;
            return Visible &&
                (RectTransformUtility.RectangleContainsScreenPoint(left.transform as RectTransform, pointerPosition, mainCamera) ||
                RectTransformUtility.RectangleContainsScreenPoint(right.transform as RectTransform, pointerPosition, mainCamera));
        }
    }
}
