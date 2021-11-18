#nullable enable

using Iviz.Core;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Iviz.Displays
{
    public abstract class XRScreenDraggable : ScreenDraggable
    {
        XRSimpleInteractable? interactable;
        Transform? interactor;

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
            interactor = args.interactor.transform;
            StartSelected();
            GameThread.EveryFrame += TriggerPointerMove;
        }

        void OnSelectExited(SelectExitEventArgs _)
        {
            GameThread.EveryFrame -= TriggerPointerMove;
            EndSelected();
        }

        void TriggerPointerMove()
        {
            if (interactor == null)
            {
                return;
            }
            
            var ray = new Ray(interactor.position, interactor.forward); 
            OnPointerMove(ray);
        }
    }
}