#nullable enable

using System.Collections.Generic;
using Iviz.Core;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Iviz.Displays
{
    public abstract class XRScreenDraggable : ScreenDraggable
    {
        readonly HashSet<Object> hovering = new();
        XRSimpleInteractable? interactable;

        protected void Start()
        {
            if (!Settings.IsXR)
            {
                return;
            }
            
            interactable = gameObject.TryAddComponent<XRSimpleInteractable>();
            interactable.hoverEntered.AddListener(OnHoverEntered);
            interactable.hoverExited.AddListener(OnHoverExited);
            interactable.selectEntered.AddListener(OnSelectEntered);
            interactable.selectExited.AddListener(OnSelectExited);
        }

        void OnSelectEntered(SelectEnterEventArgs args)
        {
            //interactorTransform = args.interactorObject.transform;
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

        void OnHoverEntered(HoverEnterEventArgs args)
        {
            hovering.Add(args.interactor.gameObject);
            if (hovering.Count == 1)
            {
                StartHover();
            }
        }

        void OnHoverExited(HoverExitEventArgs args)
        {
            UnregisterHover(args.interactor.gameObject);
        }

        public void UnregisterHover(GameObject interactor)
        {
            hovering.Remove(interactor);
            if (hovering.Count == 0)
            {
                EndHover();
            }
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