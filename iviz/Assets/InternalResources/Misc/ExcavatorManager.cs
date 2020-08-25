using UnityEngine;

namespace ISAS
{

    public class ExcavatorManager : MonoBehaviour
    {
        double startTime;
        Vector3 startPosition;
        float startAngle;
        Vector3 positionToDig;
        Vector3 targetPosition;
        float targetAngle;

        bool reachedTarget = true;

        const double RotationTime = 1000;
        const double TravelSpeed = 3f;
        const int AnimationTime = 3000;
        const float PullbackDistance = 3f;

        public Animator animator;
        public Transform targetObject;

        //public DistributionManager distributionManager;

        public double extraWaitTime = 0;

        // Start is called before the first frame update
        void Start()
        {
        }

        public void SetNewPositionToDig(Vector3 newPositionToDig)
        {
            if (!reachedTarget)
            {
                return;
            }
            //Debug.Log(newPositionToDig + " " + positionToDig);
            if ((newPositionToDig - positionToDig).sqrMagnitude < 0.01f)
            {
                return;
            }
            positionToDig = newPositionToDig;
            startTime = Time.time * 1000;
            startAngle = transform.rotation.eulerAngles.y;
            startPosition = transform.localPosition;

            Vector3 delta = startPosition - positionToDig;
            float targetAngleInRad = (delta.z == delta.x) ? -Mathf.PI/4 : Mathf.Atan2(delta.z, -delta.x);
            targetAngle = targetAngleInRad * Mathf.Rad2Deg;
            if (targetAngle - startAngle > 180) {
                targetAngle -= 360;
            }
            if (targetAngle - startAngle < -180)
            {
                targetAngle += 360;
            }
            reachedTarget = false;

            targetPosition = positionToDig + PullbackDistance * new Vector3(-Mathf.Cos(targetAngleInRad), 0, Mathf.Sin(targetAngleInRad));

        }

        // Update is called once per frame
        void Update()
        {
            SetNewPositionToDig(targetObject.position);

            if (reachedTarget)
            {
                return;
            }

            double time = Time.time * 1000;
            if (time - startTime < RotationTime)
            {
                float t = (float)((time - startTime) / RotationTime);
                float newAngle = startAngle + t * (targetAngle - startAngle);
                transform.localRotation = Quaternion.Euler(0, newAngle, 0);
                return;
            }

            double travelStartTime = startTime + RotationTime;
            double totalTravelTime = (targetPosition - startPosition).magnitude / TravelSpeed * 1000;
            if (time - travelStartTime < totalTravelTime)
            {
                float t = (float)((time - travelStartTime) / totalTravelTime);
                Vector3 newPosition = startPosition + t * (targetPosition - startPosition);
                transform.localPosition = newPosition;
                return;
            }
            if (!animator.enabled)
            {
                animator.enabled = true;
                animator.Rebind();
            }
            double finalTime = startTime + RotationTime + totalTravelTime + extraWaitTime + AnimationTime;
            if (time > finalTime)
            {
                reachedTarget = true;
                animator.enabled = false;
            }

        }
    }
}