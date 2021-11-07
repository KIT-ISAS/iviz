#nullable enable

using Iviz.Core;
using UnityEngine;

namespace Iviz.Displays
{
    public sealed class Billboard : MonoBehaviour
    {
        Transform? mTransform;
        Transform Transform => mTransform != null ? mTransform : (mTransform = transform);
        
        [SerializeField] Vector3 offset;
        [SerializeField] bool keepHorizontal = false;

        public Vector3 Offset
        {
            get => offset;
            set
            {
                offset = value;
                LateUpdate();
            } 
        }

        public bool UseAbsoluteOffset { get; set; } = true;

        void LateUpdate()
        {
            Vector3 z = 2 * Transform.position - Settings.MainCameraTransform.position;
            if (!keepHorizontal)
            {
                Transform.LookAt(z, Vector3.up);
            }
            else
            {
                Vector3 up = (Transform.position - Settings.MainCameraTransform.position).Cross(Settings.MainCameraTransform.right);
                Transform.LookAt(z, up);
            }
            if (UseAbsoluteOffset && Transform.parent != null)
            {
                Transform.position = Transform.parent.position + offset;
            } 
        }
    }
}