using System;
using System.Collections.Generic;
using System.Linq;
using Iviz.Controllers;
using Iviz.Core;
using Iviz.Resources;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;
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
        static readonly Vector3 DirectionWeight = new Vector3(1.5f, 0, 1);

        readonly SettingsConfiguration config = new SettingsConfiguration();

        Vector3 accel;
        bool pointerIsAlreadyMoving;
        bool alreadyScaling;
        [CanBeNull] TfFrame cameraViewOverride;

        IDraggable draggedObject;

        bool pointerMotionIsInvalid;

        float? lookAtAnimationStart;
        Pose lookAtCameraStartPose;
        Pose lookAtCameraTargetPose;

        [CanBeNull] Light mainLight;

        Vector3 orbitCenter;
        [CanBeNull] TfFrame orbitCenterOverride;

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

        public static GuiInputModule Instance { get; private set; }

        [NotNull] static Camera MainCamera => Settings.MainCamera;

        [CanBeNull]
        public TfFrame OrbitCenterOverride
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

        [CanBeNull]
        public IDraggable DraggedObject
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
                    : Input.GetMouseButton(1);

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

            if (DraggedObject != null)
            {
                if (!Settings.IsMobile && OrbitCenterOverride == null)
                {
                    ProcessFlying();
                }

                return;
            }

            if (Settings.IsMobile)
            {
                if (OrbitCenterOverride != null)
                {
                    ProcessOrbiting();
                    ProcessScaling(false);
                    Quaternion q = Quaternion.Euler(orbitY, orbitX, 0);
                    Transform.position = -orbitRadius * (q * Vector3.forward) + orbitCenter;
                    orbitCenter = OrbitCenterOverride.AbsoluteUnityPose.position;
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

            if (OrbitCenterOverride != null)
            {
                ProcessOrbiting();
                Quaternion q = Quaternion.Euler(orbitY, orbitX, 0);
                Transform.position = -orbitRadius * (q * Vector3.forward) + orbitCenter;
                orbitCenter = OrbitCenterOverride.AbsoluteUnityPose.position;
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
                if (ARController.HasARController)
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
                if (ARController.HasARController)
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
                config.BackgroundColor = value.WithAlpha(1);

                Color valueNoAlpha = value.WithAlpha(0);
                MainCamera.backgroundColor = valueNoAlpha;

                float maxRGB = Mathf.Max(Mathf.Max(value.r, value.g), value.b);
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

        public event Action<Vector2> LongClick;

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
            const float longClickTime = 0.5f;
            const float maxDistanceForLongClick = 20;

            bool prevPointerDown = pointerIsDown;

            QualitySettings.shadowDistance = Mathf.Max(MinShadowDistance, 2 * MainCamera.transform.position.y);


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
                else
                {
                    if (prevPointerDown
                        && Time.time - pointerDownTime > longClickTime
                        && Vector2.Distance(pointerPosition, pointerDownStart) < maxDistanceForLongClick)
                    {
                        LongClick?.Invoke(pointerPosition);
                    }
                }
            }
            else if (!Settings.IsVR)
            {
                pointerIsDown = Input.GetMouseButton(1);

                if (pointerIsDown)
                {
                    pointerPosition = Input.mousePosition;

                    if (!prevPointerDown)
                    {
                        pointerIsOnGui = IsPointerOnGui(pointerPosition);
                        pointerDownTime = Time.time;
                        pointerDownStart = Input.mousePosition;
                    }
                }
                else
                {
                    pointerIsOnGui = false;
                    if (prevPointerDown
                        && Time.time - pointerDownTime > longClickTime
                        && Vector2.Distance(pointerPosition, pointerDownStart) < maxDistanceForLongClick)
                    {
                        LongClick?.Invoke(pointerPosition);
                    }
                }
            }

            if (!Settings.IsVR) // VR manages its own dragging
            {
                if (!pointerIsDown)
                {
                    DraggedObject = null;
                }

                DraggedObject?.OnPointerMove(pointerPosition);
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


            if (Input.GetKey(KeyCode.W))
            {
                orbitRadius = Mathf.Max(0, orbitRadius - orbitRadiusAdvance);
            }
            else if (Input.GetKey(KeyCode.S))
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
                pVelocity += Vector3Int.left;
            }

            if (Input.GetKey(KeyCode.D))
            {
                pVelocity += Vector3Int.right;
            }

            return pVelocity;
        }
    }
}