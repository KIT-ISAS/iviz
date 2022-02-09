#nullable enable

using System;
using External;
using Iviz.Core;
using UnityEngine;

namespace Iviz.Controllers
{
    public sealed class TwistJoystick : MonoBehaviour
    {
        [SerializeField] FixedJoystick? left;
        [SerializeField] FixedJoystick? right;
        [SerializeField] FixedJoystick? midLeft;
        [SerializeField] FixedJoystick? midRight;
        [SerializeField] Canvas? canvas;
        bool visible;

        FixedJoystick LeftJoystick => left.AssertNotNull(nameof(left));
        FixedJoystick RightJoystick  => right.AssertNotNull(nameof(right));
        FixedJoystick MiddleLeftJoystick => midLeft.AssertNotNull(nameof(midLeft));
        FixedJoystick MiddleRightJoystick  => midRight.AssertNotNull(nameof(midRight));
        Canvas JoystickCanvas => canvas.AssertNotNull(nameof(canvas));
        
        public event Action? Changed;
        
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
        public Vector2 MiddleLeft => MiddleLeftJoystick.Direction;
        public Vector2 MiddleRight => MiddleRightJoystick.Direction;
        
        public bool LeftVisible
        {
            set => LeftJoystick.gameObject.SetActive(value);
        }

        public bool RightVisible
        {
            set => RightJoystick.gameObject.SetActive(value);
        }
        public bool MiddleLeftVisible
        {
            set => MiddleLeftJoystick.gameObject.SetActive(value);
        }
        public bool MiddleRightVisible
        {
            set => MiddleRightJoystick.gameObject.SetActive(value);
        }

        void Awake()
        {
            Visible = false;
            
            // ReSharper disable once ConvertToLocalFunction
            Action<Vector2> onChanged = _ => Changed?.Invoke();
            
            LeftJoystick.Changed += onChanged;
            RightJoystick.Changed += onChanged;
            MiddleLeftJoystick.Changed += onChanged;
            MiddleRightJoystick.Changed += onChanged;

            // ReSharper disable once ConvertToLocalFunction
            Action onPointerUp = () => Changed?.Invoke();

            LeftJoystick.PointerUp += onPointerUp;
            RightJoystick.PointerUp += onPointerUp;
            MiddleLeftJoystick.PointerUp += onPointerUp;
            MiddleRightJoystick.PointerUp += onPointerUp;
        }
    }
}
