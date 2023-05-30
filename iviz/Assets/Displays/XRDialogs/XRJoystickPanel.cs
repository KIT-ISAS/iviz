#nullable enable

using System;
using Iviz.Core;
using Iviz.Displays.XR;
using Iviz.Resources;
using UnityEngine;

namespace Iviz.Displays.XRDialogs
{
    public class XRJoystickPanel : MonoBehaviour
    {
        [SerializeField] XRButton? closeButton;
        [SerializeField] XRJoystick? joystickX;
        [SerializeField] XRJoystick? joystickY;
        [SerializeField] XRJoystick? joystickZ;
        [SerializeField] XRJoystick? joystickS;
        [SerializeField] XRJoystick? joystickA;
        [SerializeField] bool backgroundVisible;
        RoundedPlaneDisplay? background;

        RoundedPlaneDisplay Background => ResourcePool.RentChecked(ref background, transform);
        XRButton CloseButton => closeButton.AssertNotNull(nameof(closeButton));
        XRJoystick JoystickX => joystickX.AssertNotNull(nameof(joystickX));
        XRJoystick JoystickY => joystickY.AssertNotNull(nameof(joystickY));
        XRJoystick JoystickZ => joystickZ.AssertNotNull(nameof(joystickZ));
        XRJoystick JoystickA => joystickA.AssertNotNull(nameof(joystickA));
        XRJoystick JoystickS => joystickS.AssertNotNull(nameof(joystickS));

        public event Action<Vector3>? ChangedPosition;
        public event Action<float>? ChangedAngle;
        public event Action<float>? ChangedScale;
        public event Action? PointerUp;
        public event Action? Close;
        
        public bool Visible
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        void Awake()
        {
            CloseButton.Clicked += () => Close.TryRaise(this);

            JoystickX.Changed += f => ChangedPosition.TryRaise(new Vector3(f, 0, 0), this);
            JoystickY.Changed += f => ChangedPosition.TryRaise(new Vector3(0, f, 0), this);
            JoystickZ.Changed += f => ChangedPosition.TryRaise(new Vector3(0, 0, f), this);
            
            JoystickA.Changed += f => ChangedAngle.TryRaise(f, this);
            JoystickS.Changed += f => ChangedScale.TryRaise(f, this);

            Action onPointerUp = OnPointerUp;
            JoystickX.PointerUp += onPointerUp;
            JoystickY.PointerUp += onPointerUp;
            JoystickZ.PointerUp += onPointerUp;
            JoystickA.PointerUp += onPointerUp;
            JoystickS.PointerUp += onPointerUp;
            
            void OnPointerUp() => PointerUp.TryRaise(this);
            
            if (backgroundVisible)
            {
                Background.Size = new Vector2(5f, 1.3f);
                Background.Transform.localPosition = new Vector3(0, 0.125f, 0);
                Background.Radius = 0.3f;
                Background.Color = Resource.Colors.TooltipBackground;
            }
        }
    }
}