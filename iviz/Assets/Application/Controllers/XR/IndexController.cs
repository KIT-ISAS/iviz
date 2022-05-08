#nullable enable

using System.Collections.Generic;
using Iviz.Core;
using Iviz.Core.XR;
using UnityEngine;
using UnityEngine.SpatialTracking;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

namespace Iviz.Controllers.XR
{
    public sealed class IndexController : CustomController
    {
        [SerializeField] XRDirectInteractor? sphereInteractor;
        XRDirectInteractor SphereInteractor => sphereInteractor.AssertNotNull(nameof(sphereInteractor));
        Collider? interactable;

        public bool IsHovering { get; private set; }
        public bool HandButtonState { get; set; }

        void OnHoverEntered(HoverEnterEventArgs args)
        {
            IsHovering = true;
            args.interactable.TryGetComponent(out interactable);
        }

        void OnHoverExited(HoverExitEventArgs _)
        {
            IsHovering = false;
            interactable = null;
        }

        protected override void Awake()
        {
            // no base.Awake() here, this is a dummy controller
            SphereInteractor.hoverEntered.AddListener(OnHoverEntered);
            SphereInteractor.hoverExited.AddListener(OnHoverExited);
        }

        protected override bool MatchesDevice(InputDeviceCharacteristics characteristics,
            List<InputFeatureUsage> usages) => false;


        protected override void UpdateInput(XRControllerState? controllerState)
        {
            base.UpdateInput(controllerState);
            if (controllerState == null)
            {
                return;
            }

            controllerState.ResetFrameDependentStates();
            if (!IsHovering)
            {
                controllerState.selectInteractionState.SetFrameState(false);
                controllerState.uiPressInteractionState.SetFrameState(false);
                return;
            }

            bool buttonState = HandButtonState;
            if (interactable != null)
            {
                const float minDistanceToClick = 0.02f;
                var myPosition = transform.position;
                var closestPoint = interactable.ClosestPoint(myPosition);
                buttonState |= (closestPoint - myPosition).sqrMagnitude < minDistanceToClick * minDistanceToClick;
            }

            controllerState.selectInteractionState.SetFrameState(buttonState);
            controllerState.uiPressInteractionState.SetFrameState(buttonState);
        }
    }
}