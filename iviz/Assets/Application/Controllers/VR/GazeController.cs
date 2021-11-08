#nullable enable

using UnityEngine;
using UnityEngine.SpatialTracking;
using UnityEngine.XR.Interaction.Toolkit;

namespace Iviz.Controllers
{
    public sealed class HandController : XRBaseController
    {
        public enum HandType
        {
            Left,
            Right
        }

        [SerializeField] HandType handType;
        
        /// <inheritdoc />
        protected override void UpdateTrackingInput(XRControllerState? controllerState)
        {
            base.UpdateTrackingInput(controllerState);
            if (controllerState == null)
            {
                return;
            }

            //controllerState.poseDataFlags = PoseDataFlags.NoData;
            //controllerState.position = new Vector3(x, 0, 0);
            //controllerState.poseDataFlags |= PoseDataFlags.Position;

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
        protected override void UpdateInput(XRControllerState? controllerState)
        {
            base.UpdateInput(controllerState);
            if (controllerState == null)
            {
                return;
            }

            controllerState.ResetFrameDependentStates();
            //controllerState.selectInteractionState.SetFrameState(IsPressed(m_SelectUsage));
            //controllerState.activateInteractionState.SetFrameState(IsPressed(m_ActivateUsage));
            //controllerState.uiPressInteractionState.SetFrameState(IsPressed(m_UIPressUsage));
        }

        /// <inheritdoc />
        public override bool SendHapticImpulse(float amplitude, float duration)
        {
            return false;
        }
    }
}
