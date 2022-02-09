#nullable enable

using System;
using External;
using Iviz.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    /// <summary>
    /// The joystick control used to move the AR origin 
    /// </summary>
    public sealed class ARJoystick : MonoBehaviour
    {
        [SerializeField] Button? closeButton;
        [SerializeField] Button? pinDownButton;
        [SerializeField] Button? globalButton;
        [SerializeField] Joystick? joystickX;
        [SerializeField] Joystick? joystickY;
        [SerializeField] Joystick? joystickZ;
        [SerializeField] Joystick? joystickA;

        Button CloseButton => closeButton.AssertNotNull(nameof(closeButton));
        Button GlobalButton => globalButton.AssertNotNull(nameof(globalButton));
        Joystick JoystickX => joystickX.AssertNotNull(nameof(joystickX));
        Joystick JoystickY => joystickY.AssertNotNull(nameof(joystickY));
        Joystick JoystickZ => joystickZ.AssertNotNull(nameof(joystickZ));
        Joystick JoystickA => joystickA.AssertNotNull(nameof(joystickA));

        public bool IsGlobal { get; private set; }
        public event Action<Vector3>? ChangedPosition;
        public event Action<float>? ChangedAngle;
        public event Action? PointerUp;
        public event Action? Close;

        public bool Visible
        {
            set => gameObject.SetActive(value);
        }

        void Awake()
        {
            Visible = false;
            IsGlobal = true;
            GlobalButton.onClick.AddListener(OnButtonClick);
            CloseButton.onClick.AddListener(() => Close?.Invoke());

            JoystickX.Changed += OnChangedPosition;
            JoystickY.Changed += OnChangedPosition;
            JoystickZ.Changed += OnChangedPosition;
            JoystickA.Changed += OnChangedAngle;

            JoystickX.PointerUp += OnPointerUp;
            JoystickY.PointerUp += OnPointerUp;
            JoystickZ.PointerUp += OnPointerUp;
            JoystickA.PointerUp += OnPointerUp;
        }

        void OnButtonClick()
        {
            IsGlobal = !IsGlobal;
            GlobalButton.GetComponentInChildren<Text>().text = IsGlobal ? "Global" : "Screen";
        }

        void OnChangedPosition(Vector2 _)
        {
            var deltaPosition = new Vector3(JoystickX.Direction.x, JoystickY.Direction.x, JoystickZ.Direction.x);
            ChangedPosition?.Invoke(deltaPosition);
        }

        void OnChangedAngle(Vector2 _)
        {
            ChangedAngle?.Invoke(JoystickA.Direction.x);
        }

        void OnPointerUp()
        {
            PointerUp?.Invoke();
        }
    }
}