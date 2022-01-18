#nullable enable

using System;
using UnityEngine;

namespace Iviz.Controllers.XR
{
    public sealed class HandGestures
    {
        readonly HandType handType;
        bool isPalmClicking;

        public bool IsPalmUp { get; private set; }
        public bool IsHandOpen { get; private set; }
        public bool IsIndexPointing { get; private set; }

        public event Action? GestureChanged;
        public event Action? PalmClicked;

        public HandGestures(HandType handType) => this.handType = handType;

        public void Process(HandState? state)
        {
            if (state == null)
            {
                bool hasGesture = IsPalmUp || IsHandOpen || IsIndexPointing;
                IsPalmUp = false;
                //IsHandOpen = false;
                IsIndexPointing = false;
                if (hasGesture)
                {
                    GestureChanged?.Invoke();
                }

                return;
            }

            bool triggerStateChanged = false;

            var palmPosition = state.Palm.position;
            float thumbDistance = Vector3.Distance(palmPosition, state.ThumbFingertip.position);
            float indexDistance = Vector3.Distance(palmPosition, state.IndexFingertip.position);
            float middleDistance = Vector3.Distance(palmPosition, state.Fingertips[2].position);
            float ringDistance = Vector3.Distance(palmPosition, state.Fingertips[3].position);
            float pinkyDistance = Vector3.Distance(palmPosition, state.Fingertips[4].position);

            if (!IsPalmUp && CheckPalmUp(45 * Mathf.Deg2Rad))
            {
                IsPalmUp = true;
                triggerStateChanged = true;
            }
            else if (IsPalmUp && !CheckPalmUp(0 * Mathf.Deg2Rad))
            {
                IsPalmUp = false;
                triggerStateChanged = true;
            }

            if (!IsHandOpen && CheckPalmOpen(0.08f, 0.105f))
            {
                IsHandOpen = true;
                triggerStateChanged = true;
            }
            else if (IsHandOpen && !CheckPalmOpen(0.07f, 0.095f))
            {
                IsHandOpen = false;
                triggerStateChanged = true;
            }

            if (!IsIndexPointing && CheckIndexPointing(0.10f, 0.05f))
            {
                IsIndexPointing = true;
                triggerStateChanged = true;
            }
            else if (IsIndexPointing && !CheckIndexPointing(0.09f, 0.065f))
            {
                IsIndexPointing = false;
                triggerStateChanged = true;
            }

            if (triggerStateChanged)
            {
                GestureChanged?.Invoke();
            }

            bool CheckPalmUp(float threshold) => -state.Palm.up.y > Mathf.Sin(threshold);

            bool CheckPalmOpen(float thumbThreshold, float otherThreshold) => IsPalmUp
                                                                              && thumbDistance > thumbThreshold
                                                                              && indexDistance > otherThreshold
                                                                              && middleDistance > otherThreshold
                                                                              && ringDistance > otherThreshold
                                                                              && pinkyDistance > otherThreshold * 0.9f;

            bool CheckIndexPointing(float indexThreshold, float otherThreshold)
            {
                float checkedThreshold = IsPalmUp ? otherThreshold : otherThreshold * 1.2f;
                return indexDistance > indexThreshold
                       && middleDistance < checkedThreshold
                       && ringDistance < checkedThreshold
                       && pinkyDistance < checkedThreshold;
            }
        }

        public void Process(HandState? state, HandState? otherState)
        {
            if (state == null || otherState == null)
            {
                isPalmClicking = false;
                return;
            }

            if (!CheckPalmClicking())
            {
                isPalmClicking = false;
            }
            else if (!isPalmClicking)
            {
                isPalmClicking = true;
                PalmClicked?.Invoke();
            }

            bool CheckPalmClicking() => IsPalmUp &&
                                        Vector3.Distance(otherState.IndexFingertip.position,
                                            state.Palm.position + 0.02f * Vector3.up) < 0.035f;
        }
    }
}