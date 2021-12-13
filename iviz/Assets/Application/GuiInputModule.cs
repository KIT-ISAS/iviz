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
using Iviz.Core;
using Iviz.Displays;
using Iviz.Displays.Highlighters;
using Iviz.Msgs.GeometryMsgs;
using Iviz.Resources;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.Rendering.PostProcessing;
using Pose = UnityEngine.Pose;
using Quaternion = UnityEngine.Quaternion;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using Transform = UnityEngine.Transform;
using Vector3 = UnityEngine.Vector3;

namespace Iviz.App
{
    public sealed class GuiInputModule : MonoBehaviour, ISettingsManager
    {
        const float MinShadowDistance = 10;

        const float MainSpeed = 2f;
        const float MainAccel = 5f;
        const float BrakeCoeff = 0.9f;

        const float LookAtAnimationTime = 0.3f;

        static readonly string[] QualityInViewOptions =
            { "Very Low", "Low", "Medium", "High", "Very High", "Ultra" };

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
                //config.QualityInAr = value != QualityType.Mega ? value : QualityType.Ultra;
                config.QualityInAr = ValidateQuality(value);
                if (ARController.IsActive)
                {
                    QualitySettings.SetQualityLevel((int)config.QualityInAr, true);
                    Settings.RaiseQualityTypeChanged(value);
                }
            }
        }

        public QualityType QualityInView
        {
            get => config.QualityInView;
            set
            {
                config.QualityInView = ValidateQuality(value);

                QualityType qualityToUse = Settings.IsHololens ? QualityType.Low : config.QualityInView;

                if (ARController.IsActive)
                {
                    return;
                }

                /*
                if (qualityToUse == QualityType.Mega)
                {
                    QualitySettings.SetQualityLevel((int)QualityType.Ultra, true);
                    Settings.RaiseQualityTypeChanged(qualityToUse);
                    MainCamera.renderingPath = RenderingPath.DeferredShading;
                    GetComponent<PostProcessLayer>().enabled = true;
                    return;
                }
                */

                GetComponent<PostProcessLayer>().enabled = false;
                MainCamera.renderingPath = RenderingPath.Forward;
                QualitySettings.SetQualityLevel((int)qualityToUse, true);
                Settings.RaiseQualityTypeChanged(qualityToUse);
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
            get => config.BackgroundColor;
            set
            {
                config.BackgroundColor = value.WithAlpha(1);

                Color colorToUse = Settings.IsHololens ? Color.black : value;
                MainCamera.backgroundColor = colorToUse.WithAlpha(0);

                RenderSettings.ambientSkyColor = value.WithAlpha(0);
                RenderSettings.ambientEquatorColor = value.WithValue(0.5f).WithSaturation(0.3f).WithAlpha(0);
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

        public static void TryUnsetDraggedObject(IScreenDraggable draggable)
        {
            // do not fetch Instance here, we may be shutting down the scene
            if (instance == null || instance.DraggedObject != draggable)
            {
                return;
            }

            instance.DraggedObject = null;
        }

        public static void TrySetDraggedObject(IScreenDraggable draggable)
        {
            if (!IsDraggingAllowed)
            {
                // reject!
                return;
            }

            Instance.DraggedObject = draggable;
        }

        void UpdateEvenIfInactive()
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
                Ray pointerRay = Settings.MainCamera.ScreenPointToRay(pointerPosition);
                DraggedObject.OnPointerMove(pointerRay);
                if (DraggedObject.ReferencePoint is { } referencePoint
                    && DraggedObject.ReferenceNormal is { } referenceNormal)
                {
                    Leash.Set(pointerRay, referencePoint, referenceNormal);
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
            var eventSystem = EventSystem.current;
            var results = new List<RaycastResult>();
            eventSystem.RaycastAll(new PointerEventData(eventSystem) { position = pointerPosition }, results);
            return results.Any(result => result.gameObject.layer == LayerType.UI);
        }

        void StartOrbiting()
        {
            var diff = orbitCenter - Transform.position;
            orbitRadius = Mathf.Min(5, diff.magnitude);
            orbitX = Mathf.Atan2(diff.x, diff.z) * Mathf.Rad2Deg;
            orbitY = -Mathf.Atan2(diff.y, new Vector2(diff.x, diff.z).magnitude) * Mathf.Rad2Deg;

            var q = Quaternion.Euler(orbitY, orbitX, 0);
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
            const float minDistanceToAdvance = 0.5f;

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
                if (orbitRadius < minDistanceToAdvance)
                {
                    // move forward
                    float diff = minDistanceToAdvance - orbitRadius;
                    orbitCenter.AddInPlace(diff * (q * Vector3.forward));
                    orbitRadius = minDistanceToAdvance;
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

        public void LookAt(Transform targetTransform, Vector3? localOffset = null)
        {
            lookAtTokenSource?.Cancel();
            CancellationTokenSource newToken = new();
            lookAtTokenSource = newToken;

            void Update(float t)
            {
                var lookAtCameraStartPose = Transform.AsPose();
                var targetPosition = localOffset is { } validatedOffset
                    ? targetTransform.TransformPoint(validatedOffset)
                    : targetTransform.position;

                var lookAtCameraTargetPosition = CalculateTargetCameraPosition(targetPosition);
                var lookAtCameraTargetPose = lookAtCameraStartPose.WithPosition(lookAtCameraTargetPosition);
                var currentPose = lookAtCameraStartPose.Lerp(lookAtCameraTargetPose, Mathf.Sqrt(t));
                Transform.SetPose(currentPose);
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
            float baseScale = 1f / 0.125f * TfListener.Instance.FrameSize;
            float minDistanceLookAt = 0.5f * baseScale;
            float maxDistanceLookAt = 3.0f * baseScale;

            if (!Settings.IsPhone)
            {
                float distanceToFrame = (Transform.position - targetPosition).magnitude;
                float zoomRadius = Mathf.Min(Mathf.Max(distanceToFrame, minDistanceLookAt), maxDistanceLookAt);
                return targetPosition - Transform.forward * zoomRadius;
            }

            orbitCenter = targetPosition;
            orbitRadius = Mathf.Min(Mathf.Max(orbitRadius, minDistanceLookAt), maxDistanceLookAt);
            return -orbitRadius * (Transform.rotation * Vector3.forward) + orbitCenter;
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

        public static void TriggerEnvironmentClick(ClickInfo clickInfo, bool isShortClick = true)
        {
            if (clickInfo.TryGetRaycastResults(out var hitResults))
            {
                Vector3? maybeReferencePosition = null;
                List<IHighlightable>? hits = null;
                foreach (var (hitObject, hitPosition, _) in hitResults)
                {
                    if (!TryGetHighlightable(hitObject, out var toHighlight))
                    {
                        continue;
                    }

                    if (maybeReferencePosition is not { } referencePosition)
                    {
                        maybeReferencePosition = hitPosition;
                    }
                    else if (Vector3.Distance(hitPosition, referencePosition) > 0.25f)
                    {
                        continue;
                    }

                    hits ??= new List<IHighlightable>();
                    hits.Add(toHighlight);
                }

                switch (hits?.Count)
                {
                    case 1:
                        hits[0].Highlight();
                        return;
                    case > 1:
                        HighlightAll(hits);
                        return;
                }
            }

            Pose poseToHighlight;
            if (clickInfo.TryGetARRaycastResults(out var arHitResults))
            {
                poseToHighlight = arHitResults[0].AsPose();
            }
            else if (hitResults.Length != 0)
            {
                if (hitResults.Any(result => result.GameObject.layer == LayerType.Clickable))
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

            if (!isShortClick)
            {
                var poseStamped = new PoseStamped(
                    (tapSeq++, TfListener.FixedFrameId),
                    TfListener.RelativeToFixedFrame(poseToHighlight).Unity2RosPose()
                );
                TfListener.Instance.TapPublisher.Publish(poseStamped);
            }
        }

        static async void HighlightAll(IEnumerable<IHighlightable> hits)
        {
            var aliveHits = hits.Where(toHighlight => toHighlight.IsAlive);
            foreach (var toHighlight in aliveHits)
            {
                toHighlight.Highlight();
                await Task.Delay(250);
            }
        }

        static bool TryGetHighlightable(GameObject gameObject, out IHighlightable h)
        {
            Transform parent;
            return gameObject.TryGetComponent(out h) ||
                   (parent = gameObject.transform.parent) != null && parent.TryGetComponent(out h);
        }

        public static void PlayClickAudio(in Vector3 position)
        {
            var assetHolder = Resource.Extras.AppAssetHolder;
            AudioSource.PlayClipAtPoint(assetHolder.Click, position);
        }
    }
}