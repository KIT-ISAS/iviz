using System;
using Iviz.App;
using Iviz.Controllers;
using Iviz.Core;
using Iviz.Resources;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Iviz.Displays
{
    public sealed class InteractiveControl : MonoBehaviour, IControlMarker, IPointerDownHandler, IPointerUpHandler,
        IPointerEnterHandler, IPointerExitHandler
    {
        static readonly Color FrameActiveColor = Color.white.WithAlpha(0.3f);
        static readonly Color FrameInactiveColor = Color.green.WithAlpha(0.3f);

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
        [SerializeField] InteractionModeType interactionMode;

        BoundaryFrame frame;

        bool handlesPointToCamera;
        bool keepAbsoluteRotation;
        bool pointsToCamera;

        Bounds? bounds;
        int layer;

        [NotNull]
        public string Name
        {
            get => gameObject.name;
            set => gameObject.name = value ?? throw new ArgumentNullException(nameof(value));
        }

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

                Bounds newBounds = bounds != null
                    ? new Bounds(bounds.Value.center, bounds.Value.size.Abs())
                    : new Bounds(Vector3.zero, 0.5f * Vector3.one);

                float holderScale;
                switch (InteractionMode)
                {
                    case InteractionModeType.MoveAxisX:
                        holderScale = newBounds.size.z;
                        break;
                    case InteractionModeType.MovePlaneYZ:
                    case InteractionModeType.RotateAxisX:
                    case InteractionModeType.MovePlaneYZ_RotateAxisX:
                        holderScale = Mathf.Max(newBounds.size.x, newBounds.size.y);
                        break;
                    default:
                        holderScale = Mathf.Max(newBounds.size.x, Mathf.Max(newBounds.size.y, newBounds.size.z));
                        break;
                }

                GameObject holder = holderCollider.gameObject;
                holder.transform.localPosition = newBounds.center;
                holder.transform.localScale = 2 * holderScale * Vector3.one;
                holderCollider.size = newBounds.size / (2 * holderScale);

                float maxScale = Mathf.Max(newBounds.size.x, Mathf.Max(newBounds.size.y, newBounds.size.z));
                float absoluteScaleY = transform.lossyScale.y; 
                menuObject.transform.localScale = 0.5f * Vector3.one;
                menuObject.GetComponent<Billboard>().Offset =
                    new Vector3(0, 1.5f * maxScale + newBounds.center.y, 0.1f * maxScale) * absoluteScaleY;

                frame.Bounds = newBounds;
            }
        }

        public int Layer
        {
            get => layer;
            set
            {
                layer = value;
                gameObject.layer = value;
                foreach (GameObject resource in allResources)
                {
                    resource.layer = value;
                }
            }
        }

        public event MovedAction Moved;
        public event Action PointerUp;
        public event Action PointerDown;
        public event Action<Vector3> MenuClicked;


        public void Suspend()
        {
            PointsToCamera = false;
            KeepAbsoluteRotation = false;
            InteractionMode = InteractionModeType.None;
            EnableMenu = false;
            Bounds = new Bounds(Vector3.zero, Vector3.one);
            Layer = LayerType.Clickable;

            Moved = null;
            PointerUp = null;
            PointerDown = null;
            MenuClicked = null;
        }

        void RaiseMoved(in Pose pose)
        {
            if (canMove)
            {
                Moved?.Invoke(pose);
            }
        }

        void RaisePointerUp()
        {
            PointerUp?.Invoke();
            canMove = false;
        }

        void RaisePointerDown()
        {
            canMove = true;
            PointerDown?.Invoke();
        }


        void Awake()
        {
            allResources = new[]
                {arrowPx, arrowMx, arrowPy, arrowMy, arrowPz, arrowMz, ringX, ringY, ringZ, ringXPlane, ringZPlane};
            Layer = LayerType.Clickable;

            foreach (var resource in allResources)
            {
                var draggable = resource.GetComponent<IDraggable>();
                draggable.Moved += RaiseMoved;
                draggable.PointerUp += RaisePointerUp;
                draggable.PointerDown += RaisePointerDown;
            }

            frame = ResourcePool.RentDisplay<BoundaryFrame>(transform);
            frame.FrameAxisLength = 0.125f;
            frame.Bounds = new Bounds(Vector3.zero, 0.5f * Vector3.one);
            frame.Layer = LayerType.IgnoreRaycast;
            frame.Color = FrameInactiveColor;

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
            var cameraForward = Settings.MainCameraTransform.forward;
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

        bool canMove;
        public void OnPointerDown([NotNull] PointerEventData eventData)
        {
            if (eventData.pointerCurrentRaycast.gameObject != holderCollider.gameObject)
            {
                return;
            }

            RaisePointerDown();
        }

        public void OnPointerUp([NotNull] PointerEventData eventData)
        {
            if (eventData.pointerCurrentRaycast.gameObject == menuObject)
            {
                MenuClicked?.Invoke(menuObject.transform.position);
                return;
            }

            if (eventData.pointerCurrentRaycast.gameObject != holderCollider.gameObject)
            {
                return;
            }

            RaisePointerUp();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (interactionMode != InteractionModeType.ClickOnly ||
                (eventData.pointerCurrentRaycast.gameObject != holderCollider.gameObject &&
                 eventData.pointerCurrentRaycast.gameObject != menuObject))
            {
                return;
            }

            frame.Color = FrameActiveColor;
        }

        public void OnPointerExit(PointerEventData _)
        {
            if (interactionMode != InteractionModeType.ClickOnly)
            {
                return;
            }

            frame.Color = FrameInactiveColor;
        }
    }
}