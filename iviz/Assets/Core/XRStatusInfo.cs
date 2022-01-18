#nullable enable

using System;
using Iviz.Core;
using UnityEngine;

namespace Iviz.Controllers.XR
{
    public sealed class XRStatusInfo : MonoBehaviour
    {
        [SerializeField] bool isXREnabled;
        [SerializeField] bool isHololens;

        public bool IsXREnabled => isXREnabled;
        public bool IsHololens => isHololens;
    }
}