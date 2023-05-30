#nullable enable

using Iviz.App;
using Iviz.Core;
using Iviz.Displays;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

namespace Iviz.Controllers.XR
{
    public interface IControllerCanStick
    {
        bool CanStick(UnityEngine.Object? obj);
        Vector3 StickyPosition { set; }
    }

    public sealed class XRLeashVisual : MonoBehaviour
    {
        [SerializeField] XRRayInteractor? interactor;
        [SerializeField] CustomController? controller;
        [SerializeField] float noHitLength = 1.6f;
        [SerializeField] float reticleScale = 1;

        Transform? mTransform;
        LeashDisplay? leash;
        ScreenDraggable? draggable;

        Transform Transform => this.EnsureHasTransform(ref mTransform);
        CustomController Controller => controller.AssertNotNull(nameof(controller));
        XRRayInteractor Interactor => interactor.AssertNotNull(nameof(interactor));
        LeashDisplay Leash => ResourcePool.RentChecked(ref leash);

        void Awake()
        {
            Leash.ReticleScale = reticleScale;
            Interactor.selectEntered.AddListener(OnSelectEntered);
            Interactor.selectExited.AddListener(OnSelectExited);
        }

        void OnSelectEntered(SelectEnterEventArgs args)
        {
            //if (args.interactableObject is not XRBaseInteractable interactable)
            if (args.interactable is not { } interactable)
            {
                return;
            }

            interactable.TryGetComponent(out draggable);
        }

        void OnSelectExited(SelectExitEventArgs args)
        {
            draggable = null;
        }

        const float InteractingLeashWidth = 0.005f;
        const float HoveringLeashWidth = 0.0025f;
        const float NoHitLeashWidth = 0.0006f;
        const float HoverHeightNoUI = 0.001f;
        const float HoverHeightUI = 0.015f;

        void Update()
        {
            bool leashActive = Controller.IsActiveInFrame && Controller.HasCursor;
            Leash.Visible = leashActive || draggable != null;
            if (!leashActive)
            {
                return;
            }

            var currentPosition = Transform.position;
            var currentForward = Transform.forward;
            var offsetOrigin = currentPosition + currentForward * XRController.NearDistance;
            var transformRay = new Ray(offsetOrigin, currentForward);

            if (draggable != null && draggable.ReferencePoint is { } referencePoint)
            {
                Leash.Color = Color.cyan;
                Leash.ReticleColor = Color.white;
                Leash.ReticleEmissiveColor = Color.white;
                Leash.Width = InteractingLeashWidth;
                //Controller.IsNearInteraction = (currentPosition - referencePoint).Magnitude() < XRController.NearDistance;

                if (draggable.ReferenceNormal is { } referenceNormal)
                {
                    Leash.Set(transformRay, referencePoint, referenceNormal);
                }
                else
                {
                    Leash.Set(transformRay, referencePoint);
                }

                return;
            }

            bool hitExists = TryGetHitInfo(out var hitPosition, out var hitNormal, out bool isUIHitClosest,
                out var target);

            //Controller.IsNearInteraction = (currentPosition - hitPosition).Magnitude() < XRController.NearDistance;

            if (Controller.ButtonUp && !isUIHitClosest)
            {
                GuiInputModule.TriggerEnvironmentClick(new ClickHitInfo(transformRay), false);
            }

            if (hitExists)
            {
                Leash.Color = Color.white;
                Leash.ReticleColor = Color.white;
                Leash.Width = HoveringLeashWidth;
                Leash.Set(transformRay, hitPosition, hitNormal, isUIHitClosest ? HoverHeightUI : HoverHeightNoUI);

                if (isUIHitClosest
                    && Controller.ButtonDown
                    && Controller is IControllerCanStick stickyController
                    && stickyController.CanStick(target))
                {
                    stickyController.StickyPosition = hitPosition;
                }

                return;
            }

            Leash.Color = Color.white;
            Leash.Width = NoHitLeashWidth;
            Leash.Set(transformRay, Transform.TransformPoint(Vector3.forward * noHitLength));
        }

        bool TryGetHitInfo(out Vector3 position, out Vector3 normal, out bool isUIHitClosest,
            out UnityEngine.Object? target)
        {
            if (!Interactor.TryGetCurrentRaycast(out var raycastHit, out _,
                    out var raycastResult, out _, out isUIHitClosest))
            {
                position = default;
                normal = default;
                target = null;
                return false;
            }

            if (isUIHitClosest && raycastResult is { } result)
            {
                position = result.worldPosition;
                normal = result.worldNormal;
                target = result.gameObject;
                return true;
            }

            if (raycastHit is { } hit)
            {
                position = hit.point;
                normal = hit.normal;
                target = hit.collider;
                return true;
            }

            position = default;
            normal = default;
            target = null;
            return false;
        }
    }
}