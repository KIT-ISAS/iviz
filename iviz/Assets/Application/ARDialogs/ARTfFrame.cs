using Iviz.Core;
using Iviz.Displays;
using TMPro;
using UnityEngine;

namespace Iviz.App.ARDialogs
{
    public class ARTfFrame : MarkerResource
    {
        [SerializeField] Transform pivotTransform;
        [SerializeField] bool billboard;
        [SerializeField] TMP_Text text;
        float? currentAngle;

        public string Caption
        {
            get => text.text;
            set => text.text = value;
        }

        void Update()
        {
            if (billboard)
            {
                UpdateRotation();
            }
        }
        
        void UpdateRotation()
        {
            (float x, _, float z) = Transform.position - Settings.MainCameraTransform.position;
            float targetAngle = -Mathf.Atan2(z, x) * Mathf.Rad2Deg + 180;

            if (currentAngle == null)
            {
                currentAngle = targetAngle;
            }
            else
            {
                float alternativeAngle = targetAngle - 360;
                float closestAngle = Mathf.Abs(alternativeAngle - currentAngle.Value) <
                                     Mathf.Abs(targetAngle - currentAngle.Value)
                    ? alternativeAngle
                    : targetAngle;

                float deltaAngle = closestAngle - currentAngle.Value;
                if (Mathf.Abs(deltaAngle) < 1)
                {
                    return;
                }

                currentAngle = currentAngle.Value + deltaAngle * 0.02f;
                if (currentAngle > 180)
                {
                    currentAngle -= 360;
                }
                else if (currentAngle < -180)
                {
                    currentAngle += 360;
                }
            }

            pivotTransform.rotation = Quaternion.AngleAxis(currentAngle.Value, Vector3.up);
        }        
        
    }
}