#nullable enable

using UnityEngine;

namespace Iviz.Core.XR
{
    public sealed class XRStatusInfo : MonoBehaviour
    {
        [SerializeField] bool isXREnabled;
        [SerializeField] bool isHololens;

        public bool IsXREnabled => isXREnabled;
        public bool IsHololens => isHololens;
        
        void Awake()
        {
            Settings.IsShuttingDown = false;
        }
        
        void OnApplicationQuit()
        {
            Settings.IsShuttingDown = true;
        }
        
    }
}