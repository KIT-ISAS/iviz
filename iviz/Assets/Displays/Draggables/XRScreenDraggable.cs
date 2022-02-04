#nullable enable

using Iviz.Core;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Iviz.Displays
{
    public abstract class XRScreenDraggable : ScreenDraggable
    {
        XRSimpleInteractable? interactable;

        protected void Start()
        {
            if (!Settings.IsXR)
            {
                return;
            }
            
            interactable = gameObject.EnsureComponent<XRSimpleInteractable>();
            interactable.selectEntered.AddListener(OnSelectEntered);
            interactable.selectExited.AddListener(OnSelectExited);
        }

        void OnSelectEntered(SelectEnterEventArgs args)
        {
            interactorTransform = args.interactor.transform;
            StartSelected();
            GameThread.EveryFrame += TriggerPointerMove;
        }

        void OnSelectExited(SelectExitEventArgs _)
        {
            GameThread.EveryFrame -= TriggerPointerMove;
            EndSelected();
            interactorTransform = null;
        }

        void TriggerPointerMove()
        {
            if (interactorTransform == null)
            {
                return;
            }
            
            var ray = new Ray(interactorTransform.position, interactorTransform.forward); 
            OnPointerMove(ray);
        }
    }
}