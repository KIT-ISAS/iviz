#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Common;
using Iviz.Common.Configurations;
using Iviz.Controllers;
using Iviz.Controllers.TF;
using Iviz.Controllers.XR;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Displays.Highlighters;
using Iviz.Resources;
using Iviz.Tools;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using IDragHandler = Iviz.Core.IDragHandler;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace Iviz.App
{
    public sealed class GuiInputModule : MonoBehaviour, ISettingsManager, IHasFrame, IDragHandler
    {
        const float MainSpeed = 2f;
        const float MainAccel = 5f;
        const float BrakeCoeff = 0.9f;
        const float LookAtAnimationTime = 0.3f;

        static readonly string[] QualityInViewOptions =
            { "Very Low", "Low", "Medium", "High", "Very High", "Ultra" };

        static readonly string[] QualityInArOptions = { "Very Low", "Low", "Medium", "High", "Very High", "Ultra" };

        /// Weights for keyboard movements. Forward is slower.
        static readonly Vector3 MoveDirectionWeight = new(1.5f, 1.5f, 1);

        static GuiInputModule? instance;
        static uint tapSeq;

        static bool IsDraggingAllowed =>
            Settings.IsXR || (Settings.IsPhone
                //? Input.touchCount == 1
                ? Touch.activeTouches.Count == 1
                : Mouse.current.rightButton.isPressed);

        static Camera MainCamera => Settings.MainCamera;

        public static GuiInputModule Instance =>
            instance != null
                ? instance
                : instance = ((GuiInputModule)Settings.SettingsManager).AssertNotNull(nameof(instance));

        readonly SettingsConfiguration config = new();

        Vector3 accel;
        Vector3 velocity;
        bool pointerIsAlreadyMoving;
        bool alreadyScaling;
        TfFrame? cameraViewOverride;
        IScreenDraggable? draggedObject;

        bool pointerMotionIsInvalid;

        CancellationTokenSource? lookAtTokenSource;

        Light? mainLight;

        Vector3 orbitCenter;
        TfFrame? orbitCenterOverride;

        float orbitRadius = 5.0f;
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

        LeashDisplay? leash;
        Transform? mTransform;

        LeashDisplay Leash => leash != null ? leash : (leash = ResourcePool.RentDisplay<LeashDisplay>());
        Transform Transform => mTransform != null ? mTransform : (mTransform = transform);
        bool IsLookAtAnimationRunning => lookAtTokenSource is { IsCancellationRequested: false };
        Quaternion OrbitRotation => Quaternion.Euler(CameraPitch, CameraYaw, CameraRoll);

        IScreenDraggable? DraggedObject
        {
            get => draggedObject;
            set
            {
                if (draggedObject == value)
                {
                    return;
                }

                try
                {
                    draggedObject?.OnEndDragging();
                }
                catch (Exception e)
                {
                    RosLogger.Error($"{this}: Error during {nameof(IScreenDraggable.OnEndDragging)}", e);
                }

                draggedObject = value;

                try
                {
                    draggedObject?.OnStartDragging();
                }
                catch (Exception e)
                {
                    RosLogger.Error($"{this}: Error during {nameof(IScreenDraggable.OnStartDragging)}", e);
                }

                Leash.Visible = draggedObject != null;
            }
        }

        public event Action<ClickHitInfo>? LongClick;

        public TfFrame? OrbitCenterOverride
        {
            get => IsLookAtAnimationRunning ? null : orbitCenterOverride;
            set
            {
                orbitCenterOverride = value;
                if (value != null)
                {
                    CameraViewOverride = null;
                    LookAt(value.Transform);
                }

                ModuleListPanel.Instance.UnlockButtonVisible = value != null;
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

                ModuleListPanel.Instance.UnlockButtonVisible = value != null;
            }
        }

        public float CameraYaw { get; set; }
        public float CameraPitch { get; set; } = 45;
        public float CameraRoll { get; set; }

        public Vector3 CameraRpy
        {
            get => new(CameraRoll, CameraPitch, CameraYaw);
            set => (CameraRoll, CameraPitch, CameraYaw) = UnityUtils.RegularizeRpy(value);
        }

        public Vector3 CameraPosition
        {
            get => Transform.localPosition;
            set
            {
                if (OrbitCenterOverride != null || CameraViewOverride != null)
                {
                    return;
                }

                Transform.localPosition = value;
                if (Settings.IsPhone)
                {
                    orbitCenter = value + orbitRadius * OrbitRotation.Forward();
                }
            }
        }

        public float CameraFieldOfView
        {
            get => Settings.VirtualCamera == null ? 0 : Settings.VirtualCamera.GetHorizontalFov();
            set
            {
                if (Settings.VirtualCamera != null)
                {
                    Settings.VirtualCamera.SetHorizontalFov(value);
                }
            }
        }

        public TfFrame Frame => TfModule.FixedFrame;

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
                config.QualityInAr = ValidateQuality(value);
                if (!ARController.IsActive)
                {
                    return;
                }

                QualitySettings.SetQualityLevel((int)config.QualityInAr, true);
                UpdateQualityLevel(config.QualityInAr);
            }
        }

        public QualityType QualityInView
        {
            get => config.QualityInView;
            set
            {
                config.QualityInView = ValidateQuality(value);

                var qualityToUse = Settings.IsHololens ? QualityType.VeryLow : config.QualityInView;

                if (ARController.IsActive)
                {
                    return;
                }

                QualitySettings.SetQualityLevel((int)qualityToUse, true);
                UpdateQualityLevel(qualityToUse);
            }
        }

        void UpdateQualityLevel(QualityType qualityToUse)
        {
            // update materials
            bool isVeryLow = qualityToUse == QualityType.VeryLow;
            if (Settings.UseSimpleMaterials != isVeryLow)
            {
                Settings.UseSimpleMaterials = isVeryLow;
                foreach (var display in GetAllRootChildren().WithComponent<MeshMarkerDisplay>())
                {
                    display.UpdateMaterial();
                }
            }

            // update main light
            if (mainLight == null)
            {
                return;
            }

            if (isVeryLow)
            {
                mainLight.renderMode = LightRenderMode.ForceVertex;
                mainLight.intensity = Settings.IsHololens ? 2 : 1;
                mainLight.shadows = LightShadows.None;
            }
            else
            {
                mainLight.renderMode = LightRenderMode.Auto;
                mainLight.intensity = 1;
                mainLight.shadows = LightShadows.Soft;
            }
        }

        static QualityType ValidateQuality(QualityType qualityType)
        {
            return qualityType switch
            {
                < QualityType.VeryLow => QualityType.VeryLow,
                > QualityType.Ultra => QualityType.Ultra,
                _ => qualityType
            };
        }

        public int TargetFps
        {
            get => config.TargetFps;
            set
            {
                config.TargetFps = value;
                Application.targetFrameRate = value;
            }
        }

        public Color BackgroundColor
        {
            get => config.BackgroundColor.ToUnity();
            set
            {
                config.BackgroundColor = value.WithAlpha(1).ToRos();

                Color colorToUse = Settings.IsHololens ? Color.black : value;
                MainCamera.backgroundColor = colorToUse.WithAlpha(0);

                RenderSettings.ambientSkyColor = value.WithAlpha(0);

                Color.RGBToHSV(value, out float h, out float s, out float v);
                var equatorColor = Color.HSVToRGB(h, Math.Min(s, 0.3f), v * 0.5f);
                RenderSettings.ambientEquatorColor = equatorColor;
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

        public IEnumerable<string> QualityLevelsInView => QualityInViewOptions;

        public IEnumerable<string> QualityLevelsInAR => QualityInArOptions;

        public SettingsConfiguration Config
        {
            get => config;
            set
            {
                BackgroundColor = value.BackgroundColor.ToUnity();
                SunDirection = value.SunDirection;
                NetworkFrameSkip = value.NetworkFrameSkip;
                QualityInAr = value.QualityInAr;
                QualityInView = value.QualityInView;
                TargetFps = value.TargetFps;
            }
        }

        void Awake()
        {
            EnhancedTouchSupport.Enable();
            Leash.Color = Color.white.WithAlpha(0.75f);
        }

        void OnDestroy()
        {
            instance = null;
            TfListener.AfterProcessMessages -= ProcessPoseChanges;
            GameThread.EveryFrame -= ProcessPointer;
        }

        void Start()
        {
            if (!Settings.IsPhone)
            {
                QualitySettings.vSyncCount = 0;
            }

            if (!Settings.IsXR)
            {
                Settings.VirtualCamera = MainCamera;
            }

            if (!Settings.SupportsComputeBuffers)
            {
                RosLogger.Info("Platform does not support compute shaders. Point cloud rendering may look weird.");
            }

            mainLight = GameObject.Find("MainLight")?.GetComponent<Light>();

            Config = new SettingsConfiguration();

            ModuleListPanel.Instance.UnlockButton.onClick.AddListener(DisableCameraLock);

            StartOrbiting();

            TfListener.AfterProcessMessages += ProcessPoseChanges;

            ModuleListPanel.CallAfterInitialized(() => TfModule.Instance.ResetFrames += OnResetFrames);
        }

        void LateUpdate()
        {
            ProcessPointer();
        }

        void ProcessPoseChanges()
        {
            if (this == null)
            {
                Debug.Log("ProcessPose when dead!");
                return;
            }

            if (CameraViewOverride != null)
            {
                Transform.SetPose(CameraViewOverride.AbsoluteUnityPose);
            }

            var mOrbitCenterOverride = OrbitCenterOverride;

            if (DraggedObject != null)
            {
                if (!Settings.IsPhone && mOrbitCenterOverride == null)
                {
                    ProcessFlying();
                    Transform.localPosition += Transform.localRotation * velocity;
                }

                return;
            }

            if (Settings.IsPhone)
            {
                if (mOrbitCenterOverride != null)
                {
                    ProcessOrbiting();
                    ProcessScaling(false);
                    orbitCenter = mOrbitCenterOverride.AbsoluteUnityPose.position; // !
                }
                else
                {
                    ProcessOrbiting();
                    ProcessScaling(true);
                }

                ProcessTurning();
                ProcessFlying();

                var rotation = OrbitRotation;
                Transform.SetLocalPose(-orbitRadius * rotation.Forward() + orbitCenter + rotation * velocity, rotation);
                return;
            }

            if (mOrbitCenterOverride != null)
            {
                ProcessOrbiting();

                var rotation = OrbitRotation;
                orbitCenter = mOrbitCenterOverride.AbsoluteUnityPose.position;
                Transform.SetLocalPose(-orbitRadius * rotation.Forward() + orbitCenter, rotation);
            }
            else
            {
                ProcessTurning();
                ProcessFlying();

                var rotation = OrbitRotation;
                Transform.localRotation = rotation;
                Transform.localPosition += rotation * velocity;
            }
        }

        void OnEnable()
        {
            GameThread.EveryFrame -= ProcessPointer;
        }

        void OnDisable()
        {
            GameThread.EveryFrame += ProcessPointer;
        }

        void OnResetFrames()
        {
            CameraViewOverride = null;
            OrbitCenterOverride = null;
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

        public float XRDraggableNearDistance => Settings.IsXR ? XRController.NearDistance : float.MaxValue;

        public void TryUnsetDraggedObject(IScreenDraggable draggable)
        {
            // do not fetch Instance here, we may be in the middle of shutting down the scene
            if (instance == null)
            {
                return;
            }

            if (instance.DraggedObject != draggable)
            {
                return;
            }

            instance.DraggedObject = null;
        }

        public void TrySetDraggedObject(IScreenDraggable draggable)
        {
            if (!IsDraggingAllowed)
            {
                // reject!
                return;
            }

            Instance.DraggedObject = draggable;
        }

        void ProcessPointer()
        {
            const float shortClickTime = 0.3f;
            const float longClickTime = 0.5f;
            const float maxDistanceForClickEvent = 20;

            if (Settings.IsXR) // XR doesn't need cursor management
            {
                return;
            }

            bool prevPointerDown = !Settings.IsPhone
                ? pointerIsDown
                : pointerIsDown | altPointerIsDown;

            if (Settings.IsPhone)
            {
                var activeTouches = Touch.activeTouches;

                pointerIsDown = activeTouches.Count == 1;
                altPointerIsDown = activeTouches.Count == 2;

                if (altPointerIsDown)
                {
                    altPointerPosition = (activeTouches[0].screenPosition + activeTouches[1].screenPosition) / 2;
                }

                distancePointerAndAlt = altPointerIsDown
                    ? Vector2.Distance(activeTouches[0].screenPosition, activeTouches[1].screenPosition)
                    : 0;
                if (pointerIsDown || altPointerIsDown)
                {
                    pointerPosition = activeTouches[0].screenPosition;

                    if (!prevPointerDown)
                    {
                        pointerIsOnGui = IsPointerOnGui(pointerPosition) ||
                                         altPointerIsDown && IsPointerOnGui(altPointerPosition);
                        pointerDownTime = Time.time;
                        pointerDownStart = activeTouches[0].screenPosition;
                    }
                }
            }
            else
            {
                pointerIsDown = Mouse.current.rightButton.isPressed;

                if (pointerIsDown)
                {
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

            // are we dragging?
            if (!pointerIsDown)
            {
                DraggedObject = null;
            }

            if (DraggedObject != null)
            {
                var pointerRay = Settings.MainCamera.ScreenPointToRay(pointerPosition);
                DraggedObject.OnPointerMove(pointerRay);
                if (DraggedObject.ReferencePoint is { } referencePoint
                    && DraggedObject.ReferenceNormal is { } referenceNormal)
                {
                    Leash.Set(pointerRay, referencePoint, referenceNormal);
                }

                return;
            }

            // check if we are clicking something interesting
            bool anyPointerDown = pointerIsDown || altPointerIsDown;
            if (prevPointerDown
                && !anyPointerDown
                && Vector2.Distance(pointerPosition, pointerDownStart) < maxDistanceForClickEvent)
            {
                if (IsPointerOnGui(pointerPosition)) // pointerIsOnGui is only set on button down
                {
                    return;
                }

                var clickInfo = new ClickHitInfo(pointerPosition);
                float timeDown = Time.time - pointerDownTime;
                if (timeDown < shortClickTime)
                {
                    OnClick(clickInfo, true);
                }
                else if (timeDown > longClickTime)
                {
                    LongClick?.Invoke(clickInfo);
                    OnClick(clickInfo, false);
                }
            }
        }

        static bool IsPointerOnGui(Vector2 pointerPosition)
        {
            var eventSystem = EventSystem.current;
            var results = new List<RaycastResult>();
            eventSystem.RaycastAll(new PointerEventData(eventSystem) { position = pointerPosition }, results);
            return results.Any(result => result.gameObject.layer == LayerType.UI);
        }

        void StartOrbiting()
        {
            var diff = orbitCenter - Transform.localPosition;
            orbitRadius = Math.Min(5, diff.magnitude);

            var (diffX, diffY, diffZ) = diff;
            CameraYaw = Mathf.Atan2(diffX, diffZ) * Mathf.Rad2Deg;
            CameraPitch = -Mathf.Atan2(diffY, Mathf.Sqrt(diffX * diffX + diffZ * diffZ)) * Mathf.Rad2Deg;

            var rotation = OrbitRotation;
            Transform.SetLocalPose(-orbitRadius * rotation.Forward() + orbitCenter, rotation);
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

            var (pointerDiffX, pointerDiffY) = pointerIsAlreadyMoving
                ? pointerPosition - lastPointerPosition
                : Vector2.zero;

            lastPointerPosition = pointerPosition;
            pointerIsAlreadyMoving = true;

            const float orbitCoeff = 0.1f;
            const float orbitRadiusAdvance = 0.1f;
            const float minPitchInDeg = -90;
            const float maxPitchInDeg = 90;

            CameraYaw += pointerDiffX * orbitCoeff;
            CameraPitch -= pointerDiffY * orbitCoeff;
            CameraPitch = Mathf.Clamp(CameraPitch, minPitchInDeg, maxPitchInDeg);

            if (Keyboard.current[Key.W].isPressed)
            {
                orbitRadius = Math.Max(0, orbitRadius - orbitRadiusAdvance);
            }
            else if (Keyboard.current[Key.S].isPressed)
            {
                orbitRadius += orbitRadiusAdvance;
            }
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
            const float minDistanceToAdvance = 0.5f;

            orbitRadius += altDistanceDiff * radiusCoeff;

            Quaternion q = Transform.localRotation;

            if (!allowPivotMotion)
            {
                if (orbitRadius < minOrbitRadius)
                {
                    orbitRadius = minOrbitRadius;
                }
            }
            else
            {
                if (orbitRadius < minDistanceToAdvance)
                {
                    // move forward
                    orbitCenter += (minDistanceToAdvance - orbitRadius) * q.Forward();
                    orbitRadius = minDistanceToAdvance;
                }

                float orbitScale = 0.75f * orbitRadius;
                orbitCenter -= tangentCoeff * pointerAltDiff.x * orbitScale * (q * Vector3.right);
                orbitCenter += tangentCoeff * pointerAltDiff.y * orbitScale * (q * Vector3.down);
            }
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

            var (pointerDiffX, pointerDiffY) = pointerIsAlreadyMoving
                ? pointerPosition - lastPointerPosition
                : Vector2.zero;

            lastPointerPosition = pointerPosition;
            pointerIsAlreadyMoving = true;

            const float turnCoeff = 0.1f;
            const float minPitchInDeg = -89;
            const float maxPitchInDeg = 89;

            CameraYaw += pointerDiffX * turnCoeff;
            CameraPitch -= pointerDiffY * turnCoeff;
            CameraPitch = Mathf.Clamp(CameraPitch, minPitchInDeg, maxPitchInDeg);
        }

        void ProcessFlying()
        {
            if (!pointerIsDown)
            {
                accel = Vector3.zero;
                velocity = Vector3.zero;
                return;
            }

            var baseInput = GetBaseInput();
            float deltaTime = Time.deltaTime;
            const float minAccelToStop = 0.001f;

            accel += baseInput.Mult(MainAccel * MoveDirectionWeight) * deltaTime;

            if (baseInput.x == 0)
            {
                accel.x *= BrakeCoeff;
                if (Math.Abs(accel.x) < minAccelToStop)
                {
                    accel.x = 0;
                }
            }

            if (baseInput.y == 0)
            {
                accel.y *= BrakeCoeff;
                if (Math.Abs(accel.y) < minAccelToStop)
                {
                    accel.y = 0;
                }
            }

            if (baseInput.z == 0)
            {
                accel.z *= BrakeCoeff;
                if (Math.Abs(accel.z) < minAccelToStop)
                {
                    accel.z = 0;
                }
            }

            velocity = deltaTime * (baseInput.Mult(MainSpeed * MoveDirectionWeight) + accel);
        }

        public void LookAt(Transform targetTransform, Vector3? localOffset = null)
        {
            lookAtTokenSource?.Cancel();
            CancellationTokenSource newToken = new();
            lookAtTokenSource = newToken;

            var lookAtCameraStartPosition = CameraPosition;

            void Update(float t)
            {
                var targetPosition = localOffset is { } validatedOffset
                    ? targetTransform.TransformPoint(validatedOffset)
                    : targetTransform.position;

                var lookAtCameraTargetPosition = CalculateTargetCameraPosition(targetPosition);
                var currentPosition =
                    Vector3.Lerp(lookAtCameraStartPosition, lookAtCameraTargetPosition, Mathf.Sqrt(t));
                CameraPosition = currentPosition;
            }

            void Dispose()
            {
                newToken.Cancel();
                if (OrbitCenterOverride != null)
                {
                    StartOrbiting();
                }
            }

            FAnimator.Spawn(lookAtTokenSource.Token, LookAtAnimationTime, Update, Dispose);
        }

        Vector3 CalculateTargetCameraPosition(in Vector3 targetPosition)
        {
            float baseScale = 1f / 0.125f * TfModule.Instance.FrameSize;
            float minDistanceLookAt = 0.5f * baseScale;
            float maxDistanceLookAt = 3.0f * baseScale;

            if (!Settings.IsPhone)
            {
                float distanceToFrame = (Transform.localPosition - targetPosition).magnitude;
                float zoomRadius = Mathf.Clamp(distanceToFrame, minDistanceLookAt, maxDistanceLookAt);
                return targetPosition - Transform.localRotation.Forward() * zoomRadius;
            }

            orbitCenter = targetPosition;
            orbitRadius = Mathf.Clamp(orbitRadius, minDistanceLookAt, maxDistanceLookAt);
            return -orbitRadius * Transform.localRotation.Forward() + orbitCenter;
        }

        static Vector3Int GetBaseInput()
        {
            var pVelocity = new Vector3Int();
            if (Keyboard.current[Key.S].isPressed)
            {
                pVelocity.z = -1;
            }

            if (Keyboard.current[Key.W].isPressed)
            {
                pVelocity.z++;
            }

            if (Keyboard.current[Key.A].isPressed)
            {
                pVelocity.x = -1;
            }

            if (Keyboard.current[Key.D].isPressed)
            {
                pVelocity.x++;
            }

            if (Keyboard.current[Key.Q].isPressed)
            {
                pVelocity.y = -1;
            }

            if (Keyboard.current[Key.E].isPressed)
            {
                pVelocity.y++;
            }

            return pVelocity;
        }

        void OnClick(ClickHitInfo clickHitInfo, bool isShortClick)
        {
            TriggerEnvironmentClick(clickHitInfo);
        }

        public static void TriggerEnvironmentClick(ClickHitInfo clickHitInfo)
        {
            if (clickHitInfo.TryGetRaycastResults(out var hitResults))
            {
                Vector3? firstHitPoint = null;
                List<(IHighlightable highlightable, Vector3 hitPoint)>? hits = null;
                foreach (var (hitObject, hitPoint, _) in hitResults)
                {
                    if (!TryGetHighlightable(hitObject, out var toHighlight))
                    {
                        continue;
                    }

                    if (firstHitPoint is not { } referencePoint)
                    {
                        firstHitPoint = hitPoint;
                    }
                    else if (Vector3.Distance(hitPoint, referencePoint) > 0.25f)
                    {
                        continue;
                    }

                    hits ??= new List<(IHighlightable, Vector3)>();
                    hits.Add((toHighlight, hitPoint));
                }

                switch (hits?.Count)
                {
                    case 1:
                        hits[0].highlightable.Highlight(hits[0].hitPoint);
                        return;
                    case > 1:
                        HighlightAll(hits);
                        return;
                }
            }

            Pose poseToHighlight;
            if (clickHitInfo.TryGetARRaycastResults(out var arHitResults))
            {
                poseToHighlight = arHitResults[0].AsPose();
            }
            else if (hitResults.Length != 0)
            {
                if (hitResults.Any(result => result.GameObject.layer is LayerType.Clickable))
                {
                    return;
                }

                poseToHighlight = hitResults[0].AsPose();
            }
            else
            {
                return;
            }

            FAnimator.Start(new ClickedPoseHighlighter(poseToHighlight));
        }

        static async void HighlightAll(IEnumerable<(IHighlightable highlightable, Vector3)> hits)
        {
            var aliveHits = hits.Where(toHighlight => toHighlight.highlightable.IsAlive);
            foreach (var (highlightable, hitPoint) in aliveHits)
            {
                highlightable.Highlight(hitPoint);
                await Task.Delay(250);
            }
        }

        static bool TryGetHighlightable(GameObject gameObject, out IHighlightable h)
        {
            return gameObject.TryGetComponent(out h) ||
                   gameObject.transform.parent is { } parent && parent.TryGetComponent(out h);
        }

        public static void PlayClickAudio(in Vector3 position)
        {
            var assetHolder = Resource.Extras.AppAssetHolder;
            AudioSource.PlayClipAtPoint(assetHolder.Click, position);
        }

        static IEnumerable<Transform> GetAllRootChildren()
        {
            var resourcePoolChildren = ResourcePool.Transform is { } poolTransform
                ? poolTransform.GetAllChildren()
                : Enumerable.Empty<Transform>();

            var rootChildren = TfModule.HasInstance
                ? TfModule.RootFrame.Transform.GetAllChildren()
                : Enumerable.Empty<Transform>();

            return rootChildren.Concat(resourcePoolChildren);
        }
    }
}