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
        }

        void OnButtonClick()
        {
            IsGlobal = !IsGlobal;
            button.GetComponent<Text>().text = IsGlobal ? "Global" : "Screen";
        }

        void OnChangedPosition(Vector2 _)
        {
            ChangedPosition?.Invoke(new Vector3(joystickX.Direction.x, joystickY.Direction.x, joystickZ.Direction.x));
        }

        void OnChangedAngle(Vector2 _)
        {
            ChangedAngle?.Invoke(joystickA.Direction.x);
        }
    }
}