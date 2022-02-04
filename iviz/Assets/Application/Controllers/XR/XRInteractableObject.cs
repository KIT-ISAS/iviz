using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Iviz.Controllers.XR
{
    [RequireComponent(typeof(XRSimpleInteractable))]
    public class XRInteractableObject : MonoBehaviour
    {
        public void OnHoverEntered(HoverEnterEventArgs _)
        {
            //Debug.Log("hover enter!");
        }

        public void OnHoverExited(HoverExitEventArgs _)
        {
            //Debug.Log("hover exit!");
        }

        public void OnSelectEntered(SelectEnterEventArgs _)
        {
            Debug.Log("select enter!");
        }

        public void OnSelectExited(SelectExitEventArgs _)
        {
            Debug.Log("select exit!");
        }

        public void OnActivated(ActivateEventArgs _)
        {
            Debug.Log("activate enter!");
        }

        public void OnDeactivated(DeactivateEventArgs _)
        {
            Debug.Log("activate exit!");
        }
    }
}