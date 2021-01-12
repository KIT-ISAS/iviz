using System;
using External;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public class ARJoystick : MonoBehaviour
    {
        [SerializeField] Button button = null;
        [SerializeField] Joystick joystickX = null;
        [SerializeField] Joystick joystickY = null;
        [SerializeField] Joystick joystickZ = null;
        [SerializeField] Joystick joystickA = null;

        public bool IsGlobal { get; private set; }
        public event Action<Vector3> ChangedPosition;
        public event Action<float> ChangedAngle;
        public event Action PointerUp;

        public bool Visible
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        void Awake()
        {
            Visible = false;
            IsGlobal = true;
            button.onClick.AddListener(OnButtonClick);
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
            button.GetComponentInChildren<Text>().text = IsGlobal ? "Global" : "Screen";
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