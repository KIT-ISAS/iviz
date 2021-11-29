#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Iviz.Common;
using Iviz.Controllers;
using Iviz.Controllers.TF;
using Iviz.Core;
using Iviz.Displays;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Resources;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.Rendering.PostProcessing;
using Animator = UnityEngine.Animator;
using Pose = UnityEngine.Pose;
using Quaternion = UnityEngine.Quaternion;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using Transform = UnityEngine.Transform;
using Vector3 = UnityEngine.Vector3;

// ReSharper disable ConditionIsAlwaysTrueOrFalse
// ReSharper disable HeuristicUnreachableCode

#pragma warning disable CS0162 // Unerreichbarer Code wurde entdeckt.

namespace Iviz.App
{
    public sealed class GuiInputModule : MonoBehaviour, ISettingsManager
    {
        const float MinShadowDistance = 4;

        const float MainSpeed = 2f;
        const float MainAccel = 5f;
        const float BrakeCoeff = 0.9f;

        const float LookAtAnimationTime = 0.3f;

        static readonly string[] QualityInViewOptions =
            { "Very Low", "Low", "Medium", "High", "Very High", "Ultra", "Mega" };

        static readonly string[] QualityInArOptions = { "Very Low", "Low", "Medium", "High", "Very High", "Ultra" };
        static readonly Vector3 DirectionWeight = new(1.5f, 1.5f, 1);

        static GuiInputModule? instance;
        static uint tapSeq;

        public static GuiInputModule Instance =>
            instance != null
                ? instance
                : instance = ((GuiInputModule)Settings.SettingsManager).AssertNotNull(nameof(instance));

        readonly SettingsConfiguration config = new();

        Vector3 accel;
        bool pointerIsAlreadyMoving;
        bool alreadyScaling;
        TfFrame? cameraViewOverride;
        IScreenDraggable? draggedObject;

        bool pointerMotionIsInvalid;

        CancellationTokenSource? lookAtTokenSource;

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

        static Camera MainCamera => Settings.MainCamera;

        Leash? leash;
        Leash Leash => leash != null ? leash : (leash = ResourcePool.RentDisplay<Leash>());

        Transform? mTransform;
        Transform Transform => mTransform != null ? mTransform : (mTransform = transform);

        bool IsLookAtAnimationRunning => lookAtTokenSource is { IsCancellationRequested: false };

        public event Action<ClickInfo>? PointerDown;
        public event Action<ClickInfo>? ShortClick;
        public event Action<ClickInfo>? LongClick;

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

        IScreenDraggable? DraggedObject
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

                Leash.Visible = draggedObject != null;
            }
        }

        static bool IsDraggingAllowed =>
            Settings.IsXR || (Settings.IsPhone
                //? Input.touchCount == 1
                ? Touch.activeTouches.Count == 1
                : Mouse.current.rightButton.isPressed);

        void Awake()
        {
            EnhancedTouchSupport.Enable();
            Leash.Color = Color.white.WithAlpha(0.75f);
        }

        void OnDestroy()
        {
            TfListener.Instance.ResetFrames -= OnResetFrames;
        }

        void Start()
        {
            if (!Settings.IsPhone)
            {
                QualitySettings.vSyncCount = 0;
                MainCamera.allowHDR = true;
            }

            if (!Settings.SupportsComputeBuffers)
            {
                RosLogger.Info("Platform does not support compute shaders. Point cloud rendering may look weird.");
            }

            Config = new SettingsConfiguration();

            ModuleListPanel.Instance.UnlockButton.onClick.AddListener(DisableCameraLock);

            mainLight = GameObject.Find("MainLight")?.GetComponent<Light>();

            StartOrbiting();

            ModuleListPanel.CallAfterInitialized(() => TfListener.Instance.ResetFrames += OnResetFrames);
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
                if (!Settings.IsPhone && mOrbitCenterOverride == null)
                {
                    ProcessFlying();
                }

                return;
            }

            if (Settings.IsPhone)
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

        void OnResetFrames()
        {
            CameraViewOverride = null;
            OrbitCenterOverride = null;
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
                    QualitySettings.SetQualityLevel((int)value, true);
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

                QualityType qualityToUse = Settings.IsHololens ? QualityType.Low : value;

                if (ARController.IsActive)
                {
                    return;
                }

                if (qualityToUse == QualityType.Mega)
                {
                    QualitySettings.SetQualityLevel((int)QualityType.Ultra, true);
                    Settings.RaiseQualityTypeChanged(qualityToUse);
                    MainCamera.renderingPath = RenderingPath.DeferredShading;
                    GetComponent<PostProcessLayer>().enabled = true;
                    return;
                }

                GetComponent<PostProcessLayer>().enabled = false;
                MainCamera.renderingPath = RenderingPath.Forward;
                QualitySettings.SetQualityLevel((int)qualityToUse, true);
                Settings.RaiseQualityTypeChanged(qualityToUse);
            }
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
            get => config.BackgroundColor;
            set
            {
                config.BackgroundColor = value.WithAlpha(1);

                Color colorToUse = Settings.IsHololens ? Color.black : value;
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

        public void TryUnsetDraggedObject(IScreenDraggable draggable)
        {
            if (DraggedObject == draggable)
            {
                DraggedObject = null;
            }
        }

        public void TrySetDraggedObject(IScreenDraggable draggable)
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

            if (Settings.IsXR) // XR doesn't need cursor management
            {
                return;
            }

            /*
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
            */

            if (Settings.IsPhone)
            {
                var activeTouches = Touch.activeTouches;

                prevPointerDown |= altPointerIsDown;

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
                Ray pointerRay = Settings.MainCamera.ScreenPointToRay(pointerPosition);
                DraggedObject.OnPointerMove(pointerRay);
                if (DraggedObject.ReferencePoint is { } referencePoint)
                {
                    Leash.Set(pointerRay, referencePoint);
                }

                return;
            }

            // check if we are clicking something interesting
            if (pointerIsOnGui)
            {
                return;
            }

            bool anyPointerDown = pointerIsDown || altPointerIsDown;
            if (!prevPointerDown && anyPointerDown)
            {
                var clickInfo = new ClickInfo(pointerPosition);
                PointerDown?.Invoke(clickInfo);
            }

            if (prevPointerDown
                && !anyPointerDown
                && Vector2.Distance(pointerPosition, pointerDownStart) < maxDistanceForClickEvent)
            {
                var clickInfo = new ClickInfo(pointerPosition);
                float timeDown = Time.time - pointerDownTime;
                if (timeDown < shortClickTime)
                {
                    ShortClick?.Invoke(clickInfo);
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
            EventSystem eventSystem = EventSystem.current;
            var results = new List<RaycastResult>();
            eventSystem.RaycastAll(new PointerEventData(eventSystem) { position = pointerPosition }, results);
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

            var pointerDiff = pointerIsAlreadyMoving
                ? pointerPosition - lastPointerPosition
                : Vector2.zero;

            lastPointerPosition = pointerPosition;
            pointerIsAlreadyMoving = true;

            const float orbitCoeff = 0.1f;
            const float orbitRadiusAdvance = 0.1f;

            orbitX += pointerDiff.x * orbitCoeff;
            orbitY -= pointerDiff.y * orbitCoeff;

            orbitY = orbitY switch
            {
                > 90 => 90,
                < -90 => -90,
                _ => orbitY
            };

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

            Transform t = Transform;
            Quaternion q = t.rotation;

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
                    orbitCenter.AddInPlace(diff * (q * Vector3.forward));
                    orbitRadius = 0.5f;
                }

                float orbitScale = 0.75f * orbitRadius;
                orbitCenter -= tangentCoeff * pointerAltDiff.x * orbitScale *
                               t.TransformDirection(Vector3.right);
                orbitCenter += tangentCoeff * pointerAltDiff.y * orbitScale *
                               t.TransformDirection(Vector3.down);
            }

            t.position = -orbitRadius * (q * Vector3.forward) + orbitCenter;
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

            Vector3 speed = deltaTime * baseInput.Mult(MainSpeed * DirectionWeight);

            accel.AddInPlace(baseInput.Mult(MainAccel * DirectionWeight) * deltaTime);
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

            speed.AddInPlace(deltaTime * accel);

            Transform.position += Transform.rotation * speed;
        }

        public void LookAt(Transform targetTransform)
        {
            Vector3 CalculateTargetCameraPosition()
            {
                var position = targetTransform.position;

                float minDistanceLookAt = 0.5f / 0.125f * TfListener.Instance.FrameSize;
                float maxDistanceLookAt = 3.0f / 0.125f * TfListener.Instance.FrameSize;

                if (!Settings.IsPhone)
                {
                    float distanceToFrame = (Transform.position - position).magnitude;
                    float zoomRadius = Mathf.Min(Mathf.Max(distanceToFrame, minDistanceLookAt), maxDistanceLookAt);
                    return position - Transform.forward * zoomRadius;
                }
                else
                {
                    orbitCenter = position;
                    orbitRadius = Mathf.Min(Mathf.Max(orbitRadius, minDistanceLookAt), maxDistanceLookAt);
                    return -orbitRadius * (Transform.rotation * Vector3.forward) + orbitCenter;
                }
            }


            lookAtTokenSource?.Cancel();
            var newToken = lookAtTokenSource = new CancellationTokenSource();
            
            Core.FAnimator.Spawn(lookAtTokenSource.Token, LookAtAnimationTime, t =>
            {
                var lookAtCameraStartPose = Transform.AsPose();
                var lookAtCameraTargetPose = lookAtCameraStartPose.WithPosition(CalculateTargetCameraPosition());
                if (t >= 1)
                {
                    newToken.Cancel();
                    Transform.SetPose(lookAtCameraTargetPose);
                    if (OrbitCenterOverride != null)
                    {
                        StartOrbiting();
                    }
                }
                else
                {
                    var currentPose = lookAtCameraStartPose.Lerp(lookAtCameraTargetPose, Mathf.Sqrt(t));
                    Transform.SetPose(currentPose);
                }
            });
        }

        static Vector3Int GetBaseInput()
        {
            var pVelocity = new Vector3Int();
            if (Keyboard.current[Key.W].isPressed)
            {
                pVelocity.z++;
            }

            if (Keyboard.current[Key.S].isPressed)
            {
                pVelocity.z--;
            }

            if (Keyboard.current[Key.A].isPressed)
            {
                pVelocity.x--;
            }

            if (Keyboard.current[Key.D].isPressed)
            {
                pVelocity.x++;
            }

            if (Keyboard.current[Key.Q].isPressed)
            {
                pVelocity.y--;
            }

            if (Keyboard.current[Key.E].isPressed)
            {
                pVelocity.y++;
            }

            return pVelocity;
        }

        void OnClick(ClickInfo clickInfo, bool isShortClick)
        {
            TriggerEnvironmentClick(clickInfo, isShortClick);
        }

        public static void TriggerEnvironmentClick(ClickInfo clickInfo, bool isShortClick)
        {
            if (clickInfo.TryGetRaycastResults(out var hitResults))
            {
                Vector3 hitPoint = hitResults[0].Position;
                bool anyHighlighted = false;
                foreach (var (hitObject, position, _) in hitResults)
                {
                    if (Vector3.Distance(position, hitPoint) > 1
                        || !TryGetHighlightable(hitObject, out var toHighlight))
                    {
                        continue;
                    }

                    toHighlight.Highlight();
                    anyHighlighted = true;
                }

                if (anyHighlighted)
                {
                    return;
                }
            }

            Pose poseToHighlight;
            if (clickInfo.TryGetARRaycastResults(out var arHitResults))
            {
                poseToHighlight = arHitResults[0].CreatePose();
            }
            else if (hitResults.Length != 0)
            {
                poseToHighlight = hitResults[0].CreatePose();
            }
            else
            {
                return;
            }

            FAnimator.Start(new ClickedPoseHighlighter(poseToHighlight));
            
            if (!isShortClick)
            {
                var poseStamped = new PoseStamped(
                    (tapSeq++, TfListener.FixedFrameId),
                    TfListener.RelativePoseToFixedFrame(poseToHighlight).Unity2RosPose()
                );
                TfListener.Instance.TapPublisher.Publish(poseStamped);
            }
        }
        
        static bool TryGetHighlightable(GameObject gameObject, out IHighlightable h)
        {
            Transform parent;
            return gameObject.TryGetComponent(out h) ||
                   (parent = gameObject.transform.parent) != null && parent.TryGetComponent(out h);
        }
    }
}