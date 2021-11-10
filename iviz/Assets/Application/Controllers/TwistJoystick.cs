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
        bool visible;

        public enum Source
        {
            Left,
            Right
        }

        public event Action<Source, Vector2> Changed;
        
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
            left.Changed += direction => Changed?.Invoke(Source.Left, direction);
            right.Changed += direction => Changed?.Invoke(Source.Right, direction);
        }
    }
}
