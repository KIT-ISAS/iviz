#nullable enable

using System;
using UnityEngine;
using External;
using Iviz.Core;

namespace Iviz.Controllers
{
    public sealed class TwistJoystick : MonoBehaviour
    {
        [SerializeField] FixedJoystick? left;
        [SerializeField] FixedJoystick? right;
        [SerializeField] Canvas? canvas;
        bool visible;

        FixedJoystick LeftJoystick => left.AssertNotNull(nameof(left));
        FixedJoystick RightJoystick  => right.AssertNotNull(nameof(right));
        Canvas JoystickCanvas => canvas.AssertNotNull(nameof(canvas));

        public enum Source
        {
            Left,
            Right
        }

        public event Action<Source, Vector2>? Changed;
        
        public bool Visible
        {
            set
            {
                visible = value;
                JoystickCanvas.gameObject.SetActive(visible);
            }
        }

        public Vector2 Left => LeftJoystick.Direction;

        public Vector2 Right => RightJoystick.Direction;

        void Awake()
        {
            Visible = false;
            LeftJoystick.Changed += direction => Changed?.Invoke(Source.Left, direction);
            RightJoystick.Changed += direction => Changed?.Invoke(Source.Right, direction);
        }
    }
}
