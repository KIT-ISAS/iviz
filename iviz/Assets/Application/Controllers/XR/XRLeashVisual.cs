﻿#nullable enable

using Iviz.App;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Resources;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Iviz.Controllers.XR
{
    public class XRLeashVisual : MonoBehaviour
    {
        [SerializeField] XRRayInteractor? interactor;
        [SerializeField] CustomController? controller;
        [SerializeField] float noHitLength = 1.6f;

        Transform? mTransform;
        Leash? leash;
        ScreenDraggable? draggable;

        Transform Transform => mTransform != null ? mTransform : (mTransform = transform);
        CustomController Controller => controller.AssertNotNull(nameof(controller));
        XRRayInteractor Interactor => interactor.AssertNotNull(nameof(interactor));
        Leash Leash => leash != null ? leash : (leash = ResourcePool.RentDisplay<Leash>());

        void Awake()
        {
            Interactor.selectEntered.AddListener(OnSelectEntered);
            Interactor.selectExited.AddListener(OnSelectExited);
        }

        void OnSelectEntered(SelectEnterEventArgs args)
        {
            if (args.interactable == null)
            {
                return;
            }

            draggable = args.interactable.GetComponent<ScreenDraggable>();
        }

        void OnSelectExited(SelectExitEventArgs args)
        {
            draggable = null;
        }

        void Update()
        {
            const float interactingWidth = 0.005f;
            const float hoveringWidth = 0.0025f;
            const float noHitWidth = 0.0006f;
            
            bool leashActive = Controller.IsActiveInFrame && Controller.HasCursor;
            Leash.Visible = leashActive || draggable != null;
            if (!leashActive)
            {
                return;
            }

            var transformRay = new Ray(Transform.position, Transform.forward);
            if (draggable != null && draggable.ReferencePoint is { } referencePoint)
            {
                Leash.Color = Color.cyan;
                Leash.Width = interactingWidth;
                Leash.Set(transformRay, referencePoint);
                return;
            }

            if (Controller.ButtonUp)
            {
                GuiInputModule.TriggerEnvironmentClick(new ClickInfo(transformRay));
            }

            if (Interactor.TryGetHitInfo(out var hitPosition, out var hitNormal, out _, out _))
            {
                Leash.Color = Color.white;
                Leash.Width = hoveringWidth;
                Leash.Set(transformRay, hitPosition, hitNormal);
                return;
            }

            /*
            if (Physics.Raycast(transformRay, out var hitInfo, 100, LayerType.RaycastLayerMask))
            {
                Leash.Color = Color.white;
                Leash.Width = hoveringWidth;
                Leash.Set(transformRay, hitInfo.point, hitInfo.normal);
                return;
            }
            */

            Leash.Color = Color.white;
            Leash.Width = noHitWidth;
            Leash.Set(transformRay, Transform.TransformPoint(Vector3.forward * noHitLength));
        }
    }
}