using Iviz.Core;
using UnityEngine;

namespace Iviz.Controllers
{
    public sealed class XRIvizInfo : MonoBehaviour
    {
        [SerializeField] bool isXREnabled;
        [SerializeField] bool isHololens;

        void Awake()
        {
#if UNITY_EDITOR || (!UNITY_IOS && !UNITY_ANDROID && !UNITY_WSA)
            Settings.IsHololens = isHololens;
            Settings.IsXR = isXREnabled || isHololens;
#endif
        }
    }
}