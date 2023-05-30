#nullable enable

using System.Collections.Generic;
using Iviz.Core;
using Iviz.Core.XR;
using Iviz.Displays;
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

        public bool IsHovering => interactable != null;
        public bool HandButtonState { get; set; }

        void OnHoverEntered(HoverEnterEventArgs args)
        {
            args.interactable.TryGetComponent(out interactable);
        }

        void OnHoverExited(HoverExitEventArgs _)
        {
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

            if (!IsActiveInFrame || !IsHovering)
            {
                controllerState.selectInteractionState.SetFrameState(false);
                controllerState.uiPressInteractionState.SetFrameState(false);

                if (interactable != null && interactable.TryGetComponent<XRScreenDraggable>(out var draggable))
                {
                    // force unregister hover when device stops tracking, for some reason this does not happen automatically 
                    draggable.UnregisterHover(gameObject);
                    interactable = null;
                }

                return;
            }

            bool buttonState = HandButtonState;
            if (interactable != null)
            {
                const float minDistanceToClick = 0.04f;
                var myPosition = transform.position;
                var closestPoint = interactable.ClosestPoint(myPosition);
                buttonState |= (closestPoint - myPosition).sqrMagnitude < minDistanceToClick * minDistanceToClick;
            }

            controllerState.selectInteractionState.SetFrameState(buttonState);
            controllerState.uiPressInteractionState.SetFrameState(buttonState);
        }
    }
}