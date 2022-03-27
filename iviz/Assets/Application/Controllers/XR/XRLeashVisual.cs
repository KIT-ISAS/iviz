#nullable enable

using Iviz.App;
using Iviz.Core;
using Iviz.Displays;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Iviz.Controllers.XR
{
    public class XRLeashVisual : MonoBehaviour
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
            if (args.interactableObject is not XRBaseInteractable interactable)
            {
                return;
            }

            interactable.TryGetComponent(out draggable);
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

            var currentPosition = Transform.position;
            var currentForward = Transform.forward;
            var offsetOrigin = currentPosition + currentForward * XRController.NearDistance;
            var transformRay = new Ray(offsetOrigin, currentForward);

            if (draggable != null && draggable.ReferencePoint is { } referencePoint)
            {
                Leash.Color = Color.cyan;
                Leash.ReticleColor = Color.white;
                Leash.ReticleEmissiveColor = Color.white;
                Leash.Width = interactingWidth;
                Controller.IsNearInteraction = (currentPosition - referencePoint).magnitude < XRController.NearDistance;

                if (draggable.ReferenceNormal is { } referenceNormal)
                {
                    Leash.Set(transformRay, referencePoint, referenceNormal, !Controller.IsNearInteraction);
                }
                else
                {
                    Leash.Set(transformRay, referencePoint);
                }

                return;
            }

            bool hitExists = TryGetHitInfo(out var hitPosition, out var hitNormal, out bool isUIHitClosest);

            Controller.IsNearInteraction = (currentPosition - hitPosition).magnitude < XRController.NearDistance;

            if (Controller.ButtonUp && !isUIHitClosest)
            {
                GuiInputModule.TriggerEnvironmentClick(new ClickHitInfo(transformRay));
            }

            if (hitExists)
            {
                Leash.Color = Color.white;
                Leash.ReticleColor = Color.white;
                Leash.Width = hoveringWidth;
                Leash.Set(transformRay, hitPosition, hitNormal, isUIHitClosest ? 0.005f : 0.001f,
                    !Controller.IsNearInteraction);

                if (isUIHitClosest && Controller.ButtonDown && Controller is HandController handController)
                {
                    handController.LockedPosition = hitPosition;
                }

                return;
            }

            Leash.Color = Color.white;
            Leash.Width = noHitWidth;
            Leash.Set(transformRay, Transform.TransformPoint(Vector3.forward * noHitLength));
        }

        bool TryGetHitInfo(out Vector3 position, out Vector3 normal, out bool isUIHitClosest)
        {
            if (!Interactor.TryGetCurrentRaycast(out var raycastHit, out _,
                    out var raycastResult, out _, out isUIHitClosest))
            {
                position = default;
                normal = default;
                return false;
            }

            if (isUIHitClosest && raycastResult is { } result)
            {
                position = result.worldPosition;
                normal = result.worldNormal;
                return true;
            }

            if (raycastHit is { } hit)
            {
                position = hit.point;
                normal = hit.normal;
                return true;
            }

            position = default;
            normal = default;
            return false;
        }
    }
}