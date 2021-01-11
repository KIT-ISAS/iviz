using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Iviz.Displays
{
    public sealed class LineConnector : MonoBehaviour, IDisplay
    {
        [field: SerializeField] public Transform A { get; set; }
        [field: SerializeField] public Transform B { get; set;  }

        LineRenderer line;
        readonly Vector3[] positions = new Vector3[2];
        
        [NotNull]
        public string Name
        {
            get => gameObject.name;
            set => gameObject.name = value ?? throw new ArgumentNullException(nameof(value));
        }        
        
        public float LineWidth
        {
            get => line.startWidth;
            set
            {
                line.startWidth = value;
                line.endWidth = value;
            }
        }

        public Bounds? Bounds => null;

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