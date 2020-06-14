using UnityEngine;
using System.Collections;

namespace Iviz.App
{
    public class Joystick : MonoBehaviour
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
    }
}
