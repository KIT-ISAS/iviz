using System;
using Iviz.App;
using Iviz.Controllers;
using Iviz.Core;
using UnityEngine;

namespace Iviz.Displays
{
    public sealed class InteractiveControl : MonoBehaviour
    {
        public delegate void MovedAction(in Pose pose);

        public enum InteractionModeType
        {
            Disabled,

            MoveAxisX,
            MovePlaneYZ,
            RotateAxisX,
            MovePlaneYZ_RotateAxisX,
            Frame,

            Move3D,
            Rotate3D,
            MoveRotate3D
        }

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

        [SerializeField] Transform targetTransform;
        
        InteractionModeType interactionMode;
        bool cameraPivotIsParent;
        bool keepAbsoluteRotation;
        bool pointsToCamera;

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
                    CameraPivotIsParent = false;
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

        public bool CameraPivotIsParent
        {
            get => cameraPivotIsParent;
            set
            {
                if (cameraPivotIsParent == value)
                {
                    return;
                }
                
                cameraPivotIsParent = value;
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

                switch (InteractionMode)
                {
                    case InteractionModeType.Disabled:
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

        public event MovedAction Moved;
        public event Action PointerUp;
        public event Action PointerDown;
        //public event Action DoubleTap;


        public void Stop()
        {
            PointsToCamera = false;
            KeepAbsoluteRotation = false;
        }

        void Awake()
        {
            allResources = new[]
                {arrowPx, arrowMx, arrowPy, arrowMy, arrowPz, arrowMz, ringX, ringY, ringZ, ringXPlane, ringZPlane};


            void OnMoved(in Pose pose) => Moved?.Invoke(pose);
            void OnPointerUp() => PointerUp?.Invoke();
            void OnPointerDown() => PointerDown?.Invoke();
            //void OnDoubleTap() => DoubleTap?.Invoke();

            foreach (var resource in allResources)
            {
                var draggable = resource.GetComponent<IDraggable>();
                draggable.Moved += OnMoved;
                draggable.PointerUp += OnPointerUp;
                draggable.PointerDown += OnPointerDown;
                //draggable.DoubleTap += OnDoubleTap;
            }

            InteractionMode = InteractionModeType.MovePlaneYZ_RotateAxisX;
            /*
            if (TargetTransform == null)
            {
                TargetTransform = transform.parent ?? transform;
            }
            */
        }

        void LateUpdate()
        {
            if (PointsToCamera)
            {
                RotateToCamera();
            }

            /*
            const float referenceDistance = 2.0f;
            Transform cameraTransform = Settings.MainCamera.transform;
            float distanceToCamera = Vector3.Dot(cameraTransform.forward, transform.position - cameraTransform.position);
            if (distanceToCamera > referenceDistance)
            {
                transform.localScale = BaseScale * distanceToCamera / referenceDistance * Vector3.one;
            }
            */
        }

        void RotateToCamera()
        {
            var cameraForward = Settings.MainCamera.transform.forward;
            var mTransform = transform;
            if (CameraPivotIsParent)
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
            transform.rotation = TfListener.RootFrame.transform.rotation;
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
    }
}