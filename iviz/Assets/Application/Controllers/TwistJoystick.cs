using System;
using UnityEngine;
using External;

namespace Iviz.Controllers
{
    public sealed class TwistJoystick : MonoBehaviour
    {
        [SerializeField] FixedJoystick left = null;
        [SerializeField] FixedJoystick right = null;
        [SerializeField] Canvas canvas = null;

        public enum Source
        {
            Left,
            Right
        }

        public event Action<Source, Vector2> Changed;
        
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

        bool rightJoystickVisible;
        public bool RightJoystickVisible
        {
            get => right.gameObject.activeSelf;
            set
            {
                rightJoystickVisible = value;
                right.gameObject.SetActive(value);
            }
        }        

        public Vector2 Left => left.Direction;

        public Vector2 Right => right.Direction;

        void Awake()
        {
            Visible = false;
            left.Changed += direction => Changed?.Invoke(Source.Left, direction);
            right.Changed += direction => Changed?.Invoke(Source.Right, direction);
        }
    }
}
