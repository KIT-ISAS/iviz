using System;
using External;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Iviz.App
{
    public class ARJoystick : MonoBehaviour
    {
        [SerializeField] Button closeButton = null;
        [SerializeField] Button pinDownButton = null;
        [SerializeField] Button globalButton = null;
        [SerializeField] Joystick joystickX = null;
        [SerializeField] Joystick joystickY = null;
        [SerializeField] Joystick joystickZ = null;
        [SerializeField] Joystick joystickA = null;

        public bool IsGlobal { get; private set; }
        public event Action<Vector3> ChangedPosition;
        public event Action<float> ChangedAngle;
        public event Action PointerUp;
        public event Action Close;
        public event Action PinDown;

        public bool Visible
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        void Awake()
        {
            Visible = false;
            IsGlobal = true;
            globalButton.onClick.AddListener(OnButtonClick);
            closeButton.onClick.AddListener(() => Close?.Invoke());
            pinDownButton.onClick.AddListener(() => PinDown?.Invoke());

            joystickX.Changed += OnChangedPosition;
            joystickY.Changed += OnChangedPosition;
            joystickZ.Changed += OnChangedPosition;
            joystickA.Changed += OnChangedAngle;
            
            joystickX.PointerUp += OnPointerUp;
            joystickY.PointerUp += OnPointerUp;
            joystickZ.PointerUp += OnPointerUp;
            joystickA.PointerUp += OnPointerUp;
            
        }

        void OnButtonClick()
        {
            IsGlobal = !IsGlobal;
            globalButton.GetComponentInChildren<Text>().text = IsGlobal ? "Global" : "Screen";
        }

        void OnChangedPosition(Vector2 _)
        {
            var deltaPosition = new Vector3(joystickX.Direction.x, joystickY.Direction.x, joystickZ.Direction.x);
            ChangedPosition?.Invoke(deltaPosition);
        }

        void OnChangedAngle(Vector2 _)
        {
            ChangedAngle?.Invoke(joystickA.Direction.x);
        }

        void OnPointerUp()
        {
            PointerUp?.Invoke();
        }
    }
}