using System;
using Iviz.App;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Msgs.SensorMsgs;
using Iviz.Resources;
using Iviz.Ros;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Iviz.Controllers
{
    public interface IVRButton
    {
        bool PointerDown();
        bool PointerUp();
        bool State();
        bool AltState();
    }

    public class VRController : MonoBehaviour
    {
        static readonly int[][] Indices = 
        {
            Array.Empty<int>(),
            new[] {0},
            new[] {1},
            new[] {0, 1},
        };
        
        [SerializeField] Camera mainCamera;
        [SerializeField] EventSystem eventSystem = null;
        [CanBeNull] IVRButton button;

        [SerializeField] string tfName = "vr";
        [SerializeField] bool canInteract = false;

        [SerializeField] float scale = 1;

        Transform mTransform;
        [NotNull] Transform Transform => mTransform != null ? mTransform : (mTransform = transform);

        LineResource resource;
        MeshMarkerResource target;
        
        [CanBeNull] Transform currentHit;

        Sender<Joy> sender;

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
            Settings.MainCamera = mainCamera;

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
            if (button == null)
            {
                Debug.LogError("VRController: Button is not set!");
            }
        }

        void Start()
        {
            sender = new Sender<Joy>($"~{tfName}");
        }

        uint seq = 0;
        void Update()
        {
            TfListener.Publish($"/isas/{tfName}", Transform.AsPose());
            sender.Publish(new Joy
            {
                Header = (seq++, "/isas/vr_station"),
                Buttons = button == null 
                    ? Array.Empty<int>() 
                    : Indices[(button.State() ? 1 : 0) + (button.AltState() ? 2 : 0)]
            });
            
            if (!canInteract)
            {
                return;
            }

            TfListener.RootScale = scale;
            Ray raycast = new Ray(Transform.position, Transform.forward);
            CurrentHit = !Physics.Raycast(raycast, out var hit)
                ? null
                : hit.collider.transform;


            if (CurrentHit == null)
            {
                target.Visible = false;
            }
            else
            {
                target.Visible = true;
                target.Transform.position = hit.point;
                target.Transform.rotation =
                    Quaternion.LookRotation(hit.normal) * Quaternion.AngleAxis(90, Vector3.left);
            }

            if (button == null)
            {
                return;
            }

            Settings.IsVRButtonDown = button.State();
            if (button.PointerDown())
            {
                if (CurrentHit == null)
                {
                    return;
                }

                var ptr = new PointerEventData(eventSystem);
                foreach (var handler in CurrentHit.GetComponentsInParent<IPointerDownHandler>())
                {
                    handler.OnPointerDown(ptr);
                }
            }
            else if (button.PointerUp())
            {
                ModuleListPanel.GuiInputModule.ResetDraggedObject();

                if (CurrentHit == null)
                {
                    return;
                }

                var ptr = new PointerEventData(eventSystem);
                foreach (var handler in CurrentHit.GetComponentsInParent<IPointerUpHandler>())
                {
                    handler.OnPointerUp(ptr);
                }

                foreach (var handler in CurrentHit.GetComponentsInParent<IPointerClickHandler>())
                {
                    handler.OnPointerClick(ptr);
                }
            }
            else
            {
                ModuleListPanel.GuiInputModule.DraggedObject?.OnPointerMove(raycast);
            }
        }
    }
}