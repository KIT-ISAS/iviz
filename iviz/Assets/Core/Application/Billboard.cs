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

        public Vector3 Offset
        {
            set
            {
                offset = value;
                LateUpdate();
            }
        }

        public bool UseAbsoluteOffset { get; set; } = true;

        void LateUpdate()
        {
            var z = 2 * Transform.position - Settings.MainCameraTransform.position;
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