#nullable enable

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
                LateUpdate();
            }
        }

        public bool UseAbsoluteOffset
        {
            get => useAbsoluteOffset;
            set => useAbsoluteOffset = value;
        }

        void LateUpdate()
        {
            var z = 2 * Transform.position - Settings.MainCameraPose.position;
            Transform.LookAt(z, keepHorizontal
                ? Settings.MainCameraTransform.up
                : Vector3.up);
            if (UseAbsoluteOffset && Transform.parent is { } parent)
            {
                Transform.position = parent.position + offset;
            }
        }
    }
}