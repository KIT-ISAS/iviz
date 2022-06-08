#nullable enable

using Iviz.Controllers.TF;
using Iviz.Core;
using UnityEngine;

namespace Iviz.Displays
{
    public sealed class Billboard : MonoBehaviour
    {
        Transform? mTransform;
        Transform Transform => this.EnsureHasTransform(ref mTransform);

        [SerializeField] Vector3 offset;
        [SerializeField] bool keepHorizontal;
        [SerializeField] bool useAbsoluteOffset = true;

        public Vector3 Offset
        {
            set
            {
                offset = value;
                UpdatePose();
            }
        }

        public bool UseAbsoluteOffset
        {
            get => useAbsoluteOffset;
            set => useAbsoluteOffset = value;
        }
        
        void OnEnable()
        {
            GameThread.AfterFramesUpdatedLate += UpdatePose;
        }

        void OnDisable()
        {
            GameThread.AfterFramesUpdatedLate -= UpdatePose;
        }

        void UpdatePose()
        {
            var mainCameraPose = Settings.MainCameraPose;
            var lookAtPosition = 2 * Transform.position - mainCameraPose.position;
            
            Transform.LookAt(lookAtPosition, keepHorizontal
                ? mainCameraPose.Up()
                : Vector3.up);
            
            if (UseAbsoluteOffset && Transform.parent is { } parent)
            {
                Transform.position = parent.position + offset;
            }
        }
    }
}