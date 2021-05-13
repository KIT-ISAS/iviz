﻿using Iviz.App;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Resources;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Iviz.Controllers
{
    public interface IVRButton
    {
        bool PointerDown();
        bool PointerUp();
    }

    public class VRController : MonoBehaviour
    {
        //[SerializeField] Camera mainCamera;
        [SerializeField] EventSystem eventSystem;
        IVRButton button;

        Transform mTransform;
        Transform Transform => mTransform != null ? mTransform : (mTransform = transform);

        LineResource resource;
        MeshMarkerResource target;

        [CanBeNull] Transform currentHit;

        [CanBeNull]
        Transform CurrentHit
        {
            get => currentHit;
            set
            {
                if (currentHit == value)
                {
                    return;
                }

                if (currentHit != null)
                {
                    foreach (var handler in currentHit.GetComponentsInParent<IPointerExitHandler>())
                    {
                        handler.OnPointerExit(new PointerEventData(eventSystem));
                    }
                }

                currentHit = value;

                if (currentHit != null)
                {
                    foreach (var handler in currentHit.GetComponentsInParent<IPointerEnterHandler>())
                    {
                        handler.OnPointerEnter(new PointerEventData(eventSystem));
                    }
                }
            }
        }


        void Awake()
        {
            Settings.IsVR = true;
            
            resource = ResourcePool.RentDisplay<LineResource>(transform);
            resource.Set(new[]
            {
                new LineWithColor(Vector3.zero, Color.white, 0.5f * Vector3.forward, Color.white.WithAlpha(0))
            });
            resource.ElementScale = 0.005f;

            target = ResourcePool.Rent<MeshMarkerResource>(Resource.Displays.Cylinder);
            target.Transform.localScale = new Vector3(0.05f, 0.001f, 0.05f);
            target.GetComponent<BoxCollider>().enabled = false;

            button = GetComponent<IVRButton>();
        }

        void Update()
        {
            Ray raycast = new Ray(Transform.position, Transform.forward);
            CurrentHit= !Physics.Raycast(raycast, out var hit)
                ? null
                : hit.collider.transform;


            if (CurrentHit is null)
            {
                target.Visible = false;
            }
            else
            {
                target.Visible = true;
                target.Transform.position = hit.point;
                target.Transform.rotation = Quaternion.LookRotation(hit.normal) * Quaternion.AngleAxis(90, Vector3.left);
            }

            if (button.PointerDown())
            {
                if (CurrentHit == null)
                {
                    return;
                }

                foreach (var handler in CurrentHit.GetComponentsInParent<IPointerDownHandler>())
                {
                    handler.OnPointerDown(new PointerEventData(eventSystem));
                }
            }
            else if (button.PointerUp())
            {
                ModuleListPanel.GuiInputModule.DraggedObject = null;

                if (CurrentHit == null)
                {
                    return;
                }

                foreach (var handler in CurrentHit.GetComponentsInParent<IPointerUpHandler>())
                {
                    handler.OnPointerUp(new PointerEventData(eventSystem));
                }

            }
            else
            {
                ModuleListPanel.GuiInputModule.DraggedObject?.OnPointerMove(raycast);
            }
        }
    }
}