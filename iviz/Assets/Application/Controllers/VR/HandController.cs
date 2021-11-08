using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SpatialTracking;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using CommonUsages = UnityEngine.XR.CommonUsages;
using InputDevice = UnityEngine.XR.InputDevice;

namespace Iviz.Controllers
{
    public sealed class MyController : XRBaseController
    {
        float x = 0;
        /// <inheritdoc />
        protected override void UpdateTrackingInput(XRControllerState controllerState)
        {
            base.UpdateTrackingInput(controllerState);
            if (controllerState == null)
                return;

            x += 0.01f;
            controllerState.poseDataFlags = PoseDataFlags.NoData;
            controllerState.position = new Vector3(x, 0, 0);
            controllerState.poseDataFlags |= PoseDataFlags.Position;

            /*
            {
                if (inputDevice.TryGetFeatureValue(CommonUsages.trackingState, out var trackingState))
                {
                    if ((trackingState & InputTrackingState.Position) != 0 &&
                        inputDevice.TryGetFeatureValue(CommonUsages.devicePosition, out var devicePosition))
                    {
                        controllerState.position = devicePosition;
                        controllerState.poseDataFlags |= PoseDataFlags.Position;
                    }

                    if ((trackingState & InputTrackingState.Rotation) != 0 &&
                        inputDevice.TryGetFeatureValue(CommonUsages.deviceRotation, out var deviceRotation))
                    {
                        controllerState.rotation = deviceRotation;
                        controllerState.poseDataFlags |= PoseDataFlags.Rotation;
                    }
                }
            }
            */
        }

        /// <inheritdoc />
        protected override void UpdateInput(XRControllerState controllerState)
        {
            base.UpdateInput(controllerState);
            if (controllerState == null)
                return;

            controllerState.ResetFrameDependentStates();
            controllerState.selectInteractionState.SetFrameState(IsPressed(m_SelectUsage));
            controllerState.activateInteractionState.SetFrameState(IsPressed(m_ActivateUsage));
            controllerState.uiPressInteractionState.SetFrameState(IsPressed(m_UIPressUsage));
        }

        bool IsPressed(InputHelpers.Button button)
        {
            return false;
        }

        /// <inheritdoc />
        public override bool SendHapticImpulse(float amplitude, float duration)
        {
            return false;
        }
    }
}
