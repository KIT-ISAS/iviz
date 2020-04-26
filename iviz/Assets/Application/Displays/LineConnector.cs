using UnityEngine;

namespace Iviz.App
{
    public class LineConnector : MonoBehaviour
    {
        public Transform A;
        public Transform B;

        LineRenderer line;
        readonly Vector3[] positions = new Vector3[2];

        public float LineWidth
        {
            get => line.startWidth;
            set
            {
                line.startWidth = value;
                line.endWidth = value;
            }
        }

        void Awake()
        {
            line = GetComponent<LineRenderer>();
            line.positionCount = 2;
            line.startColor = Color.yellow;
            line.endColor = Color.red;
        }

        void Update()
        {
            if (A == null || B == null)
            {
                return;
            }
            positions[0] = A.position;
            positions[1] = B.position;
            line.SetPositions(positions);
        }
    }
}