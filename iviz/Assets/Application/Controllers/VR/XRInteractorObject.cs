#nullable enable

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Iviz.Controllers
{
    public class XRInteractorObject : XRBaseInteractor
    {
        [SerializeField] XRSimpleInteractable? cube;
        
        public override void GetValidTargets(List<XRBaseInteractable> targets)
        {
            Canvas canvas;
            
            //targets.Add(cube!);
        }
    }
}