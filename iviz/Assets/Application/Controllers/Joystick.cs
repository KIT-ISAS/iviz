using UnityEngine;
using External;

namespace Iviz.Controllers
{
    public sealed class Joystick : MonoBehaviour
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
    }
}
