using System;
using Iviz.Controllers;
using Iviz.Core;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace Iviz.App
{
    public class ARSidePanel : MonoBehaviour
    {
        [CanBeNull] RectTransform mTransform;
        [NotNull] RectTransform Transform => mTransform != null ? mTransform : (mTransform = (RectTransform) transform);

        [SerializeField] GameObject divider = null;
        [SerializeField] Button toggle = null;
        [CanBeNull] LauncherButton selected;

        [NotNull]
        static ARFoundationController Controller =>
            ARController.Instance.CheckedNull() ?? throw new NullReferenceException("AR Controller is not set");

        [SerializeField] PopupButton openPanel = null;
        [SerializeField] PopupButton arVisible = null;
        [SerializeField] PopupButton move = null;
        [SerializeField] PopupButton reposition = null;

        [SerializeField] PopupButton reset = null;

        //[SerializeField] PopupButton pin;
        //[SerializeField] PopupButton meshVisible;
        [SerializeField] PopupButton meshEnabled = null;

        [SerializeField] PopupButton meshReset = null;

        //[SerializeField] PopupButton meshPing;
        [SerializeField] PopupButton qrEnabled = null;
        [SerializeField] PopupButton arucoEnabled = null;
        [SerializeField] PopupButton occlusionEnabled = null;

        [SerializeField] PopupButton tfVisible = null;
        [SerializeField] PopupButton tfText = null;
        [SerializeField] PopupButton tfConnect = null;

        [SerializeField] float shiftLeft = -30;
        [SerializeField] float shiftRight = 40;

        float ScaledShiftLeft => shiftLeft * ModuleListPanel.CanvasScale;
        float ScaledShiftRight => shiftRight * ModuleListPanel.CanvasScale;

        bool active;
        bool visible;

        bool Active
        {
            get => active;
            set
            {
                if (active == value)
                {
                    return;
                }

                active = value;
                var parent = Transform.parent;
                parent.position = parent.position.WithX(active ? ScaledShiftRight : ScaledShiftLeft);
                Hide();
            }
        }

        public bool Visible
        {
            get => visible;
            set
            {
                visible = value;
                if (!value)
                {
                    gameObject.SetActive(false);
                    divider.SetActive(false);
                    toggle.gameObject.SetActive(false);
                    Hide();
                    return;
                }

                if (ARController.Instance == null)
                {
                    return;
                }

                gameObject.SetActive(true);
                divider.SetActive(true);
                toggle.gameObject.SetActive(true);
                Setup();
            }
        }

        void Setup()
        {
            arVisible.Enabled = Controller.Visible;
            move.Enabled = Controller.ShowARJoystick;
            qrEnabled.Enabled = Controller.EnableQrDetection;
            arucoEnabled.Enabled = Controller.EnableArucoDetection;
            meshEnabled.Enabled = Controller.EnableMeshing;
            //pin.Enabled = Controller.PinRootMarker;
            occlusionEnabled.Enabled = Controller.OcclusionQuality != OcclusionQualityType.Off;
            tfVisible.Enabled = TfListener.Instance.Visible;
            tfConnect.Enabled = TfListener.Instance.ParentConnectorVisible;
            tfText.Enabled = TfListener.Instance.FrameLabelsVisible;
        }

        void Awake()
        {
            
            
            ARController.ARStateChanged += OnArEnabledChanged;

            toggle.onClick.AddListener(() =>
            {
                if (ARController.Instance != null)
                {
                    ARController.Instance.ModuleData.ShowPanel();
                }
            });
            arVisible.Clicked += () =>
                Controller.Visible = (arVisible.Enabled = !arVisible.Enabled);
            move.Clicked += () =>
            {
                if (ARController.Instance != null && !ARController.Instance.SetupModeEnabled)
                {
                    ToggleARJoystick();
                }
                else if (move.Enabled)
                {
                    Controller.ShowARJoystick = false;
                    move.Enabled = false;
                }
            };
            reposition.Clicked += () =>
                Controller.ResetSetupMode();
            qrEnabled.Clicked += () =>
                Controller.EnableQrDetection = (qrEnabled.Enabled = !qrEnabled.Enabled);
            arucoEnabled.Clicked += () =>
                Controller.EnableArucoDetection = (arucoEnabled.Enabled = !arucoEnabled.Enabled);
            meshEnabled.Clicked += () =>
                Controller.EnableMeshing = (meshEnabled.Enabled = !meshEnabled.Enabled);
            //pin.Clicked += () =>
            //    Controller.PinRootMarker = (pin.Enabled = !pin.Enabled);
            reset.Clicked += () => Controller.ResetSession();
            meshReset.Clicked += () =>
            {
                Controller.EnableMeshing = false;
                Controller.EnableMeshing = true;
            };
            occlusionEnabled.Clicked += () =>
            {
                occlusionEnabled.Enabled = !occlusionEnabled.Enabled;
                Controller.OcclusionQuality =
                    occlusionEnabled.Enabled ? OcclusionQualityType.Fast : OcclusionQualityType.Off;
            };

            tfVisible.Clicked += () =>
                TfListener.Instance.Visible = (tfVisible.Enabled = !tfVisible.Enabled);
            tfConnect.Clicked += () =>
                TfListener.Instance.ParentConnectorVisible = (tfConnect.Enabled = !tfConnect.Enabled);
            tfText.Clicked += () =>
                TfListener.Instance.FrameLabelsVisible = (tfText.Enabled = !tfText.Enabled);

            Active = true;
        }

        public void ToggleARJoystick()
        {
            Controller.ShowARJoystick = (move.Enabled = !move.Enabled);            
        }

        void OnDestroy()
        {
            ARController.ARStateChanged -= OnArEnabledChanged;
        }

        void OnArEnabledChanged(bool _)
        {
            Visible = Visible;
        }

        public void OnChildSelected(LauncherButton newSelected)
        {
            if (selected != null && newSelected != selected)
            {
                selected.HideChildren();
            }

            if (newSelected != selected)
            {
                Setup();
            }

            selected = newSelected;
        }

        public void OnDividerClicked()
        {
            Active = !Active;
        }

        public void Hide()
        {
            if (selected != null)
            {
                selected.HideChildren();
            }

            selected = null;
        }
    }
}