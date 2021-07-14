using System;
using Iviz.Controllers;
using Iviz.Core;
using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.App
{
    public class ARSidePanel : MonoBehaviour
    {
        [CanBeNull] RectTransform mTransform;
        [NotNull] RectTransform Transform => mTransform != null ? mTransform : (mTransform = (RectTransform) transform);

        [SerializeField] GameObject divider;
        [CanBeNull] LauncherButton selected;

        [NotNull]
        static ARFoundationController Controller =>
            ARController.Instance.CheckedNull() ?? throw new NullReferenceException("AR Controller is not set");

        [SerializeField] PopupButton arVisible;
        [SerializeField] PopupButton move;
        [SerializeField] PopupButton mesh;
        [SerializeField] PopupButton reset;
        [SerializeField] PopupButton pin;
        [SerializeField] PopupButton meshVisible;
        [SerializeField] PopupButton meshEnabled;
        [SerializeField] PopupButton meshReset;
        [SerializeField] PopupButton meshPing;
        [SerializeField] PopupButton qrEnabled;
        [SerializeField] PopupButton arucoEnabled;
        
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
                parent.position = parent.position.WithX(active ? 40 : -30);
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
                    Hide();
                    return;
                }

                if (ARController.Instance == null)
                {
                    return;
                }

                gameObject.SetActive(true);
                divider.SetActive(true);

                arVisible.Enabled = Controller.Visible;
                move.Enabled = Controller.ShowARJoystick;
                mesh.Enabled = Controller.EnableMeshing;
                qrEnabled.Enabled = Controller.EnableQrDetection;
                arucoEnabled.Enabled = Controller.EnableArucoDetection;
                meshEnabled.Enabled = Controller.EnableMeshing;
                pin.Enabled = Controller.PinRootMarker;
                reset.Enabled = Controller.SetupModeEnabled;
            }
        }

        void Awake()
        {
            ARController.ARActiveChanged += OnArEnabledChanged;

            arVisible.Clicked += () =>
                Controller.Visible = (arVisible.Enabled = !arVisible.Enabled);
            move.Clicked += () =>
                Controller.ShowARJoystick = (move.Enabled = !move.Enabled);
            mesh.Clicked += () =>
                Controller.EnableMeshing = (mesh.Enabled = !mesh.Enabled);
            qrEnabled.Clicked += () =>
                Controller.EnableQrDetection = (qrEnabled.Enabled = !qrEnabled.Enabled);
            arucoEnabled.Clicked += () =>
                Controller.EnableArucoDetection = (arucoEnabled.Enabled = !arucoEnabled.Enabled);
            meshEnabled.Clicked += () =>
                Controller.EnableMeshing = meshEnabled.Enabled = !meshEnabled.Enabled;
            pin.Clicked += () =>
                Controller.PinRootMarker = (pin.Enabled = !pin.Enabled);
            reset.Clicked += () => Controller.ResetSession();
            meshReset.Clicked += () =>
            {
                Controller.EnableMeshing = false;
                Controller.EnableMeshing = true;
            };
        }

        void OnDestroy()
        {
            ARController.ARActiveChanged -= OnArEnabledChanged;
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