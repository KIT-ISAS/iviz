#nullable enable

using System;
using External;
using Iviz.Core;
using TMPro;
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
        [SerializeField] Button? globalButton;
        [SerializeField] Joystick? joystickX;
        [SerializeField] Joystick? joystickY;
        [SerializeField] Joystick? joystickZ;
        [SerializeField] Joystick? joystickA;
        [SerializeField] Joystick? joystickS;
        [SerializeField] Button? resetScale;
        [SerializeField] TMP_Text? globalButtonText;

        Button CloseButton => closeButton.AssertNotNull(nameof(closeButton));
        Button GlobalButton => globalButton.AssertNotNull(nameof(globalButton));
        Joystick JoystickX => joystickX.AssertNotNull(nameof(joystickX));
        Joystick JoystickY => joystickY.AssertNotNull(nameof(joystickY));
        Joystick JoystickZ => joystickZ.AssertNotNull(nameof(joystickZ));
        Joystick JoystickA => joystickA.AssertNotNull(nameof(joystickA));
        Joystick JoystickS => joystickS.AssertNotNull(nameof(joystickS));
        Button ResetScaleButton => resetScale.AssertNotNull(nameof(resetScale));
        TMP_Text GlobalButtonText => globalButtonText.AssertNotNull(nameof(globalButtonText));

        public bool IsGlobal { get; private set; }
        public event Action<Vector3>? ChangedPosition;
        public event Action<float>? ChangedAngle;
        public event Action<float>? ChangedScale;
        public event Action? ResetScale;
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
            ResetScaleButton.onClick.AddListener(() => ResetScale?.Invoke());

            Action<Vector2> onChangedPosition = OnChangedPosition;
            JoystickX.Changed += onChangedPosition;
            JoystickY.Changed += onChangedPosition;
            JoystickZ.Changed += onChangedPosition;
            
            JoystickA.Changed += OnChangedAngle;
            JoystickS.Changed += OnChangedScale;

            Action onPointerUp = OnPointerUp;
            JoystickX.PointerUp += onPointerUp;
            JoystickY.PointerUp += onPointerUp;
            JoystickZ.PointerUp += onPointerUp;
            JoystickA.PointerUp += onPointerUp;
            JoystickS.PointerUp += onPointerUp;
        }

        void OnButtonClick()
        {
            IsGlobal = !IsGlobal;
            GlobalButtonText.text = IsGlobal ? "Global" : "Screen";
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

        void OnChangedScale(Vector2 _)
        {
            ChangedScale?.Invoke(JoystickS.Direction.x);
        }

        void OnPointerUp()
        {
            PointerUp?.Invoke();
        }
    }
}