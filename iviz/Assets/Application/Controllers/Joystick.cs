using UnityEngine;
using System.Collections;
using Iviz.App.Listeners;

namespace Iviz.App
{
    public interface IBlocksPointer
    {
        bool IsPointerOnGui(Vector2 PointerPosition);
    }

    public class Joystick : MonoBehaviour, IBlocksPointer
    {
        [SerializeField] FixedJoystick left = null;
        [SerializeField] FixedJoystick right = null;
        [SerializeField] Canvas canvas = null;

        bool visible_;
        public bool Visible
        {
            get => visible_;
            set
            {
                visible_ = value;
                canvas.gameObject.SetActive(visible_);
            }
        }

        public Vector2 Left => left.Direction;

        public Vector2 Right => right.Direction;

        void Awake()
        {
            Visible = false;
        }

        public bool IsPointerOnGui(Vector2 PointerPosition)
        {
            Camera MainCamera = TFListener.MainCamera;
            return Visible &&
                (RectTransformUtility.RectangleContainsScreenPoint(left.transform as RectTransform, PointerPosition, MainCamera) ||
                RectTransformUtility.RectangleContainsScreenPoint(right.transform as RectTransform, PointerPosition, MainCamera));
        }
    }
}
