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
        
        public float LineWidth
        {
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

        public bool Visible
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }
        
        public void Suspend()
        {
        }
    }
}