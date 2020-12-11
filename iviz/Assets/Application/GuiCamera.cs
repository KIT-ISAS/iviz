using System;
using Iviz.Resources;
using System.Collections.Generic;
using Iviz.Controllers;
using Iviz.Core;
using Iviz.Displays;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// ReSharper disable ConditionIsAlwaysTrueOrFalse
// ReSharper disable HeuristicUnreachableCode

#pragma warning disable CS0162 // Unerreichbarer Code wurde entdeckt.

namespace Iviz.App
{
    public sealed class GuiCamera : FrameNode
    {
        const float MinShadowDistance = 4;

        public static GuiCamera Instance { get; private set; }
        
        Vector2 lastPointer;
        Vector2 lastPointerAlt;
        float lastAltDistance;

        float orbitX, orbitY = 45, orbitRadius = 5.0f;

        bool invalidMotion;
        bool alreadyMoving;
        bool alreadyScaling;

        static Camera MainCamera => Settings.MainCamera;

        bool PointerOnGui { get; set; }

        Vector3 orbitCenter;

        TfFrame orbitCenterOverride;

        [CanBeNull]
        public TfFrame OrbitCenterOverride
        {
            get => orbitCenterOverride;
            set
            {
                orbitCenterOverride = value;
                if (value != null)
                {
                    StartOrbiting();
                    CameraViewOverride = null;
                }

                ModuleListPanel.Instance.UnlockButtonVisible = value;
            }
        }

        TfFrame cameraViewOverride;

        [CanBeNull]
        public TfFrame CameraViewOverride
        {
            get => cameraViewOverride;
            set
            {
                cameraViewOverride = value;
                if (value != null)
                {
                    OrbitCenterOverride = null;
                }

                ModuleListPanel.Instance.UnlockButtonVisible = value;
            }
        }

        public void DisableCameraLock()
        {
            CameraViewOverride = null;
            OrbitCenterOverride = null;
        }

        float pointerDownTime;
        Vector2 pointerDownStart;
        
        bool PointerDown
        {
            get;
            set;
        }

        public event Action<Vector2> LongClick; 

        bool PointerAltDown { get; set; }

        Vector2 PointerPosition { get; set; }

        Vector2 PointerAltPosition { get; set; }

        float PointerAltDistance { get; set; }

        IDraggable draggedObject;

        [CanBeNull]
        public IDraggable DraggedObject
        {
            get => draggedObject;
            set
            {
                if (draggedObject == value)
                {
                    return;
                }
                draggedObject?.OnEndDragging();
                draggedObject = value;
                draggedObject?.OnStartDragging();
            }
        }
        
        void Awake()
        {
            Instance = this;
        }
        
        void Start()
        {
#if UNITY_EDITOR
            QualitySettings.vSyncCount = 0;  // VSync must be disabled
            UnityEngine.Application.targetFrameRate = 60;
#endif

            CanvasScaler canvas = GameObject.Find("Canvas").GetComponent<CanvasScaler>();
            canvas.referenceResolution = Settings.IsMobile ? 
                new Vector2(800, 600) :
                new Vector2(800, 800);
                
            QualitySettings.antiAliasing = Settings.IsMobile ? 0 : 2;

            ModuleListPanel.Instance.UnlockButton.onClick.AddListener(DisableCameraLock);

            StartOrbiting();
        }
        
        void OnEnable()
        {
            GameThread.EveryFrame -= UpdateEvenIfInactive;
        }

        void OnDisable()
        {
            GameThread.EveryFrame += UpdateEvenIfInactive;
        }

        void UpdateEvenIfInactive()
        {
            const float longClickTime = 0.5f;
            const float maxDistanceForLongClick = 20;

            bool prevPointerDown = PointerDown;

            QualitySettings.shadowDistance = Mathf.Max(MinShadowDistance, 2 * MainCamera.transform.position.y);
            //Debug.Log(QualitySettings.shadowDistance);

            if (Settings.IsMobile)
            {
                prevPointerDown |= PointerAltDown;
                
                PointerDown = Input.touchCount == 1;
                PointerAltDown = Input.touchCount == 2;
                
                if (PointerAltDown)
                {
                    PointerAltPosition = (Input.GetTouch(0).position + Input.GetTouch(1).position) / 2;
                }

                PointerAltDistance = PointerAltDown
                    ? Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position)
                    : 0;
                if (PointerDown || PointerAltDown)
                {
                    PointerPosition = Input.GetTouch(0).position;
                    
                    if (!prevPointerDown)
                    {
                        PointerOnGui = IsPointerOnGui(PointerPosition) || 
                                       (PointerAltDown && IsPointerOnGui(PointerAltPosition));
                        pointerDownTime = Time.time;
                        pointerDownStart = Input.GetTouch(0).position;
                    }                    
                }
                else
                {
                    if (prevPointerDown 
                        && Time.time - pointerDownTime > longClickTime 
                        && Vector2.Distance(PointerPosition, pointerDownStart) < maxDistanceForLongClick)
                    {
                        LongClick?.Invoke(PointerPosition);
                    }
                }
            }
            else
            {
                PointerDown = Input.GetMouseButton(1);

                if (PointerDown)
                {
                    PointerPosition = Input.mousePosition;

                    if (!prevPointerDown)
                    {
                        PointerOnGui = IsPointerOnGui(PointerPosition);
                        pointerDownTime = Time.time;
                        pointerDownStart = Input.mousePosition;
                    }
                }
                else
                {
                    PointerOnGui = false;
                    if (prevPointerDown 
                        && Time.time - pointerDownTime > longClickTime 
                        && Vector2.Distance(PointerPosition, pointerDownStart) < maxDistanceForLongClick)
                    {
                        LongClick?.Invoke(PointerPosition);
                    }
                }
            }

            if (!PointerDown)
            {
                DraggedObject = null;
            }

            DraggedObject?.OnPointerMove(PointerPosition);
        }

        static bool IsPointerOnGui(Vector2 pointerPosition)
        {
            var eventSystem = EventSystem.current;
            List<RaycastResult> results = new List<RaycastResult>();
            eventSystem.RaycastAll(new PointerEventData(eventSystem) {position = pointerPosition},  results);
            return results.Any(result => result.gameObject.layer == LayerType.UI);            
        }
        
        void LateUpdate()
        {
            UpdateEvenIfInactive();

            if (!(DraggedObject is null))
            {
                return;
            }

            if (!(CameraViewOverride is null))
            {
                Transform.SetPose(CameraViewOverride.AbsolutePose);
            }
                
            if (Settings.IsMobile)
            {
                if (OrbitCenterOverride != null)
                {
                    ProcessOrbiting();
                    ProcessScaling(false);
                    Quaternion q = OrbitCenterOverride.AbsolutePose.rotation * Quaternion.Euler(orbitY, orbitX, 0);
                    Transform.SetPositionAndRotation(
                        -orbitRadius * (q * Vector3.forward) + orbitCenter,
                        q);
                    orbitCenter = OrbitCenterOverride.AbsolutePose.position;
                }
                else
                {
                    ProcessOrbiting();
                    ProcessScaling(true);
                }
            }
            else
            {
                if (OrbitCenterOverride != null)
                {
                    ProcessOrbiting();
                    Quaternion q = OrbitCenterOverride.AbsolutePose.rotation * Quaternion.Euler(orbitY, orbitX, 0);
                    orbitCenter = OrbitCenterOverride.AbsolutePose.position;
                    Transform.SetPositionAndRotation(
                        -orbitRadius * (q * Vector3.forward) + orbitCenter,
                        q);
                }
                else
                {
                    ProcessTurning();
                    ProcessFlying();
                }
            }
        }
        
        void StartOrbiting()
        {
            Vector3 diff = orbitCenter - Transform.position;
            orbitRadius = Mathf.Min(5, diff.magnitude);
            orbitX = Mathf.Atan2(diff.x, diff.z) * Mathf.Rad2Deg;
            orbitY = -Mathf.Atan2(diff.y, new Vector2(diff.x, diff.z).magnitude) * Mathf.Rad2Deg;

            Quaternion q = Quaternion.Euler(orbitY, orbitX, 0);
            Transform.SetPositionAndRotation(
                -orbitRadius * (q * Vector3.forward) + orbitCenter,
                q);
        }

        void ProcessOrbiting()
        {
            if (!PointerDown)
            {
                alreadyMoving = false;
                invalidMotion = false;
                return;
            }

            if (!alreadyMoving && PointerOnGui)
            {
                invalidMotion = true;
                return;
            }

            if (invalidMotion)
            {
                return;
            }

            Vector2 pointerDiff;
            if (alreadyMoving)
            {
                pointerDiff = PointerPosition - lastPointer;
            }
            else
            {
                pointerDiff = Vector2.zero;
            }

            lastPointer = PointerPosition;
            alreadyMoving = true;

            const float orbitCoeff = 0.1f;
            const float orbitRadiusAdvance = 0.1f;

            orbitX += pointerDiff.x * orbitCoeff;
            orbitY -= pointerDiff.y * orbitCoeff;
            if (orbitY > 90)
            {
                orbitY = 90;
            }

            if (orbitY < -90)
            {
                orbitY = -90;
            }


            if (Input.GetKey(KeyCode.W))
            {
                orbitRadius = Mathf.Max(0, orbitRadius - orbitRadiusAdvance);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                orbitRadius += orbitRadiusAdvance; // TODO: time scaling
            }

            Quaternion q = Quaternion.Euler(orbitY, orbitX, 0);
            Transform.SetPositionAndRotation(-orbitRadius * (q * Vector3.forward) + orbitCenter, q);
        }

        void ProcessScaling(bool allowPivotMotion)
        {
            if (!PointerAltDown)
            {
                alreadyScaling = false;
                return;
            }

            Vector2 pointerAltDiff;
            float altDistanceDiff;
            if (alreadyScaling)
            {
                pointerAltDiff = PointerAltPosition - lastPointerAlt;
                altDistanceDiff = PointerAltDistance - lastAltDistance;
            }
            else
            {
                pointerAltDiff = Vector2.zero;
                altDistanceDiff = 0;
            }

            lastPointerAlt = PointerAltPosition;
            lastAltDistance = PointerAltDistance;

            alreadyScaling = true;


            const float radiusCoeff = -0.0025f;
            const float tangentCoeff = 0.001f;
            const float minOrbitRadius = 0.1f;

            orbitRadius += altDistanceDiff * radiusCoeff;

            Transform mTransform = Transform;
            Quaternion q = mTransform.rotation;

            if (!allowPivotMotion)
            {
                if (orbitRadius < minOrbitRadius)
                {
                    orbitRadius = minOrbitRadius;
                }
            }
            else
            {
                if (orbitRadius < 0.5f)
                {
                    float diff = 0.5f - orbitRadius;
                    orbitCenter += diff * (q * Vector3.forward);
                    orbitRadius = 0.5f;
                }

                float orbitScale = 0.75f * orbitRadius;
                orbitCenter -= tangentCoeff * pointerAltDiff.x * orbitScale *
                               mTransform.TransformDirection(Vector3.right);
                orbitCenter += tangentCoeff * pointerAltDiff.y * orbitScale *
                               mTransform.TransformDirection(Vector3.down);
            }

            mTransform.position = -orbitRadius * (q * Vector3.forward) + orbitCenter;
        }

        void ProcessTurning()
        {
            if (!PointerDown)
            {
                alreadyMoving = false;
                invalidMotion = false;
                return;
            }

            if (!alreadyMoving && PointerOnGui)
            {
                invalidMotion = true;
                return;
            }

            if (invalidMotion)
            {
                return;
            }
            //Debug.Log(alreadyMoving);

            Vector2 pointerDiff;
            if (alreadyMoving)
            {
                pointerDiff = PointerPosition - lastPointer;
            }
            else
            {
                pointerDiff = Vector2.zero;
            }

            lastPointer = PointerPosition;
            alreadyMoving = true;

            const float turnCoeff = 0.1f;

            orbitX += pointerDiff.x * turnCoeff;
            orbitY -= pointerDiff.y * turnCoeff;

            orbitY = Mathf.Min(Mathf.Max(orbitY, -89), 89);

            Transform.rotation = Quaternion.Euler(orbitY, orbitX, 0);
        }

        const float MainSpeed = 2f; 
        const float MainAccel = 5f; 
        const float BrakeCoeff = 0.9f; 
        static readonly Vector3 DirectionWeight = new Vector3(1.5f,  0, 1);
        
        Vector3 accel;
        void ProcessFlying()
        {
            if (!PointerDown)
            {
                accel = Vector3.zero;
                return;
            }
            
            Vector3Int baseInput = GetBaseInput();
            float deltaTime = Time.deltaTime;
            
            Vector3 speed = deltaTime * Vector3.Scale(baseInput, MainSpeed * DirectionWeight);

            accel += Vector3.Scale(baseInput, MainAccel * DirectionWeight) * deltaTime;
            if (baseInput.x == 0)
            {
                accel.x *= BrakeCoeff;
                if (Mathf.Abs(accel.x) < 0.001f)
                {
                    accel.x = 0;
                }
            }
            if (baseInput.z == 0)
            {
                accel.z *= BrakeCoeff;
                if (Mathf.Abs(accel.z) < 0.001f)
                {
                    accel.z = 0;
                }
            }
            speed += deltaTime * accel;  

            Transform.position += Transform.rotation * speed;
        }

        public void LookAt(in Vector3 position)
        {
            if (!Settings.IsMobile)
            {
                Transform.position = position - Transform.forward * 3;
            }
            else
            {
                const float maxOrbitRadiusLookAt = 3.0f;
                    
                orbitCenter = position;
                orbitRadius = Mathf.Min(orbitRadius, maxOrbitRadiusLookAt);
                Transform.position = -orbitRadius * (Transform.rotation * Vector3.forward) + orbitCenter;
            }
        }

        static Vector3Int GetBaseInput()
        {
            Vector3Int pVelocity = new Vector3Int();
            if (Input.GetKey(KeyCode.W))
            {
                pVelocity += new Vector3Int(0, 0, 1);
            }

            if (Input.GetKey(KeyCode.S))
            {
                pVelocity += new Vector3Int(0, 0, -1);
            }

            if (Input.GetKey(KeyCode.A))
            {
                pVelocity += new Vector3Int(-1, 0, 0);
            }

            if (Input.GetKey(KeyCode.D))
            {
                pVelocity += new Vector3Int(1, 0, 0);
            }

            return pVelocity;
        }
    }
}