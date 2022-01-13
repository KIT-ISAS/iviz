using System;
using Iviz.App;
using Iviz.Common;
using Iviz.Controllers;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Msgs;
using Iviz.Resources;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;

namespace Iviz.Displays
{
    [Obsolete]
    public sealed class InteractiveControl : MonoBehaviour
    {
    }

#if false
    [Obsolete]
    public sealed class InteractiveControl : MonoBehaviour, IControlMarker, IPointerDownHandler, IPointerUpHandler,
        IPointerEnterHandler, IPointerExitHandler
    {
        static readonly Color FrameActiveColor = Color.white.WithAlpha(0.3f);
#if UNITY_EDITOR
        static readonly Color FrameInactiveColor = Color.green.WithAlpha(0.3f);
#else
        static readonly Color FrameInactiveColor = Color.green.WithAlpha(0f);
#endif

        MeshMarkerResource[] allResources;
        [SerializeField] MeshMarkerResource arrowMx = null;
        [SerializeField] MeshMarkerResource arrowMy = null;
        [SerializeField] MeshMarkerResource arrowMz = null;

        [SerializeField] MeshMarkerResource arrowPx = null;
        [SerializeField] MeshMarkerResource arrowPy = null;
        [SerializeField] MeshMarkerResource arrowPz = null;

        [SerializeField] MeshMarkerResource ringX = null;

        //[SerializeField] MeshMarkerResource ringXPlane = null;
        [SerializeField] MeshMarkerResource ringY = null;

        [SerializeField] MeshMarkerResource ringZ = null;
        //[SerializeField] GameObject ringZPlane = null;

        [SerializeField] BoxCollider holderCollider = null;
        [SerializeField] GameObject menuObject = null;

        [SerializeField] Transform targetTransform;
        [SerializeField] InteractionModeType interactionMode;

        LineDraggable colliderDraggableTranslation;
        PlaneDraggable colliderDraggablePlane;

        BoundaryFrame frame;

        bool handlesPointToCamera;
        bool keepAbsoluteRotation;
        bool pointsToCamera;

        Bounds? bounds;
        [SerializeField] Bounds serializedBounds; // for display only
        bool interactable;

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
                    resource.GetComponent<IScreenDraggable>().TargetTransform = value;
                }

                if (colliderDraggablePlane != null)
                {
                    colliderDraggablePlane.TargetTransform = value;
                }

                if (colliderDraggableTranslation != null)
                {
                    colliderDraggableTranslation.TargetTransform = value;
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

                /*
                foreach (var resource in allResources)
                {
                    var draggable = resource.GetComponent<RotationDraggable>();
                    if (draggable != null)
                    {
                        draggable.DoesRotationReset = value;
                    }
                }
                */
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

                /*
                foreach (var resource in allResources)
                {
                    var draggable = resource.GetComponent<RotationDraggable>();
                    if (draggable != null)
                    {
                        draggable.DoesRotationReset = value;
                    }
                }
                */
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

        enum BoundaryMode
        {
            None,
            Button,
            MoveAxisX,
            MovePlaneYZ
        }

        void SetColliderInteractableMode(BoundaryMode mode)
        {
            switch (mode)
            {
                case BoundaryMode.None:
                    Destroy(colliderDraggableTranslation);
                    Destroy(colliderDraggablePlane);
                    holderCollider.enabled = false;
                    break;
                case BoundaryMode.Button:
                    Destroy(colliderDraggableTranslation);
                    Destroy(colliderDraggablePlane);
                    holderCollider.enabled = true;
                    break;
                case BoundaryMode.MoveAxisX:
                    Destroy(colliderDraggablePlane);
                    holderCollider.enabled = true;
                    if (colliderDraggableTranslation == null)
                    {
                        colliderDraggableTranslation = gameObject.AddComponent<LineDraggable>();
                        colliderDraggableTranslation.RayCollider = holderCollider;
                        colliderDraggableTranslation.TargetTransform = TargetTransform;
                        colliderDraggableTranslation.Moved += RaiseMoved;
                        colliderDraggableTranslation.PointerDown += PointerDown;
                        colliderDraggableTranslation.PointerUp += PointerUp;
                    }

                    break;
                case BoundaryMode.MovePlaneYZ:
                    Destroy(colliderDraggableTranslation);
                    holderCollider.enabled = true;
                    if (colliderDraggablePlane == null)
                    {
                        colliderDraggablePlane = gameObject.AddComponent<PlaneDraggable>();
                        colliderDraggablePlane.RayCollider = holderCollider;
                        colliderDraggablePlane.TargetTransform = TargetTransform;
                        colliderDraggablePlane.Moved += RaiseMoved;
                        colliderDraggablePlane.PointerDown += PointerDown;
                        colliderDraggablePlane.PointerUp += PointerUp;
                    }

                    break;
            }
        }

        public void SetColliderInteractable()
        {
            BoundaryMode boundaryMode;
            switch (InteractionMode)
            {
                case InteractionModeType.ClickOnly:
                    boundaryMode = BoundaryMode.Button;
                    break;
                case InteractionModeType.MoveAxisX:
                    boundaryMode = BoundaryMode.MoveAxisX;
                    break;
                case InteractionModeType.MovePlaneYz:
                    boundaryMode = BoundaryMode.MovePlaneYZ;
                    break;
                case InteractionModeType.MovePlaneYzRotateAxisX:
                    boundaryMode = BoundaryMode.MovePlaneYZ;
                    break;
                default:
                    boundaryMode = BoundaryMode.None;
                    break;
            }

            SetColliderInteractableMode(boundaryMode);
        }

        public bool ColliderCanInteract => InteractionMode == InteractionModeType.ClickOnly
                                           || InteractionMode == InteractionModeType.MoveAxisX
                                           || InteractionMode == InteractionModeType.MovePlaneYz
                                           || InteractionMode == InteractionModeType.MovePlaneYzRotateAxisX;


        public InteractionModeType InteractionMode
        {
            get => interactionMode;
            set
            {
                if (interactionMode == value)
                {
                    UpdateColor();
                    return;
                }

                interactionMode = value;

                foreach (var resource in allResources)
                {
                    resource.Visible = false;
                }

                holderCollider.enabled = false;
                frame.Visible = false;

                SetColliderInteractableMode(BoundaryMode.None);

                switch (InteractionMode)
                {
                    case InteractionModeType.ClickOnly:
                        frame.Visible = true;
                        break;
                    case InteractionModeType.MoveAxisX:
                        arrowPx.Visible = true;
                        arrowMx.Visible = true;
                        break;
                    case InteractionModeType.MovePlaneYz:
                        arrowPy.Visible = true;
                        arrowMy.Visible = true;
                        arrowPz.Visible = true;
                        arrowMz.Visible = true;
                        break;
                    case InteractionModeType.RotateAxisX:
                        ringX.Visible = true;
                        break;
                    case InteractionModeType.MovePlaneYzRotateAxisX:
                        arrowPy.Visible = true;
                        arrowMy.Visible = true;
                        arrowPz.Visible = true;
                        arrowMz.Visible = true;
                        ringX.Visible = true;
                        break;
                    case InteractionModeType.Frame:
                        arrowPx.Visible = true;
                        arrowMx.Visible = true;
                        arrowPy.Visible = true;
                        arrowMy.Visible = true;
                        ringZ.Visible = true;
                        break;
                    case InteractionModeType.Move3D:
                        arrowPx.Visible = true;
                        arrowMx.Visible = true;
                        arrowPy.Visible = true;
                        arrowMy.Visible = true;
                        arrowPz.Visible = true;
                        arrowMz.Visible = true;
                        break;
                    case InteractionModeType.Rotate3D:
                        ringX.Visible = true;
                        ringY.Visible = true;
                        ringZ.Visible = true;
                        break;
                    case InteractionModeType.MoveRotate3D:
                        arrowPx.Visible = true;
                        arrowMx.Visible = true;
                        arrowPy.Visible = true;
                        arrowMy.Visible = true;
                        arrowPz.Visible = true;
                        arrowMz.Visible = true;
                        ringX.Visible = true;
                        ringY.Visible = true;
                        ringZ.Visible = true;
                        break;
                }

                UpdateColor();
            }
        }

        void UpdateColor()
        {
            Color color = Interactable
                ? ColorFromOrientation(Msgs.GeometryMsgs.Vector3.UnitX)
                : Color.black.WithAlpha(0.5f);

            switch (InteractionMode)
            {
                case InteractionModeType.None:
                case InteractionModeType.ClickOnly:
                    break;
                case InteractionModeType.MoveAxisX:
                    arrowPx.Color = color;
                    arrowMx.Color = color;
                    break;
                case InteractionModeType.MovePlaneYz:
                    arrowPy.Color = color;
                    arrowMy.Color = color;
                    arrowPz.Color = color;
                    arrowMz.Color = color;
                    break;
                case InteractionModeType.RotateAxisX:
                    ringX.Color = color;
                    break;
                case InteractionModeType.MovePlaneYzRotateAxisX:
                    arrowPy.Color = color;
                    arrowMy.Color = color;
                    arrowPz.Color = color;
                    arrowMz.Color = color;
                    ringX.Color = color;
                    break;
                case InteractionModeType.Frame:
                {
                    Color colorY = Interactable
                        ? ColorFromOrientation(Msgs.GeometryMsgs.Vector3.UnitY)
                        : Color.black.WithAlpha(0.5f);

                    arrowPx.Color = color;
                    arrowMx.Color = color;
                    arrowPy.Color = colorY;
                    arrowMy.Color = colorY;
                    ringZ.Color = color;
                    break;
                }
                case InteractionModeType.Move3D:
                {
                    Color colorY = Interactable
                        ? ColorFromOrientation(Msgs.GeometryMsgs.Vector3.UnitY)
                        : Color.black.WithAlpha(0.5f);
                    Color colorZ = Interactable
                        ? ColorFromOrientation(Msgs.GeometryMsgs.Vector3.UnitZ)
                        : Color.black.WithAlpha(0.5f);

                    arrowPx.Color = color;
                    arrowMx.Color = color;
                    arrowPy.Color = colorY;
                    arrowMy.Color = colorY;
                    arrowPz.Color = colorZ;
                    arrowMz.Color = colorZ;
                    break;
                }
                case InteractionModeType.Rotate3D:
                {
                    Color colorY = Interactable
                        ? ColorFromOrientation(Msgs.GeometryMsgs.Vector3.UnitY)
                        : Color.black.WithAlpha(0.5f);
                    Color colorZ = Interactable
                        ? ColorFromOrientation(Msgs.GeometryMsgs.Vector3.UnitZ)
                        : Color.black.WithAlpha(0.5f);

                    ringX.Color = color;
                    ringY.Color = colorY;
                    ringZ.Color = colorZ;
                    break;
                }
                case InteractionModeType.MoveRotate3D:
                {
                    Color colorY = Interactable
                        ? ColorFromOrientation(Msgs.GeometryMsgs.Vector3.UnitY)
                        : Color.black.WithAlpha(0.5f);
                    Color colorZ = Interactable
                        ? ColorFromOrientation(Msgs.GeometryMsgs.Vector3.UnitZ)
                        : Color.black.WithAlpha(0.5f);

                    arrowPx.Color = color;
                    arrowMx.Color = color;
                    arrowPy.Color = colorY;
                    arrowMy.Color = colorY;
                    arrowPz.Color = colorZ;
                    arrowMz.Color = colorZ;

                    ringX.Color = color;
                    ringY.Color = colorY;
                    ringZ.Color = colorZ;
                    break;
                }
            }
        }
        Color ColorFromOrientation(in Msgs.GeometryMsgs.Vector3 direction)
        {
            var (x, y, z) = transform.parent.AsLocalPose().Unity2RosTransform() * direction;
            return new Color(
                Math.Abs((float) x),
                Math.Abs((float) y),
                Math.Abs((float) z));
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
                serializedBounds = newBounds;

                float holderScale;
                switch (InteractionMode)
                {
                    case InteractionModeType.MoveAxisX:
                        holderScale = newBounds.size.z;
                        break;
                    case InteractionModeType.MovePlaneYz:
                    case InteractionModeType.RotateAxisX:
                    case InteractionModeType.MovePlaneYzRotateAxisX:
                        holderScale = Math.Max(newBounds.size.x, newBounds.size.y);
                        break;
                    default:
                        holderScale = Math.Max(newBounds.size.x, Math.Max(newBounds.size.y, newBounds.size.z));
                        break;
                }

                var holder = holderCollider.gameObject;
                holder.transform.localPosition = newBounds.center;
                holder.transform.localScale = 2 * holderScale * Vector3.one;
                holderCollider.size = newBounds.size / (2 * holderScale);


                float maxScale = Math.Max(newBounds.size.x, Math.Max(newBounds.size.y, newBounds.size.z));
                float absoluteScaleY = transform.lossyScale.y;
                menuObject.transform.localScale = 0.5f * Vector3.one;
                menuObject.GetComponent<Billboard>().Offset =
                    new Vector3(0, 1.5f * maxScale + newBounds.center.y, 0.1f * maxScale) * absoluteScaleY;

                frame.Bounds = newBounds;
            }
        }

        public int Layer { get; set; } // ignored

        public bool Interactable
        {
            get => interactable;
            set
            {
                interactable = value;

                int layer = interactable
                    ? LayerType.Clickable
                    : LayerType.IgnoreRaycast;
                gameObject.layer = layer;
                holderCollider.gameObject.layer = layer;
                foreach (var resource in allResources)
                {
                    resource.Layer = layer;
                }

                UpdateColor();
            }
        }

        public event Action Moved;
        public event Action PointerUp;
        public event Action PointerDown;
        public event Action<Vector3> MenuClicked;


        public void Suspend()
        {
            SetColliderInteractableMode(BoundaryMode.None);

            PointsToCamera = false;
            KeepAbsoluteRotation = false;
            InteractionMode = InteractionModeType.None;
            EnableMenu = false;
            Bounds = new Bounds(Vector3.zero, Vector3.one);
            Interactable = true;

            Moved = null;
            PointerUp = null;
            PointerDown = null;
            MenuClicked = null;
        }

        void RaiseMoved()
        {
            if (isEngaged)
            {
                Moved?.Invoke();
            }
        }

        void RaisePointerUp()
        {
            if (!isEngaged)
            {
                return;
            }

            PointerUp?.Invoke();
            isEngaged = false;
        }

        void RaisePointerDown()
        {
            if (isEngaged)
            {
                return;
            }

            isEngaged = true;
            PointerDown?.Invoke();
        }


        void Awake()
        {
            allResources = new[] {arrowPx, arrowMx, arrowPy, arrowMy, arrowPz, arrowMz, ringX, ringY, ringZ};
            Interactable = true;

            foreach (var resource in allResources)
            {
                var draggable = resource.GetComponent<IScreenDraggable>();
                draggable.Moved += RaiseMoved;
                draggable.PointerUp += RaisePointerUp;
                draggable.PointerDown += RaisePointerDown;
            }

            frame = ResourcePool.RentDisplay<BoundaryFrame>(transform);
            frame.FrameAxisLength = 0.125f;
            frame.Bounds = new Bounds(Vector3.zero, 0.5f * Vector3.one);
            frame.Layer = LayerType.IgnoreRaycast;
            frame.Color = FrameInactiveColor;
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
            transform.rotation = TfListener.OriginFrame.Transform.rotation;
        }

        bool isEngaged;

        void IPointerDownHandler.OnPointerDown([NotNull] PointerEventData eventData)
        {
            if (eventData.pointerCurrentRaycast.gameObject != holderCollider.gameObject)
            {
                return;
            }

            RaisePointerDown();
        }

        void IPointerUpHandler.OnPointerUp([NotNull] PointerEventData eventData)
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

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            if (interactionMode != InteractionModeType.ClickOnly ||
                (eventData.pointerCurrentRaycast.gameObject != holderCollider.gameObject &&
                 eventData.pointerCurrentRaycast.gameObject != menuObject))
            {
                return;
            }

            frame.Color = FrameActiveColor;
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData _)
        {
            if (interactionMode != InteractionModeType.ClickOnly)
            {
                return;
            }

            frame.Color = FrameInactiveColor;
        }
    }
#endif
}