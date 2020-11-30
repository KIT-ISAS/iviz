using System;
using Iviz.App;
using Iviz.Controllers;
using Iviz.Core;
using Iviz.Resources;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;
using Vector4 = System.Numerics.Vector4;

namespace Iviz.Displays
{
    public sealed class InteractiveControl : MonoBehaviour, IControlMarker, IPointerDownHandler, IPointerUpHandler,
        IPointerEnterHandler, IPointerExitHandler
    {
        GameObject[] allResources;
        [SerializeField] GameObject arrowMx = null;
        [SerializeField] GameObject arrowMy = null;
        [SerializeField] GameObject arrowMz = null;

        [SerializeField] GameObject arrowPx = null;
        [SerializeField] GameObject arrowPy = null;
        [SerializeField] GameObject arrowPz = null;

        [SerializeField] GameObject ringX = null;
        [SerializeField] GameObject ringXPlane = null;
        [SerializeField] GameObject ringY = null;
        [SerializeField] GameObject ringZ = null;
        [SerializeField] GameObject ringZPlane = null;

        [SerializeField] BoxCollider holderCollider = null;
        [SerializeField] GameObject menuObject = null;

        [SerializeField] Transform targetTransform;
        BoundaryFrame frame;

        InteractionModeType interactionMode;
        bool handlesPointToCamera;
        bool keepAbsoluteRotation;
        bool pointsToCamera;

        Bounds? bounds;

        public bool EnableMenu
        {
            get => menuObject.activeSelf;
            set => menuObject.SetActive(value);
        }

        public Transform TargetTransform
        {
            get => targetTransform;
            set
            {
                targetTransform = value;
                foreach (var resource in allResources)
                {
                    resource.GetComponent<IDraggable>().TargetTransform = value;
                }
            }
        }

        public bool Visible
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        public float BaseScale { get; set; } = 1.0f;

        public bool PointsToCamera
        {
            get => pointsToCamera;
            set
            {
                if (value == pointsToCamera)
                {
                    return;
                }

                pointsToCamera = value;
                if (!value)
                {
                    HandlesPointToCamera = false;
                    transform.localRotation = Quaternion.identity;
                }

                foreach (var resource in allResources)
                {
                    var draggable = resource.GetComponent<DraggableRotation>();
                    if (draggable != null)
                    {
                        draggable.DoesRotationReset = value;
                    }
                }
            }
        }

        public bool KeepAbsoluteRotation
        {
            get => keepAbsoluteRotation;
            set
            {
                if (keepAbsoluteRotation == value)
                {
                    return;
                }

                keepAbsoluteRotation = value;
                if (value)
                {
                    GameThread.LateEveryFrame += RotateBackToFixed;
                }
                else
                {
                    GameThread.LateEveryFrame -= RotateBackToFixed;
                    transform.localRotation = Quaternion.identity;
                }

                foreach (var resource in allResources)
                {
                    var draggable = resource.GetComponent<DraggableRotation>();
                    if (draggable != null)
                    {
                        draggable.DoesRotationReset = value;
                    }
                }
            }
        }

        public bool HandlesPointToCamera
        {
            get => handlesPointToCamera;
            set
            {
                if (handlesPointToCamera == value)
                {
                    return;
                }

                handlesPointToCamera = value;
                if (!value)
                {
                    transform.localRotation = Quaternion.identity;
                }
            }
        }

        public InteractionModeType InteractionMode
        {
            get => interactionMode;
            set
            {
                if (interactionMode == value)
                {
                    return;
                }

                interactionMode = value;

                foreach (var resource in allResources)
                {
                    resource.SetActive(false);
                }

                holderCollider.enabled = false;
                frame.Visible = false;

                switch (InteractionMode)
                {
                    case InteractionModeType.ClickOnly:
                        holderCollider.enabled = true;
                        frame.Visible = true;
                        break;
                    case InteractionModeType.MoveAxisX:
                        arrowPx.SetActive(true);
                        arrowMx.SetActive(true);
                        break;
                    case InteractionModeType.MovePlaneYZ:
                        //arrowPy.SetActive(true);
                        //arrowMy.SetActive(true);
                        //arrowPz.SetActive(true);
                        //arrowMz.SetActive(true);
                        ringXPlane.SetActive(true);
                        break;
                    case InteractionModeType.RotateAxisX:
                        ringX.SetActive(true);
                        break;
                    case InteractionModeType.MovePlaneYZ_RotateAxisX:
                        //arrowPy.SetActive(true);
                        //arrowMy.SetActive(true);
                        //arrowPz.SetActive(true);
                        //arrowMz.SetActive(true);
                        ringX.SetActive(true);
                        ringXPlane.SetActive(true);
                        break;
                    case InteractionModeType.Frame:
                        //arrowPx.SetActive(true);
                        //arrowMx.SetActive(true);
                        //arrowPy.SetActive(true);
                        //arrowMy.SetActive(true);
                        //arrowPz.SetActive(true);
                        //arrowMZ.SetActive(true);
                        ringZ.SetActive(true);
                        ringZPlane.SetActive(true);
                        break;
                    case InteractionModeType.Move3D:
                        arrowPx.SetActive(true);
                        arrowMx.SetActive(true);
                        arrowPy.SetActive(true);
                        arrowMy.SetActive(true);
                        arrowPz.SetActive(true);
                        arrowMz.SetActive(true);
                        ringZPlane.SetActive(true);
                        break;
                    case InteractionModeType.Rotate3D:
                        ringX.SetActive(true);
                        ringY.SetActive(true);
                        ringZ.SetActive(true);
                        break;
                    case InteractionModeType.MoveRotate3D:
                        arrowPx.SetActive(true);
                        arrowMx.SetActive(true);
                        arrowPy.SetActive(true);
                        arrowMy.SetActive(true);
                        arrowPz.SetActive(true);
                        arrowMz.SetActive(true);
                        ringX.SetActive(true);
                        ringY.SetActive(true);
                        ringZ.SetActive(true);
                        break;
                }
            }
        }

        public Bounds? Bounds
        {
            get => bounds;
            set
            {
                bounds = value;

                Bounds newBounds = bounds ?? new Bounds(Vector3.zero, 0.5f * Vector3.one);

                float maxScale = Mathf.Max(newBounds.size.x, Mathf.Max(newBounds.size.y, newBounds.size.z));

                GameObject holder = holderCollider.gameObject;
                holder.transform.localPosition = newBounds.center;
                holder.transform.localScale = 2 * maxScale * Vector3.one;

                menuObject.transform.localScale = 0.25f * maxScale * Vector3.one;
                menuObject.GetComponent<Billboard>().offset =
                    new Vector3(0, 0.75f * maxScale + newBounds.center.y, 0.05f * maxScale);
            }
        }

        public int Layer
        {
            get => LayerType.Clickable;
            set
            {
                if (value != LayerType.Clickable)
                {
                    throw new InvalidOperationException("This display cannot change layers");
                }
            }
        }

        public event MovedAction Moved;
        public event Action PointerUp;
        public event Action PointerDown;
        public event Action MenuClicked;


        public void Suspend()
        {
            PointsToCamera = false;
            KeepAbsoluteRotation = false;
            InteractionMode = InteractionModeType.None;
            Bounds = new Bounds(Vector3.zero, Vector3.one);
        }

        void Awake()
        {
            allResources = new[]
                {arrowPx, arrowMx, arrowPy, arrowMy, arrowPz, arrowMz, ringX, ringY, ringZ, ringXPlane, ringZPlane};

            void Moved(in Pose pose) => this.Moved?.Invoke(pose);
            void PointerUp() => this.PointerUp?.Invoke();
            void PointerDown() => this.PointerDown?.Invoke();

            foreach (var resource in allResources)
            {
                var draggable = resource.GetComponent<IDraggable>();
                draggable.Moved += Moved;
                draggable.PointerUp += PointerUp;
                draggable.PointerDown += PointerDown;
            }

            frame = ResourcePool.GetOrCreateDisplay<BoundaryFrame>(holderCollider.transform);
            frame.FrameAxisLength = 0.125f;
            frame.Bounds = new Bounds(Vector3.zero, 0.5f * Vector3.one);
            frame.Layer = LayerType.Unclickable;

            InteractionMode = InteractionModeType.MovePlaneYZ_RotateAxisX;
        }

        void LateUpdate()
        {
            if (PointsToCamera)
            {
                RotateToCamera();
            }
        }

        void RotateToCamera()
        {
            var cameraForward = Settings.MainCamera.transform.forward;
            var mTransform = transform;
            if (HandlesPointToCamera)
            {
                mTransform.parent.LookAt(mTransform.parent.position + cameraForward);
            }
            else
            {
                mTransform.LookAt(mTransform.position + cameraForward);
            }
        }

        void RotateBackToFixed()
        {
            transform.rotation = TfListener.OriginFrame.transform.rotation;
        }

        public void SetTargetPoseUpdater(Action<Pose> setTargetPose)
        {
            foreach (var resource in allResources)
            {
                var draggable = resource.GetComponent<IDraggable>();
                if (draggable != null)
                {
                    draggable.SetTargetPose = setTargetPose;
                }
            }
        }

        public void OnPointerDown([NotNull] PointerEventData eventData)
        {
            if (eventData.pointerCurrentRaycast.gameObject != holderCollider.gameObject)
            {
                return;
            }

            PointerDown?.Invoke();
        }

        public void OnPointerUp([NotNull] PointerEventData eventData)
        {
            if (eventData.pointerCurrentRaycast.gameObject == menuObject)
            {
                MenuClicked?.Invoke();
                return;
            }

            if (eventData.pointerCurrentRaycast.gameObject != holderCollider.gameObject)
            {
                return;
            }

            PointerUp?.Invoke();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (interactionMode != InteractionModeType.ClickOnly ||
                (eventData.pointerCurrentRaycast.gameObject != holderCollider.gameObject &&
                 eventData.pointerCurrentRaycast.gameObject != menuObject))
            {
                return;
            }

            frame.Color = Color.white;
        }

        public void OnPointerExit(PointerEventData _)
        {
            if (interactionMode != InteractionModeType.ClickOnly)
            {
                return;
            }

            frame.Color = Color.green;
        }
    }
}