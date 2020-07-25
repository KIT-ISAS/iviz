using UnityEngine;

namespace Iviz.Displays
{
    public sealed class LineConnector : MonoBehaviour, IDisplay
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

        public string Name => "LineConnector";
        public Bounds Bounds => new Bounds();
        public Bounds WorldBounds => new Bounds();
        public Pose WorldPose => transform.AsPose();
        public Vector3 WorldScale => transform.lossyScale;

        public bool ColliderEnabled { get => false; set { } }

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

        public Transform Parent
        {
            get => transform.parent;
            set => transform.parent = value;
        }

        public bool Visible
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }
        public int Layer
        {
            get => gameObject.layer;
            set => gameObject.layer = value;
        }

        public void Suspend()
        {
        }
    }
}