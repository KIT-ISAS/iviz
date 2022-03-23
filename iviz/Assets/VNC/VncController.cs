using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Displays;
using UnityEngine;

namespace VNC
{
    public class VncController : MonoBehaviour
    {
        TfModule tfModule;

        void Awake()
        {
            tfModule = new TfModule(id => new TfFrameDisplay(id));            
        }
    }
}