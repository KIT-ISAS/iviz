#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using Iviz.Controllers;
using Iviz.Core;
using Iviz.Resources;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.PostProcessing;
using Logger = Iviz.Core.Logger;

// ReSharper disable ConditionIsAlwaysTrueOrFalse
// ReSharper disable HeuristicUnreachableCode

#pragma warning disable CS0162 // Unerreichbarer Code wurde entdeckt.

namespace Iviz.App
{
    public sealed class GuiInputModule : FrameNode, ISettingsManager
    {
        const float MinShadowDistance = 4;

        const float MainSpeed = 2f;
        const float MainAccel = 5f;
        const float BrakeCoeff = 0.9f;

        const float AnimationTime = 0.3f;

        static readonly string[] QualityInViewOptions =
            {"Very Low", "Low", "Medium", "High", "Very High", "Ultra", "Mega"};

        static readonly string[] QualityInArOptions = {"Very Low", "Low", "Medium", "High", "Very High", "Ultra"};
        static readonly Vector3 DirectionWeight = new(1.5f, 1.5f, 1);

        readonly SettingsConfiguration config = new();

        Vector3 accel;
        bool pointerIsAlreadyMoving;
        bool alreadyScaling;
        TfFrame? cameraViewOverride;
        IDraggable? draggedObject;

        bool pointerMotionIsInvalid;

        float? lookAtAnimationStart;
        Pose lookAtCameraStartPose;
        Pose lookAtCameraTargetPose;

        Light? mainLight;

        Vector3 orbitCenter;
        TfFrame? orbitCenterOverride;

        float orbitX, orbitY = 45, orbitRadius = 5.0f;
        Vector2 pointerDownStart;
        float pointerDownTime;
        bool pointerIsOnGui;

        bool pointerIsDown;
        bool altPointerIsDown;

        Vector2 pointerPosition;
        Vector2 altPointerPosition;
        Vector2 lastPointerPosition;
        Vector2 lastAltPointerPosition;

        float distancePointerAndAlt;
        float lastAltDistancePointerAndAlt;

        public static GuiInputModule? Instance { get; private set; }

        static Camera MainCamera => Settings.MainCamera;

        public event Action<ClickInfo>? PointerDown;
        public event Action<ClickInfo>? ShortClick;
        public event Action<ClickInfo>? LongClick;

        public TfFrame? OrbitCenterOverride
        {
            get => lookAtAnimationStart != null ? null : orbitCenterOverride;
            set
            {
                orbitCenterOverride = value;
                if (value != null)
                {
                    CameraViewOverride = null;
                    LookAt(value.AbsoluteUnityPose.position);
                }

                ModuleListPanel.Instance.UnlockButtonVisible = value;
            }
        }

        public TfFrame? CameraViewOverride
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

        public IDraggable? DraggedObject
        {
            get => draggedObject;
            private set
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

        static bool IsDraggingAllowed =>
            Settings.IsMobile
                ? Input.touchCount == 1
                : Settings.IsVR
                    ? Settings.IsVRButtonDown
                    : Mouse.current.rightButton.isPressed;

        void Awake()
        {
            Instance = this;
            Settings.SettingsManager = this;
        }

        void Start()
        {
            if (!Settings.IsMobile)
            {
                QualitySettings.vSyncCount = 0;
                MainCamera.allowHDR = true;
            }

            if (!Settings.SupportsComputeBuffers)
            {
                Logger.Info("Platform does not support compute shaders. Point cloud rendering may look weird.");
            }

            Config = new SettingsConfiguration();

            ModuleListPanel.Instance.UnlockButton.onClick.AddListener(DisableCameraLock);

            mainLight = GameObject.Find("MainLight")?.GetComponent<Light>();

            StartOrbiting();
        }

        void LateUpdate()
        {
            UpdateEvenIfInactive();


            if (CameraViewOverride != null)
            {
                Transform.SetPose(CameraViewOverride.AbsoluteUnityPose);
            }

            var mOrbitCenterOverride = OrbitCenterOverride;

            if (DraggedObject != null)
            {
                if (!Settings.IsMobile && mOrbitCenterOverride == null)
                {
                    ProcessFlying();
                }

                return;
            }

            if (Settings.IsMobile)
            {
                if (mOrbitCenterOverride != null)
                {
                    ProcessOrbiting();
                    ProcessScaling(false);
                    Quaternion q = Quaternion.Euler(orbitY, orbitX, 0);
                    Transform.position = -orbitRadius * (q * Vector3.forward) + orbitCenter;
                    orbitCenter = mOrbitCenterOverride.AbsoluteUnityPose.position;
                }
                else
                {
                    ProcessOrbiting();
                    ProcessScaling(true);
                }

                ProcessTurning();
                ProcessFlying();
                return;
            }

            if (mOrbitCenterOverride != null)
            {
                ProcessOrbiting();
                Quaternion q = Quaternion.Euler(orbitY, orbitX, 0);
                Transform.position = -orbitRadius * (q * Vector3.forward) + orbitCenter;
                orbitCenter = mOrbitCenterOverride.AbsoluteUnityPose.position;
            }
            else
            {
                ProcessTurning();
                ProcessFlying();
            }
        }

        void OnEnable()
        {
            GameThread.EveryFrame -= UpdateEvenIfInactive;
        }

        void OnDisable()
        {
            GameThread.EveryFrame += UpdateEvenIfInactive;
        }

        void OnDestroy()
        {
            Instance = null;
            Settings.SettingsManager = null;
        }

        public int SunDirection
        {
            get => config.SunDirection;
            set
            {
                config.SunDirection = value;
                if (mainLight != null)
                {
                    mainLight.transform.rotation = Quaternion.Euler(90 + value, 0, 0);
                }
            }
        }

        public QualityType QualityInAr
        {
            get => config.QualityInAr;
            set
            {
                config.QualityInAr = value != QualityType.Mega ? value : QualityType.Ultra;
                if (ARController.IsActive)
                {
                    QualitySettings.SetQualityLevel((int) value, true);
                    Settings.RaiseQualityTypeChanged(value);
                }
            }
        }

        public QualityType QualityInView
        {
            get => config.QualityInView;
            set
            {
                config.QualityInView = value;
                if (ARController.IsActive)
                {
                    return;
                }

                if (value == QualityType.Mega)
                {
                    QualitySettings.SetQualityLevel((int) QualityType.Ultra, true);
                    Settings.RaiseQualityTypeChanged(value);
                    MainCamera.renderingPath = RenderingPath.DeferredShading;
                    GetComponent<PostProcessLayer>().enabled = true;
                    return;
                }

                GetComponent<PostProcessLayer>().enabled = false;
                MainCamera.renderingPath = RenderingPath.Forward;
                QualitySettings.SetQualityLevel((int) value, true);
                Settings.RaiseQualityTypeChanged(value);
            }
        }

        public int TargetFps
        {
            get => config.TargetFps;
            set
            {
                config.TargetFps = value;
                UnityEngine.Application.targetFrameRate = value;
            }
        }

        public Color BackgroundColor
        {
            get => config.BackgroundColor;
            set
            {
                Color colorToUse = Settings.IsHololens ? Color.black : value;
                
                config.BackgroundColor = colorToUse.WithAlpha(1);

                Color valueNoAlpha = colorToUse.WithAlpha(0);
                MainCamera.backgroundColor = valueNoAlpha;

                float maxRGB = Mathf.Max(Mathf.Max(colorToUse.r, colorToUse.g), colorToUse.b);
                Color skyColor = maxRGB == 0 ? Color.black : valueNoAlpha / maxRGB;
                RenderSettings.ambientSkyColor = skyColor.WithAlpha(0);
            }
        }

        public int NetworkFrameSkip
        {
            get => config.NetworkFrameSkip;
            set
            {
                config.NetworkFrameSkip = value;
                GameThread.NetworkFrameSkip = value;
            }
        }

        public bool SupportsView => true;

        public bool SupportsAR => Settings.IsMobile;

        public IEnumerable<string> QualityLevelsInView => QualityInViewOptions;

        public IEnumerable<string> QualityLevelsInAR => QualityInArOptions;

        public SettingsConfiguration Config
        {
            get => config;
            set
            {
                BackgroundColor = value.BackgroundColor;
                SunDirection = value.SunDirection;
                NetworkFrameSkip = value.NetworkFrameSkip;
                QualityInAr = value.QualityInAr;
                QualityInView = value.QualityInView;
                TargetFps = value.TargetFps;
            }
        }

        public void DisableCameraLock()
        {
            CameraViewOverride = null;
            OrbitCenterOverride = null;
        }

        public void UpdateQualityLevel()
        {
            QualityInAr = QualityInAr;
            QualityInView = QualityInView;
        }

        public void ResetDraggedObject()
        {
            DraggedObject = null;
        }

        public void TrySetDraggedObject(IDraggable draggable)
        {
            if (!IsDraggingAllowed)
            {
                // reject!
                return;
            }

            DraggedObject = draggable;
        }

        void UpdateEvenIfInactive()
        {
            const float shortClickTime = 0.3f;
            const float longClickTime = 0.5f;
            const float maxDistanceForClickEvent = 20;

            bool prevPointerDown = pointerIsDown;

            QualitySettings.shadowDistance = Mathf.Max(MinShadowDistance, 2 * MainCamera.transform.position.y);

            if (Settings.IsVR) // VR manages its own stuff
            {
                return;
            }

            if (lookAtAnimationStart != null)
            {
                float diff = Time.time - lookAtAnimationStart.Value;
                if (diff > AnimationTime)
                {
                    Transform.SetPose(lookAtCameraTargetPose);
                    lookAtAnimationStart = null;
                    if (OrbitCenterOverride != null)
                    {
                        StartOrbiting();
                    }
                }
                else
                {
                    float t = diff / AnimationTime;
                    Pose currentPose = lookAtCameraStartPose.Lerp(lookAtCameraTargetPose, Mathf.Sqrt(t));
                    Transform.SetPose(currentPose);
                    return;
                }
            }

            //Debug.Log(QualitySettings.shadowDistance);

            if (Settings.IsMobile)
            {
                prevPointerDown |= altPointerIsDown;

                pointerIsDown = Input.touchCount == 1;
                altPointerIsDown = Input.touchCount == 2;

                if (altPointerIsDown)
                {
                    altPointerPosition = (Input.GetTouch(0).position + Input.GetTouch(1).position) / 2;
                }

                distancePointerAndAlt = altPointerIsDown
                    ? Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position)
                    : 0;
                if (pointerIsDown || altPointerIsDown)
                {
                    pointerPosition = Input.GetTouch(0).position;

                    if (!prevPointerDown)
                    {
                        pointerIsOnGui = IsPointerOnGui(pointerPosition) ||
                                         altPointerIsDown && IsPointerOnGui(altPointerPosition);
                        pointerDownTime = Time.time;
                        pointerDownStart = Input.GetTouch(0).position;
                    }
                }
            }
            else
            {
                //pointerIsDown = Input.GetMouseButton(1);
                pointerIsDown = Mouse.current.rightButton.isPressed;

                if (pointerIsDown)
                {
                    //pointerPosition = Input.mousePosition;
                    pointerPosition = Mouse.current.position.ReadValue();

                    if (!prevPointerDown)
                    {
                        pointerIsOnGui = IsPointerOnGui(pointerPosition);
                        pointerDownTime = Time.time;
                        pointerDownStart = pointerPosition;
                    }
                }
                else
                {
                    pointerIsOnGui = false;
                }
            }

            if (!pointerIsDown)
            {
                DraggedObject = null;
            }

            if (DraggedObject != null)
            {
                DraggedObject.OnPointerMove(pointerPosition);
            }
            else if (!pointerIsOnGui)
            {
                bool anyPointerDown = pointerIsDown ||  altPointerIsDown;
                if (!prevPointerDown && anyPointerDown)
                {
                    PointerDown?.Invoke(new ClickInfo(pointerPosition));
                }

                if (prevPointerDown && !anyPointerDown
                    && Vector2.Distance(pointerPosition, pointerDownStart) < maxDistanceForClickEvent)
                {
                    float timeDown = Time.time - pointerDownTime;
                    if (timeDown < shortClickTime)
                    {
                        ShortClick?.Invoke(new ClickInfo(pointerPosition));
                    }
                    else if (timeDown > longClickTime)
                    {
                        LongClick?.Invoke(new ClickInfo(pointerPosition));
                    }
                }
            }
        }

        static bool IsPointerOnGui(Vector2 pointerPosition)
        {
            EventSystem eventSystem = EventSystem.current;
            var results = new List<RaycastResult>();
            eventSystem.RaycastAll(new PointerEventData(eventSystem) {position = pointerPosition}, results);
            return results.Any(result => result.gameObject.layer == LayerType.UI);
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
            if (!pointerIsDown)
            {
                pointerIsAlreadyMoving = false;
                pointerMotionIsInvalid = false;
                return;
            }

            if (!pointerIsAlreadyMoving && pointerIsOnGui)
            {
                pointerMotionIsInvalid = true;
                return;
            }

            if (pointerMotionIsInvalid)
            {
                return;
            }

            Vector2 pointerDiff;
            if (pointerIsAlreadyMoving)
            {
                pointerDiff = pointerPosition - lastPointerPosition;
            }
            else
            {
                pointerDiff = Vector2.zero;
            }

            lastPointerPosition = pointerPosition;
            pointerIsAlreadyMoving = true;

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


            if (Keyboard.current[Key.W].isPressed)
            {
                orbitRadius = Mathf.Max(0, orbitRadius - orbitRadiusAdvance);
            }
            else if (Keyboard.current[Key.S].isPressed)
            {
                orbitRadius += orbitRadiusAdvance;
            }

            Quaternion q = Quaternion.Euler(orbitY, orbitX, 0);
            Transform.SetPositionAndRotation(-orbitRadius * (q * Vector3.forward) + orbitCenter, q);
        }

        void ProcessScaling(bool allowPivotMotion)
        {
            if (!altPointerIsDown)
            {
                alreadyScaling = false;
                return;
            }

            Vector2 pointerAltDiff;
            float altDistanceDiff;
            if (alreadyScaling && !pointerIsOnGui)
            {
                pointerAltDiff = altPointerPosition - lastAltPointerPosition;
                altDistanceDiff = distancePointerAndAlt - lastAltDistancePointerAndAlt;
            }
            else
            {
                pointerAltDiff = Vector2.zero;
                altDistanceDiff = 0;
            }

            lastAltPointerPosition = altPointerPosition;
            lastAltDistancePointerAndAlt = distancePointerAndAlt;

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
            if (!pointerIsDown)
            {
                pointerIsAlreadyMoving = false;
                pointerMotionIsInvalid = false;
                return;
            }

            if (!pointerIsAlreadyMoving && pointerIsOnGui)
            {
                pointerMotionIsInvalid = true;
                return;
            }

            if (pointerMotionIsInvalid)
            {
                return;
            }
            //Debug.Log(alreadyMoving);

            Vector2 pointerDiff;
            if (pointerIsAlreadyMoving)
            {
                pointerDiff = pointerPosition - lastPointerPosition;
            }
            else
            {
                pointerDiff = Vector2.zero;
            }

            lastPointerPosition = pointerPosition;
            pointerIsAlreadyMoving = true;

            const float turnCoeff = 0.1f;
            orbitX += pointerDiff.x * turnCoeff;
            orbitY -= pointerDiff.y * turnCoeff;

            orbitY = Mathf.Min(Mathf.Max(orbitY, -89), 89);

            //Debug.Log(multiplier);
            Transform.rotation = Quaternion.Euler(orbitY, orbitX, 0);
        }

        void ProcessFlying()
        {
            if (!pointerIsDown)
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

            if (baseInput.y == 0)
            {
                accel.y *= BrakeCoeff;
                if (Mathf.Abs(accel.y) < 0.001f)
                {
                    accel.y = 0;
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
            lookAtAnimationStart = Time.time;
            lookAtCameraStartPose = Transform.AsPose();

            float minDistanceLookAt = 0.5f / 0.125f * TfListener.Instance.FrameSize;
            float maxDistanceLookAt = 3.0f / 0.125f * TfListener.Instance.FrameSize;

            if (!Settings.IsMobile)
            {
                float distanceToFrame = (Transform.position - position).magnitude;
                float zoomRadius = Mathf.Min(Mathf.Max(distanceToFrame, minDistanceLookAt), maxDistanceLookAt);
                lookAtCameraTargetPose = lookAtCameraStartPose.WithPosition(position - Transform.forward * zoomRadius);
                //Transform.position = position - Transform.forward * 3;
            }
            else
            {
                orbitCenter = position;
                orbitRadius = Mathf.Min(Mathf.Max(orbitRadius, minDistanceLookAt), maxDistanceLookAt);
                lookAtCameraTargetPose =
                    lookAtCameraStartPose.WithPosition(-orbitRadius * (Transform.rotation * Vector3.forward) +
                                                       orbitCenter);
                //Transform.position = -orbitRadius * (Transform.rotation * Vector3.forward) + orbitCenter;
            }
        }

        static Vector3Int GetBaseInput()
        {
            var pVelocity = new Vector3Int();
            if (Keyboard.current[Key.W].isPressed)
            {
                pVelocity += new Vector3Int(0, 0, 1);
            }

            if (Keyboard.current[Key.S].isPressed)
            {
                pVelocity += new Vector3Int(0, 0, -1);
            }

            if (Keyboard.current[Key.A].isPressed)
            {
                pVelocity += Vector3Int.left;
            }

            if (Keyboard.current[Key.D].isPressed)
            {
                pVelocity += Vector3Int.right;
            }

            if (Keyboard.current[Key.Q].isPressed)
            {
                pVelocity += Vector3Int.down;
            }

            if (Keyboard.current[Key.E].isPressed)
            {
                pVelocity += Vector3Int.up;
            }

            return pVelocity;
        }
    }
}